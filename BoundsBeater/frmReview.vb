Imports OSMLibrary

Public Class frmReview
    Private iRel As Long
    Private xDbRelation As BoundaryDB.BoundaryItem
    Private TagList As New List(Of String)
    Private xRetriever As New OSMRetriever
    Private xDoc As OSMDoc
    Private xRel As OSMRelation

    Public Sub New(db As BoundaryDB.BoundaryItem)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        xDbRelation = db
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub AddTag(tag As String)
        If Not TagList.Contains(tag) Then TagList.Add(tag)
    End Sub

    Private Sub populate()
        Dim sTag As String
        Dim sTmp As String
        If xDbRelation Is Nothing Then Return
        If xDbRelation.OSMRelation <= 0 Then Return
        Try
            xDoc = xRetriever.API.GetOSMDoc(OSMObject.ObjectType.Relation, xDbRelation.OSMRelation, False)
            xRel = xDoc.Relations(xDbRelation.OSMRelation)
        Catch
            Return
        End Try

        ' tags we will need for certain
        AddTag("type")
        AddTag("boundary")
        AddTag("admin_level")
        AddTag("designation")
        AddTag("name")
        AddTag("council_name")
        AddTag("council_style")
        AddTag("borough")
        AddTag("royal")

        ' tags dependent on the type of boundary
        If xDbRelation.BoundaryType = BoundaryDB.BoundaryItem.BoundaryTypes.BT_CivilParish Or
            xDbRelation.BoundaryType = BoundaryDB.BoundaryItem.BoundaryTypes.BT_Community Then
            AddTag("parish_type")
        End If

        ' tags from the current OSM relation
        If Not IsNothing(xRel) Then
            For Each t In xRel.Tags.Keys
                AddTag(t)
            Next
        End If

        TagList.Sort()

        ' add a row for each tag
        With lvTagList.Items
            .Clear()
            For Each sTag In TagList
                .Add(sTag)
            Next
        End With

        ' fill in the current, database, and suggested value
        For Each lvi As ListViewItem In lvTagList.Items
            sTag = lvi.Text
            Select Case sTag
                Case "admin_level" : sTmp = xDbRelation.AdminLevel.ToString()
                Case "boundary" : sTmp = "boundary"
                Case "name" : sTmp = xDbRelation.Name
                Case "designation" : sTmp = xDbRelation.ToString
                Case Else
                    sTmp = ""
            End Select
            lvi.SubItems.Add(sTmp)
            lvi.SubItems.Add(xRel.Tag(sTag))
        Next
    End Sub

    Private Sub frmReview_Load(sender As Object, e As EventArgs) Handles Me.Load
        populate()
    End Sub
End Class
