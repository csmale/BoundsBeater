Imports OSMLibrary
Imports System.Xml
Imports System.IO
Imports System.Data.SQLite
Imports Microsoft.VisualBasic.FileIO


Public Class frmMain
    Dim WithEvents oDoc As OSMDoc
    Dim log As myLogger

    ' The column currently used for sorting.
    Private m_SortingColumn As ColumnHeader

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        With OpenFileDialog1
            .Filter = "OSM Files (*.osm)|*.osm"
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                oDoc = New OSMDoc()
                lblStatus.Text = "Loading " & .FileName
                Application.DoEvents()
                oDoc.LoadBigXML(.FileName)
                lblStatus.Text = "Loaded " & .FileName & "; " & oDoc.Relations.Count & " relations."
                btnReport.Enabled = True
                LoadTree()
                LoadList()
            End If
        End With
    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        If oDoc Is Nothing Then
            Exit Sub
        End If
        With FolderBrowserDialog1
            If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim rep As New RelationReport(oDoc)
                rep.MakeReports(.SelectedPath)
            End If
        End With
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        InitSettings()
        lblStatus.Text = "Please load a file"
        Dim ver As String
        If (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed) Then
            With System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion
                ver = "V" & .Major & "." & .Minor & "." & .Build & "." & .Revision
            End With
        Else
            ver = "(unknown version)"
        End If
        Me.Text += " " + ver
        btnReport.Enabled = False
    End Sub

    Private Sub btnResolve_Click(sender As Object, e As EventArgs) Handles btnResolve.Click
        Dim r As OSMRelation
        Dim iRelID As Long
        If oDoc Is Nothing Then
            Exit Sub
        End If
        If Not IsNumeric(txtID.Text) Then
            Exit Sub
        End If
        iRelID = Long.Parse(txtID.Text)
        r = oDoc.Relations(iRelID)
        If IsNothing(r) Then
            MsgBox("Cannot find relation #" & iRelID & " in file.")
            Exit Sub
        End If
        Dim res As New OSMResolver
        res.Merge(r)
    End Sub

    ''' <summary>
    ''' Loads the TreeView with the boundaryDB
    ''' </summary>
    Private Sub LoadTree()
        Dim xRel As OSMRelation
        tvBounds.Nodes.Clear()
        tvBounds.BeginUpdate()
        For Each xRel In oDoc.Relations.Values
            tvBounds.Nodes.Add(xRel.ToString)
        Next
        tvBounds.EndUpdate()
    End Sub

    ''' <summary>
    ''' Loads the details of the child boundaries into the listview in the right pane
    ''' </summary>
    Private Sub LoadList()
        Dim xRel As OSMRelation
        lvBounds.Items.Clear()
        lvBounds.BeginUpdate()
        For Each xRel In oDoc.Relations.Values
            With lvBounds.Items.Add(xRel.ID.ToString)
                .Tag = xRel
                .SubItems.Add(xRel.Tag("type"))
                .SubItems.Add(xRel.Tag("admin_level"))
                .SubItems.Add(xRel.Name("en"))
                .SubItems.Add(xRel.Tag("designation"))
                .SubItems.Add(xRel.Tag("ref:gss"))
            End With
        Next
        lvBounds.EndUpdate()
    End Sub
    Private Sub tvReload_Click(sender As Object, e As EventArgs) Handles tvReload.Click
        LoadTree()
        Application.DoEvents()
        LoadList()
        Application.DoEvents()
    End Sub

    Private Sub oDoc_LoadProgress(nRels As ULong, nWays As ULong, nNodes As ULong) Handles oDoc.LoadProgress
        If (nNodes Mod 100) = 0 Then
            lblStatus.Text = "Loaded " & nRels & " relations, " & nWays & " ways, " & nNodes & " nodes..."
            Application.DoEvents()
        End If
    End Sub

    Private Sub lvBounds_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lvBounds.ColumnClick
        ' Get the new sorting column.
        Dim new_sorting_column As ColumnHeader = lvBounds.Columns(e.Column)

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
            m_SortingColumn.Text = m_SortingColumn.Text.Substring(2)
        End If

        ' Display the new sort order.
        m_SortingColumn = new_sorting_column
        If sort_order = SortOrder.Ascending Then
            m_SortingColumn.Text = "> " & m_SortingColumn.Text
        Else
            m_SortingColumn.Text = "< " & m_SortingColumn.Text
        End If

        ' Create a comparer.
        lvBounds.ListViewItemSorter = New BoundsListComparer(e.Column, sort_order)

        ' Sort.
        lvBounds.Sort()
    End Sub

    Private Sub btnReportOne_Click(sender As Object, e As EventArgs) Handles btnReportOne.Click
        Dim tmpDoc As OSMDoc
        Dim xRetriever As New OSMRetriever
        Dim xRel As OSMRelation
        Dim iRelID As Long
        Dim sFolder As String = "C:\Temp"
        Dim sFile As String
        If Not IsNumeric(txtID.Text) Then
            Exit Sub
        End If
        If Not Long.TryParse(txtID.Text, iRelID) Then
            MsgBox($"Relation ID {txtID.Text} must be numeric")
            Return
        End If
        If chkOnline.Checked Then
            tmpDoc = xRetriever.API.GetOSMDoc(OSMObject.ObjectType.Relation, iRelID, True)
            If IsNothing(tmpDoc) Then
                xRel = Nothing
            Else
                xRel = tmpDoc.Relations(iRelID)
                xRel.LoadNeighbours()
            End If
        Else
            If oDoc Is Nothing Then
                Exit Sub
            End If
            xRel = oDoc.Relations(iRelID)
            If xRel Is Nothing Then
                Exit Sub
            End If
        End If

        Dim rep As New RelationReport(oDoc)
        sFile = System.IO.Path.Combine(SpecialDirectories.Temp, $"r{xRel.ID}.htm")
        rep.RelationReport(sFile, xRel)

        OpenBrowserAt(sFile)
    End Sub

    Private Sub btnAnalyzer_Click(sender As Object, e As EventArgs) Handles btnAnalyzer.Click
        ActiveForm.Hide()
        frmAnalyze.ShowDialog(Me)
        Me.Show()
    End Sub

    Private Sub btnRelMgr_Click(sender As Object, e As EventArgs) Handles btnRelMgr.Click
        frmRelMgr.ShowDialog(Me)
    End Sub

    Private Sub btnRecreate_Click(sender As Object, e As EventArgs) Handles btnRecreate.Click
        Dim xRel As OSMRelation
        Dim xNew As XmlDocument
        Dim strMstream As New System.IO.MemoryStream
        If lvBounds.SelectedItems.Count <> 1 Then
            Exit Sub
        End If
        xRel = DirectCast(lvBounds.SelectedItems(0).Tag, OSMRelation)
        xNew = New XmlDocument()

        Dim xtw As New XmlTextWriter(strMstream, System.Text.Encoding.UTF8)
        xtw.Formatting = Formatting.Indented
        xtw.WriteStartDocument()
        xtw.WriteStartElement("osm")
        xRel.Serialize(xtw)
        xtw.WriteEndElement()
        xtw.WriteEndDocument()
        xtw.Flush()
        strMstream.Seek(0, IO.SeekOrigin.Begin)
        Dim s As String
        Dim sr As New System.IO.StreamReader(strMstream)
        s = sr.ReadToEnd
        MsgBox(s)
    End Sub



    Sub LoadTree(sFile As String)
        Dim xDoc As New XmlDocument
        Dim sName As String
        Dim sOns As String
        Dim xPar As TreeNode
        Dim xNode As TreeNode
        Dim sTag As String
        Dim onsPar As String
        tvDir.Nodes.Clear()

        xDoc.Load(sFile)
        For Each xBnd As XmlElement In xDoc.SelectNodes("/boundaries/boundary")
            sName = NodeText(xBnd.SelectSingleNode("name"))
            sOns = NodeText(xBnd.SelectSingleNode("ons"))
            onsPar = NodeText(xBnd.SelectSingleNode("par_ons"))
            With tvDir.Nodes.Add(sName)
                If Len(sOns) > 0 Then
                    .Name = sOns
                    .Tag = onsPar
                End If
            End With
        Next
        For Each xNode In tvDir.Nodes
            sTag = TryCast(xNode.Tag, String)
            If Len(sTag) > 0 Then  'we should have a parent
                If tvDir.Nodes.ContainsKey(sTag) Then
                    xPar = tvDir.Nodes(tvDir.Nodes.IndexOfKey(sTag))
                    xPar.Nodes.Add(xNode.Clone)
                End If
            End If
        Next
    End Sub

    Private Sub btnDir_Click(sender As Object, e As EventArgs) Handles btnDir.Click
        LoadTree("C:\VMShare\mkgmap\ccc.xml")
    End Sub
    Private Function NodeText(xNode As XmlNode) As String
        Return If(IsNothing(xNode), "", xNode.InnerText)
    End Function

    Private Sub btnAnalyseWay_Click(sender As Object, e As EventArgs) Handles btnAnalyseWay.Click
        Dim w As OSMWay
        Dim iWayID As Long
        Dim xRetriever As New OSMRetriever

        If Not IsNumeric(txtWayID.Text) Then
            Exit Sub
        End If
        iWayID = Long.Parse(txtWayID.Text)
        w = TryCast(xRetriever.GetOSMObject(OSMObject.ObjectType.Way, iWayID, True), OSMWay)
        If IsNothing(w) Then
            MsgBox("Cannot find way #" & iWayID & " in file.")
            Exit Sub
        End If
        MsgBox("Way #" & iWayID.ToString & " contains " & w.Nodes.Count & " nodes and is " & Math.Round(w.Length).ToString & "m long")
    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        Dim xCache As New OSMCache("C:\temp\cache.sqlite", True)
        Dim sTmp As String
        Dim oTmp As Object
        If xCache.AddObject(OSMObject.ObjectType.Way, 123, 1, Now(), "a string") Then
            oTmp = xCache.GetObject(OSMObject.ObjectType.Way, 123, 0)
            If Not IsNothing(oTmp) Then
                sTmp = oTmp.ToString
                MsgBox(sTmp)
            End If
        End If
        Exit Sub

        Dim x As New BoundaryDB("c:\VMShare\mkgmap\ccc.xml")
        Dim xItem As BoundaryDB.BoundaryItem
        tvDir.Nodes.Clear()

        ' load the root nodes - lower nodes get done recursively
        tvDir.BeginUpdate()
        For Each xItem In x.Items.Values
            If IsNothing(xItem.Parent) Then
                LoadTreeView(tvDir, Nothing, x, xItem)
            End If
        Next
        tvDir.Sort()
        tvDir.EndUpdate()
    End Sub
    Private Sub LoadTreeView(tv As TreeView, xParentNode As TreeNode, xDB As BoundaryDB, xItem As BoundaryDB.BoundaryItem)
        Dim tvn As TreeNode
        Dim xChild As BoundaryDB.BoundaryItem
        Dim sName As String

        sName = xItem.TypeCode & " " & xItem.Name
        If Len(xItem.Name2) > 0 And xItem.Name2 <> xItem.Name Then
            sName = sName & " (" & xItem.Name2 & ")"
        End If
        If IsNothing(xParentNode) Then
            tvn = tv.Nodes.Add(xItem.ONSCode, sName)
        Else
            tvn = xParentNode.Nodes.Add(xItem.ONSCode, sName)
        End If
        tvn.Tag = xItem
        For Each xChild In xDB.Items.Values
            If xChild.Parent Is xItem Then
                LoadTreeView(tv, tvn, xDB, xChild)
            End If
        Next
    End Sub
    Sub InitSettings()
        Dim bChanged As Boolean = False
        If My.Settings.MaxCacheAge < 1 Then
            My.Settings.MaxCacheAge = 86400
            bChanged = True
        End If
        If My.Settings.xapiAPI = "" Then
            My.Settings.xapiAPI = "http://overpass-api.de/api/xapi"
            bChanged = True
        End If
        If My.Settings.OSMCache = "" Then
            My.Settings.OSMCache = "%APPDATA%\BoundsBeater\OSMCache.osm"
            bChanged = True
        End If
        If My.Settings.BoundaryXML = "" Then
            My.Settings.BoundaryXML = "%APPDATA%\BoundsBeater\UKBoundaries.xml"
            bChanged = True
        End If
        If bChanged Then
            My.Settings.Save()
        End If
    End Sub

    Private Sub btnPrefs_Click(sender As Object, e As EventArgs) Handles btnPrefs.Click
        frmPreferences.ShowDialog()
    End Sub
End Class
