Imports OSMLibrary

Public Class BoundaryDBReviewProvider
    Implements IOSMReviewProvider
    Private Const ADMIN_LEVEL As String = "admin_level"
    Private Const ADMINISTRATIVE As String = "administrative"
    Private Const BOROUGH As String = "borough"
    Private Const BOUNDARY As String = "boundary"
    Private Const NAME As String = "name"
    Private Const NAME_WELSH As String = "name:cy"
    Private Const NAME_GAELIC As String = "name:gd"
    Private Const CEREMONIAL As String = "ceremonial"
    Private Const CEREMONIAL_COUNTY As String = "ceremonial_county"
    Private Const CITY As String = "city"
    Private Const CITY_AND_COUNTY As String = "city_and_county"
    Private Const CITY_AND_DISTRICT As String = "city_and_district"
    Private Const CIVIL_PARISH As String = "civil_parish"
    Private Const COMMUNITY As String = "community"
    Private Const COUNCIL_NAME As String = "council_name"
    Private Const COUNCIL_NAME_WELSH As String = "council_name:cy"
    Private Const COUNCIL_NAME_GAELIC As String = "council_name:gd"
    Private Const COUNCIL_STYLE As String = "council_style"
    Private Const COUNTY As String = "county"
    Private Const DESIGNATION As String = "designation"
    Private Const EMPTY As String = ""
    Private Const LONDON_BOROUGH As String = "london_borough"
    Private Const JOINT_PARISH_COUNCIL As String = "joint_parish_council"
    Private Const JOINT_PARISH_MEETING As String = "joint_parish_meeting"
    Private Const METRO_COUNTY As String = "metropolitan_county"
    Private Const METRO_DISTRICT As String = "metropolitan_district"
    Private Const NEIGHBOURHOOD As String = "neighbourhood"
    Private Const NON_METRO_COUNTY As String = "non_metropolitan_county"
    Private Const NON_METRO_DISTRICT As String = "non_metropolitan_district"
    Private Const PARISH_COUNCIL As String = "parish_council"
    Private Const PARISH_MEETING As String = "parish_meeting"
    Private Const PARISH_TYPE As String = "parish_type"
    Private Const PRESERVED_COUNTY As String = "preserved_county"
    Private Const PRINCIPAL_AREA As String = "principal_area"
    Private Const REF_GSS As String = "ref:gss"
    Private Const ROYAL As String = "royal"
    Private Const TOWN As String = "town"
    Private Const TYPE As String = "type"
    Private Const UNITARY_AUTHORITY As String = "unitary_authority"
    Private Const VILLAGE As String = "village"
    Private Const WEBSITE As String = "website"
    Private Const YES As String = "yes"


    Public dbi As BoundaryDB.BoundaryItem
    Private dict As Dictionary(Of String, String)
    Private rel As OSMRelation

    Public Function Process(o As OSMObject, d As Dictionary(Of String, String)) As OSMReviewResult Implements IOSMReviewProvider.Process
        If dbi Is Nothing Then Return OSMReviewResult.NoData
        If o.Type <> OSMObject.ObjectType.Relation Then Return OSMReviewResult.WrongType
        If d Is Nothing Then Throw New ArgumentNullException()
        dict = d
        dict.Clear()

        rel = DirectCast(o, OSMRelation)

        Add(TYPE, BOUNDARY)
        Add(BOUNDARY, ADMINISTRATIVE)
        AddNonEmpty(COUNCIL_NAME, dbi.CouncilName)
        AddNonEmpty(NAME, dbi.Name)
        AddNonEmpty(WEBSITE, dbi.Website)
        If dbi.IsRoyal Then Add(ROYAL, YES)

        Select Case dbi.BoundaryType
            Case BoundaryDB.BoundaryItem.BoundaryTypes.BT_CeremonialCounty
                Return ProcessCeremonialCounty()
            Case BoundaryDB.BoundaryItem.BoundaryTypes.BT_CivilParish
                Return ProcessCivilParish()
            Case BoundaryDB.BoundaryItem.BoundaryTypes.BT_Community
                Return ProcessCommunity()
            Case BoundaryDB.BoundaryItem.BoundaryTypes.BT_Country
                Return ProcessCountry()
            Case BoundaryDB.BoundaryItem.BoundaryTypes.BT_Liberty
                Return ProcessLiberty()
            Case BoundaryDB.BoundaryItem.BoundaryTypes.BT_LondonBorough
                Return ProcessLondonBorough()
            Case BoundaryDB.BoundaryItem.BoundaryTypes.BT_MetroCounty
                Return ProcessMetroCounty()
            Case BoundaryDB.BoundaryItem.BoundaryTypes.BT_MetroDistrict
                Return ProcessMetroDistrict()
            Case BoundaryDB.BoundaryItem.BoundaryTypes.BT_Nation
                Return OSMReviewResult.WrongType
            Case BoundaryDB.BoundaryItem.BoundaryTypes.BT_NIreDistrict
                Return OSMReviewResult.WrongType
            Case BoundaryDB.BoundaryItem.BoundaryTypes.BT_NonMetroCounty
                Return ProcessNonMetroCounty()
            Case BoundaryDB.BoundaryItem.BoundaryTypes.BT_NonMetroDistrict
                Return ProcessNonMetroDistrict()
            Case BoundaryDB.BoundaryItem.BoundaryTypes.BT_ParishGroup
                Return OSMReviewResult.WrongType
            Case BoundaryDB.BoundaryItem.BoundaryTypes.BT_PreservedCounty
                Return ProcessPreservedCounty()
            Case BoundaryDB.BoundaryItem.BoundaryTypes.BT_PrincipalArea
                Return ProcessPrincipalArea()
            Case BoundaryDB.BoundaryItem.BoundaryTypes.BT_Region
                Return ProcessCivilParish()
            Case BoundaryDB.BoundaryItem.BoundaryTypes.BT_ScotCouncil
                Return ProcessCivilParish()
            Case BoundaryDB.BoundaryItem.BoundaryTypes.BT_SuiGeneris
                Return ProcessCivilParish()
            Case BoundaryDB.BoundaryItem.BoundaryTypes.BT_Unitary
                Return ProcessUnitary()
            Case BoundaryDB.BoundaryItem.BoundaryTypes.BT_Unknown
                Return OSMReviewResult.WrongType
            Case Else
                Return OSMReviewResult.WrongType
        End Select
        Return OSMReviewResult.WrongType
    End Function
    Private Sub Add(Tag As String, Value As String)
        dict(Tag) = Value
    End Sub
    Private Sub Remove(Tag As String)
        dict(Tag) = EMPTY
    End Sub
    Private Sub AddNonEmpty(Tag As String, Value As String)
        If Len(Value) > 0 Then dict(Tag) = Value
    End Sub
    Private Sub UpdateGSS(Prefix As String)
        Dim sGSS As String = rel.Tag(REF_GSS)

        If Left(dbi.ONSCode, Len(Prefix)) <> Prefix Then
            Debug.Print($"UpdateGSS: DB value {dbi.ONSCode} does not match required prefix {Prefix}")
            Return
        End If
        Add(REF_GSS, dbi.ONSCode)
        If Len(sGSS) > 0 Then ' there is a value in OSM already
            If Left(sGSS, Len(Prefix)) = Prefix Then ' looks possibly valid
                If sGSS > dbi.ONSCode Then ' database out of date?
                    Debug.Print($"UpdateGSS: OSM value {sGSS} seems newer than DB value {dbi.ONSCode}")
                ElseIf sGSS < dbi.ONSCode Then ' database is newer
                    Debug.Print($"UpdateGSS: OSM value {sGSS} seems older than DB value {dbi.ONSCode}, map update required?")
                End If
            Else
                Debug.Print($"UpdateGSS: OSM value {sGSS} does not match required prefix {Prefix}")
            End If
        End If
    End Sub
    Private Sub DoCouncilStyle()
        Select Case dbi.CouncilStyle
            Case BoundaryDB.BoundaryItem.CouncilStyles.CS_City : Add(COUNCIL_STYLE, CITY)
            Case BoundaryDB.BoundaryItem.CouncilStyles.CS_Community : Add(COUNCIL_STYLE, COMMUNITY)
            Case BoundaryDB.BoundaryItem.CouncilStyles.CS_Neighbourhood : Add(COUNCIL_STYLE, NEIGHBOURHOOD)
            Case BoundaryDB.BoundaryItem.CouncilStyles.CS_Town : Add(COUNCIL_STYLE, TOWN)
            Case BoundaryDB.BoundaryItem.CouncilStyles.CS_Village : Add(COUNCIL_STYLE, VILLAGE)
            Case BoundaryDB.BoundaryItem.CouncilStyles.CS_Borough : Add(COUNCIL_STYLE, BOROUGH)
            Case BoundaryDB.BoundaryItem.CouncilStyles.CS_County : Add(COUNCIL_STYLE, COUNTY)
            Case BoundaryDB.BoundaryItem.CouncilStyles.CS_CityAndCounty : Add(COUNCIL_STYLE, CITY_AND_COUNTY)
            Case BoundaryDB.BoundaryItem.CouncilStyles.CS_CityAndDistrict : Add(COUNCIL_STYLE, CITY_AND_DISTRICT)
        End Select
    End Sub


    Private Function ProcessCeremonialCounty() As OSMReviewResult
        Add(BOUNDARY, CEREMONIAL)
        Remove(COUNCIL_NAME)
        Return OSMReviewResult.OK
    End Function
    Private Function ProcessCivilParish() As OSMReviewResult
        Add(ADMIN_LEVEL, "10")
        Add(DESIGNATION, CIVIL_PARISH)
        Select Case dbi.ParishType
            Case BoundaryDB.BoundaryItem.ParishTypes.PT_JointParishCouncil : Add(PARISH_TYPE, JOINT_PARISH_COUNCIL)
            Case BoundaryDB.BoundaryItem.ParishTypes.PT_JointParishMeeting : Add(PARISH_TYPE, JOINT_PARISH_MEETING)
            ' Case BoundaryDB.BoundaryItem.ParishTypes.PT_ParishCouncil : Add(PARISH_TYPE, PARISH_COUNCIL)
            Case BoundaryDB.BoundaryItem.ParishTypes.PT_ParishMeeting : Add(PARISH_TYPE, PARISH_MEETING)
        End Select
        DoCouncilStyle()
        UpdateGSS("E04")
        Return OSMReviewResult.OK
    End Function
    Private Function ProcessCommunity() As OSMReviewResult
        Add(ADMIN_LEVEL, "10")
        Add(DESIGNATION, COMMUNITY)
        AddNonEmpty(NAME_WELSH, dbi.Name2)
        AddNonEmpty(COUNCIL_NAME_WELSH, dbi.CouncilName2)
        UpdateGSS("W04")
        Return OSMReviewResult.OK
    End Function
    Private Function ProcessCountry() As OSMReviewResult
        Add(ADMIN_LEVEL, "2")
        Remove(COUNCIL_NAME)
        Remove(COUNCIL_NAME_GAELIC)
        Remove(COUNCIL_NAME_WELSH)
        Return OSMReviewResult.OK
    End Function
    Private Function ProcessLiberty() As OSMReviewResult
        Add(ADMIN_LEVEL, "9")
        Remove(COUNCIL_NAME)
        Add(DESIGNATION, "liberty")
        Return OSMReviewResult.OK
    End Function
    Private Function ProcessLondonBorough() As OSMReviewResult
        Add(ADMIN_LEVEL, "8")
        Add(DESIGNATION, LONDON_BOROUGH)
        UpdateGSS("E09")
        Return OSMReviewResult.OK
    End Function
    Private Function ProcessMetroCounty() As OSMReviewResult
        Add(ADMIN_LEVEL, "6")
        Add(DESIGNATION, METRO_COUNTY)
        Return OSMReviewResult.OK
    End Function
    Private Function ProcessMetroDistrict() As OSMReviewResult
        Add(ADMIN_LEVEL, "8")
        Add(DESIGNATION, METRO_DISTRICT)
        If dbi.IsBorough Then Add(BOROUGH, YES)
        DoCouncilStyle()
        UpdateGSS("E08")
        Return OSMReviewResult.OK
    End Function
    Private Function ProcessNonMetroCounty() As OSMReviewResult
        Add(ADMIN_LEVEL, "6")
        Add(DESIGNATION, NON_METRO_COUNTY)
        UpdateGSS("E10")
        Return OSMReviewResult.OK
    End Function
    Private Function ProcessNonMetroDistrict() As OSMReviewResult
        Add(ADMIN_LEVEL, "8")
        Add(DESIGNATION, NON_METRO_DISTRICT)
        If dbi.IsBorough Then Add(BOROUGH, YES)
        DoCouncilStyle()
        UpdateGSS("E07")
        Return OSMReviewResult.OK
    End Function
    Private Function ProcessUnitary() As OSMReviewResult
        Add(ADMIN_LEVEL, "6")
        Add(DESIGNATION, UNITARY_AUTHORITY)
        If dbi.IsBorough Then Add(BOROUGH, YES)
        DoCouncilStyle()
        UpdateGSS("E06")
        Return OSMReviewResult.OK
    End Function
    Private Function ProcessPrincipalArea() As OSMReviewResult
        Add(ADMIN_LEVEL, "6")
        Add(DESIGNATION, PRINCIPAL_AREA)
        If dbi.IsBorough Then Add(BOROUGH, YES)
        DoCouncilStyle()
        UpdateGSS("W06")
        Return OSMReviewResult.OK
    End Function
    Private Function ProcessPreservedCounty() As OSMReviewResult
        Add(BOUNDARY, CEREMONIAL)
        Add(DESIGNATION, PRESERVED_COUNTY)
        Remove(COUNCIL_NAME)
        Return OSMReviewResult.OK
    End Function
End Class
