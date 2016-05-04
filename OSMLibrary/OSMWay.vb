Imports System.Xml

Public Class OSMWay
    Inherits OSMObject
    Private _Nodes As New LinkedList(Of OSMNode)

    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(XML As XmlElement, p As OSMPlanet)
        MyBase.New(XML, p)
        Dim n As OSMNode
        Dim nID As Long
        For Each x As XmlElement In XML.SelectNodes("nd")
            nID = Long.Parse(x.GetAttribute("ref"))
            n = Planet.Node(nID)
            If IsNothing(n) Then
                MsgBox("Way " & ID & " referenced undefined node " & nID)
            Else
                _Nodes.AddLast(n)
            End If
        Next
    End Sub
    Public ReadOnly Property Nodes As LinkedList(Of OSMNode)
        Get
            Return _Nodes
        End Get
    End Property

    Public Function SimplifiedNodes(Epsilon As Double) As LinkedList(Of OSMNode)
        Dim aNodes() As OSMNode = _Nodes.ToArray()
        ' e is Epsilon, max deviation in meters
        Dim e As Double = 10.0

        aNodes = DouglasPeucker(aNodes, e)

        Return New LinkedList(Of OSMNode)(aNodes)

    End Function
    Private Function DouglasPeucker(aNodes() As OSMNode, Epsilon As Double) As OSMNode()
        Dim dMax As Double, d As Double
        Dim Index As Integer
        Dim res1() As OSMNode
        Dim res2() As OSMNode
        Dim res() As OSMNode

        ' Find the point with the maximum distance
        dMax = 0
        Index = 0
        For i = 1 To (aNodes.Length - 2)
            d = Distance(aNodes(i), aNodes.First, aNodes.Last)
            If d > dMax Then
                Index = i
                dMax = d
            End If
        Next

        ' If max distance is greater than epsilon, recursively simplify
        If dMax > Epsilon Then
            ' Recursive call
            res1 = DouglasPeucker(aNodes.Take(Index + 1), Epsilon)
            res2 = DouglasPeucker(aNodes.Skip(Index), Epsilon)

            ' Build the result list
            res = res1.Take(res1.Length - 1).Concat(res2)
        Else
            res = aNodes
        End If

        ' Return the result
        Return res
    End Function

    Private Function PerpendicularDistance(a As OSMNode, l1 As OSMNode, l2 As OSMNode) As Double
        ' if start and end point are on the same x the distance is the difference in X.
        Dim result As Double, slope As Double, intercept As Double, latFactor As Double
        latFactor = 1.0
        If l1.Lon = l2.Lon Then
            result = Math.Abs(a.Lon - l1.Lon)
        Else
            slope = (l2.Lat - l1.Lat) / (l2.Lon - l1.Lon)
            intercept = l1.Lat - (slope * l1.Lon)
            result = Math.Abs(slope * a.Lon - a.Lat + intercept) / Math.Sqrt(Math.Pow(slope, 2) + 1)
        End If
        Return result
    End Function

    Public Function Distance(P As OSMNode, L1 As OSMNode, L2 As OSMNode) As Double
        Dim d13 = GreatCircleDistance(L1.Lat, L1.Lon, P.Lat, P.Lon)
        Dim R = 3961300.0
        Dim brng12 = Bearing(L1, L2)
        Dim brng13 = Bearing(L1, P)
        Dim dXt = Math.Asin(Math.Sin(d13 / R) * Math.Sin(brng13 - brng12)) * R
        Return dXt
    End Function

    ''' <summary>
    ''' Returns the great circle distance or Haversine distance in meters between the two submitted points. Don't forget to import System.Math!
    ''' From http://www.meridianworlddata.com/Distance-Calculation.asp
    ''' </summary>
    ''' <param name="Lat1">Latitude of Point 1</param>
    ''' <param name="Lon1">Longitude of Point 1</param>
    ''' <param name="Lat2">Latitude of Point 2</param>
    ''' <param name="Lon2">Longitude of Point 2</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GreatCircleDistance(ByVal Lat1 As Double, ByVal Lon1 As Double, ByVal Lat2 As Double, ByVal Lon2 As Double) As Double
        Dim Radius As Double = 6378137.0
        Dim x As Double = (Math.Sin(Lat1 / 57.29577951) * Math.Sin(Lat2 / 57.29577951)) +
            (Math.Cos(Lat1 / 57.29577951) * Math.Cos(Lat2 / 57.29577951) * (Math.Cos(Lon2 / 57.29577951 - Lon1 / 57.29577951)))
        Return Radius * Math.Atan(Math.Sqrt(1 - x ^ 2) / x)
    End Function

    Public Function Bearing(coordinate1 As OSMNode, coordinate2 As OSMNode) As Double
        Dim latitude1 As Double = ToRadian(coordinate1.Lat)
        Dim latitude2 = ToRadian(coordinate2.Lat)
        Dim longitudeDifference = ToRadian(coordinate2.Lon - coordinate1.Lon)
        Dim y = Math.Sin(longitudeDifference) * Math.Cos(latitude2)
        Dim x = Math.Cos(latitude1) * Math.Sin(latitude2) -
            Math.Sin(latitude1) * Math.Cos(latitude2) * Math.Cos(longitudeDifference)
        Return (ToDegree(Math.Atan2(y, x)) + 360) Mod 360
    End Function

    Private Function ToRadian(angle As Double) As Double
        Return (Math.PI * angle / 180.0)
    End Function

    Private Function ToDegree(angle As Double) As Double
        Return (Math.PI * angle / 180.0)
    End Function
End Class
