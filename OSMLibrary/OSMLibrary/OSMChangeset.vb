Imports System.Xml

Public Class OSMChangeset
        Inherits OSMObject
        Private _Created As DateTime
        Private _Closed As DateTime
        Private _IsOpen As Boolean = True
        Friend _Comments As New List(Of OSMChangesetComment)
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(xNode As XmlNode)
            MyBase.New()
            LoadXML(xNode)
        End Sub
        Public Sub New(xNode As XmlNode, xDoc As OSMDoc)
            MyBase.New(xDoc)
            LoadXML(xNode)
        End Sub
        Public ReadOnly Property Comments As List(Of OSMChangesetComment)
            Get
                Return _Comments
            End Get
        End Property
        Public Sub LoadXML(xNode As XmlNode)
            MyBase.LoadGenericXML(xNode)
            __Bbox = New BBox()
            With __Bbox
                .MinLon = CDbl(xNode.Attributes("min_lon").InnerText)
                .MaxLon = CDbl(xNode.Attributes("max_lon").InnerText)
                .MinLat = CDbl(xNode.Attributes("min_lat").InnerText)
                .MaxLat = CDbl(xNode.Attributes("max_lat").InnerText)
            End With
            _IsOpen = (xNode.Attributes("open").InnerText <> "false")
            _Created = DateTime.Parse(xNode.Attributes("created_at").InnerText)
            _Closed = DateTime.Parse(xNode.Attributes("closed_at").InnerText)
            For Each x As XmlElement In xNode.SelectNodes("discussion/comment")
                Dim xCom As New OSMChangesetComment(Me)
                'date, user, UID
                With xCom
                    .Timestamp = Date.Parse(x.GetAttribute("date"))
                    .UID = CLng(x.GetAttribute("uid"))
                    .User = x.GetAttribute("user")
                    .Text = x.SelectSingleNode("text").InnerText
                End With
                _Comments.Add(xCom)
            Next
        End Sub
        Public ReadOnly Property IsOpen As Boolean
            Get
                Return _IsOpen
            End Get
        End Property
        Public ReadOnly Property ClosedAt As DateTime
            Get
                Return _Closed
            End Get
        End Property
        Public ReadOnly Property CreatedAt As DateTime
            Get
                Return _Created
            End Get
        End Property
        Public Overrides ReadOnly Property JSON() As String
            Get
                Return ""
            End Get
        End Property
        Public Overrides ReadOnly Property GeoJSON() As String
            Get
                Return ""
            End Get
        End Property
        Public Overrides ReadOnly Property Bbox() As BBox
            Get
                Return __Bbox
            End Get
        End Property
        Public Overrides ReadOnly Property Centroid As DPoint
            Get
                Return New DPoint(0.0, 0.0)
            End Get
        End Property
        Public Overrides Sub SerializeMe(x As XmlWriter)
            x.WriteAttributeString("created_at", CreatedAt.ToUniversalTime.ToString("o"))
            x.WriteAttributeString("closed_at", ClosedAt.ToUniversalTime.ToString("o"))
            x.WriteAttributeString("open", DirectCast(IIf(_IsOpen, "true", "false"), String))
            If __Bbox IsNot Nothing Then
                x.WriteAttributeString("min_lat", __Bbox.MinLat.ToString)
                x.WriteAttributeString("min_lat", __Bbox.MinLat.ToString)
                x.WriteAttributeString("min_lat", __Bbox.MinLat.ToString)
                x.WriteAttributeString("min_lat", __Bbox.MinLat.ToString)
            End If
            x.WriteAttributeString("comments_count", Comments.Count.ToString)
        End Sub
        Public Overrides Sub SerializeEnd(x As XmlWriter)
            If _Comments.Count > 0 Then
                x.WriteStartElement("discussion")
                For Each xCom As OSMChangesetComment In _Comments
                    xCom.Serialize(x)
                Next
                x.WriteEndElement()
            End If
        End Sub

    End Class

    Public Class OSMUpdateableChangeset
        Inherits OSMChangeset
        Public Function Open() As Boolean
            Dim xCS As XmlDocument
            Dim ms As New System.IO.MemoryStream
            Dim xWriter As New XmlTextWriter(ms, System.Text.Encoding.UTF8)

            With xWriter
                .Formatting = Formatting.Indented
                .Indentation = 4
                .WriteStartDocument(True)
                .WriteStartElement("osm")
                .WriteStartElement("changeset")



                .WriteEndElement()
                .WriteEndElement()
                .WriteEndDocument()
            End With

        End Function
        Public Sub Close()

        End Sub
        Public Function NewComment() As OSMChangesetComment
            Dim x As New OSMChangesetComment(Me)
            _Comments.Add(x)
            Return x
        End Function
    End Class

    Public Class OSMChangesetComment
        Public User As String
        Public UID As Long
        Public Timestamp As DateTime
        Private _Changeset As OSMChangeset
        Public Text As String

        Public Sub New(xChg As OSMChangeset)
            _Changeset = xChg
        End Sub
        Public ReadOnly Property Changeset As OSMChangeset
            Get
                Return _Changeset
            End Get
        End Property

        Public Sub Serialize(x As XmlWriter)
            x.WriteStartElement("comment")
            x.WriteAttributeString("date", Timestamp.ToUniversalTime.ToString("o"))
            x.WriteAttributeString("uid", UID.ToString)
            x.WriteAttributeString("user", User)
            x.WriteElementString("text", Text)
            x.WriteEndElement()
        End Sub
    End Class

