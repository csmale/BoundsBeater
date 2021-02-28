Imports System.IO
Imports System.Xml


Public Class OSMDoc
        Dim xDoc As New XmlDocument
        Public Relations As New OSMCollection(Of OSMRelation)
        Public Ways As New OSMCollection(Of OSMWay)
        '    Public Nodes As New Dictionary(Of ULong, OSMNode)
        Public Nodes As New OSMCollection(Of OSMNode)
        Public Changesets As New OSMCollection(Of OSMChangeset)
        Public Retriever As OSMRetriever
        Public Users As New Dictionary(Of ULong, OSMUser)

        Public Event LoadProgress(nRels As Integer, nWays As Integer, nNodes As Integer)

        Private Function LoadXMLDoc(xDoc As XmlDocument) As Boolean
            Dim xNodes As XmlNodeList
            Dim xWays As XmlNodeList
            Dim xRels As XmlNodeList
            Dim xChgs As XmlNodeList
            Dim xNode As XmlNode
            Dim xOsmNode As OSMNode
            Dim xParentNode As OSMObject
            Dim xOsmWay As OSMWay
            Dim xOsmRel As OSMRelation
            Dim xMbr As OSMRelationMember
            Dim xOsmChg As OSMChangeset
            Dim ID As Long
            Dim iVer As Long
            xNodes = xDoc.SelectNodes("/osm/node")
            '        MsgBox("found " & xNodes.Count & " nodes")
            For Each xNode In xNodes
                ID = CLng(xNode.Attributes("id").InnerText)
                iVer = CLng(xNode.Attributes("version").InnerText)
                xOsmNode = Nodes(ID)
                xParentNode = Nothing
                If xOsmNode Is Nothing Then ' first time seeing this node id
                    xOsmNode = New OSMNode(xNode, Me) With {
                    .Doc = Me
                }
                    Nodes.Add(ID, xOsmNode)
                    xParentNode = Nodes(ID)
                Else    ' exists already! placeholder? update in place
                    If xOsmNode.IsPlaceholder Then
                        xOsmNode.LoadXML(xNode)
                        xParentNode = Nodes(ID)
                    Else    ' different version? add version to collection
                        If xOsmNode.VersionByNumber(iVer) Is Nothing Then
                            xParentNode = xOsmNode
                            xOsmNode = New OSMNode(xNode, Me) With {.Doc = Me}
                        End If
                    End If
                End If
                If Not IsNothing(xParentNode) Then
                    xParentNode.InsertVersion(xOsmNode)
                End If
            Next
            xWays = xDoc.SelectNodes("/osm/way")
            '        MsgBox("found " & xWays.Count & " ways")
            For Each xNode In xWays
                ID = CLng(xNode.Attributes("id").InnerText)
                iVer = CLng(xNode.Attributes("version").InnerText)
                xOsmWay = Ways(ID)
                xParentNode = Nothing
                If xOsmWay Is Nothing Then ' first time seeing this node id
                    xOsmWay = New OSMWay(xNode, Nodes, Me) With {.Doc = Me}
                    Ways.Add(ID, xOsmWay)
                    xParentNode = Ways(ID)
                Else    ' exists already! placeholder? update in place
                    If xOsmWay.IsPlaceholder Then
                        xOsmWay.LoadXML(xNode, Nodes)
                        xParentNode = Ways(ID)
                    Else    ' different version? add version to collection
                        If xOsmWay.VersionByNumber(iVer) Is Nothing Then
                            xParentNode = xOsmWay
                            xOsmWay = New OSMWay(xNode, Nodes, Me) With {.Doc = Me}
                        End If
                    End If
                End If
                If Not IsNothing(xParentNode) Then
                    xParentNode.InsertVersion(xOsmWay)
                End If
            Next
            xRels = xDoc.SelectNodes("/osm/relation")
            '        MsgBox("found " & xRels.Count & " relations")
            For Each xNode In xRels
                ID = CLng(xNode.Attributes("id").InnerText)
                xOsmRel = Relations(ID)
                If xOsmRel Is Nothing Then
                    ' normal case, relation not seen before in this file
                    xOsmRel = New OSMRelation(xNode, Me) With {.Doc = Me}
                    If Not Relations.Contains(xOsmRel.ID) Then ' first version - treat as base
                        Relations.Add(xOsmRel.ID, xOsmRel)
                    End If
                    xParentNode = Relations(xOsmRel.ID)
                    If xParentNode.VersionByNumber(xOsmRel.Version) Is Nothing Then
                        xParentNode.InsertVersion(xOsmRel)
                    End If
                    If xOsmRel.Version > xParentNode.Version Then
                        Relations(ID) = DirectCast(xParentNode, OSMRelation)
                    End If
                Else
                    ' must be a placeholder?
                    If xOsmRel.IsPlaceholder Then
                        xOsmRel.LoadXML(xNode)
                    Else
                        ' will need some work for version support
                        ' duplicate relation in file!!!
                        If xOsmRel.VersionByNumber(iVer) Is Nothing Then
                            xParentNode = xOsmRel
                            xOsmRel = New OSMRelation(xNode, Me) With {
                            .Doc = Me
                        }
                            xParentNode.InsertVersion(xOsmRel)
                        End If
                    End If
                End If
            Next
            xChgs = xDoc.SelectNodes("/osm/changeset")
            '        MsgBox("found " & xchgs.Count & " changesets")
            For Each xNode In xChgs
                ID = CLng(xNode.Attributes("id").InnerText)
                xOsmChg = New OSMChangeset(xNode, Me)
                Changesets.Add(xOsmChg.ID, xOsmChg)
            Next
            ' deal with missing nodes and ways which are referenced by ways and relations
            Return True
        End Function
        Private Function ParseNode(xDoc As XmlReader) As Boolean
            Return True
        End Function
        Private Function ParseWay(xDoc As XmlReader) As Boolean
            Return True
        End Function
        Private Function ParseRel(xDoc As XmlReader) As Boolean
            Return True
        End Function
    Public Function GetOSMObject(Type As OSMObject.ObjectType, ID As Long) As OSMObject
        Select Case Type
            Case OSMObject.ObjectType.Node
                Return Nodes(ID)
            Case OSMObject.ObjectType.Way
                Return Ways(ID)
            Case OSMObject.ObjectType.Relation
                Return Relations(ID)
        End Select
        Return Nothing
    End Function
    Public Function LoadXML(sXML As String) As Boolean
            Try
                xDoc.LoadXml(sXML)
            Catch
                Return False
            End Try
            Return LoadXMLDoc(xDoc)
        End Function

        Public Function Load(OSMFile As String) As Boolean
            Try
                xDoc.Load(OSMFile)
            Catch
                Return False
            End Try
            Return LoadXMLDoc(xDoc)
        End Function
        Public Sub New(OSMFile As String)
            Load(OSMFile)
        End Sub
        Public Sub New()
        End Sub
    Public Sub Merge(xOther As OSMDoc)
        Dim xNode As OSMNode
        Dim xWay As OSMWay
        Dim xRel As OSMRelation
        Dim lRef As Long
        ' Debug.Assert(xOther IsNot Nothing)
        If xOther Is Nothing Then Return

        For Each lRef In xOther.Nodes.Keys
            xNode = xOther.Nodes(lRef)
            If Not Nodes.Contains(lRef) Then
                Nodes.Add(lRef, xNode)
            ElseIf Nodes(lRef).IsPlaceholder OrElse xNode.Version > Nodes(lRef).Version _
                    OrElse (xNode.HistoryLoaded AndAlso Not Nodes(lRef).HistoryLoaded) Then
                Nodes(lRef) = xNode
            End If
            xNode.Doc = Me
        Next
        For Each lRef In xOther.Ways.Keys
            xWay = xOther.Ways(lRef)
            If Not Ways.Contains(lRef) Then
                Ways.Add(lRef, xWay)
            ElseIf Ways(lRef).IsPlaceholder OrElse xWay.Version > Ways(lRef).Version _
                    OrElse (xway.HistoryLoaded AndAlso Not ways(lRef).HistoryLoaded) Then
                Ways(lRef) = xWay
            End If
            xWay.Doc = Me
        Next
        For Each lRef In xOther.Relations.Keys
            xRel = xOther.Relations(lRef)
            If Not Relations.Contains(lRef) Then
                Relations.Add(lRef, xRel)
            ElseIf Relations(lRef).IsPlaceholder OrElse xRel.Version > Relations(lRef).Version _
                    OrElse (xrel.HistoryLoaded AndAlso Not Relations(lRef).HistoryLoaded) Then
                Relations(lRef) = xRel
            End If
            xRel.Doc = Me
        Next
        For Each UID As ULong In xOther.Users.Keys
            If Not Users.ContainsKey(UID) Then
                Users.Add(UID, New OSMUser(UID, xOther.Users(UID).Name))
            End If
        Next
    End Sub

    Public Function LoadPBF(sFile As String) As Boolean
        Return Utilities.LoadPBFAsXML(sFile, AddressOf LoadXMLStream)
    End Function

    Public Function LoadBigXML(sFile As String) As Boolean
            Dim xRdr As XmlTextReader
            Try
                xRdr = New XmlTextReader(sFile)
            Catch
                Return False
            End Try
            Return LoadXMLStream(xRdr)
        End Function
        Public Function LoadXMLStream(xrdr As XmlTextReader) As Boolean
            Dim xDoc As XmlDocument
            Dim xEl As XmlElement
            Dim xNode As OSMNode
            Dim xWay As OSMWay
            Dim xRel As OSMRelation
            Dim ID As Long

            xrdr.WhitespaceHandling = WhitespaceHandling.None
            While Not xrdr.EOF
                Select Case xrdr.NodeType
                    Case XmlNodeType.Element
                        xDoc = New XmlDocument()
                        Select Case xrdr.Name
                            Case "node"
                                xEl = DirectCast(xDoc.ReadNode(xrdr), XmlElement)
                                xNode = New OSMNode(xEl, Me)
                                ' xNode.Doc = Me
                                Nodes.Add(xNode.ID, xNode)
                            Case "way"
                                xEl = DirectCast(xDoc.ReadNode(xrdr), XmlElement)
                                xWay = New OSMWay(xEl, Me)
                                Ways.Add(xWay.ID, xWay)
                            Case "relation"
                                xEl = DirectCast(xDoc.ReadNode(xrdr), XmlElement)
                                ID = CLng(xEl.Attributes("id").InnerText)
                                xRel = Relations(ID)
                                If xRel Is Nothing Then
                                    ' normal case, relation not seen before in this file
                                    xRel = New OSMRelation(xEl, Me)
                                    '                               xRel.Doc = Me
                                    Relations.Add(xRel.ID, xRel)
                                Else
                                    ' must be a placeholder?
                                    If xRel.IsPlaceholder Then
                                        xRel.LoadXML(xEl)
                                    Else
                                        ' duplicate relation in file!!!
                                    End If
                                End If
                            Case Else
                                xrdr.Read()
                        End Select
                        RaiseEvent LoadProgress(Relations.Count, Ways.Count, Nodes.Count)
                    Case Else
                        xrdr.Read()
                End Select
            End While
            Return True
        End Function
        Public Function SaveXML(sFile As String, Optional UseVersions As Boolean = False) As Boolean
            Dim x As XmlTextWriter
            Dim bRet As Boolean

            Try
                x = New XmlTextWriter(sFile, System.Text.Encoding.UTF8)
            Catch e As System.IO.IOException
                MsgBox(e.Message)
                Return False
            End Try
            x.Formatting = Formatting.Indented
            x.WriteStartDocument(True)

            bRet = SaveXML(x, UseVersions)

            x.WriteEndDocument()
            x.Close()
            Return bRet
        End Function

        Public Function SaveXML(x As XmlWriter, Optional UseVersions As Boolean = False) As Boolean
            'write header

            x.WriteStartElement("osm")
            x.WriteAttributeString("version", "0.6")
            x.WriteAttributeString("generator", "BoundsBeater")
            x.WriteAttributeString("copyright", "OpenStreetMap and contributors")
            x.WriteAttributeString("attribution", "http://www.openstreetmap.org/copyright")
            x.WriteAttributeString("license", "http://opendatacommons.org/licenses/odbl/1-0/")
            'write nodes
            For Each xn As OSMNode In Nodes.Values.Cast(Of OSMNode)()
                If Not xn.IsPlaceholder Then
                    If UseVersions Then
                        For Each xnv As OSMNode In xn.Versions
                            xnv.Serialize(x)
                        Next
                    Else
                        xn.Serialize(x)
                    End If
                End If
            Next
            'write ways
            For Each xw As OSMWay In Ways.Values.Cast(Of OSMWay)()
                If Not xw.IsPlaceholder Then
                    If UseVersions Then
                        For Each xwv As OSMWay In xw.Versions
                            xwv.Serialize(x)
                        Next
                    Else
                        xw.Serialize(x)
                    End If
                End If
            Next
            'write relations
            For Each xr As OSMRelation In Relations.Values.Cast(Of OSMRelation)()
                If Not xr.IsPlaceholder Then
                    If UseVersions Then
                        For Each xrv As OSMRelation In xr.Versions
                            xrv.Serialize(x)
                        Next
                    Else
                        xr.Serialize(x)
                    End If
                End If
            Next
            'write trailer
            x.WriteEndElement()
            Return True
        End Function
        Public Sub ClearChangedFlags()
            For Each xn As OSMNode In Nodes.Values.Cast(Of OSMNode)()
                xn.Changed = False
            Next
            'write ways
            For Each xw As OSMWay In Ways.Values.Cast(Of OSMWay)()
                xw.Changed = False
            Next
            'write relations
            For Each xr As OSMRelation In Relations.Values.Cast(Of OSMRelation)()
                xr.Changed = False
            Next
        End Sub
        Public Function GetChangeFile(x As XmlWriter) As Boolean
            x.WriteStartDocument(True)
            x.WriteStartElement("osmChange")
            x.WriteAttributeString("version", "0.6")
            x.WriteAttributeString("generator", "BoundsBeater")

            x.WriteStartElement("modify")
            'write nodes
            For Each xn As OSMNode In Nodes.Values.Cast(Of OSMNode)()
                If Not xn.IsPlaceholder AndAlso xn.Changed Then
                    xn.Serialize(x)
                End If
            Next
            'write ways
            For Each xw As OSMWay In Ways.Values.Cast(Of OSMWay)()
                If Not xw.IsPlaceholder AndAlso xw.Changed Then
                    xw.Serialize(x)
                End If
            Next
            'write relations
            For Each xr As OSMRelation In Relations.Values.Cast(Of OSMRelation)()
                If Not xr.IsPlaceholder AndAlso xr.Changed Then
                    xr.Serialize(x)
                End If
            Next
            'write trailer

            x.WriteEndElement() ' modify
            x.WriteEndElement() ' osmChange
            x.WriteEndDocument()
            x.Close()
            Return True
        End Function
    End Class

