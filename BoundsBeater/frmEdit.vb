Public Class frmEdit
    Public xItem As BoundaryDB.BoundaryItem
    Public xDB As BoundaryDB
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
        With cbParishType.Items
            .Clear()
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.ParishTypes)("Parish Council", BoundaryDB.BoundaryItem.ParishTypes.PT_ParishCouncil))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.ParishTypes)("Joint Parish Council", BoundaryDB.BoundaryItem.ParishTypes.PT_JointParishCouncil))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.ParishTypes)("Parish Meeting", BoundaryDB.BoundaryItem.ParishTypes.PT_ParishMeeting))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.ParishTypes)("N/A", BoundaryDB.BoundaryItem.ParishTypes.PT_NA))
        End With
        With cbType.Items
            .Clear()
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.BoundaryTypes)("Country", BoundaryDB.BoundaryItem.BoundaryTypes.BT_Country))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.BoundaryTypes)("Nation", BoundaryDB.BoundaryItem.BoundaryTypes.BT_Nation))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.BoundaryTypes)("Region", BoundaryDB.BoundaryItem.BoundaryTypes.BT_Region))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.BoundaryTypes)("Ceremonial Country", BoundaryDB.BoundaryItem.BoundaryTypes.BT_CeremonialCounty))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.BoundaryTypes)("Metropolitan County", BoundaryDB.BoundaryItem.BoundaryTypes.BT_MetroCounty))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.BoundaryTypes)("Metropolitan District", BoundaryDB.BoundaryItem.BoundaryTypes.BT_MetroDistrict))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.BoundaryTypes)("Non-Metropolitan District", BoundaryDB.BoundaryItem.BoundaryTypes.BT_NonMetroDistrict))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.BoundaryTypes)("Non-Metropolitan Country", BoundaryDB.BoundaryItem.BoundaryTypes.BT_NonMetroCounty))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.BoundaryTypes)("Civil Parish", BoundaryDB.BoundaryItem.BoundaryTypes.BT_CivilParish))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.BoundaryTypes)("Community", BoundaryDB.BoundaryItem.BoundaryTypes.BT_Community))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.BoundaryTypes)("Unitary Authority", BoundaryDB.BoundaryItem.BoundaryTypes.BT_Unitary))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.BoundaryTypes)("Sui Generis", BoundaryDB.BoundaryItem.BoundaryTypes.BT_SuiGeneris))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.BoundaryTypes)("Liberty", BoundaryDB.BoundaryItem.BoundaryTypes.BT_Liberty))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.BoundaryTypes)("London Borough", BoundaryDB.BoundaryItem.BoundaryTypes.BT_LondonBorough))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.BoundaryTypes)("Preserved County", BoundaryDB.BoundaryItem.BoundaryTypes.BT_PreservedCounty))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.BoundaryTypes)("Prinicipal Area", BoundaryDB.BoundaryItem.BoundaryTypes.BT_PrincipalArea))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.BoundaryTypes)("Scottish Council", BoundaryDB.BoundaryItem.BoundaryTypes.BT_ScotCouncil))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.BoundaryTypes)("Northern Ireland District", BoundaryDB.BoundaryItem.BoundaryTypes.BT_NIreDistrict))
        End With
        With cbStyle.Items
            .Clear()
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.CouncilStyles)("(default)", BoundaryDB.BoundaryItem.CouncilStyles.CS_Default))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.CouncilStyles)("Borough", BoundaryDB.BoundaryItem.CouncilStyles.CS_Borough))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.CouncilStyles)("Town", BoundaryDB.BoundaryItem.CouncilStyles.CS_Town))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.CouncilStyles)("City", BoundaryDB.BoundaryItem.CouncilStyles.CS_City))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.CouncilStyles)("City and County", BoundaryDB.BoundaryItem.CouncilStyles.CS_CityAndCounty))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.CouncilStyles)("City and District", BoundaryDB.BoundaryItem.CouncilStyles.CS_CityAndDistrict))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.CouncilStyles)("County", BoundaryDB.BoundaryItem.CouncilStyles.CS_County))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.CouncilStyles)("Community", BoundaryDB.BoundaryItem.CouncilStyles.CS_Community))
            .Add(New GenericListItem(Of BoundaryDB.BoundaryItem.CouncilStyles)("Village", BoundaryDB.BoundaryItem.CouncilStyles.CS_Village))
        End With
        With xItem
            txtName.Text = .Name
            txtName2.Text = .Name2
            txtGSS.Text = .ONSCode
            txtCouncilName.Text = .CouncilName
            txtCouncilName2.Text = .CouncilName2
            For Each x In cbParishType.Items
                If CType(x, GenericListItem(Of BoundaryDB.BoundaryItem.ParishTypes)).Value = .ParishType Then
                    cbParishType.SelectedItem = x
                    Exit For
                End If
            Next
            For Each x In cbType.Items
                If CType(x, GenericListItem(Of BoundaryDB.BoundaryItem.BoundaryTypes)).Value = .BoundaryType Then
                    cbType.SelectedItem = x
                    Exit For
                End If
            Next
            For Each x In cbStyle.Items
                If CType(x, GenericListItem(Of BoundaryDB.BoundaryItem.CouncilStyles)).Value = .CouncilStyle Then
                    cbStyle.SelectedItem = x
                    Exit For
                End If
            Next
            chkBorough.Checked = .IsBorough
            chkRoyal.Checked = .IsRoyal
            chkCity.Checked = .IsCity
            txtNotes.Text = .Notes
        End With
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        With xItem
            .Name = Trim(txtName.Text)
            .Name2 = Trim(txtName2.Text)
            .ONSCode = Trim(txtGSS.Text)
            .CouncilName = Trim(txtCouncilName.Text)
            .CouncilName2 = Trim(txtCouncilName2.Text)
            .BoundaryType = CType(cbType.SelectedItem, GenericListItem(Of BoundaryDB.BoundaryItem.BoundaryTypes)).Value
            .ParishType = CType(cbParishType.SelectedItem, GenericListItem(Of BoundaryDB.BoundaryItem.ParishTypes)).Value
            .CouncilStyle = CType(cbStyle.SelectedItem, GenericListItem(Of BoundaryDB.BoundaryItem.CouncilStyles)).Value
            .IsBorough = chkBorough.Checked
            .IsRoyal = chkRoyal.Checked
            .IsCity = chkCity.Checked
            .Notes = Trim(txtNotes.Text)
        End With
    End Sub
End Class