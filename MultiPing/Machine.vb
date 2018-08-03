Imports System.Xml
Imports System.Xml.Serialization

Public Class Machine
    Private _ipTable As DataTable
    Private _row As DataRow
    Private _dropsInARow As Integer
    Private Shared _threadWait As New Threading.AutoResetEvent(True)

    <XmlAttribute("IP")> Property IP As String
    <XmlAttribute("Device")> Property Device As String
    <XmlIgnore()> Property IsDead As Boolean
    <XmlIgnore()> Property RemoveIfDead As Boolean
    <XmlIgnore()> Property MaxDropsInARow As Integer = 20
    <XmlIgnore()> Property Latency As Integer
    '<XmlIgnore()> Property TimeOut As Integer = 4000
    <XmlIgnore()> Property AverageLatency As Double
    <XmlIgnore()> Property HighestLatency As Integer
    <XmlIgnore()> Property DropData As New DataSet
    <XmlIgnore()> Property DropForm As Drops

    <XmlElement("LatencyList")> Property LatencyList As New List(Of Integer)
    <XmlElement("LatencyTimeList")> Property LatencyTimeList As New List(Of Date)
    <XmlElement("DropTimeList")> Property DropTimeList As New List(Of Date)
    <XmlElement("DropList")> Property DropList As New List(Of Boolean)

    Public Shared SaveingResults As Boolean = False
    <XmlIgnore()> Public Shared Property LoadWait As New Threading.ManualResetEvent(True)

    <XmlIgnore()> Private PingThread As Threading.Thread

    Public Sub New()
    End Sub

    Public Sub New(ByVal ipTable As DataTable, ByVal device As String, ByVal ip As String)
        Me.IP = ip
        Me.Device = device
        Initialize(ipTable)
    End Sub

    Public Sub Initialize(ipTable As DataTable, Optional ByVal ActivatePingThread As Boolean = True)
        _ipTable = ipTable
        _row = _ipTable.NewRow
        _row("Device") = Device
        _row("IP") = IP
        _row("Status") = "Checking"
        _row("Latency") = "-"
        _row("Pings") = 0
        _row("Drops") = 0
        _row("FirstResponded") = "Never"
        _row("LastResponded") = "Never"
        _ipTable.Rows.Add(_row)

        'DropData.Tables.Add(New DataTable("DropData"))
        'With DropData.Tables("DropData").Columns
        '    .Add("Time of Drop")
        'End With

        'Dim thread As New Threading.Thread(AddressOf Me.PingItThread)
        If ActivatePingThread Then
            PingThread = New Threading.Thread(AddressOf Me.PingItThread)
            PingThread.SetApartmentState(Threading.ApartmentState.STA)
            PingThread.IsBackground = True
            PingThread.Start()
        End If
    End Sub

    Public Sub Kill()
        PingThread.Abort()
    End Sub

    Public Function ShouldSerializeLatencyList() As Boolean
        Return SaveingResults
    End Function

    Public Function ShouldSerializeLatencyTimeList() As Boolean
        Return SaveingResults
    End Function

    Public Function ShouldSerializeDropTimeList() As Boolean
        Return SaveingResults
    End Function

    Public Function ShouldSerializeDropList() As Boolean
        Return SaveingResults
    End Function

    Private Sub PingItThread()
        Dim pingSender As New System.Net.NetworkInformation.Ping
        Do
            Try
                Dim reply As System.Net.NetworkInformation.PingReply = pingSender.Send(_row("IP"), Main.TimeoutTime)
                If _threadWait.WaitOne(-1) And Machine.LoadWait.WaitOne(-1) Then
                    _row.BeginEdit()
                    Select Case reply.Status
                        Case Net.NetworkInformation.IPStatus.Success
                            Me.RemoveIfDead = False
                            ' Me.TimeOut = 4000
                            Me.Latency = reply.RoundtripTime
                            LatencyList.Add(Me.Latency)
                            LatencyTimeList.Add(Now)
                            DropTimeList.Add(Now)
                            DropList.Add(False)
                            'My.Computer.FileSystem.WriteAllText("C:\" & Me.IP.Replace(".", "_") & ".txt", Me.Latency & "," & Now & vbCrLf, True)
                            If Me.Latency > Me.HighestLatency Then Me.HighestLatency = Me.Latency
                            Me.AverageLatency = (Me.AverageLatency * 0.9) + (Me.Latency * 0.1)
                            _row("Latency") = "  Current:" & Me.Latency.ToString("000") & " Average: (" & CInt(Me.AverageLatency).ToString("000") & ") Highest: [" & Me.HighestLatency.ToString("000") & "]  "
                            If _row("Status") = "Dead" Then _row("Drops") = 0
                            _row("Status") = "Active"
                            If _row("FirstResponded") = "Never" Or Me.IsDead Then _row("FirstResponded") = Now
                            Me.IsDead = False
                            _row("LastResponded") = Now
                            _row("Pings") = Me.LatencyTimeList.Count + Val(_row("Drops"))
                            _dropsInARow = 0
                            _row.EndEdit()
                            _threadWait.Set()
                            Threading.Thread.Sleep(1000)
                        Case Else
                            _dropsInARow += 1
                            DropTimeList.Add(Now)
                            DropList.Add(True)
                            _row("Latency") = "  Current:--- Average: (" & CInt(Me.AverageLatency).ToString("000") & ") Highest: [" & Me.HighestLatency.ToString("000") & "]  "
                            If _dropsInARow > Me.MaxDropsInARow Then
                                Me.IsDead = True
                                If Me.RemoveIfDead Then
                                    _row("Status") = "Not Found"
                                    _row("Drops") = "-"
                                    _row.EndEdit()
                                    _threadWait.Set()
                                    Exit Do
                                End If
                                _row("Status") = "Dead"
                                _row("Pings") = Me.LatencyTimeList.Count + Val(_row("Drops"))
                                _row("Drops") = "-"
                                _row.EndEdit()
                                _threadWait.Set()
                                Threading.Thread.Sleep(60000) 'Only check every 60 seconds for the device to come back up.
                            Else
                                _row("Drops") += 1
                                _row("Pings") = Me.LatencyTimeList.Count + Val(_row("Drops"))
                                _row.EndEdit()
                                _threadWait.Set()
                                Threading.Thread.Sleep(1000)
                            End If
                            UpdateDropDataSet(Now)
                    End Select
                End If
            Catch ex As System.Net.NetworkInformation.PingException
                _row("Status") = "Ping Error"
                _threadWait.Set()
                Exit Do
            Catch ex As Exception
                If _row.RowState = DataRowState.Detached Then
                    _threadWait.Set()
                    Exit Do
                Else
                    If ex.InnerException IsNot Nothing Then
                        _row("Status") = ex.InnerException.Message
                    Else
                        _row("Status") = ex.Message
                    End If
                End If
            End Try
        Loop
    End Sub

    Public ReadOnly Property TotalPings() As Integer
        Get
            Return LatencyTimeList.Count + DropPings
        End Get
    End Property

    Public ReadOnly Property DropPings As Integer
        Get
            Dim dropcount As Integer = 0
            For i As Integer = 0 To DropList.Count - 1
                If DropList(i) Then dropcount += 1
            Next
            Return dropcount
        End Get
    End Property

    Public Sub UpdateTotalPings()
        _row("Pings") = TotalPings
    End Sub

    Public Sub UpdateDrops()
        _row("Drops") = DropPings
    End Sub

    Private Sub UpdateDropDataSet(ByVal dropTime As Date)
        Dim row As DataRow
        row = Me.DropData.Tables("DropData").NewRow
        If Me.IsDead Then
            row("Time of Drop") = "DEAD"
        Else
            row("Time of Drop") = dropTime
        End If
        Me.DropData.Tables("DropData").Rows.Add(row)
    End Sub
End Class
