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
    Private newBDB As New Dictionary(Of String, String)

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
        If xDbRelation.BoundaryType = BoundaryItem.BoundaryTypes.BT_CivilParish Or
            xDbRelation.BoundaryType = BoundaryItem.BoundaryTypes.BT_Community Then
            AddTag("parish_type")
        End If

        ' tags from the current OSM relation
        If Not IsNothing(xRel) Then
            For Each t In xRel.Tags.Keys
                AddTag(t)
            Next
        End If

        ' tags from osm
        tagsOSM.Clear()
        newOSM.Clear()
        For Each t In xRel.Tags.Keys
            tagsOSM.Add(t, xRel.Tag(t))
            newOSM.Add(t, xRel.Tag(t))
        Next

        ' tags derived from database
        tagsBDB.Clear()
        newBDB.Clear()
        tagsBDB("type") = "boundary"
        tagsBDB("boundary") = "administrative"
        tagsBDB("name") = xDbRelation.Name
        If Len(xDbRelation.Name2) > 0 Then
            tagsBDB("name:en") = xDbRelation.Name
            tagsBDB("name:cy") = xDbRelation.Name2
        End If
        tagsBDB("designation") = BoundaryItem.BoundaryType_ToString(xDbRelation.BoundaryType)
        If xDbRelation.CouncilStyle <> BoundaryItem.CouncilStyles.CS_Default Then
            tagsBDB("council_style") = BoundaryItem.CouncilStyle_ToString(xDbRelation.CouncilStyle)
        End If
        If xDbRelation.IsBorough Then tagsBDB("borough") = "1"
        If xDbRelation.IsRoyal Then tagsBDB("royal") = "1"
        Select Case xDbRelation.BoundaryType
            Case BoundaryItem.BoundaryTypes.BT_CivilParish
                tagsBDB("admin_level") = "10"
                Select Case xDbRelation.ParishType
                    Case BoundaryItem.ParishTypes.PT_ParishCouncil
                        tagsBDB("parish_type") = "parish_council"
                    Case BoundaryItem.ParishTypes.PT_ParishMeeting
                        tagsBDB("parish_type") = "parish_meeting"
                    Case BoundaryItem.ParishTypes.PT_JointParishCouncil
                        tagsBDB("parish_type") = "joint_parish_council"
                    Case BoundaryItem.ParishTypes.PT_JointParishMeeting
                        tagsBDB("parish_type") = "joint_parish_meeting"
                End Select
            Case BoundaryItem.BoundaryTypes.BT_CeremonialCounty
                tagsBDB("boundary") = "ceremonial"
            Case BoundaryItem.BoundaryTypes.BT_Community
                tagsBDB("admin_level") = "10"
            Case BoundaryItem.BoundaryTypes.BT_Country
                tagsBDB("admin_level") = "2"
            Case BoundaryItem.BoundaryTypes.BT_Liberty
                tagsBDB("admin_level") = "9"
            Case BoundaryItem.BoundaryTypes.BT_LondonBorough
                tagsBDB("admin_level") = "8"
            Case BoundaryItem.BoundaryTypes.BT_MetroCounty
                tagsBDB("admin_level") = "6"
            Case BoundaryItem.BoundaryTypes.BT_MetroDistrict
                tagsBDB("admin_level") = "8"
            Case BoundaryItem.BoundaryTypes.BT_NIreDistrict
                tagsBDB("admin_level") = "8"
            Case BoundaryItem.BoundaryTypes.BT_Nation
                tagsBDB("admin_level") = "4"
            Case BoundaryItem.BoundaryTypes.BT_ParishGroup
                tagsBDB("admin_level") = "10"
            Case BoundaryItem.BoundaryTypes.BT_PreservedCounty
                tagsBDB("boundary") = "ceremonial"
            Case BoundaryItem.BoundaryTypes.BT_PrincipalArea
                tagsBDB("admin_level") = "6"
            Case BoundaryItem.BoundaryTypes.BT_Region
                tagsBDB("admin_level") = "5"
            Case BoundaryItem.BoundaryTypes.BT_ScotCouncil
                tagsBDB("admin_level") = "6"
            Case BoundaryItem.BoundaryTypes.BT_SuiGeneris
                tagsBDB("admin_level") = "6"
            Case BoundaryItem.BoundaryTypes.BT_Unitary
                tagsBDB("admin_level") = "6"
            Case BoundaryItem.BoundaryTypes.BT_Unknown
            Case Else
        End Select

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
                lvi.SubItems.Add(newOSM(sTag))
            Else
                lvi.SubItems.Add("")
            End If
            If newBDB.ContainsKey(sTag) Then
                lvi.SubItems.Add(newBDB(sTag))
            Else
                lvi.SubItems.Add("")
            End If
        Next
    End Sub

    Private Sub frmReview_Load(sender As Object, e As EventArgs) Handles Me.Load
        populate()
    End Sub
End Class
