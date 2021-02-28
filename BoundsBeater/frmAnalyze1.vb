Imports OSMLibrary
Imports System.Environment
Imports System.Runtime.InteropServices.ComTypes
Imports Microsoft.VisualBasic.FileIO
' Imports ProtoBuf.Meta
Imports System.ComponentModel
Imports System.Text
Imports System.Threading.Tasks
Imports BoundsBeater.BoundaryDB

Public Class frmAnalyze
    Private m_SortingColumn As ColumnHeader

    Dim loading As Boolean = True
    Dim WithEvents tmpDoc As New OSMDoc
    Dim bLargeCache As Boolean = True
    Dim xRetriever As New OSMRetriever
    Dim bMapInitDone As Boolean = False
    Dim sDBPath As String
    Dim sCache As String = ""
    Dim xDB As BoundaryDB
    Dim fntNormal As Font
    Dim fntBold As Font
    Dim bInRightClick As Boolean = False
    Const DUMMY_MARKER As String = "dummy"
    Dim bAllowExpandCollapse As Boolean = False
    Dim bExpanding As Boolean = False
    Dim biListViewParent As BoundaryDB.BoundaryItem
    ' Dim x As New System.Data.SQLite.SQLiteConnection()

    Private Sub testpbf()
        Dim x As New OSMPBF.Blob
        ' Dim sc As New ProtoBuf.SerializationContext
        'Dim tm As New TypeModel()
        'Dim z As New ProtoBuf.ProtoReader(IStream, t, sc)
    End Sub

    ''' <summary>
    ''' Handles Click event on Single Report button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnSingle_Click(sender As Object, e As EventArgs) Handles btnSingle.Click
        Dim iRel As Long
        Dim sTmp As String = Me.txtSingle.Text
        If Len(sTmp) < 1 Or Len(sTmp) > 10 Then Exit Sub
        If Not Long.TryParse(sTmp, iRel) Then Exit Sub
        If iRel < 1 Then Exit Sub
        Dim xAPI As New OSMApi
        Dim xDoc As New OSMDoc
        Dim xRel As OSMObject
        Dim xCache As New OSMHistoryCache

        xRel = xCache.OSMNodeHistory(iRel)

        xRel = xRetriever.GetOSMObjectHistory(OSMObject.ObjectType.Node, iRel, True)

        ' Dim xWay As OSMWay
        'xWay = xAPI.GetOSMObjectHistory(OSMObject.ObjectType.Way, iRel)

        '        ShowRelation(iRel)
    End Sub

    ''' <summary>
    ''' Shows a boundary on the map, with some information in a text box
    ''' </summary>
    ''' <param name="iRel">The OSM ID of the selecte relation</param>
    Private Function ShowRelation(iRel As Long) As OSMRelation
        Dim xRel As OSMRelation
        Dim xRes As OSMResolver
        Dim sLine As String
        Dim sTmp As String = ""

        '        txtReport.Clear()
        xRetriever.MaxAge = My.Settings.MaxCacheAge
        xRel = DirectCast(xRetriever.GetOSMObject(tmpDoc, OSMObject.ObjectType.Relation, iRel), OSMRelation)
        If IsNothing(xRel) Then
            xRel = Nothing
            txtReport.Text = "Unable to retrieve relation #" & iRel
            Return Nothing
        End If
        sTmp = "Loaded " & xRel.Name() & ", OSM Relation " & iRel.ToString & ", version " & xRel.Version.ToString() & " of " & xRel.Timestamp.ToString() & " by " & xRel.User
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
            Return xRel
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
                If xWays.Contains(DirectCast(xMbr.Member, OSMWay)) Then
                    sLine = "Relation contains duplicate Way #" & xMbr.Member.ID
                    sTmp = sTmp & vbCrLf & sLine
                Else
                    xWays.Add(DirectCast(xMbr.Member, OSMWay))
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
            sLine &= xRing.Role
            If xRing.isClosed Then
                sLine &= " (closed)"
            Else
                If xRing.Head.ExtremelyCloseTo(xRing.Tail) Then
                    sLine &= " (NOT closed - head/tail node not shared)"
                Else
                    sLine &= " (NOT closed - missing segment)"
                End If
            End If
            sTmp &= vbCrLf & sLine
        Next
        txtReport.Text = txtReport.Text & sTmp
        Dim b As BBox = xRel.BBox
        sTmp = "BBox [" & b.MinLon & "," & b.MinLat & "," & b.MaxLon & "," & b.MaxLat & "]"
        txtReport.Text = txtReport.Text & vbCrLf & sTmp
        sTmp = My.Settings.xapiAPI & $"?relation[type=boundary][bbox={b.MinLon},{b.MinLat},{b.MaxLon},{b.MaxLat}][@meta]"
        txtReport.Text = txtReport.Text & vbCrLf & sTmp
        Return xRel
    End Function

