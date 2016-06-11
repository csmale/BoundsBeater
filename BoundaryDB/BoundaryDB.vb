Imports System.Configuration
Imports System.Xml
Imports System.Xml.XPath
Imports OSMLibrary

Public Class BoundaryDB
    Private xDoc As XmlDocument
    Private bFromXML As Boolean = False
    Public Path As String
    Public Items As New Dictionary(Of String, BoundaryItem)
    Dim ss() As String
    Dim bChanges As Boolean
    Private _ChangedItems As List(Of BoundaryItem)

    Public ReadOnly Property ChangedItems As List(Of BoundaryItem)
        Get
            Return _ChangedItems
        End Get
    End Property
    Public Sub New()
    End Sub
    Public Sub New(sFile As String)
        LoadFromFile(sFile)
    End Sub
    Public Function LoadFromFile(sFile As String) As Boolean
        xDoc = New XmlDocument
        ' xDoc.PreserveWhitespace = True
        Try
            xDoc.Load(sFile)
        Catch e As Exception
            MsgBox(e.Message)
            Return False
        End Try
        Path = sFile
        Return LoadXML(xDoc)
    End Function
    Public Function LoadXML(xDoc As XmlDocument) As Boolean
        Dim xItem As BoundaryItem
        Dim xUnknown As New BoundaryItem(Me)

        For Each xBnd As XmlElement In xDoc.SelectNodes("/boundaries/boundary")
            xItem = New BoundaryItem(Me, xBnd)
            Items.Add(xItem.ONSCode, xItem)
        Next
        Items.Add("Unknown", xUnknown)

        For Each xItem In Items.Values
            If Len(xItem.ParentCode) > 0 Then
                If Items.ContainsKey(xItem.ParentCode) Then
                    xItem.Parent = Items(xItem.ParentCode)
                Else
                    If xItem.ParentCode <> "0" Then
                        xItem.Parent = xUnknown
                        MsgBox("undefined parent referenced: " & xItem.ParentCode)
                    End If
                End If
            End If
        Next

        Dim itmp As Integer
        For Each xItem In Items.Values
            If IsNothing(xItem.Parent) Then
                If xItem.AdminLevel <> 2 AndAlso xItem IsNot xUnknown Then
                    itmp = itmp + 1
                End If
            End If
        Next
        If itmp > 1 Then
            MsgBox(itmp & " items have no parent.")
        End If
        bFromXML = True
        Return True
    End Function
    Public Function LoadCSV(sFile As String) As Boolean

    End Function
    Public Function Save() As Boolean
        Return SaveAsXML(Path)
    End Function
    Public Function SaveAsXML(sFile As String) As Boolean
        Dim xw As XmlTextWriter
        Try
            xw = New XmlTextWriter(sFile, Nothing)
            xw.Formatting = Formatting.Indented
            xDoc.Save(xw)
            xw.Close()
            bChanges = False
            Return True
        Catch e As XmlException
            xw?.Close()
            MsgBox(e.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Save error")
            Return False
        End Try
    End Function
    Public Function SaveAsCSV(sFile As String) As Boolean

    End Function

    Public Function UpdateFromOSMFile(sFile As String) As Boolean
        Dim xRdr As XmlTextReader
        Dim xDoc As XmlDocument
        Dim xEl As XmlElement
        Dim ID As Long
        Dim sBndType As String
        Dim OSMIndex As New Dictionary(Of Long, BoundaryItem)
        Dim toRemove As New List(Of Long)
        Dim bi As BoundaryItem
        For Each x In Items
            If x.Value.OSMRelation <> 0 Then
                If OSMIndex.Keys.Contains(x.Value.OSMRelation) Then
                    If Not toRemove.Contains(x.Value.OSMRelation) Then toRemove.Add(x.Value.OSMRelation)
                Else
                    OSMIndex.Add(x.Value.OSMRelation, x.Value)
                End If
            End If
        Next
        For Each x In toRemove
            OSMIndex.Remove(x)
        Next

        Try
            xRdr = New XmlTextReader(sFile)
        Catch
            Return False
        End Try
        xRdr.WhitespaceHandling = WhitespaceHandling.None
        While Not xRdr.EOF
            Select Case xRdr.NodeType
                Case XmlNodeType.Element
                    xDoc = New XmlDocument()
                    Select Case xRdr.Name
                        Case "relation"
                            xEl = xDoc.ReadNode(xRdr)
                            If GetTag(xEl, "type") <> "boundary" Then
                                xRdr.Read()
                                Continue While
                            End If
                            sBndType = GetTag(xEl, "boundary")
                            If sBndType = "administrative" Or sBndType = "ceremonial" Then
                                ID = CLng(xEl.Attributes("id").InnerText)
                                If OSMIndex.Keys.Contains(ID) Then
                                    bi = OSMIndex(ID)
                                    ' now update bi from the tags in xEl
                                    updateBIfromXML(bi, xEl)
                                End If
                            End If
                        Case Else
                            xRdr.Read()
                    End Select
                Case Else
                    xRdr.Read()
            End Select
        End While
        If bChanges Then
            Return Save()
        End If
        Return True
    End Function
    Private Sub updateBIfromXML(bi As BoundaryItem, xEl As XmlElement)
        Dim sTmp As String
        Dim btType As BoundaryItem.BoundaryTypes
        Dim csStyle As BoundaryItem.CouncilStyles

        Dim sCouncilName As String
        Dim sCouncilNameWelsh As String
        Dim ptParishType As BoundaryItem.ParishTypes
        Dim sGSS As String

        sTmp = GetTag(xEl, "designation")
        btType = BoundaryItem.DesignationToBoundaryType(sTmp)
        If btType = BoundaryItem.BoundaryTypes.BT_Unknown AndAlso Len(sTmp) > 0 Then
            MsgBox(String.Format("Unknown designation {0} in relation {1} ({2})", sTmp, bi.OSMRelation, bi.Name))
        End If
        sCouncilName = GetTag(xEl, "council_name:en")
        If Len(sCouncilName) = 0 Then sCouncilName = GetTag(xEl, "council_name")
        sCouncilNameWelsh = GetTag(xEl, "council_name:cy")
        If sCouncilNameWelsh = "" Then
            sCouncilNameWelsh = GetTag(xEl, "council_name:kw")
        End If
        ptParishType = BoundaryItem.ParishType_FromString(GetTag(xEl, "parish_type"))
        csStyle = BoundaryItem.CouncilStyle_FromString(GetTag(xEl, "council_style"))
        sGSS = GetTag(xEl, "ref:gss")

        If Len(sGSS) = 8 Then bi.ONSCode = sGSS
        If btType <> BoundaryItem.BoundaryTypes.BT_Unknown Then bi.BoundaryType = btType
        If Len(sCouncilName) > 0 Then bi.CouncilName = sCouncilName
        If Len(sCouncilNameWelsh) > 0 Then bi.CouncilName2 = sCouncilNameWelsh
        If btType = BoundaryItem.BoundaryTypes.BT_CivilParish Then
            bi.ParishType = ptParishType
        End If
        If csStyle <> BoundaryItem.CouncilStyles.CS_Default Then bi.CouncilStyle = csStyle
        bi.UpdateXML()
    End Sub
    Private Function GetTag(xElement As XmlElement, sKey As String) As String
        Dim xTag As XmlElement
        xTag = xElement.SelectSingleNode("tag[@k='" & sKey & "']")
        If xTag Is Nothing Then Return ""
        Return xTag.GetAttribute("v")
    End Function


    '	<boundary>
    '	<type>adm_county</type>
    '	<newcode>E10000008</newcode>
    '	<ons>18</ons>
    '	<name>Devon</name>
    '	<name2>Gogledd Ddwyrain</name2>
    '	<par_name/>
    '	<admin_level>6</admin_level>
    '	<norm_name>devon</norm_name>
    '	<osmid>r190380</osmid>
    '   </boundary>

    '<ID>1</ID>
    '<region>region</region>
    '<newcode>E12000001</newcode>
    '<ons>A</ons>
    '<name>North East England</name>
    '<name2>Gogledd Ddwyrain</name2>
    '<par_name></par_name>
    '<par_new></par_new>
    '<tgt_lvl>5</tgt_lvl>
    '<norm_name>north east england</norm_name>
    '<osmid>r151164</osmid>
    '</boundary>

    Public Class BoundaryItem
        Private _bdb As BoundaryDB
        Private _xNode As XmlElement
        Public Property Name As String
        Public Name2 As String
        Public Enum BoundaryTypes
            BT_Unknown
            BT_Country
            BT_Nation
            BT_Region
            BT_CeremonialCounty
            BT_MetroCounty
            BT_NonMetroCounty
            BT_MetroDistrict
            BT_NonMetroDistrict
            BT_CivilParish
            BT_Community
            BT_Unitary
            BT_SuiGeneris
            BT_Liberty
            BT_LondonBorough
            BT_PreservedCounty
            BT_PrincipalArea
            BT_ScotCouncil
            BT_NIreDistrict
            BT_ParishGroup
        End Enum
        Public Enum CouncilStyles
            CS_Default
            CS_District
            CS_Borough
            CS_Town
            CS_City
            CS_CityAndCounty
            CS_CityAndDistrict
            CS_County
            CS_Community
            CS_Village
        End Enum
        Public Enum ParishTypes
            PT_ParishCouncil
            PT_ParishMeeting
            PT_JointParishCouncil
            PT_LandsCommon
            PT_NA
        End Enum
        Public Enum LeadershipTypes
            LT_Leader
            LT_Convenor
            LT_Mayor
            LT_LordMayor
            LT_ElectedMayor
            LT_LordProvost
        End Enum
        Public CouncilName As String
        Public CouncilName2 As String
        Public ParishType As ParishTypes
        Public CouncilStyle As CouncilStyles
        Public BoundaryType As BoundaryTypes
        Public Property ONSCode As String
        Public ParentCode As String
        Public OSMRelation As Long
        Public AdminLevel As Integer
        Public NormName As String
        Public IsBorough As Boolean
        Public IsRoyal As Boolean
        Public IsCity As Boolean
        Public Notes As String
        Protected Friend Parent As BoundaryItem
        Private _btcode As String

        Public Sub New(bdb As BoundaryDB)
            _bdb = bdb
        End Sub
        Public Sub New(bdb As BoundaryDB, xBnd As XmlElement)
            _bdb = bdb
            LoadFromXML(xBnd)
        End Sub
        Public Shadows ReadOnly Property ToString As String
            Get
                Return Name
            End Get
        End Property

        Public ReadOnly Property TypeCode As String
            Get
                Return _btcode
            End Get
        End Property
        Public Function LoadFromXML(xBnd As XmlElement) As Boolean
            Dim sTmp As String
            Name = NodeText(xBnd.SelectSingleNode("name"))
            Name2 = NodeText(xBnd.SelectSingleNode("name2"))
            ONSCode = NodeText(xBnd.SelectSingleNode("newcode"))
            If Len(ONSCode) = 0 Then
                ONSCode = NodeText(xBnd.SelectSingleNode("ons"))
            End If
            CouncilName = NodeText(xBnd.SelectSingleNode("council_name"))
            CouncilName2 = NodeText(xBnd.SelectSingleNode("council_name2"))
            sTmp = NodeText(xBnd.SelectSingleNode("type"))
            Select Case sTmp
                Case "region"
                    BoundaryType = BoundaryTypes.BT_Region
                    _btcode = "RGN"
                Case "unitary"
                    BoundaryType = BoundaryTypes.BT_Unitary
                    _btcode = "UA"
                Case "civil_parish"
                    BoundaryType = BoundaryTypes.BT_CivilParish
                    _btcode = "PAR"
                Case "adm_county"
                    BoundaryType = BoundaryTypes.BT_NonMetroCounty
                    _btcode = "CTY"
                Case "non_metro_district"
                    BoundaryType = BoundaryTypes.BT_NonMetroDistrict
                    _btcode = "NMD"
                Case "metro_county"
                    BoundaryType = BoundaryTypes.BT_MetroCounty
                    _btcode = "MCTY"
                Case "metro_district"
                    BoundaryType = BoundaryTypes.BT_MetroDistrict
                    _btcode = "MD"
                Case "nation"
                    BoundaryType = BoundaryTypes.BT_Nation
                    _btcode = "CTRY"
                Case "ceremonial_county"
                    BoundaryType = BoundaryTypes.BT_CeremonialCounty
                    _btcode = "CCTY"
                Case "sui_generis"
                    BoundaryType = BoundaryTypes.BT_SuiGeneris
                    _btcode = "SGEN"
                Case "liberty"
                    BoundaryType = BoundaryTypes.BT_Liberty
                    _btcode = "LBTY"
                Case "london_borough"
                    BoundaryType = BoundaryTypes.BT_LondonBorough
                    _btcode = "LONB"
                Case "preserved_county"
                    BoundaryType = BoundaryTypes.BT_PreservedCounty
                    _btcode = "PCTY"
                Case "wales_district"
                    BoundaryType = BoundaryTypes.BT_PrincipalArea
                    _btcode = "UA"
                Case "scotland_district"
                    BoundaryType = BoundaryTypes.BT_ScotCouncil
                    _btcode = "CA"
                Case "country"
                    BoundaryType = BoundaryTypes.BT_Country
                    _btcode = "UK"
                Case "community"
                    BoundaryType = BoundaryTypes.BT_Community
                    _btcode = "COM"
                Case "n_ireland_district"
                    BoundaryType = BoundaryTypes.BT_NIreDistrict
                    _btcode = "NID"
                Case "parish_group"
                    BoundaryType = BoundaryTypes.BT_ParishGroup
                    _btcode = "PGRP"
                Case Else
                    MsgBox("unimplemented boundary type " & sTmp)
                    BoundaryType = BoundaryTypes.BT_Nation
                    _btcode = "?"
            End Select
            If BoundaryType = BoundaryTypes.BT_CivilParish Then
                ParishType = ParishType_FromString(NodeText(xBnd.SelectSingleNode("parish_type")))
            Else
                ParishType = ParishTypes.PT_NA
            End If
            CouncilStyle = CouncilStyle_FromString(NodeText(xBnd.SelectSingleNode("council_style")))
            sTmp = NodeText(xBnd.SelectSingleNode("osmid"))
            If Left(sTmp, 1) = "r" Then
                OSMRelation = CLng(Mid(sTmp, 2))
            End If
            ParentCode = NodeText(xBnd.SelectSingleNode("par_new"))
            If Len(ParentCode) = 0 Or ParentCode = "0" Then
                ParentCode = NodeText(xBnd.SelectSingleNode("par_ons"))
            End If
            NormName = NodeText(xBnd.SelectSingleNode("norm_name"))
            sTmp = NodeText(xBnd.SelectSingleNode("admin_level"))
            If IsNumeric(sTmp) Then
                AdminLevel = CLng(sTmp)
                If AdminLevel < 0 Or AdminLevel > 11 Then AdminLevel = 0
            Else
                AdminLevel = 0
            End If
            sTmp = NodeText(xBnd.SelectSingleNode("is_borough"))
            If Len(sTmp) = 0 Then
                IsBorough = (CouncilStyle = CouncilStyles.CS_Borough) Or (BoundaryType = BoundaryTypes.BT_LondonBorough)
            Else
                IsBorough = sTmp = "1"
            End If
            sTmp = NodeText(xBnd.SelectSingleNode("is_royal"))
            If Len(sTmp) = 0 Then
                IsRoyal = (InStr(LCase(CouncilName), "royal") + InStr(LCase(CouncilName), "regis") + InStr(LCase(Name), "royal") + InStr(LCase(Name), "regis")) > 0
            Else
                IsRoyal = sTmp = "1"
            End If
            sTmp = NodeText(xBnd.SelectSingleNode("is_city"))
            If Len(sTmp) = 0 Then
                IsCity = CouncilStyle = CouncilStyles.CS_City Or CouncilStyle = CouncilStyles.CS_CityAndCounty Or CouncilStyle = CouncilStyles.CS_CityAndDistrict
            Else
                IsCity = sTmp = "1"
            End If
            Notes = NodeText(xBnd.SelectSingleNode("notes"))
            _xNode = xBnd
            Return True
        End Function
        Public Shared Function BoundaryType_FromString(s As String) As BoundaryTypes
            Dim BoundaryType As BoundaryTypes
            If Len(s) = 0 Then Return BoundaryTypes.BT_Unknown
            Select Case s
                Case "region" : BoundaryType = BoundaryTypes.BT_Region
                Case "unitary" : BoundaryType = BoundaryTypes.BT_Unitary
                Case "civil_parish" : BoundaryType = BoundaryTypes.BT_CivilParish
                Case "adm_county" : BoundaryType = BoundaryTypes.BT_NonMetroCounty
                Case "non_metro_district" : BoundaryType = BoundaryTypes.BT_NonMetroDistrict
                Case "metro_county" : BoundaryType = BoundaryTypes.BT_MetroCounty
                Case "metro_district" : BoundaryType = BoundaryTypes.BT_MetroDistrict
                Case "nation" : BoundaryType = BoundaryTypes.BT_Nation
                Case "ceremonial_county" : BoundaryType = BoundaryTypes.BT_CeremonialCounty
                Case "sui_generis" : BoundaryType = BoundaryTypes.BT_SuiGeneris
                Case "liberty" : BoundaryType = BoundaryTypes.BT_Liberty
                Case "london_borough" : BoundaryType = BoundaryTypes.BT_LondonBorough
                Case "preserved_county" : BoundaryType = BoundaryTypes.BT_PreservedCounty
                Case "wales_district" : BoundaryType = BoundaryTypes.BT_PrincipalArea
                Case "scotland_district" : BoundaryType = BoundaryTypes.BT_ScotCouncil
                Case "country" : BoundaryType = BoundaryTypes.BT_Country
                Case "community" : BoundaryType = BoundaryTypes.BT_Community
                Case "n_ireland_district" : BoundaryType = BoundaryTypes.BT_NIreDistrict
                Case "parish_group" : BoundaryType = BoundaryTypes.BT_ParishGroup
                Case Else
                    MsgBox("unrecognised boundary type " & s)
                    BoundaryType = BoundaryTypes.BT_Unknown
            End Select
            Return BoundaryType
        End Function
        Public Shared Function CouncilStyle_ToString(cs As CouncilStyles) As String
            Dim sTmp As String
            Select Case cs
                Case CouncilStyles.CS_Default : sTmp = ""
                Case CouncilStyles.CS_District : sTmp = "district"
                Case CouncilStyles.CS_Borough : sTmp = "borough"
                Case CouncilStyles.CS_Town : sTmp = "town"
                Case CouncilStyles.CS_City : sTmp = "city"
                Case CouncilStyles.CS_CityAndCounty : sTmp = "city_and_county"
                Case CouncilStyles.CS_CityAndDistrict : sTmp = "city_and_district"
                Case CouncilStyles.CS_County : sTmp = "county"
                Case CouncilStyles.CS_Community : sTmp = "community"
                Case CouncilStyles.CS_Village : sTmp = "village"
                Case Else
                    sTmp = ""
            End Select
            Return sTmp
        End Function
        Public Shared Function CouncilStyle_FromString(s As String) As CouncilStyles
            Dim xRet As CouncilStyles
            Select Case s
                Case "borough"
                    xRet = CouncilStyles.CS_Borough
                Case "district"
                    xRet = CouncilStyles.CS_District
                Case "city"
                    xRet = CouncilStyles.CS_City
                Case "town"
                    xRet = CouncilStyles.CS_Town
                Case "community"
                    xRet = CouncilStyles.CS_Community
                Case "city_and_county", "city and county"
                    xRet = CouncilStyles.CS_CityAndCounty
                Case "city_and_district"
                    xRet = CouncilStyles.CS_CityAndDistrict
                Case "county"
                    xRet = CouncilStyles.CS_County
                Case "village"
                    xRet = CouncilStyles.CS_Village
                Case Else
                    If Len(s) > 0 Then
                        MsgBox("unknown council style " & s)
                    End If
                    xRet = CouncilStyles.CS_Default
            End Select
            Return xRet
        End Function
        Public Shared Function BoundaryType_ToString(bt As BoundaryTypes) As String
            Dim sTmp As String
            Select Case bt
                Case BoundaryTypes.BT_Country : sTmp = "country"
                Case BoundaryTypes.BT_Nation : sTmp = "nation"
                Case BoundaryTypes.BT_Region : sTmp = "region"
                Case BoundaryTypes.BT_CeremonialCounty : sTmp = "ceremonial_county"
                Case BoundaryTypes.BT_MetroCounty : sTmp = "metro_county"
                Case BoundaryTypes.BT_NonMetroCounty : sTmp = "adm_county"
                Case BoundaryTypes.BT_MetroDistrict : sTmp = "metro_district"
                Case BoundaryTypes.BT_NonMetroDistrict : sTmp = "non_metro_district"
                Case BoundaryTypes.BT_CivilParish : sTmp = "civil_parish"
                Case BoundaryTypes.BT_Community : sTmp = "community"
                Case BoundaryTypes.BT_Unitary : sTmp = "unitary"
                Case BoundaryTypes.BT_SuiGeneris : sTmp = "sui_generis"
                Case BoundaryTypes.BT_Liberty : sTmp = "liberty"
                Case BoundaryTypes.BT_LondonBorough : sTmp = "london_borough"
                Case BoundaryTypes.BT_PreservedCounty : sTmp = "preserved_county"
                Case BoundaryTypes.BT_PrincipalArea : sTmp = "wales_district"
                Case BoundaryTypes.BT_ScotCouncil : sTmp = "scotland_district"
                Case BoundaryTypes.BT_NIreDistrict : sTmp = "n_ireland_district"
                Case BoundaryTypes.BT_ParishGroup : sTmp = "parish_group"
                Case Else
                    sTmp = ""
            End Select
            Return sTmp
        End Function
        Public Shared Function DesignationToBoundaryType(s As String) As BoundaryTypes
            Dim BoundaryType As BoundaryTypes
            If Len(s) = 0 Then Return BoundaryTypes.BT_Unknown
            Select Case s
                'Case "region" : BoundaryType = BoundaryTypes.BT_Region
                Case "unitary_authority" : BoundaryType = BoundaryTypes.BT_Unitary
                Case "civil_parish" : BoundaryType = BoundaryTypes.BT_CivilParish
                Case "non_metropolitan_county" : BoundaryType = BoundaryTypes.BT_NonMetroCounty
                Case "non_metropolitan_district" : BoundaryType = BoundaryTypes.BT_NonMetroDistrict
                Case "metropolitan_county" : BoundaryType = BoundaryTypes.BT_MetroCounty
                Case "metropolitan_district" : BoundaryType = BoundaryTypes.BT_MetroDistrict
                'Case "nation" : BoundaryType = BoundaryTypes.BT_Nation
                Case "ceremonial_county" : BoundaryType = BoundaryTypes.BT_CeremonialCounty
                Case "sui_generis" : BoundaryType = BoundaryTypes.BT_SuiGeneris
                Case "liberty" : BoundaryType = BoundaryTypes.BT_Liberty
                Case "inner_london_borough", "outer_london_borough" : BoundaryType = BoundaryTypes.BT_LondonBorough
                Case "preserved_county" : BoundaryType = BoundaryTypes.BT_PreservedCounty
                Case "principal_area" : BoundaryType = BoundaryTypes.BT_PrincipalArea
                Case "scotland_district" : BoundaryType = BoundaryTypes.BT_ScotCouncil
                Case "country" : BoundaryType = BoundaryTypes.BT_Country
                Case "community" : BoundaryType = BoundaryTypes.BT_Community
                Case "n_ireland_district" : BoundaryType = BoundaryTypes.BT_NIreDistrict
                Case Else
                    ' MsgBox("unrecognised boundary designation " & s)
                    BoundaryType = BoundaryTypes.BT_Unknown
            End Select
            Return BoundaryType
        End Function
        Public Shared Function ParishType_ToString(pt As ParishTypes) As String
            Dim sTmp As String
            Select Case pt
                Case ParishTypes.PT_ParishCouncil : sTmp = "parish_council"
                Case ParishTypes.PT_ParishMeeting : sTmp = "parish_meeting"
                Case ParishTypes.PT_JointParishCouncil : sTmp = "joint_parish_council"
                Case ParishTypes.PT_LandsCommon : sTmp = "lands_common"
                Case ParishTypes.PT_NA : sTmp = ""
                Case Else
                    sTmp = ""
            End Select
            Return sTmp
        End Function
        Public Shared Function ParishType_FromString(s As String) As ParishTypes
            Dim xRet As ParishTypes
            Select Case s
                Case "parish_council"
                    xRet = ParishTypes.PT_ParishCouncil
                Case "parish_meeting"
                    xRet = ParishTypes.PT_ParishMeeting
                Case "joint_parish_council"
                    xRet = ParishTypes.PT_JointParishCouncil
                Case "lands_common"
                    xRet = ParishTypes.PT_LandsCommon
                Case ""
                    xRet = ParishTypes.PT_ParishCouncil
                Case Else
                    xRet = ParishTypes.PT_NA
            End Select
            Return xRet
        End Function
        Public Sub SetIDinXML()
            Dim xNode As XmlElement = _xNode
            If OSMRelation = 0 Then
                SetValue(xNode, "osmid", "")
            Else
                SetValue(xNode, "osmid", "r" & OSMRelation.ToString())
            End If
        End Sub
        Public Function Edit() As Boolean
            Dim f As New frmEdit
            f.xDB = _bdb
            f.xItem = Me
            If f.ShowDialog() = DialogResult.OK Then
                NormName = BoundaryDB.Normalise(Name)
                If _xNode Is Nothing Then
                    _xNode = _bdb.xDoc.CreateElement("boundary")
                    _bdb.xDoc.DocumentElement.AppendChild(_xNode)
                    SetValue(_xNode, "par_new", ParentCode)
                    SetValue(_xNode, "par_name", Parent.Name)
                End If
                UpdateXML()
                If _bdb.bChanges Then
                    _bdb.Save()
                End If
            Else
                Return False
            End If
            Return True
        End Function
        Friend Sub UpdateXML()
            SetValue(_xNode, "name", Name)
            SetValue(_xNode, "name2", Name2)
            SetValue(_xNode, "council_name", CouncilName)
            SetValue(_xNode, "council_name2", CouncilName2)
            SetValue(_xNode, "newcode", ONSCode)
            SetValue(_xNode, "type", BoundaryType_ToString(BoundaryType))
            SetValue(_xNode, "council_style", CouncilStyle_ToString(CouncilStyle))
            SetValue(_xNode, "parish_type", If(BoundaryType = BoundaryTypes.BT_CivilParish, ParishType_ToString(ParishType), ""))
            SetValue(_xNode, "norm_name", NormName)
            SetValue(_xNode, "is_borough", If(IsBorough, "1", "0"))
            SetValue(_xNode, "is_royal", If(IsRoyal, "1", "0"))
            SetValue(_xNode, "is_city", If(IsCity, "1", "0"))
            SetValue(_xNode, "notes", Notes)
        End Sub
        Private Sub SetValue(xNode As XmlElement, sKey As String, sValue As String)
            Dim xChild As XmlElement = xNode.SelectSingleNode(sKey)
            ' how to insert a missing element?
            If IsNothing(xChild) Then
                ' don't *add* element that doesn't already exist if the value is blank or boolean "false"
                If Len(sValue) = 0 Then Exit Sub
                If Left(sKey, 3) = "is_" AndAlso sValue = "0" Then Exit Sub
                xChild = xNode.OwnerDocument.CreateElement(sKey)
                xNode.AppendChild(xChild)
            End If
            If xChild.InnerText <> sValue Then
                If sValue = "" Then
                    xChild.IsEmpty = True
                Else
                    xChild.InnerText = sValue
                End If
                _bdb.bChanges = True
            End If
        End Sub
    End Class
    Private Shared Function NodeText(xNode As XmlNode) As String
        If IsNothing(xNode) Then
            Return ""
        Else
            Return xNode.InnerText
        End If
    End Function

    Public Function MergeOSM(xOSMDoc As OSMDoc, Optional bUpdateAll As Boolean = False) As Boolean
        Dim xRel As OSMRelation
        Dim iLevel As Integer
        Dim sLevel As String
        Dim sName As String
        Dim sNorm As String
        Dim sType As String
        Dim sBType As String
        Dim sONS As String
        Dim oMatches As New List(Of BoundaryItem)
        Dim xMatch As BoundaryItem
        Dim xBnd As BoundaryItem
        Dim bAbort As Boolean = False
        Dim iIndex As Integer

        bChanges = False
        _ChangedItems = New List(Of BoundaryItem)
        iIndex = 0
        For Each xRel In xOSMDoc.Relations.Values
            iIndex = iIndex + 1
            sType = xRel.Tag("type")
            If sType = "boundary" Then
                sBType = xRel.Tag("boundary")
                If sBType <> "administrative" And sBType <> "ceremonial" Then
                    Continue For
                End If
                sName = xRel.Tag("name")
                sLevel = xRel.Tag("admin_level")
                If IsNumeric(sLevel) Then
                    iLevel = CLng(sLevel)
                    If iLevel < 2 Or iLevel > 11 Then iLevel = 0
                Else
                    iLevel = 0
                End If

                ' now we have a boundary relation from the OSM file
                ' time to find the real boundary it corresponds to
                ' match on normalised name, admin level
                oMatches.Clear()
                sNorm = Normalise(sName)

                For Each sONS In Me.Items.Keys
                    xBnd = Items(sONS)
                    If xBnd.NormName = sNorm Then
                        If xBnd.AdminLevel = iLevel Then
                            ' excellent match!
                            oMatches.Add(xBnd)
                        Else
                            oMatches.Add(xBnd)
                        End If
                    End If
                Next

                ' see if there is an obvious match
                ' check for a single match with the correct OSM ID
                Dim i As Integer = 0
                For Each xMatch In oMatches
                    If xMatch.OSMRelation = xRel.ID Then i += 1
                Next
                If i <> 1 Or bUpdateAll Then
                    Dim frm As New frmMatch
                    frm.oMatches = oMatches
                    frm.oOSMDoc = xOSMDoc
                    frm.oDB = Me
                    frm.oRel = xRel
                    frm.sComment = "Relation " & iIndex.ToString & " Of " & xOSMDoc.Relations.Count.ToString
                    xMatch = Nothing
                    Select Case frm.ShowDialog()
                        Case DialogResult.OK
                            ' MsgBox("Match found For " & sName & " (" & iLevel & ")")
                            xMatch = frm.oMatch
                        Case DialogResult.Ignore
                            bAbort = True
                    End Select
                    frm.Dispose()
                    frm = Nothing
                    If Not IsNothing(xMatch) Then
                        Dim bchg As Boolean = False
                        Dim pt As BoundaryItem.ParishTypes
                        ' update osmid in boundary item
                        If xMatch.OSMRelation <> xRel.ID Then
                            xMatch.OSMRelation = xRel.ID
                            xMatch.SetIDinXML()
                            bchg = True
                        End If
                        pt = BoundaryItem.ParishType_FromString(xRel.Tag("parish_type"))
                        If pt <> xMatch.ParishType Then
                            xMatch.ParishType = pt
                            bchg = True
                        End If
                        If xMatch.CouncilName <> xRel.Tag("council_name") Then
                            xMatch.CouncilName = xRel.Tag("council_name")
                            bchg = True
                        End If
                        If bchg Then
                            _ChangedItems.Add(xMatch)
                            bChanges = True
                        End If
                    End If
                    End If
                If bAbort Then Exit For
            End If
        Next
        If bChanges Then
            If MsgBox("Save Changes?", MsgBoxStyle.OkCancel + MsgBoxStyle.Question) = MsgBoxResult.Ok Then
                ' but now we have to persist the changes somehow...
                Save()
            End If
        End If
        Return True
    End Function
    Private Shared Function Normalise(sName As String) As String
        Static ss() As String = {
            "london borough Of ",
            "royal borough Of ",
            "city Of ",
            " cp",
            " civil parish",
            "district Of ",
            "borough Of ",
            "parish Of ",
            " county council",
            " district council",
            " borough council",
            " city council",
            " district",
            " borough",
            " parish",
            " community",
            " tc",
            ".",
            "'",
            ","
        }
        Dim sTmp As String
        Dim iParen As Integer
        If sName = "RESET" Then
            Return "OK"
        End If
        sTmp = sName
        iParen = InStr(sTmp, "(")
        If iParen > 0 Then
            sTmp = Left(sTmp, iParen - 1)
        End If
        sTmp = RemoveStuff(LCase(sTmp), ss)
        sTmp = Replace(sTmp, "-", " ")
        sTmp = Replace(sTmp, "hertfordshire", "herts")
        sTmp = Replace(sTmp, "saint ", "st ")
        sTmp = Replace(sTmp, "&", " and ")
        sTmp = Trim(Replace(sTmp, "  ", " "))
        Return sTmp
    End Function
    Shared Function RemoveStuff(s As String, x As String()) As String
        Dim i As Integer
        Dim sTmp As String
        sTmp = s
        For i = LBound(x) To UBound(x)
            sTmp = Replace(sTmp, x(i), "")
        Next
        Return sTmp
    End Function

End Class
