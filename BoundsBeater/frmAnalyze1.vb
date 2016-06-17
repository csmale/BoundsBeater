Imports OSMLibrary
Imports System.Environment
Imports System.Runtime.InteropServices.ComTypes
Imports Microsoft.VisualBasic.FileIO
Imports ProtoBuf.Meta
Imports System.ComponentModel

Public Class frmAnalyze
    Private m_SortingColumn As ColumnHeader

    Dim tmpDoc As New OSMDoc
    Dim xRetriever As New OSMRetriever
    Dim bMapInitDone As Boolean = False
    Dim sDBPath As String
    Dim sCache As String = ""
    Dim xDB As BoundaryDB
    Dim fntNormal As Font
    Dim fntBold As Font
    Dim bInRightClick As Boolean = False
    Dim x As New System.Data.SQLite.SQLiteConnection()

    Private Sub testpbf()
        Dim x As New OSMPBF.Blob
        Dim sc As New ProtoBuf.SerializationContext
        'Dim tm As New TypeModel()
        'Dim z As New ProtoBuf.ProtoReader(IStream, t, sc)
    End Sub

    ''' <summary>
    ''' Handles Click event on Close button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If Len(sCache) > 0 Then
            tmpDoc.SaveXML(sCache)
        End If
        Me.Close()
    End Sub

    ''' <summary>
    ''' Handles Click event on Single Report button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnSingle_Click(sender As Object, e As EventArgs) Handles btnSingle.Click
        Dim iRel As ULong
        Dim sTmp As String = Me.txtSingle.Text
        If Len(sTmp) < 1 Or Len(sTmp) > 10 Then Exit Sub
        If Not ULong.TryParse(sTmp, iRel) Then Exit Sub
        If iRel < 1 Then Exit Sub
        Dim xAPI As New OSMApi
        Dim xDoc As New OSMDoc
        Dim xRel As OSMObject
        Dim xCache As New OSMHistoryCache
        Dim xHObj As OSMHistoryCache.OSMHistoryObject

        xRel = xCache.OSMNodeHistory(iRel)

        xRel = xRetriever.GetOSMObjectHistory(OSMObject.ObjectType.Node, iRel, True)

        Dim xWay As OSMWay
        'xWay = xAPI.GetOSMObjectHistory(OSMObject.ObjectType.Way, iRel)

        '        ShowRelation(iRel)
    End Sub

    ''' <summary>
    ''' Shows a boundary on the map, with some information in a text box
    ''' </summary>
    ''' <param name="iRel">The OSM ID of the selecte relation</param>
    Private Sub ShowRelation(iRel As ULong)
        Dim xRel As OSMRelation
        Dim xRes As OSMResolver
        Dim sLine As String
        Dim sTmp As String = ""

        '        txtReport.Clear()
        xRetriever.MaxAge = 60 * 15
        xRel = xRetriever.GetOSMObject(tmpDoc, OSMObject.ObjectType.Relation, iRel)
        If IsNothing(xRel) Then
            xRel = Nothing
            txtReport.Text = "Unable to retrieve relation #" & iRel
            Exit Sub
        End If
        sTmp = "Loaded " & xRel.Name() & ", OSM Relation " & iRel.ToString
        tsStatus.Text = sTmp

        Dim bHasWays As Boolean = False
        For Each xMbr As OSMRelationMember In xRel.Members
            If xMbr.Type = OSMObject.ObjectType.Way Then
                bHasWays = True
                Exit For
            End If
        Next
        If Not bHasWays Then
            sLine = "Relation has no ways"
            sTmp = sTmp & vbCrLf & sLine
            txtReport.Text = txtReport.Text & sTmp
            Return
        End If

        ShowOnMap(xRel)
        For Each x As OSMTag In xRel.Tags.Values
            sLine = x.Key & " = " & x.Value
            sTmp = sTmp & vbCrLf & sLine
        Next
        txtReport.Text = sTmp
        sTmp = vbCrLf & "Analysing ways..."
        Dim xWays As New List(Of OSMWay)
        For Each xMbr As OSMRelationMember In xRel.Members
            If xMbr.Type = OSMObject.ObjectType.Way Then
                If xWays.Contains(xMbr.Member) Then
                    sLine = "Relation contains duplicate Way #" & xMbr.Member.ID
                    sTmp = sTmp & vbCrLf & sLine
                Else
                    xWays.Add(xMbr.Member)
                End If
            End If
        Next
        txtReport.Text = txtReport.Text & sTmp
        sTmp = "Relation contains " & xWays.Count & " ways."
        txtReport.Text = txtReport.Text & sTmp
        sTmp = vbCrLf & "Resolving polygons..."
        xRes = New OSMResolver(xRel)
        sLine = xRes.Rings.Count & " ring(s) found"
        sTmp = sTmp & vbCrLf & sLine
        For i As Integer = 0 To xRes.Rings.Count - 1
            Dim xRing As OSMResolver.Ring = xRes.Rings(i)
            sLine = "Ring #" & i & " (" & xRing.Ways.Count & " ways): "
            sLine = sLine & xRing.Role
            If xRing.isClosed Then
                sLine = sLine & " (closed)"
            Else
                sLine = sLine & " (NOT closed)"
            End If
            sTmp = sTmp & vbCrLf & sLine
        Next
        txtReport.Text = txtReport.Text & sTmp
        Dim b As BBox = xRel.BBox
        sTmp = "BBox [" & b.MinLon & "," & b.MinLat & "," & b.MaxLon & "," & b.MaxLat & "]"
        txtReport.Text = txtReport.Text & vbCrLf & sTmp
        sTmp = "http://overpass-api.de/api/xapi?relation[type=boundary][bbox=" _
            & b.MinLon & "," & b.MinLat & "," & b.MaxLon & "," & b.MaxLat _
            & "][@meta]"
        txtReport.Text = txtReport.Text & vbCrLf & sTmp
    End Sub


    Private Sub LoadTreeView(tv As TreeView, xParentNode As TreeNode, xDB As BoundaryDB, xItem As BoundaryDB.BoundaryItem, ByRef iTotal As Integer, ByRef iCount As Integer)
        Dim tvn As TreeNode
        Dim xChild As BoundaryDB.BoundaryItem
        Dim sName As String

        If IsNothing(fntBold) Then
            fntNormal = tvList.Font
            fntBold = New Font(tvList.Font, FontStyle.Bold)
        End If

        sName = xItem.TypeCode & " " & xItem.Name
        If Len(xItem.Name2) > 0 And xItem.Name2 <> xItem.Name Then
            sName = sName & " (" & xItem.Name2 & ")"
        End If
        If IsNothing(xParentNode) Then
            tvn = tv.Nodes.Add(xItem.ONSCode, sName)
        Else
            tvn = xParentNode.Nodes.Add(xItem.ONSCode, sName)
        End If
        If xItem.OSMRelation > 0 Then
            tvn.NodeFont = fntBold
        Else
            tvn.NodeFont = fntNormal
        End If
        tvn.Tag = xItem
        tvn.ContextMenuStrip = cmsNode
        Dim iThisCount As Integer = 0, iThisTotal As Integer = 0
        For Each xChild In xItem.Children
            iThisTotal += 1
            If xChild.OSMRelation > 0 Then iThisCount += 1
            LoadTreeView(tv, tvn, xDB, xChild, iThisTotal, iThisCount)
        Next
        '        If bChildren Then
        '        tvn.Text = tvn.Text & " [" & iThisCount.ToString & "/" & iThisTotal.ToString & "]"
        '        End If
        iTotal += iThisTotal
        iCount += iThisCount
    End Sub
    Private Sub SetTreeItems2(tvi As TreeNode, ByRef iTotal As Integer, ByRef iCount As Integer)
        Dim iThisTotal As Integer = 0, iThisCount As Integer = 0
        Dim sName As String
        Dim fntRequired As Font
        Dim colRequired As Color

        Dim xItem As BoundaryDB.BoundaryItem = tvi.Tag

        If xItem Is Nothing Then
            sName = tvi.Text
            fntRequired = fntNormal
        Else
            sName = xItem.TypeCode & " " & xItem.Name
            If Len(xItem.Name2) > 0 And xItem.Name2 <> xItem.Name Then
                sName = $"{sName} ({xItem.Name2})"
                ' sName = sName & " (" & xItem.Name2 & ")"
            End If
            If xItem.OSMRelation = 0 Then
                fntRequired = fntNormal
            Else
                fntRequired = fntBold
            End If
        End If

        If Not (tvi.NodeFont Is fntRequired) Then
            tvi.NodeFont = fntRequired
        End If


        For Each tvi2 As TreeNode In tvi.Nodes
            SetTreeItems2(tvi2, iThisTotal, iThisCount)
        Next

        If tvi.Nodes.Count > 0 Then
            sName = $"{sName} [{iThisCount}/{iThisTotal}]"
            If iThisCount = iThisTotal Then
                colRequired = Color.Green
            Else
                colRequired = Color.Black
            End If
            If tvi.ForeColor <> colRequired Then
                tvi.ForeColor = colRequired
            End If
        End If
        ' setting .text is extremely slow so only do it if it is essential
        If tvi.Text <> sName Then tvi.Text = sName

        Application.DoEvents()

        If (Not xItem Is Nothing) AndAlso xItem.BoundaryType <> BoundaryDB.BoundaryItem.BoundaryTypes.BT_ParishGroup Then
            iThisTotal += 1
            If (Not xItem Is Nothing) AndAlso xItem.OSMRelation > 0 Then iThisCount += 1
        End If
        iTotal += iThisTotal
        iCount += iThisCount
    End Sub
    Private Sub SetTreeItems(tv As TreeView)
        Dim iTotal As Integer, iCount As Integer
        For Each tvi As TreeNode In tv.Nodes
            iTotal = 0
            iCount = 0
            SetTreeItems2(tvi, iTotal, iCount)
        Next
    End Sub
    Private Function FindBoundaryDB() As String
        Dim sDB As String = ""

        sDB = System.Environment.ExpandEnvironmentVariables(My.Settings.BoundaryXML)
        If System.IO.File.Exists(sDB) Then Return sDB

        sDB = GetFolderPath(SpecialFolder.ApplicationData) & "\BoundsBeater\UKBoundaries.xml"
        If System.IO.File.Exists(sDB) Then Return sDB

        sDB = System.IO.Path.GetDirectoryName(Application.ExecutablePath) & "\UKBoundaries.xml"
        If System.IO.File.Exists(sDB) Then Return sDB

        Return ""
    End Function

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Dim sDB As String

        sDB = FindBoundaryDB()

        If Len(sDB) = 0 Then
            MsgBox("No Boundary XML file defined")
            Exit Sub
        End If

        sCache = System.Environment.ExpandEnvironmentVariables(My.Settings.OSMCache)
        If Len(sCache) > 0 Then
            If System.IO.File.Exists(sCache) Then
                tmpDoc = New OSMDoc(sCache)
            End If
        End If

        tsStatus.Text = "Loading " & sDB & "..."
        Application.DoEvents()
        Dim x As New BoundaryDB(sDB)
        tsStatus.Text = "Loaded " & sDB

        ReloadTV(x)

        xDB = x
        sDBPath = sDB
    End Sub
    Public Sub ReloadTV(x As BoundaryDB)
        Dim xItem As BoundaryDB.BoundaryItem

        tvList.Nodes.Clear()

        ' load the root nodes - lower nodes get done recursively
        tvList.BeginUpdate()
        tsProgress.Maximum = x.Items.Count
        tsProgress.Minimum = 0
        tsProgress.Value = 0
        Application.DoEvents()
        Dim i As Integer = 0
        Dim iCount As Integer = 0, iTotal As Integer = 0
        For Each xItem In x.Root.Children
            i = i + 1
            tsProgress.Value = i
            Application.DoEvents()
            LoadTreeView(tvList, Nothing, x, xItem, iTotal, iCount)
        Next

        SetTreeItems(tvList)
        Application.DoEvents()
        tvList.Sort()
        tvList.EndUpdate()

    End Sub
    Private Sub tvList_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tvList.AfterSelect
        Dim iRel As ULong
        Dim x As BoundaryDB.BoundaryItem

        If bInRightClick Then Exit Sub
        x = e.Node.Tag
        ShowChildren(e.Node)
        If Not IsNothing(x) Then
            iRel = x.OSMRelation
            If iRel > 0 Then
                ShowRelation(iRel)
            End If
        End If
    End Sub

    Private Sub ShowChildren(n As TreeNode)
        Dim p As BoundaryDB.BoundaryItem
        lvChildren.Items.Clear()
        For Each x As TreeNode In n.Nodes
            p = DirectCast(x.Tag, BoundaryDB.BoundaryItem)
            Dim lvi As ListViewItem = lvChildren.Items.Add("")
            lvChildren.ContextMenuStrip = cmsChild
            LoadChildListItem(lvi, p)
            lvi.Tag = x
        Next
    End Sub
    Private Sub LoadChildListItem(lvi As ListViewItem, p As BoundaryDB.BoundaryItem)
        With lvi
            .Text = p.Name
            If .SubItems.Count = 1 Then
                .SubItems.Add(p.OSMRelation.ToString)
                .SubItems.Add(p.TypeCode)
                .SubItems.Add(p.ONSCode)
                .SubItems.Add(BoundaryDB.BoundaryItem.CouncilStyle_ToString(p.CouncilStyle))
                .SubItems.Add(BoundaryDB.BoundaryItem.ParishType_ToString(p.ParishType))
                .SubItems.Add(p.CouncilName.ToString)
            Else
                .SubItems(1).Text = p.OSMRelation.ToString
                .SubItems(2).Text = p.TypeCode
                .SubItems(3).Text = p.ONSCode
                .SubItems(4).Text = BoundaryDB.BoundaryItem.CouncilStyle_ToString(p.CouncilStyle)
                .SubItems(5).Text = BoundaryDB.BoundaryItem.ParishType_ToString(p.ParishType)
                .SubItems(6).Text = p.CouncilName.ToString
            End If
        End With
    End Sub
    Private Sub ShowOnMap(xRel As OSMRelation)
        Dim sJSON As String, sID As String
        If Not bMapInitDone Then Exit Sub
        Dim res As New OSMResolver(xRel)
        'sJSON = xRel.GeoJSON
        sJSON = res.GeoJSON()
        Dim xArgs(2) As Object
        If Len(sJSON) > 0 Then
            xArgs(0) = sJSON
            xArgs(1) = "r" & xRel.ID.ToString
            wbMap.Document.InvokeScript("drawJSON", xArgs)
        End If
    End Sub
    Private Sub tbpMap_Enter(sender As Object, e As EventArgs) Handles tbpMap.Enter
        If Not bMapInitDone Then
            wbMap.Navigate(System.IO.Path.GetDirectoryName(Application.ExecutablePath) & "\HTML\index.htm")
            bMapInitDone = True
        End If
    End Sub

    Private Sub frmAnalyze_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Me.tabDetail.Width = Me.Width - Me.tabDetail.Left - 32
        Me.tabDetail.Height = Me.Height - Me.tabDetail.Top - 64
    End Sub
    Private Sub CollectExpanded(tvn As TreeNode, l As List(Of BoundaryDB.BoundaryItem))
        If tvn.IsExpanded Then l.Add(tvn.Tag)
        For Each xNode In tvn.Nodes
            CollectExpanded(xNode, l)
        Next
    End Sub
    Private Sub RestoreExpanded(tvn As TreeNode, l As List(Of BoundaryDB.BoundaryItem))
        If l.Contains(tvn.Tag) Then tvn.Expand()
        For Each xnode In tvn.Nodes
            RestoreExpanded(xnode, l)
        Next
    End Sub
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim oDoc As OSMDoc
        Dim bBox As New BBox
        Dim iRel As ULong
        Dim x As BoundaryDB.BoundaryItem
        Dim xNode As TreeNode
        Dim sURL As String
        Dim bUpdateAll As Boolean = chkUpdateAll.Checked
        Dim sAdminLevel As String = txtAdminLevel.Text
        Dim fntRequired As Font

        If Not GetMapBBox(bBox) Then
            xNode = tvList.SelectedNode
            If IsNothing(xNode) Then
                Exit Sub
            End If
            x = xNode.Tag
            If IsNothing(x) Then
                Exit Sub
            End If
            iRel = x.OSMRelation
            If iRel <= 0 Then
                Exit Sub
            End If
            bBox = tmpDoc.Relations(iRel).Bbox
        End If

        If Len(sAdminLevel) = 0 Then
            sURL = "http://overpass-api.de/api/xapi?relation[type=boundary][bbox=" _
                & bBox.MinLon & "," & bBox.MinLat & "," & bBox.MaxLon & "," & bBox.MaxLat _
                & "][@meta]"
            'sURL = "http://overpass-api.de/api/interpreter?data=[bbox];rel[type=boundary];[bbox];out+meta;&bbox=" _
            '         & bBox.MinLon & "," & bBox.MinLat & "," & bBox.MaxLon & "," & bBox.MaxLat _
            '    & ""
        Else
            sURL = "http://overpass-api.de/api/xapi?relation[type=boundary][admin_level=" & sAdminLevel & "][bbox=" _
                & bBox.MinLon & "," & bBox.MinLat & "," & bBox.MaxLon & "," & bBox.MaxLat _
                & "][@meta]"
        End If

        Try
            tsStatus.Tag = "Retrieving " & sURL
            Application.DoEvents()
            oDoc = xRetriever.API.GetOSM(sURL)
            If IsNothing(oDoc) Then
                tsStatus.Tag = "Query failed or returned no data"
                Exit Sub
            End If
        Catch
            MsgBox($"Unable to retrieve {sURL}")
            Exit Sub
        End Try

        tsStatus.Text = "Merging new data with library..."
        Application.DoEvents()
        xDB.MergeOSM(oDoc, bUpdateAll)
        If xDB.ChangedItems.Count > 0 Then

            Dim xExpanded As New List(Of BoundaryDB.BoundaryItem)
            Dim xSelected As BoundaryDB.BoundaryItem = tvList.SelectedNode?.Tag
            For Each xNode In tvList.Nodes
                CollectExpanded(xNode, xExpanded)
            Next
            tsStatus.Text = "Updating tree..."
            Application.DoEvents()
            ReloadTV(xDB)
            For Each xNode In tvList.Nodes
                RestoreExpanded(xNode, xExpanded)
            Next

