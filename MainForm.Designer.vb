<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Me.tmcMap = New Microsoft.Research.CommunityTechnologies.Treemap.TreemapControl()
        Me.mnuApplication = New System.Windows.Forms.MenuStrip()
        Me.mnuConnect = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDatabases = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRefresh = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblCurrentActivity = New System.Windows.Forms.ToolStripStatusLabel()
        Me.sbProgress = New System.Windows.Forms.ToolStripProgressBar()
        Me.mnuApplication.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tmcMap
        '
        Me.tmcMap.AllowDrag = False
        Me.tmcMap.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tmcMap.BorderColor = System.Drawing.SystemColors.WindowFrame
        Me.tmcMap.DiscreteNegativeColors = 20
        Me.tmcMap.DiscretePositiveColors = 20
        Me.tmcMap.EmptySpaceLocation = Microsoft.Research.CommunityTechnologies.Treemap.EmptySpaceLocation.DeterminedByLayoutAlgorithm
        Me.tmcMap.FontFamily = "Arial"
        Me.tmcMap.FontSolidColor = System.Drawing.SystemColors.WindowText
        Me.tmcMap.IsZoomable = False
        Me.tmcMap.LayoutAlgorithm = Microsoft.Research.CommunityTechnologies.Treemap.LayoutAlgorithm.BottomWeightedSquarified
        Me.tmcMap.Location = New System.Drawing.Point(12, 27)
        Me.tmcMap.MaxColor = System.Drawing.Color.Green
        Me.tmcMap.MaxColorMetric = 100.0!
        Me.tmcMap.MinColor = System.Drawing.Color.Red
        Me.tmcMap.MinColorMetric = -100.0!
        Me.tmcMap.Name = "tmcMap"
        Me.tmcMap.NodeColorAlgorithm = Microsoft.Research.CommunityTechnologies.Treemap.NodeColorAlgorithm.UseColorMetric
        Me.tmcMap.NodeLevelsWithText = Microsoft.Research.CommunityTechnologies.Treemap.NodeLevelsWithText.All
        Me.tmcMap.PaddingDecrementPerLevelPx = 1
        Me.tmcMap.PaddingPx = 5
        Me.tmcMap.PenWidthDecrementPerLevelPx = 1
        Me.tmcMap.PenWidthPx = 3
        Me.tmcMap.SelectedBackColor = System.Drawing.SystemColors.Highlight
        Me.tmcMap.SelectedFontColor = System.Drawing.SystemColors.HighlightText
        Me.tmcMap.ShowToolTips = True
        Me.tmcMap.Size = New System.Drawing.Size(800, 449)
        Me.tmcMap.TabIndex = 0
        Me.tmcMap.TextLocation = Microsoft.Research.CommunityTechnologies.Treemap.TextLocation.Top
        '
        'mnuApplication
        '
        Me.mnuApplication.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuConnect, Me.mnuDatabases, Me.mnuRefresh, Me.mnuAbout, Me.mnuExit})
        Me.mnuApplication.Location = New System.Drawing.Point(0, 0)
        Me.mnuApplication.Name = "mnuApplication"
        Me.mnuApplication.Size = New System.Drawing.Size(824, 24)
        Me.mnuApplication.TabIndex = 2
        Me.mnuApplication.Text = "AppMenu"
        '
        'mnuConnect
        '
        Me.mnuConnect.Name = "mnuConnect"
        Me.mnuConnect.Size = New System.Drawing.Size(73, 20)
        Me.mnuConnect.Text = "&Connect..."
        '
        'mnuDatabases
        '
        Me.mnuDatabases.Enabled = False
        Me.mnuDatabases.Name = "mnuDatabases"
        Me.mnuDatabases.Size = New System.Drawing.Size(110, 20)
        Me.mnuDatabases.Text = "Choose &Database"
        '
        'mnuRefresh
        '
        Me.mnuRefresh.Enabled = False
        Me.mnuRefresh.Name = "mnuRefresh"
        Me.mnuRefresh.Size = New System.Drawing.Size(101, 20)
        Me.mnuRefresh.Text = "&Refresh Current"
        '
        'mnuAbout
        '
        Me.mnuAbout.Name = "mnuAbout"
        Me.mnuAbout.Size = New System.Drawing.Size(61, 20)
        Me.mnuAbout.Text = "&About..."
        '
        'mnuExit
        '
        Me.mnuExit.Name = "mnuExit"
        Me.mnuExit.Size = New System.Drawing.Size(37, 20)
        Me.mnuExit.Text = "E&xit"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblCurrentActivity, Me.sbProgress})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 479)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(824, 22)
        Me.StatusStrip1.TabIndex = 3
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblCurrentActivity
        '
        Me.lblCurrentActivity.Name = "lblCurrentActivity"
        Me.lblCurrentActivity.Size = New System.Drawing.Size(103, 17)
        Me.lblCurrentActivity.Text = "Application Status"
        '
        'sbProgress
        '
        Me.sbProgress.Name = "sbProgress"
        Me.sbProgress.Size = New System.Drawing.Size(100, 16)
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(824, 501)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.tmcMap)
        Me.Controls.Add(Me.mnuApplication)
        Me.MainMenuStrip = Me.mnuApplication
        Me.Name = "MainForm"
        Me.Text = "SQL Space Mapper"
        Me.mnuApplication.ResumeLayout(False)
        Me.mnuApplication.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tmcMap As Microsoft.Research.CommunityTechnologies.Treemap.TreemapControl
    Friend WithEvents mnuApplication As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuConnect As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblCurrentActivity As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents sbProgress As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents mnuRefresh As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDatabases As System.Windows.Forms.ToolStripMenuItem

End Class
