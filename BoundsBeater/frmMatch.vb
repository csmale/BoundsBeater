Imports OSMLibrary
Imports System.Text.RegularExpressions

Public Class frmMatch
    Public oOSMDoc As OSMDoc    ' holds our OSM universe
    Public oRel As OSMRelation  ' the OSM releation we have found which we want to match to a real admin area
    Public oDB As BoundaryDB
    Public oMatches As List(Of BoundaryDB.BoundaryItem)
    Public oMatch As BoundaryDB.BoundaryItem
    Public sComment As String

    ''' <summary>
    ''' Handles Click event on Cancel button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        oMatch = Nothing
        Me.Close()
    End Sub

    ''' <summary>
    ''' Handles Click event on OK button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Dim sGSS As String
        If lvAreas.SelectedItems.Count <> 1 Then
            Exit Sub
        End If
        sGSS = CType(lvAreas.SelectedItems(0).Tag, BoundaryDB.BoundaryItem).ONSCode
        If sGSS <> "" AndAlso lblGSS.Text <> "" AndAlso sGSS <> lblGSS.Text Then
            ' GSS code is different!!!
            If MsgBox(String.Format("GSS code is different: Database has {0}, selected has {1}. Update database?", lblGSS.Text, sGSS), MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                CType(lvAreas.SelectedItems(0).Tag, BoundaryDB.BoundaryItem).ONSCode = sGSS
            End If
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
        oMatch = lvAreas.SelectedItems(0).Tag
        Me.Close()
    End Sub

    ''' <summary>
    ''' Handles Shown event on Match form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub frmMatch_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Debug.Assert(Not (oOSMDoc Is Nothing))
        Debug.Assert(Not (oRel Is Nothing))
        Debug.Assert(Not (oMatches Is Nothing))

        lblName.Text = oRel.Name
        lblBoundaryType.Text = oRel.Tag("boundary")
        lblCouncilName.Text = oRel.Tag("council_name")
        lblGSS.Text = oRel.Tag("ref:gss")
        lblParishType.Text = oRel.Tag("parish_type")

        If oRel.ID = 0 Then
            llblOSMID.Text = ""
        Else
            llblOSMID.Text = oRel.ID.ToString()
            llblOSMID.Links(0).LinkData = oRel.BrowseURL
        End If
        lblLevel.Text = oRel.Tag("admin_level")
        lblDesignation.Text = oRel.Tag("designation")
        lblComment.Text = sComment
        btnOK.Enabled = False
        btnForget.Enabled = False
        For Each bi As BoundaryDB.BoundaryItem In oMatches
            AddMatch(bi)
        Next

        If oMatches.Count > 0 Then
            ' see if there is one which matches the osmid
            For Each i As ListViewItem In lvAreas.Items
                If CType(i.Tag, BoundaryDB.BoundaryItem).OSMRelation = oRel.ID Then
                    i.Selected = True
                    Exit For
                End If
            Next
            ' see if there is one which matches the GSS polygon identifier
            If lblGSS.Text <> "" Then
                For Each i As ListViewItem In lvAreas.Items
                    If CType(i.Tag, BoundaryDB.BoundaryItem).ONSCode = lblGSS.Text Then
                        i.Selected = True
                        Exit For
                    End If
                Next
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles Click event on Abort button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnAbort_Click(sender As Object, e As EventArgs) Handles btnAbort.Click
        Me.DialogResult = Windows.Forms.DialogResult.Ignore
        oMatch = Nothing
        Me.Close()
    End Sub

    Private Sub lvAreas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvAreas.SelectedIndexChanged
        btnOK.Enabled = (lvAreas.SelectedItems.Count = 1)
        btnForget.Enabled = btnOK.Enabled
    End Sub

    Private Sub SearchFor(sText As String)
        If IsNothing(oDB) Then Exit Sub
        Dim re As Regex = New Regex(sText, RegexOptions.IgnoreCase)
        lvAreas.Items.Clear()
        For Each bi As BoundaryDB.BoundaryItem In oDB.Items.Values
            If Not IsNothing(bi.Name) AndAlso re.IsMatch(bi.Name) Then
                AddMatch(bi)
            End If
        Next
    End Sub
    Private Sub AddMatch(bi As BoundaryDB.BoundaryItem)
        With lvAreas.Items.Add(bi.Name)
            .Tag = bi
            .SubItems.Add(bi.TypeCode)
            .SubItems.Add(bi.OSMRelation.ToString())
            .SubItems.Add(bi.ONSCode)
            If Not IsNothing(bi.Parent) Then
                .SubItems.Add(bi.Parent.Name)
            End If
        End With
    End Sub

    Private Sub txtMatcher_KeyUp(sender As Object, e As KeyEventArgs) Handles txtMatcher.KeyUp
        If txtMatcher.TextLength > 0 Then
            SearchFor(txtMatcher.Text)
        End If
    End Sub

    Private Sub btnForget_Click(sender As Object, e As EventArgs) Handles btnForget.Click
        Dim xItem As BoundaryDB.BoundaryItem
        xItem = lvAreas.SelectedItems(0).Tag
        xItem.OSMRelation = 0
        xItem.SetIDinXML()
        lvAreas.SelectedItems(0).SubItems(2).Text = "0"
    End Sub

    Private Sub llblOSMID_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llblOSMID.LinkClicked
        Process.Start(e.Link.LinkData.ToString)
    End Sub
End Class