#If False Then
            ' tvList.BeginUpdate()
            For Each xDBitem In xDB.ChangedItems
                xNodes = tvList.Nodes.Find(xDBitem.ONSCode, True)
                For Each xNode In xNodes
                    If xDBitem.OSMRelation = 0 Then
                        fntRequired = fntNormal
                    Else
                        fntRequired = fntBold
                    End If
                    If Not (xNode.NodeFont Is fntRequired) Then
                        xNode.NodeFont = fntRequired
                    End If
                    ' force display bbox recalculation following font change
                    ' xNode.Text = xNode.Text
                Next
            Next
            ' tvList.EndUpdate()
            Application.DoEvents()
            ' tvList.BeginUpdate()
            SetTreeItems(tvList)
            ' tvList.EndUpdate()
#End If
        End If
        tsStatus.Text = "Update complete."
        MsgBox("Update complete.", MsgBoxStyle.OkOnly + MsgBoxStyle.Information)
    End Sub

    Private Sub lvChildren_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lvChildren.ColumnClick
        ' Get the new sorting column.
        Dim lv As ListView = sender
        Dim new_sorting_column As ColumnHeader =
            lv.Columns(e.Column)

        ' Figure out the new sorting order.
        Dim sort_order As System.Windows.Forms.SortOrder
        If m_SortingColumn Is Nothing Then
            ' New column. Sort ascending.
            sort_order = SortOrder.Ascending
        Else
            ' See if this is the same column.
            If new_sorting_column.Equals(m_SortingColumn) Then
                ' Same column. Switch the sort order.
                If m_SortingColumn.Text.StartsWith("> ") Then
                    sort_order = SortOrder.Descending
                Else
                    sort_order = SortOrder.Ascending
                End If
            Else
                ' New column. Sort ascending.
                sort_order = SortOrder.Ascending
            End If

            ' Remove the old sort indicator.
            m_SortingColumn.Text =
                m_SortingColumn.Text.Substring(2)
        End If

        ' Display the new sort order.
        m_SortingColumn = new_sorting_column
        If sort_order = SortOrder.Ascending Then
            m_SortingColumn.Text = "> " & m_SortingColumn.Text
        Else
            m_SortingColumn.Text = "< " & m_SortingColumn.Text
        End If

        ' Create a comparer.
        lv.ListViewItemSorter = New ListViewComparer(e.Column, sort_order)

        ' Sort.
        lv.Sort()
    End Sub

    Private Sub lvChildren_DoubleClick(sender As Object, e As EventArgs) Handles lvChildren.DoubleClick
        If lvChildren.SelectedItems.Count <> 1 Then Exit Sub
        Dim x As TreeNode = lvChildren.SelectedItems(0).Tag
        x.EnsureVisible()
        tvList.SelectedNode = x
    End Sub

    Private Sub tsmiChildEdit_Click(sender As Object, e As EventArgs) Handles tsmiChildEdit.Click
        If lvChildren.SelectedItems.Count = 0 Then Return
        Dim lvi As ListViewItem = lvChildren.SelectedItems(0)
        If IsNothing(lvi) Then Return
        Dim x As TreeNode = DirectCast(lvi.Tag, TreeNode)
        If IsNothing(x) Then Return
        Dim bi As BoundaryDB.BoundaryItem
        bi = DirectCast(x.Tag, BoundaryDB.BoundaryItem)
        If bi.Edit() Then
            LoadChildListItem(lvi, bi)
            ' reload current lv item
        End If
    End Sub

    Private Sub tsmiEdit_Click(sender As Object, e As EventArgs) Handles tsmiEdit.Click
        Dim x As TreeNode = tvList.SelectedNode
        If IsNothing(x) Then Return
        Dim bi As BoundaryDB.BoundaryItem
        bi = DirectCast(x.Tag, BoundaryDB.BoundaryItem)
        If bi.Edit() Then
            ' reload current tv item name possibly
        End If
    End Sub

    Private Sub tsmiFlush_Click(sender As Object, e As EventArgs) Handles tsmiFlush.Click
        Dim x As TreeNode = tvList.SelectedNode
        MsgBox("Flush : " & x.Text)
    End Sub

    Private Sub tsmiShowAll_Click(sender As Object, e As EventArgs) Handles tsmiShowAll.Click
        Dim x As TreeNode = tvList.SelectedNode
        Dim p As BoundaryDB.BoundaryItem
        p = DirectCast(x.Tag, BoundaryDB.BoundaryItem)
        Dim iPar As Long = p.OSMRelation
        If iPar > 0 Then
            ShowRelation(iPar)
        End If
        Dim iChild As Long
        For Each xChild As TreeNode In x.Nodes
            p = DirectCast(xChild.Tag, BoundaryDB.BoundaryItem)
            iChild = p.OSMRelation
            If iChild > 0 Then
                ShowRelation(iChild)
                Application.DoEvents()
            End If
        Next
    End Sub

    Private Sub tsmiJSON_Click(sender As Object, e As EventArgs) Handles tsmiJSON.Click
        Dim x As TreeNode = tvList.SelectedNode
        Dim p As BoundaryDB.BoundaryItem
        Dim xRel As OSMRelation
        Dim sJSON As String
        p = DirectCast(x.Tag, BoundaryDB.BoundaryItem)
        Dim iPar As Long = p.OSMRelation
        If iPar > 0 Then
            xRel = xRetriever.GetOSMObject(tmpDoc, OSMObject.ObjectType.Relation, iPar)
            sJSON = xRel.GeoJSON()
            If MsgBox(sJSON, MsgBoxStyle.Question + MsgBoxStyle.OkCancel, "Save JSON to Clipboard?") = MsgBoxResult.Ok Then
                Clipboard.SetText(sJSON)
            End If
        End If
    End Sub

    Function GetMapBBox(ByRef b As BBox) As Boolean
        If Not bMapInitDone Then Return False
        Dim xbb As Object
        Static bFlags As System.Reflection.BindingFlags = Reflection.BindingFlags.DeclaredOnly Or
            System.Reflection.BindingFlags.Public Or
            System.Reflection.BindingFlags.NonPublic Or
            System.Reflection.BindingFlags.Instance Or
            System.Reflection.BindingFlags.GetProperty

        xbb = wbMap.Document.InvokeScript("getMapBounds")

        Dim type As System.Type = xbb.GetType()
        b.MinLat = DirectCast(type.InvokeMember("minLat",
            bFlags, Nothing, xbb, Nothing), Double)
        b.MaxLat = DirectCast(type.InvokeMember("maxLat",
            bFlags, Nothing, xbb, Nothing), Double)
        b.MinLon = DirectCast(type.InvokeMember("minLon",
            bFlags, Nothing, xbb, Nothing), Double)
        b.MaxLon = DirectCast(type.InvokeMember("maxLon",
            bFlags, Nothing, xbb, Nothing), Double)
        Return True
    End Function

    Private Sub btnHist_Click(sender As Object, e As EventArgs) Handles btnHist.Click
        Dim iRel As ULong
        Dim sTmp As String = Me.txtSingle.Text
        If Len(sTmp) < 1 Or Len(sTmp) > 10 Then Exit Sub
        If Not ULong.TryParse(sTmp, iRel) Then Exit Sub
        If iRel < 1 Then Exit Sub
        Dim xRet As New OSMRetriever
        Dim xDoc As New OSMDoc
        Dim xRel As OSMObject

        xDoc = xRet.GetOSMObjectHistoryFull(OSMObject.ObjectType.Relation, iRel)
        If xDoc Is Nothing Then
            MsgBox("relation " & sTmp & "not found")
        Else
            MsgBox("Found version " & xDoc.Relations(iRel).Version)
        End If
    End Sub

    Private Sub btnOSM_Click(sender As Object, e As EventArgs) Handles btnOSM.Click
        If IsNothing(xDB) Then Exit Sub
        If System.IO.File.Exists("C:\VMShare\mkgmap\work\uk\gbb.osm") Then
            xDB.UpdateFromOSMFile("C:\VMShare\mkgmap\work\uk\gbb.osm")
        ElseIf System.IO.File.Exists("L:\VMShare\mkgmap\work\uk\gbb.osm") Then
            xDB.UpdateFromOSMFile("L:\VMShare\mkgmap\work\uk\gbb.osm")
        Else
            MsgBox("gbb.osm not found")
        End If
    End Sub

    Private Sub tvList_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles tvList.NodeMouseClick
        If e.Button = MouseButtons.Right Then
            bInRightClick = True
            tvList.SelectedNode = e.Node
            bInRightClick = False
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        If Not bMapInitDone Then Return
        wbMap.Document.InvokeScript("clearLayers")
    End Sub

    Private Sub tsmiChildReport_Click(sender As Object, e As EventArgs) Handles tsmiChildReport.Click
        If lvChildren.SelectedItems.Count = 0 Then Return
        Dim lvi As ListViewItem = lvChildren.SelectedItems(0)
        If IsNothing(lvi) Then Return
        Dim x As TreeNode = DirectCast(lvi.Tag, TreeNode)
        If IsNothing(x) Then Return
        Dim p As BoundaryDB.BoundaryItem
        Dim xRel As OSMRelation
        Dim sFile As String
        p = DirectCast(x.Tag, BoundaryDB.BoundaryItem)
        Dim iRel As Long = p.OSMRelation
        If iRel > 0 Then
            sFile = SpecialDirectories.Temp & "\r" & iRel & ".htm"
            xRel = xRetriever.GetOSMObject(tmpDoc, OSMObject.ObjectType.Relation, iRel)
            Dim rep As New RelationReport(tmpDoc)
            rep.RelationReport(sFile, xRel)
            OpenBrowserAt(sFile)
        End If
    End Sub

    Private Sub tsmiReport_Click(sender As Object, e As EventArgs) Handles tsmiReport.Click
        Dim x As TreeNode = tvList.SelectedNode
        Dim p As BoundaryDB.BoundaryItem
        Dim xRel As OSMRelation
        Dim sFile As String
        p = DirectCast(x.Tag, BoundaryDB.BoundaryItem)
        Dim iRel As Long = p.OSMRelation

        If iRel > 0 Then
            sFile = SpecialDirectories.Temp & "\r" & iRel & ".htm"
            xRel = xRetriever.GetOSMObject(tmpDoc, OSMObject.ObjectType.Relation, iRel)
            Dim rep As New RelationReport(tmpDoc)
            rep.RelationReport(sFile, xRel)
            OpenBrowserAt(sFile)
        End If
    End Sub

    Private Sub tsmiAddChild_Click(sender As Object, e As EventArgs) Handles tsmiAddChild.Click
        Dim x As TreeNode = tvList.SelectedNode
        Dim p As BoundaryDB.BoundaryItem
        p = DirectCast(x.Tag, BoundaryDB.BoundaryItem)
        Dim iRel As Long = p.OSMRelation

        Dim xItem As New BoundaryDB.BoundaryItem(xDB)
        xItem.Parent = p
        xItem.ParentCode = p.ONSCode
        If xItem.Edit() Then
            xDB.Items.Add(xItem.ONSCode, xItem)
            MsgBox("edit successful")
        End If
    End Sub

    Private Sub SetParishGroup(ByVal sender As Object, ByVal e As EventArgs)
        Dim myItem As ToolStripMenuItem
        Dim xGroupItem As BoundaryDB.BoundaryItem
        Dim xParishItem As BoundaryDB.BoundaryItem

        ' Extract the tag value from the item received.
        myItem = CType(sender, ToolStripMenuItem)
        xGroupItem = CType(myItem.Tag, BoundaryDB.BoundaryItem)

        ' get the node to operate on
        Dim x As TreeNode = tvList.SelectedNode
        xParishItem = DirectCast(x.Tag, BoundaryDB.BoundaryItem)

        ' set its parish group name into CouncilName
        If xParishItem.BoundaryType <> BoundaryDB.BoundaryItem.BoundaryTypes.BT_CivilParish Then
            Return
        End If
        If xParishItem.ParishType <> BoundaryDB.BoundaryItem.ParishTypes.PT_JointParishCouncil Then
            Return
        End If
        xParishItem.CouncilName = xGroupItem.Name
        xParishItem.CouncilName2 = xGroupItem.Name2
        xDB.ChangedItems.Add(xParishItem)

        ' move the check mark
        myItem.Checked = True
    End Sub

    Private Sub tsmiReview_Click(sender As Object, e As EventArgs) Handles tsmiReview.Click
        Dim x As TreeNode = tvList.SelectedNode
        If x Is Nothing Then Return
        If x.Tag Is Nothing Then Return
        Dim p As BoundaryDB.BoundaryItem
        p = DirectCast(x.Tag, BoundaryDB.BoundaryItem)
        Dim f As New frmReview(p)
        f.ShowDialog(Me)
    End Sub
