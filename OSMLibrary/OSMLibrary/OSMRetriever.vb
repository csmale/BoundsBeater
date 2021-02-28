Imports System.Net
Imports System.IO
Imports System.Security

Public Class OSMRetriever
        Public MaxAge As Integer = 24 * 60 * 60   ' in seconds, default is one day
        Public API As OSMApi
        Public LastError As String

        Public Sub New()
            MyBase.New()
            API = New OSMApi()
        End Sub
        Public Sub New(URL As String)
            MyBase.New()
            API = New OSMApi(URL)
            'BaseURL = URL
        End Sub
        Public Sub New(URL As String, Credentials As NetworkCredential)
            MyBase.New()
            'Me.BaseURL = URL
            API = New OSMApi(URL, Credentials)
            'Me.Credentials = Credentials
        End Sub
        Public Sub New(Credentials As NetworkCredential)
            MyBase.New()
            API = New OSMApi(Credentials)
            'Me.Credentials = Credentials
        End Sub
        Public Function GetOSMObject(xType As OSMObject.ObjectType, lRef As Long, Optional bFull As Boolean = False) As OSMObject
            Dim xDoc As OSMDoc
            Try
                xDoc = API.GetOSMDoc(xType, lRef, bFull)
            Catch e As OSMWebException
                LastError = API.LastError
                Return Nothing
            End Try
            If xDoc Is Nothing Then
                LastError = API.LastError
                Return Nothing
            End If
            LastError = ""
            Select Case xType
                Case OSMObject.ObjectType.Node
                    Return xDoc.Nodes(lRef)
                Case OSMObject.ObjectType.Way
                    Return xDoc.Ways(lRef)
                Case OSMObject.ObjectType.Relation
                    Return xDoc.Relations(lRef)
                Case Else
                    LastError = "Unknown object type"
                    Return Nothing
            End Select
        End Function
        Public Function GetOSMObjectVersion(xType As OSMObject.ObjectType, lRef As Long, lVer As Long) As OSMObject
            Dim xDoc As OSMDoc
            If lVer = 0 Then
                xDoc = API.GetOSMDoc(xType, lRef, False)
            Else
                xDoc = API.GetOSMDocVersion(xType, lRef, lVer)
            End If
            If xDoc Is Nothing Then
                LastError = API.LastError
                Return Nothing
            End If
            Select Case xType
                Case OSMObject.ObjectType.Node
                    Return xDoc.Nodes(lRef)
                Case OSMObject.ObjectType.Way
                    Return xDoc.Ways(lRef)
                Case OSMObject.ObjectType.Relation
                    Return xDoc.Relations(lRef)
                Case OSMObject.ObjectType.Changeset
                    Return xDoc.Changesets(lRef)
                Case Else
                    LastError = "Unknown object type"
                    Return Nothing
            End Select
        End Function
        Public Function GetOSMObjectHistory(xType As OSMObject.ObjectType, lRef As Long, Optional bFull As Boolean = False) As OSMObject
        Dim xObj As OSMObject = Nothing
        Dim xDoc As OSMDoc
        ' this just gets the history of the object itself, not the referenced ways and nodes

        xDoc = API.GetOSMObjectHistory(xType, lRef)
        If xDoc Is Nothing Then Return Nothing
        Select Case xType
            Case OSMObject.ObjectType.Relation
                xObj = xDoc.Relations(lRef)
            Case OSMObject.ObjectType.Way
                xObj = xDoc.Ways(lRef)
            Case OSMObject.ObjectType.Node
                xObj = xDoc.Nodes(lRef)
        End Select
        Return xObj
        End Function
        Public Function GetOSMObjectHistoryFull(xType As OSMObject.ObjectType, lRef As Long) As OSMDoc
            Dim xWay As OSMWay
            Dim xNode As OSMNode
            Dim xDoc As OSMDoc
            Dim xDoc2 As OSMDoc
            Dim xRel As OSMRelation
            Dim xRel2 As OSMRelation
            Dim xWay2 As OSMWay
            Dim xMbr As OSMRelationMember
            Dim listWays As New OSMCollection(Of OSMWay)
            Dim listNodes As New OSMCollection(Of OSMNode)

            ' this just gets the history of the object itself, not the referenced ways and nodes
            xDoc = API.GetOSMObjectHistory(xType, lRef)

            'if we are looking for node history, or if we can't find the requested object, we are done
            If xType = OSMObject.ObjectType.Node Or IsNothing(xDoc) Then
                Return xDoc
            End If

            ' history of a relation means history of all its members as well
            If xType = OSMObject.ObjectType.Relation Then
                ' direct references from relations - all versions of the relation!
                xRel = xDoc.Relations(lRef)
                For Each xRel2 In xRel.Versions
                    For Each xMbr In xRel2.Members
                        If xMbr.Type = OSMObject.ObjectType.Way Then
                            If Not listWays.Contains(xMbr.Member.ID) Then
                                listWays.Add(xMbr.Member.ID, DirectCast(xMbr.Member, OSMWay))
                            End If
                        ElseIf xMbr.Type = OSMObject.ObjectType.Node Then
                            If Not listNodes.Contains(xMbr.Member.ID) Then
                                listNodes.Add(xMbr.Member.ID, DirectCast(xMbr.Member, OSMNode))
                            End If
                        End If
                    Next
                Next

            End If

            ' if we are looking for a way history, we will be needing to run through the versions of that way to get the node histories
            If xType = OSMObject.ObjectType.Way Then
                xWay = xDoc.Ways(lRef)
                listWays.Add(xWay.ID, xWay)
            End If

            ' fetch the way histories to find the nodes we will need
            For Each xWay In listWays.Values
                xDoc2 = API.GetOSMObjectHistory(OSMObject.ObjectType.Way, xWay.ID)
                xDoc.Merge(xDoc2)
                For Each xWay2 In xDoc.Ways(xWay.ID).Versions
                    For Each xNode In xWay2.Nodes
                        If Not listNodes.Contains(xNode.ID) Then
                            listNodes.Add(xNode.ID, xNode)
                        End If
                    Next
                Next
            Next

            ' for each way history required, we also need the history of each node in each version
            For Each xWay In listWays.Values
                For Each xWay2 In xDoc.Ways(xWay.ID).Versions
                    For Each xNode In xWay2.Nodes
                        If Not listNodes.Contains(xNode.ID) Then
                            listNodes.Add(xNode.ID, xNode)
                        End If
                    Next
                Next
            Next

        ' now fetch the node histories - don't bother if still at V1
        For Each xNode In listNodes.Values
            If xNode.Version > 1 Then
                xDoc2 = API.GetOSMObjectHistory(OSMObject.ObjectType.Node, xNode.ID)
                xDoc.Merge(xDoc2)
            End If
        Next

        Return xDoc
        End Function
        Public Function GetOSMObjectByTimestamp(d As DateTime) As OSMObject

        End Function
        Public Function GetOSMObjectVersion() As OSMObject

        End Function

    ' this uses xDoc as a cache and only fetches what is missing
    ' or stale?
    Public Function GetOSMObject(xDoc As OSMDoc, xType As OSMObject.ObjectType, lRef As Long, Optional bFull As Boolean = False) As OSMObject
        Dim xRel As OSMRelation, xWay As OSMWay, xNode As OSMNode
        Dim tmpDoc As OSMDoc
        Dim dCutoff As Date = Now - New TimeSpan(0, 0, MaxAge)
        Dim alNodes As New ArrayList()
        Dim alWays As New ArrayList()

        Select Case xType
            Case OSMObject.ObjectType.Node
                If Not xDoc.Nodes.Contains(lRef) Then
                    Try
                        tmpDoc = API.GetOSMDoc(xType, lRef, True)
                        xDoc.Merge(tmpDoc)
                    Catch e As OSMWebException
                    End Try
                Else
                    xNode = xDoc.Nodes(lRef)
                    If xNode.IsPlaceholder Or xNode.Cached < dCutoff Then
                        Try
                            tmpDoc = API.GetOSMDoc(xType, lRef, True)
                            xDoc.Merge(tmpDoc)
                        Catch e As OSMWebException
                        End Try
                    End If
                End If
                Return xDoc.Nodes(lRef)
            Case OSMObject.ObjectType.Way
                If Not xDoc.Ways.Contains(lRef) Then
                    Try
                        tmpDoc = API.GetOSMDoc(xType, lRef, True)
                        xDoc.Merge(tmpDoc)
                    Catch e As OSMWebException
                        MsgBox($"Error retrieving way {lRef}: {e.Message}")
                    End Try
                Else
                    xWay = xDoc.Ways(lRef)
                    If xWay.IsPlaceholder Or xWay.Cached < dCutoff Then
                        Try
                            tmpDoc = API.GetOSMDoc(xType, lRef, True)
                            If Not IsNothing(tmpDoc) Then
                                xDoc.Merge(tmpDoc)
                                xWay = xDoc.Ways(lRef)
                            End If
                        Catch e As OSMWebException
                            MsgBox($"Error retrieving way {lRef}: {e.Message}")
                        End Try
                    End If
                End If
                Return xDoc.Ways(lRef)
            Case OSMObject.ObjectType.Relation
                If Not xDoc.Relations.Contains(lRef) Then
                    Try
                        tmpDoc = API.GetOSMDoc(xType, lRef, False)
                        If Not IsNothing(tmpDoc) Then xDoc.Merge(tmpDoc)
                        xRel = xDoc.Relations(lRef)
                    Catch e As OSMWebException
                    End Try
                Else
                    xRel = xDoc.Relations(lRef)
                    If xRel.IsPlaceholder Or xRel.Cached < dCutoff Then
                        Try
                            tmpDoc = API.GetOSMDoc(xType, lRef, False)
                            If Not IsNothing(tmpDoc) Then xDoc.Merge(tmpDoc)
                            xRel = xDoc.Relations(lRef)
                        Catch e As OSMWebException
                        End Try
                    End If
                End If
                ' Collect lists of the members to be fetched
                If Not xRel Is Nothing Then
                    For Each xMbr As OSMRelationMember In xRel.Members
                        Select Case xMbr.Type
                            Case OSMObject.ObjectType.Node
                                If xMbr.IsPlaceholder Then
                                    alNodes.Add(xMbr.Member.ID)
                                End If
                            Case OSMObject.ObjectType.Way
                                If xMbr.IsPlaceholder Then
                                    alWays.Add(xMbr.Member.ID)
                                End If
                        End Select
                    Next
                    ' now use multifetch on the ways
                    For Each xMbr As OSMRelationMember In xRel.Members
                        Select Case xMbr.Type
                            Case OSMObject.ObjectType.Relation
                            ' don't fetch nested relations
                            Case OSMObject.ObjectType.Way
                                If xMbr.IsPlaceholder Then
                                    ' if this fails, keep it as a placeholder?
                                    ' if the way has been deleted, we need to refetch the relation and try again
                                    xMbr.Member = GetOSMObject(xDoc, xMbr.Type, xMbr.Member.ID)
                                End If
                            Case OSMObject.ObjectType.Node
                                If xMbr.IsPlaceholder Then
                                    ' if this fails, keep it as a placeholder?
                                    ' if the node has been deleted, we need to refetch the enclosing way or relation and try again
                                    xMbr.Member = GetOSMObject(xDoc, xMbr.Type, xMbr.Member.ID)
                                End If
                        End Select

                    Next
                End If
                Return xDoc.Relations(lRef)
            Case Else
                Return Nothing
        End Select
    End Function

    Public Function GetNeighbours(xDoc As OSMDoc, xType As OSMObject.ObjectType, lRef As Long) As OSMDoc
            Dim sUrl As String = API.GetUrl(xType, lRef, False)
        sUrl &= "/relations"
        Return API.GetOSM(sUrl)
        End Function
    End Class
