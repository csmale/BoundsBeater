<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmEdit
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
        Me.chkDeleted = New System.Windows.Forms.CheckBox()
        Me.lblLandsCommon = New System.Windows.Forms.Label()
        Me.lvLCPs = New System.Windows.Forms.ListView()
        Me.colGSS = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblNextElection = New System.Windows.Forms.Label()
        Me.txtElecCycle = New System.Windows.Forms.TextBox()
        Me.txtElecFraction = New System.Windows.Forms.TextBox()
        Me.txtElecMod = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(385, 680)
        Me.btnOK.Margin = New System.Windows.Forms.Padding(4)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(100, 28)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(251, 680)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 28)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 44)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 17)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 192)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 17)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Council Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(19, 165)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 17)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "GSS code"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(19, 249)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(89, 17)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Council Style"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(19, 277)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(84, 17)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Parish Type"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(19, 70)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(98, 17)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Name (Welsh)"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(19, 139)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(105, 17)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Boundary Type"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(19, 219)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(148, 17)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Council Name (Welsh)"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(181, 36)
        Me.txtName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(530, 22)
        Me.txtName.TabIndex = 11
        '
        'txtName2
        '
        Me.txtName2.Location = New System.Drawing.Point(181, 62)
        Me.txtName2.Margin = New System.Windows.Forms.Padding(4)
        Me.txtName2.Name = "txtName2"
        Me.txtName2.Size = New System.Drawing.Size(530, 22)
        Me.txtName2.TabIndex = 12
        '
        'txtCouncilName
        '
        Me.txtCouncilName.Location = New System.Drawing.Point(181, 183)
        Me.txtCouncilName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCouncilName.Name = "txtCouncilName"
        Me.txtCouncilName.Size = New System.Drawing.Size(493, 22)
        Me.txtCouncilName.TabIndex = 13
        '
        'txtCouncilName2
        '
        Me.txtCouncilName2.Location = New System.Drawing.Point(181, 210)
        Me.txtCouncilName2.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCouncilName2.Name = "txtCouncilName2"
        Me.txtCouncilName2.Size = New System.Drawing.Size(493, 22)
        Me.txtCouncilName2.TabIndex = 14
        '
        'cbStyle
        '
        Me.cbStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbStyle.FormattingEnabled = True
        Me.cbStyle.Location = New System.Drawing.Point(181, 239)
        Me.cbStyle.Margin = New System.Windows.Forms.Padding(4)
        Me.cbStyle.Name = "cbStyle"
        Me.cbStyle.Size = New System.Drawing.Size(132, 24)
        Me.cbStyle.TabIndex = 15
        '
        'cbParishType
        '
        Me.cbParishType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbParishType.FormattingEnabled = True
        Me.cbParishType.Location = New System.Drawing.Point(181, 267)
        Me.cbParishType.Margin = New System.Windows.Forms.Padding(4)
        Me.cbParishType.Name = "cbParishType"
        Me.cbParishType.Size = New System.Drawing.Size(160, 24)
        Me.cbParishType.TabIndex = 16
        '
        'cbType
        '
        Me.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbType.FormattingEnabled = True
        Me.cbType.Location = New System.Drawing.Point(181, 129)
        Me.cbType.Margin = New System.Windows.Forms.Padding(4)
        Me.cbType.Name = "cbType"
        Me.cbType.Size = New System.Drawing.Size(525, 24)
        Me.cbType.TabIndex = 17
        '
        'txtGSS
        '
        Me.txtGSS.Location = New System.Drawing.Point(181, 156)
        Me.txtGSS.Margin = New System.Windows.Forms.Padding(4)
        Me.txtGSS.Name = "txtGSS"
        Me.txtGSS.Size = New System.Drawing.Size(132, 22)
        Me.txtGSS.TabIndex = 18
        '
        'chkBorough
        '
        Me.chkBorough.AutoSize = True
        Me.chkBorough.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkBorough.Location = New System.Drawing.Point(37, 300)
        Me.chkBorough.Margin = New System.Windows.Forms.Padding(4)
        Me.chkBorough.Name = "chkBorough"
        Me.chkBorough.Size = New System.Drawing.Size(157, 21)
        Me.chkBorough.TabIndex = 19
        Me.chkBorough.Text = "Has Borough Status"
        Me.chkBorough.UseVisualStyleBackColor = True
        '
        'chkRoyal
        '
        Me.chkRoyal.AutoSize = True
        Me.chkRoyal.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkRoyal.Location = New System.Drawing.Point(55, 329)
        Me.chkRoyal.Margin = New System.Windows.Forms.Padding(4)
        Me.chkRoyal.Name = "chkRoyal"
        Me.chkRoyal.Size = New System.Drawing.Size(139, 21)
        Me.chkRoyal.TabIndex = 20
        Me.chkRoyal.Text = "Has Royal Status"
        Me.chkRoyal.UseVisualStyleBackColor = True
        '
        'chkCity
        '
        Me.chkCity.AutoSize = True
        Me.chkCity.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkCity.Location = New System.Drawing.Point(68, 357)
        Me.chkCity.Margin = New System.Windows.Forms.Padding(4)
        Me.chkCity.Name = "chkCity"
        Me.chkCity.Size = New System.Drawing.Size(126, 21)
        Me.chkCity.TabIndex = 21
        Me.chkCity.Text = "Has City Status"
        Me.chkCity.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(23, 561)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 17)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Notes"
        '
        'txtNotes
        '
        Me.txtNotes.Location = New System.Drawing.Point(77, 561)
        Me.txtNotes.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtNotes.Size = New System.Drawing.Size(634, 110)
        Me.txtNotes.TabIndex = 23
        '
        'cbGroup
        '
        Me.cbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbGroup.FormattingEnabled = True
        Me.cbGroup.Location = New System.Drawing.Point(-3, 4)
        Me.cbGroup.Margin = New System.Windows.Forms.Padding(4)
        Me.cbGroup.Name = "cbGroup"
        Me.cbGroup.Size = New System.Drawing.Size(160, 24)
        Me.cbGroup.TabIndex = 24
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(20, 102)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(95, 17)
        Me.Label10.TabIndex = 27
        Me.Label10.Text = "OSM Relation"
        '
        'btnSelRelation
        '
        Me.btnSelRelation.Location = New System.Drawing.Point(658, 95)
        Me.btnSelRelation.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSelRelation.Name = "btnSelRelation"
        Me.btnSelRelation.Size = New System.Drawing.Size(48, 28)
        Me.btnSelRelation.TabIndex = 28
        Me.btnSelRelation.Text = "..."
        Me.btnSelRelation.UseVisualStyleBackColor = True
        '
        'txtRelID
        '
        Me.txtRelID.Location = New System.Drawing.Point(181, 95)
        Me.txtRelID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtRelID.Name = "txtRelID"
        Me.txtRelID.Size = New System.Drawing.Size(469, 22)
        Me.txtRelID.TabIndex = 29
        '
        'btnAutoName
        '
        Me.btnAutoName.Location = New System.Drawing.Point(682, 204)
        Me.btnAutoName.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAutoName.Name = "btnAutoName"
        Me.btnAutoName.Size = New System.Drawing.Size(24, 28)
        Me.btnAutoName.TabIndex = 30
        Me.btnAutoName.Text = "*"
        Me.btnAutoName.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(23, 389)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(28, 17)
        Me.Label11.TabIndex = 31
        Me.Label11.Text = "Lat"
        '
        'txtLat
        '
        Me.txtLat.Location = New System.Drawing.Point(77, 385)
        Me.txtLat.Margin = New System.Windows.Forms.Padding(4)
        Me.txtLat.Name = "txtLat"
        Me.txtLat.Size = New System.Drawing.Size(192, 22)
        Me.txtLat.TabIndex = 32
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(281, 385)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(32, 17)
        Me.Label12.TabIndex = 33
        Me.Label12.Text = "Lon"
        '
        'txtLon
        '
        Me.txtLon.Location = New System.Drawing.Point(315, 385)
        Me.txtLon.Margin = New System.Windows.Forms.Padding(4)
        Me.txtLon.Name = "txtLon"
        Me.txtLon.Size = New System.Drawing.Size(196, 22)
        Me.txtLon.TabIndex = 34
        '
        'txtWebsite
        '
        Me.txtWebsite.Location = New System.Drawing.Point(77, 528)
        Me.txtWebsite.Margin = New System.Windows.Forms.Padding(4)
        Me.txtWebsite.Name = "txtWebsite"
        Me.txtWebsite.Size = New System.Drawing.Size(603, 22)
        Me.txtWebsite.TabIndex = 35
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(16, 532)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(59, 17)
        Me.Label13.TabIndex = 36
        Me.Label13.Text = "Website"
        '
        'btnOpenWebsite
        '
        Me.btnOpenWebsite.Location = New System.Drawing.Point(688, 528)
        Me.btnOpenWebsite.Margin = New System.Windows.Forms.Padding(4)
        Me.btnOpenWebsite.Name = "btnOpenWebsite"
        Me.btnOpenWebsite.Size = New System.Drawing.Size(23, 23)
        Me.btnOpenWebsite.TabIndex = 37
        Me.btnOpenWebsite.Text = "*"
        Me.btnOpenWebsite.UseVisualStyleBackColor = True
        '
        'chkDeleted
        '
        Me.chkDeleted.AutoSize = True
        Me.chkDeleted.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDeleted.ForeColor = System.Drawing.Color.Red
        Me.chkDeleted.Location = New System.Drawing.Point(235, 7)
        Me.chkDeleted.Margin = New System.Windows.Forms.Padding(4)
        Me.chkDeleted.Name = "chkDeleted"
        Me.chkDeleted.Size = New System.Drawing.Size(95, 21)
        Me.chkDeleted.TabIndex = 38
        Me.chkDeleted.Text = "Deleted?"
        Me.chkDeleted.UseVisualStyleBackColor = True
        '
        'lblLandsCommon
        '
        Me.lblLandsCommon.AutoSize = True
        Me.lblLandsCommon.Location = New System.Drawing.Point(23, 418)
        Me.lblLandsCommon.Name = "lblLandsCommon"
        Me.lblLandsCommon.Size = New System.Drawing.Size(41, 17)
        Me.lblLandsCommon.TabIndex = 39
        Me.lblLandsCommon.Text = "LCPs"
        '
        'lvLCPs
        '
        Me.lvLCPs.CheckBoxes = True
        Me.lvLCPs.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colGSS, Me.colType, Me.colName})
        Me.lvLCPs.GridLines = True
        Me.lvLCPs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvLCPs.Location = New System.Drawing.Point(76, 414)
        Me.lvLCPs.MultiSelect = False
        Me.lvLCPs.Name = "lvLCPs"
        Me.lvLCPs.Size = New System.Drawing.Size(636, 108)
        Me.lvLCPs.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvLCPs.TabIndex = 40
        Me.lvLCPs.UseCompatibleStateImageBehavior = False
        Me.lvLCPs.View = System.Windows.Forms.View.Details
        '
        'colGSS
        '
        Me.colGSS.Text = "GSS"
        Me.colGSS.Width = 88
        '
        'colType
        '
        Me.colType.Text = "Type"
        Me.colType.Width = 48
        '
        'colName
        '
        Me.colName.Text = "Name"
        Me.colName.Width = 400
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(451, 245)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(145, 17)
        Me.Label14.TabIndex = 41
        Me.Label14.Text = "Election Cycle (years)"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(451, 267)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(113, 17)
        Me.Label15.TabIndex = 42
        Me.Label15.Text = "Election Fraction"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(451, 288)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(123, 17)
        Me.Label16.TabIndex = 43
        Me.Label16.Text = "Election Year Mod"
        '
        'lblNextElection
        '
        Me.lblNextElection.AutoSize = True
        Me.lblNextElection.Location = New System.Drawing.Point(451, 307)
        Me.lblNextElection.Name = "lblNextElection"
        Me.lblNextElection.Size = New System.Drawing.Size(109, 17)
        Me.lblNextElection.TabIndex = 44
        Me.lblNextElection.Text = "Next Election in:"
        '
        'txtElecCycle
        '
        Me.txtElecCycle.Location = New System.Drawing.Point(603, 240)
        Me.txtElecCycle.MaxLength = 1
        Me.txtElecCycle.Name = "txtElecCycle"
        Me.txtElecCycle.Size = New System.Drawing.Size(58, 22)
        Me.txtElecCycle.TabIndex = 45
        '
        'txtElecFraction
        '
        Me.txtElecFraction.Location = New System.Drawing.Point(603, 262)
        Me.txtElecFraction.MaxLength = 1
        Me.txtElecFraction.Name = "txtElecFraction"
        Me.txtElecFraction.Size = New System.Drawing.Size(58, 22)
        Me.txtElecFraction.TabIndex = 46
        '
        'txtElecMod
        '
        Me.txtElecMod.Location = New System.Drawing.Point(603, 284)
        Me.txtElecMod.MaxLength = 1
        Me.txtElecMod.Name = "txtElecMod"
        Me.txtElecMod.Size = New System.Drawing.Size(58, 22)
        Me.txtElecMod.TabIndex = 47
        '
        'frmEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(724, 720)
        Me.Controls.Add(Me.txtElecMod)
        Me.Controls.Add(Me.txtElecFraction)
        Me.Controls.Add(Me.txtElecCycle)
        Me.Controls.Add(Me.lblNextElection)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.lvLCPs)
        Me.Controls.Add(Me.lblLandsCommon)
        Me.Controls.Add(Me.chkDeleted)
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
        Me.Margin = New System.Windows.Forms.Padding(4)
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
    Friend WithEvents chkDeleted As CheckBox
    Friend WithEvents lblLandsCommon As Label
    Friend WithEvents lvLCPs As ListView
    Friend WithEvents colGSS As ColumnHeader
    Friend WithEvents colName As ColumnHeader
    Friend WithEvents colType As ColumnHeader
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents lblNextElection As Label
    Friend WithEvents txtElecCycle As TextBox
    Friend WithEvents txtElecFraction As TextBox
    Friend WithEvents txtElecMod As TextBox
End Class
