Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Threading

Public Class Chart
    Private _chartUpdateThread As Thread
    Private _loadFileList As List(Of String)
    Private _timeStep As Double = 2

    Private Delegate Sub UpdateChartCallBackDelegate(ByVal path As String)

    Property Machine As Machine

    Private Sub Chart_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UpTimeChart.BorderlineDashStyle = DataVisualization.Charting.ChartDashStyle.Solid

        Dim enumValues As Array = System.[Enum].GetValues(GetType(SeriesChartType))
        For Each eChartType As SeriesChartType In enumValues
            ChartTypeComboBox.Items.Add(eChartType)
        Next

    End Sub

    Public Sub InitalizeWithData(ByVal machine As Machine)
        Me.Machine = machine
        UpTimeChart.Series.Clear()
        UpTimeChart.Titles.Add("Ping Times for Item (in Milliseconds)")
        Dim series As New Series
        series.Name = "Latency"
        series.XValueType = ChartValueType.Date
        series.ChartType = SeriesChartType.FastLine
        For i As Integer = 0 To machine.LatencyList.Count - 1
            series.Points.AddXY(machine.LatencyTimeList(i).Hour & ":" & machine.LatencyTimeList(i).Minute & ":" & machine.LatencyTimeList(i).Second, machine.LatencyList(i))
        Next
        series.MarkerStyle = MarkerStyle.Diamond
        series.SmartLabelStyle.CalloutLineWidth = 5
        UpTimeChart.Series.Add(series)

        Dim dropSeries As New Series
        dropSeries.Name = "Drops"
        dropSeries.Color = Color.Red
        dropSeries.ChartType = SeriesChartType.FastLine
        dropSeries.MarkerSize = 10
        dropSeries.XValueType = ChartValueType.Date
        For i As Integer = 0 To machine.DropTimeList.Count - 1
            If machine.DropList(i) Then
                dropSeries.Points.AddXY(machine.DropTimeList(i).Hour & ":" & machine.DropTimeList(i).Minute & ":" & machine.DropTimeList(i).Second, 5)
            Else
                dropSeries.Points.AddXY(machine.DropTimeList(i).Hour & ":" & machine.DropTimeList(i).Minute & ":" & machine.DropTimeList(i).Second, 0)
            End If
        Next
        dropSeries.MarkerStyle = MarkerStyle.Diamond
        dropSeries.SmartLabelStyle.CalloutLineWidth = 5
        UpTimeChart.Series.Add(dropSeries)
        'Dim view As New DataView(machine.dataTable)
    End Sub

    Private Sub LoadPreviousDataButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadPreviousDataButton.Click
        Dim dialog As New OpenFileDialog
        If dialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            'Threading.ThreadPool.QueueUserWorkItem(AddressOf ChartLoadThread, dialog.FileName)
            Machine.SaveingResults = True
            For Each mMachine As Machine In MachineList.Open(dialog.FileName).Machines
                If mMachine.IP = Machine.IP Then
                    Me.Machine = mMachine
                    RefreshList()
                    Exit For
                End If
            Next
            Machine.SaveingResults = False
        End If
    End Sub

    Private Sub RefreshListButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshListButton.Click
        UpTimeChart.Series.Clear()
        Me.RefreshList()
    End Sub

    Private Sub RefreshList()
        UpTimeChart.Series.Clear()

        Dim dropSeries As New Series
        dropSeries.Name = "Drops"
        dropSeries.Color = Color.Red
        dropSeries.ChartType = SeriesChartType.FastLine
        dropSeries.MarkerSize = 10
        dropSeries.XValueType = ChartValueType.Date
        For i As Integer = 0 To Machine.DropTimeList.Count - 1
            If Machine.DropList(i) Then
                dropSeries.Points.AddXY(Machine.DropTimeList(i).Hour & ":" & Machine.DropTimeList(i).Minute & ":" & Machine.DropTimeList(i).Second, 5)
            Else
                dropSeries.Points.AddXY(Machine.DropTimeList(i).Hour & ":" & Machine.DropTimeList(i).Minute & ":" & Machine.DropTimeList(i).Second, 0)
            End If

        Next
        dropSeries.MarkerStyle = MarkerStyle.Diamond
        dropSeries.SmartLabelStyle.CalloutLineWidth = 5
        UpTimeChart.Series.Add(dropSeries)

        Dim series As New Series
        series.ChartType = SeriesChartType.FastLine
        series.Name = "Latency"
        series.XValueType = ChartValueType.Date
        For i As Integer = 0 To Machine.LatencyList.Count - 1
            series.Points.AddXY(Machine.LatencyTimeList(i).Hour & ":" & Machine.LatencyTimeList(i).Minute & ":" & Machine.LatencyTimeList(i).Second, Machine.LatencyList(i))
        Next
        series.MarkerStyle = MarkerStyle.Diamond
        series.SmartLabelStyle.CalloutLineWidth = 5
        UpTimeChart.Series.Add(series)

        DatesComboBox.Visible = False
        DateRangeLabel.Visible = False
    End Sub

    Private Sub ChartLoadThread(ByVal path As String)
        Dim file As New IO.StreamReader(path)
        Dim stringList As New List(Of String)

        Do
            stringList.Add(file.ReadLine.Trim)
        Loop Until file.EndOfStream

        file.Close()
        _loadFileList = stringList

        Me.BeginInvoke(New UpdateChartCallBackDelegate(AddressOf UpdateChart), New Object() {path})
    End Sub

    Private Sub UpdateChart(ByVal path As String)
        UpTimeChart.Series.Clear()
        Dim series As New Series
        series.ChartType = SeriesChartType.FastLine
        Dim newName As String = IO.Path.GetFileName(path)
        series.Name = newName.Replace(".txt", "").Replace("_", ".")

        Try
            Try
                RemoveHandler DatesComboBox.SelectedIndexChanged, AddressOf DatesComboBox_SelectedIndexChanged 'remove the handler so we don't accidentally fire an index changed event while loading data
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine("Handler was never added, can't remove it.")
            End Try
            DatesComboBox.Items.Clear() 'clear the combo box since we are loading a new file

            Dim firstDateInFile As Date = _loadFileList(0).Split(",")(1) 'record the first date so we may fill the combo box with it
            DatesComboBox.Items.Add("Show All")
            DatesComboBox.Items.Add(firstDateInFile)
            DatesComboBox.SelectedIndex = 0

            AddHandler DatesComboBox.SelectedIndexChanged, AddressOf DatesComboBox_SelectedIndexChanged 'add the handler back, safe to do so now

            Dim lastDateRead As Date = firstDateInFile
            Dim lastIntervalChecked As Date = firstDateInFile
            For Each item As String In _loadFileList 'parse the loadfilelist
                Dim latency As String = item.Split(",")(0)
                Dim dropTime As Date = item.Split(",")(1)
                If dropTime.Day <> lastDateRead.Day Then
                    DatesComboBox.Items.Add(dropTime)
                    lastDateRead = dropTime
                End If
                'going to chew through some data here for averaging
                Dim newTime As Date = lastIntervalChecked.AddSeconds(_timeStep)
                If dropTime >= newTime Then
                    series.Points.AddXY(dropTime.Hour & ":" & dropTime.Minute & ":" & dropTime.Second, latency)
                    lastIntervalChecked = dropTime
                End If

            Next
            series.MarkerStyle = MarkerStyle.Diamond
            series.XValueType = ChartValueType.Date
            series.SmartLabelStyle.CalloutLineWidth = 5
            UpTimeChart.Series.Add(series)
            DatesComboBox.Visible = True
            DateRangeLabel.Visible = True
        Catch ex As Exception
            MessageBox.Show("Error reading the file.  The file format is not correct." & ex.ToString)
        End Try

    End Sub

    Private Sub DatesComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            UpTimeChart.Series.Clear()
            Dim series As New Series
            series.ChartType = SeriesChartType.FastLine
            If DatesComboBox.SelectedItem.ToString = "Show All" Then
                Dim lastIntervalChecked As Date = _loadFileList.Item(0).Split(",")(1)
                For Each item As String In _loadFileList
                    Dim latency As String = item.Split(",")(0)
                    Dim currDt As Date = item.Split(",")(1)
                    Dim newTime As Date = lastIntervalChecked.AddSeconds(_timeStep)
                    If currDt >= newTime Then
                        series.Points.AddXY(currDt.Hour & ":" & currDt.Minute & ":" & currDt.Second & vbNewLine & currDt.Month & "/" & currDt.Day & "/" & currDt.Year, latency)
                        lastIntervalChecked = currDt
                    End If
                Next
            Else
                Dim newDT As Date = DatesComboBox.SelectedItem
                Dim lastIntervalChecked As Date = newDT
                For Each item As String In _loadFileList
                    Dim latency As String = item.Split(",")(0)
                    Dim currDt As Date = item.Split(",")(1)
                    If currDt.Day = newDT.Day AndAlso currDt.Month = newDT.Month AndAlso currDt.Year = newDT.Year Then
                        Dim newTime As Date = lastIntervalChecked.AddSeconds(_timeStep)
                        If currDt >= newTime Then
                            series.Points.AddXY(currDt.Hour & ":" & currDt.Minute & ":" & currDt.Second & vbNewLine & currDt.Month & "/" & currDt.Day & "/" & currDt.Year, latency)
                            lastIntervalChecked = currDt
                        End If
                    End If

                Next
            End If
            series.MarkerStyle = MarkerStyle.Diamond
            series.XValueType = ChartValueType.Date
            series.SmartLabelStyle.CalloutLineWidth = 5
            UpTimeChart.Series.Add(series)
        Catch ex As Exception
            MessageBox.Show("An error parsing the file ocurred." & ex.ToString)
        End Try

    End Sub

    'repositioning method for the combo box in the event the form is resized
    Private Sub Chart_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Dim newPT As Point = DatesComboBox.Location
        newPT.X = SplitContainer1.Panel2.Width / 2 - DatesComboBox.Width / 2
        DatesComboBox.Location = newPT
        newPT.Y = DateRangeLabel.Location.Y
        DateRangeLabel.Location = newPT

        newPT = ChartTypeComboBox.Location
        newPT.X = SplitContainer1.Panel2.Width / 2 - ChartTypeComboBox.Width / 2
        ChartTypeComboBox.Location = newPT
        newPT.Y = ChartTypeLabel.Location.Y
        ChartTypeLabel.Location = newPT

        newPT = TimeStepComboBox.Location
        newPT.X = SplitContainer1.Panel2.Width / 2 - TimeStepComboBox.Width / 2
        TimeStepComboBox.Location = newPT
        newPT.Y = TimeStepLabel.Location.Y
        TimeStepLabel.Location = newPT
    End Sub

    Private Sub ChartTypeComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChartTypeComboBox.SelectedIndexChanged
        Dim currCollection As SeriesCollection = UpTimeChart.Series
        Dim series As Series = Nothing
        For Each item As Series In currCollection
            series = item
        Next

        series.ChartType = DirectCast([Enum].Parse(GetType(SeriesChartType), ChartTypeComboBox.Text), SeriesChartType)
        UpTimeChart.Series.Clear()
        UpTimeChart.Series.Add(series)
    End Sub

    Private Sub TimeStepComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimeStepComboBox.SelectedIndexChanged
        Try
            _timeStep = CInt(TimeStepComboBox.SelectedItem)
            UpTimeChart.Series.Clear()
            Dim series As New Series
            series.ChartType = SeriesChartType.FastLine
            If DatesComboBox.SelectedItem.ToString = "Show All" Then
                Dim lastIntervalChecked As Date = _loadFileList.Item(0).Split(",")(1)
                For Each item As String In _loadFileList
                    Dim latency As String = item.Split(",")(0)
                    Dim currDt As Date = item.Split(",")(1)
                    Dim newTime As Date = lastIntervalChecked.AddSeconds(_timeStep)
                    If currDt >= newTime Then
                        series.Points.AddXY(currDt.Hour & ":" & currDt.Minute & ":" & currDt.Second & vbNewLine & currDt.Month & "/" & currDt.Day & "/" & currDt.Year, latency)
                        lastIntervalChecked = currDt
                    End If
                Next
            Else
                Dim newDT As Date = DatesComboBox.SelectedItem
                Dim lastIntervalChecked As Date = newDT
                For Each item As String In _loadFileList
                    Dim latency As String = item.Split(",")(0)
                    Dim currDt As Date = item.Split(",")(1)
                    If currDt.Day = newDT.Day AndAlso currDt.Month = newDT.Month AndAlso currDt.Year = newDT.Year Then
                        Dim newTime As DateTime = lastIntervalChecked.AddSeconds(_timeStep)
                        If currDt >= newTime Then
                            series.Points.AddXY(currDt.Hour & ":" & currDt.Minute & ":" & currDt.Second & vbNewLine & currDt.Month & "/" & currDt.Day & "/" & currDt.Year, latency)
                            lastIntervalChecked = currDt
                        End If
                    End If
                Next
            End If
            series.MarkerStyle = MarkerStyle.Diamond
            series.XValueType = ChartValueType.Date
            series.SmartLabelStyle.CalloutLineWidth = 5
            UpTimeChart.Series.Add(series)
        Catch ex As Exception
            MessageBox.Show("An error parsing the file ocurred." & ex.ToString)
        End Try
    End Sub
End Class