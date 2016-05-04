Public Class BBox
    Public MinLat As Double = 180.0
    Public MaxLat As Double = -180.0
    Public MinLon As Double = 180.0
    Public MaxLon As Double = -180.0
    Public Sub Merge(lat As Double, lon As Double)
        If lat < MinLat Then MinLat = lat
        If lat > MaxLat Then MaxLat = lat
        If lon < MinLon Then MinLon = lon
        If lon > MaxLon Then MaxLon = lon
    End Sub
    Public Sub Merge(bb As BBox)
        If bb.MinLat < MinLat Then MinLat = bb.MinLat
        If bb.MaxLat > MaxLat Then MaxLat = bb.MaxLat
        If bb.MinLon < MinLon Then MinLon = bb.MinLon
        If bb.MaxLon > MaxLon Then MaxLon = bb.MaxLon
    End Sub
End Class
