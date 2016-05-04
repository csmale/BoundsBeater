Public Class OSMLineString
    Public Nodes As New LinkedList(Of OSMNode)
    Public ReadOnly Property BBox As BBox
        Get
            Dim bb As New BBox
            Dim x As OSMNode
            For Each x In Nodes
                bb.Merge(x.Lat, x.Lon)
            Next
            Return bb
        End Get
    End Property
End Class
