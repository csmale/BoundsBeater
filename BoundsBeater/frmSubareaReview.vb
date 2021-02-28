Imports OSMLibrary
Imports BoundsBeater.BoundaryDB
Imports System.Net

Public Class frmSubareaReview
    Private sBaseTitle As String
    Private sSubTitle As String
    Private iRel As Long
    Private ItemList() As BoundaryItem
    Private CurrentItem As Integer = 0
    Private CurrentHasChanges As Boolean
    Private xDbRelation As BoundaryItem
    Private MemberList As List(Of OSMRelationMember)
    ' Private TagList As List(Of String)
    Private xRetriever As New OSMRetriever
    Private xDoc As OSMDoc
    Private xObj As OSMRelation
    Private mbrsOSM As List(Of OSMRelationReviewItem)
    ' Private mbrsBDB As List(Of OSMRelationReviewItem)
    Private newOSM As List(Of OSMRelationReviewItem)
    Private rev As IOSMRelationReviewProvider
    Private cred As NetworkCredential
    Private u As OSMUpdater
    Public ChangesetComment As String
    Const MIN_COMMENT_LENGTH As Integer = 10
    'declare a global variable to store the column index
    Private lviCurrent As ListViewItem
    Private Shared colourAdded As Color = Color.Green
    Private Shared colourChanged As Color = Color.Yellow
    Private Shared colourDeleted As Color = Color.Red
    Private Shared colourLeave As Color = Color.White
    Const FinalSubitem As Integer = 3

    Public Sub New(Provider As IOSMRelationReviewProvider, db As BoundaryDB.BoundaryItem, Optional sTitle As String = "")

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        rev = Provider
        ItemList = {db}
        sBaseTitle = Me.Text
        sSubTitle = sTitle
        NextItem()
    End Sub
    Public Sub New(Provider As IOSMRelationReviewProvider, dblist() As BoundaryItem, Optional sTitle As String = "")
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        rev = Provider
        ItemList = dblist
        sBaseTitle = Me.Text
        sSubTitle = sTitle
        NextItem()
    End Sub
    Public Sub New(Provider As IOSMRelationReviewProvider, dblist As List(Of BoundaryItem), Optional sTitle As String = "")
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        rev = Provider
        ReDim ItemList(dblist.Count - 1)
        Dim i As Integer = 0
        For Each x In dblist
            ItemList(i) = x
            i = i + 1
        Next
        sBaseTitle = Me.Text
        sSubTitle = sTitle
        NextItem()
    End Sub
    Private Sub NextItem()
        xDbRelation = ItemList(CurrentItem)
        populate()
        Me.Text = $"{sBaseTitle} - Object {CurrentItem + 1} of {ItemList.Length}{sSubTitle}"
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If u?.ChangesetID = 0 Then
            u.Close()
        End If
        Me.Close()
    End Sub
    Private Sub AddMember(x As OSMRelationMember)
        If Not MemberList.Contains(x) Then MemberList.Add(x)
    End Sub

    Private Sub populate()
        Dim sTag As String
        If xDbRelation Is Nothing Then Return
        If xDbRelation.OSMRelation <= 0 Then Return
        iRel = xDbRelation.OSMRelation
        ' iRel = 4302675228

        mbrsOSM = New List(Of OSMRelationReviewItem)
        newOSM = New List(Of OSMRelationReviewItem)
        MemberList = New List(Of OSMRelationMember)
        CurrentHasChanges = False

        Try
            xDoc = xRetriever.API.GetOSMDoc(OSMObject.ObjectType.Relation, iRel, False)
            xObj = xDoc.Relations(iRel)
        Catch
            Return
        End Try

        ' header
        lblTitle.Text = $"Reviewing: {xObj.ID} {xObj.Name} Version {xObj.Version} dated {xObj.Timestamp} by {xObj.User}"

        ' collect existing roles from osm
        mbrsOSM.Clear()
        Dim iSeq As Integer = 0
        For Each t In xObj.Members
            iSeq = iSeq + 1
            Dim n As New OSMRelationReviewItem With {
                .Seq = iSeq,
                .ID = t.ID,
                .Type = t.Type,
                .Obj = t.Member,
                .OSMRole = t.Role,
                .SourceRole = OSMRelationReviewItem.OSMROLE_NA,
                .NewRole = t.Role
            }
            mbrsOSM.Add(n)
        Next

        ' entries derived from source db go into SourceRole etc
        ' new entries may be added, and existing entries may be marked for deletion
        Select Case rev.Process(xObj, xDbRelation, mbrsOSM)
            Case OSMReviewResult.OK
            Case OSMReviewResult.NoData
                MsgBox("No database entry for Subarea Review")
                Return
            Case OSMReviewResult.WrongType
                MsgBox("Wrong object type for Subarea Review")
                Return
        End Select

        ' here be magic - merge the osm data with the source data
        For Each mbr In mbrsOSM
            ' start with current data
            mbr.NewRole = mbr.OSMRole
            ' merge in data from source DB
            If mbr.SourceRole <> OSMRelationReviewItem.OSMROLE_NA Then
                mbr.NewRole = mbr.SourceRole
            End If
        Next

        ' TagList.Sort()

        ' add a row for each tag
        With lvTagList.Items
            .Clear()
            For Each mbr In mbrsOSM
                LoadItem(.Add(""), mbr)
            Next
        End With
    End Sub

    Private Sub LoadItem(lvi As ListViewItem, mbr As OSMRelationReviewItem)
        With lvi
            .Text = mbr.Seq.ToString()
            .Tag = mbr
            .Checked = False
            .UseItemStyleForSubItems = False
            If .SubItems.Count = 0 Then
                For i = 1 To .ListView.Columns.Count
                    .SubItems.Add("")
                Next
            End If
            .SubItems(colType.Index).Text = OSMObject.ObjectTypeChar(mbr.Type)
            .SubItems(colID.Index).Text = mbr.ID.ToString
            .SubItems(colOSM.Index).Text = mbr.OSMRole
            .SubItems(colSource.Index).Text = mbr.SourceRole
            With .SubItems(colOSMNew.Index)
                .Text = mbr.NewRole
                If mbr.OSMRole = OSMRelationReviewItem.OSMROLE_NA Then
                    .BackColor = colourAdded
                ElseIf mbr.SourceRole = OSMRelationReviewItem.OSMROLE_DELETE Then
                    .BackColor = colourDeleted
                ElseIf mbr.NewRole <> mbr.OSMRole Then
                    .BackColor = colourChanged
                Else
                    .BackColor = colourLeave
                End If
            End With
            .SubItems(colComments.Index).Text = ""
        End With
    End Sub
    Private Sub frmSubareaReview_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtChangesetComment.Text = ChangesetComment
        NextItem()
    End Sub

    Private Function GetNewVersion() As OSMObject
        Dim xNew As OSMObject = xObj.Clone()
        Dim sTag As String, sValue As String
        Dim xMbr As OSMRelationMember
        Dim xNewObj As OSMObject
        Dim xItem As OSMRelationReviewItem
        Dim bChanges As Boolean = False
        ' now update the members from the listview
        For Each li As ListViewItem In lvTagList.Items
            If Not li.Checked Then Continue For
            If li.Tag Is Nothing Then Continue For
            xItem = DirectCast(li.Tag, OSMRelationReviewItem)
            If xItem.ID < 0 Then
                xNewObj = xDoc.GetOSMObject(xItem.Type, xItem.ID)
                xMbr = New OSMRelationMember(xNewObj, xItem.NewRole)
                xObj.Members.Add(xMbr)
                bChanges = True
            Else
                xMbr = xObj.Members(xItem.Seq)
                If xItem.NewRole <> xItem.OSMRole Then
                    xMbr.Role = xItem.NewRole
                    bChanges = True
                End If
            End If
        Next
        ' second scan for deleting so as not to upset the sequence numbers
        For Each li As ListViewItem In lvTagList.Items
            If Not li.Checked Then Continue For
            If li.Tag Is Nothing Then Continue For
            xItem = DirectCast(li.Tag, OSMRelationReviewItem)

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
            txtChangesetComment.Enabled = False
        End If

        ' submit the update
        xNew.Changeset = CType(u.ChangesetID, ULong)
        bSuccess = u.Update(xNew)

        If bSuccess Then
            u.Close()
            Me.Close()
        End If
    End Sub

    Private Sub lvTagList_MouseUp(sender As Object, e As MouseEventArgs) Handles lvTagList.MouseUp
        If e.Button <> MouseButtons.Right Then Return
        Dim CurrentSB As ListViewItem.ListViewSubItem
        ' check where clicked
        lviCurrent = lvTagList.GetItemAt(e.X, e.Y)     ' which listviewitem was clicked
        If lviCurrent Is Nothing Then Return
        CurrentSB = lviCurrent.GetSubItemAt(e.X, e.Y)  ' which subitem was clicked
        If CurrentSB Is Nothing Then Return
        cmsTagAction.Show(MousePosition)
    End Sub

    Private Sub cmsiCustom_Click(sender As Object, e As EventArgs) Handles cmsiCustom.Click
        If lviCurrent Is Nothing Then Return
        Dim sTmp As String
        sTmp = InputBox($"Enter new custom value for {lviCurrent.Text}", $"Tag: {lviCurrent.Text}", lviCurrent.SubItems(FinalSubitem).Text)
        If Len(sTmp) = 0 Then Return
        Dim sTag As String = lviCurrent.Text
        Dim sVal As String
