Imports OSMLibrary
Imports BoundsBeater.BoundaryDB
Imports System.Windows.Input

Public Class frmEdit
    Public xItem As BoundaryItem
    Public xDB As BoundaryDB
    Public sOriginalGSS As String
    Public GssPrefix As String
    Private iOriginalRel As Long
    Public Retriever As OSMRetriever
    Public RelChangeAllowed As Boolean = True
    Private bIgnoreKeyPress As Boolean = False
    Private _groups As New List(Of BoundaryItem)

    Private Class GenericListItem(Of T)
        Private mText As String
        Private mValue As T
        Public Sub New(ByVal pText As String, ByVal pValue As T)
            mText = pText
            mValue = pValue
        End Sub
        Public ReadOnly Property Text() As String
            Get
                Return mText
            End Get
        End Property
        Public ReadOnly Property Value() As T
            Get
                Return mValue
            End Get
        End Property
        Public Overrides Function ToString() As String
            Return mText
        End Function
    End Class
    Private Sub frmEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If xItem Is Nothing Then Return
        chkDeleted.Checked = xItem.IsDeleted
        sOriginalGSS = xItem.ONSCode
        iOriginalRel = xItem.OSMRelation
        cbGroup.Left = txtCouncilName.Left
        cbGroup.Width = txtCouncilName.Width
        cbGroup.Top = txtCouncilName.Top
        With cbParishType.Items
            .Clear()
            .Add(New GenericListItem(Of BoundaryItem.ParishTypes)("Parish Council", BoundaryItem.ParishTypes.PT_ParishCouncil))
            .Add(New GenericListItem(Of BoundaryItem.ParishTypes)("Parish Meeting", BoundaryItem.ParishTypes.PT_ParishMeeting))
            .Add(New GenericListItem(Of BoundaryItem.ParishTypes)("Joint Parish Council", BoundaryItem.ParishTypes.PT_JointParishCouncil))
            .Add(New GenericListItem(Of BoundaryItem.ParishTypes)("Joint Parish Meeting", BoundaryItem.ParishTypes.PT_JointParishMeeting))
            .Add(New GenericListItem(Of BoundaryItem.ParishTypes)("Lands Common", BoundaryItem.ParishTypes.PT_LandsCommon))
            .Add(New GenericListItem(Of BoundaryItem.ParishTypes)("Welsh Community Council", BoundaryItem.ParishTypes.PT_CommunityCouncil))
            .Add(New GenericListItem(Of BoundaryItem.ParishTypes)("N/A", BoundaryItem.ParishTypes.PT_NA))
        End With
        With cbType.Items
            .Clear()
            .Add(New GenericListItem(Of BoundaryItem.BoundaryTypes)("Country", BoundaryItem.BoundaryTypes.BT_Country))
            .Add(New GenericListItem(Of BoundaryItem.BoundaryTypes)("Nation", BoundaryItem.BoundaryTypes.BT_Nation))
            .Add(New GenericListItem(Of BoundaryItem.BoundaryTypes)("Region", BoundaryItem.BoundaryTypes.BT_Region))
            .Add(New GenericListItem(Of BoundaryItem.BoundaryTypes)("Ceremonial Country", BoundaryItem.BoundaryTypes.BT_CeremonialCounty))
            .Add(New GenericListItem(Of BoundaryItem.BoundaryTypes)("Metropolitan County", BoundaryItem.BoundaryTypes.BT_MetroCounty))
            .Add(New GenericListItem(Of BoundaryItem.BoundaryTypes)("Metropolitan District", BoundaryItem.BoundaryTypes.BT_MetroDistrict))
            .Add(New GenericListItem(Of BoundaryItem.BoundaryTypes)("Non-Metropolitan District", BoundaryItem.BoundaryTypes.BT_NonMetroDistrict))
            .Add(New GenericListItem(Of BoundaryItem.BoundaryTypes)("Non-Metropolitan Country", BoundaryItem.BoundaryTypes.BT_NonMetroCounty))
            .Add(New GenericListItem(Of BoundaryItem.BoundaryTypes)("Civil Parish", BoundaryItem.BoundaryTypes.BT_CivilParish))
            .Add(New GenericListItem(Of BoundaryItem.BoundaryTypes)("Community", BoundaryItem.BoundaryTypes.BT_Community))
            .Add(New GenericListItem(Of BoundaryItem.BoundaryTypes)("Unitary Authority", BoundaryItem.BoundaryTypes.BT_Unitary))
            .Add(New GenericListItem(Of BoundaryItem.BoundaryTypes)("Sui Generis", BoundaryItem.BoundaryTypes.BT_SuiGeneris))
            .Add(New GenericListItem(Of BoundaryItem.BoundaryTypes)("Liberty", BoundaryItem.BoundaryTypes.BT_Liberty))
            .Add(New GenericListItem(Of BoundaryItem.BoundaryTypes)("London Borough", BoundaryItem.BoundaryTypes.BT_LondonBorough))
            .Add(New GenericListItem(Of BoundaryItem.BoundaryTypes)("Preserved County", BoundaryItem.BoundaryTypes.BT_PreservedCounty))
            .Add(New GenericListItem(Of BoundaryItem.BoundaryTypes)("Prinicipal Area", BoundaryItem.BoundaryTypes.BT_PrincipalArea))
            .Add(New GenericListItem(Of BoundaryItem.BoundaryTypes)("Scottish Council", BoundaryItem.BoundaryTypes.BT_ScotCouncil))
            .Add(New GenericListItem(Of BoundaryItem.BoundaryTypes)("Northern Ireland District", BoundaryItem.BoundaryTypes.BT_NIreDistrict))
            .Add(New GenericListItem(Of BoundaryItem.BoundaryTypes)("Parish Group", BoundaryItem.BoundaryTypes.BT_ParishGroup))
        End With
        With cbStyle.Items
            .Clear()
            .Add(New GenericListItem(Of BoundaryItem.CouncilStyles)("(default)", BoundaryItem.CouncilStyles.CS_Default))
            .Add(New GenericListItem(Of BoundaryItem.CouncilStyles)("District", BoundaryItem.CouncilStyles.CS_District))
            .Add(New GenericListItem(Of BoundaryItem.CouncilStyles)("Borough", BoundaryItem.CouncilStyles.CS_Borough))
            .Add(New GenericListItem(Of BoundaryItem.CouncilStyles)("Town", BoundaryItem.CouncilStyles.CS_Town))
            .Add(New GenericListItem(Of BoundaryItem.CouncilStyles)("City", BoundaryItem.CouncilStyles.CS_City))
            .Add(New GenericListItem(Of BoundaryItem.CouncilStyles)("City and County", BoundaryItem.CouncilStyles.CS_CityAndCounty))
            .Add(New GenericListItem(Of BoundaryItem.CouncilStyles)("City and District", BoundaryItem.CouncilStyles.CS_CityAndDistrict))
            .Add(New GenericListItem(Of BoundaryItem.CouncilStyles)("County", BoundaryItem.CouncilStyles.CS_County))
            .Add(New GenericListItem(Of BoundaryItem.CouncilStyles)("County Borough", BoundaryItem.CouncilStyles.CS_CountyBorough))
            .Add(New GenericListItem(Of BoundaryItem.CouncilStyles)("Community", BoundaryItem.CouncilStyles.CS_Community))
            .Add(New GenericListItem(Of BoundaryItem.CouncilStyles)("Parish", BoundaryItem.CouncilStyles.CS_Parish))
            .Add(New GenericListItem(Of BoundaryItem.CouncilStyles)("Neighbourhood", BoundaryItem.CouncilStyles.CS_Neighbourhood))
            .Add(New GenericListItem(Of BoundaryItem.CouncilStyles)("Village", BoundaryItem.CouncilStyles.CS_Village))
        End With
        Dim bGotGroup As Boolean = False
        Dim xSelected As BoundaryItem = Nothing
        For Each x In xItem.Parent.Children
            If x.BoundaryType = BoundaryItem.BoundaryTypes.BT_ParishGroup Then
                _groups.Add(x)
                If x.CouncilName = xItem.CouncilName Then xSelected = x
            End If
        Next
        If xSelected Is Nothing _
            AndAlso xItem.BoundaryType = BoundaryItem.BoundaryTypes.BT_CivilParish _
            AndAlso (xItem.ParishType = BoundaryItem.ParishTypes.PT_JointParishCouncil OrElse xItem.ParishType = BoundaryItem.ParishTypes.PT_JointParishMeeting) Then
            Dim x As New BoundaryItem(xDB)
            x.Name = xItem.CouncilName
            x.BoundaryType = BoundaryItem.BoundaryTypes.BT_ParishGroup
            x.ONSCode = "dummy"
            xSelected = x
            _groups.Add(x)
        End If

        cbGroup.DataSource = _groups
        cbGroup.DisplayMember = "CouncilName"
        cbGroup.ValueMember = "ONSCode"
        cbGroup.SelectedItem = xSelected
        With xItem
            txtName.Text = .Name
            txtName2.Text = .Name2
            txtRelID.Text = If(.OSMRelation = 0, "", CStr(.OSMRelation))
            txtGSS.Text = .ONSCode
            txtCouncilName.Text = .CouncilName
            txtCouncilName2.Text = .CouncilName2
            For Each x In cbParishType.Items
                If CType(x, GenericListItem(Of BoundaryItem.ParishTypes)).Value = .ParishType Then
                    cbParishType.SelectedItem = x
                    Exit For
                End If
            Next
            For Each x In cbType.Items
                If CType(x, GenericListItem(Of BoundaryItem.BoundaryTypes)).Value = .BoundaryType Then
                    cbType.SelectedItem = x
                    Exit For
                End If
            Next
            For Each x In cbStyle.Items
                If CType(x, GenericListItem(Of BoundaryItem.CouncilStyles)).Value = .CouncilStyle Then
                    cbStyle.SelectedItem = x
                    Exit For
                End If
            Next
            chkBorough.Checked = .IsBorough
            chkRoyal.Checked = .IsRoyal
            chkCity.Checked = .IsCity
            txtNotes.Text = .Notes
            txtLat.Text = CStr(.Lat)
            txtLon.Text = CStr(.Lon)
            txtWebsite.Text = .Website
            Dim sGSS As String = .ONSCode
            Dim sGSSPrefix As String = .GSSPrefix
            If sGSSPrefix <> "" AndAlso sGSS <> "" Then
                If Strings.Left(sGSS, Len(sGSSPrefix)) <> sGSSPrefix Then
                    MsgBox($"Warning: GSS code {sGSS} is not appropriate for this boundary type, {sGSSPrefix} expected", MsgBoxStyle.Exclamation)
                End If
            End If
        End With
        SwitchNameFields()
    End Sub

    Private Sub SwitchNameFields()
        If cbType.SelectedItem Is Nothing Or cbParishType.SelectedItem Is Nothing Then Return
        Dim bt As BoundaryItem.BoundaryTypes = CType(cbType.SelectedItem, GenericListItem(Of BoundaryItem.BoundaryTypes)).Value
        Dim pt As BoundaryItem.ParishTypes = CType(cbParishType.SelectedItem, GenericListItem(Of BoundaryItem.ParishTypes)).Value
        If bt = BoundaryItem.BoundaryTypes.BT_CivilParish AndAlso (pt = BoundaryItem.ParishTypes.PT_JointParishCouncil OrElse pt = BoundaryItem.ParishTypes.PT_JointParishMeeting) Then
            cbGroup.Visible = True
            txtCouncilName.Visible = False
            btnNewGroup.Visible = True
        Else
            cbGroup.Visible = False
            txtCouncilName.Visible = True
            btnNewGroup.Visible = False
            If bt = BoundaryItem.BoundaryTypes.BT_ParishGroup Then
                If txtGSS.Text = "" Then txtGSS.Text = CreateGroupID()
            Else
                txtGSS.Text = sOriginalGSS
            End If
        End If
    End Sub

    Private Function CreateGroupID() As String
        Dim iMax As Integer = 0
        Dim i As Integer
        Dim sTmp As String
        If GssPrefix = "" Then Return ""
        For Each x In xDB.Items.Values
            If x.ONSCode.StartsWith(GssPrefix) Then
                sTmp = Mid(x.ONSCode, Len(GssPrefix) + 1)
                If Integer.TryParse(sTmp, i) Then
                    If i > iMax Then iMax = i
                End If
            End If
        Next
        Return $"{GssPrefix}{iMax + 1:000}"
    End Function

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Dim lTmp As Long
        Dim sGSS As String = Trim(txtGSS.Text)
        If Len(cbType.Text) = 0 Then
            MsgBox("Please select a boundary type")
            cbType.Focus()
            Me.DialogResult = DialogResult.None
            Return
        End If
        If Len(sGSS) = 0 Then
            MsgBox("GSS code is mandatory (and must be unique)")
            txtGSS.Focus()
            Me.DialogResult = DialogResult.None
            Return
        End If
        If sOriginalGSS <> sGSS Then
            If xDB.Items.ContainsKey(sGSS) Then
                MsgBox("GSS code exists already in database - must be unique")
                txtGSS.Focus()
                Me.DialogResult = DialogResult.None
                Return
            End If
        End If
        If Len(Trim(txtName.Text)) = 0 Then
            MsgBox("Name is mandatory")
            txtName.Focus()
            Me.DialogResult = DialogResult.None
            Return
        End If
        With xItem
            .IsDeleted = chkDeleted.Checked
            .Name = Trim(txtName.Text)
            .Name2 = Trim(txtName2.Text)
            If Long.TryParse(txtRelID.Text, lTmp) Then .OSMRelation = lTmp
            .ONSCode = Trim(txtGSS.Text)
            .BoundaryType = CType(cbType.SelectedItem, GenericListItem(Of BoundaryItem.BoundaryTypes)).Value
            .ParishType = CType(cbParishType.SelectedItem, GenericListItem(Of BoundaryItem.ParishTypes)).Value
            If .BoundaryType = BoundaryItem.BoundaryTypes.BT_CivilParish AndAlso
                    (.ParishType = BoundaryDB.BoundaryItem.ParishTypes.PT_JointParishCouncil OrElse .ParishType = BoundaryItem.ParishTypes.PT_JointParishMeeting) Then
                .CouncilName = Trim(cbGroup.Text)
                .CouncilName2 = ""
            Else
                .CouncilName = Trim(txtCouncilName.Text)
                .CouncilName2 = Trim(txtCouncilName2.Text)
            End If
            .CouncilStyle = CType(cbStyle.SelectedItem, GenericListItem(Of BoundaryItem.CouncilStyles)).Value
            .IsBorough = chkBorough.Checked
            .IsRoyal = chkRoyal.Checked
            .IsCity = chkCity.Checked
            Double.TryParse(txtLat.Text, .Lat)
            Double.TryParse(txtLon.Text, .Lon)
            .Website = txtWebsite.Text
            .Notes = Trim(txtNotes.Text)
        End With
    End Sub

    Private Sub cbParishType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbParishType.SelectedIndexChanged
        SwitchNameFields()
    End Sub

    Private Sub cbType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbType.SelectedIndexChanged
        SwitchNameFields()
    End Sub

    Private Sub btnNewGroup_Click(sender As Object, e As EventArgs) Handles btnNewGroup.Click
        MsgBox("add new parish group")
    End Sub

    Private Sub txtRelID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRelID.KeyPress
        If bIgnoreKeyPress OrElse Char.IsDigit(e.KeyChar) OrElse e.KeyChar = vbBack OrElse e.KeyChar = Chr(127) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtRelID_Leave(sender As Object, e As EventArgs) Handles txtRelID.Leave
        ' validate relation id
        Dim iRel As Long
        If txtRelID.Text = "" Then Return
        If Not Long.TryParse(txtRelID.Text, iRel) Then
            MsgBox("Please enter a numeric relation ID")
            txtRelID.Focus()
            Return
        End If
        ' no change from saved value
        If iRel = iOriginalRel Then
            Return
        End If
        Dim xNewRel As OSMRelation
        Dim sName As String = "?", sLevel As String = "?", sGSS As String = ""
        If iRel = 0 Then
            If MsgBox("Do you want to remove the OSM relation from this boundary?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Return
            Else
                txtRelID.Text = iOriginalRel.ToString()
                txtRelID.Focus()
                Return
            End If
        End If

        ' check for relation
        ' does not exist
        ' not correct type
        ' check intended
        If Not Retriever Is Nothing Then
            xNewRel = DirectCast(Retriever.GetOSMObject(OSMObject.ObjectType.Relation, iRel), OSMRelation)
            If xNewRel Is Nothing Then
                MsgBox($"Unable to retrieve relation {iRel}")
                txtRelID.Focus()
                Return
            End If
            If xNewRel.Tag("type") <> "boundary" Then
                MsgBox($"Relation {iRel} is not a boundary relation")
                txtRelID.Focus()
                Return
            End If
            If xNewRel.Tag("boundary") <> "administrative" AndAlso xNewRel.Tag("boundary") <> "ceremonial" Then
                MsgBox($"Boundary relation {iRel} is not administrative or ceremonial")
                txtRelID.Focus()
                Return
            End If
            sName = xNewRel.Name()
            sLevel = xNewRel.Tag("admin_level")
            sGSS = xNewRel.Tag("ref:gss")
        Else
            sName = iRel.ToString()
            sLevel = "unknown"
            sGSS = ""
        End If
        If sGSS <> "" Then
            Dim sExpectedGSS As String = xItem.GSSPrefix
            If sExpectedGSS <> "" Then
                If Strings.Left(sGSS, Len(sExpectedGSS)) <> sExpectedGSS Then
                    MsgBox($"Selected relation has inappropriate GSS code {sGSS} for this boundary type - expected {sExpectedGSS}")
                    Return
                End If
            End If
        End If
        If MsgBox($"Do you want to set the OSM relation to {sName} (level {sLevel})? ", MsgBoxStyle.YesNo, "Change OSM Relation") = MsgBoxResult.Yes Then
            iOriginalRel = iRel
        Else
            txtRelID.Text = iOriginalRel.ToString()
            txtRelID.Focus()
            Return
        End If
    End Sub

    Private Sub txtRelID_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRelID.KeyDown
        If e.Control AndAlso (e.KeyCode = Keys.V Or e.KeyCode = Keys.C Or e.KeyCode = Keys.X) Then
            bIgnoreKeyPress = True
        End If
    End Sub

    ''' <summary>
    ''' AutoName button generates a council name based on the type of council
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnAutoName_Click(sender As Object, e As EventArgs) Handles btnAutoName.Click
        Dim x = cbType.SelectedItem
        If IsNothing(x) Then Exit Sub
        Dim boundaryType As BoundaryItem.BoundaryTypes = CType(x, GenericListItem(Of BoundaryItem.BoundaryTypes)).Value
        x = cbStyle.SelectedItem
        If IsNothing(x) Then Exit Sub
        Dim councilStyle As BoundaryItem.CouncilStyles = CType(x, GenericListItem(Of BoundaryItem.CouncilStyles)).Value
        Dim parishType As BoundaryItem.ParishTypes
        If boundaryType = BoundaryItem.BoundaryTypes.BT_CivilParish Then
            x = cbParishType.SelectedItem
            If IsNothing(x) Then Exit Sub
            parishType = CType(x, GenericListItem(Of BoundaryItem.ParishTypes)).Value
        Else
            parishType = BoundaryItem.ParishTypes.PT_NA
        End If
        Dim sSuffix As String = ""
        Dim sSuffix2 As String = ""
        Select Case boundaryType
            Case BoundaryItem.BoundaryTypes.BT_CivilParish
                If parishType = BoundaryItem.ParishTypes.PT_JointParishCouncil OrElse parishType = BoundaryItem.ParishTypes.PT_JointParishMeeting Then
                    sSuffix = ""
                ElseIf chkCity.Checked Then
                    sSuffix = "City Council"
                Else
                    Select Case councilStyle
                        Case BoundaryItem.CouncilStyles.CS_Town
                            sSuffix = "Town Council"
                        Case BoundaryItem.CouncilStyles.CS_Neighbourhood
                            sSuffix = "Neighbourhood Council"
                        Case BoundaryItem.CouncilStyles.CS_Village
                            sSuffix = "Village Council"
                        Case BoundaryItem.CouncilStyles.CS_Community
                            sSuffix = "Community Council"
                        Case BoundaryItem.CouncilStyles.CS_City
                            sSuffix = "City Council"
                        Case Else
                            Select Case parishType
                                Case BoundaryItem.ParishTypes.PT_ParishCouncil
                                    sSuffix = "Parish Council"
                                Case BoundaryItem.ParishTypes.PT_ParishMeeting
                                    sSuffix = "Parish Meeting"
                            End Select
                    End Select
                End If
            Case BoundaryItem.BoundaryTypes.BT_ParishGroup
                Select Case parishType
                    Case BoundaryItem.ParishTypes.PT_ParishCouncil
                        sSuffix = "Parish Council"
                    Case BoundaryItem.ParishTypes.PT_ParishMeeting
                        sSuffix = "Parish Meeting"
                End Select
            Case BoundaryItem.BoundaryTypes.BT_NonMetroDistrict
                If chkCity.Checked Then
                    sSuffix = "City Council"
                ElseIf chkBorough.Checked Then
                    sSuffix = "Borough Council"
                Else
                    sSuffix = "District Council"
                End If
            Case BoundaryItem.BoundaryTypes.BT_MetroDistrict
                If chkCity.Checked Then
                    sSuffix = "City Council"
                Else
                    sSuffix = "Council"
                End If
            Case BoundaryItem.BoundaryTypes.BT_Unitary
                If chkCity.Checked Then
                    sSuffix = "City Council"
                ElseIf chkBorough.Checked Then
                    sSuffix = "Borough Council"
                Else
                    If councilStyle = BoundaryItem.CouncilStyles.CS_County Then
                        sSuffix = "County Council"
                    Else
                        sSuffix = "Council"
                    End If
                End If
            Case BoundaryItem.BoundaryTypes.BT_MetroCounty, BoundaryItem.BoundaryTypes.BT_NonMetroCounty
                sSuffix = "County Council"
            Case BoundaryItem.BoundaryTypes.BT_PrincipalArea
                If chkCity.Checked Then
                    sSuffix = "City Council"
                    sSuffix2 = "Cyngol Dinas"
                ElseIf chkBorough.Checked Then
                    sSuffix = "County Borough Council"
                    sSuffix2 = "Cyngor Bwrdeisdref Sirol"
                Else
                    sSuffix = "County Council"
                    If txtName2.Text.Substring(0, 4) = "Sir " Then
                        sSuffix2 = "Cyngor"
                    Else
                        sSuffix2 = "Cyngor Sir"
                    End If
                End If
            Case BoundaryItem.BoundaryTypes.BT_Community
                Select Case councilStyle
                    Case BoundaryItem.CouncilStyles.CS_Town
                        sSuffix = "Town Council"
                        sSuffix2 = "Cyngor Tref"
                    Case BoundaryItem.CouncilStyles.CS_City
                        sSuffix = "City Council"
                        sSuffix2 = "Cyngor Dinas"
                    Case Else
                        sSuffix = "Community Council"
                        sSuffix2 = "Cyngor Cymuned"
                End Select
        End Select
        If Len(sSuffix) > 0 Then
            txtCouncilName.Text = txtName.Text & " " & sSuffix
        End If
        If Len(sSuffix2) > 0 AndAlso Len(txtName2.Text) > 0 Then
            txtCouncilName2.Text = sSuffix2 & " " & txtName2.Text
        End If
    End Sub

    Private Sub txtWebsite_Leave(sender As Object, e As EventArgs) Handles txtWebsite.Leave
        Dim sTmp As String = txtWebsite.Text
        If sTmp <> "" Then
            If Strings.Left(sTmp, 7) <> "http://" AndAlso Strings.Left(sTmp, 8) <> "https://" Then
                MsgBox($"Website URL must start with http:// or https://")
                txtWebsite.Focus()
                Return
            End If
        End If
    End Sub

    Private Sub btnOpenWebsite_Click(sender As Object, e As EventArgs) Handles btnOpenWebsite.Click
        Dim sTmp As String = txtWebsite.Text
        If Len(sTmp) > 0 Then OpenBrowserAt(sTmp)
    End Sub
End Class