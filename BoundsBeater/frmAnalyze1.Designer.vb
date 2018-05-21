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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAnalyze))
        Me.txtSingle = New System.Windows.Forms.TextBox()
        Me.btnSingle = New System.Windows.Forms.Button()
        Me.txtReport = New System.Windows.Forms.TextBox()
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
        Me.ssStatus = New System.Windows.Forms.StatusStrip()
        Me.tsProgress = New System.Windows.Forms.ToolStripProgressBar()
        Me.tsStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.btnHist = New System.Windows.Forms.Button()
        Me.cmsChild = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiChildEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiChildReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiChildOpenWebsite = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiChildReview = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiChildShowInOsm = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiChildAnalyze = New System.Windows.Forms.ToolStripMenuItem()
        Me.sfdReports = New System.Windows.Forms.SaveFileDialog()
        Me.tvList = New System.Windows.Forms.TreeView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.scon1 = New System.Windows.Forms.SplitContainer()
        Me.scon2 = New System.Windows.Forms.SplitContainer()
        Me.ofdBoundaries = New System.Windows.Forms.OpenFileDialog()
        Me.btnUpload = New System.Windows.Forms.Button()
        Me.ofdUpload = New System.Windows.Forms.OpenFileDialog()
        Me.chkShowDeleted = New System.Windows.Forms.CheckBox()
        Me.tsmiFlush = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiSearchNom = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.cmsNode = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsButtons = New System.Windows.Forms.ToolStrip()
        Me.tsmiOpenDB = New System.Windows.Forms.ToolStripButton()
        Me.tsmiSearchText = New System.Windows.Forms.ToolStripTextBox()
        Me.tsImport = New System.Windows.Forms.ToolStripDropDownButton()
        Me.tsmiImportCentroids = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiOsmBoundaries = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiSearch = New System.Windows.Forms.ToolStripSplitButton()
        Me.tsmiSearchNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiSearchAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiBrowseMap = New System.Windows.Forms.ToolStripButton()
        Me.tsmiEditMap = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiClearMap = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiClose = New System.Windows.Forms.ToolStripButton()
        Me.btnParent = New System.Windows.Forms.Button()
        Me.lvSearchResults = New System.Windows.Forms.ListView()
        Me.colBType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colBName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colBPar = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tabDetail.SuspendLayout()
        Me.tbpChildren.SuspendLayout()
        Me.tbpMap.SuspendLayout()
        Me.ssStatus.SuspendLayout()
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
        Me.cmsNode.SuspendLayout()
        Me.tsButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtSingle
        '
        Me.txtSingle.Location = New System.Drawing.Point(171, 56)
        Me.txtSingle.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSingle.Name = "txtSingle"
        Me.txtSingle.Size = New System.Drawing.Size(100, 22)
        Me.txtSingle.TabIndex = 2
        '
        'btnSingle
        '
        Me.btnSingle.Location = New System.Drawing.Point(280, 57)
        Me.btnSingle.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSingle.Name = "btnSingle"
        Me.btnSingle.Size = New System.Drawing.Size(49, 23)
        Me.btnSingle.TabIndex = 3
        Me.btnSingle.Text = "Go"
        Me.btnSingle.UseVisualStyleBackColor = True
        '
        'txtReport
        '
        Me.txtReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtReport.Location = New System.Drawing.Point(0, 0)
        Me.txtReport.Margin = New System.Windows.Forms.Padding(4)
        Me.txtReport.Multiline = True
        Me.txtReport.Name = "txtReport"
        Me.txtReport.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtReport.Size = New System.Drawing.Size(389, 320)
        Me.txtReport.TabIndex = 6
        '
        'tabDetail
        '
        Me.tabDetail.Controls.Add(Me.tbpChildren)
        Me.tabDetail.Controls.Add(Me.tbpMap)
        Me.tabDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabDetail.Location = New System.Drawing.Point(0, 0)
        Me.tabDetail.Margin = New System.Windows.Forms.Padding(4)
        Me.tabDetail.Name = "tabDetail"
        Me.tabDetail.SelectedIndex = 0
        Me.tabDetail.Size = New System.Drawing.Size(775, 651)
        Me.tabDetail.TabIndex = 11
        '
        'tbpChildren
        '
        Me.tbpChildren.Controls.Add(Me.lvChildren)
        Me.tbpChildren.Location = New System.Drawing.Point(4, 25)
        Me.tbpChildren.Margin = New System.Windows.Forms.Padding(4)
        Me.tbpChildren.Name = "tbpChildren"
        Me.tbpChildren.Padding = New System.Windows.Forms.Padding(4)
        Me.tbpChildren.Size = New System.Drawing.Size(767, 622)
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
        Me.lvChildren.Location = New System.Drawing.Point(4, 4)
        Me.lvChildren.Margin = New System.Windows.Forms.Padding(4)
        Me.lvChildren.Name = "lvChildren"
        Me.lvChildren.Size = New System.Drawing.Size(759, 614)
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
        Me.tbpMap.Location = New System.Drawing.Point(4, 25)
        Me.tbpMap.Margin = New System.Windows.Forms.Padding(4)
        Me.tbpMap.Name = "tbpMap"
        Me.tbpMap.Size = New System.Drawing.Size(767, 622)
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
        Me.wbMap.MinimumSize = New System.Drawing.Size(27, 25)
        Me.wbMap.Name = "wbMap"
        Me.wbMap.ScrollBarsEnabled = False
        Me.wbMap.Size = New System.Drawing.Size(767, 622)
        Me.wbMap.TabIndex = 0
        '
        'ssStatus
        '
        Me.ssStatus.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ssStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsProgress, Me.tsStatus})
        Me.ssStatus.Location = New System.Drawing.Point(0, 796)
        Me.ssStatus.Name = "ssStatus"
        Me.ssStatus.Padding = New System.Windows.Forms.Padding(1, 0, 19, 0)
        Me.ssStatus.Size = New System.Drawing.Size(1552, 29)
        Me.ssStatus.TabIndex = 14
        Me.ssStatus.Text = "StatusStrip1"
        '
        'tsProgress
        '
        Me.tsProgress.Name = "tsProgress"
        Me.tsProgress.Size = New System.Drawing.Size(133, 23)
        Me.tsProgress.Step = 1
        Me.tsProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'tsStatus
        '
        Me.tsStatus.Name = "tsStatus"
        Me.tsStatus.Size = New System.Drawing.Size(153, 24)
        Me.tsStatus.Text = "ToolStripStatusLabel1"
        '
        'btnHist
        '
        Me.btnHist.Location = New System.Drawing.Point(111, 56)
        Me.btnHist.Margin = New System.Windows.Forms.Padding(4)
        Me.btnHist.Name = "btnHist"
        Me.btnHist.Size = New System.Drawing.Size(52, 28)
        Me.btnHist.TabIndex = 16
        Me.btnHist.Text = "Hist"
        Me.btnHist.UseVisualStyleBackColor = True
        '
        'cmsChild
        '
        Me.cmsChild.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.cmsChild.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiChildEdit, Me.tsmiChildReport, Me.tsmiChildOpenWebsite, Me.tsmiChildReview, Me.tsmiChildShowInOsm, Me.tsmiChildAnalyze})
        Me.cmsChild.Name = "cmsChild"
        Me.cmsChild.Size = New System.Drawing.Size(171, 148)
        '
        'tsmiChildEdit
        '
        Me.tsmiChildEdit.Name = "tsmiChildEdit"
        Me.tsmiChildEdit.Size = New System.Drawing.Size(170, 24)
        Me.tsmiChildEdit.Text = "Edit"
        '
        'tsmiChildReport
        '
        Me.tsmiChildReport.Name = "tsmiChildReport"
        Me.tsmiChildReport.Size = New System.Drawing.Size(170, 24)
        Me.tsmiChildReport.Text = "Report"
        '
        'tsmiChildOpenWebsite
        '
        Me.tsmiChildOpenWebsite.Name = "tsmiChildOpenWebsite"
        Me.tsmiChildOpenWebsite.Size = New System.Drawing.Size(170, 24)
        Me.tsmiChildOpenWebsite.Text = "Go to website"
        '
        'tsmiChildReview
        '
        Me.tsmiChildReview.Name = "tsmiChildReview"
        Me.tsmiChildReview.Size = New System.Drawing.Size(170, 24)
        Me.tsmiChildReview.Text = "OSM Review"
        '
        'tsmiChildShowInOsm
        '
        Me.tsmiChildShowInOsm.Name = "tsmiChildShowInOsm"
        Me.tsmiChildShowInOsm.Size = New System.Drawing.Size(170, 24)
        Me.tsmiChildShowInOsm.Text = "Show in OSM"
        '
        'tsmiChildAnalyze
        '
        Me.tsmiChildAnalyze.Name = "tsmiChildAnalyze"
        Me.tsmiChildAnalyze.Size = New System.Drawing.Size(170, 24)
        Me.tsmiChildAnalyze.Text = "Analyze"
        '
        'tvList
        '
        Me.tvList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvList.HideSelection = False
        Me.tvList.Location = New System.Drawing.Point(0, 0)
        Me.tvList.Margin = New System.Windows.Forms.Padding(4)
        Me.tvList.Name = "tvList"
        Me.tvList.Size = New System.Drawing.Size(389, 326)
        Me.tvList.TabIndex = 9
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.scon1)
        Me.Panel1.Location = New System.Drawing.Point(15, 97)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1169, 651)
        Me.Panel1.TabIndex = 19
        '
        'scon1
        '
        Me.scon1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scon1.Location = New System.Drawing.Point(0, 0)
        Me.scon1.Margin = New System.Windows.Forms.Padding(4)
        Me.scon1.Name = "scon1"
        '
        'scon1.Panel1
        '
        Me.scon1.Panel1.Controls.Add(Me.scon2)
        '
        'scon1.Panel2
        '
        Me.scon1.Panel2.Controls.Add(Me.tabDetail)
        Me.scon1.Size = New System.Drawing.Size(1169, 651)
        Me.scon1.SplitterDistance = 389
        Me.scon1.SplitterWidth = 5
        Me.scon1.TabIndex = 0
        '
        'scon2
        '
        Me.scon2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scon2.Location = New System.Drawing.Point(0, 0)
        Me.scon2.Margin = New System.Windows.Forms.Padding(4)
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
        Me.scon2.Size = New System.Drawing.Size(389, 651)
        Me.scon2.SplitterDistance = 326
        Me.scon2.SplitterWidth = 5
        Me.scon2.TabIndex = 10
        '
        'ofdBoundaries
        '
        Me.ofdBoundaries.Filter = "CSV Files|*.csv|All Files|*.*"
        '
        'btnUpload
        '
        Me.btnUpload.Location = New System.Drawing.Point(626, 54)
        Me.btnUpload.Margin = New System.Windows.Forms.Padding(4)
        Me.btnUpload.Name = "btnUpload"
        Me.btnUpload.Size = New System.Drawing.Size(77, 28)
        Me.btnUpload.TabIndex = 21
        Me.btnUpload.Text = "Upload"
        Me.btnUpload.UseVisualStyleBackColor = True
        '
        'chkShowDeleted
        '
        Me.chkShowDeleted.AutoSize = True
        Me.chkShowDeleted.Location = New System.Drawing.Point(432, 57)
        Me.chkShowDeleted.Margin = New System.Windows.Forms.Padding(4)
        Me.chkShowDeleted.Name = "chkShowDeleted"
        Me.chkShowDeleted.Size = New System.Drawing.Size(154, 21)
        Me.chkShowDeleted.TabIndex = 22
        Me.chkShowDeleted.Text = "Show Deleted Items"
        Me.chkShowDeleted.UseVisualStyleBackColor = True
        '
        'tsmiFlush
        '
        Me.tsmiFlush.Name = "tsmiFlush"
        Me.tsmiFlush.Size = New System.Drawing.Size(226, 24)
        Me.tsmiFlush.Text = "Flush from cache"
        '
        'tsmiEdit
        '
        Me.tsmiEdit.Name = "tsmiEdit"
        Me.tsmiEdit.Size = New System.Drawing.Size(226, 24)
        Me.tsmiEdit.Text = "Edit"
        '
        'tsmiSearchNom
        '
        Me.tsmiSearchNom.Name = "tsmiSearchNom"
        Me.tsmiSearchNom.Size = New System.Drawing.Size(226, 24)
        Me.tsmiSearchNom.Text = "Search"
        '
        'tsmiShowAll
        '
        Me.tsmiShowAll.Name = "tsmiShowAll"
        Me.tsmiShowAll.Size = New System.Drawing.Size(226, 24)
        Me.tsmiShowAll.Text = "Show all on map"
        '
        'tsmiJSON
        '
        Me.tsmiJSON.Name = "tsmiJSON"
        Me.tsmiJSON.Size = New System.Drawing.Size(226, 24)
        Me.tsmiJSON.Text = "Show JSON"
        '
        'tsmiReport
        '
        Me.tsmiReport.Name = "tsmiReport"
        Me.tsmiReport.Size = New System.Drawing.Size(226, 24)
        Me.tsmiReport.Text = "Report"
        '
        'tsmiShowInOsm
        '
        Me.tsmiShowInOsm.Name = "tsmiShowInOsm"
        Me.tsmiShowInOsm.Size = New System.Drawing.Size(226, 24)
        Me.tsmiShowInOsm.Text = "Show in OSM"
        '
        'tsmiAnalyze
        '
        Me.tsmiAnalyze.Name = "tsmiAnalyze"
        Me.tsmiAnalyze.Size = New System.Drawing.Size(226, 24)
        Me.tsmiAnalyze.Text = "Analyze"
        '
        'tsmiChildOverviewReport
        '
        Me.tsmiChildOverviewReport.Name = "tsmiChildOverviewReport"
        Me.tsmiChildOverviewReport.Size = New System.Drawing.Size(226, 24)
        Me.tsmiChildOverviewReport.Text = "Child Overview Report"
        '
        'tsmiDeepChildReport
        '
        Me.tsmiDeepChildReport.Name = "tsmiDeepChildReport"
        Me.tsmiDeepChildReport.Size = New System.Drawing.Size(226, 24)
        Me.tsmiDeepChildReport.Text = "Deep Child Report"
        '
        'tsmiAddChild
        '
        Me.tsmiAddChild.Name = "tsmiAddChild"
        Me.tsmiAddChild.Size = New System.Drawing.Size(226, 24)
        Me.tsmiAddChild.Text = "Add Child"
        '
        'tsmiReview
        '
        Me.tsmiReview.Name = "tsmiReview"
        Me.tsmiReview.Size = New System.Drawing.Size(226, 24)
        Me.tsmiReview.Text = "Review OSM"
        '
        'tsmiOpenWebsite
        '
        Me.tsmiOpenWebsite.Name = "tsmiOpenWebsite"
        Me.tsmiOpenWebsite.Size = New System.Drawing.Size(226, 24)
        Me.tsmiOpenWebsite.Text = "Go to website"
        '
        'cmsNode
        '
        Me.cmsNode.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.cmsNode.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiFlush, Me.tsmiEdit, Me.tsmiSearchNom, Me.tsmiShowAll, Me.tsmiJSON, Me.tsmiReport, Me.tsmiShowInOsm, Me.tsmiAnalyze, Me.tsmiChildOverviewReport, Me.tsmiDeepChildReport, Me.tsmiAddChild, Me.tsmiReview, Me.tsmiOpenWebsite})
        Me.cmsNode.Name = "ContextMenuStrip1"
        Me.cmsNode.Size = New System.Drawing.Size(227, 316)
        '
        'tsButtons
        '
        Me.tsButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tsButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiOpenDB, Me.tsmiSearchText, Me.tsImport, Me.ToolStripSeparator1, Me.tsmiSearch, Me.ToolStripSeparator2, Me.tsmiBrowseMap, Me.tsmiEditMap, Me.ToolStripSeparator3, Me.tsmiClearMap, Me.ToolStripSeparator4, Me.tsmiClose})
        Me.tsButtons.Location = New System.Drawing.Point(0, 0)
        Me.tsButtons.Name = "tsButtons"
        Me.tsButtons.Size = New System.Drawing.Size(1552, 39)
        Me.tsButtons.TabIndex = 25
        Me.tsButtons.Text = "ToolStrip1"
        '
        'tsmiOpenDB
        '
        Me.tsmiOpenDB.Image = CType(resources.GetObject("tsmiOpenDB.Image"), System.Drawing.Image)
        Me.tsmiOpenDB.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsmiOpenDB.Name = "tsmiOpenDB"
        Me.tsmiOpenDB.Size = New System.Drawing.Size(78, 36)
        Me.tsmiOpenDB.Text = "Load"
        Me.tsmiOpenDB.ToolTipText = "Load Boundary DB"
        '
        'tsmiSearchText
        '
        Me.tsmiSearchText.CausesValidation = False
        Me.tsmiSearchText.Name = "tsmiSearchText"
        Me.tsmiSearchText.Size = New System.Drawing.Size(100, 39)
        '
        'tsImport
        '
        Me.tsImport.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiImportCentroids, Me.tsmiOsmBoundaries})
        Me.tsImport.Image = CType(resources.GetObject("tsImport.Image"), System.Drawing.Image)
        Me.tsImport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsImport.Name = "tsImport"
        Me.tsImport.Size = New System.Drawing.Size(100, 36)
        Me.tsImport.Text = "Import"
        Me.tsImport.ToolTipText = "Import Stuff"
        '
        'tsmiImportCentroids
        '
        Me.tsmiImportCentroids.Name = "tsmiImportCentroids"
        Me.tsmiImportCentroids.Size = New System.Drawing.Size(255, 26)
        Me.tsmiImportCentroids.Text = "Centroids"
        '
        'tsmiOsmBoundaries
        '
        Me.tsmiOsmBoundaries.Name = "tsmiOsmBoundaries"
        Me.tsmiOsmBoundaries.Size = New System.Drawing.Size(255, 26)
        Me.tsmiOsmBoundaries.Text = "Boundaries from OSM file"
        Me.tsmiOsmBoundaries.ToolTipText = "Merge boundary data from an OSM file"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 39)
        '
        'tsmiSearch
        '
        Me.tsmiSearch.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiSearchNew, Me.tsmiSearchAll})
        Me.tsmiSearch.Image = CType(resources.GetObject("tsmiSearch.Image"), System.Drawing.Image)
        Me.tsmiSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsmiSearch.Name = "tsmiSearch"
        Me.tsmiSearch.Size = New System.Drawing.Size(127, 36)
        Me.tsmiSearch.Text = "Scan OSM"
        '
        'tsmiSearchNew
        '
        Me.tsmiSearchNew.Name = "tsmiSearchNew"
        Me.tsmiSearchNew.Size = New System.Drawing.Size(149, 26)
        Me.tsmiSearchNew.Text = "Scan New"
        Me.tsmiSearchNew.ToolTipText = "Search New"
        '
        'tsmiSearchAll
        '
        Me.tsmiSearchAll.Name = "tsmiSearchAll"
        Me.tsmiSearchAll.Size = New System.Drawing.Size(149, 26)
        Me.tsmiSearchAll.Text = "Scan All"
        Me.tsmiSearchAll.ToolTipText = "Search All"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 39)
        '
        'tsmiBrowseMap
        '
        Me.tsmiBrowseMap.Image = CType(resources.GetObject("tsmiBrowseMap.Image"), System.Drawing.Image)
        Me.tsmiBrowseMap.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsmiBrowseMap.Name = "tsmiBrowseMap"
        Me.tsmiBrowseMap.Size = New System.Drawing.Size(115, 36)
        Me.tsmiBrowseMap.Text = "Open map"
        '
        'tsmiEditMap
        '
        Me.tsmiEditMap.Image = CType(resources.GetObject("tsmiEditMap.Image"), System.Drawing.Image)
        Me.tsmiEditMap.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsmiEditMap.Name = "tsmiEditMap"
        Me.tsmiEditMap.Size = New System.Drawing.Size(105, 36)
        Me.tsmiEditMap.Text = "Edit map"
        Me.tsmiEditMap.ToolTipText = "Edit map in browser"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 39)
        '
        'tsmiClearMap
        '
        Me.tsmiClearMap.Image = CType(resources.GetObject("tsmiClearMap.Image"), System.Drawing.Image)
        Me.tsmiClearMap.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsmiClearMap.Name = "tsmiClearMap"
        Me.tsmiClearMap.Size = New System.Drawing.Size(79, 36)
        Me.tsmiClearMap.Text = "Clear"
        Me.tsmiClearMap.ToolTipText = "Clear map layers"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 39)
        '
        'tsmiClose
        '
        Me.tsmiClose.Image = CType(resources.GetObject("tsmiClose.Image"), System.Drawing.Image)
        Me.tsmiClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsmiClose.Name = "tsmiClose"
        Me.tsmiClose.Size = New System.Drawing.Size(81, 36)
        Me.tsmiClose.Text = "Close"
        '
        'btnParent
        '
        Me.btnParent.Location = New System.Drawing.Point(15, 54)
        Me.btnParent.Name = "btnParent"
        Me.btnParent.Size = New System.Drawing.Size(75, 30)
        Me.btnParent.TabIndex = 26
        Me.btnParent.Text = "Parent"
        Me.btnParent.UseVisualStyleBackColor = True
        '
        'lvSearchResults
        '
        Me.lvSearchResults.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colBType, Me.colBName, Me.colBPar})
        Me.lvSearchResults.FullRowSelect = True
        Me.lvSearchResults.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvSearchResults.HideSelection = False
        Me.lvSearchResults.Location = New System.Drawing.Point(89, 42)
        Me.lvSearchResults.MultiSelect = False
        Me.lvSearchResults.Name = "lvSearchResults"
        Me.lvSearchResults.Size = New System.Drawing.Size(625, 167)
        Me.lvSearchResults.TabIndex = 27
        Me.lvSearchResults.UseCompatibleStateImageBehavior = False
        Me.lvSearchResults.View = System.Windows.Forms.View.Details
        '
        'colBType
        '
        Me.colBType.Text = "Type"
        Me.colBType.Width = 48
        '
        'colBName
        '
        Me.colBName.Text = "Name"
        Me.colBName.Width = 200
        '
        'colBPar
        '
        Me.colBPar.Text = "Parent"
        Me.colBPar.Width = 200
        '
        'frmAnalyze
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1552, 825)
        Me.Controls.Add(Me.lvSearchResults)
        Me.Controls.Add(Me.btnParent)
        Me.Controls.Add(Me.tsButtons)
        Me.Controls.Add(Me.chkShowDeleted)
        Me.Controls.Add(Me.btnUpload)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnHist)
        Me.Controls.Add(Me.ssStatus)
        Me.Controls.Add(Me.btnSingle)
        Me.Controls.Add(Me.txtSingle)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmAnalyze"
        Me.Text = "Boundary Analyzer"
        Me.tabDetail.ResumeLayout(False)
        Me.tbpChildren.ResumeLayout(False)
        Me.tbpMap.ResumeLayout(False)
        Me.ssStatus.ResumeLayout(False)
        Me.ssStatus.PerformLayout()
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
        Me.cmsNode.ResumeLayout(False)
        Me.tsButtons.ResumeLayout(False)
        Me.tsButtons.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtSingle As System.Windows.Forms.TextBox
    Friend WithEvents btnSingle As System.Windows.Forms.Button
    Friend WithEvents txtReport As System.Windows.Forms.TextBox
    Friend WithEvents tabDetail As System.Windows.Forms.TabControl
    Friend WithEvents tbpChildren As System.Windows.Forms.TabPage
    Friend WithEvents lvChildren As System.Windows.Forms.ListView
    Friend WithEvents tbpMap As System.Windows.Forms.TabPage
    Friend WithEvents wbMap As System.Windows.Forms.WebBrowser
    Friend WithEvents ssStatus As System.Windows.Forms.StatusStrip
    Friend WithEvents tsProgress As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents tsStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents colName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colOSMID As System.Windows.Forms.ColumnHeader
    Friend WithEvents colType As System.Windows.Forms.ColumnHeader
    Friend WithEvents colGSS As System.Windows.Forms.ColumnHeader
    Friend WithEvents colCouncilStyle As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnHist As System.Windows.Forms.Button
    Friend WithEvents colCouncilName As ColumnHeader
    Friend WithEvents cmsChild As ContextMenuStrip
    Friend WithEvents tsmiChildEdit As ToolStripMenuItem
    Friend WithEvents colParishType As ColumnHeader
    Friend WithEvents tsmiChildReport As ToolStripMenuItem
    Friend WithEvents sfdReports As SaveFileDialog
    Friend WithEvents tvList As TreeView
    Friend WithEvents scon1 As SplitContainer
    Friend WithEvents scon2 As SplitContainer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ofdBoundaries As OpenFileDialog
    Friend WithEvents tsmiChildOpenWebsite As ToolStripMenuItem
    Friend WithEvents colWebsite As ColumnHeader
    Friend WithEvents tsmiChildReview As ToolStripMenuItem
    Friend WithEvents btnUpload As Button
    Friend WithEvents ofdUpload As OpenFileDialog
    Friend WithEvents chkShowDeleted As CheckBox
    Friend WithEvents tsmiChildShowInOsm As ToolStripMenuItem
    Friend WithEvents tsmiChildAnalyze As ToolStripMenuItem
    Friend WithEvents tsmiFlush As ToolStripMenuItem
    Friend WithEvents tsmiEdit As ToolStripMenuItem
    Friend WithEvents tsmiSearchNom As ToolStripMenuItem
    Friend WithEvents tsmiShowAll As ToolStripMenuItem
    Friend WithEvents tsmiJSON As ToolStripMenuItem
    Friend WithEvents tsmiReport As ToolStripMenuItem
    Friend WithEvents tsmiShowInOsm As ToolStripMenuItem
    Friend WithEvents tsmiAnalyze As ToolStripMenuItem
    Friend WithEvents tsmiChildOverviewReport As ToolStripMenuItem
    Friend WithEvents tsmiDeepChildReport As ToolStripMenuItem
    Friend WithEvents tsmiAddChild As ToolStripMenuItem
    Friend WithEvents tsmiReview As ToolStripMenuItem
    Friend WithEvents tsmiOpenWebsite As ToolStripMenuItem
    Friend WithEvents cmsNode As ContextMenuStrip
    Friend WithEvents tsButtons As ToolStrip
    Friend WithEvents tsmiOpenDB As ToolStripButton
    Friend WithEvents tsImport As ToolStripDropDownButton
    Friend WithEvents tsmiImportCentroids As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents tsmiOsmBoundaries As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents tsmiBrowseMap As ToolStripButton
    Friend WithEvents tsmiEditMap As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents tsmiClearMap As ToolStripButton
    Friend WithEvents tsmiSearch As ToolStripSplitButton
    Friend WithEvents tsmiSearchNew As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents tsmiClose As ToolStripButton
    Friend WithEvents tsmiSearchAll As ToolStripMenuItem
    Friend WithEvents btnParent As Button
    Friend WithEvents tsmiSearchText As ToolStripTextBox
    Friend WithEvents lvSearchResults As ListView
    Friend WithEvents colBType As ColumnHeader
    Friend WithEvents colBName As ColumnHeader
    Friend WithEvents colBPar As ColumnHeader
End Class
