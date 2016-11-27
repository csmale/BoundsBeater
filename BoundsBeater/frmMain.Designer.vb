<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.btnClose = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.btnReport = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.btnResolve = New System.Windows.Forms.Button()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.tvBounds = New System.Windows.Forms.TreeView()
        Me.tvReload = New System.Windows.Forms.Button()
        Me.lvBounds = New System.Windows.Forms.ListView()
        Me.colID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colLevel = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colDesgn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colONS = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chkOnline = New System.Windows.Forms.CheckBox()
        Me.btnReportOne = New System.Windows.Forms.Button()
        Me.btnAnalyzer = New System.Windows.Forms.Button()
        Me.btnRelMgr = New System.Windows.Forms.Button()
        Me.btnRecreate = New System.Windows.Forms.Button()
        Me.tvDir = New System.Windows.Forms.TreeView()
        Me.btnDir = New System.Windows.Forms.Button()
        Me.btnAnalyseWay = New System.Windows.Forms.Button()
        Me.txtWayID = New System.Windows.Forms.TextBox()
        Me.btnTest = New System.Windows.Forms.Button()
        Me.btnPrefs = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(794, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 0
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'btnOpen
        '
        Me.btnOpen.Location = New System.Drawing.Point(13, 13)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(75, 23)
        Me.btnOpen.TabIndex = 1
        Me.btnOpen.Text = "Open..."
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'btnReport
        '
        Me.btnReport.Location = New System.Drawing.Point(13, 51)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(75, 23)
        Me.btnReport.TabIndex = 2
        Me.btnReport.Text = "Report"
        Me.btnReport.UseVisualStyleBackColor = True
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(94, 13)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(37, 13)
        Me.lblStatus.TabIndex = 3
        Me.lblStatus.Text = "Status"
        '
        'btnResolve
        '
        Me.btnResolve.Location = New System.Drawing.Point(157, 91)
        Me.btnResolve.Name = "btnResolve"
        Me.btnResolve.Size = New System.Drawing.Size(75, 23)
        Me.btnResolve.TabIndex = 4
        Me.btnResolve.Text = "Resolve"
        Me.btnResolve.UseVisualStyleBackColor = True
        '
        'txtID
        '
        Me.txtID.Location = New System.Drawing.Point(13, 91)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(138, 20)
        Me.txtID.TabIndex = 5
        '
        'tvBounds
        '
        Me.tvBounds.FullRowSelect = True
        Me.tvBounds.HideSelection = False
        Me.tvBounds.Location = New System.Drawing.Point(13, 118)
        Me.tvBounds.Name = "tvBounds"
        Me.tvBounds.Size = New System.Drawing.Size(331, 66)
        Me.tvBounds.TabIndex = 6
        '
        'tvReload
        '
        Me.tvReload.Location = New System.Drawing.Point(713, 8)
        Me.tvReload.Name = "tvReload"
        Me.tvReload.Size = New System.Drawing.Size(75, 23)
        Me.tvReload.TabIndex = 7
        Me.tvReload.Text = "Reload"
        Me.tvReload.UseVisualStyleBackColor = True
        '
        'lvBounds
        '
        Me.lvBounds.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colID, Me.colType, Me.colLevel, Me.colName, Me.colDesgn, Me.colONS})
        Me.lvBounds.FullRowSelect = True
        Me.lvBounds.HideSelection = False
        Me.lvBounds.Location = New System.Drawing.Point(365, 118)
        Me.lvBounds.Name = "lvBounds"
        Me.lvBounds.Size = New System.Drawing.Size(504, 370)
        Me.lvBounds.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvBounds.TabIndex = 8
        Me.lvBounds.UseCompatibleStateImageBehavior = False
        Me.lvBounds.View = System.Windows.Forms.View.Details
        '
        'colID
        '
        Me.colID.Text = "ID"
        '
        'colType
        '
        Me.colType.Text = "Type"
        '
        'colLevel
        '
        Me.colLevel.Text = "Level"
        Me.colLevel.Width = 40
        '
        'colName
        '
        Me.colName.Text = "Name"
        Me.colName.Width = 140
        '
        'colDesgn
        '
        Me.colDesgn.Text = "Designation"
        '
        'colONS
        '
        Me.colONS.Text = "GSS"
        '
        'chkOnline
        '
        Me.chkOnline.AutoSize = True
        Me.chkOnline.Checked = True
        Me.chkOnline.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOnline.Location = New System.Drawing.Point(319, 94)
        Me.chkOnline.Name = "chkOnline"
        Me.chkOnline.Size = New System.Drawing.Size(121, 17)
        Me.chkOnline.TabIndex = 9
        Me.chkOnline.Text = "Retreive data online"
        Me.chkOnline.UseVisualStyleBackColor = True
        '
        'btnReportOne
        '
        Me.btnReportOne.Location = New System.Drawing.Point(238, 91)
        Me.btnReportOne.Name = "btnReportOne"
        Me.btnReportOne.Size = New System.Drawing.Size(75, 23)
        Me.btnReportOne.TabIndex = 10
        Me.btnReportOne.Text = "Single Rpt"
        Me.btnReportOne.UseVisualStyleBackColor = True
        '
        'btnAnalyzer
        '
        Me.btnAnalyzer.Location = New System.Drawing.Point(560, 91)
        Me.btnAnalyzer.Name = "btnAnalyzer"
        Me.btnAnalyzer.Size = New System.Drawing.Size(75, 23)
        Me.btnAnalyzer.TabIndex = 11
        Me.btnAnalyzer.Text = "Analyzer"
        Me.btnAnalyzer.UseVisualStyleBackColor = True
        '
        'btnRelMgr
        '
        Me.btnRelMgr.Location = New System.Drawing.Point(715, 43)
        Me.btnRelMgr.Name = "btnRelMgr"
        Me.btnRelMgr.Size = New System.Drawing.Size(73, 21)
        Me.btnRelMgr.TabIndex = 12
        Me.btnRelMgr.Text = "Rel Mgr"
        Me.btnRelMgr.UseVisualStyleBackColor = True
        '
        'btnRecreate
        '
        Me.btnRecreate.Location = New System.Drawing.Point(792, 91)
        Me.btnRecreate.Name = "btnRecreate"
        Me.btnRecreate.Size = New System.Drawing.Size(66, 22)
        Me.btnRecreate.TabIndex = 13
        Me.btnRecreate.Text = "Recreate"
        Me.btnRecreate.UseVisualStyleBackColor = True
        '
        'tvDir
        '
        Me.tvDir.Location = New System.Drawing.Point(12, 190)
        Me.tvDir.Name = "tvDir"
        Me.tvDir.Size = New System.Drawing.Size(332, 298)
        Me.tvDir.TabIndex = 14
        '
        'btnDir
        '
        Me.btnDir.Location = New System.Drawing.Point(174, 51)
        Me.btnDir.Name = "btnDir"
        Me.btnDir.Size = New System.Drawing.Size(75, 23)
        Me.btnDir.TabIndex = 15
        Me.btnDir.Text = "Directory"
        Me.btnDir.UseVisualStyleBackColor = True
        '
        'btnAnalyseWay
        '
        Me.btnAnalyseWay.Location = New System.Drawing.Point(560, 62)
        Me.btnAnalyseWay.Name = "btnAnalyseWay"
        Me.btnAnalyseWay.Size = New System.Drawing.Size(75, 23)
        Me.btnAnalyseWay.TabIndex = 16
        Me.btnAnalyseWay.Text = "Show Way"
        Me.btnAnalyseWay.UseVisualStyleBackColor = True
        '
        'txtWayID
        '
        Me.txtWayID.Location = New System.Drawing.Point(454, 62)
        Me.txtWayID.Name = "txtWayID"
        Me.txtWayID.Size = New System.Drawing.Size(100, 20)
        Me.txtWayID.TabIndex = 17
        '
        'btnTest
        '
        Me.btnTest.Location = New System.Drawing.Point(319, 12)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(75, 23)
        Me.btnTest.TabIndex = 18
        Me.btnTest.Text = "db test"
        Me.btnTest.UseVisualStyleBackColor = True
        '
        'btnPrefs
        '
        Me.btnPrefs.Location = New System.Drawing.Point(794, 41)
        Me.btnPrefs.Name = "btnPrefs"
        Me.btnPrefs.Size = New System.Drawing.Size(75, 23)
        Me.btnPrefs.TabIndex = 19
        Me.btnPrefs.Text = "Preferences"
        Me.btnPrefs.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(881, 500)
        Me.Controls.Add(Me.btnPrefs)
        Me.Controls.Add(Me.btnTest)
        Me.Controls.Add(Me.txtWayID)
        Me.Controls.Add(Me.btnAnalyseWay)
        Me.Controls.Add(Me.btnDir)
        Me.Controls.Add(Me.tvDir)
        Me.Controls.Add(Me.btnRecreate)
        Me.Controls.Add(Me.btnRelMgr)
        Me.Controls.Add(Me.btnAnalyzer)
        Me.Controls.Add(Me.btnReportOne)
        Me.Controls.Add(Me.chkOnline)
        Me.Controls.Add(Me.lvBounds)
        Me.Controls.Add(Me.tvReload)
        Me.Controls.Add(Me.tvBounds)
        Me.Controls.Add(Me.txtID)
        Me.Controls.Add(Me.btnResolve)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.btnReport)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.btnClose)
        Me.Name = "frmMain"
        Me.Text = "BoundaryBeater"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnOpen As System.Windows.Forms.Button
    Friend WithEvents btnReport As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents btnResolve As System.Windows.Forms.Button
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents tvBounds As System.Windows.Forms.TreeView
    Friend WithEvents tvReload As System.Windows.Forms.Button
    Friend WithEvents lvBounds As System.Windows.Forms.ListView
    Friend WithEvents colID As System.Windows.Forms.ColumnHeader
    Friend WithEvents colType As System.Windows.Forms.ColumnHeader
    Friend WithEvents colLevel As System.Windows.Forms.ColumnHeader
    Friend WithEvents colName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colDesgn As System.Windows.Forms.ColumnHeader
    Friend WithEvents colONS As System.Windows.Forms.ColumnHeader
    Friend WithEvents chkOnline As System.Windows.Forms.CheckBox
    Friend WithEvents btnReportOne As System.Windows.Forms.Button
    Friend WithEvents btnAnalyzer As System.Windows.Forms.Button
    Friend WithEvents btnRelMgr As System.Windows.Forms.Button
    Friend WithEvents btnRecreate As System.Windows.Forms.Button
    Friend WithEvents tvDir As System.Windows.Forms.TreeView
    Friend WithEvents btnDir As System.Windows.Forms.Button
    Friend WithEvents btnAnalyseWay As System.Windows.Forms.Button
    Friend WithEvents txtWayID As System.Windows.Forms.TextBox
    Friend WithEvents btnTest As System.Windows.Forms.Button
    Friend WithEvents btnPrefs As Button
End Class
