Imports OSMLibrary
Imports BoundsBeater.BoundaryDB

Public Class frmReview
    Private iRel As Long
    Private xDbRelation As BoundaryItem
    Private TagList As New List(Of String)
    Private xRetriever As New OSMRetriever
    Private xDoc As OSMDoc
    Private xRel As OSMRelation
    Private tagsOSM As New Dictionary(Of String, String)
    Private tagsBDB As New Dictionary(Of String, String)
    Private newOSM As New Dictionary(Of String, String)
    Private rev As New BoundaryDBReviewProvider

    Public Sub New(db As BoundaryDB.BoundaryItem)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        xDbRelation = db
        rev.dbi = db
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub AddTag(tag As String)
        If Not TagList.Contains(tag) Then TagList.Add(tag)
    End Sub

    Private Sub populate()
        Dim sTag As String
        If xDbRelation Is Nothing Then Return
        If xDbRelation.OSMRelation <= 0 Then Return
        Try
            xDoc = xRetriever.API.GetOSMDoc(OSMObject.ObjectType.Relation, xDbRelation.OSMRelation, False)
            xRel = xDoc.Relations(xDbRelation.OSMRelation)
        Catch
            Return
        End Try

        ' tags from the current OSM relation
        If Not IsNothing(xRel) Then
            For Each t In xRel.Tags.Keys
                AddTag(t)
            Next
        End If

        ' tags from osm
        tagsOSM.Clear()
        For Each t In xRel.Tags.Keys
            tagsOSM.Add(t, xRel.Tag(t))
        Next

        ' tags derived from source db
        Select Case rev.Process(xRel, tagsBDB)
            Case OSMReviewResult.OK
            Case OSMReviewResult.NoData
            Case OSMReviewResult.WrongType
        End Select

        ' merge in tags from the source db
        For Each t In tagsBDB.Keys
            AddTag(t)
        Next

        ' here be magic - merge the osm data with the source data
        For Each sTag In TagList
            ' start with current data
            If tagsOSM.Keys.Contains(sTag) Then
                newOSM(sTag) = tagsOSM(sTag)
            End If
            ' merge in data from source DB
            If tagsBDB.Keys.Contains(sTag) Then
                newOSM(sTag) = tagsBDB(sTag)
            End If
        Next

        TagList.Sort()

        ' add a row for each tag
        With lvTagList.Items
            .Clear()
            For Each sTag In TagList
                With .Add(sTag)
                    .UseItemStyleForSubItems = False
                End With
            Next
        End With

        ' fill in the current, database, and suggested value
        For Each lvi As ListViewItem In lvTagList.Items
            sTag = lvi.Text
            If tagsOSM.ContainsKey(sTag) Then
                lvi.SubItems.Add(tagsOSM(sTag))
            Else
                lvi.SubItems.Add("")
            End If
            If tagsBDB.ContainsKey(sTag) Then
                lvi.SubItems.Add(tagsBDB(sTag))
            Else
                lvi.SubItems.Add("")
            End If
            If newOSM.ContainsKey(sTag) Then
                With lvi.SubItems.Add(newOSM(sTag))
                    If tagsOSM.Keys.Contains(sTag) Then
                        If .Text <> tagsOSM(sTag) Then
                            lvi.Checked = True
                            If .Text = "" Then
                                .BackColor = Color.Red
                            Else
                                .BackColor = Color.Yellow
                            End If
                        End If
                    Else
                        lvi.Checked = True
                        .BackColor = Color.LightGreen
                    End If
                End With
            Else
                lvi.SubItems.Add("")
            End If
        Next
    End Sub

    Private Sub frmReview_Load(sender As Object, e As EventArgs) Handles Me.Load
        populate()
    End Sub

    Private Sub btnCommit_Click(sender As Object, e As EventArgs) Handles btnCommit.Click
        MsgBox("commit")
    End Sub
End Class
