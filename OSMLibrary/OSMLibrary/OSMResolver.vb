Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Text

Public Class DPoint
    Public X As Double
    Public Y As Double
    Sub New(x0 As Double, y0 As Double)
        X = x0
        Y = y0
    End Sub
End Class
Public Class OSMResolver
    Public Class Ring
        Public ReadOnly Property Role As String
        Public ReadOnly Property Ways As LinkedList(Of OSMWay)
        Public Head As OSMNode
        Public Tail As OSMNode
        Public Index As Integer
        Public Resolver As OSMResolver
        Public Property EnclosingRing As Ring

        Public Function isClosed() As Boolean
            If Ways.Count = 0 Then
                Return False
            End If
            If Head Is Nothing Or Tail Is Nothing Then
                Return False
            End If
            Return Head.ID = Tail.ID
        End Function
        Public Function isClosable() As Boolean
            If Ways.Count = 0 Then
                Return False
            End If
            Return Head.ExtremelyCloseTo(Tail)
        End Function
        Public ReadOnly Property Length As Double
            Get
                Dim l As Double = 0.0
                For Each w As OSMWay In Ways
                    l += w.Length
                Next
                Return l
            End Get
        End Property
        ' Area is not as easy, as the coordinates are in degrees not metres, and of course the earth is not flat
        Public ReadOnly Property Area() As Double
            Get
                If Not isClosed() Then Return 0.0
                Dim nlist As LinkedList(Of OSMNode) = NodeList()
                Dim MinLat As Double = 90.0
                Dim ln As LinkedListNode(Of OSMNode) = nlist.First
                Dim n As OSMNode
                While Not IsNothing(ln.Next)
                    n = ln.Value
                    If n.Lat < MinLat Then MinLat = n.Lat
                    ln = ln.Next
                End While
                ln = nlist.First
                Dim a As Double = 0.0
                While Not IsNothing(ln.Next)

                    ln = ln.Next
                End While
                Return a
            End Get
        End Property
        Public ReadOnly Property BBox As BBox
            Get
                Dim b As New BBox
                For Each w In Ways
                    b.Merge(w.BBox)
                Next
                Return b
            End Get
        End Property
        Public Sub SetRole(newRole As String)
            _Role = newRole
        End Sub
        Public ReadOnly Property Centroid() As DPoint
            Get
                Dim cLat As Double, cLon As Double
                Dim b As BBox
                If isClosed() Then
                    ' needs replacing with proper centroid calculation at some stage
                    b = BBox
                    cLat = (b.MaxLat - b.MinLat) / 2.0
                    cLon = (b.MaxLon - b.MinLon) / 2.0
                Else
                    b = BBox
                    cLat = (b.MaxLat - b.MinLat) / 2.0
                    cLon = (b.MaxLon - b.MinLon) / 2.0
                End If
                Return New DPoint(cLon, cLat)
            End Get
        End Property
        Public Shared Function IsClockwise(n As LinkedList(Of OSMNode)) As Boolean
            Dim a As Single = 0
            Dim j As Integer
            For i = 0 To (n.Count) - 1
                j = (i + 1) Mod n.Count
                a += n(i).Lon * n(j).Lat
                a -= n(j).Lon * n(i).Lat
            Next
            Return (a < 0)
        End Function
        Private Shared Function Reverse(i As LinkedList(Of OSMNode)) As LinkedList(Of OSMNode)
            Dim o As New LinkedList(Of OSMNode)
            Dim n As LinkedListNode(Of OSMNode)
            n = i.First
            While n IsNot Nothing
                o.AddFirst(n.Value)
                n = n.Next
            End While
            Return o
        End Function

        Public Function NodeListClockwise() As LinkedList(Of OSMNode)
            Dim n As LinkedList(Of OSMNode) = NodeList()
            If IsClockwise(n) Then
                Return n
            Else
                Return Reverse(n)
            End If
        End Function
        Public Function NodeListAnticlockwise() As LinkedList(Of OSMNode)
            Dim n As LinkedList(Of OSMNode) = NodeList()
            If IsClockwise(n) Then
                Return Reverse(n)
            Else
                Return n
            End If
        End Function
        Public Shared Function PointInPoly(n As LinkedList(Of OSMNode), p As OSMNode) As Boolean
            ' Int pnpoly(Int nvert, float * vertx, float * verty, float testx, float testy)
            Dim c As Boolean = False
            Dim i As Integer, j As Integer
            Dim testx As Double = p.Lon
            Dim testy As Double = p.Lat
            j = n.Count - 1
            For i = 0 To n.Count - 1
                If ((n(i).Lat > testy) <> (n(j).Lat > testy)) AndAlso
                    (testx < (n(j).Lon - n(i).Lon) * (testy - n(i).Lat) / (n(j).Lat - n(i).Lat) + n(i).Lon) Then
                    c = Not c
                End If
                j = i
            Next
            Return c
        End Function
        Public Function Encloses(inner As Ring) As Boolean
            Return PointInPoly(NodeList(), inner.Head)
        End Function
        Public ReadOnly Property NodeList() As LinkedList(Of OSMNode)
            Get
                Dim h As OSMNode = Head
                Dim t As OSMNode = Tail
                Dim nlist As New LinkedList(Of OSMNode)
                ' ways are in a linked list, in order - but the points may need reversing
                nlist.AddFirst(h)
                'Dim wnodes As LinkedList(Of OSMNode)
                Dim wnodes() As OSMNode
                Dim wn As LinkedListNode(Of OSMWay) = Ways.First
                Dim w As OSMWay
                While Not IsNothing(wn)
                    w = wn.Value
                    If w.Nodes.Count > 1 Then
                        wnodes = w.Nodes.ToArray
                        If h.ID <> wnodes(0).ID Then
                            Array.Reverse(wnodes)
                            Debug.Assert(h.ID = wnodes(0).ID, "problem with way list after reversing way")
                        End If
                        h = wnodes(UBound(wnodes))
                        For i As Integer = 1 To UBound(wnodes)
                            nlist.AddLast(wnodes(i))
                        Next
                    End If
                    wn = wn.Next
                End While
                Return nlist
            End Get
        End Property

        Public Function checkGeometry() As Boolean
            If Not isClosed() Then
                Return False
            End If

            Dim nlist As LinkedList(Of OSMNode) = NodeList()
            ' now we have a single list of all nodes in the ring
            ' now we check if the ring self-intersects

            Dim ln1 As LinkedListNode(Of OSMNode)
            Dim ln2 As LinkedListNode(Of OSMNode)
            Dim lnn1 As LinkedListNode(Of OSMNode)
            Dim lnn2 As LinkedListNode(Of OSMNode)

            ' outer loop goes through the segments in order
            ' inner loop goes through segments from there (leave a gap as they intersect if they share a point) to end in order
            ln1 = nlist.First
            While Not IsNothing(ln1.Next)
                ln2 = ln1.Next
                lnn1 = ln2
                ' need to reorganise these loops. adjacent segments are always ok - we can therefore skip the first and last on the inner loop
                While Not IsNothing(lnn1.Next)
                    lnn2 = lnn1.Next
                    ' if the segments share an end node, don't complain that they intersect...
                    If Not (ln1.Value.ID = lnn1.Value.ID Or ln1.Value.ID = lnn2.Value.ID Or ln2.Value.ID = lnn1.Value.ID Or ln2.Value.ID = lnn2.Value.ID) Then
                        If linesIntersect2(New DPoint(ln1.Value.Lon, ln1.Value.Lat), New DPoint(ln2.Value.Lon, ln2.Value.Lat),
                                          New DPoint(lnn1.Value.Lon, lnn1.Value.Lat), New DPoint(lnn2.Value.Lon, lnn2.Value.Lat)) Then
                            ' bale out
                            Dim sTmp As String
                            sTmp = String.Format("intersection line 1 = {0}-{1} line 2 = {2}-{3}", ln1.Value.ID, ln2.Value.ID, lnn1.Value.ID, lnn2.Value.ID)
                            My.Computer.Clipboard.SetText(sTmp)
                            MsgBox(sTmp)
                            Return False
                        End If
                    End If
                    lnn1 = lnn2
                End While
                ln1 = ln2
            End While
            Return True
        End Function
        Public Shared Function linesintersect(x1 As Double, y1 As Double, x2 As Double, y2 As Double,
                                       a1 As Double, b1 As Double, a2 As Double, b2 As Double) As Boolean

            Dim dx As Double
            Dim dy As Double
            Dim da As Double
            Dim db As Double
            Dim t As Double
            Dim s As Double

            dx = x2 - x1
            dy = y2 - y1
            da = a2 - a1
            db = b2 - b1
            If (da * dy - db * dx) = 0 Then
                ' The segments are parallel.
                linesintersect = False
                Exit Function
            End If

            s = (dx * (b1 - y1) + dy * (x1 - a1)) / (da * dy - db * dx)
            t = (da * (y1 - b1) + db * (a1 - x1)) / (db * dx - da * dy)

            linesintersect = (s >= 0.0 And s <= 1.0 And
                                 t >= 0.0 And t <= 1.0)

            ' If it exists, the point of intersection is:
            ' (x1 + t * dx, y1 + t * dy)
            MsgBox(String.Format("intersection at {0},{1}", x1 + t * dx, y1 + t * dy))
        End Function

        Public Shared Function linesIntersect2(p1 As DPoint, p2 As DPoint, p3 As DPoint, p4 As DPoint) As Boolean
            'If (CCW(p1, p2, p3) * CCW(p1, p2, p4)) > 0 Then Return False
            'If (CCW(p3, p4, p1) * CCW(p3, p4, p2)) > 0 Then Return False
            Return ((CCW(p1, p2, p3) * CCW(p1, p2, p4)) <= 0) And ((CCW(p3, p4, p1) * CCW(p3, p4, p2)) <= 0)
            'Return True
        End Function
        Public Shared Function CCW(p0 As DPoint, p1 As DPoint, p2 As DPoint) As Double
            Return (p1.X - p0.X) * (p2.Y - p0.Y) - (p2.X - p0.X) * (p1.Y - p0.Y)
        End Function

        Public Function Link(w As OSMWay) As Boolean
            Dim Hw As OSMNode, Tw As OSMNode
            If w.Nodes.Count = 0 Then
                ' ways with no nodes are ignored completely
                Return True
            End If
            Hw = w.Nodes.First.Value
            Tw = w.Nodes.Last.Value
            If Ways.Count = 0 Then
                Ways.AddFirst(w)
                Head = Hw
                Tail = Tw
            Else
                If Hw.ExtremelyCloseTo(Tail) Then
                    Ways.AddLast(w)
                    Tail = Tw
                ElseIf Tw.ExtremelyCloseTo(Tail) Then
                    Ways.AddLast(w)
                    Tail = Hw
                ElseIf Hw.ExtremelyCloseTo(Head) Then
                    Ways.AddFirst(w)
                    Head = Tw
                ElseIf Tw.ExtremelyCloseTo(Head) Then
                    Ways.AddFirst(w)
                    Head = Hw
                Else
                    Return False
                End If
            End If
            Return True
        End Function
        Private Function WayList() As String
            Dim wn As LinkedListNode(Of OSMWay)
            Dim w As OSMWay
            Dim sTmp As String = ""
            wn = Ways.First
            Do While Not IsNothing(wn)
                w = wn.Value
                If Len(sTmp) > 0 Then sTmp = sTmp & ","
                sTmp = sTmp & w.ID & "<" & w.Nodes(0).ID & "-" & w.Nodes(w.Nodes.Count - 1).ID & ">"
                wn = wn.Next
            Loop
            Return sTmp
        End Function
        Public Function Coalesce(r2 As Ring) As Boolean
            Dim wn As LinkedListNode(Of OSMWay)
            If Me.Role <> r2.Role Then
                ' cannot/should not merge with different roles
                Debug.Print(String.Format("Cannot coalesce rings with different roles: #{0} is {1}, #{2} is {3}",
                                          Me.Index, Me.Role, r2.Index, r2.Role))
                Return False
            End If
            Debug.Print("Coalescing ring #" & r2.Index & " into ring #" & Me.Index)
            Debug.Print("Ring #" & Me.Index & ": <" & Me.Head.ID & ">{" & Me.WayList() & "}<" & Me.Tail.ID & ">")
            Debug.Print("Ring #" & r2.Index & ": <" & r2.Head.ID & ">{" & r2.WayList() & "}<" & r2.Tail.ID & ">")

            Debug.Assert(Me.Ways.Count > 0, "Coalescing into empty ring")
            Debug.Assert(r2.Ways.Count > 0, "Coalescing from empty ring")
            Debug.Assert(Me.isClosed() = False, "Coalescing into closed ring")
            ' Debug.Assert(r2.isClosed() = False, "Coalescing from closed ring")
            If Me.Head Is Nothing Then ' ring has ways but no nodes!
                Debug.Print("Ring #" & Me.Index & " has no nodes")
                wn = r2.Ways.First
                Do
                    Me.Ways.AddLast(wn.Value)
                    wn = wn.Next
                Loop Until IsNothing(wn)
                Me.Head = r2.Head
                Me.Tail = r2.Tail
                Return True
            End If
            If Me.Head.ExtremelyCloseTo(r2.Head) Then
                'add ways from r2 in reverse order before head
                Debug.Print("me.head equals other.head, add ways from r2 in reverse order before head")
                wn = r2.Ways.First
                Do
                    Me.Ways.AddFirst(wn.Value)
                    wn = wn.Next
                Loop Until IsNothing(wn)
                Me.Head = r2.Tail
            ElseIf Me.Tail.ExtremelyCloseTo(r2.Head) Then
                ' add ways from r2 in normal order after tail
                Debug.Print("me.tail equals other.head, add ways from r2 in normal order after tail")
                wn = r2.Ways.First
                Do
                    Me.Ways.AddLast(wn.Value)
                    wn = wn.Next
                Loop Until IsNothing(wn)
                Me.Tail = r2.Tail
            ElseIf Me.Head.ExtremelyCloseTo(r2.Tail) Then
                ' add ways from r2 in reverse order before head
                Debug.Print("me.head equals other.tail, add ways from r2 in reverse order before head")
                If r2.Ways.Count > 0 Then
                    wn = r2.Ways.Last
                    Do While Not (wn Is Nothing)
                        Me.Ways.AddFirst(wn.Value)
                        wn = wn.Previous
                    Loop
                    Me.Head = r2.Head
                End If
            ElseIf Me.Tail.ExtremelyCloseTo(r2.Tail) Then
                ' add ways from r2 in reverse order after tail
                Debug.Print("me.tail equals other.tail, add ways from r2 in reverse order after tail")
                wn = r2.Ways.Last
                Do Until (wn Is Nothing)
                    Me.Ways.AddLast(wn.Value)
                    wn = wn.Previous
                Loop
                Me.Tail = r2.Head
            Else
                Debug.Print("no match for head or tail")
                Return False
            End If
            Debug.Print("After Coalesce, Ring #" & Me.Index & ": <" & Me.Head.ID & ">{" & Me.WayList() & "}<" & Me.Tail.ID & ">")
            Return True
        End Function

        Function JSON() As String
            Dim jb As New StringBuilder
            Dim nlist As LinkedList(Of OSMNode)

            If isClosed() Then
                ' we have a complete ring - return as a polygon
                nlist = NodeList()
                Dim bFirst As Boolean = True
                Dim n As OSMNode
                Dim wn As LinkedListNode(Of OSMNode)
                jb.Append("{ ""points"" : [")
                wn = nlist.First
                While wn IsNot Nothing
                    n = wn.Value
                    If Not bFirst Then jb.Append(",")
                    jb.Append(n.JSON)
                    bFirst = False
                    wn = wn.Next
                End While
                jb.Append("] }")
            Else
                ' incomplete ring - return as a bunch of ways

            End If
            Return jb.ToString
        End Function
        ''' <summary>
        ''' Returns a new Ring which has the common boundary between this and Other removed
        ''' </summary>
        ''' <param name="Other">The Ring to be combined</param>
        ''' <returns></returns>
        Public Function Combine(Other As Ring) As Ring
            If Other Is Nothing Then Return Me
            If Not Me.isClosed() Then
                Throw New Exception("This Ring not closed")
                Return Nothing
            End If
            If Not Other.isClosed() Then
                Throw New Exception("Other Ring not closed")
                Return Nothing
            End If
            Dim out As New Ring()
            out.Resolver = Me.Resolver
            Dim nIn As LinkedList(Of OSMNode) = Me.NodeListClockwise()
            Dim nOther As LinkedList(Of OSMNode) = Other.NodeListClockwise()
            Dim nOut As New LinkedList(Of OSMNode)
            Dim bInseg As Boolean = False
            Dim segStart As OSMNode = Nothing
            Dim segEnd As OSMNode = Nothing
            Dim otherStart As LinkedListNode(Of OSMNode)
            Dim otherEnd As LinkedListNode(Of OSMNode)

            For Each n In nIn
                If bInseg Then
                    If nOther.Contains(n) Then
                        segEnd = n
                    Else
                        ' just off the end of a common segment from segStart to segEnd inclusive
                        ' go round the other ring until we get back to the join
                        otherStart = nOther.Find(segStart)
                        otherEnd = nOther.Find(segEnd)
                        While otherStart IsNot otherEnd
                            nOut.AddLast(otherStart.Value)
                            If otherStart.Next Is Nothing Then
                                otherStart = nOther.First
                            Else
                                otherStart = otherStart.Next
                            End If
                        End While
                        nOut.AddLast(segEnd)
                        nOut.AddLast(n)
                        bInseg = False
                    End If
                Else
                    If nOther.Contains(n) Then
                        segStart = n
                        segEnd = n
                        bInseg = True
                    Else
                        nOut.AddLast(n)
                        ' still looking for the start of a common segment
                    End If
                End If
            Next
            ' the start/end point may also be the end of the common segment
            If bInseg Then
                otherStart = nOther.Find(segStart)
                otherEnd = nOther.Find(segEnd)
                While otherStart IsNot otherEnd
                    nOut.AddLast(otherStart.Value)
                    If otherStart.Next Is Nothing Then
                        otherStart = nOther.First
                    Else
                        otherStart = otherStart.Next
                    End If
                End While
                nOut.AddLast(segEnd)
            End If

            ' returned ring has a single way containing all the nodes - but nothing else
            Dim w As New OSMWay
            w.Nodes = nOut
            out.Ways.AddLast(w)
            out.Head = nOut.First.Value
            out.Tail = nOut.Last.Value
            Return out
        End Function

        Public ReadOnly Property GeoJSON As String
            Get
                Dim sJSON As New System.Text.StringBuilder
                If isClosed() Then
                    sJSON.Append("{ ""type"": ""Polygon"", ""coordinates"" : [")
                Else
                    sJSON.Append("{ ""type"": ""MultiLineString"", ""coordinates"" : [")
                End If
                sJSON.Append(GeoJSONCoords(NodeList()))
                sJSON.Append("]}")
                Return sJSON.ToString
            End Get
        End Property
        Public Shared Function GeoJSONCoords(nlist As LinkedList(Of OSMNode)) As String
            Dim sJSON As New System.Text.StringBuilder
            Dim bFirst As Boolean = True
            Dim n As OSMNode
            Dim wn As LinkedListNode(Of OSMNode)
            sJSON.Append("[")
            wn = nlist.First
            While wn IsNot Nothing
                n = wn.Value
                If Not bFirst Then sJSON.Append(",")
                sJSON.Append(n.GeoJSONCoords)
                bFirst = False
                wn = wn.Next
            End While
            sJSON.Append("]")
            Return sJSON.ToString()
        End Function

        Sub New()
            MyBase.New()
            _Ways = New LinkedList(Of OSMWay)
        End Sub
        Sub New(res As OSMResolver, w As OSMWay, r As String)
            MyBase.New()
            _Ways = New LinkedList(Of OSMWay)
            Resolver = res
            If w.Nodes.Count > 0 Then
                Ways.AddFirst(w)
                Head = w.Nodes.First.Value
                Tail = w.Nodes.Last.Value
            End If
            Role = r
        End Sub
    End Class

    Private _relation As OSMRelation
    Public Enum ResolverMode
        Linear
        Polygon
    End Enum
    Public Mode As ResolverMode = ResolverMode.Polygon
    Public Rings As New LinkedList(Of Ring)
    Public Nodes As New List(Of OSMNode)
    Public Ways As New List(Of OSMWay)
    Public IgnoreWays As New List(Of OSMWay)

    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(r As OSMRelation)
        MyBase.New()
        _relation = r
        Merge(r)
    End Sub

    Public Function Merge(rel As OSMRelation) As Boolean
        Dim bLinked As Boolean
        Dim r As Ring, r2 As Ring
        Dim sTmp As String

        If _relation Is Nothing Then _relation = rel

        ' pass 1: collect all ways which are not internal borders between any of the
        ' relations merged in
        For Each m As OSMRelationMember In rel.Members
            If m.Type <> OSMObject.ObjectType.Way Then
                Continue For
            End If
            If Ways.Contains(m.Member) Then
                IgnoreWays.Add(m.Member)
            Else
                Ways.Add(m.Member)
            End If
        Next

        ' pass 2: process node members
        For Each m As OSMRelationMember In rel.Members
            If m.Type = OSMObject.ObjectType.Node Then
                If Not Nodes.Contains(m.Member) Then Nodes.Add(m.Member)
            End If
        Next

        ' pass 3: starting from clean slate, process ways into (partial) rings
        Rings.Clear()
        For Each m As OSMRelationMember In rel.Members
            sTmp = ""
            If m.Type = OSMObject.ObjectType.Way Then
                ' here be magic!
                If IgnoreWays.Contains(m.Member) Then Continue For
                bLinked = False
                For Each r In Rings
                    If r.isClosed() Then
                        Continue For
                    End If
                    If r.Link(m.Member) Then
                        bLinked = True
                        Debug.Print("Linked way " & m.Member.ID & " to ring #" & r.Index)
                        If m.Role <> r.Role Then
                            'role mismatch!
                        End If
                        Exit For
                    End If
                Next
                If Not bLinked Then
                    r = New Ring(Me, m.Member, m.Role)
                    Rings.AddLast(r)
                    r.Index = Rings.Count
                    Debug.Print("Way " & m.Member.ID & " starts new ring #" & r.Index)
                End If
            End If
        Next

        ' pass 4: see if we can link the partial rings together
        Dim rn As LinkedListNode(Of Ring)
        Dim r2n As LinkedListNode(Of Ring)

        Do
            rn = Rings.First
            bLinked = False
            Do While Not IsNothing(rn)
                r = rn.Value
                If Not r.isClosed() Then
                    r2n = Rings.First
                    Do While Not IsNothing(r2n)
                        r2 = r2n.Value
                        ' if r2 can link to r then
                        If r2 IsNot r Then
                            If r.Coalesce(r2) Then
                                Debug.Print("Removing ring #" & r2.Index)
                                Rings.Remove(r2)
                                bLinked = True
                                Exit Do
                            End If
                        End If
                        r2n = r2n.Next
                    Loop
                End If
                If bLinked Then
                    Exit Do
                End If
                rn = rn.Next
            Loop
        Loop Until Not bLinked
        FindHoles()
        Return True
    End Function
    Public Function checkGeometry() As Boolean
        For Each r As Ring In Rings
            If Not r.checkGeometry() Then
                Return False
            End If
        Next
        Return True
    End Function
    Public ReadOnly Property BBox As BBox
        Get
            Dim b As New BBox
            For Each r In Rings
                If r.Role = "outer" Or r.Role = "" Then
                    b.Merge(r.BBox)
                End If
            Next
        End Get
    End Property
    Public ReadOnly Property OuterRing As Ring
        Get
            For Each r In Rings
                If r.isClosed AndAlso (r.Role = "outer" OrElse r.Role = "") Then Return r
            Next
            Return Nothing
        End Get
    End Property
    Private Sub FindHoles()
        Dim bFound As Boolean
        For Each r In Rings
            If r.isClosed AndAlso r.Role = "inner" Then
                bFound = False
                For Each outer In Rings
                    If outer.isClosed AndAlso (outer.Role = "outer" OrElse outer.Role = "") Then
                        ' if o is inside r
                        If outer.Encloses(r) Then
                            r.EnclosingRing = outer
                            bFound = True
                            Exit For
                        End If
                    End If
                Next
                If Not bFound Then
                    Debug.Print("Inner ring not enclosed by outer ring")
                End If
            Else
                r.EnclosingRing = Nothing
            End If
        Next
    End Sub

    Public Function GeoJSON() As String
        Dim sJSON As New StringBuilder(10000)
        Dim xMem As OSMRelationMember
        Dim bFirst As Boolean = True
        Dim rIn As Ring
        Dim RingList As New List(Of Ring)
        Dim b As New BBox
        If _relation.Members.Count = 0 Then
            Return ""
        End If
        RingList.AddRange(Rings)
        '  FeatureCollection features [
        '  for each outer ring
        '   Polygon
        '       [ coords clockwise ]
        '       for each inner ring within this outer ring
        '           [ coords counterclockwise ]
        '       next
        '   next
        '   other members
        '   ]
        sJSON.Append("{ ""type"": ""FeatureCollection"", ""features"" : [")
        For Each r In Rings
            If r.isClosed AndAlso (r.Role = "outer" Or r.Role = "") Then
                If Not bFirst Then sJSON.Append(", ")
                sJSON.Append("{ ""type"": ""Polygon"", ""coordinates"" : [")
                sJSON.Append(Ring.GeoJSONCoords(r.NodeListClockwise()))
                RingList.Remove(r)
                For Each inner In Rings
                    If inner.isClosed() AndAlso inner.EnclosingRing Is r Then
                        sJSON.Append(", ")
                        sJSON.Append(Ring.GeoJSONCoords(inner.NodeListAnticlockwise()))
                        RingList.Remove(inner)
                    End If
                Next
                sJSON.Append("] }")
                b.Merge(r.BBox)
                bFirst = False
            End If
        Next
        For Each r In RingList
            If Not bFirst Then sJSON.Append(", ")
            sJSON.Append(r.GeoJSON) ' unclosed ring will be emitted as linestring not polygon
            b.Merge(r.BBox)
            bFirst = False
        Next
        For Each xMem In _relation.Members
            If xMem.Type = OSMObject.ObjectType.Node Then
                If Not bFirst Then sJSON.Append(", ")
                sJSON.Append(xMem.GeoJSON)
                bFirst = False
            End If
        Next
        Dim p As New OSMNode
        Dim sName As String = _relation.Name()
        p.Lat = (b.MaxLat + b.MinLat) / 2.0
        p.Lon = (b.MaxLon + b.MinLon) / 2.0
        p.__Placeholder = False
        p.Tags.Add("_bblabel", New OSMTag("_bblabel", sName))
        sJSON.Append(", ")
        sJSON.Append(p.GeoJSON)
        sJSON.Append("] ")
        If _relation.Tags.Count > 0 Then
            sJSON.Append(", ""properties"": " & _relation.TagGeoJSON)
        End If
        sJSON.Append("}")
        Return sJSON.ToString
    End Function
End Class