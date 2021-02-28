Imports System.Configuration
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.Xml.XPath
Imports OSMLibrary

Public Class BoundaryDB
    Private xDoc As XmlDocument
    Private bFromXML As Boolean = False
    Public Path As String
    Public Items As New Dictionary(Of String, BoundaryItem)
    Dim ss() As String
    Public bChanges As Boolean
    Private _root As BoundaryItem
    Private _ChangedItems As List(Of BoundaryItem)
    Private Const DeletedAttribute As String = "deleted"
    Private Shared NormaliseRegexps As New ArrayList
    Private Structure NormStuff
        Public re As Regex
        Public repl As String
    End Structure
    Private Const NormaliseFile As String = "Normalise.txt"

    Public ReadOnly Property ChangedItems As List(Of BoundaryItem)
        Get
            Return _ChangedItems
        End Get
    End Property
    Public Sub New()
        _root = New BoundaryItem(Me)
        _root.Parent = Nothing
        GetNormaliseData
    End Sub
    Public Sub New(sFile As String)
        _root = New BoundaryItem(Me)
        _root.Parent = Nothing
        GetNormaliseData()
        LoadFromFile(sFile)
    End Sub
    Public ReadOnly Property Root As BoundaryItem
        Get
            Return _root
        End Get
    End Property
    Private Sub GetNormaliseData()
        Dim sLine As String
        Dim sDir As String
        Dim iHash As Integer
        Dim sPattern As String
        Dim sReplacement As String
        Const DummyChar As Char = ChrW(1)
        Dim re As Regex
        Dim ns As NormStuff
        sDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)

        Dim f As System.IO.StreamReader
        Try
            f = My.Computer.FileSystem.OpenTextFileReader(System.IO.Path.Combine(sDir, NormaliseFile))
        Catch e As Exception
            MsgBox($"Error opening normalisation rules file: {e.Message}")
            Return
        End Try

        Dim iLineNum As Integer = 0
        Try
            While Not f.EndOfStream
                iLineNum += 1
                sLine = f.ReadLine
                iHash = InStr(sLine, "#")
                If iHash > 0 Then sLine = Left(sLine, iHash - 1)
                If Len(Trim(sLine)) = 0 Then Continue While
                sLine = Replace(sLine, "\|", DummyChar)
                Dim a = Split(sLine, "|")
                sPattern = a(0)
                If a.Length > 1 Then
                    sReplacement = a(1)
                    If sReplacement Is Nothing Then sReplacement = ""
                Else
                    sReplacement = ""
                End If
                sPattern = Replace(sPattern, DummyChar, "|")
                sReplacement = Replace(sReplacement, DummyChar, "|")
                If sReplacement Is Nothing Then sReplacement = ""
                re = New Regex(sPattern)
                ns = New NormStuff With {
                    .re = re,
                    .repl = sReplacement
                }
                NormaliseRegexps.Add(ns)
            End While
        Catch e As Exception
            MsgBox($"Error at line {iLineNum} of normalisation rules: {e.Message}")
        Finally
            f.Close()
        End Try
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
            xItem.Parent = _root
        Next
        ' Items.Add("Unknown", xUnknown)

        For Each xItem In Items.Values
            If Len(xItem.ParentCode) > 0 Then
                If Items.ContainsKey(xItem.ParentCode) Then
                    xItem.Parent = Items(xItem.ParentCode)
                Else
                    If xItem.ParentCode <> "0" Then
                        MsgBox($"Item {xItem.ONSCode} ({xItem.Name}) referenced undefined parent: {xItem.ParentCode}")
                    End If
                End If
            End If
        Next

#If True Then
        ' hack to move dorset parishes
        MergeStuff("E06000059", {"E07000049", "E07000050", "E07000051", "E07000052", "E07000053", "E06000059"})

        ' bournemouth, christchurch and poole
        MergeStuff("E06000058", {"E06000028", "E07000048", "E06000029"})

        ' east suffolk
        MergeStuff("E07000244", {"E07000205", "E07000206"})

        ' west suffolk
        MergeStuff("E07000245", {"E07000201", "E07000204"})

        ' somerset west and taunton
        MergeStuff("E07000246", {"E07000190", "E07000191"})

        ' glasgow city
        MergeStuff("S12000049", {"S12000046"})

        ' north lanarkshire
        MergeStuff("S12000050", {"S12000044"})

        Me.Save()