End Class
Public Class ListViewComparer
    Implements IComparer

    Private m_ColumnNumber As Integer
    Private m_SortOrder As SortOrder

    Public Sub New(ByVal column_number As Integer, ByVal _
        sort_order As SortOrder)
        m_ColumnNumber = column_number
        m_SortOrder = sort_order
    End Sub

    ' Compare the items in the appropriate column
    ' for objects x and y.
    Public Function Compare(ByVal x As Object, ByVal y As _
        Object) As Integer Implements _
        System.Collections.IComparer.Compare
        Dim item_x As ListViewItem = DirectCast(x,
            ListViewItem)
        Dim item_y As ListViewItem = DirectCast(y,
            ListViewItem)

        ' Get the sub-item values.
        Dim string_x As String
        If item_x.SubItems.Count <= m_ColumnNumber Then
            string_x = ""
        Else
            string_x = item_x.SubItems(m_ColumnNumber).Text
        End If

        Dim string_y As String
        If item_y.SubItems.Count <= m_ColumnNumber Then
            string_y = ""
        Else
            string_y = item_y.SubItems(m_ColumnNumber).Text
        End If

        ' Compare them.
        If m_SortOrder = SortOrder.Ascending Then
            If IsNumeric(string_x) And IsNumeric(string_y) _
                Then
                Return Val(string_x).CompareTo(Val(string_y))
            ElseIf IsDate(string_x) And IsDate(string_y) _
                Then
                Return DateTime.Parse(string_x).CompareTo(DateTime.Parse(string_y))
            Else
                Return String.Compare(string_x, string_y)
            End If
        Else
            If IsNumeric(string_x) And IsNumeric(string_y) _
                Then
                Return Val(string_y).CompareTo(Val(string_x))
            ElseIf IsDate(string_x) And IsDate(string_y) _
                Then
                Return DateTime.Parse(string_y).CompareTo(DateTime.Parse(string_x))
            Else
                Return String.Compare(string_y, string_x)
            End If
        End If
    End Function
End Class