#If False Then
        If mbrsOSM.ContainsKey(sTag) Then
            sVal = mbrsOSM(sTag)
        Else
            sVal = ""
        End If

        With lviCurrent.SubItems(FinalSubitem)
            If sVal = "" Then
                .BackColor = colourAdded
                lviCurrent.Checked = True
            ElseIf sVal = sTmp Then
                .BackColor = colourLeave
                lviCurrent.Checked = False
            Else
                .BackColor = colourChanged
                lviCurrent.Checked = True
            End If
            .Text = sTmp
            newOSM(sTag).Role = sTmp
        End With
#End If
    End Sub

    Private Sub cmsiDeleteTag_Click(sender As Object, e As EventArgs) Handles cmsiDeleteTag.Click
        If lviCurrent Is Nothing Then Return
        Dim sTag As String = lviCurrent.Text
        Dim sVal As String
#If False Then
        If tagsOSM.ContainsKey(sTag) Then
            sVal = tagsOSM(sTag)
        Else
            sVal = ""
        End If
        With lviCurrent.SubItems(FinalSubitem)
            If sVal = "" Then
                .BackColor = colourLeave
                lviCurrent.Checked = False
            Else
                .BackColor = colourDeleted
                lviCurrent.Checked = True
            End If
            .Text = ""
            newOSM(sTag).Role = ""
        End With
