<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Hosts")
        Me.menuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.OpenItemInBrowserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewUptimeGraphToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lvMachines = New System.Windows.Forms.TreeView()
        Me.cmsMachines = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.btnSaveResults = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnLoadResults = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnAdd = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.IPGrid = New System.Windows.Forms.DataGridView()
        Me.IPTextBox = New System.Windows.Forms.TextBox()
        Me.menuStrip.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.cmsMachines.SuspendLayout()
        CType(Me.IPGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'menuStrip
        '
        Me.menuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenItemInBrowserToolStripMenuItem, Me.ViewUptimeGraphToolStripMenuItem})
        Me.menuStrip.Name = "menuStrip"
        Me.menuStrip.Size = New System.Drawing.Size(189, 48)
        '
        'OpenItemInBrowserToolStripMenuItem
        '
        Me.OpenItemInBrowserToolStripMenuItem.Name = "OpenItemInBrowserToolStripMenuItem"
        Me.OpenItemInBrowserToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.OpenItemInBrowserToolStripMenuItem.Text = "Open item in Browser"
        '
        'ViewUptimeGraphToolStripMenuItem
        '
        Me.ViewUptimeGraphToolStripMenuItem.Name = "ViewUptimeGraphToolStripMenuItem"
        Me.ViewUptimeGraphToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.ViewUptimeGraphToolStripMenuItem.Text = "View Uptime Graph"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lvMachines)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.IPGrid)
        Me.SplitContainer1.Panel2.Controls.Add(Me.IPTextBox)
        Me.SplitContainer1.Size = New System.Drawing.Size(1297, 607)
        Me.SplitContainer1.SplitterDistance = 180
        Me.SplitContainer1.TabIndex = 3
        '
        'lvMachines
        '
        Me.lvMachines.ContextMenuStrip = Me.cmsMachines
        Me.lvMachines.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvMachines.Location = New System.Drawing.Point(0, 0)
        Me.lvMachines.Name = "lvMachines"
        TreeNode1.Name = "Node1"
        TreeNode1.Text = "Hosts"
        Me.lvMachines.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode1})
        Me.lvMachines.Size = New System.Drawing.Size(180, 607)
        Me.lvMachines.TabIndex = 1
        '
        'cmsMachines
        '
        Me.cmsMachines.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnSaveResults, Me.btnLoadResults, Me.btnAdd, Me.btnDelete})
        Me.cmsMachines.Name = "cmsMachines"
        Me.cmsMachines.Size = New System.Drawing.Size(141, 92)
        '
        'btnSaveResults
        '
        Me.btnSaveResults.Name = "btnSaveResults"
        Me.btnSaveResults.Size = New System.Drawing.Size(140, 22)
        Me.btnSaveResults.Text = "Save Results"
        '
        'btnLoadResults
        '
        Me.btnLoadResults.Name = "btnLoadResults"
        Me.btnLoadResults.Size = New System.Drawing.Size(140, 22)
        Me.btnLoadResults.Text = "Load Results"
        '
        'btnAdd
        '
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(140, 22)
        Me.btnAdd.Text = "Add"
        '
        'btnDelete
        '
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(140, 22)
        Me.btnDelete.Text = "Delete"
        '
        'IPGrid
        '
        Me.IPGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IPGrid.Location = New System.Drawing.Point(0, 0)
        Me.IPGrid.Name = "IPGrid"
        Me.IPGrid.Size = New System.Drawing.Size(1113, 607)
        Me.IPGrid.TabIndex = 0
        '
        'IPTextBox
        '
        Me.IPTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IPTextBox.Location = New System.Drawing.Point(0, 0)
        Me.IPTextBox.Name = "IPTextBox"
        Me.IPTextBox.Size = New System.Drawing.Size(1113, 20)
        Me.IPTextBox.TabIndex = 1
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1297, 607)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MultiPing"
        Me.menuStrip.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.cmsMachines.ResumeLayout(False)
        CType(Me.IPGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents menuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents OpenItemInBrowserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewUptimeGraphToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lvMachines As System.Windows.Forms.TreeView
    Friend WithEvents IPGrid As System.Windows.Forms.DataGridView
    Friend WithEvents IPTextBox As System.Windows.Forms.TextBox
    Friend WithEvents cmsMachines As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents btnAdd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnSaveResults As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnLoadResults As System.Windows.Forms.ToolStripMenuItem

End Class
