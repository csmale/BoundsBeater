<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEdit
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
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.txtName2 = New System.Windows.Forms.TextBox()
        Me.txtCouncilName = New System.Windows.Forms.TextBox()
        Me.txtCouncilName2 = New System.Windows.Forms.TextBox()
        Me.cbStyle = New System.Windows.Forms.ComboBox()
        Me.cbParishType = New System.Windows.Forms.ComboBox()
        Me.cbType = New System.Windows.Forms.ComboBox()
        Me.txtGSS = New System.Windows.Forms.TextBox()
        Me.chkBorough = New System.Windows.Forms.CheckBox()
        Me.chkRoyal = New System.Windows.Forms.CheckBox()
        Me.chkCity = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.cbGroup = New System.Windows.Forms.ComboBox()
        Me.btnNewGroup = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnSelRelation = New System.Windows.Forms.Button()
        Me.txtRelID = New System.Windows.Forms.TextBox()
        Me.btnAutoName = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtLat = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtLon = New System.Windows.Forms.TextBox()
        Me.txtWebsite = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.btnOpenWebsite = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(236, 439)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(136, 439)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 133)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Council Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 111)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "GSS code"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 179)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Council Style"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(14, 202)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(63, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Parish Type"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(14, 34)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 13)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Name (Welsh)"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(14, 90)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(79, 13)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Boundary Type"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(14, 155)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(112, 13)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Council Name (Welsh)"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(136, 6)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(290, 20)
        Me.txtName.TabIndex = 11
        '
        'txtName2
        '
        Me.txtName2.Location = New System.Drawing.Point(136, 27)
        Me.txtName2.Name = "txtName2"
        Me.txtName2.Size = New System.Drawing.Size(290, 20)
        Me.txtName2.TabIndex = 12
        '
        'txtCouncilName
        '
        Me.txtCouncilName.Location = New System.Drawing.Point(136, 126)
        Me.txtCouncilName.Name = "txtCouncilName"
        Me.txtCouncilName.Size = New System.Drawing.Size(266, 20)
        Me.txtCouncilName.TabIndex = 13
        '
        'txtCouncilName2
        '
        Me.txtCouncilName2.Location = New System.Drawing.Point(136, 148)
        Me.txtCouncilName2.Name = "txtCouncilName2"
        Me.txtCouncilName2.Size = New System.Drawing.Size(266, 20)
        Me.txtCouncilName2.TabIndex = 14
        '
        'cbStyle
        '
        Me.cbStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbStyle.FormattingEnabled = True
        Me.cbStyle.Location = New System.Drawing.Point(136, 171)
        Me.cbStyle.Name = "cbStyle"
        Me.cbStyle.Size = New System.Drawing.Size(100, 21)
        Me.cbStyle.TabIndex = 15
        '
        'cbParishType
        '
        Me.cbParishType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbParishType.FormattingEnabled = True
        Me.cbParishType.Location = New System.Drawing.Point(136, 194)
        Me.cbParishType.Name = "cbParishType"
        Me.cbParishType.Size = New System.Drawing.Size(121, 21)
        Me.cbParishType.TabIndex = 16
        '
        'cbType
        '
        Me.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbType.FormattingEnabled = True
        Me.cbType.Location = New System.Drawing.Point(136, 82)
        Me.cbType.Name = "cbType"
        Me.cbType.Size = New System.Drawing.Size(290, 21)
        Me.cbType.TabIndex = 17
        '
        'txtGSS
        '
        Me.txtGSS.Location = New System.Drawing.Point(136, 104)
        Me.txtGSS.Name = "txtGSS"
        Me.txtGSS.Size = New System.Drawing.Size(100, 20)
        Me.txtGSS.TabIndex = 18
        '
        'chkBorough
        '
        Me.chkBorough.AutoSize = True
        Me.chkBorough.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkBorough.Location = New System.Drawing.Point(28, 221)
        Me.chkBorough.Name = "chkBorough"
        Me.chkBorough.Size = New System.Drawing.Size(121, 17)
        Me.chkBorough.TabIndex = 19
        Me.chkBorough.Text = "Has Borough Status"
        Me.chkBorough.UseVisualStyleBackColor = True
        '
        'chkRoyal
        '
        Me.chkRoyal.AutoSize = True
        Me.chkRoyal.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkRoyal.Location = New System.Drawing.Point(41, 244)
        Me.chkRoyal.Name = "chkRoyal"
        Me.chkRoyal.Size = New System.Drawing.Size(108, 17)
        Me.chkRoyal.TabIndex = 20
        Me.chkRoyal.Text = "Has Royal Status"
        Me.chkRoyal.UseVisualStyleBackColor = True
        '
        'chkCity
        '
        Me.chkCity.AutoSize = True
        Me.chkCity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkCity.Location = New System.Drawing.Point(51, 267)
        Me.chkCity.Name = "chkCity"
        Me.chkCity.Size = New System.Drawing.Size(98, 17)
        Me.chkCity.TabIndex = 21
        Me.chkCity.Text = "Has City Status"
        Me.chkCity.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 343)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Notes"
        '
        'txtNotes
        '
        Me.txtNotes.Location = New System.Drawing.Point(58, 343)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtNotes.Size = New System.Drawing.Size(368, 90)
        Me.txtNotes.TabIndex = 23
        '
        'cbGroup
        '
        Me.cbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbGroup.FormattingEnabled = True
        Me.cbGroup.Location = New System.Drawing.Point(276, 240)
        Me.cbGroup.Name = "cbGroup"
        Me.cbGroup.Size = New System.Drawing.Size(121, 21)
        Me.cbGroup.TabIndex = 24
        '
        'btnNewGroup
        '
        Me.btnNewGroup.Location = New System.Drawing.Point(351, 104)
        Me.btnNewGroup.Name = "btnNewGroup"
        Me.btnNewGroup.Size = New System.Drawing.Size(75, 23)
        Me.btnNewGroup.TabIndex = 25
        Me.btnNewGroup.Text = "New Group"
        Me.btnNewGroup.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(15, 60)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(73, 13)
        Me.Label10.TabIndex = 27
        Me.Label10.Text = "OSM Relation"
        '
        'btnSelRelation
        '
        Me.btnSelRelation.Location = New System.Drawing.Point(390, 54)
        Me.btnSelRelation.Name = "btnSelRelation"
        Me.btnSelRelation.Size = New System.Drawing.Size(36, 23)
        Me.btnSelRelation.TabIndex = 28
        Me.btnSelRelation.Text = "..."
        Me.btnSelRelation.UseVisualStyleBackColor = True
        '
        'txtRelID
        '
        Me.txtRelID.Location = New System.Drawing.Point(136, 54)
        Me.txtRelID.Name = "txtRelID"
        Me.txtRelID.Size = New System.Drawing.Size(248, 20)
        Me.txtRelID.TabIndex = 29
        '
        'btnAutoName
        '
        Me.btnAutoName.Location = New System.Drawing.Point(408, 134)
        Me.btnAutoName.Name = "btnAutoName"
        Me.btnAutoName.Size = New System.Drawing.Size(18, 23)
        Me.btnAutoName.TabIndex = 30
        Me.btnAutoName.Text = "*"
        Me.btnAutoName.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(17, 293)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(22, 13)
        Me.Label11.TabIndex = 31
        Me.Label11.Text = "Lat"
        '
        'txtLat
        '
        Me.txtLat.Location = New System.Drawing.Point(58, 290)
        Me.txtLat.Name = "txtLat"
        Me.txtLat.Size = New System.Drawing.Size(145, 20)
        Me.txtLat.TabIndex = 32
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(211, 290)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(25, 13)
        Me.Label12.TabIndex = 33
        Me.Label12.Text = "Lon"
        '
        'txtLon
        '
        Me.txtLon.Location = New System.Drawing.Point(236, 290)
        Me.txtLon.Name = "txtLon"
        Me.txtLon.Size = New System.Drawing.Size(148, 20)
        Me.txtLon.TabIndex = 34
        '
        'txtWebsite
        '
        Me.txtWebsite.Location = New System.Drawing.Point(58, 316)
        Me.txtWebsite.Name = "txtWebsite"
        Me.txtWebsite.Size = New System.Drawing.Size(343, 20)
        Me.txtWebsite.TabIndex = 35
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(12, 319)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(46, 13)
        Me.Label13.TabIndex = 36
        Me.Label13.Text = "Website"
        '
        'btnOpenWebsite
        '
        Me.btnOpenWebsite.Location = New System.Drawing.Point(408, 318)
        Me.btnOpenWebsite.Name = "btnOpenWebsite"
        Me.btnOpenWebsite.Size = New System.Drawing.Size(17, 25)
        Me.btnOpenWebsite.TabIndex = 37
        Me.btnOpenWebsite.Text = "*"
        Me.btnOpenWebsite.UseVisualStyleBackColor = True
        '
        'frmEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(433, 466)
        Me.Controls.Add(Me.btnOpenWebsite)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtWebsite)
        Me.Controls.Add(Me.txtLon)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtLat)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.btnAutoName)
        Me.Controls.Add(Me.txtRelID)
        Me.Controls.Add(Me.btnSelRelation)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.btnNewGroup)
        Me.Controls.Add(Me.cbGroup)
        Me.Controls.Add(Me.txtNotes)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.chkCity)
        Me.Controls.Add(Me.chkRoyal)
        Me.Controls.Add(Me.chkBorough)
        Me.Controls.Add(Me.txtGSS)
        Me.Controls.Add(Me.cbType)
        Me.Controls.Add(Me.cbParishType)
        Me.Controls.Add(Me.cbStyle)
        Me.Controls.Add(Me.txtCouncilName2)
        Me.Controls.Add(Me.txtCouncilName)
        Me.Controls.Add(Me.txtName2)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Name = "frmEdit"
        Me.Text = "Edit Boundary Metadata"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnOK As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents txtName As TextBox
    Friend WithEvents txtName2 As TextBox
    Friend WithEvents txtCouncilName As TextBox
    Friend WithEvents txtCouncilName2 As TextBox
    Friend WithEvents cbStyle As ComboBox
    Friend WithEvents cbParishType As ComboBox
    Friend WithEvents cbType As ComboBox
    Friend WithEvents txtGSS As TextBox
    Friend WithEvents chkBorough As CheckBox
    Friend WithEvents chkRoyal As CheckBox
    Friend WithEvents chkCity As CheckBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtNotes As TextBox
    Friend WithEvents cbGroup As ComboBox
    Friend WithEvents btnNewGroup As Button
    Friend WithEvents Label10 As Label
    Friend WithEvents btnSelRelation As Button
    Friend WithEvents txtRelID As TextBox
    Friend WithEvents btnAutoName As Button
    Friend WithEvents Label11 As Label
    Friend WithEvents txtLat As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents txtLon As TextBox
    Friend WithEvents txtWebsite As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents btnOpenWebsite As Button
End Class