#End If
    End Sub

    Private Sub cmsiKeepOsm_Click(sender As Object, e As EventArgs) Handles cmsiKeepOsm.Click
        If lviCurrent Is Nothing Then Return
        Debug.Assert(lviCurrent IsNot Nothing)
        Dim xItem = DirectCast(lviCurrent.Tag, OSMRelationReviewItem)
        With lviCurrent.SubItems(colOSMNew.Index)
            xItem.NewRole = xItem.OSMRole
            .Text = lviCurrent.SubItems(colOSM.Index).Text
            .BackColor = colourLeave
        End With
        lviCurrent.Checked = False
    End Sub

    Private Sub cmsiTakeSource_Click(sender As Object, e As EventArgs) Handles cmsiTakeSource.Click
        If lviCurrent Is Nothing Then Return
        Debug.Assert(lviCurrent IsNot Nothing)
        Dim xItem = DirectCast(lviCurrent.Tag, OSMRelationReviewItem)
        With lviCurrent.SubItems(colOSMNew.Index)
            .Text = lviCurrent.SubItems(colSource.Index).Text
            If .Text <> lviCurrent.SubItems(colSource.Index).Text Then
                .BackColor = colourLeave
            End If
        End With
        lviCurrent.Checked = False
    End Sub
    Private Function MKey(obj As OSMObject) As String
        Return obj.ObjectTypeChar() & obj.ID.ToString()
    End Function
End Class
