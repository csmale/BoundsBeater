Imports OSMLibrary
Imports BoundsBeater.BoundaryDB
Imports BoundsBeater.BoundaryDB.BoundaryItem
Imports System.Windows.Input

Public Class frmEdit
    Public xItem As BoundaryItem
    Public xDB As BoundaryDB
    Public sOriginalGSS As String = String.Empty
    Public GssPrefix As String = String.Empty
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
            .Add(New GenericListItem(Of ParishTypes)("Parish Council", ParishTypes.PT_ParishCouncil))
            .Add(New GenericListItem(Of ParishTypes)("Parish Meeting", ParishTypes.PT_ParishMeeting))
            .Add(New GenericListItem(Of ParishTypes)("Joint Parish Council", ParishTypes.PT_JointParishCouncil))
            .Add(New GenericListItem(Of ParishTypes)("Joint Parish Meeting", ParishTypes.PT_JointParishMeeting))
            .Add(New GenericListItem(Of ParishTypes)("Lands Common", ParishTypes.PT_LandsCommon))
            .Add(New GenericListItem(Of ParishTypes)("Detached Area", ParishTypes.PT_DetachedArea))
            .Add(New GenericListItem(Of ParishTypes)("Welsh Community Council", ParishTypes.PT_CommunityCouncil))
            .Add(New GenericListItem(Of ParishTypes)("Welsh Joint Community Council", ParishTypes.PT_JointCommunityCouncil))
            .Add(New GenericListItem(Of ParishTypes)("N/A", ParishTypes.PT_NA))
        End With
        With cbType.Items
            .Clear()
            .Add(New GenericListItem(Of BoundaryTypes)("Country", BoundaryTypes.BT_Country))
            .Add(New GenericListItem(Of BoundaryTypes)("Nation", BoundaryTypes.BT_Nation))
            .Add(New GenericListItem(Of BoundaryTypes)("Region", BoundaryTypes.BT_Region))
            .Add(New GenericListItem(Of BoundaryTypes)("Ceremonial Country", BoundaryTypes.BT_CeremonialCounty))
            .Add(New GenericListItem(Of BoundaryTypes)("Metropolitan County", BoundaryTypes.BT_MetroCounty))
            .Add(New GenericListItem(Of BoundaryTypes)("Metropolitan District", BoundaryTypes.BT_MetroDistrict))
            .Add(New GenericListItem(Of BoundaryTypes)("Non-Metropolitan District", BoundaryTypes.BT_NonMetroDistrict))
            .Add(New GenericListItem(Of BoundaryTypes)("Non-Metropolitan Country", BoundaryTypes.BT_NonMetroCounty))
            .Add(New GenericListItem(Of BoundaryTypes)("Civil Parish", BoundaryTypes.BT_CivilParish))
            .Add(New GenericListItem(Of BoundaryTypes)("Community", BoundaryTypes.BT_Community))
            .Add(New GenericListItem(Of BoundaryTypes)("Unitary Authority", BoundaryTypes.BT_Unitary))
            .Add(New GenericListItem(Of BoundaryTypes)("Sui Generis", BoundaryTypes.BT_SuiGeneris))
            .Add(New GenericListItem(Of BoundaryTypes)("Liberty", BoundaryTypes.BT_Liberty))
            .Add(New GenericListItem(Of BoundaryTypes)("London Borough", BoundaryTypes.BT_LondonBorough))
            .Add(New GenericListItem(Of BoundaryTypes)("Preserved County", BoundaryTypes.BT_PreservedCounty))
            .Add(New GenericListItem(Of BoundaryTypes)("Prinicipal Area", BoundaryTypes.BT_PrincipalArea))
            .Add(New GenericListItem(Of BoundaryTypes)("Scottish Council", BoundaryTypes.BT_ScotCouncil))
            .Add(New GenericListItem(Of BoundaryTypes)("Northern Ireland District", BoundaryTypes.BT_NIreDistrict))
            .Add(New GenericListItem(Of BoundaryTypes)("Parish Group", BoundaryTypes.BT_ParishGroup))
            .Add(New GenericListItem(Of BoundaryTypes)("Historical County", BoundaryTypes.BT_HistoricCounty))
            .Add(New GenericListItem(Of BoundaryTypes)("Vice County", BoundaryTypes.BT_ViceCounty))
        End With
        With cbStyle.Items
            .Clear()
            .Add(New GenericListItem(Of CouncilStyles)("(default)", CouncilStyles.CS_Default))
            .Add(New GenericListItem(Of CouncilStyles)("District", CouncilStyles.CS_District))
            .Add(New GenericListItem(Of CouncilStyles)("Borough", CouncilStyles.CS_Borough))
            .Add(New GenericListItem(Of CouncilStyles)("Town", CouncilStyles.CS_Town))
            .Add(New GenericListItem(Of CouncilStyles)("City", CouncilStyles.CS_City))
            .Add(New GenericListItem(Of CouncilStyles)("City and County", CouncilStyles.CS_CityAndCounty))
            .Add(New GenericListItem(Of CouncilStyles)("City and District", CouncilStyles.CS_CityAndDistrict))
            .Add(New GenericListItem(Of CouncilStyles)("County", CouncilStyles.CS_County))
            .Add(New GenericListItem(Of CouncilStyles)("County Borough", CouncilStyles.CS_CountyBorough))
            .Add(New GenericListItem(Of CouncilStyles)("Community", CouncilStyles.CS_Community))
            .Add(New GenericListItem(Of CouncilStyles)("Parish", CouncilStyles.CS_Parish))
            .Add(New GenericListItem(Of CouncilStyles)("Neighbourhood", CouncilStyles.CS_Neighbourhood))
            .Add(New GenericListItem(Of CouncilStyles)("Village", CouncilStyles.CS_Village))
        End With
        Dim bGotGroup As Boolean = False
        Dim xSelected As BoundaryItem = Nothing
        For Each x In xItem.Parent.Children
            If x.BoundaryType = BoundaryTypes.BT_ParishGroup Then
                _groups.Add(x)
                If x.CouncilName = xItem.CouncilName Then xSelected = x
            End If
        Next
        If xSelected Is Nothing _
            AndAlso (xItem.BoundaryType = BoundaryTypes.BT_CivilParish OrElse xItem.BoundaryType = BoundaryTypes.BT_Community) _
            AndAlso (xItem.ParishType = ParishTypes.PT_JointParishCouncil OrElse xItem.ParishType = ParishTypes.PT_JointParishMeeting OrElse xItem.ParishType = ParishTypes.PT_JointCommunityCouncil) Then
            Dim x As New BoundaryItem(xDB)
            x.Name = xItem.CouncilName
            x.BoundaryType = BoundaryTypes.BT_ParishGroup
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
                If CType(x, GenericListItem(Of ParishTypes)).Value = .ParishType Then
                    cbParishType.SelectedItem = x
                    Exit For
                End If
            Next
            For Each x In cbType.Items
                If CType(x, GenericListItem(Of BoundaryTypes)).Value = .BoundaryType Then
                    cbType.SelectedItem = x
                    Exit For
                End If
            Next
            For Each x In cbStyle.Items
                If CType(x, GenericListItem(Of CouncilStyles)).Value = .CouncilStyle Then
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
            If .HasElections Then
                txtElecCycle.Enabled = True
                txtElecCycle.Text = .ElectionCycle.ToString
                txtElecFraction.Enabled = True
                txtElecFraction.Text = .ElectionFraction.ToString
                txtElecMod.Enabled = True
                txtElecMod.Text = .ElectionCycleStartMod.ToString
                lblNextElection.Text = $"Next election cycle starts in { .NextElectionCycleYear.ToString}"
            Else
                txtElecCycle.Text = ""
                txtElecCycle.Enabled = False
                txtElecFraction.Text = ""
                txtElecFraction.Enabled = False
                txtElecMod.Text = ""
                txtElecMod.Enabled = False
                lblNextElection.Text = ""
            End If
            txtWebsite.Text = .Website
            Dim sGSS As String = .ONSCode
            Dim sGSSPrefix As String = .GSSPrefix
            If sGSSPrefix <> "" AndAlso sGSS <> "" Then
                If Strings.Left(sGSS, Len(sGSSPrefix)) <> sGSSPrefix Then
                    MsgBox($"Warning: GSS code {sGSS} is not appropriate for this boundary type, {sGSSPrefix} expected", MsgBoxStyle.Exclamation)
                End If
            End If
            lvLCPs.Items.Clear()
            If .BoundaryType = BoundaryTypes.BT_CivilParish Then
                lvLCPs.Enabled = True
                If .ParishType = ParishTypes.PT_LandsCommon Then
                    lvLCPs.CheckBoxes = False
                    For Each i In xItem.Parent.Children
                        If i.BoundaryType = BoundaryTypes.BT_CivilParish Then
                            If i.LandsCommon.Contains(xItem) Then
                                With lvLCPs.Items.Add(i.ONSCode)
                                    .SubItems.Add("PAR")
                                    .SubItems.Add(i.Name)
                                    .Tag = i
                                End With
                            End If
                        End If
                    Next
                    lvLCPs.Enabled = False
                ElseIf .ParishType = ParishTypes.PT_DetachedArea Then
                    lvLCPs.CheckBoxes = False
                    For Each i In xItem.Parent.Children
                        If i.BoundaryType = BoundaryTypes.BT_CivilParish Then
                            If i.DetachedAreas.Contains(xItem) Then
                                With lvLCPs.Items.Add(i.ONSCode)
                                    .SubItems.Add("PAR")
                                    .SubItems.Add(i.Name)
                                    .Tag = i
                                End With
                            End If
                        End If
                    Next
                    lvLCPs.Enabled = False
                Else
                    lvLCPs.CheckBoxes = True
                    For Each i In xItem.Parent.Children
                        If i.BoundaryType = BoundaryTypes.BT_CivilParish _
                        AndAlso (i.ParishType = ParishTypes.PT_LandsCommon _
                            OrElse i.ParishType = ParishTypes.PT_DetachedArea) Then
                            If i.IsDeleted Then Continue For
                            With lvLCPs.Items.Add(i.ONSCode)
                                .SubItems.Add(If(i.ParishType = ParishTypes.PT_LandsCommon, "LCP", "DET"))
                                .SubItems.Add(i.Name)
                                .Tag = i
                                .Checked = xItem.LandsCommon.Contains(i) OrElse xItem.DetachedAreas.Contains(i)
                            End With
                        End If
                    Next
                End If
            Else
                lvLCPs.Enabled = False
            End If
        End With
        SwitchNameFields()
    End Sub

    Private Sub SwitchNameFields()
        If cbType.SelectedItem Is Nothing Or cbParishType.SelectedItem Is Nothing Then Return
        Dim bt As BoundaryTypes = CType(cbType.SelectedItem, GenericListItem(Of BoundaryTypes)).Value
        Dim pt As ParishTypes = CType(cbParishType.SelectedItem, GenericListItem(Of ParishTypes)).Value
        If bt = BoundaryTypes.BT_CivilParish _
            AndAlso (pt = ParishTypes.PT_JointParishCouncil OrElse pt = ParishTypes.PT_JointParishMeeting) Then
            cbGroup.Visible = True
            txtCouncilName2.Enabled = False
            txtCouncilName.Visible = False
        ElseIf bt = BoundaryTypes.BT_Community _
            AndAlso (pt = ParishTypes.PT_JointCommunityCouncil) Then
            cbGroup.Visible = True
            txtCouncilName2.Enabled = False
            txtCouncilName.Visible = False
        Else
            cbGroup.Visible = False
            txtCouncilName2.Enabled = True
            txtCouncilName.Visible = True
            If bt = BoundaryTypes.BT_ParishGroup Then
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
            .BoundaryType = CType(cbType.SelectedItem, GenericListItem(Of BoundaryTypes)).Value
            .ParishType = CType(cbParishType.SelectedItem, GenericListItem(Of ParishTypes)).Value
            If .BoundaryType = BoundaryTypes.BT_CivilParish AndAlso
                    (.ParishType = ParishTypes.PT_JointParishCouncil OrElse .ParishType = ParishTypes.PT_JointParishMeeting) Then
                .CouncilName = Trim(cbGroup.Text)
                .CouncilName2 = ""
            ElseIf .BoundaryType = BoundaryTypes.BT_Community AndAlso
                    (.ParishType = ParishTypes.PT_JointCommunityCouncil) Then
                ' problem here: doesnt handle welsh joint council names
                .CouncilName = Trim(cbGroup.Text)
                .CouncilName2 = Trim(txtCouncilName2.Text)
            Else
                .CouncilName = Trim(txtCouncilName.Text)
                .CouncilName2 = Trim(txtCouncilName2.Text)
            End If
            .CouncilStyle = CType(cbStyle.SelectedItem, GenericListItem(Of CouncilStyles)).Value
            .IsBorough = chkBorough.Checked
            .IsRoyal = chkRoyal.Checked
            .IsCity = chkCity.Checked
            Double.TryParse(txtLat.Text, .Lat)
            Double.TryParse(txtLon.Text, .Lon)
            Integer.TryParse(txtElecCycle.Text, .ElectionCycle)
            Integer.TryParse(txtElecFraction.Text, .ElectionFraction)
            Integer.TryParse(txtElecMod.Text, .ElectionCycleStartMod)
            .Website = txtWebsite.Text
            .Notes = Trim(txtNotes.Text)
            If lvLCPs.Enabled AndAlso .ParishType <> ParishTypes.PT_LandsCommon AndAlso .ParishType <> ParishTypes.PT_DetachedArea Then
                Dim a As New List(Of String)
                For Each x As ListViewItem In lvLCPs.Items
                    If x.Checked Then a.Add(x.Text)
                Next
                If .ParishType = ParishTypes.PT_LandsCommon Then
                    .SetLandsCommon(a)
                Else
                    .SetDetachedAreas(a)
                End If
            End If
            If sGSS <> sOriginalGSS Then
                xDB.Items.Add(sGSS, xItem)
                If Not String.IsNullOrEmpty(sOriginalGSS) Then xDB.Items.Remove(sOriginalGSS)
            End If
        End With
    End Sub

    Private Sub cbParishType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbParishType.SelectedIndexChanged
        SwitchNameFields()
    End Sub

    Private Sub cbType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbType.SelectedIndexChanged
        SwitchNameFields()
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
            If xNewRel.Tag("boundary") <> "administrative" AndAlso xNewRel.Tag("boundary") <> "ceremonial" AndAlso xNewRel.Tag("boundary") <> "vice_county" Then
                MsgBox($"Boundary relation {iRel} is not administrative, ceremonial or vice_county")
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
        Dim boundaryType As BoundaryTypes = CType(x, GenericListItem(Of BoundaryTypes)).Value
        x = cbStyle.SelectedItem
        If IsNothing(x) Then Exit Sub
        Dim councilStyle As CouncilStyles = CType(x, GenericListItem(Of CouncilStyles)).Value
        Dim parishType As ParishTypes
        If boundaryType = BoundaryTypes.BT_CivilParish Or boundaryType = BoundaryTypes.BT_ParishGroup Then
            x = cbParishType.SelectedItem
            If IsNothing(x) Then Exit Sub
            parishType = CType(x, GenericListItem(Of ParishTypes)).Value
        Else
            parishType = BoundaryItem.ParishTypes.PT_NA
        End If
        Dim sSuffix As String = ""
        Dim sSuffix2 As String = ""
        Select Case boundaryType
            Case BoundaryTypes.BT_CivilParish
                If parishType = ParishTypes.PT_JointParishCouncil OrElse parishType = ParishTypes.PT_JointParishMeeting Then
                    sSuffix = ""
                ElseIf chkCity.Checked Then
                    sSuffix = "City Council"
                Else
                    Select Case councilStyle
                        Case CouncilStyles.CS_Town
                            sSuffix = "Town Council"
                        Case CouncilStyles.CS_Neighbourhood
                            sSuffix = "Neighbourhood Council"
                        Case CouncilStyles.CS_Village
                            sSuffix = "Village Council"
                        Case CouncilStyles.CS_Community
                            sSuffix = "Community Council"
                        Case CouncilStyles.CS_City
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

    Private Sub cbGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbGroup.SelectedIndexChanged
        If xItem.BoundaryType <> BoundaryTypes.BT_Community Then Return
        Dim ons As String
        Dim x As BoundaryItem
        If TypeOf cbGroup.SelectedValue Is String Then
            ons = CType(cbGroup.SelectedValue, String)
            x = xDB.Items(ons)
        ElseIf TypeOf cbGroup.Selectedvalue Is BoundaryItem Then
            x = CType(cbGroup.SelectedValue, BoundaryItem)
            ' ons = x.ONSCode
        Else
            Return
        End If

        If x Is Nothing Then
            txtCouncilName2.Text = ""
        Else
            txtCouncilName2.Text = x.CouncilName2
        End If
    End Sub
End Class