Imports System.Xml
Imports System.Drawing
Imports System.Text
Imports OSMLibrary

''' <summary>
''' Represents an OSM Way
''' </summary>
Public Class OSMWay
        Inherits OSMObject
        Public Nodes As New LinkedList(Of OSMNode)
        Public UsedByRelations As New List(Of OSMRelation)
        Public Overrides ReadOnly Property Type As ObjectType
            Get
                Return ObjectType.Way
            End Get
        End Property
        Public Overrides ReadOnly Property BBox As BBox
            Get
                If __Bbox Is Nothing Then
                    Dim bb As New BBox
                    Dim x As OSMNode
                    For Each x In Nodes
                        bb.Merge(x.Lat, x.Lon)
                    Next
                    __Bbox = bb
                End If
                Return __Bbox
            End Get
        End Property
        Public Overrides Function Clone() As OSMObject
            Dim xNew As OSMWay = DirectCast(MyBase.Clone(), OSMWay)

            Return xNew
        End Function
        Public Overrides ReadOnly Property JSON As String
            Get
                Dim sbJSON As New StringBuilder
                Dim bFirst As Boolean = True
                Dim n As OSMNode
                Dim wn As LinkedListNode(Of OSMNode)
                sbJSON.Append("{ ""points"" : [")
                wn = Nodes.First
                While wn IsNot Nothing
                    n = wn.Value
                    If Not bFirst Then sbJSON.Append(",")
                    sbJSON.Append(n.JSON)
                    bFirst = False
                    wn = wn.Next
                End While
                sbJSON.Append("] }")
                Return sbJSON.ToString
            End Get
        End Property
        Public Overrides ReadOnly Property GeoJSON As String
            Get
                Dim sJSON As New System.Text.StringBuilder
                Dim bFirst As Boolean = True
                '            If Nodes.Count = 0 Then
                '               Return ""
                '            End If
                Dim n As OSMNode
                Dim wn As LinkedListNode(Of OSMNode)
                sJSON.Append("{ ""type"": ""LineString"", ""coordinates"" : [")
                wn = Nodes.First
                While wn IsNot Nothing
                    n = wn.Value
                    If Not bFirst Then sJSON.Append(",")
                    sJSON.Append(n.GeoJSONCoords)
                    bFirst = False
                    wn = wn.Next
                End While
                sJSON.Append("]")
                If Tags.Count > 0 Then
                    sJSON.Append(", ""properties"": ")
                    sJSON.Append(TagGeoJSON)
                End If
                sJSON.Append("}")
                Return sJSON.ToString
            End Get
        End Property

        Public Sub LoadXML(x As XmlNode, xNodes As OSMCollection(Of OSMNode))
            MyBase.LoadGenericXML(x)
            Dim xRef As XmlNode
            Dim lRef As Long
            Dim xNode As OSMNode
            For Each xRef In x.SelectNodes("nd")
                lRef = CLng(xRef.Attributes("ref").InnerText)
                If Not IsNothing(xNodes) Then
                    If xNodes.Contains(lRef) Then
                        xNode = xNodes(lRef)
                    Else
                        xNode = New OSMNode ' creates a placeholder by default
                        xNode.ID = lRef
                        xNodes.Add(xNode.ID, xNode)
                    End If
                    xNode.UsedByWays.Add(Me)
                    Nodes.AddLast(xNode)
                End If
            Next
            __Bbox = Nothing
        End Sub
        Public Sub New(xWay As XmlNode, xNodes As OSMCollection(Of OSMNode))
            LoadXML(xWay, xNodes)
        End Sub
        Public Sub New(xWay As XmlNode)
            LoadXML(xWay, Nothing)
        End Sub

        ''' <summary>
        ''' Default constructor
        ''' </summary>
        Public Sub New()
            MyBase.New()
        End Sub

        ''' <summary>
        ''' Creates an OSMWay, consisting solely of the given node, within the given OSMDoc
        ''' </summary>
        ''' <param name="Node">The OSMNode to be inserted into the new way</param>
        ''' <param name="xDoc">The OSMDoc containing bothe the given node and the new way</param>
        Public Sub New(Node As Xml.XmlNode, xDoc As OSMDoc)
            MyBase.New(xDoc)
            LoadXML(Node, Doc.Nodes)
        End Sub
        Public Sub New(x As Xml.XmlNode, xNodes As OSMCollection(Of OSMNode), xDoc As OSMDoc)
            MyBase.New(xDoc)
            LoadXML(x, xNodes)
        End Sub

        Public Overrides ReadOnly Property Centroid As DPoint
            Get
                Dim bb As BBox = Me.BBox
                Return New DPoint((bb.MinLon + bb.MaxLon) / 2.0, (bb.MinLat + bb.MaxLat) / 2.0)
            End Get
        End Property
        Public Overrides Sub SerializeMe(x As XmlWriter)
            For Each n As OSMNode In Nodes
                x.WriteStartElement("nd")
                x.WriteAttributeString("ref", n.ID.ToString)
                x.WriteEndElement()
            Next
        End Sub
        Public ReadOnly Property Length() As Double
            Get
                Dim l As Double
                Dim n1 As LinkedListNode(Of OSMNode), n2 As LinkedListNode(Of OSMNode)
                If Nodes.Count < 2 Then
                    Return 0.0
                End If
                l = 0.0
                n1 = Nodes.First
                n2 = n1.Next
                While n2 IsNot Nothing
                    l += n1.Value.DistanceTo(n2.Value)
                    n1 = n2
                    n2 = n2.Next
                End While
                Return l
            End Get
        End Property
    End Class
