Imports OSMLibrary
Imports BoundsBeater.BoundaryDB
Imports System.Net

Public Class frmReview
    Private sBaseTitle As String
    Private iRel As Long
    Private ItemList() As BoundaryItem
    Private CurrentItem As Integer = 0
    Private xDbRelation As BoundaryItem
    Private TagList As List(Of String)
    Private xRetriever As New OSMRetriever
    Private xDoc As OSMDoc
    Private xObj As OSMObject
    Private tagsOSM As Dictionary(Of String, String)
    Private tagsBDB As Dictionary(Of String, String)
    Private newOSM As Dictionary(Of String, String)
    Private rev As New BoundaryDBReviewProvider
    Private cred As NetworkCredential
    Private u As OSMUpdater
    Public ChangesetComment As String
    Const MIN_COMMENT_LENGTH As Integer = 10

    Public Sub New(db As BoundaryDB.BoundaryItem)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ItemList = {db}
        sBaseTitle = Me.Text
        NextItem()
    End Sub
    Public Sub New(dblist() As BoundaryItem)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ItemList = dblist
        sBaseTitle = Me.Text
        NextItem()
    End Sub
    Public Sub New(dblist As List(Of BoundaryItem))
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ReDim ItemList(dblist.Count - 1)
        Dim i As Integer = 0
        For Each x In dblist
            ItemList(i) = x
            i = i + 1
        Next
        sBaseTitle = Me.Text
        NextItem()
    End Sub
    Private Sub NextItem()
        xDbRelation = ItemList(CurrentItem)
        rev.dbi = ItemList(CurrentItem)
        populate()
        btnNext.Enabled = CurrentItem < (ItemList.Length - 1)
        Me.Text = $"{sBaseTitle} - Object {CurrentItem + 1} of {ItemList.Length}"
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If u?.ChangesetID = 0 Then
            u.Close()
        End If
        Me.Close()
    End Sub
    Private Sub AddTag(tag As String)
        If Not TagList.Contains(tag) Then TagList.Add(tag)
    End Sub

    Private Sub populate()
        Dim sTag As String
        If xDbRelation Is Nothing Then Return
        If xDbRelation.OSMRelation <= 0 Then Return
        iRel = xDbRelation.OSMRelation
        ' iRel = 4302675228

        tagsOSM = New Dictionary(Of String, String)
        tagsBDB = New Dictionary(Of String, String)
        newOSM = New Dictionary(Of String, String)
        TagList = New List(Of String)

        Try
            xDoc = xRetriever.API.GetOSMDoc(OSMObject.ObjectType.Relation, iRel, False)
            xObj = xDoc.Relations(iRel)
        Catch
            Return
        End Try

        ' header
        lblTitle.Text = $"Reviewing: {xObj.ID} {xObj.Name} Version {xObj.Version} dated {xObj.Timestamp} by {xObj.User}"

        ' tags from the current OSM relation
        If Not IsNothing(xObj) Then
            For Each t In xObj.Tags.Keys
                AddTag(t)
            Next
        End If

        ' tags from osm
        tagsOSM.Clear()
        For Each t In xObj.Tags.Keys
            tagsOSM.Add(t, xObj.Tag(t))
        Next

        ' tags derived from source db
        Select Case rev.Process(xObj, tagsBDB)
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
                            If .Text = "" Then
                                .BackColor = Color.Red
                            Else
                                .BackColor = Color.Yellow
                            End If
                        End If
                    Else
                        If Len(newOSM(sTag)) > 0 Then
                            lvi.Checked = True
                            .BackColor = Color.LightGreen
                        End If
                    End If
                End With
            Else
                lvi.SubItems.Add("")
            End If
        Next
    End Sub

    Private Sub frmReview_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtChangesetComment.Text = ChangesetComment
        btnNewChangeset.Enabled = False
        NextItem()
    End Sub

    Private Function GetNewVersion() As OSMObject
        Dim xNew As OSMObject = xObj.Clone()
        Dim sTag As String, sValue As String
        Dim bChanges As Boolean = False
        ' now update the tags from the listview
        For Each li As ListViewItem In lvTagList.Items
            If Not li.Checked Then Continue For
            sTag = li.Text
            If newOSM.ContainsKey(sTag) Then
                sValue = newOSM(sTag)
                If Len(sValue) > 0 Then
                    If xNew.Tags.ContainsKey(sTag) Then
                        If xNew.Tags(sTag).Value <> sValue Then
                            xNew.Tags(sTag).Value = sValue
                            bChanges = True
                        End If
                    Else
                        xNew.Tags.Add(sTag, New OSMTag(sTag, sValue))
                        bChanges = True
                    End If
                Else
                    If xNew.Tags.ContainsKey(sTag) Then
                        xNew.Tags.Remove(sTag)
                        bChanges = True
                    End If
                End If
            End If
        Next
        If bChanges Then
            Return xNew
        Else
            Return Nothing
        End If
    End Function

    Private Sub btnCommit_Click(sender As Object, e As EventArgs) Handles btnCommit.Click
        Dim bSuccess As Boolean
        Dim xNew As OSMObject = GetNewVersion()
        If xNew Is Nothing Then
            MsgBox("No changes, nothing to save")
            Return
        End If

        If Len(txtChangesetComment.Text) < MIN_COMMENT_LENGTH Then
            MsgBox($"Please enter a changeset comment of at least {MIN_COMMENT_LENGTH} characters")
            Return
        End If

        ' if this is the first change in the batch, get a new updater
        If u Is Nothing Then
            cred = New NetworkCredential(OSMLibrary.My.MySettings.Default.APIUser,
                                         OSMLibrary.My.MySettings.Default.APIPassword)
            u = New OSMUpdater(cred)
        End If

        ' open a new changeset if required
        If u.ChangesetID = 0 Then
            If u.Open(txtChangesetComment.Text) = 0 Then
                MsgBox($"Unable to create changeset: {u.api.LastError}")
                Return
            End If
            Debug.Print($"Changeset {u.ChangesetID} opened")
            btnNewChangeset.Enabled = True
            txtChangesetComment.Enabled = False
        End If

        ' submit the update
        xNew.Changeset = CType(u.ChangesetID, ULong)
        bSuccess = u.Update(xNew)

        If bSuccess Then
            If btnNext.Enabled Then
                CurrentItem = CurrentItem + 1
                NextItem()
            Else
                u.Close()
                Me.Close()
            End If
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        CurrentItem = CurrentItem + 1
        NextItem()
    End Sub

    Private Sub btnNewChangeset_Click(sender As Object, e As EventArgs) Handles btnNewChangeset.Click
        txtChangesetComment.Enabled = True
        If u IsNot Nothing AndAlso u.ChangesetID > 0 Then
            btnNewChangeset.Enabled = False
            u.Close()
        End If
    End Sub
End Class
