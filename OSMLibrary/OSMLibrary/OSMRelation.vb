Imports System.Xml
Imports System.Drawing

Public Class OSMRelation
    Inherits OSMObject
    Public UsedByRelations As New List(Of OSMRelation)
    Public Members As New List(Of OSMRelationMember)
    Public Overrides ReadOnly Property Type As ObjectType
        Get
            Return ObjectType.Relation
        End Get
    End Property
    Public Overrides ReadOnly Property JSON As String
        Get
            Dim sJSON As New System.Text.StringBuilder
            Dim xMem As OSMRelationMember
            Dim bFirst As Boolean = True
            sJSON.Append("{ ""members"" : [")
            For Each xMem In Members
                If Not bFirst Then sJSON.Append(",")
                bFirst = False
                sJSON.Append(xMem.Member.JSON)
            Next
            sJSON.Append("] }")
            Return sJSON.ToString
        End Get
    End Property
    Public Overrides ReadOnly Property GeoJSON As String
        Get
            Dim sJSON As New System.Text.StringBuilder
            Dim sThis As String
            Dim xMem As OSMRelationMember
            Dim bFirst As Boolean = True
            If Members.Count = 0 Then
                Return ""
            End If
            sJSON.Append("{ ""type"": ""FeatureCollection"", ""features"" : [")
            For Each xMem In Members
                If xMem.type = ObjectType.Relation AndAlso xMem.Role = "subarea" Then
                    Continue For
                End If
                If xMem.Member.IsPlaceholder Then
                    If (Not IsNothing(xMem.Member.Doc)) AndAlso (Not IsNothing(xMem.Member.Doc.Retriever)) Then
                        xMem.Member = xMem.Member.Doc.Retriever.GetOSMObject(xMem.Member.Type, xMem.Member.ID)
                    Else
                        Continue For
                    End If
                End If
                sThis = xMem.Member.GeoJSON
                If Len(sThis) > 0 Then
                    If Not bFirst Then sJSON.Append(",")
                    bFirst = False
                    sJSON.Append(sThis)
                Else
                    MsgBox("empty json")
                End If
            Next
            sJSON.Append("] ")
            If Tags.Count > 0 Then
                sJSON.Append(", ""properties"": " & TagGeoJSON)
            End If
            sJSON.Append("}")
            Return sJSON.ToString
        End Get
    End Property
    Public Overrides ReadOnly Property BBox As BBox
        Get
            If __Bbox Is Nothing Then
                Dim bb As New BBox
                Dim xMem As OSMRelationMember
                Dim xRel As OSMRelation, xWay As OSMWay, xNode As OSMNode
                For Each xMem In Members
                    Select Case xMem.Type
                        Case ObjectType.Relation
                            xRel = CType(xMem.Member, OSMRelation)
                            bb.Merge(xRel.BBox)
                        Case ObjectType.Way
                            xWay = CType(xMem.Member, OSMWay)
                            bb.Merge(xWay.BBox)
                        Case ObjectType.Node
                            xNode = CType(xMem.Member, OSMNode)
                            bb.Merge(xNode.Lat, xNode.Lon)
                    End Select
                Next
                __Bbox = bb
            End If
            Return __Bbox
        End Get
    End Property
    Public Sub LoadXML(x As XmlNode)
        MyBase.LoadGenericXML(x)
        Dim xNode As XmlElement
        Dim xMbr As OSMRelationMember
        Dim xObj As OSMObject
        Dim sType As String, sRef As String, sRole As String
        Dim iRef As ULong
        If Doc Is Nothing Then
            Exit Sub
        End If
        For Each xNode In x.SelectNodes("member")
            sType = xNode.GetAttribute("type")
            sRole = xNode.GetAttribute("role")
            sRef = xNode.GetAttribute("ref")
            iRef = ULong.Parse(sRef)
            xObj = Nothing
            Select Case sType
                Case "node"
                    If Doc.Nodes.Contains(iRef) Then
                        xObj = Doc.Nodes(iRef)
                    Else
                        ' forward reference to undefined way, use a placeholder for now, contains only an ID
                        xObj = New OSMNode
                        xObj.ID = iRef
                        xObj.Doc = Doc
                        Doc.Nodes.Add(iRef, xObj)
                        Debug.Assert(xObj.IsPlaceholder)
                    End If
                    DirectCast(xObj, OSMNode).UsedByRelations.Add(Me)
                Case "way"
                    If Doc.Ways.Contains(iRef) Then
                        xObj = Doc.Ways(iRef)
                    Else
                        ' forward reference to undefined way, use a placeholder for now, contains only an ID
                        xObj = New OSMWay
                        xObj.ID = iRef
                        xObj.Doc = Doc
                        Doc.Ways.Add(iRef, xObj)
                        Debug.Assert(xObj.IsPlaceholder)
                    End If
                    DirectCast(xObj, OSMWay).UsedByRelations.Add(Me)
                Case "relation"
                    If Doc.Relations.Contains(iRef) Then
                        xObj = Doc.Relations(iRef)
                    Else
                        ' forward reference to other relation, use a placeholder for now, contains only an ID
                        xObj = New OSMRelation
                        xObj.ID = iRef
                        xObj.Doc = Doc
                        Doc.Relations.Add(iRef, xObj)
                        Debug.Assert(xObj.IsPlaceholder)
                    End If
                    DirectCast(xObj, OSMRelation).UsedByRelations.Add(Me)
                Case Else
                    Throw New MemberAccessException("Unrecognised member type '" & sType & "'")
                    Exit Sub
            End Select
            If Not (xObj Is Nothing) Then
                xMbr = New OSMRelationMember(xObj, sRole)
                Members.Add(xMbr)
            End If
            __Bbox = Nothing
        Next
    End Sub
    Public Sub New(x As XmlNode)
        LoadXML(x)
    End Sub
    Public Sub New()
    End Sub

    Public Sub New(x As Xml.XmlNode, Doc As OSMDoc)
        MyBase.New(Doc)
        LoadXML(x)
    End Sub
    Default Overloads ReadOnly Property Tag(Key As String) As String
        Get
            If Tags.ContainsKey(Key) Then
                Return Tags(Key).Value
            Else
                Return ""
            End If
        End Get
    End Property
    Public Shadows Function ToString() As String
        Dim sName As String
        sName = Tag("name")
        If Len(sName) = 0 Then
            sName = "Relation " & ID.ToString
        Else
            sName = sName & " (" & ID.ToString & ")"
        End If
        Return sName
    End Function
    Public Overrides ReadOnly Property Centroid As PointF
        Get
            Dim bb As BBox = Me.BBox
            Return New PointF((bb.MinLon + bb.MaxLon) / 2.0, (bb.MinLat + bb.MaxLat) / 2.0)
        End Get
    End Property
    Public Overrides Sub SerializeMe(x As XmlTextWriter)
        For Each m As OSMRelationMember In Members
            x.WriteStartElement("member")
            x.WriteAttributeString("type", m.TypeString)
            x.WriteAttributeString("ref", m.Member.ID.ToString)
            x.WriteAttributeString("role", m.Role)
            x.WriteEndElement()
        Next
    End Sub
    Public Sub LoadNeighbours()
        Dim xTmp As OSMDoc
        If Doc Is Nothing Then
            Exit Sub
        End If
        If Doc.Retriever Is Nothing Then
            Exit Sub
        End If
        For Each xMbr In Members
            If xMbr.type = ObjectType.Node Or xMbr.type = ObjectType.Way Then
                xTmp = Doc.Retriever.GetNeighbours(Doc, xMbr.type, xMbr.Member.ID)
                If Not (xTmp Is Nothing) Then
                    Doc.Merge(xTmp)
                End If
            End If
        Next
    End Sub
End Class
