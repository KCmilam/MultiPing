<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Chart
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Chart))
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.RefreshListButton = New System.Windows.Forms.ToolStripButton()
        Me.LoadPreviousDataButton = New System.Windows.Forms.ToolStripButton()
        Me.DatesComboBox = New System.Windows.Forms.ComboBox()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.UpTimeChart = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.TimeStepLabel = New System.Windows.Forms.Label()
        Me.TimeStepComboBox = New System.Windows.Forms.ComboBox()
        Me.ChartTypeLabel = New System.Windows.Forms.Label()
        Me.ChartTypeComboBox = New System.Windows.Forms.ComboBox()
        Me.DateRangeLabel = New System.Windows.Forms.Label()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.UpTimeChart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefreshListButton, Me.LoadPreviousDataButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1270, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'RefreshListButton
        '
        Me.RefreshListButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.RefreshListButton.Image = CType(resources.GetObject("RefreshListButton.Image"), System.Drawing.Image)
        Me.RefreshListButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.RefreshListButton.Name = "RefreshListButton"
        Me.RefreshListButton.Size = New System.Drawing.Size(71, 22)
        Me.RefreshListButton.Text = "Refresh List"
        '
        'LoadPreviousDataButton
        '
        Me.LoadPreviousDataButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.LoadPreviousDataButton.Image = CType(resources.GetObject("LoadPreviousDataButton.Image"), System.Drawing.Image)
        Me.LoadPreviousDataButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.LoadPreviousDataButton.Name = "LoadPreviousDataButton"
        Me.LoadPreviousDataButton.Size = New System.Drawing.Size(112, 22)
        Me.LoadPreviousDataButton.Text = "Load Previous Data"
        '
        'DatesComboBox
        '
        Me.DatesComboBox.FormattingEnabled = True
        Me.DatesComboBox.Location = New System.Drawing.Point(6, 110)
        Me.DatesComboBox.Name = "DatesComboBox"
        Me.DatesComboBox.Size = New System.Drawing.Size(128, 21)
        Me.DatesComboBox.TabIndex = 2
        Me.DatesComboBox.Visible = False
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(0, 25)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 606)
        Me.Splitter1.TabIndex = 3
        Me.Splitter1.TabStop = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 25)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.UpTimeChart)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TimeStepLabel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TimeStepComboBox)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ChartTypeLabel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ChartTypeComboBox)
        Me.SplitContainer1.Panel2.Controls.Add(Me.DateRangeLabel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.DatesComboBox)
        Me.SplitContainer1.Size = New System.Drawing.Size(1267, 606)
        Me.SplitContainer1.SplitterDistance = 1237
        Me.SplitContainer1.TabIndex = 4
        '
        'UpTimeChart
        '
        ChartArea1.AxisY.LineWidth = 4
        ChartArea1.Name = "ChartArea1"
        Me.UpTimeChart.ChartAreas.Add(ChartArea1)
        Me.UpTimeChart.Dock = System.Windows.Forms.DockStyle.Fill
        Legend1.Name = "Legend1"
        Me.UpTimeChart.Legends.Add(Legend1)
        Me.UpTimeChart.Location = New System.Drawing.Point(0, 0)
        Me.UpTimeChart.Name = "UpTimeChart"
        Me.UpTimeChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.UpTimeChart.Series.Add(Series1)
        Me.UpTimeChart.Size = New System.Drawing.Size(1237, 606)
        Me.UpTimeChart.TabIndex = 1
        Me.UpTimeChart.Text = "UpTime"
        '
        'TimeStepLabel
        '
        Me.TimeStepLabel.AutoSize = True
        Me.TimeStepLabel.Location = New System.Drawing.Point(3, 54)
        Me.TimeStepLabel.Name = "TimeStepLabel"
        Me.TimeStepLabel.Size = New System.Drawing.Size(118, 13)
        Me.TimeStepLabel.TabIndex = 7
        Me.TimeStepLabel.Text = "Time Step (in seconds):"
        Me.TimeStepLabel.Visible = False
        '
        'TimeStepComboBox
        '
        Me.TimeStepComboBox.FormattingEnabled = True
        Me.TimeStepComboBox.Items.AddRange(New Object() {"2", "10", "30", "60", "120", "180", "360", "720", "960"})
        Me.TimeStepComboBox.Location = New System.Drawing.Point(6, 70)
        Me.TimeStepComboBox.Name = "TimeStepComboBox"
        Me.TimeStepComboBox.Size = New System.Drawing.Size(128, 21)
        Me.TimeStepComboBox.TabIndex = 6
        Me.TimeStepComboBox.Visible = False
        '
        'ChartTypeLabel
        '
        Me.ChartTypeLabel.AutoSize = True
        Me.ChartTypeLabel.Location = New System.Drawing.Point(3, 14)
        Me.ChartTypeLabel.Name = "ChartTypeLabel"
        Me.ChartTypeLabel.Size = New System.Drawing.Size(62, 13)
        Me.ChartTypeLabel.TabIndex = 5
        Me.ChartTypeLabel.Text = "Chart Type:"
        Me.ChartTypeLabel.Visible = False
        '
        'ChartTypeComboBox
        '
        Me.ChartTypeComboBox.FormattingEnabled = True
        Me.ChartTypeComboBox.Location = New System.Drawing.Point(6, 30)
        Me.ChartTypeComboBox.Name = "ChartTypeComboBox"
        Me.ChartTypeComboBox.Size = New System.Drawing.Size(128, 21)
        Me.ChartTypeComboBox.TabIndex = 4
        Me.ChartTypeComboBox.Visible = False
        '
        'DateRangeLabel
        '
        Me.DateRangeLabel.AutoSize = True
        Me.DateRangeLabel.Location = New System.Drawing.Point(3, 94)
        Me.DateRangeLabel.Name = "DateRangeLabel"
        Me.DateRangeLabel.Size = New System.Drawing.Size(68, 13)
        Me.DateRangeLabel.TabIndex = 3
        Me.DateRangeLabel.Text = "Date Range:"
        Me.DateRangeLabel.Visible = False
        '
        'Chart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1270, 631)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "Chart"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.UpTimeChart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents LoadPreviousDataButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents RefreshListButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents DatesComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents UpTimeChart As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents DateRangeLabel As System.Windows.Forms.Label
    Friend WithEvents ChartTypeLabel As System.Windows.Forms.Label
    Friend WithEvents ChartTypeComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents TimeStepLabel As System.Windows.Forms.Label
    Friend WithEvents TimeStepComboBox As System.Windows.Forms.ComboBox
End Class
