Imports System.Text.RegularExpressions

Public Class Main
    Private _ipData As DataSet
    Private _dropForm As New Drops
    Private _chartForm As New Chart
    Private _browser As New SwitchBrowser
    'Private _machineList As New List(Of Machine)

    Public Shared TimeoutTime As Integer = 4000

    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        _ipData = New DataSet
        _ipData.Tables.Add(New DataTable("IPData"))
        With (_ipData.Tables("IPData").Columns)
            .Add("Device")
            .Add("IP")
            .Add("Status")
            .Add("Latency")
            .Add("Pings")
            .Add("Drops")
            .Add("FirstResponded")
            .Add("LastResponded")
        End With
        IPGrid.DataSource = _ipData.Tables("IPData")
        IPGrid.Columns(0).Width = 122
        IPGrid.Columns(1).Width = 118
        IPGrid.Columns(2).Width = 88
        IPGrid.Columns(3).Width = 253
        IPGrid.Columns(4).Width = 45
        IPGrid.Columns(5).Width = 45
        IPGrid.Columns(6).Width = 142
        IPGrid.Columns(7).Width = 177

        For i As Integer = 0 To MachineList.Instance.Machines.Count - 1
            Dim Machine As Machine = MachineList.Instance.Machines(i)
            AddMachine(Machine.Device, Machine.IP, False)
        Next
        lvMachines.ExpandAll()
    End Sub


    Private Sub CommonItemsTreeView_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvMachines.DoubleClick
        If lvMachines.SelectedNode.Tag <> String.Empty Then
            AddMachine(lvMachines.SelectedNode.Text, lvMachines.SelectedNode.Tag.ToString, False)
        End If
        For Each ChildNode As TreeNode In lvMachines.SelectedNode.Nodes
            If ChildNode.Tag <> String.Empty Then
                AddMachine(ChildNode.Text, ChildNode.Tag.ToString, 2000, False)
            End If
        Next
        lvMachines.ExpandAll()
    End Sub

    Private Sub AddMachine(ByVal device As String, ByVal ip As String, ByVal removeIfDead As Boolean, Optional ByVal ActivatePingThread As Boolean = True)
        Dim machineExists As Boolean = False
        Dim machine As Machine = Nothing
        For Each row As DataRow In _ipData.Tables("IPData").Rows
            If row("IP") = ip Then
                machineExists = True
            End If
        Next
        For Each mMachine As Machine In MachineList.Instance.Machines
            If mMachine.IP = ip Then
                machine = mMachine
                Exit For
            End If
        Next

        If machine Is Nothing Then machine = MachineList.AddMachine(device, ip)

        machine.Initialize(_ipData.Tables("IPData"), ActivatePingThread)
        'machine.TimeOut = Main.TimeoutTime
        If removeIfDead Then
            machine.RemoveIfDead = removeIfDead
            machine.MaxDropsInARow = 3
        End If
        With lvMachines.Nodes(0).Nodes.Add(device)
            .Tag = machine
        End With
    End Sub

    Private Sub IPGrid_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs)
        Select Case e.Context
            Case Else
        End Select
        e.ThrowException = False
    End Sub

    Private Sub IPGrid_RowHeaderMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs)
        If IPGrid.CurrentRow IsNot Nothing AndAlso Not IPGrid.CurrentRow.IsNewRow Then
            Dim machine As Machine = MachineList.Instance.Machines.ElementAt(IPGrid.CurrentRow.Index)
            System.Diagnostics.Debug.WriteLine(machine.IP)
            InitializeDropForm(machine)
        End If
    End Sub

    Private Sub IPGrid_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles IPGrid.RowHeaderMouseClick
        If e.Button = MouseButtons.Right Then
            If IPGrid.CurrentRow IsNot Nothing AndAlso Not IPGrid.CurrentRow.IsNewRow Then
                Dim machine As Machine = MachineList.Instance.Machines.ElementAt(IPGrid.CurrentRow.Index)
                menuStrip.Text = machine.IP.ToString
                menuStrip.Show(MousePosition)
            End If
        End If
    End Sub

    'Private Sub IPGrid_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs)
    '    If IPGrid.CurrentRow IsNot Nothing AndAlso Not IPGrid.CurrentRow.IsNewRow Then
    '        Dim machine As Machine = MachineList.Instance.Machines.ElementAt(IPGrid.CurrentRow.Index)
    '        System.Diagnostics.Debug.WriteLine(machine.IP)
    '        _machineList.Remove(machine)
    '    End If
    'End Sub

    Private Sub InitializeDropForm(ByVal machine As Machine)
        If Not _dropForm Is Nothing Then
            _dropForm.Dispose()
        End If
        If machine.DropTimeList.Count > 0 Then
            _dropForm = New Drops
            _dropForm.Show()
            'Dim position As Point = Me.Location
            'position.X = Me.Location.X + Me.Width
            ' _dropForm.Location = position
            _dropForm.GenerateNewDropList(machine)
        End If
    End Sub

    Private Sub OpenItemInBrowserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenItemInBrowserToolStripMenuItem.Click
        If Not _browser Is Nothing Then
            _browser.Dispose()
        End If
        _browser = New SwitchBrowser
        _browser.Show()
        Dim position As Point = Me.Location
        position.X = Me.Location.X - _browser.Width
        _browser.Location = position
        _browser.SetURL("http://" & menuStrip.Text.ToString)
    End Sub

    Private Sub ViewUptimeGraphToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewUptimeGraphToolStripMenuItem.Click
        'Dim machine As Machine = MachineList.Instance.Machines.ElementAt(IPGrid.CurrentRow.Index)
        Dim mRow As DataGridViewRow = IPGrid.SelectedRows(0)

        For Each machine In MachineList.Instance.Machines
            If machine.Device = mRow.Cells(0).Value And machine.IP = mRow.Cells(1).Value Then
                If Not _chartForm Is Nothing Then
                    _chartForm.Close()
                End If
                _chartForm = New Chart
                _chartForm.Show()
                _chartForm.InitalizeWithData(machine)
            End If
        Next

    End Sub

    Private Sub AddToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If lvMachines.SelectedNode IsNot Nothing Then
            Dim Device As String = InputBox("Enter Device Name", "Device")
            If Device <> "" Then
                Dim IP As String = InputBox("Enter IP Address", "IP")
                If IP <> "" Then
                    Dim newMachine As Machine = MachineList.AddMachine(Device, IP)
                    With lvMachines.Nodes(0).Nodes.Add(Device)
                        .Tag = newMachine
                    End With
                    newMachine.Initialize(_ipData.Tables("IPData"))
                    MachineList.Instance.Save()
                End If
            End If
        End If
    End Sub

    Private Sub lvMachines_MouseClick(sender As Object, e As MouseEventArgs) Handles lvMachines.MouseClick
        sender.SelectedNode = sender.GetNodeAt(New Point(e.X, e.Y))
    End Sub

    Private Sub cmsMachines_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsMachines.Opening
        If lvMachines.SelectedNode IsNot Nothing Then
            If lvMachines.SelectedNode.Level = 0 Then
                btnAdd.Visible = True
                btnSaveResults.Visible = True
                btnDelete.Visible = False
            Else
                btnSaveResults.Visible = False
                btnAdd.Visible = False
                btnDelete.Visible = True
            End If
        End If
    End Sub

    Private Sub btnSaveResults_Click(sender As Object, e As EventArgs) Handles btnSaveResults.Click
        MachineList.Instance.SaveResults()
    End Sub

    Private Sub btnLoadResults_Click(sender As System.Object, e As System.EventArgs) Handles btnLoadResults.Click
        Dim dialog As New OpenFileDialog
        If dialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            lvMachines.Nodes(0).Nodes.Clear()
            _ipData.Tables("IPData").Rows.Clear()

            Machine.SaveingResults = True
            MachineList.SetMachines(MachineList.Open(dialog.FileName).Machines)
            Me.Text = "Multiping - " & My.Computer.FileSystem.GetName(dialog.FileName)

            _ipData = New DataSet
            _ipData.Tables.Add(New DataTable("IPData"))
            With (_ipData.Tables("IPData").Columns)
                .Add("Device")
                .Add("IP")
                .Add("Status")
                .Add("Latency")
                .Add("Pings")
                .Add("Drops")
                .Add("FirstResponded")
                .Add("LastResponded")
            End With
            IPGrid.DataSource = _ipData.Tables("IPData")

            For i As Integer = 0 To MachineList.Instance.Machines.Count - 1
                Dim Machine As Machine = MachineList.Instance.Machines(i)
                AddMachine(Machine.Device, Machine.IP, False, False)
                Machine.UpdateTotalPings()
                Machine.UpdateDrops()
            Next
            lvMachines.ExpandAll()

            Machine.SaveingResults = False
        End If
    End Sub

End Class
