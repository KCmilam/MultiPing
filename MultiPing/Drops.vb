Imports System.Threading

Public Class Drops
    Private _dropData As DataSet
    Private _machine As Machine
    Private _gridUpdateThread As Thread
    Private _performUpdate As Boolean = True

    Private Delegate Sub UpdateGridCallBackDelegate()

    Public Sub GenerateNewDropList(ByRef machine As Machine)
        _machine = machine
        Me.Text = machine.IP
        DropGrid.DataSource = _machine.DropData.Tables("DropData")
        _machine.DropForm = Me

        _gridUpdateThread = New Thread(AddressOf UpdateDropListDataThread)
        _gridUpdateThread.IsBackground = True
        _gridUpdateThread.Start()

        'Dim timer = New Timer
        'timer.Interval = 1500
        'timer.Enabled = True
        'timer.Start()
        'AddHandler timer.Tick, AddressOf updateDropListData
        'DropData = Nothing
        'DropData = New DataSet
        'machine.dataTable = New DataTable("DropData")
        'DropData.Tables.Add(machine.dataTable)
        'With DropData.Tables("DropData").Columns
        '    .Add("Time of Drop")
        'End With

        'Dim row As DataRow
        'For Each dt As Date In machine.dropList
        '    row = DropData.Tables("DropData").NewRow
        '    row("Time of Drop") = dt
        '    DropData.Tables("DropData").Rows.Add(row)
        'Next

        ''add an indicator if the clock is dead
        'If machine.isDead Then
        '    row = DropData.Tables("DropData").NewRow
        '    row("Time of Drop") = "DEAD"
        '    DropData.Tables("DropData").Rows.Add(row)
        'End If
    End Sub

    Public Sub UpdateDropListDataThread()
        While _performUpdate
            Me.BeginInvoke(New UpdateGridCallBackDelegate(AddressOf UpdateGrid))
            Thread.Sleep(200)
        End While
        DropGrid.Update()
        System.Diagnostics.Debug.WriteLine("Timer should be updating...")
    End Sub

    Private Sub UpdateGrid()
        DropGrid.Update()
        DropGrid.Refresh()
    End Sub

    Private Sub Drops_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        _gridUpdateThread.Abort()
    End Sub
End Class