#If False Then
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
    Private Sub FormatTreeItem(tv As TreeView, xParentNode As TreeNode, xItem As BoundaryDB.BoundaryItem)
        Dim sName As String
        Dim tvn As TreeNode

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

    End Sub
    Private Sub SetTreeItems2(tvi As TreeNode, ByRef iTotal As Integer, ByRef iCount As Integer)
        Dim iThisTotal As Integer = 0, iThisCount As Integer = 0
        Dim sName As String
        Dim fntRequired As Font
        Dim colRequired As Color

        Dim xItem As BoundaryDB.BoundaryItem = DirectCast(tvi.Tag, BoundaryDB.BoundaryItem)

        If xItem Is Nothing Then
            sName = tvi.Text
            fntRequired = fntNormal
        Else
            sName = DisplayString(xItem)
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
#End If

    Sub ShowLoadProgress(r As Long, w As Long, n As Long) Handles tmpDoc.LoadProgress
        Static lastupdate As Date
        Dim rightnow As Date = Now()
        If Math.Abs(DateDiff(DateInterval.Second, rightnow, lastupdate)) < 1 Then Return
        tsStatus.Text = $"Loaded {r} relations, {w} ways, {n} nodes"
        Application.DoEvents()
        lastupdate = rightnow
    End Sub

    ''' <summary>
    ''' Creates a string for display of a BoundaryItem
    ''' </summary>
    ''' <param name="xItem">The BoundaryItem</param>
    ''' <returns>The generated display string</returns>
    Private Function DisplayString(xItem As BoundaryItem) As String
        Dim sName As String
        sName = xItem.TypeCode & " " & xItem.Name
        If Len(xItem.Name2) > 0 And xItem.Name2 <> xItem.Name Then
            sName = $"{sName} ({xItem.Name2})"
            ' sName = sName & " (" & xItem.Name2 & ")"
        End If
        If xItem.BoundaryType = BoundaryItem.BoundaryTypes.BT_CivilParish Then
            If xItem.LandsCommon.Count > 0 Then sName &= "*"
            If xItem.DetachedAreas.Count > 0 Then sName &= "+"
        End If
        Return sName
    End Function
#If False Then
    Private Sub SetTreeItems(tv As TreeView)
        Dim iTotal As Integer, iCount As Integer
        For Each tvi As TreeNode In tv.Nodes
            iTotal = 0
            iCount = 0
            SetTreeItems2(tvi, iTotal, iCount)
        Next
    End Sub
#End If
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
    ''' <summary>
    ''' Handle click on Go button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub btnGo_Click(sender As Object, e As EventArgs)
        Dim sDB As String

        sDB = FindBoundaryDB()

        If Len(sDB) = 0 Then
            MsgBox("No Boundary XML file defined")
            Exit Sub
        End If

        Dim x As New BoundaryDB
        Dim s As String = Await loadxml(sDB, x)

        If IsNothing(fntBold) Then
            fntNormal = tvList.Font
            fntBold = New Font(tvList.Font, FontStyle.Bold)
        End If

        ReloadTV(x)

        xDB = x
        sDBPath = sDB
        Application.DoEvents()

        sCache = System.Environment.ExpandEnvironmentVariables(My.Settings.OSMCache)
        If Len(sCache) > 0 Then
            If System.IO.File.Exists(sCache) Then
                tmpDoc = New OSMDoc()
                If bLargeCache Then
                    tmpDoc.LoadBigXML(sCache)
                Else
                    tmpDoc.Load(sCache)
                End If
            End If
        End If

    End Sub

    Delegate Sub doit(s As String)

    Public Async Function loadxml(sdb As String, x As BoundaryDB) As Task(Of String)
        tsStatus.Text = "Loading " & sdb & "..."
        Application.DoEvents()
        Await LoadXDocumentAsync(x, sdb)
        tsStatus.Text = "Loaded " & sdb
        Return "hhh"
    End Function
    Public Async Function LoadXDocumentAsync(x As BoundaryDB, uri As String) As Task(Of Boolean)
        Dim t As New Task(Of Boolean)(Function() x.LoadFromFile(uri))
        t.Start()
        Return Await t
    End Function
    ''' <summary>
    ''' Clears and reloads the main TreeView
    ''' </summary>
    ''' <param name="x">The BoundaryDB to be loaded</param>
    Public Sub ReloadTV(x As BoundaryDB)
        Dim xItem As BoundaryItem

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
            i += 1
            tsProgress.Value = i
            Application.DoEvents()
            LoadTreeNode(tvList, Nothing, xItem)
        Next

        Application.DoEvents()
        tvList.Sort()
        tvList.EndUpdate()

    End Sub

    ''' <summary>
    ''' After the treeview selection changes, show the selected relation on the map and/or show its children in the listview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tvList_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tvList.AfterSelect
        If bInRightClick Then Exit Sub
        ShowChildren(e.Node)
    End Sub

    ''' <summary>
    ''' Show the children of the given treenode into the listview
    ''' </summary>
    ''' <param name="n"></param>
    Private Sub ShowChildren(n As TreeNode)
        Dim p As BoundaryItem
        biListViewParent = DirectCast(n.Tag, BoundaryItem)
        lvChildren.Items.Clear()
        For Each x As TreeNode In n.Nodes
            p = DirectCast(x.Tag, BoundaryItem)
            If Not p Is Nothing Then
                If Not chkShowDeleted.Checked AndAlso p.IsDeleted Then Continue For
                Dim lvi As ListViewItem = lvChildren.Items.Add("")
                lvChildren.ContextMenuStrip = cmsChild
                LoadChildListItem(lvChildren, lvi, p)
                lvi.Tag = x
            End If
        Next
        lvChildren.Sort()
    End Sub

    ''' <summary>
    ''' Fills a single row of the listview will all the details of a boundary.
    ''' </summary>
    ''' <param name="lvi">ListViewItem to be populated</param>
    ''' <param name="p">BoundaryItem to use</param>
    Private Sub LoadChildListItem(lv As ListView, lvi As ListViewItem, p As BoundaryItem)
        With lvi
            If p.IsDeleted Then .ForeColor = Color.Red Else .ForeColor = Color.Black
            .Text = p.Name
            If .SubItems.Count = 1 Then
                For i = 1 To lvi.ListView.Columns.Count - 1
                    .SubItems.Add("")
                Next
            End If
            .SubItems(colOSMID.Index).Text = p.OSMRelation.ToString
            .SubItems(colType.Index).Text = p.TypeCode
            .SubItems(colGSS.Index).Text = p.ONSCode
            .SubItems(colCouncilStyle.Index).Text = BoundaryItem.CouncilStyle_ToString(p.CouncilStyle)
            .SubItems(colParishType.Index).Text = BoundaryItem.ParishType_ToString(p.ParishType)
            .SubItems(colCouncilName.Index).Text = p.CouncilName.ToString
            .SubItems(colWebsite.Index).Text = p.Website
        End With
    End Sub
    Private Function RelationLabel(xRel As OSMRelation) As String
        Dim sLabel As String = xRel.Tag("ref:gss")
        If sLabel = "" Then sLabel = "r" & xRel.ID.ToString
        Return sLabel
    End Function
    Private Sub ShowOnMap(xRel As OSMRelation)
        Dim sJSON As String
        If Not bMapInitDone Then Exit Sub
        Dim res As New OSMResolver(xRel)
        'sJSON = xRel.GeoJSON
        sJSON = res.GeoJSON()
        ShowJSON(sJSON, RelationLabel(xRel))
    End Sub
    Private Sub ZoomToLayer(uid As String)
        If Not bMapInitDone Then Exit Sub
        Dim xArgs(1) As Object
        xArgs(0) = uid
        wbMap.Document.InvokeScript("zoomToLayer", xArgs)
    End Sub
    Private Sub ShowJSON(sJSON As String, uid As String)
        If Not bMapInitDone Then Exit Sub
        Dim xArgs(2) As Object
        If Len(sJSON) > 0 Then
            xArgs(0) = sJSON
            xArgs(1) = uid
            wbMap.Document.InvokeScript("drawJSON", xArgs)
        End If
    End Sub
    Private Sub GotoLatLong(Lat As Double, Lon As Double)
        If Not bMapInitDone Then Exit Sub
        Dim xArgs(2) As Object
        xArgs(0) = Lat
        xArgs(1) = Lon
        wbMap.Document.InvokeScript("gotoLatLon", xArgs)
    End Sub
    Private Sub GotoZoom(Zoom As Integer)
        If Not bMapInitDone Then Exit Sub
        Dim xArgs(1) As Object
        xArgs(0) = Zoom
        wbMap.Document.InvokeScript("gotoZoom", xArgs)
    End Sub
    Private Sub GotoPanZoom(Lat As Double, Lon As Double, Zoom As Integer)
        If Not bMapInitDone Then Exit Sub
        Dim xArgs(3) As Object
        xArgs(0) = Lat
        xArgs(1) = Lon
        xArgs(2) = Zoom
        wbMap.Document.InvokeScript("gotoPanZoom", xArgs)
    End Sub

    Private Sub tbpMap_Enter(sender As Object, e As EventArgs) Handles tbpMap.Enter
        If Not bMapInitDone Then
            wbMap.Navigate(System.IO.Path.GetDirectoryName(Application.ExecutablePath) & "\HTML\index.htm")
            bMapInitDone = True
        End If
    End Sub

    Private Sub frmAnalyze_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        'Me.tabDetail.Width = Me.Width - Me.tabDetail.Left - 32
        'Me.tabDetail.Height = Me.Height - Me.tabDetail.Top - 64
        Dim frameWidth As Integer = (Me.Width - Me.ClientSize.Width) \ 2
        Me.Panel1.Width = Me.ClientSize.Width - 2 * Me.Panel1.Left
        Me.Panel1.Height = Me.ClientSize.Height - Me.Panel1.Top - Me.ssStatus.Height - frameWidth
        My.Settings.frmAnalyze_MinMax = Me.WindowState
        My.Settings.Save()
    End Sub
    Private Sub CollectExpanded(tvn As TreeNode, l As List(Of BoundaryItem))
        If tvn.IsExpanded Then l.Add(DirectCast(tvn.Tag, BoundaryItem))
        For Each xNode As TreeNode In tvn.Nodes
            CollectExpanded(xNode, l)
        Next
    End Sub
    Private Sub RestoreExpanded(tvn As TreeNode, l As List(Of BoundaryItem))
        If l.Contains(DirectCast(tvn.Tag, BoundaryItem)) Then tvn.Expand()
        For Each xnode As TreeNode In tvn.Nodes
            RestoreExpanded(xnode, l)
        Next
    End Sub

    Private Sub lvChildren_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lvChildren.ColumnClick
        Const SortUpPrefix As String = "> "
        Const SortDownPrefix As String = "< "
        Const SortPrefixLen As Integer = 2

        ' Get the new sorting column.
        Dim lv As ListView = DirectCast(sender, ListView)
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
                If m_SortingColumn.Text.StartsWith(SortUpPrefix) Then
                    sort_order = SortOrder.Descending
                Else
                    sort_order = SortOrder.Ascending
                End If
            Else
                ' New column. Sort ascending.
                sort_order = SortOrder.Ascending
            End If

            ' Remove the old sort indicator.
            If Strings.Left(m_SortingColumn.Text, SortPrefixLen) = SortUpPrefix _
                OrElse Strings.Left(m_SortingColumn.Text, SortPrefixLen) = SortDownPrefix Then
                m_SortingColumn.Text = m_SortingColumn.Text.Substring(SortPrefixLen)
            End If
        End If

        ' Display the new sort order.
        m_SortingColumn = new_sorting_column
        If sort_order = SortOrder.Ascending Then
            m_SortingColumn.Text = SortUpPrefix & m_SortingColumn.Text
        Else
            m_SortingColumn.Text = SortDownPrefix & m_SortingColumn.Text
        End If

        ' Create a comparer.
        lv.ListViewItemSorter = New ListViewComparer(e.Column, sort_order)

        ' Sort.
        lv.Sort()
    End Sub

    Private Sub lvChildren_DoubleClick(sender As Object, e As EventArgs) Handles lvChildren.DoubleClick
        If lvChildren.SelectedItems.Count <> 1 Then Exit Sub
        Dim x As TreeNode = DirectCast(lvChildren.SelectedItems(0).Tag, TreeNode)
        x.EnsureVisible()
        tvList.SelectedNode = x
    End Sub

    Private Sub tsmiChildEdit_Click(sender As Object, e As EventArgs) Handles tsmiChildEdit.Click
        If lvChildren.SelectedItems.Count <> 1 Then Return
        Dim lvi As ListViewItem = lvChildren.SelectedItems(0)
        If IsNothing(lvi) Then Return
        Dim x As TreeNode = DirectCast(lvi.Tag, TreeNode)
        If IsNothing(x) Then Return
        Dim bi As BoundaryItem
        bi = DirectCast(x.Tag, BoundaryItem)
        If IsNothing(bi) Then Exit Sub
        Dim wasDeleted As Boolean = bi.IsDeleted
        If bi.Edit(xRetriever) Then
            If chkShowDeleted.Checked Or Not bi.IsDeleted Then
                LoadChildListItem(lvChildren, lvi, bi)
            ElseIf (Not wasDeleted) And bi.IsDeleted Then
                ' delete from listview
                lvi.Remove()
            End If
            ' reload current lv item
            If Not (x Is Nothing) Then
                Dim xNode As TreeNode = x
                Do Until xNode Is Nothing
                    LoadTreeNodeText(xNode)
                    xNode = xNode.Parent
                Loop
                '                tvList.Sort()
            End If
        End If
    End Sub

    Private Sub tsmiEdit_Click(sender As Object, e As EventArgs) Handles tsmiEdit.Click
        Dim xNode As TreeNode = tvList.SelectedNode
        If IsNothing(xNode) Then Return
        Dim bi As BoundaryItem
        bi = DirectCast(xNode.Tag, BoundaryItem)
        If bi.Edit(xRetriever) Then
            ' reload current tv item name possibly - no, definitely!
            Do Until xNode Is Nothing
                LoadTreeNodeText(xNode)
                xNode = xNode.Parent
            Loop
            '             tvList.Sort()
        End If
    End Sub

    Private Sub tsmiFlush_Click(sender As Object, e As EventArgs) Handles tsmiFlush.Click
        Dim x As TreeNode = tvList.SelectedNode
        MsgBox("Flush : " & x.Text)
    End Sub

    Private Sub tsmiShowAll_Click(sender As Object, e As EventArgs) Handles tsmiShowAll.Click
        Dim x As TreeNode = tvList.SelectedNode
        Dim p As BoundaryItem
        p = DirectCast(x.Tag, BoundaryItem)
        Dim iPar As Long = p.OSMRelation
        Dim iChild As Long
        For Each xChild As TreeNode In x.Nodes
            If xChild.Tag Is Nothing Then Continue For
            p = DirectCast(xChild.Tag, BoundaryItem)
            If p.IsDeleted Then Continue For
            If p.BoundaryType = BoundaryItem.BoundaryTypes.BT_CivilParish _
                AndAlso (p.ParishType = BoundaryItem.ParishTypes.PT_DetachedArea) Then Continue For
            iChild = p.OSMRelation
            If iChild > 0 Then
                ShowRelation(iChild)
                Application.DoEvents()
            ElseIf p.BoundaryType = BoundaryItem.BoundaryTypes.BT_ParishGroup Then
                ShowParishGroup(p)
            ElseIf p.ONSCode <> "" AndAlso p.Lat <> 0.0 AndAlso p.Lon <> 0.0 Then
                ShowPlaceHolder(p)
            End If
        Next
        If iPar > 0 Then
            ShowRelation(iPar)
        End If
    End Sub


    Private Sub ShowPlaceHolder(p As BoundaryItem)
        Dim sJSON As String
        Dim xNode As New OSMNode
        Dim xName As New OSMTag("name", p.Name)
        xNode.Tags.Add(xName.Key, xName)
        Dim xLevel As New OSMTag("admin_level", "12")
        xNode.Tags.Add(xLevel.Key, xLevel)
        Dim xLabel As New OSMTag("_bblabel", p.Name)
        xNode.Tags.Add(xLabel.Key, xLabel)
        xNode.Lat = p.Lat
        xNode.Lon = p.Lon
        xNode.IsPlaceholder = False
        sJSON = xNode.GeoJSON()
        ShowJSON(sJSON, p.ONSCode)
    End Sub
    Private Sub tsmiJSON_Click(sender As Object, e As EventArgs) Handles tsmiJSON.Click
        Dim x As TreeNode = tvList.SelectedNode
        Dim p As BoundaryItem
        Dim xRel As OSMRelation
        Dim sJSON As String
        p = DirectCast(x.Tag, BoundaryItem)
        Dim iPar As Long = p.OSMRelation
        If iPar > 0 Then
            xRel = TryCast(xRetriever.GetOSMObject(tmpDoc, OSMObject.ObjectType.Relation, iPar), OSMRelation)
            sJSON = xRel.GeoJSON()
            If MsgBox(sJSON, MsgBoxStyle.Question Or MsgBoxStyle.OkCancel, "Save JSON to Clipboard?") = MsgBoxResult.Ok Then
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

    Function GetMapLoc(ByRef Zoom As Integer, ByRef Centre As DPoint) As Boolean
        If Not bMapInitDone Then Return False
        Dim xbb As Object
        Static bFlags As System.Reflection.BindingFlags = Reflection.BindingFlags.DeclaredOnly Or
            System.Reflection.BindingFlags.Public Or
            System.Reflection.BindingFlags.NonPublic Or
            System.Reflection.BindingFlags.Instance Or
            System.Reflection.BindingFlags.GetProperty Or
            System.Reflection.BindingFlags.GetField

        xbb = wbMap.Document.InvokeScript("getMapLoc")
        Dim type As System.Type = xbb.GetType()
        Zoom = CInt(DirectCast(type.InvokeMember("Zoom", bFlags, Nothing, xbb, Nothing), Double))
        Centre.X = DirectCast(type.InvokeMember("Lon", bFlags, Nothing, xbb, Nothing), Double)
        Centre.Y = DirectCast(type.InvokeMember("Lat", bFlags, Nothing, xbb, Nothing), Double)
        Return True
    End Function

    Private Sub btnHist_Click(sender As Object, e As EventArgs) Handles btnHist.Click
        Dim iRel As Long
        Dim sTmp As String = Me.txtSingle.Text
        If Len(sTmp) < 1 Or Len(sTmp) > 10 Then Exit Sub
        If Not Long.TryParse(sTmp, iRel) Then Exit Sub
        If iRel < 1 Then Exit Sub
        Dim xRet As New OSMRetriever
        Dim xDoc As New OSMDoc

        xDoc = xRet.GetOSMObjectHistoryFull(OSMObject.ObjectType.Relation, iRel)
        If xDoc Is Nothing Then
            MsgBox("relation " & sTmp & "not found")
        Else
            MsgBox("Found version " & xDoc.Relations(iRel).Version)
        End If
    End Sub

    Private Sub tvList_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles tvList.NodeMouseClick
        If e.Button = MouseButtons.Right Then
            bInRightClick = True
            tvList.SelectedNode = e.Node
            bInRightClick = False
        End If
    End Sub

    Private Sub tsmiChildReport_Click(sender As Object, e As EventArgs) Handles tsmiChildReport.Click
        If lvChildren.SelectedItems.Count <> 1 Then Return
        Dim lvi As ListViewItem = lvChildren.SelectedItems(0)
        If IsNothing(lvi) Then Return
        Dim x As TreeNode = DirectCast(lvi.Tag, TreeNode)
        If IsNothing(x) Then Return
        Dim p As BoundaryItem
        Dim xRel As OSMRelation
        Dim sFile As String
        p = DirectCast(x.Tag, BoundaryItem)
        Dim iRel As Long = p.OSMRelation
        If iRel > 0 Then
            sFile = SpecialDirectories.Temp & "\r" & iRel & ".htm"
            xRel = TryCast(xRetriever.GetOSMObject(tmpDoc, OSMObject.ObjectType.Relation, iRel), OSMRelation)
            Dim rep As New RelationReport(tmpDoc, xRetriever)
            rep.RelationReport(sFile, xRel)
            OpenBrowserAt(sFile)
        End If
    End Sub

    Private Sub tsmiReport_Click(sender As Object, e As EventArgs) Handles tsmiReport.Click
        Dim x As TreeNode = tvList.SelectedNode
        Dim p As BoundaryItem
        Dim xRel As OSMRelation
        Dim sFile As String
        p = DirectCast(x.Tag, BoundaryItem)
        Dim iRel As Long = p.OSMRelation

        If iRel > 0 Then
            sFile = SpecialDirectories.Temp & "\r" & iRel & ".htm"
            xRel = TryCast(xRetriever.GetOSMObject(tmpDoc, OSMObject.ObjectType.Relation, iRel), OSMRelation)
            Dim rep As New RelationReport(tmpDoc, xRetriever)
            rep.RelationReport(sFile, xRel)
            OpenBrowserAt(sFile)
        End If
    End Sub

    Private Sub tsmiAddChild_Click(sender As Object, e As EventArgs) Handles tsmiAddChild.Click
        Dim x As TreeNode = tvList.SelectedNode
        Dim p As BoundaryItem
        p = DirectCast(x.Tag, BoundaryItem)
        Dim iRel As Long = p.OSMRelation
        Dim tvn As TreeNode

        Dim xItem As New BoundaryItem(xDB)
        xItem.Parent = p
        xItem.ParentCode = p.ONSCode
        Select Case p.BoundaryType
            Case BoundaryItem.BoundaryTypes.BT_Unitary,
                 BoundaryItem.BoundaryTypes.BT_MetroDistrict,
                 BoundaryItem.BoundaryTypes.BT_NonMetroDistrict
                xItem.BoundaryType = BoundaryItem.BoundaryTypes.BT_CivilParish
            Case BoundaryItem.BoundaryTypes.BT_PrincipalArea
                xItem.BoundaryType = BoundaryItem.BoundaryTypes.BT_Community
        End Select
        If xItem.Edit() Then
            ' frmEdit adds it to the DB already
            ' xDB.Items.Add(xItem.ONSCode, xItem)
            tvn = x.Nodes.Add(xItem.ONSCode, "")
            tvn.Tag = xItem
            tvn.ContextMenuStrip = cmsNode
            LoadTreeNodeText(tvn)
            MsgBox("edit successful")
        End If
    End Sub

    Private Sub SetParishGroup(ByVal sender As Object, ByVal e As EventArgs)
        Dim myItem As ToolStripMenuItem
        Dim xGroupItem As BoundaryItem
        Dim xParishItem As BoundaryItem

        ' Extract the tag value from the item received.
        myItem = CType(sender, ToolStripMenuItem)
        xGroupItem = CType(myItem.Tag, BoundaryItem)

        ' get the node to operate on
        Dim x As TreeNode = tvList.SelectedNode
        xParishItem = DirectCast(x.Tag, BoundaryItem)

        ' set its parish group name into CouncilName
        If xParishItem.BoundaryType <> BoundaryItem.BoundaryTypes.BT_CivilParish Then
            Return
        End If
        If xParishItem.ParishType <> BoundaryItem.ParishTypes.PT_JointParishCouncil AndAlso
            xParishItem.ParishType <> BoundaryItem.ParishTypes.PT_JointParishMeeting Then
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
        Dim p As BoundaryItem
        p = DirectCast(x.Tag, BoundaryItem)
        Dim f As New frmReview(New BoundaryDBReviewProvider, p, "")
        f.ShowDialog(Me)
    End Sub

    Private Sub tsmiChildOverviewReport_Click(sender As Object, e As EventArgs) Handles tsmiChildOverviewReport.Click
        Dim x As TreeNode = tvList.SelectedNode
        If x Is Nothing Then Return
        If x.Tag Is Nothing Then Return
        Dim p As BoundaryItem
        p = DirectCast(x.Tag, BoundaryItem)
        DoChildOverviewReport(p, False)
    End Sub

    Private Sub DoChildOverviewReport(p As BoundaryDB.BoundaryItem, Optional bDeep As Boolean = False)
        ' Type = boundary, boundary=administrative, admin_level, designation, name, name:en, Name: cy,
        ' website, council_name, council_name: en, council_name: cy, council_style, parish_type, source,
        ' ref: gss
        Static hdrs() As String = {"+status", "+dbname", "+dbtype", "+osmid", "+osmver", "type", "boundary", "admin_level", "designation", "name", "council_name", "council_style", "parish_type", "ref:gss"}

        With sfdReports
            .Filter = "CSV Files|*.csv|All files|*.*"
            .FilterIndex = 0
            .FileName = ""
            .CheckPathExists = True
            If .ShowDialog() = DialogResult.OK Then
                Dim csv As New CSVWriter(.FileName)
                csv.WriteLine(hdrs)
                DoChildOverviewReportEx(hdrs, csv, p, bDeep)
                csv.Close()
            End If
        End With
    End Sub
    Private Sub DoChildOverviewReportEx(hdrs() As String, csv As CSVWriter, p As BoundaryItem, bDeep As Boolean)
        Dim cols(hdrs.Length) As String
        Dim r As OSMRelation
        Dim i As Integer
        Dim status As String
        For Each pr In p.Children
            If pr.OSMRelation > 0 Then
                r = TryCast(xRetriever.GetOSMObject(OSMObject.ObjectType.Relation, pr.OSMRelation), OSMRelation)
                If Not r Is Nothing Then
                    status = "ok"
                Else
                    status = "cannot retrieve"
                End If
            Else
                r = Nothing
                status = "unknown"
            End If

            For i = 0 To hdrs.Length - 1
                If hdrs(i).Chars(1) = "+" Then
                    Select Case hdrs(i)
                        Case "+status"
                            cols(i) = status
                        Case "+dbtype"
                            cols(i) = BoundaryItem.BoundaryType_ToString(pr.BoundaryType)
                        Case "+osmid"
                            cols(i) = pr.OSMRelation.ToString()
                        Case "+dbname"
                            cols(i) = pr.Name
                        Case "+osmver"
                            If r Is Nothing Then
                                cols(i) = ""
                            Else
                                cols(i) = r.Version.ToString()
                            End If
                    End Select
                Else
                    If r Is Nothing Then
                        cols(i) = ""
                    Else
                        cols(i) = r.Tag(hdrs(i))
                    End If
                End If
            Next
            csv.WriteLine(cols)
        Next
        If bDeep Then
            For Each pr In p.Children
                If pr.Children.Count > 0 Then
                    DoChildOverviewReportEx(hdrs, csv, pr, bDeep)
                End If
            Next
        End If
    End Sub

    Private Sub tsmiDeepChildReport_Click(sender As Object, e As EventArgs) Handles tsmiDeepChildReport.Click
        Dim x As TreeNode = tvList.SelectedNode
        If x Is Nothing Then Return
        If x.Tag Is Nothing Then Return
        Dim p As BoundaryItem
        p = DirectCast(x.Tag, BoundaryItem)
        DoChildOverviewReport(p, True)
    End Sub

    Private Sub tvList_BeforeExpand(sender As Object, e As TreeViewCancelEventArgs) Handles tvList.BeforeExpand
        If Not bAllowExpandCollapse Then
            e.Cancel = True
            Return
        End If
        Dim n As TreeNode = e.Node
        ExpandNode(n)
    End Sub

    Private Sub ExpandNode(n As TreeNode)
        Dim xItem As BoundaryItem = DirectCast(n.Tag, BoundaryItem)
        If n.Nodes.Count <> 1 Then Return
        If n.Nodes(0).Text <> DUMMY_MARKER Then Return
        n.Nodes(0).Remove()
        LoadTreeNode(tvList, n, xItem)
    End Sub

    Private Sub LoadTreeNodeText(tvn As TreeNode)
        Dim iCount As Integer, iCountOSM As Integer
        Dim sName As String
        Dim colRequired As Color
        If tvn Is Nothing OrElse tvn.Tag Is Nothing Then Exit Sub
        Dim xItem As BoundaryItem = DirectCast(tvn.Tag, BoundaryItem)
        iCount = xItem.NumChildren
        iCountOSM = xItem.NumKnownChildren
        sName = DisplayString(xItem)
        If iCount > 0 Then
            sName = $"{sName} [{iCountOSM}/{iCount}]"
        End If
        If iCount > 0 AndAlso iCountOSM = iCount Then
            colRequired = Color.Green
        Else
            colRequired = Color.Black
        End If

        If xItem.OSMRelation > 0 Then
            tvn.NodeFont = fntBold
        Else
            If xItem.BoundaryType = BoundaryItem.BoundaryTypes.BT_ParishGroup Then
                Dim parts = xItem.GroupMembers
                If parts Is Nothing Then
                    tvn.NodeFont = fntNormal
                Else
                    tvn.NodeFont = fntBold
                    For Each d In parts
                        If d.OSMRelation = 0 Then
                            tvn.NodeFont = fntNormal
                            Exit For
                        End If
                    Next
                End If
            Else
                tvn.NodeFont = fntNormal
            End If
        End If
        If tvn.ForeColor <> colRequired Then
            tvn.ForeColor = colRequired
        End If
        If tvn.Text <> sName Then
            tvn.Text = sName
        End If
    End Sub
    ''' <summary>
    ''' Adds all the children of the given node in the TreeView. If they have children, a dummy grandchild node is added.
    ''' </summary>
    ''' <param name="tv">The TreeView control</param>
    ''' <param name="n">The TreeNode whose children are to be loaded</param>
    ''' <param name="xItem">The BoundaryItem corresponding to the TreeNode</param>
    Private Sub LoadTreeNode(tv As TreeView, n As TreeNode, xItem As BoundaryItem)
        Dim tvn As TreeNode
        ' ensure the nodes are sorted by name
        Dim xList As New SortedList(Of String, BoundaryItem)
        Dim sName As String
        For Each xChild In xItem.Children
            If (Not chkShowDeleted.Checked) AndAlso xChild.IsDeleted Then Continue For
            sName = xChild.Name & xChild.TypeCode
            If xList.ContainsKey(sName) Then
                MsgBox($"Duplicate node name {sName}", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            Else
                xList.Add(sName, xChild)
            End If
        Next
        Dim xEntry As BoundaryItem
        tv.Sorted = False
        tv.BeginUpdate()
        For Each xKVP As KeyValuePair(Of String, BoundaryItem) In xList
            xEntry = xKVP.Value

            If n Is Nothing Then
                tvn = tv.Nodes.Add(xEntry.ONSCode, "")
            Else
                tvn = n.Nodes.Add(xEntry.ONSCode, "")
            End If

            tvn.Tag = xEntry
            tvn.ContextMenuStrip = cmsNode

            LoadTreeNodeText(tvn)

            If xEntry.Children.Count > 0 Then
                tvn.Nodes.Add(DUMMY_MARKER)
            End If
        Next
        tv.EndUpdate()
    End Sub

    Private Sub tvList_DoubleClick(sender As Object, e As EventArgs) Handles tvList.DoubleClick
        Dim iRel As Long
        Dim xItem As BoundaryItem

        xRetriever.MaxAge = My.Settings.MaxCacheAge

        If tvList.SelectedNode Is Nothing Then Exit Sub
        If tvList.SelectedNode.Tag Is Nothing Then Exit Sub
        xItem = DirectCast(tvList.SelectedNode.Tag, BoundaryItem)
        If Not IsNothing(xItem) Then
            If xItem.BoundaryType = BoundaryItem.BoundaryTypes.BT_ParishGroup Then
                ShowParishGroup(xItem)
            Else
                iRel = xItem.OSMRelation
                If iRel > 0 Then
                    Dim xRel As OSMRelation = ShowRelation(iRel)
                    If xRel IsNot Nothing Then ZoomToLayer(RelationLabel(xRel))
                Else
                    If xItem.Lat * xItem.Lon <> 0.0 Then
                        ShowPlaceHolder(xItem)
                        GotoPanZoom(xItem.Lat, xItem.Lon, 13)
                    End If
                End If
            End If
        End If
    End Sub
    Private Function ShowParishGroup(xItem As BoundaryItem) As OSMRelation
        Dim rTemp As OSMRelation = Nothing
        Dim xRel As OSMRelation
        Dim xResolver As OSMResolver = Nothing
        Dim parts As List(Of BoundaryItem)

        parts = xItem.GroupMembers
        If parts Is Nothing Then Return Nothing

        For Each d In parts
            If d.OSMRelation = 0 Then Continue For
            xRel = DirectCast(xRetriever.GetOSMObject(tmpDoc, OSMObject.ObjectType.Relation, d.OSMRelation), OSMRelation)
            If xRel Is Nothing Then
                MsgBox($"Unable to retrieve relation #{d.OSMRelation} ({d.Name})")
            Else
                If rTemp Is Nothing Then
                    rTemp = DirectCast(xRel.Clone(), OSMRelation)
                Else
                    rTemp = rTemp.Combine(xRel)
                End If
            End If
        Next

        If rTemp Is Nothing Then Return Nothing
        Try
            rTemp.Tags("admin_level") = New OSMTag("admin_level", "10")
            rTemp.Tags("parish_type") = New OSMTag("parish_type", "parish_group") ' special value to trigger javascript
            rTemp.Tags("ref:gss") = New OSMTag("ref:gss", xItem.ONSCode)
            rTemp.Tags("name") = New OSMTag("name", xItem.Name)
        Catch
        End Try

        ' resolve into rings
        xResolver = New OSMResolver(rTemp)
        ' draw boundary of combined area if any
        If xResolver IsNot Nothing Then
            Dim sJson As String
            sJson = xResolver.GeoJSON
            ShowJSON(sJson, xItem.ONSCode)
        End If
        Return rTemp
    End Function
    Private Sub frmAnalyze_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Dim sbTmp As New StringBuilder
        ' column widths
        For i = 0 To lvChildren.Columns.Count - 1
            If i > 0 Then
                sbTmp.Append(",")
            End If
            sbTmp.Append(lvChildren.Columns(i).Width)
        Next
        My.Settings.ListColumnWidth = sbTmp.ToString()
        ' column display order
        sbTmp.Clear()
        For i = 0 To lvChildren.Columns.Count - 1
            If i > 0 Then
                sbTmp.Append(",")
            End If
            sbTmp.Append(lvChildren.Columns(i).DisplayIndex)
        Next
        My.Settings.ListColumnOrder = sbTmp.ToString()
        ' sorting column and order
        sbTmp.Clear()
        If m_SortingColumn Is Nothing Then
        Else
            My.Settings.ListColumnSorting = m_SortingColumn.Index.ToString & "," & lvChildren.Sorting.ToString()
        End If

    End Sub

    Private Sub frmAnalyze_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' restore listview settings
        Dim iWidth As Integer
        Dim sTmp As String
        Dim aTmp() As String
        lvSearchResults.Visible = False
        ' column width
        sTmp = My.Settings.ListColumnWidth
        aTmp = Split(sTmp, ",")
        If UBound(aTmp) = lvChildren.Columns.Count - 1 Then
            For i = 0 To lvChildren.Columns.Count - 1
                If Integer.TryParse(aTmp(i), iWidth) Then
                    If iWidth > 10 And iWidth < 500 Then
                        lvChildren.Columns(i).Width = iWidth
                    End If
                End If
            Next
        End If
        'column display order
        ' this needs to be set in order of the displayindex values to stop things getting confused
        sTmp = My.Settings.ListColumnOrder
        aTmp = Split(sTmp, ",")
        Dim tmpMap As New SortedList(Of Integer, Integer)
        If UBound(aTmp) = lvChildren.Columns.Count - 1 Then
            For i = 0 To lvChildren.Columns.Count - 1
                If Integer.TryParse(aTmp(i), iWidth) Then
                    tmpMap(i) = iWidth
                End If
            Next
            For Each i In tmpMap.Keys
                If tmpMap(i) >= 0 And tmpMap(i) < lvChildren.Columns.Count Then
                    lvChildren.Columns(i).DisplayIndex = tmpMap(i)
                End If
            Next
        End If
        ' sort column and order
        sTmp = My.Settings.ListColumnSorting
        aTmp = Split(sTmp, ",")
        If UBound(aTmp) = 1 Then
            If Integer.TryParse(aTmp(0), iWidth) Then
                m_SortingColumn = lvChildren.Columns(iWidth)
            End If
            If Integer.TryParse(aTmp(1), iWidth) Then
                lvChildren.Sorting = DirectCast(iWidth, SortOrder)
            End If
        End If
        scon1.SplitterDistance = My.Settings.frmAnalyze_Splitter1
        scon2.SplitterDistance = My.Settings.frmAnalyze_Splitter2
        Me.WindowState = DirectCast(My.Settings.frmAnalyze_MinMax, FormWindowState)
        loading = False
    End Sub

    Private Sub tvList_MouseDown(sender As Object, e As MouseEventArgs) Handles tvList.MouseDown
        '   check if clicked on node's plus/minus sign
        Dim info As TreeViewHitTestInfo = tvList.HitTest(e.X, e.Y)
        If info IsNot Nothing AndAlso info.Location = TreeViewHitTestLocations.PlusMinus Then
            '   in that case, allow expand/collapse
            bAllowExpandCollapse = True
        Else
            '   otherwise don't allow expand/collapse
            bAllowExpandCollapse = False
        End If
    End Sub

    Private Sub tvList_BeforeCollapse(sender As Object, e As TreeViewCancelEventArgs) Handles tvList.BeforeCollapse
        If Not bAllowExpandCollapse Then
            e.Cancel = True
            Return
        End If
    End Sub

    ''' <summary>
    ''' Search menu item - uses Nominatim to search for the selected place, and centres the map on that place
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tsmiSearchNom_Click(sender As Object, e As EventArgs) Handles tsmiSearchNom.Click
        If Not bMapInitDone Then Exit Sub
        Dim x As TreeNode = tvList.SelectedNode
        If x Is Nothing Then Exit Sub
        Dim p As BoundaryDB.BoundaryItem = DirectCast(x.Tag, BoundaryDB.BoundaryItem)
        If p Is Nothing Then Exit Sub
        Dim bb As New BBox
        If Not GetMapBBox(bb) Then Exit Sub
        Dim dTmp As Double
        dTmp = (bb.MaxLon - bb.MinLon) / 2
        bb.MaxLon += dTmp
        If bb.MaxLon > 180.0 Then bb.MaxLon -= 180.0
        If bb.MaxLon < -180.0 Then bb.MaxLon += 180.0
        bb.MinLon -= dTmp
        If bb.MinLon > 180.0 Then bb.MinLon -= 180.0
        If bb.MinLon < -180.0 Then bb.MinLon += 180.0
        dTmp = (bb.MaxLat - bb.MinLat) / 2
        bb.MaxLat += dTmp
        If bb.MaxLat > 90.0 Then bb.MaxLat = 90.0
        If bb.MaxLat < -90.0 Then bb.MaxLat = -90.0
        bb.MinLat -= dTmp
        If bb.MinLat > 90.0 Then bb.MinLat = 90.0
        If bb.MinLat < -90.0 Then bb.MinLat = -90.0
        Dim sURL As String = String.Format(My.Settings.NominatimURL,
                                           System.Net.WebUtility.HtmlEncode(p.Name),
                                           CStr(bb.MinLon), CStr(bb.MaxLat), CStr(bb.MaxLon), CStr(bb.MinLat))
        Dim sResult As String

        'MsgBox(sURL)

        Dim req As New System.Net.WebClient
        req.Encoding = System.Text.Encoding.UTF8
        sResult = req.DownloadString(sURL)

        'MsgBox(sResult)

        Dim xDoc As New System.Xml.XmlDocument()
        xDoc.LoadXml(sResult)
        Dim xPlace As System.Xml.XmlNode = Nothing
        Dim xPlaces As System.Xml.XmlNodeList = xDoc.SelectNodes("/searchresults/place")
        If xPlaces.Count = 0 Then
            MsgBox("Nominatim returned no results.")
            Exit Sub
        ElseIf xPlaces.Count > 1 Then
            Dim f As New frmChoosePlace
            f.xDoc = xDoc
            If f.ShowDialog() = DialogResult.OK Then
                xPlace = f.SelectedPlace
            End If
        Else
            xPlace = xPlaces(0)
        End If
        If xPlace Is Nothing Then Exit Sub
        Dim dLat As Double, dLon As Double
        dLon = Double.Parse(xPlace.SelectSingleNode("@lon").InnerText)
        dLat = Double.Parse(xPlace.SelectSingleNode("@lat").InnerText)
        tbpMap.Select()
        GotoPanZoom(dLat, dLon, 15)
    End Sub

    Private Sub scon1_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles scon1.SplitterMoved
        If Not loading Then
            If scon1.Orientation = Orientation.Vertical Then
                My.Settings.frmAnalyze_Splitter1 = e.SplitX
            Else
                My.Settings.frmAnalyze_Splitter1 = e.SplitY
            End If
            My.Settings.Save()
        End If
    End Sub

    Private Sub scon2_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles scon2.SplitterMoved
        If Not loading Then
            If scon2.Orientation = Orientation.Vertical Then
                My.Settings.frmAnalyze_Splitter2 = e.SplitX
            Else
                My.Settings.frmAnalyze_Splitter2 = e.SplitY
            End If
            My.Settings.Save()
        End If
    End Sub

    Private Sub tsmiOpenWebsite_Click(sender As Object, e As EventArgs) Handles tsmiOpenWebsite.Click
        Dim x As TreeNode = tvList.SelectedNode
        If x Is Nothing Then Exit Sub
        Dim p As BoundaryItem = DirectCast(x.Tag, BoundaryItem)
        If p Is Nothing Then Exit Sub
        If Len(p.Website) > 0 Then OpenBrowserAt(p.Website)
    End Sub

    Private Sub tsmiChildOpenWebsite_Click(sender As Object, e As EventArgs) Handles tsmiChildOpenWebsite.Click
        If lvChildren.SelectedItems.Count <> 1 Then Return
        Dim lvi As ListViewItem = lvChildren.SelectedItems(0)
        If IsNothing(lvi) Then Return
        Dim x As TreeNode = DirectCast(lvi.Tag, TreeNode)
        If IsNothing(x) Then Return
        Dim bi As BoundaryItem
        bi = DirectCast(x.Tag, BoundaryItem)
        If bi Is Nothing Then Exit Sub
        If Len(bi.Website) > 0 Then OpenBrowserAt(bi.Website)
    End Sub

    Private Sub tsmiChildReview_Click(sender As Object, e As EventArgs) Handles tsmiChildReview.Click
        If lvChildren.SelectedItems.Count < 1 Then Return
        Dim items As New List(Of BoundaryItem)
        Dim x As TreeNode
        Dim bi As BoundaryItem
        Dim sTitle As String

        sTitle = " in " & biListViewParent.Name
        For Each lvi As ListViewItem In lvChildren.SelectedItems
            x = DirectCast(lvi.Tag, TreeNode)
            If x IsNot Nothing Then
                bi = DirectCast(x.Tag, BoundaryItem)
                If bi IsNot Nothing Then
                    If bi.IsDeleted Then Continue For
                    If bi.OSMRelation > 0 Then
                        items.Add(bi)
                    End If
                End If
            End If
        Next
        Dim f As New frmReview(New BoundaryDBReviewProvider, items, sTitle)
        f.ShowDialog()
    End Sub

    Private Sub cmsChild_Opening(sender As Object, e As CancelEventArgs) Handles cmsChild.Opening
        tsmiChildEdit.Enabled = (lvChildren.SelectedItems.Count = 1)
        tsmiChildOpenWebsite.Enabled = (lvChildren.SelectedItems.Count = 1)
        tsmiChildOverviewReport.Enabled = (lvChildren.SelectedItems.Count = 1)
        tsmiChildAnalyze.Enabled = (lvChildren.SelectedItems.Count = 1)
        tsmiChildShowInOsm.Enabled = (lvChildren.SelectedItems.Count = 1)
        tsmiChildReport.Enabled = (lvChildren.SelectedItems.Count = 1)
        tsmiChildReview.Enabled = (lvChildren.SelectedItems.Count >= 1)
        tsmiChildSubareaReview.Enabled = (lvChildren.SelectedItems.Count >= 1)
    End Sub

    Private Sub cmsNode_Opening(sender As Object, e As CancelEventArgs) Handles cmsNode.Opening
        tsmiAddChild.Enabled = (tvList.SelectedNode IsNot Nothing)
        tsmiDeepChildReport.Enabled = (tvList.SelectedNode IsNot Nothing)
        tsmiFlush.Enabled = (tvList.SelectedNode IsNot Nothing)
        tsmiJSON.Enabled = (tvList.SelectedNode IsNot Nothing)
        tsmiOpenWebsite.Enabled = (tvList.SelectedNode IsNot Nothing)
        tsmiAnalyze.Enabled = (tvList.SelectedNode IsNot Nothing)
        tsmiShowInOsm.Enabled = (tvList.SelectedNode IsNot Nothing)
        tsmiReport.Enabled = (tvList.SelectedNode IsNot Nothing)
        tsmiReview.Enabled = (tvList.SelectedNode IsNot Nothing)
        tsmiSubareaReview.Enabled = (tvList.SelectedNode IsNot Nothing)
        tsmiSearchNom.Enabled = (tvList.SelectedNode IsNot Nothing)
        tsmiShowAll.Enabled = (tvList.SelectedNode IsNot Nothing)
    End Sub

    Private Sub lvChildren_KeyDown(sender As Object, e As KeyEventArgs) Handles lvChildren.KeyDown
        If e.KeyData = (Keys.A Or Keys.Control) Then
            For Each item As ListViewItem In lvChildren.Items
                item.Selected = True
            Next
        End If
    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        With ofdUpload
            .Title = "Select osmChange file to upload"
            .Filter = "OSM Change files(*.osc)|*.osc|All files|*.*"
            .FileName = ""
            If .ShowDialog() = DialogResult.OK Then

            End If
        End With
    End Sub

    Private Sub chkShowDeleted_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowDeleted.CheckedChanged
        ReloadTV(xDB)
        Dim tvn As TreeNode = tvList.SelectedNode
        If tvn Is Nothing Then Return
        ShowChildren(tvn)
    End Sub

    Private Sub tsmiShowInOsm_Click(sender As Object, e As EventArgs) Handles tsmiShowInOsm.Click
        Dim x As TreeNode = tvList.SelectedNode
        If x Is Nothing Then Return
        Dim p As BoundaryItem = DirectCast(x.Tag, BoundaryItem)
        If p Is Nothing Then Return
        Dim sUrl As String = ""
        If p.OSMRelation > 0 Then
            sUrl = OSMObject.BrowseURL(OSMObject.ObjectType.Relation, p.OSMRelation)
        ElseIf p.Lat * p.lon <> 0.0 Then
            sUrl = OSMObject.BrowseUrl(p.Lat, p.Lon, My.Settings.BrowseZoom)
        End If
        If Len(sUrl) > 0 Then OpenBrowserAt(sUrl)
    End Sub

    Private Sub tsmiAnalyze_Click(sender As Object, e As EventArgs) Handles tsmiAnalyze.Click
        Dim x As TreeNode = tvList.SelectedNode
        If x Is Nothing Then Return
        Dim p As BoundaryItem = DirectCast(x.Tag, BoundaryItem)
        If p Is Nothing Then Return
        If p.OSMRelation = 0 Then Return
        Dim sUrl As String = AnalyzeUrl(p.OSMRelation)
        If Len(sUrl) > 0 Then OpenBrowserAt(sUrl)
    End Sub

    Private Function AnalyzeUrl(ID As Long) As String
        Dim sUrl = My.Settings.AnalyzeUrl
        sUrl = Replace(sUrl, "{id}", ID.ToString())
        Return sUrl
    End Function

    Private Sub tsmiChildAnalyze_Click(sender As Object, e As EventArgs) Handles tsmiChildAnalyze.Click
        If lvChildren.SelectedItems.Count <> 1 Then Return
        Dim lvi As ListViewItem = lvChildren.SelectedItems(0)
        If IsNothing(lvi) Then Return
        Dim x As TreeNode = DirectCast(lvi.Tag, TreeNode)
        If IsNothing(x) Then Return
        Dim bi As BoundaryItem = DirectCast(x.Tag, BoundaryItem)
        If bi Is Nothing Then Exit Sub
        If bi.OSMRelation = 0 Then Return
        Dim sUrl As String = AnalyzeUrl(bi.OSMRelation)
        If Len(sUrl) > 0 Then OpenBrowserAt(sUrl)
    End Sub

    Private Sub tsmiChildShowInOsm_Click(sender As Object, e As EventArgs) Handles tsmiChildShowInOsm.Click
        If lvChildren.SelectedItems.Count <> 1 Then Return
        Dim lvi As ListViewItem = lvChildren.SelectedItems(0)
        If IsNothing(lvi) Then Return
        Dim x As TreeNode = DirectCast(lvi.Tag, TreeNode)
        If IsNothing(x) Then Return
        Dim bi As BoundaryItem = DirectCast(x.Tag, BoundaryItem)
        If bi Is Nothing Then Exit Sub
        Dim sUrl As String
        If bi.OSMRelation > 0 Then
            sUrl = OSMObject.BrowseURL(OSMObject.ObjectType.Relation, bi.OSMRelation)
        ElseIf bi.Lat * bi.Lon <> 0.0 Then
            sUrl = OSMObject.BrowseUrl(bi.Lat, bi.Lon, My.Settings.BrowseZoom)
        End If
        If Len(sUrl) > 0 Then OpenBrowserAt(sUrl)
    End Sub

    Private Async Sub tsmiOpenDB_Click(sender As Object, e As EventArgs) Handles tsmiOpenDB.Click
        Dim sDB As String

        sDB = FindBoundaryDB()

        If Len(sDB) = 0 Then
            MsgBox("No Boundary XML file defined")
            Exit Sub
        End If

        sCache = System.Environment.ExpandEnvironmentVariables(My.Settings.OSMCache)
        If Len(sCache) > 0 Then
            If System.IO.File.Exists(sCache) Then
                tmpDoc = New OSMDoc()
                If bLargeCache Then
                    tmpDoc.LoadBigXML(sCache)
                Else
                    tmpDoc.Load(sCache)
                End If
            End If
        End If

        Dim x As New BoundaryDB
        Dim s As String = Await loadxml(sDB, x)

        If IsNothing(fntBold) Then
            fntNormal = tvList.Font
            fntBold = New Font(tvList.Font, FontStyle.Bold)
        End If

        ReloadTV(x)

        xDB = x
        sDBPath = sDB
    End Sub

    Private Sub tsmiImportCentroids_Click(sender As Object, e As EventArgs) Handles tsmiImportCentroids.Click
        Dim sLatLong As String
        Dim fi As System.IO.FileInfo

        If xDB Is Nothing Then
            MsgBox("Must load boundary database before importing centroids")
            Exit Sub
        End If
        sLatLong = My.Settings.LatLongFile
        With ofdBoundaries
            .Title = "Import Lat/Lon from GSS CSV File"
            If Len(sLatLong) > 0 Then
                fi = New System.IO.FileInfo(sLatLong)
                .FileName = fi.Name
                .InitialDirectory = fi.DirectoryName
            Else
                .FileName = ""
                .InitialDirectory = GetFolderPath(SpecialFolder.MyDocuments)
            End If
            If .ShowDialog <> DialogResult.OK Then
                Exit Sub
            End If
            sLatLong = .FileName
        End With
        If Len(sLatLong) > 0 AndAlso System.IO.File.Exists(sLatLong) Then
            tsStatus.Text = "Importing centroids from " & sLatLong
            If xDB.ImportLatLong(sLatLong) Then
                tsStatus.Text = "Centroids imported from " & sLatLong
                My.Settings.LatLongFile = sLatLong
            Else
                tsStatus.Text = "Error importing centroids from " & sLatLong
            End If
        End If
    End Sub

    Private Sub DoSearch(bUpdateAll As Boolean)
        Dim oDoc As OSMDoc
        Dim bBox As New BBox
        Dim iRel As Long
        Dim x As BoundaryItem
        Dim xNode As TreeNode
        Dim sURL As String
        Dim sAdminLevel As String = ""

        If Not GetMapBBox(bBox) Then
            xNode = tvList.SelectedNode
            If IsNothing(xNode) Then
                Exit Sub
            End If
            x = DirectCast(xNode.Tag, BoundaryItem)
            If IsNothing(x) Then
                Exit Sub
            End If
            iRel = x.OSMRelation
            If iRel <= 0 Then
                Exit Sub
            End If
            bBox = tmpDoc.Relations(iRel).BBox
        End If

        tsmiSearchAll.Enabled = False
        tsmiSearchNew.Enabled = False

        If Len(sAdminLevel) = 0 Then
            sURL = My.Settings.xapiAPI & $"?relation[type=boundary][bbox={bBox.MinLon},{bBox.MinLat},{bBox.MaxLon},{bBox.MaxLat}][@meta]"
        Else
            sURL = My.Settings.xapiAPI & $"?relation[type=boundary][admin_level={sAdminLevel}][bbox={bBox.MinLon},{bBox.MinLat},{bBox.MaxLon},{bBox.MaxLat}][@meta]"
        End If

        Try
            tsStatus.Text = "Retrieving " & sURL
            Application.DoEvents()
            oDoc = xRetriever.API.GetOSM(sURL)
            If IsNothing(oDoc) Then
                tsStatus.Text = "Query failed or returned no data: " & xRetriever.API.LastError
                tsmiSearchAll.Enabled = True
                tsmiSearchNew.Enabled = True
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox($"Unable to retrieve {sURL} : {ex.Message}")
            tsmiSearchAll.Enabled = True
            tsmiSearchNew.Enabled = True
            Exit Sub
        End Try

        tsStatus.Text = "Merging new data with library..."
        Application.DoEvents()
        xDB.MergeOSM(oDoc, bUpdateAll)
        If xDB.ChangedItems.Count > 0 Then

            Dim xExpanded As New List(Of BoundaryItem)
            Dim xSelected As BoundaryItem = TryCast(tvList.SelectedNode?.Tag, BoundaryItem)
            Dim selectedNodes(10) As String
            Dim xTmp As BoundaryItem = xSelected
            Dim i As Integer = 0
            Do
                If xTmp Is Nothing Then Exit Do
                selectedNodes(i) = xTmp.ONSCode
                xTmp = xTmp.Parent
                i += 1
            Loop

            ' For Each xNode In tvList.Nodes
            ' CollectExpanded(xNode, xExpanded)
            ' Next

            tsStatus.Text = "Updating tree..."
            Application.DoEvents()
            ReloadTV(xDB) ' only reloads top level initially!

            Dim j As Integer = i - 2
            If j > 0 Then
                i = tvList.Nodes.IndexOfKey(selectedNodes(j))
                xNode = tvList.Nodes(i)
                Do
                    bAllowExpandCollapse = True ' otherwise the expand gets cancelled
                    xNode.Expand()
                    If j <= 0 Then Exit Do
                    j -= 1
                    If xNode.Nodes.IndexOfKey(selectedNodes(j)) >= 0 Then
                        xNode = xNode.Nodes(xNode.Nodes.IndexOfKey(selectedNodes(j)))
                    End If
                Loop
            End If

            ' For Each xNode In tvList.Nodes
            ' RestoreExpanded(xNode, xExpanded)
            ' Next
        End If
        tsStatus.Text = "Update complete."
        MsgBox("Update complete.", MsgBoxStyle.OkOnly Or MsgBoxStyle.Information)
        tsmiSearchNew.Enabled = True
        tsmiSearchAll.Enabled = True
    End Sub
    Private Sub tsmiSearchNew_Click(sender As Object, e As EventArgs) Handles tsmiSearchNew.Click
        DoSearch(False)
    End Sub

    Private Sub tsmiSearchAll_Click(sender As Object, e As EventArgs) Handles tsmiSearchAll.Click
        DoSearch(True)
    End Sub

    Private Sub tsmiOsmBoundaries_DragEnter(sender As Object, e As DragEventArgs) Handles tsmiOsmBoundaries.DragEnter
        If IsNothing(xDB) Then Exit Sub
        If System.IO.File.Exists("C:\VMShare\mkgmap\work\uk\gbb.osm") Then
            xDB.UpdateFromOSMFile("C:\VMShare\mkgmap\work\uk\gbb.osm")
        ElseIf System.IO.File.Exists("L:\VMShare\mkgmap\work\uk\gbb.osm") Then
            xDB.UpdateFromOSMFile("L:\VMShare\mkgmap\work\uk\gbb.osm")
        Else
            MsgBox("gbb.osm not found")
        End If
    End Sub

    Private Sub tsmiEditMap_Click(sender As Object, e As EventArgs) Handles tsmiEditMap.Click
        Dim ctr As New DPoint(0, 0)
        Dim zoom As Integer
        If Not GetMapLoc(zoom, ctr) Then Return
        If zoom < OSMObject.MinEditZoom Then
            MsgBox($"Zoom in further to edit map")
            Return
        End If
        Dim sUrl As String = OSMObject.EditUrl(ctr.Y, ctr.X, zoom)
        If Len(sUrl) > 0 Then OpenBrowserAt(sUrl)
    End Sub

    Private Sub tsmiBrowseMap_Click(sender As Object, e As EventArgs) Handles tsmiBrowseMap.Click
        Dim ctr As New DPoint(0, 0)
        Dim zoom As Integer
        If Not GetMapLoc(zoom, ctr) Then Return
        Dim sUrl As String = OSMObject.BrowseUrl(ctr.Y, ctr.X, zoom)
        If Len(sUrl) > 0 Then OpenBrowserAt(sUrl)
    End Sub

    Private Sub tsmiClearMap_Click(sender As Object, e As EventArgs) Handles tsmiClearMap.Click
        If Not bMapInitDone Then Return
        wbMap.Document.InvokeScript("clearLayers")
    End Sub

    ''' <summary>
    ''' Handles Click event on Close button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tsmiClose_Click(sender As Object, e As EventArgs) Handles tsmiClose.Click
        If Len(sCache) > 0 Then
            tmpDoc.SaveXML(sCache)
        End If
        Me.Close()
    End Sub

    Private Sub tsmiSearch_ButtonClick(sender As Object, e As EventArgs) Handles tsmiSearch.ButtonClick
        DoSearch(False)
    End Sub

    Private Sub btnParent_Click(sender As Object, e As EventArgs) Handles btnParent.Click
        Dim i As TreeNode = tvList.SelectedNode
        If i Is Nothing Then Return
        Dim p As TreeNode = i.Parent
        If p Is Nothing Then Return
        Dim bnd As BoundaryItem = DirectCast(p.Tag, BoundaryItem)
        If bnd Is Nothing Then Return
        tvList.SelectedNode = p
    End Sub
    Private Sub tsmiSearchText_GotFocus(sender As Object, e As EventArgs) Handles tsmiSearchText.GotFocus
        lvSearchResults.SelectedItems.Clear()
        lvSearchResults.Visible = True
    End Sub
    Private Sub tsmiSearchText_LostFocus(sender As Object, e As EventArgs) Handles tsmiSearchText.LostFocus
        ' lvSearchResults.Visible = False
    End Sub
    Private Sub InstantSearch(s As String)
        Dim maxHits As Integer = My.Settings.InstantSearchCount
        If maxHits < 1 Or maxHits > 1000 Then maxHits = 10
        s = Replace(s, "(", "\(")
        s = Replace(s, ")", "\)")
        s = Trim(s)
        If Len(s) = 0 Then Return
        Dim re As New RegularExpressions.Regex(s, RegularExpressions.RegexOptions.IgnoreCase)
        lvSearchResults.Items.Clear()
        For Each b In xDB.Items.Values
            If b.IsDeleted Then Continue For
            If re.IsMatch(b.Name) Then
                Dim p As BoundaryItem = b.Parent
                With lvSearchResults.Items.Add(b.TypeCode)
                    .SubItems.Add(b.Name)
                    .SubItems.Add(p.Name)
                    .Tag = b
                End With
                If lvSearchResults.Items.Count >= maxHits Then Exit Sub
            End If
        Next
    End Sub

    Private Sub tsmiSearchText_TextChanged(sender As Object, e As EventArgs) Handles tsmiSearchText.TextChanged
        If xDB Is Nothing Then Exit Sub
        InstantSearch(tsmiSearchText.Text)
    End Sub

    Private Sub tsmiSearchText_KeyUp(sender As Object, e As KeyEventArgs) Handles tsmiSearchText.KeyUp
        If e.KeyCode = Keys.Escape Then lvSearchResults.Visible = False
    End Sub

    Private Sub frmAnalyze_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then lvSearchResults.Visible = False
    End Sub

    Private Sub lvSearchResults_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvSearchResults.SelectedIndexChanged
        If lvSearchResults.SelectedItems.Count = 1 Then
            Dim b As BoundaryItem = DirectCast(lvSearchResults.SelectedItems(0).Tag, BoundaryItem)
            Dim s As String = b.Name
            lvSearchResults.Visible = False
            ' MsgBox($"Selected {s}")
            ShowBoundaryItem(b)
        End If
    End Sub
    Private Sub ShowBoundaryItem(bi As BoundaryItem)
        Dim lvls As New List(Of BoundaryItem)
        Dim b As BoundaryItem = bi
        Do
            If b.BoundaryType <> BoundaryItem.BoundaryTypes.BT_Country Then
                lvls.Add(b)
            End If
            b = b.Parent
        Loop Until b Is Nothing
        lvls.Reverse()
        ' first in list is top level
        Dim tn As TreeNode = tvList.Nodes.Item(0)
        While tn.Parent IsNot Nothing
            tn = tn.Parent
        End While
        ' select nation node
        Dim tbi As BoundaryItem
        For Each n As TreeNode In tvList.Nodes
            If n.Level = 0 Then
                tbi = DirectCast(n.Tag, BoundaryItem)
                If tbi Is lvls(0) Then
                    tn = n
                    lvls.Remove(lvls(0))
                End If
            End If
        Next
        ' unless we were looking for the nation node, drill down
        If DirectCast(tn.Tag, BoundaryItem) IsNot bi Then
            For Each b In lvls
                ExpandNode(tn)
                For Each t As TreeNode In tn.Nodes
                    tbi = DirectCast(t.Tag, BoundaryItem)
                    If tbi Is Nothing Then Continue For
                    If tbi Is b Then
                        bAllowExpandCollapse = True
                        tn.Expand()
                        tn = t
                        Exit For
                    End If
                Next
            Next
        End If
        If tn IsNot Nothing Then
            tn.EnsureVisible()
            tvList.SelectedNode = tn
        End If
    End Sub

    Private Sub tsmiChildSubareaReview_Click(sender As Object, e As EventArgs) Handles tsmiChildSubareaReview.Click
        MsgBox("child / subarea review")
    End Sub

    Private Sub tsmiSubareaReview_Click(sender As Object, e As EventArgs) Handles tsmiSubareaReview.Click
        MsgBox("subarea review")
    End Sub

    Private Sub tsmiOsmBoundaries_Click(sender As Object, e As EventArgs) Handles tsmiOsmBoundaries.Click
        Dim sIn As String = "C:\VMShare\mkgmap\work\uk\great-britain-latest.osm.pbf"
        '        Dim sIn As String = "C:\VMShare\mkgmap\work\uk\gbb.osm"
        Dim sOut As String = "C:\Temp\boundsrpt.csv"
        If Not System.IO.File.Exists(sIn) Then
            MsgBox($"{sIn} not found")
            Return
        End If
        Dim fOut As New IO.StreamWriter(sOut, False, Encoding.UTF8)
        Dim rpt = New OSMFileReporter(sIn, fOut)
        Dim bRet As Boolean = rpt.Run()
        fOut.Close()
        If bRet Then
            MsgBox($"CSV created in {sOut}")
        Else
            MsgBox("Error writing output file")
        End If
    End Sub
End Class


Public Class ListViewComparer
    Implements IComparer

    Private m_ColumnNumber As Integer
    Private m_SortOrder As SortOrder

    Public Sub New(ByVal column_number As Integer, ByVal sort_order As SortOrder)
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
