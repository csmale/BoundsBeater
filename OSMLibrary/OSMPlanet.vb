Imports System.Xml

Public Class OSMPlanet
    Dim BaseURL As String
    Dim DownloadOnDemand As Boolean = True
    Dim RetrieveFullData As Boolean = True
    Private _Nodes As New Dictionary(Of Long, OSMNode)
    Private _Ways As New Dictionary(Of Long, OSMWay)
    Private _Relations As New Dictionary(Of Long, OSMRelation)
    Private _Users As New Dictionary(Of Long, OSMUser)

    Sub New()
        BaseURL = "http://api.openstreetmap.org/api/0.6/"
    End Sub
    Sub New(sURL As String)
        BaseURL = sURL
    End Sub
    Function AddFromXML(XML As XmlDocument) As Boolean
        Dim x As XmlElement
        Dim n As OSMNode
        Dim w As OSMWay
        Dim r As OSMRelation
        Dim ID As Long
        For Each x In XML.SelectNodes("/osm/node")
            ID = x.GetAttribute("id")
            If Not _Nodes.ContainsKey(ID) Then
                n = New OSMNode(x, Me)
                _Nodes.Add(ID, n)
            End If
        Next
        For Each x In XML.SelectNodes("/osm/way")
            ID = x.GetAttribute("id")
            If Not _Ways.ContainsKey(ID) Then
                w = New OSMWay(x, Me)
                _Ways.Add(ID, w)
            End If
        Next
        For Each x In XML.SelectNodes("/osm/relation")
            ID = x.GetAttribute("id")
            If Not _Relations.ContainsKey(ID) Then
                r = New OSMRelation(x, Me)
                _Relations.Add(ID, r)
            End If
        Next
        Return True
    End Function
    Function Node(ID As Long) As OSMNode
        If Not _Nodes.ContainsKey(ID) Then
            If DownloadOnDemand Then
                AddFromXML(APIGetObject(ID, "node"))
                If Not _Nodes.ContainsKey(ID) Then
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        End If
        Return _Nodes(ID)
    End Function
    Public ReadOnly Property Nodes As Dictionary(Of Long, OSMNode)
        Get
            Return _Nodes
        End Get
    End Property
    Function Way(ID As Long) As OSMWay
        If Not _Ways.ContainsKey(ID) Then
            If DownloadOnDemand Then
                AddFromXML(APIGetObject(ID, "way", RetrieveFullData))
                If Not _Ways.ContainsKey(ID) Then
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        End If
        Return _Ways(ID)
    End Function
    Public ReadOnly Property Ways As Dictionary(Of Long, OSMWay)
        Get
            Return _Ways
        End Get
    End Property
    Function Relation(ID As Long) As OSMRelation
        If Not _Relations.ContainsKey(ID) Then
            If DownloadOnDemand Then
                AddFromXML(APIGetObject(ID, "relation", RetrieveFullData))
                If Not _Relations.ContainsKey(ID) Then
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        End If
        Return _Relations(ID)
    End Function
    Public ReadOnly Property Relations As Dictionary(Of Long, OSMRelation)
        Get
            Return _Relations
        End Get
    End Property
    Public Function APIGetObject(ID As Long, ObjType As String, Optional FullData As Boolean = False) As XmlDocument
        If FullData Then
            Return APIGet(ID, ObjType, "full")
        Else
            Return APIGet(ID, ObjType)
        End If
    End Function
    Public Function APIGet(ID As Long, ObjType As String, Optional QueryType As String = "") As XmlDocument
        Dim x As New XmlDocument

        Dim url As String
        url = BaseURL & ObjType & "/" & CStr(ID)
        If Len(QueryType) > 0 Then
            url = url & "/" & QueryType
        End If
        x.Load(url)
        Return x
    End Function
End Class
