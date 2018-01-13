Imports System.Xml
Imports System.Drawing
Imports OSMLibrary

''' <summary>
''' Represents an OSM Node
''' </summary>
Public Class OSMNode
    Inherits OSMObject
    Public Lat As Double
    Public Lon As Double
    Public UsedByWays As New List(Of OSMWay)
    Public UsedByRelations As New List(Of OSMRelation)
    Public Overrides ReadOnly Property Type As ObjectType
        Get
            Return ObjectType.Node
        End Get
    End Property
    Public Overrides ReadOnly Property JSON As String
        Get
            Dim sJSON As New System.Text.StringBuilder, sTmp As String
            sJSON.Append("{ ")
            sTmp = MyBase.TagJSON
            If Len(sTmp) > 0 Then
                sJSON.Append(sTmp & ",")
            End If
            sJSON.Append("""lat"":" & CStr(Lat) & ",""lon"":" & CStr(Lon) & " }")
            Return sJSON.ToString
        End Get
    End Property
    Public Overrides Function Clone() As OSMObject
        Dim xNew As OSMNode = DirectCast(MyBase.Clone(), OSMNode)
        xNew.UsedByWays.AddRange(UsedByWays)
        xNew.UsedByRelations.AddRange(UsedByRelations)
        Return xNew
    End Function
    Public Overrides ReadOnly Property GeoJSON As String
        Get
            If IsPlaceholder Then
                Return ""
            End If
            Dim sJSON As New System.Text.StringBuilder
            '{ "type": "Point", "coordinates": [100.0, 0.0] }
            sJSON.Append("{ ""type"": ""Point"", ""coordinates"": " & GeoJSONCoords)
            If Tags.Count > 0 Then
                sJSON.Append(", ""properties"": " & TagGeoJSON)
            End If
            sJSON.Append("}")
            Return sJSON.ToString
        End Get
    End Property

    Public ReadOnly Property GeoJSONCoords As String
        Get
            Return "[" & CStr(Lon) & "," & CStr(Lat) & "]"
        End Get
    End Property
    Public Async Sub LoadXML(x As Xml.XmlNode)
        MyBase.LoadGenericXML(x)
        Try
            Lat = CDbl(x.Attributes("lat").InnerText)
            Lon = CDbl(x.Attributes("lon").InnerText)
        Catch
        End Try
        __Bbox = Nothing
    End Sub
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(x As Xml.XmlNode)
        MyBase.New()
        LoadXML(x)
    End Sub
    Public Sub New(x As Xml.XmlNode, Doc As OSMDoc)
        MyBase.New(Doc)
        LoadXML(x)
    End Sub
    Public Function ExtremelyCloseTo(n As OSMNode) As Boolean
        '        Return Math.Sqrt((n.Lat - Lat) ^ 2 + (n.Lon - Lon) ^ 2) < 0.01
        If n Is Nothing Then
            Return False
        End If
        'Return (n.Lat = Lat) And (n.Lon = Lon)
        ' Return (Me.ID = n.ID)
        ' is it within 2m?
        Return (DistanceTo(n) < 2.0)
    End Function
    Public Overrides ReadOnly Property Bbox As BBox
        Get
            If __Bbox Is Nothing Then
                __Bbox = MakeBBox(0.0)
            End If
            Return __Bbox
        End Get
    End Property
    Public Function MakeBBox(r As Double) As BBox
        Return New BBox With {
            .MinLon = Lon - r,
            .MinLat = Lat - r,
            .MaxLon = Lon + r,
            .MaxLat = Lat + r
        }
    End Function
    Public Overrides ReadOnly Property Centroid As DPoint
        Get
            Return New DPoint(Lon, Lat)
        End Get
    End Property
    Public Overrides Sub SerializeMe(x As XmlWriter)
    End Sub

    ''' <summary>
    ''' Returns the distance in metres from this node to the given node
    ''' </summary>
    ''' <param name="x"></param>
    ''' <returns></returns>
    Public ReadOnly Property DistanceTo(other As OSMNode) As Double
        Get
            Const R As Double = 6371000
            Dim dLat As Double = DegreeToRadian(other.Lat - Lat)
            Dim dLon As Double = DegreeToRadian(other.Lon - Lon)
            Dim lat1 As Double = DegreeToRadian(Lat)
            Dim lat2 As Double = DegreeToRadian(other.Lat)

            Dim a As Double = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2)
            Dim c As Double = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a))
            Return R * c
        End Get
    End Property

    ''' <summary>
    ''' Converts an angle in degrees to radians
    ''' </summary>
    ''' <param name="degree"></param>
    ''' <returns></returns>
    Public Shared Function DegreeToRadian(ByVal degree As Double) As Double
        Const x As Double = Math.PI / 180.0
        Return x * degree
    End Function
End Class
