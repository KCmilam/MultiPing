<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Drops
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
        Me.DropGrid = New System.Windows.Forms.DataGridView()
        CType(Me.DropGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DropGrid
        '
        Me.DropGrid.AllowUserToAddRows = False
        Me.DropGrid.AllowUserToDeleteRows = False
        Me.DropGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DropGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DropGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DropGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DropGrid.Location = New System.Drawing.Point(0, 0)
        Me.DropGrid.Name = "DropGrid"
        Me.DropGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.DropGrid.Size = New System.Drawing.Size(213, 482)
        Me.DropGrid.TabIndex = 0
        '
        'frmDrops
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(213, 482)
        Me.Controls.Add(Me.DropGrid)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmDrops"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Drop Time Stamps"
        CType(Me.DropGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DropGrid As System.Windows.Forms.DataGridView
End Class
