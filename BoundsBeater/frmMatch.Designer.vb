<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMatch
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
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.lvAreas = New System.Windows.Forms.ListView()
        Me.colName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colOSMid = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colONSCode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colParent = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblLevel = New System.Windows.Forms.Label()
        Me.lblCouncilName = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblDesignation = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblBoundaryType = New System.Windows.Forms.Label()
        Me.btnAbort = New System.Windows.Forms.Button()
        Me.txtMatcher = New System.Windows.Forms.TextBox()
        Me.btnForget = New System.Windows.Forms.Button()
        Me.lblComment = New System.Windows.Forms.Label()
        Me.llblOSMID = New System.Windows.Forms.LinkLabel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblGSS = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblParishType = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(609, 12)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.Text = "Skip"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(609, 41)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "Update"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'lvAreas
        '
        Me.lvAreas.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colName, Me.colType, Me.colOSMid, Me.colONSCode, Me.colParent})
        Me.lvAreas.FullRowSelect = True
        Me.lvAreas.HideSelection = False
        Me.lvAreas.Location = New System.Drawing.Point(12, 148)
        Me.lvAreas.MultiSelect = False
        Me.lvAreas.Name = "lvAreas"
        Me.lvAreas.Size = New System.Drawing.Size(663, 248)
        Me.lvAreas.TabIndex = 2
        Me.lvAreas.UseCompatibleStateImageBehavior = False
        Me.lvAreas.View = System.Windows.Forms.View.Details
        '
        'colName
        '
        Me.colName.Text = "Name"
        Me.colName.Width = 162
        '
        'colType
        '
        Me.colType.Text = "Type"
        Me.colType.Width = 79
        '
        'colOSMid
        '
        Me.colOSMid.Text = "OSMID"
        '
        'colONSCode
        '
        Me.colONSCode.Text = "ONS"
        Me.colONSCode.Width = 103
        '
        'colParent
        '
        Me.colParent.Text = "Parent"
        Me.colParent.Width = 167
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Name:"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(110, 21)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(88, 13)
        Me.lblName.TabIndex = 4
        Me.lblName.Text = "admin area name"
        Me.lblName.UseMnemonic = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(21, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Admin Level:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(21, 78)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Council Name:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(21, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "OSM ID:"
        '
        'lblLevel
        '
        Me.lblLevel.AutoSize = True
        Me.lblLevel.Location = New System.Drawing.Point(110, 59)
        Me.lblLevel.Name = "lblLevel"
        Me.lblLevel.Size = New System.Drawing.Size(60, 13)
        Me.lblLevel.TabIndex = 8
        Me.lblLevel.Text = "admin level"
        Me.lblLevel.UseMnemonic = False
        '
        'lblCouncilName
        '
        Me.lblCouncilName.AutoSize = True
        Me.lblCouncilName.Location = New System.Drawing.Point(110, 78)
        Me.lblCouncilName.Name = "lblCouncilName"
        Me.lblCouncilName.Size = New System.Drawing.Size(70, 13)
        Me.lblCouncilName.TabIndex = 9
        Me.lblCouncilName.Text = "council name"
        Me.lblCouncilName.UseMnemonic = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(21, 116)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Designation:"
        '
        'lblDesignation
        '
        Me.lblDesignation.AutoSize = True
        Me.lblDesignation.Location = New System.Drawing.Point(110, 116)
        Me.lblDesignation.Name = "lblDesignation"
        Me.lblDesignation.Size = New System.Drawing.Size(61, 13)
        Me.lblDesignation.TabIndex = 12
        Me.lblDesignation.Text = "designation"
        Me.lblDesignation.UseMnemonic = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(21, 40)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(34, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Type:"
        '
        'lblBoundaryType
        '
        Me.lblBoundaryType.AutoSize = True
        Me.lblBoundaryType.Location = New System.Drawing.Point(110, 40)
        Me.lblBoundaryType.Name = "lblBoundaryType"
        Me.lblBoundaryType.Size = New System.Drawing.Size(74, 13)
        Me.lblBoundaryType.TabIndex = 14
        Me.lblBoundaryType.Text = "boundary type"
        Me.lblBoundaryType.UseMnemonic = False
        '
        'btnAbort
        '
        Me.btnAbort.Location = New System.Drawing.Point(609, 73)
        Me.btnAbort.Name = "btnAbort"
        Me.btnAbort.Size = New System.Drawing.Size(75, 23)
        Me.btnAbort.TabIndex = 15
        Me.btnAbort.Text = "Abort"
        Me.btnAbort.UseVisualStyleBackColor = True
        '
        'txtMatcher
        '
        Me.txtMatcher.Location = New System.Drawing.Point(503, 113)
        Me.txtMatcher.Name = "txtMatcher"
        Me.txtMatcher.Size = New System.Drawing.Size(172, 20)
        Me.txtMatcher.TabIndex = 16
        '
        'btnForget
        '
        Me.btnForget.Location = New System.Drawing.Point(332, 106)
        Me.btnForget.Name = "btnForget"
        Me.btnForget.Size = New System.Drawing.Size(75, 23)
        Me.btnForget.TabIndex = 17
        Me.btnForget.Text = "Forget"
        Me.btnForget.UseVisualStyleBackColor = True
        '
        'lblComment
        '
        Me.lblComment.AutoSize = True
        Me.lblComment.Location = New System.Drawing.Point(332, 2)
        Me.lblComment.Name = "lblComment"
        Me.lblComment.Size = New System.Drawing.Size(51, 13)
        Me.lblComment.TabIndex = 18
        Me.lblComment.Text = "Comment"
        '
        'llblOSMID
        '
        Me.llblOSMID.AutoSize = True
        Me.llblOSMID.Location = New System.Drawing.Point(110, 97)
        Me.llblOSMID.Name = "llblOSMID"
        Me.llblOSMID.Size = New System.Drawing.Size(34, 13)
        Me.llblOSMID.TabIndex = 19
        Me.llblOSMID.TabStop = True
        Me.llblOSMID.Text = "osmid"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(329, 59)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(32, 13)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "GSS:"
        '
        'lblGSS
        '
        Me.lblGSS.AutoSize = True
        Me.lblGSS.Location = New System.Drawing.Point(397, 59)
        Me.lblGSS.Name = "lblGSS"
        Me.lblGSS.Size = New System.Drawing.Size(50, 13)
        Me.lblGSS.TabIndex = 21
        Me.lblGSS.Text = "gss code"
        Me.lblGSS.UseMnemonic = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(329, 78)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(62, 13)
        Me.Label8.TabIndex = 22
        Me.Label8.Text = "Parish type:"
        '
        'lblParishType
        '
        Me.lblParishType.AutoSize = True
        Me.lblParishType.Location = New System.Drawing.Point(397, 78)
        Me.lblParishType.Name = "lblParishType"
        Me.lblParishType.Size = New System.Drawing.Size(58, 13)
        Me.lblParishType.TabIndex = 23
        Me.lblParishType.Text = "parish type"
        '
        'frmMatch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(687, 408)
        Me.Controls.Add(Me.lblParishType)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.lblGSS)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.llblOSMID)
        Me.Controls.Add(Me.lblComment)
        Me.Controls.Add(Me.btnForget)
        Me.Controls.Add(Me.txtMatcher)
        Me.Controls.Add(Me.btnAbort)
        Me.Controls.Add(Me.lblBoundaryType)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lblDesignation)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblCouncilName)
        Me.Controls.Add(Me.lblLevel)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lvAreas)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnCancel)
        Me.MinimizeBox = False
        Me.Name = "frmMatch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Identify OSM Admin Boundary Relation"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents lvAreas As System.Windows.Forms.ListView
    Friend WithEvents colName As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents colType As System.Windows.Forms.ColumnHeader
    Friend WithEvents colOSMid As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblLevel As System.Windows.Forms.Label
    Friend WithEvents lblCouncilName As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblDesignation As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblBoundaryType As System.Windows.Forms.Label
    Friend WithEvents btnAbort As System.Windows.Forms.Button
    Friend WithEvents colParent As System.Windows.Forms.ColumnHeader
    Friend WithEvents colONSCode As System.Windows.Forms.ColumnHeader
    Friend WithEvents txtMatcher As System.Windows.Forms.TextBox
    Friend WithEvents btnForget As System.Windows.Forms.Button
    Friend WithEvents lblComment As System.Windows.Forms.Label
    Friend WithEvents llblOSMID As System.Windows.Forms.LinkLabel
    Friend WithEvents Label7 As Label
    Friend WithEvents lblGSS As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents lblParishType As Label
End Class
