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
        Me.colWebsite = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
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
        Me.tsmiSearch = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiShowAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiJSON = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiShowInOsm = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAnalyze = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiChildOverviewReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiDeepChildReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiAddChild = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiReview = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiOpenWebsite = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtAdminLevel = New System.Windows.Forms.TextBox()
        Me.btnHist = New System.Windows.Forms.Button()
        Me.cmsChild = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiChildEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiChildReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiChildOpenWebsite = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiChildReview = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnOSM = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.sfdReports = New System.Windows.Forms.SaveFileDialog()
        Me.tvList = New System.Windows.Forms.TreeView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.scon1 = New System.Windows.Forms.SplitContainer()
        Me.scon2 = New System.Windows.Forms.SplitContainer()
        Me.btnCentroids = New System.Windows.Forms.Button()
        Me.ofdBoundaries = New System.Windows.Forms.OpenFileDialog()
        Me.btnUpload = New System.Windows.Forms.Button()
        Me.ofdUpload = New System.Windows.Forms.OpenFileDialog()
        Me.chkShowDeleted = New System.Windows.Forms.CheckBox()
        Me.tsmiChildShowInOsm = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiChildAnalyze = New System.Windows.Forms.ToolStripMenuItem()
        Me.tabDetail.SuspendLayout()
        Me.tbpChildren.SuspendLayout()
        Me.tbpMap.SuspendLayout()
        Me.ssStatus.SuspendLayout()
        Me.cmsNode.SuspendLayout()
        Me.cmsChild.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.scon1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scon1.Panel1.SuspendLayout()
        Me.scon1.Panel2.SuspendLayout()
        Me.scon1.SuspendLayout()
        CType(Me.scon2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scon2.Panel1.SuspendLayout()
        Me.scon2.Panel2.SuspendLayout()
        Me.scon2.SuspendLayout()
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
        Me.cbLists.Size = New System.Drawing.Size(158, 21)
        Me.cbLists.TabIndex = 4
        '
        'txtReport
        '
        Me.txtReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtReport.Location = New System.Drawing.Point(0, 0)
        Me.txtReport.Multiline = True
        Me.txtReport.Name = "txtReport"
        Me.txtReport.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtReport.Size = New System.Drawing.Size(292, 370)
        Me.txtReport.TabIndex = 6
        '
        'btnGo
        '
        Me.btnGo.Location = New System.Drawing.Point(176, 9)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(53, 23)
        Me.btnGo.TabIndex = 8
        Me.btnGo.Text = "Load"
        Me.btnGo.UseVisualStyleBackColor = True
        '
        'tabDetail
        '
        Me.tabDetail.Controls.Add(Me.tbpChildren)
        Me.tabDetail.Controls.Add(Me.tbpMap)
        Me.tabDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabDetail.Location = New System.Drawing.Point(0, 0)
        Me.tabDetail.Name = "tabDetail"
        Me.tabDetail.SelectedIndex = 0
        Me.tabDetail.Size = New System.Drawing.Size(581, 603)
        Me.tabDetail.TabIndex = 11
        '
        'tbpChildren
        '
        Me.tbpChildren.Controls.Add(Me.lvChildren)
        Me.tbpChildren.Location = New System.Drawing.Point(4, 22)
        Me.tbpChildren.Name = "tbpChildren"
        Me.tbpChildren.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpChildren.Size = New System.Drawing.Size(573, 577)
        Me.tbpChildren.TabIndex = 0
        Me.tbpChildren.Text = "Children"
        Me.tbpChildren.UseVisualStyleBackColor = True
        '
        'lvChildren
        '
        Me.lvChildren.AllowColumnReorder = True
        Me.lvChildren.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colName, Me.colOSMID, Me.colType, Me.colGSS, Me.colCouncilStyle, Me.colParishType, Me.colCouncilName, Me.colWebsite})
        Me.lvChildren.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvChildren.FullRowSelect = True
        Me.lvChildren.HideSelection = False
        Me.lvChildren.Location = New System.Drawing.Point(3, 3)
        Me.lvChildren.Name = "lvChildren"
        Me.lvChildren.Size = New System.Drawing.Size(567, 571)
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
        'colWebsite
        '
        Me.colWebsite.Text = "Website"
        Me.colWebsite.Width = 200
        '
        'tbpMap
        '
        Me.tbpMap.Controls.Add(Me.wbMap)
        Me.tbpMap.Location = New System.Drawing.Point(4, 22)
        Me.tbpMap.Name = "tbpMap"
        Me.tbpMap.Size = New System.Drawing.Size(573, 577)
        Me.tbpMap.TabIndex = 1
        Me.tbpMap.Text = "Map"
        Me.tbpMap.UseVisualStyleBackColor = True
        '
        'wbMap
        '
        Me.wbMap.AllowWebBrowserDrop = False
        Me.wbMap.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wbMap.Location = New System.Drawing.Point(0, 0)
        Me.wbMap.Margin = New System.Windows.Forms.Padding(0)
        Me.wbMap.MinimumSize = New System.Drawing.Size(20, 20)
        Me.wbMap.Name = "wbMap"
        Me.wbMap.ScrollBarsEnabled = False
        Me.wbMap.Size = New System.Drawing.Size(573, 577)
        Me.wbMap.TabIndex = 0
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(316, 9)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(53, 23)
        Me.btnUpdate.TabIndex = 12
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'chkUpdateAll
        '
        Me.chkUpdateAll.AutoSize = True
        Me.chkUpdateAll.Location = New System.Drawing.Point(381, 14)
        Me.chkUpdateAll.Name = "chkUpdateAll"
        Me.chkUpdateAll.Size = New System.Drawing.Size(110, 17)
        Me.chkUpdateAll.TabIndex = 13
        Me.chkUpdateAll.Text = "Update/review all"
        Me.chkUpdateAll.UseVisualStyleBackColor = True
        '
        'ssStatus
        '
        Me.ssStatus.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ssStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsProgress, Me.tsStatus})
        Me.ssStatus.Location = New System.Drawing.Point(0, 645)
        Me.ssStatus.Name = "ssStatus"
        Me.ssStatus.Size = New System.Drawing.Size(1133, 25)
        Me.ssStatus.TabIndex = 14
        Me.ssStatus.Text = "StatusStrip1"
        '
        'tsProgress
        '
        Me.tsProgress.Name = "tsProgress"
        Me.tsProgress.Size = New System.Drawing.Size(100, 19)
        Me.tsProgress.Step = 1
        Me.tsProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'tsStatus
        '
        Me.tsStatus.Name = "tsStatus"
        Me.tsStatus.Size = New System.Drawing.Size(120, 20)
        Me.tsStatus.Text = "ToolStripStatusLabel1"
        '
        'cmsNode
        '
        Me.cmsNode.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.cmsNode.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiFlush, Me.tsmiEdit, Me.tsmiSearch, Me.tsmiShowAll, Me.tsmiJSON, Me.tsmiReport, Me.tsmiShowInOsm, Me.tsmiAnalyze, Me.tsmiChildOverviewReport, Me.tsmiDeepChildReport, Me.tsmiAddChild, Me.tsmiReview, Me.tsmiOpenWebsite})
        Me.cmsNode.Name = "ContextMenuStrip1"
        Me.cmsNode.Size = New System.Drawing.Size(193, 290)
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
        'tsmiSearch
        '
        Me.tsmiSearch.Name = "tsmiSearch"
        Me.tsmiSearch.Size = New System.Drawing.Size(192, 22)
        Me.tsmiSearch.Text = "Search"
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
        'tsmiShowInOsm
        '
        Me.tsmiShowInOsm.Name = "tsmiShowInOsm"
        Me.tsmiShowInOsm.Size = New System.Drawing.Size(192, 22)
        Me.tsmiShowInOsm.Text = "Show in OSM"
        '
        'tsmiAnalyze
        '
        Me.tsmiAnalyze.Name = "tsmiAnalyze"
        Me.tsmiAnalyze.Size = New System.Drawing.Size(192, 22)
        Me.tsmiAnalyze.Text = "Analyze"
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
        'tsmiOpenWebsite
        '
        Me.tsmiOpenWebsite.Name = "tsmiOpenWebsite"
        Me.tsmiOpenWebsite.Size = New System.Drawing.Size(192, 22)
        Me.tsmiOpenWebsite.Text = "Go to website"
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
        Me.cmsChild.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.cmsChild.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiChildEdit, Me.tsmiChildReport, Me.tsmiChildOpenWebsite, Me.tsmiChildReview, Me.tsmiChildShowInOsm, Me.tsmiChildAnalyze})
        Me.cmsChild.Name = "cmsChild"
        Me.cmsChild.Size = New System.Drawing.Size(147, 136)
        '
        'tsmiChildEdit
        '
        Me.tsmiChildEdit.Name = "tsmiChildEdit"
        Me.tsmiChildEdit.Size = New System.Drawing.Size(146, 22)
        Me.tsmiChildEdit.Text = "Edit"
        '
        'tsmiChildReport
        '
        Me.tsmiChildReport.Name = "tsmiChildReport"
        Me.tsmiChildReport.Size = New System.Drawing.Size(146, 22)
        Me.tsmiChildReport.Text = "Report"
        '
        'tsmiChildOpenWebsite
        '
        Me.tsmiChildOpenWebsite.Name = "tsmiChildOpenWebsite"
        Me.tsmiChildOpenWebsite.Size = New System.Drawing.Size(146, 22)
        Me.tsmiChildOpenWebsite.Text = "Go to website"
        '
        'tsmiChildReview
        '
        Me.tsmiChildReview.Name = "tsmiChildReview"
        Me.tsmiChildReview.Size = New System.Drawing.Size(146, 22)
        Me.tsmiChildReview.Text = "OSM Review"
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
        Me.btnClear.Location = New System.Drawing.Point(751, 7)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(49, 19)
        Me.btnClear.TabIndex = 17
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'tvList
        '
        Me.tvList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvList.HideSelection = False
        Me.tvList.Location = New System.Drawing.Point(0, 0)
        Me.tvList.Name = "tvList"
        Me.tvList.Size = New System.Drawing.Size(292, 229)
        Me.tvList.TabIndex = 9
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.scon1)
        Me.Panel1.Location = New System.Drawing.Point(12, 42)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(877, 603)
        Me.Panel1.TabIndex = 19
        '
        'scon1
        '
        Me.scon1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scon1.Location = New System.Drawing.Point(0, 0)
        Me.scon1.Name = "scon1"
        '
        'scon1.Panel1
        '
        Me.scon1.Panel1.Controls.Add(Me.scon2)
        '
        'scon1.Panel2
        '
        Me.scon1.Panel2.Controls.Add(Me.tabDetail)
        Me.scon1.Size = New System.Drawing.Size(877, 603)
        Me.scon1.SplitterDistance = 292
        Me.scon1.TabIndex = 0
        '
        'scon2
        '
        Me.scon2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scon2.Location = New System.Drawing.Point(0, 0)
        Me.scon2.Name = "scon2"
        Me.scon2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scon2.Panel1
        '
        Me.scon2.Panel1.Controls.Add(Me.tvList)
        '
        'scon2.Panel2
        '
        Me.scon2.Panel2.Controls.Add(Me.txtReport)
        Me.scon2.Size = New System.Drawing.Size(292, 603)
        Me.scon2.SplitterDistance = 229
        Me.scon2.TabIndex = 10
        '
        'btnCentroids
        '
        Me.btnCentroids.Location = New System.Drawing.Point(236, 9)
        Me.btnCentroids.Name = "btnCentroids"
        Me.btnCentroids.Size = New System.Drawing.Size(73, 23)
        Me.btnCentroids.TabIndex = 20
        Me.btnCentroids.Text = "Centroids"
        Me.btnCentroids.UseVisualStyleBackColor = True
        '
        'ofdBoundaries
        '
        Me.ofdBoundaries.Filter = "CSV Files|*.csv|All Files|*.*"
        '
        'btnUpload
        '
        Me.btnUpload.Location = New System.Drawing.Point(795, 5)
        Me.btnUpload.Name = "btnUpload"
        Me.btnUpload.Size = New System.Drawing.Size(34, 23)
        Me.btnUpload.TabIndex = 21
        Me.btnUpload.Text = "Up"
        Me.btnUpload.UseVisualStyleBackColor = True
        '
        'chkShowDeleted
        '
        Me.chkShowDeleted.AutoSize = True
        Me.chkShowDeleted.Location = New System.Drawing.Point(896, 7)
        Me.chkShowDeleted.Name = "chkShowDeleted"
        Me.chkShowDeleted.Size = New System.Drawing.Size(121, 17)
        Me.chkShowDeleted.TabIndex = 22
        Me.chkShowDeleted.Text = "Show Deleted Items"
        Me.chkShowDeleted.UseVisualStyleBackColor = True
        '
        'tsmiChildShowInOsm
        '
        Me.tsmiChildShowInOsm.Name = "tsmiChildShowInOsm"
        Me.tsmiChildShowInOsm.Size = New System.Drawing.Size(146, 22)
        Me.tsmiChildShowInOsm.Text = "Show in OSM"
        '
        'tsmiChildAnalyze
        '
        Me.tsmiChildAnalyze.Name = "tsmiChildAnalyze"
        Me.tsmiChildAnalyze.Size = New System.Drawing.Size(146, 22)
        Me.tsmiChildAnalyze.Text = "Analyze"
        '
        'frmAnalyze
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1133, 670)
        Me.Controls.Add(Me.chkShowDeleted)
        Me.Controls.Add(Me.btnUpload)
        Me.Controls.Add(Me.btnCentroids)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnOSM)
        Me.Controls.Add(Me.btnHist)
        Me.Controls.Add(Me.txtAdminLevel)
        Me.Controls.Add(Me.ssStatus)
        Me.Controls.Add(Me.chkUpdateAll)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.btnGo)
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
        Me.Panel1.ResumeLayout(False)
        Me.scon1.Panel1.ResumeLayout(False)
        Me.scon1.Panel2.ResumeLayout(False)
        CType(Me.scon1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scon1.ResumeLayout(False)
        Me.scon2.Panel1.ResumeLayout(False)
        Me.scon2.Panel2.ResumeLayout(False)
        Me.scon2.Panel2.PerformLayout()
        CType(Me.scon2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scon2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents txtSingle As System.Windows.Forms.TextBox
    Friend WithEvents btnSingle As System.Windows.Forms.Button
    Friend WithEvents cbLists As System.Windows.Forms.ComboBox
    Friend WithEvents txtReport As System.Windows.Forms.TextBox
    Friend WithEvents btnGo As System.Windows.Forms.Button
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
    Friend WithEvents tsmiSearch As ToolStripMenuItem
    Friend WithEvents tvList As TreeView
    Friend WithEvents scon1 As SplitContainer
    Friend WithEvents scon2 As SplitContainer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnCentroids As Button
    Friend WithEvents ofdBoundaries As OpenFileDialog
    Friend WithEvents tsmiOpenWebsite As ToolStripMenuItem
    Friend WithEvents tsmiChildOpenWebsite As ToolStripMenuItem
    Friend WithEvents colWebsite As ColumnHeader
    Friend WithEvents tsmiChildReview As ToolStripMenuItem
    Friend WithEvents btnUpload As Button
    Friend WithEvents ofdUpload As OpenFileDialog
    Friend WithEvents chkShowDeleted As CheckBox
    Friend WithEvents tsmiShowInOsm As ToolStripMenuItem
    Friend WithEvents tsmiAnalyze As ToolStripMenuItem
    Friend WithEvents tsmiChildShowInOsm As ToolStripMenuItem
    Friend WithEvents tsmiChildAnalyze As ToolStripMenuItem
End Class