#End If

        Dim itmp As Integer
        For Each xItem In Items.Values
            If IsNothing(xItem.Parent) Then
                If xItem.AdminLevel <> 2 AndAlso xItem IsNot Root Then
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

    Public Function MergeStuff(sTarget As String, sFrom() As String) As Integer
        Dim xitem As BoundaryItem
        Dim iCount As Integer = 0
        Dim xTarget As BoundaryItem = Items(sTarget)
        If IsNothing(xTarget) Then Return iCount
        For Each sSource In sFrom
            If Items.ContainsKey(sSource) Then
                xitem = Items(sSource)
                xTarget.MergeFrom(xitem)
                iCount += 1
            End If
        Next
        Return icount
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
            MsgBox(e.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Save error")
            Return False
        End Try
    End Function
    Public Function SaveAsCSV(sFile As String) As Boolean

    End Function
    Private Shared Function MakePrefix(sName As String) As String
        If Len(sName) = 0 Then Return sName
        Dim sTmp As String = LCase(Replace(sName, " ", ""))

        If sTmp.StartsWith("northeast") OrElse sTmp.StartsWith("northwest") _
            OrElse sTmp.StartsWith("southeast") OrElse sTmp.StartsWith("southwest") Then
            sTmp = Left(sTmp, 1) & Mid(sTmp, 6, 1) & Mid(sTmp, 10)
        ElseIf sTmp.StartsWith("west") OrElse sTmp.StartsWith("east") Then
            sTmp = Left(sTmp, 1) & Mid(sTmp, 5)
        ElseIf sTmp.StartsWith("north") OrElse sTmp.StartsWith("south") Then
            sTmp = Left(sTmp, 1) & Mid(sTmp, 6)
        End If
        Return Left(sTmp, 5)
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
                            xEl = DirectCast(xDoc.ReadNode(xRdr), XmlElement)
                            If GetTag(xEl, "type") <> "boundary" Then
                                xRdr.Read()
                                Continue While
                            End If
                            sBndType = GetTag(xEl, "boundary")
                            If BoundaryItem.CanHandleBoundaryType(sBndType) Then
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
            MsgBox($"Unknown designation {sTmp} in relation {bi.OSMRelation} ({bi.Name})")
        End If
        sCouncilName = GetTag(xEl, "council_name:en")
        If Len(sCouncilName) = 0 Then sCouncilName = GetTag(xEl, "council_name")
        ' try welsh, cornish and scots gaelic to get second name value
        sCouncilNameWelsh = GetTag(xEl, "council_name:cy")
        If sCouncilNameWelsh = "" Then
            sCouncilNameWelsh = GetTag(xEl, "council_name:kw")
        End If
        If sCouncilNameWelsh = "" Then
            sCouncilNameWelsh = GetTag(xEl, "council_name:gd")
        End If
        ptParishType = BoundaryItem.ParishType_FromString(GetTag(xEl, "parish_type"))
        csStyle = BoundaryItem.CouncilStyle_FromString(GetTag(xEl, "council_style"))
        sGSS = GetTag(xEl, "ref:gss")

        If Len(sGSS) = 8 Then bi.ONSCode = sGSS
        'If btType <> BoundaryItem.BoundaryTypes.BT_Unknown Then bi.BoundaryType = btType
        'If Len(sCouncilName) > 0 Then bi.CouncilName = sCouncilName
        'If Len(sCouncilNameWelsh) > 0 Then bi.CouncilName2 = sCouncilNameWelsh
        If btType = BoundaryItem.BoundaryTypes.BT_CivilParish Then
            'bi.ParishType = ptParishType
        End If
        If Len(bi.Website) = 0 Then
            sTmp = GetTag(xEl, "website")
            If sTmp = "" Then sTmp = GetTag(xEl, "url")
            bi.Website = sTmp
        End If
        'If csStyle <> BoundaryItem.CouncilStyles.CS_Default Then bi.CouncilStyle = csStyle
        bi.UpdateXML()
    End Sub
    Private Function GetTag(xElement As XmlElement, sKey As String) As String
        Dim xTag As XmlNode
        xTag = xElement.SelectSingleNode("tag[@k='" & sKey & "']")
        If xTag Is Nothing Then Return ""
        Return DirectCast(xTag, XmlElement).GetAttribute("v")
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
        Public Property Name2 As String
        Public Enum BoundaryTypes
            ''' <summary>
            ''' Unknown Boundary Type
            ''' </summary>
            BT_Unknown
            ''' <summary>
            ''' Country - probably Great Britain or United Kingdom
            ''' </summary>
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
            BT_HistoricCounty
            BT_ViceCounty
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
            CS_CountyBorough
            CS_Community
            CS_Parish
            CS_Village
            CS_Neighbourhood
        End Enum
        Public Enum ParishTypes
            PT_ParishCouncil
            PT_ParishMeeting
            PT_JointParishCouncil
            PT_JointParishMeeting
            PT_LandsCommon
            PT_CommunityCouncil
            PT_JointCommunityCouncil
            PT_DetachedArea
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
        Public Property CouncilName As String
        Public Property CouncilName2 As String
        Public Property ParishType As ParishTypes
        Public Property CouncilStyle As CouncilStyles
        Public Property Lat As Double
        Public Property Lon As Double
        Public Property Website As String
        Public Property Prefix As String
        Public Property IsDeleted As Boolean
        ' election cycles
        ''' <summary>
        ''' Number of years in an electoral cycle
        ''' </summary>
        ''' <returns>Usually 4</returns>
        Public Property ElectionCycle As Integer
        ''' <summary>
        ''' Fraction of the council which is elected at each election
        ''' </summary>
        ''' <returns>1 (whole council),2 (by halves),3 (by thirds)</returns>
        Public Property ElectionFraction As Integer
        ''' <summary>
        ''' Value of (year MOD ElectionCycle) when (first) election is held
        ''' </summary>
        ''' <returns>0..(ElectionCycle -1)</returns>
        Public Property ElectionCycleStartMod As Integer
        Private _BoundaryType As BoundaryTypes
        Private Shared _mapBTString As Dictionary(Of BoundaryTypes, String)
        Private Shared _mapStringBT As Dictionary(Of String, BoundaryTypes)
        Private Shared _mapBTtoGSSPrefix As Dictionary(Of BoundaryTypes, String)

        Shared Sub New()
            _mapBTString = New Dictionary(Of BoundaryTypes, String) From {
                {BoundaryTypes.BT_Region, "RGN"},
                {BoundaryTypes.BT_Unitary, "UA"},
                {BoundaryTypes.BT_CivilParish, "PAR"},
                {BoundaryTypes.BT_NonMetroCounty, "CTY"},
                {BoundaryTypes.BT_NonMetroDistrict, "NMD"},
                {BoundaryTypes.BT_MetroCounty, "MCTY"},
                {BoundaryTypes.BT_MetroDistrict, "MD"},
                {BoundaryTypes.BT_Nation, "CTRY"},
                {BoundaryTypes.BT_CeremonialCounty, "CCTY"},
                {BoundaryTypes.BT_SuiGeneris, "SGEN"},
                {BoundaryTypes.BT_Liberty, "LBTY"},
                {BoundaryTypes.BT_LondonBorough, "LONB"},
                {BoundaryTypes.BT_PreservedCounty, "PCTY"},
                {BoundaryTypes.BT_PrincipalArea, "WPA"},
                {BoundaryTypes.BT_ScotCouncil, "SCA"},
                {BoundaryTypes.BT_Country, "UK"},
                {BoundaryTypes.BT_Community, "COM"},
                {BoundaryTypes.BT_NIreDistrict, "NID"},
                {BoundaryTypes.BT_ParishGroup, "PGRP"},
                {BoundaryTypes.BT_HistoricCounty, "HCTY"},
                {BoundaryTypes.BT_ViceCounty, "VCTY"}
            }
            _mapStringBT = New Dictionary(Of String, BoundaryTypes) From {
                {"region", BoundaryTypes.BT_Region},
                {"unitary", BoundaryTypes.BT_Unitary},
                {"civil_parish", BoundaryTypes.BT_CivilParish},
                {"adm_county", BoundaryTypes.BT_NonMetroCounty},
                {"non_metro_district", BoundaryTypes.BT_NonMetroDistrict},
                {"metro_county", BoundaryTypes.BT_MetroCounty},
                {"metro_district", BoundaryTypes.BT_MetroDistrict},
                {"nation", BoundaryTypes.BT_Nation},
                {"ceremonial_county", BoundaryTypes.BT_CeremonialCounty},
                {"sui_generis", BoundaryTypes.BT_SuiGeneris},
                {"liberty", BoundaryTypes.BT_Liberty},
                {"london_borough", BoundaryTypes.BT_LondonBorough},
                {"preserved_county", BoundaryTypes.BT_PreservedCounty},
                {"wales_district", BoundaryTypes.BT_PrincipalArea},
                {"scotland_district", BoundaryTypes.BT_ScotCouncil},
                {"country", BoundaryTypes.BT_Country},
                {"community", BoundaryTypes.BT_Community},
                {"n_ireland_district", BoundaryTypes.BT_NIreDistrict},
                {"parish_group", BoundaryTypes.BT_ParishGroup},
                {"historic_county", BoundaryTypes.BT_HistoricCounty},
                {"vice_county", BoundaryTypes.BT_ViceCounty}
            }
            _mapBTtoGSSPrefix = New Dictionary(Of BoundaryTypes, String) From {
                {BoundaryTypes.BT_Region, "E12"},
                {BoundaryTypes.BT_Unitary, "E06"},
                {BoundaryTypes.BT_CivilParish, "E04"},
                {BoundaryTypes.BT_NonMetroCounty, "E10"},
                {BoundaryTypes.BT_NonMetroDistrict, "E07"},
                {BoundaryTypes.BT_MetroCounty, "E11"},
                {BoundaryTypes.BT_MetroDistrict, "E08"},
                {BoundaryTypes.BT_LondonBorough, "E09"},
                {BoundaryTypes.BT_PrincipalArea, "W06"},
                {BoundaryTypes.BT_ScotCouncil, "S12"},
                {BoundaryTypes.BT_Community, "W04"},
                {BoundaryTypes.BT_NIreDistrict, "N09"}
            }
        End Sub
        Public Sub New()

        End Sub
        Public Property BoundaryType As BoundaryTypes
            Get
                Return _BoundaryType
            End Get
            Set(value As BoundaryTypes)
                _BoundaryType = value
                _btcode = _mapBTString(value)
            End Set
        End Property
        Private _ONSCode As String
        Public Property ONSCode As String
            Get
                Return _ONSCode
            End Get
            Set(value As String)
                If _ONSCode <> value Then
                    _ONSCode = value
                    If _bdb IsNot Nothing Then _bdb.bChanges = True
                End If
            End Set
        End Property
        Public Shared Function CanHandleBoundaryType(sType As String) As Boolean
            Return sType = "administrative" OrElse sType = "ceremonial" OrElse sType = "historic" OrElse sType = "traditional"
        End Function
        Public ReadOnly Property GSSPrefix As String
            Get
                Return GSSPrefixForBoundaryType(_BoundaryType)
            End Get
        End Property
        Private _LandsCommonIDs As New List(Of String)
        ''' <summary>
        ''' For a civil parish, returns the LCP items if any which are part of this parish
        ''' </summary>
        ''' <returns>List(Of BoundaryItem)</returns>
        Public ReadOnly Property LandsCommon As List(Of BoundaryItem)
            Get
                Dim a As New List(Of BoundaryItem)
                For Each s In _LandsCommonIDs
                    If _bdb.Items.ContainsKey(s) Then
                        Dim i = _bdb.Items(s)
                        If i.BoundaryType = BoundaryTypes.BT_CivilParish _
                            AndAlso i.ParishType = ParishTypes.PT_LandsCommon Then
                            a.Add(i)
                        End If
                    End If
                Next
                Return a
            End Get
        End Property
        Public Sub AddLandsCommon(LCP As BoundaryItem)
            If Not _LandsCommonIDs.Contains(LCP.ONSCode) Then _LandsCommonIDs.Add(LCP.ONSCode)
        End Sub
        Public Sub RemoveLandsCommon(LCP As BoundaryItem)
            If _LandsCommonIDs.Contains(LCP.ONSCode) Then _LandsCommonIDs.Remove(LCP.ONSCode)
        End Sub
        Public Sub SetLandsCommon(LCPs As List(Of String))
            _LandsCommonIDs.Clear()
            For Each s In LCPs
                _LandsCommonIDs.Add(s)
            Next
        End Sub
        Public Sub SetLandsCommon(LCPs As List(Of BoundaryItem))
            _LandsCommonIDs.Clear()
            For Each i In LCPs
                _LandsCommonIDs.Add(i.ONSCode)
            Next
        End Sub
        Private _DetachedAreaIDs As New List(Of String)
        ''' <summary>
        ''' For a civil parish, returns the DET items if any which are part of this parish
        ''' </summary>
        ''' <returns>List(Of BoundaryItem)</returns>
        Public ReadOnly Property DetachedAreas As List(Of BoundaryItem)
            Get
                Dim a As New List(Of BoundaryItem)
                For Each s In _DetachedAreaIDs
                    If _bdb.Items.ContainsKey(s) Then
                        Dim i = _bdb.Items(s)
                        If i.BoundaryType = BoundaryTypes.BT_CivilParish _
                            AndAlso i.ParishType = ParishTypes.PT_DetachedArea Then
                            a.Add(i)
                        End If
                    End If
                Next
                Return a
            End Get
        End Property
        Public Sub AddDetachedArea(Det As BoundaryItem)
            If Not _DetachedAreaIDs.Contains(Det.ONSCode) Then _DetachedAreaIDs.Add(Det.ONSCode)
        End Sub
        Public Sub RemoveDetachedArea(Det As BoundaryItem)
            If _DetachedAreaIDs.Contains(Det.ONSCode) Then _DetachedAreaIDs.Remove(Det.ONSCode)
        End Sub
        Public Sub SetDetachedAreas(Dets As List(Of String))
            _DetachedAreaIDs.Clear()
            For Each s In Dets
                _DetachedAreaIDs.Add(s)
            Next
        End Sub
        Public Sub SetDetachedAreas(Dets As List(Of BoundaryItem))
            _DetachedAreaIDs.Clear()
            For Each i In Dets
                _DetachedAreaIDs.Add(i.ONSCode)
            Next
        End Sub
        Public ParentCode As String
        Public OSMRelation As Long
        Public AdminLevel As Integer
        Public NormName As String
        Public IsBorough As Boolean
        Public IsRoyal As Boolean
        Public IsCity As Boolean
        Public Notes As String
        Private _Parent As BoundaryItem
        Private _btcode As String
        Private _Children As New List(Of BoundaryItem)
        Private ReadOnly Property DeepCount As Integer
        Private ReadOnly Property DeepCountOSM As Integer

        Public Sub New(bdb As BoundaryDB)
            Me.New()
            _bdb = bdb
        End Sub
        Public Sub New(bdb As BoundaryDB, xBnd As XmlElement)
            Me.New()
            _bdb = bdb
            LoadFromXML(xBnd)
        End Sub
        Public Shadows ReadOnly Property ToString As String
            Get
                Return Name
            End Get
        End Property
        Public Property Parent As BoundaryItem
            Get
                If _bdb._root Is _Parent Then Return Nothing
                Return _Parent
            End Get
            Set(value As BoundaryItem)
                If Not IsNothing(_Parent) Then
                    _Parent.Children.Remove(Me)
                End If
                If Not IsNothing(value) Then
                    value.Children.Add(Me)
                End If
                _Parent = value
            End Set
        End Property
        Public ReadOnly Property Children() As List(Of BoundaryItem)
            Get
                Return _Children
            End Get
        End Property

        Public ReadOnly Property NumChildren() As Integer
            Get
                Dim count As Integer = 0
                For Each i In Children
                    If i.IsDeleted Then Continue For
                    If i.BoundaryType <> BoundaryTypes.BT_ParishGroup Then count += 1
                    count += i.NumChildren
                Next
                Return count
            End Get
        End Property
        Public ReadOnly Property NumKnownChildren() As Integer
            Get
                Dim count As Integer = 0
                For Each i In Children
                    If i.IsDeleted Then Continue For
                    If i.OSMRelation > 0 Then count += 1
                    count += i.NumKnownChildren
                Next
                Return count
            End Get
        End Property
        Public ReadOnly Property HasElections As Boolean
            Get
                Select Case BoundaryType
                    Case BoundaryTypes.BT_CivilParish,
                         BoundaryTypes.BT_Community,
                         BoundaryTypes.BT_LondonBorough,
                         BoundaryTypes.BT_MetroDistrict,
                         BoundaryTypes.BT_NIreDistrict,
                         BoundaryTypes.BT_NonMetroCounty,
                         BoundaryTypes.BT_NonMetroDistrict,
                         BoundaryTypes.BT_ParishGroup,
                         BoundaryTypes.BT_PrincipalArea,
                         BoundaryTypes.BT_ScotCouncil,
                         BoundaryTypes.BT_SuiGeneris
                    Case Else
                        Return False
                End Select
                If BoundaryType = BoundaryTypes.BT_CivilParish OrElse BoundaryType = BoundaryTypes.BT_ParishGroup Then
                    If ParishType = ParishTypes.PT_ParishCouncil OrElse ParishType = ParishTypes.PT_CommunityCouncil Then
                        Return True
                    End If
                    Return False
                End If
                Return True
            End Get
        End Property
        Public ReadOnly Property NextElection As Date
            Get
                ' start of current cycle
                Dim iYear As Integer = NextElectionCycleYear - ElectionCycle
                Return FirstThursdayInMay(Now().Year)
            End Get
        End Property
        Public Shared ReadOnly Property FirstThursdayInMay(iYear As Integer) As Date
            Get
                Dim iDate As Date = New Date(iYear, 5, 1) ' 1st of may
                Select Case iDate.DayOfWeek
                    Case DayOfWeek.Friday
                        iDate = iDate.AddDays(6)
                    Case DayOfWeek.Saturday
                        iDate = iDate.AddDays(5)
                    Case DayOfWeek.Sunday
                        iDate = iDate.AddDays(4)
                    Case DayOfWeek.Monday
                        iDate = iDate.AddDays(3)
                    Case DayOfWeek.Tuesday
                        iDate = iDate.AddDays(2)
                    Case DayOfWeek.Wednesday
                        iDate = iDate.AddDays(1)
                End Select
                Return iDate
            End Get
        End Property
        Public ReadOnly Property NextElectionCycleYear As Integer
            Get
                If ElectionCycle = 0 Then Return 0
                Dim iDate As Date = Now()
                Dim iYear As Integer = iDate.Year
                ' if we are past e-day, add a year
                If iDate > FirstThursdayInMay(iYear) Then iYear += 1
                ' roll forward to next cycle year
                Dim iCycleYear As Integer = (iYear Mod ElectionCycle) + ElectionCycle

                Return iYear + ElectionCycle - iCycleYear + ElectionCycleStartMod
            End Get
        End Property
        Public ReadOnly Property TypeCode As String
            Get
                Return _btcode
            End Get
        End Property
        Public Function LoadFromXML(xBnd As XmlElement) As Boolean
            Dim sTmp As String
            IsDeleted = (xBnd.GetAttribute(DeletedAttribute) = "1")
            Name = NodeText(xBnd.SelectSingleNode("name"))
            ' temp code
            If InStr(Name, "**delete**") > 0 Then
                IsDeleted = True
                Name = Trim(Replace(Name, "**delete**", ""))
            End If
            Name2 = NodeText(xBnd.SelectSingleNode("name2"))
            ONSCode = NodeText(xBnd.SelectSingleNode("newcode"))
            If Len(ONSCode) = 0 Then
                ONSCode = NodeText(xBnd.SelectSingleNode("ons"))
            End If
            CouncilName = NodeText(xBnd.SelectSingleNode("council_name"))
            CouncilName2 = NodeText(xBnd.SelectSingleNode("council_name2"))
            sTmp = NodeText(xBnd.SelectSingleNode("type"))
            If _mapStringBT.ContainsKey(sTmp) Then
                BoundaryType = _mapStringBT(sTmp)
            Else
                BoundaryType = BoundaryTypes.BT_Nation
                _btcode = "?"
            End If

            If BoundaryType = BoundaryTypes.BT_CivilParish OrElse BoundaryType = BoundaryTypes.BT_Community Then
                ParishType = ParishType_FromString(NodeText(xBnd.SelectSingleNode("parish_type")))
            Else
                ParishType = ParishTypes.PT_NA
            End If

            If BoundaryType = BoundaryTypes.BT_Community Then
                If ONSCode.StartsWith("W04") AndAlso ParishType = ParishTypes.PT_ParishCouncil Then
                    BoundaryType = BoundaryTypes.BT_Community
                End If
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
            Prefix = NodeText(xBnd.SelectSingleNode("prefix"))
            If Prefix = "" Then
                Prefix = MakePrefix(NormName)
            End If
            sTmp = NodeText(xBnd.SelectSingleNode("admin_level"))
            If Integer.TryParse(sTmp, AdminLevel) Then
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
            Double.TryParse(NodeText(xBnd.SelectSingleNode("lat")), Lat)
            Double.TryParse(NodeText(xBnd.SelectSingleNode("lon")), Lon)
            Website = NodeText(xBnd.SelectSingleNode("website"))
            ' Electoral cycle
            sTmp = NodeText(xBnd.SelectSingleNode("election_cycle"))
            If Integer.TryParse(sTmp, ElectionCycle) Then
                If ElectionCycle < 0 Or ElectionCycle > 4 Then ElectionCycle = 4
            Else
                ElectionCycle = 4
            End If
            sTmp = NodeText(xBnd.SelectSingleNode("election_fraction"))
            If Integer.TryParse(sTmp, ElectionFraction) Then
                If ElectionFraction < 1 Or ElectionFraction > 3 Then ElectionFraction = 1
            Else
                ElectionFraction = 1
            End If
            sTmp = NodeText(xBnd.SelectSingleNode("election_mod"))
            If Integer.TryParse(sTmp, ElectionCycleStartMod) Then
                If ElectionCycleStartMod < 0 Or ElectionCycleStartMod > (ElectionCycle - 1) Then ElectionCycleStartMod = 0
            Else
                ElectionCycleStartMod = 0
            End If

            If BoundaryType = BoundaryTypes.BT_CivilParish Then
                For Each x In xBnd.SelectNodes("lands_common")
                    sTmp = DirectCast(x, XmlElement).GetAttribute("gss")
                    If Len(sTmp) > 0 Then
                        _LandsCommonIDs.Add(sTmp)
                    End If
                Next
                For Each x In xBnd.SelectNodes("detached_area")
                    sTmp = DirectCast(x, XmlElement).GetAttribute("gss")
                    If Len(sTmp) > 0 Then
                        _DetachedAreaIDs.Add(sTmp)
                    End If
                Next
            End If
            _xNode = xBnd
            Return True
        End Function
        Public Shared Function GSSPrefixForBoundaryType(bt As BoundaryTypes) As String
            If _mapBTtoGSSPrefix.ContainsKey(bt) Then Return _mapBTtoGSSPrefix(bt)
            Return ""
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
                Case "historic_county" : BoundaryType = BoundaryTypes.BT_HistoricCounty
                Case "vice_county" : BoundaryType = BoundaryTypes.BT_ViceCounty
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
                Case CouncilStyles.CS_CountyBorough : sTmp = "county_borough"
                Case CouncilStyles.CS_Community : sTmp = "community"
                Case CouncilStyles.CS_Parish : sTmp = "parish"
                Case CouncilStyles.CS_Neighbourhood : sTmp = "neighbourhood"
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
                Case "county_borough"
                    xRet = CouncilStyles.CS_CountyBorough
                Case "village"
                    xRet = CouncilStyles.CS_Village
                Case "neighbourhood"
                    xRet = CouncilStyles.CS_Neighbourhood
                Case "parish"
                    xRet = CouncilStyles.CS_Parish
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
                Case BoundaryTypes.BT_HistoricCounty : sTmp = "historic_county"
                Case BoundaryTypes.BT_ViceCounty : sTmp = "vice_county"
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
                Case "historic_county" : BoundaryType = BoundaryTypes.BT_HistoricCounty
                Case "vice_county" : BoundaryType = BoundaryTypes.BT_ViceCounty
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
                Case ParishTypes.PT_JointParishMeeting : sTmp = "joint_parish_meeting"
                Case ParishTypes.PT_LandsCommon : sTmp = "lands_common"
                Case ParishTypes.PT_CommunityCouncil : sTmp = "community_council"
                Case ParishTypes.PT_JointCommunityCouncil : sTmp = "joint_community_council"
                Case ParishTypes.PT_DetachedArea : sTmp = "detached_area"
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
                Case "joint_parish_meeting"
                    xRet = ParishTypes.PT_JointParishMeeting
                Case "lands_common"
                    xRet = ParishTypes.PT_LandsCommon
                Case "community_council"
                    xRet = ParishTypes.PT_CommunityCouncil
                Case "joint_community_council"
                    xRet = ParishTypes.PT_JointCommunityCouncil
                Case "detached_area"
                    xRet = ParishTypes.PT_DetachedArea
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
        ' move children of donor node to become children of this node
        Public Sub MergeFrom(xFrom As BoundaryItem)
            If xFrom.Children.Count = 0 Then Return
            Dim xChildren(xFrom.Children.Count - 1) As BoundaryItem
            xFrom.Children.CopyTo(xChildren)
            For Each x In xChildren
                x.Parent = Me
                x.ParentCode = ParentCode
                UpdateXML()
            Next
        End Sub
        Public Function Edit(Optional Retriever As OSMRetriever = Nothing) As Boolean
            Dim f As New frmEdit
            f.xDB = _bdb
            f.Retriever = Retriever
            f.xItem = Me
            f.GssPrefix = Parent.Prefix
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
            If IsDeleted Then _xNode.SetAttribute(DeletedAttribute, "1") Else _xNode.RemoveAttribute(DeletedAttribute)
            SetValue(_xNode, "name", Name)
            SetValue(_xNode, "name2", Name2)
            SetValue(_xNode, "par_new", Parent.ONSCode)
            SetValue(_xNode, "par_name", Parent.Name)
            SetValue(_xNode, "par_ons", Nothing)
            SetValue(_xNode, "council_name", CouncilName)
            SetValue(_xNode, "council_name2", CouncilName2)
            SetIDinXML()
            SetValue(_xNode, "newcode", ONSCode)
            SetValue(_xNode, "type", BoundaryType_ToString(BoundaryType))
            SetValue(_xNode, "council_style", CouncilStyle_ToString(CouncilStyle))
            SetValue(_xNode, "parish_type", If(BoundaryType = BoundaryTypes.BT_CivilParish OrElse BoundaryType = BoundaryTypes.BT_Community, ParishType_ToString(ParishType), ""))
            SetValue(_xNode, "norm_name", NormName)
            SetValue(_xNode, "prefix", Prefix)
            SetValue(_xNode, "is_borough", If(IsBorough, "1", "0"))
            SetValue(_xNode, "is_royal", If(IsRoyal, "1", "0"))
            SetValue(_xNode, "is_city", If(IsCity, "1", "0"))
            SetValue(_xNode, "lat", Lat.ToString)
            SetValue(_xNode, "lon", Lon.ToString)
            SetValue(_xNode, "website", Website)
            SetValue(_xNode, "election_cycle", ElectionCycle.ToString)
            SetValue(_xNode, "election_fraction", ElectionFraction.ToString)
            SetValue(_xNode, "election_mod", ElectionCycleStartMod.ToString)
            SetValue(_xNode, "notes", Notes)
            UpdateLCPs(_xNode)
            UpdateDetachedAreas(_xNode)
        End Sub
        Private Sub UpdateLCPs(_xNode As XmlElement)
            ' if list of lcps has changed
            ' get list of what is in the xml
            Dim xList As New List(Of String)
            For Each x In _xNode.SelectNodes("lands_common")
                xList.Add(DirectCast(x, XmlElement).GetAttribute("gss"))
            Next
            ' remove items in our list
            For Each s In _LandsCommonIDs
                If xList.Contains(s) Then
                    xList.Remove(s)
                Else
                    Dim xChild = _xNode.OwnerDocument.CreateElement("lands_common")
                    DirectCast(xChild, XmlElement).SetAttribute("gss", s)
                    _xNode.AppendChild(xChild)
                    _bdb.bChanges = True
                End If
            Next
            ' if there are any left in the list from the xml, they need removing from the xml
            For Each s In xList
                Dim xChild = _xNode.SelectSingleNode($"lands_common[@gss='{s}']")
                If xChild IsNot Nothing Then
                    _xNode.RemoveChild(xChild)
                    _bdb.bChanges = True
                End If
            Next
        End Sub
        Private Sub UpdateDetachedAreas(_xNode As XmlElement)
            ' if list of lcps has changed
            ' get list of what is in the xml
            Dim xList As New List(Of String)
            For Each x In _xNode.SelectNodes("detached_area")
                xList.Add(DirectCast(x, XmlElement).GetAttribute("gss"))
            Next
            ' remove items in our list
            For Each s In _DetachedAreaIDs
                If xList.Contains(s) Then
                    xList.Remove(s)
                Else
                    Dim xChild = _xNode.OwnerDocument.CreateElement("detached_area")
                    DirectCast(xChild, XmlElement).SetAttribute("gss", s)
                    _xNode.AppendChild(xChild)
                    _bdb.bChanges = True
                End If
            Next
            ' if there are any left in the list from the xml, they need removing from the xml
            For Each s In xList
                Dim xChild = _xNode.SelectSingleNode($"detached_area[@gss='{s}']")
                If xChild IsNot Nothing Then
                    _xNode.RemoveChild(xChild)
                    _bdb.bChanges = True
                End If
            Next
        End Sub
        Private Sub SetValue(xNode As XmlElement, sKey As String, sValue As String)
            Dim xChild As XmlElement = DirectCast(xNode.SelectSingleNode(sKey), XmlElement)
            ' how to insert a missing element?
            If IsNothing(xChild) Then
                ' don't *add* element that doesn't already exist if the value is blank or boolean "false"
                If IsNothing(sValue) OrElse Len(sValue) = 0 Then Exit Sub
                If Left(sKey, 3) = "is_" AndAlso sValue = "0" Then Exit Sub
                xChild = xNode.OwnerDocument.CreateElement(sKey)
                xNode.AppendChild(xChild)
            End If
            If xChild.InnerText <> sValue Then
                If IsNothing(sValue) Then
                    xChild.ParentNode.RemoveChild(xChild)
                ElseIf sValue = "" Then
                    xChild.IsEmpty = True
                Else
                    xChild.InnerText = sValue
                End If
                _bdb.bChanges = True
            End If
        End Sub
        Public ReadOnly Property GroupMembers() As List(Of BoundaryItem)
            Get
                If Me.BoundaryType <> BoundaryItem.BoundaryTypes.BT_ParishGroup Then Return Nothing
                Dim a As New List(Of BoundaryItem)
                For Each x As BoundaryItem In _bdb.Items.Values
                    If (x.BoundaryType <> BoundaryTypes.BT_CivilParish) _
                        AndAlso (x.BoundaryType <> BoundaryTypes.BT_Community) _
                            Then Continue For
                    If (x.ParishType <> ParishTypes.PT_JointParishCouncil) _
                        AndAlso (x.ParishType <> ParishTypes.PT_JointParishMeeting) _
                        AndAlso (x.ParishType <> ParishTypes.PT_JointCommunityCouncil) _
                            Then Continue For
                    If x.CouncilName = CouncilName Then a.Add(x)
                Next
                Return a
            End Get
        End Property
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
        Dim sLevel As String = String.Empty
        Dim sName As String = String.Empty
        Dim sNorm As String = String.Empty
        Dim sType As String = String.Empty
        Dim sBType As String = String.Empty
        Dim sONS As String = String.Empty
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
                If Not BoundaryItem.CanHandleBoundaryType(sBType) Then
                    Continue For
                End If
                sName = xRel.Tag("name")
                sLevel = xRel.Tag("admin_level")
                If Integer.TryParse(sLevel, iLevel) Then
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
                        If pt <> BoundaryItem.ParishTypes.PT_ParishCouncil AndAlso pt <> xMatch.ParishType Then
                            xMatch.ParishType = pt
                            bchg = True
                        End If
                        If xRel.Tag("council_name") <> "" AndAlso xMatch.CouncilName <> xRel.Tag("council_name") Then
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
            If MsgBox("Save Changes?", MsgBoxStyle.OkCancel Or MsgBoxStyle.Question) = MsgBoxResult.Ok Then
                ' but now we have to persist the changes somehow...
                Save()
            End If
        End If
        Return True
    End Function
    Private Shared Function Normalise(sName As String) As String
        If sName = "RESET" Then
            Return "OK"
        End If
        Dim s As String = LCase(sName)
        For Each ns In NormaliseRegexps.Cast(Of NormStuff)
            s = ns.re.Replace(s, ns.repl)
        Next
        s = Replace(s, "  ", " ")
        Return Trim(s)
    End Function
    Private Function IsGSSType(t As BoundaryItem.BoundaryTypes) As Boolean
        Select Case t
            Case BoundaryItem.BoundaryTypes.BT_CivilParish,
                 BoundaryItem.BoundaryTypes.BT_Community,
                 BoundaryItem.BoundaryTypes.BT_LondonBorough,
                 BoundaryItem.BoundaryTypes.BT_MetroCounty,
                 BoundaryItem.BoundaryTypes.BT_MetroDistrict,
                 BoundaryItem.BoundaryTypes.BT_Nation,
                 BoundaryItem.BoundaryTypes.BT_NonMetroCounty,
                 BoundaryItem.BoundaryTypes.BT_NonMetroDistrict,
                 BoundaryItem.BoundaryTypes.BT_PrincipalArea,
                 BoundaryItem.BoundaryTypes.BT_ScotCouncil,
                 BoundaryItem.BoundaryTypes.BT_Unitary
                Return True
        End Select
        Return False
    End Function

    Private Structure latlongdata
        Dim GSS As String
        Dim Lat As Double
        Dim Lon As Double
        Dim Area As Double
    End Structure

    Public Function ImportLatLong(Path As String) As Boolean
        Dim tfp As Microsoft.VisualBasic.FileIO.TextFieldParser
        Dim fLat As Integer = -1, fLon As Integer = -1, fGSS As Integer = -1, fArea As Integer = -1
        Dim i As Integer
        Dim sFields() As String
        Dim g As List(Of BoundaryItem)
        Dim re As New System.Text.RegularExpressions.Regex("[a-z]+\d\dcd")

        tfp = New Microsoft.VisualBasic.FileIO.TextFieldParser(Path)

        tfp.Delimiters = {","}
        tfp.HasFieldsEnclosedInQuotes = True
        sFields = tfp.ReadFields()
        For i = 0 To sFields.Count - 1
            Select Case sFields(i)
                Case "lat"
                    fLat = i
                Case "long"
                    fLon = i
                Case "st_areashape"
                    fArea = i
                Case Else
                    If fGSS < 0 AndAlso re.IsMatch(sFields(i)) Then
                        fGSS = i
                    End If
            End Select
        Next
        If (fGSS * fLat * fLon * fArea) < 0 Then
            MsgBox($"Unable to find the required fields in Lat/Lon data, GSS={fGSS} Lat={fLat} Lon={fLon} Area={fArea}")
            tfp.Close()
            Return False
        End If

        Dim lld As New Dictionary(Of String, latlongdata)
        Dim x As latlongdata
        While Not tfp.EndOfData
            sFields = tfp.ReadFields()
            x = New latlongdata
            x.GSS = sFields(fGSS)
            x.Lat = Double.Parse(sFields(fLat))
            x.Lon = Double.Parse(sFields(fLon))
            x.Area = Double.Parse(sFields(fArea))
            lld(x.GSS) = x
        End While
        tfp.Close()

        For Each bi As BoundaryItem In Items.Values
            If IsGSSType(bi.BoundaryType) Then
                If lld.ContainsKey(bi.ONSCode) Then
                    x = lld(bi.ONSCode)
                    bi.Lat = x.Lat
                    bi.Lon = x.Lon
                    bi.UpdateXML()
                Else
                    ' MsgBox("Entity " & bi.ONSCode & " not found in latlong data")
                    Debug.Print($"Entity {bi.ONSCode} not found in latlong data")
                End If
            End If
        Next

        For Each bi As BoundaryItem In Items.Values
            If bi.BoundaryType = BoundaryItem.BoundaryTypes.BT_ParishGroup Then
                Dim baseLat As Double = 0.0
                Dim baseLon As Double = 0.0
                Dim totArea As Double = 0.0
                Dim totCoG As Double = 0.0
                Dim totLat As Double = 0.0
                Dim totLon As Double = 0.0
                Dim nMembers As Integer = 0
                g = bi.GroupMembers
                If g IsNot Nothing Then
                    For Each gp In g
                        If lld.ContainsKey(gp.ONSCode) Then
                            x = lld(gp.ONSCode)
                            If x.Lat < baseLat Then baseLat = x.Lat
                            If x.Lon < baseLon Then baseLon = x.Lon
                            totArea += x.Area
                            nMembers += 1
                        End If
                        If nMembers = 0 Then Continue For
                    Next
                    For Each gp In g
                        If lld.ContainsKey(gp.ONSCode) Then
                            x = lld(gp.ONSCode)
                            totLat += (x.Lat - baseLat) * x.Area
                            totLon += (x.Lon - baseLon) * x.Area
                        End If
                        bi.Lat = (totLat / totArea) + baseLat
                        bi.Lon = (totLon / totArea) + baseLon
                        bi.UpdateXML()
                    Next
                End If
            End If
        Next

        If bChanges Then
            Save()
        End If
        Return True
    End Function

    Public Function ImportElectoralCycles(Path As String) As Boolean
        Dim tfp As Microsoft.VisualBasic.FileIO.TextFieldParser
        Dim fLat As Integer = -1, fLon As Integer = -1, fGSS As Integer = -1, fArea As Integer = -1
        Dim i As Integer
        Dim sFields() As String
        Dim g As List(Of BoundaryItem)
        Dim re As New System.Text.RegularExpressions.Regex("[a-z]+\d\dcd")

        tfp = New Microsoft.VisualBasic.FileIO.TextFieldParser(Path)

        tfp.Delimiters = {","}
        tfp.HasFieldsEnclosedInQuotes = True
        sFields = tfp.ReadFields()
        For i = 0 To sFields.Count - 1
            Select Case sFields(i)
                Case "lat"
                    fLat = i
                Case "long"
                    fLon = i
                Case "st_areashape"
                    fArea = i
                Case Else
                    If fGSS < 0 AndAlso re.IsMatch(sFields(i)) Then
                        fGSS = i
                    End If
            End Select
        Next
        If (fGSS * fLat * fLon * fArea) < 0 Then
            MsgBox($"Unable to find the required fields in Lat/Lon data, GSS={fGSS} Lat={fLat} Lon={fLon} Area={fArea}")
            tfp.Close()
            Return False
        End If

        Dim lld As New Dictionary(Of String, latlongdata)
        Dim x As latlongdata
        While Not tfp.EndOfData
            sFields = tfp.ReadFields()
            x = New latlongdata
            x.GSS = sFields(fGSS)
            x.Lat = Double.Parse(sFields(fLat))
            x.Lon = Double.Parse(sFields(fLon))
            x.Area = Double.Parse(sFields(fArea))
            lld(x.GSS) = x
        End While
        tfp.Close()

        For Each bi As BoundaryItem In Items.Values
            If IsGSSType(bi.BoundaryType) Then
                If lld.ContainsKey(bi.ONSCode) Then
                    x = lld(bi.ONSCode)
                    bi.Lat = x.Lat
                    bi.Lon = x.Lon
                    bi.UpdateXML()
                Else
                    ' MsgBox("Entity " & bi.ONSCode & " not found in latlong data")
                    Debug.Print($"Entity {bi.ONSCode} not found in latlong data")
                End If
            End If
        Next

        For Each bi As BoundaryItem In Items.Values
            If bi.BoundaryType = BoundaryItem.BoundaryTypes.BT_ParishGroup Then
                Dim baseLat As Double = 0.0
                Dim baseLon As Double = 0.0
                Dim totArea As Double = 0.0
                Dim totCoG As Double = 0.0
                Dim totLat As Double = 0.0
                Dim totLon As Double = 0.0
                Dim nMembers As Integer = 0
                g = bi.GroupMembers
                If g IsNot Nothing Then
                    For Each gp In g
                        If lld.ContainsKey(gp.ONSCode) Then
                            x = lld(gp.ONSCode)
                            If x.Lat < baseLat Then baseLat = x.Lat
                            If x.Lon < baseLon Then baseLon = x.Lon
                            totArea += x.Area
                            nMembers += 1
                        End If
                        If nMembers = 0 Then Continue For
                    Next
                    For Each gp In g
                        If lld.ContainsKey(gp.ONSCode) Then
                            x = lld(gp.ONSCode)
                            totLat += (x.Lat - baseLat) * x.Area
                            totLon += (x.Lon - baseLon) * x.Area
                        End If
                        bi.Lat = (totLat / totArea) + baseLat
                        bi.Lon = (totLon / totArea) + baseLon
                        bi.UpdateXML()
                    Next
                End If
            End If
        Next

        If bChanges Then
            Save()
        End If
        Return True
    End Function

End Class
