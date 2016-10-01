<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAnalyze
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.txtSingle = New System.Windows.Forms.TextBox()
        Me.btnSingle = New System.Windows.Forms.Button()
        Me.cbLists = New System.Windows.Forms.ComboBox()
        Me.txtReport = New System.Windows.Forms.TextBox()
        Me.btnGo = New System.Windows.Forms.Button()
        Me.tvList = New System.Windows.Forms.TreeView()
        Me.tabDetail = New System.Windows.Forms.TabControl()
        Me.tbpChildren = New System.Windows.Forms.TabPage()
        Me.lvChildren = New System.Windows.Forms.ListView()
        Me.colName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colOSMID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colGSS = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colCouncilStyle = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colParishType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colCouncilName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tbpMap = New System.Windows.Forms.TabPage()
        Me.wbMap = New System.Windows.Forms.WebBrowser()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.chkUpdateAll = New System.Windows.Forms.CheckBox()
        Me.ssStatus = New System.Windows.Forms.StatusStrip()
        Me.tsProgress = New System.Windows.Forms.ToolStripProgressBar()
        Me.tsStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.cmsNode = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiFlush = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiShowAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiJSON = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiChildOverviewReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiDeepChildReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAddChild = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiReview = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtAdminLevel = New System.Windows.Forms.TextBox()
        Me.btnHist = New System.Windows.Forms.Button()
        Me.cmsChild = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiChildEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiChildReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnOSM = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.sfdReports = New System.Windows.Forms.SaveFileDialog()
        Me.tabDetail.SuspendLayout()
        Me.tbpChildren.SuspendLayout()
        Me.tbpMap.SuspendLayout()
        Me.ssStatus.SuspendLayout()
        Me.cmsNode.SuspendLayout()
        Me.cmsChild.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(835, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(54, 22)
        Me.btnClose.TabIndex = 0
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'txtSingle
        '
        Me.txtSingle.Location = New System.Drawing.Point(626, 7)
        Me.txtSingle.Name = "txtSingle"
        Me.txtSingle.Size = New System.Drawing.Size(76, 20)
        Me.txtSingle.TabIndex = 2
        '
        'btnSingle
        '
        Me.btnSingle.Location = New System.Drawing.Point(708, 8)
        Me.btnSingle.Name = "btnSingle"
        Me.btnSingle.Size = New System.Drawing.Size(37, 19)
        Me.btnSingle.TabIndex = 3
        Me.btnSingle.Text = "Go"
        Me.btnSingle.UseVisualStyleBackColor = True
        '
        'cbLists
        '
        Me.cbLists.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbLists.FormattingEnabled = True
        Me.cbLists.Location = New System.Drawing.Point(12, 11)
        Me.cbLists.Name = "cbLists"
        Me.cbLists.Size = New System.Drawing.Size(200, 21)
        Me.cbLists.TabIndex = 4
        '
        'txtReport
        '
        Me.txtReport.Location = New System.Drawing.Point(12, 403)
        Me.txtReport.Multiline = True
        Me.txtReport.Name = "txtReport"
        Me.txtReport.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtReport.Size = New System.Drawing.Size(355, 242)
        Me.txtReport.TabIndex = 6
        '
        'btnGo
        '
        Me.btnGo.Location = New System.Drawing.Point(218, 12)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(75, 23)
        Me.btnGo.TabIndex = 8
        Me.btnGo.Text = "Load"
        Me.btnGo.UseVisualStyleBackColor = True
        '
        'tvList
        '
        Me.tvList.HideSelection = False
        Me.tvList.Location = New System.Drawing.Point(12, 38)
        Me.tvList.Name = "tvList"
        Me.tvList.Size = New System.Drawing.Size(354, 363)
        Me.tvList.TabIndex = 9
        '
        'tabDetail
        '
        Me.tabDetail.Controls.Add(Me.tbpChildren)
        Me.tabDetail.Controls.Add(Me.tbpMap)
        Me.tabDetail.Location = New System.Drawing.Point(372, 38)
        Me.tabDetail.Name = "tabDetail"
        Me.tabDetail.SelectedIndex = 0
        Me.tabDetail.Size = New System.Drawing.Size(433, 363)
        Me.tabDetail.TabIndex = 11
        '
        'tbpChildren
        '
        Me.tbpChildren.Controls.Add(Me.lvChildren)
        Me.tbpChildren.Location = New System.Drawing.Point(4, 22)
        Me.tbpChildren.Name = "tbpChildren"
        Me.tbpChildren.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpChildren.Size = New System.Drawing.Size(425, 337)
        Me.tbpChildren.TabIndex = 0
        Me.tbpChildren.Text = "Children"
        Me.tbpChildren.UseVisualStyleBackColor = True
        '
        'lvChildren
        '
        Me.lvChildren.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colName, Me.colOSMID, Me.colType, Me.colGSS, Me.colCouncilStyle, Me.colParishType, Me.colCouncilName})
        Me.lvChildren.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvChildren.FullRowSelect = True
        Me.lvChildren.HideSelection = False
        Me.lvChildren.Location = New System.Drawing.Point(3, 3)
        Me.lvChildren.MultiSelect = False
        Me.lvChildren.Name = "lvChildren"
        Me.lvChildren.Size = New System.Drawing.Size(419, 331)
        Me.lvChildren.TabIndex = 11
        Me.lvChildren.UseCompatibleStateImageBehavior = False
        Me.lvChildren.View = System.Windows.Forms.View.Details
        '
        'colName
        '
        Me.colName.Text = "Name"
        Me.colName.Width = 200
        '
        'colOSMID
        '
        Me.colOSMID.Text = "OSM ID"
        Me.colOSMID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'colType
        '
        Me.colType.Text = "Type"
        '
        'colGSS
        '
        Me.colGSS.Text = "GSS code"
        Me.colGSS.Width = 81
        '
        'colCouncilStyle
        '
        Me.colCouncilStyle.Text = "Style"
        Me.colCouncilStyle.Width = 120
        '
        'colParishType
        '
        Me.colParishType.Text = "Parish Type"
        Me.colParishType.Width = 120
        '
        'colCouncilName
        '
        Me.colCouncilName.Text = "Council Name"
        Me.colCouncilName.Width = 250
        '
        'tbpMap
        '
        Me.tbpMap.Controls.Add(Me.wbMap)
        Me.tbpMap.Location = New System.Drawing.Point(4, 22)
        Me.tbpMap.Name = "tbpMap"
        Me.tbpMap.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpMap.Size = New System.Drawing.Size(425, 337)
        Me.tbpMap.TabIndex = 1
        Me.tbpMap.Text = "Map"
        Me.tbpMap.UseVisualStyleBackColor = True
        '
        'wbMap
        '
        Me.wbMap.AllowWebBrowserDrop = False
        Me.wbMap.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wbMap.IsWebBrowserContextMenuEnabled = False
        Me.wbMap.Location = New System.Drawing.Point(3, 3)
        Me.wbMap.Margin = New System.Windows.Forms.Padding(0)
        Me.wbMap.MinimumSize = New System.Drawing.Size(20, 20)
        Me.wbMap.Name = "wbMap"
        Me.wbMap.ScrollBarsEnabled = False
        Me.wbMap.Size = New System.Drawing.Size(419, 331)
        Me.wbMap.TabIndex = 0
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(300, 13)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(75, 23)
        Me.btnUpdate.TabIndex = 12
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'chkUpdateAll
        '
        Me.chkUpdateAll.AutoSize = True
        Me.chkUpdateAll.Location = New System.Drawing.Point(381, 19)
        Me.chkUpdateAll.Name = "chkUpdateAll"
        Me.chkUpdateAll.Size = New System.Drawing.Size(110, 17)
        Me.chkUpdateAll.TabIndex = 13
        Me.chkUpdateAll.Text = "Update/review all"
        Me.chkUpdateAll.UseVisualStyleBackColor = True
        '
        'ssStatus
        '
        Me.ssStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsProgress, Me.tsStatus})
        Me.ssStatus.Location = New System.Drawing.Point(0, 648)
        Me.ssStatus.Name = "ssStatus"
        Me.ssStatus.Size = New System.Drawing.Size(901, 22)
        Me.ssStatus.TabIndex = 14
        Me.ssStatus.Text = "StatusStrip1"
        '
        'tsProgress
        '
        Me.tsProgress.Name = "tsProgress"
        Me.tsProgress.Size = New System.Drawing.Size(100, 16)
        Me.tsProgress.Step = 1
        Me.tsProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'tsStatus
        '
        Me.tsStatus.Name = "tsStatus"
        Me.tsStatus.Size = New System.Drawing.Size(120, 17)
        Me.tsStatus.Text = "ToolStripStatusLabel1"
        '
        'cmsNode
        '
        Me.cmsNode.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiFlush, Me.tsmiEdit, Me.tsmiShowAll, Me.tsmiJSON, Me.tsmiReport, Me.tsmiChildOverviewReport, Me.tsmiDeepChildReport, Me.tsmiAddChild, Me.tsmiReview})
        Me.cmsNode.Name = "ContextMenuStrip1"
        Me.cmsNode.Size = New System.Drawing.Size(193, 202)
        '
        'tsmiFlush
        '
        Me.tsmiFlush.Name = "tsmiFlush"
        Me.tsmiFlush.Size = New System.Drawing.Size(192, 22)
        Me.tsmiFlush.Text = "Flush from cache"
        '
        'tsmiEdit
        '
        Me.tsmiEdit.Name = "tsmiEdit"
        Me.tsmiEdit.Size = New System.Drawing.Size(192, 22)
        Me.tsmiEdit.Text = "Edit"
        '
        'tsmiShowAll
        '
        Me.tsmiShowAll.Name = "tsmiShowAll"
        Me.tsmiShowAll.Size = New System.Drawing.Size(192, 22)
        Me.tsmiShowAll.Text = "Show all on map"
        '
        'tsmiJSON
        '
        Me.tsmiJSON.Name = "tsmiJSON"
        Me.tsmiJSON.Size = New System.Drawing.Size(192, 22)
        Me.tsmiJSON.Text = "Show JSON"
        '
        'tsmiReport
        '
        Me.tsmiReport.Name = "tsmiReport"
        Me.tsmiReport.Size = New System.Drawing.Size(192, 22)
        Me.tsmiReport.Text = "Report"
        '
        'tsmiChildOverviewReport
        '
        Me.tsmiChildOverviewReport.Name = "tsmiChildOverviewReport"
        Me.tsmiChildOverviewReport.Size = New System.Drawing.Size(192, 22)
        Me.tsmiChildOverviewReport.Text = "Child Overview Report"
        '
        'tsmiDeepChildReport
        '
        Me.tsmiDeepChildReport.Name = "tsmiDeepChildReport"
        Me.tsmiDeepChildReport.Size = New System.Drawing.Size(192, 22)
        Me.tsmiDeepChildReport.Text = "Deep Child Report"
        '
        'tsmiAddChild
        '
        Me.tsmiAddChild.Name = "tsmiAddChild"
        Me.tsmiAddChild.Size = New System.Drawing.Size(192, 22)
        Me.tsmiAddChild.Text = "Add Child"
        '
        'tsmiReview
        '
        Me.tsmiReview.Name = "tsmiReview"
        Me.tsmiReview.Size = New System.Drawing.Size(192, 22)
        Me.tsmiReview.Text = "Review OSM"
        '
        'txtAdminLevel
        '
        Me.txtAdminLevel.Location = New System.Drawing.Point(530, 15)
        Me.txtAdminLevel.Name = "txtAdminLevel"
        Me.txtAdminLevel.Size = New System.Drawing.Size(43, 20)
        Me.txtAdminLevel.TabIndex = 15
        '
        'btnHist
        '
        Me.btnHist.Location = New System.Drawing.Point(581, 7)
        Me.btnHist.Name = "btnHist"
        Me.btnHist.Size = New System.Drawing.Size(39, 23)
        Me.btnHist.TabIndex = 16
        Me.btnHist.Text = "Hist"
        Me.btnHist.UseVisualStyleBackColor = True
        '
        'cmsChild
        '
        Me.cmsChild.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiChildEdit, Me.tsmiChildReport})
        Me.cmsChild.Name = "cmsChild"
        Me.cmsChild.Size = New System.Drawing.Size(110, 48)
        '
        'tsmiChildEdit
        '
        Me.tsmiChildEdit.Name = "tsmiChildEdit"
        Me.tsmiChildEdit.Size = New System.Drawing.Size(109, 22)
        Me.tsmiChildEdit.Text = "Edit"
        '
        'tsmiChildReport
        '
        Me.tsmiChildReport.Name = "tsmiChildReport"
        Me.tsmiChildReport.Size = New System.Drawing.Size(109, 22)
        Me.tsmiChildReport.Text = "Report"
        '
        'btnOSM
        '
        Me.btnOSM.Location = New System.Drawing.Point(491, 13)
        Me.btnOSM.Name = "btnOSM"
        Me.btnOSM.Size = New System.Drawing.Size(34, 23)
        Me.btnOSM.TabIndex = 12
        Me.btnOSM.Text = "osm"
        Me.btnOSM.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(767, 11)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(49, 19)
        Me.btnClear.TabIndex = 17
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'frmAnalyze
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(901, 670)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnOSM)
        Me.Controls.Add(Me.btnHist)
        Me.Controls.Add(Me.txtAdminLevel)
        Me.Controls.Add(Me.ssStatus)
        Me.Controls.Add(Me.chkUpdateAll)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.tabDetail)
        Me.Controls.Add(Me.tvList)
        Me.Controls.Add(Me.btnGo)
        Me.Controls.Add(Me.txtReport)
        Me.Controls.Add(Me.cbLists)
        Me.Controls.Add(Me.btnSingle)
        Me.Controls.Add(Me.txtSingle)
        Me.Controls.Add(Me.btnClose)
        Me.Name = "frmAnalyze"
        Me.Text = "Boundary Analyzer"
        Me.tabDetail.ResumeLayout(False)
        Me.tbpChildren.ResumeLayout(False)
        Me.tbpMap.ResumeLayout(False)
        Me.ssStatus.ResumeLayout(False)
        Me.ssStatus.PerformLayout()
        Me.cmsNode.ResumeLayout(False)
        Me.cmsChild.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents txtSingle As System.Windows.Forms.TextBox
    Friend WithEvents btnSingle As System.Windows.Forms.Button
    Friend WithEvents cbLists As System.Windows.Forms.ComboBox
    Friend WithEvents txtReport As System.Windows.Forms.TextBox
    Friend WithEvents btnGo As System.Windows.Forms.Button
    Friend WithEvents tvList As System.Windows.Forms.TreeView
    Friend WithEvents tabDetail As System.Windows.Forms.TabControl
    Friend WithEvents tbpChildren As System.Windows.Forms.TabPage
    Friend WithEvents lvChildren As System.Windows.Forms.ListView
    Friend WithEvents tbpMap As System.Windows.Forms.TabPage
    Friend WithEvents wbMap As System.Windows.Forms.WebBrowser
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents chkUpdateAll As System.Windows.Forms.CheckBox
    Friend WithEvents ssStatus As System.Windows.Forms.StatusStrip
    Friend WithEvents tsProgress As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents tsStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents cmsNode As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents txtAdminLevel As System.Windows.Forms.TextBox
    Friend WithEvents colName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colOSMID As System.Windows.Forms.ColumnHeader
    Friend WithEvents colType As System.Windows.Forms.ColumnHeader
    Friend WithEvents colGSS As System.Windows.Forms.ColumnHeader
    Friend WithEvents colCouncilStyle As System.Windows.Forms.ColumnHeader
    Friend WithEvents tsmiFlush As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiShowAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmiJSON As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnHist As System.Windows.Forms.Button
    Friend WithEvents colCouncilName As ColumnHeader
    Friend WithEvents cmsChild As ContextMenuStrip
    Friend WithEvents tsmiChildEdit As ToolStripMenuItem
    Friend WithEvents colParishType As ColumnHeader
    Friend WithEvents btnOSM As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents tsmiReport As ToolStripMenuItem
    Friend WithEvents tsmiChildReport As ToolStripMenuItem
    Friend WithEvents tsmiAddChild As ToolStripMenuItem
    Friend WithEvents tsmiReview As ToolStripMenuItem
    Friend WithEvents tsmiChildOverviewReport As ToolStripMenuItem
    Friend WithEvents tsmiDeepChildReport As ToolStripMenuItem
    Friend WithEvents sfdReports As SaveFileDialog
End Class
