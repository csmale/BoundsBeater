Imports OSMLibrary
Imports System.Xml
Imports System.IO
Imports System.Text

Public Class RelationReport
    Dim myLog As myLogger
    Dim oDoc As OSMDoc

    Sub New(doc As OSMDoc)
        oDoc = doc
    End Sub

    Public Sub MakeReports(sFolder As String)
        Dim xRel As OSMRelation
        Dim xFile As XmlTextWriter
        Dim xLevelLists(12) As SortedDictionary(Of String, OSMRelation)
        Dim iLvl As Integer
        Dim sLvl As String
        Dim sName As String
        myLog = New myLogger(sFolder & "\issues.txt")

        For Each xRelID As Long In oDoc.Relations.Keys
            xRel = oDoc.Relations(xRelID)
            If xRel.Tag("type") = "boundary" Or xRel.Tag("type") = "multipolygon" Then
                If IsNothing(xRel.Tag("admin_level")) Then
                    iLvl = 0
                Else
                    sLvl = xRel.Tag("admin_level")
                    If IsNumeric(sLvl) Then
                        iLvl = Integer.Parse(sLvl)
                        If iLvl < 1 Or iLvl > 12 Then iLvl = 0
                    Else
                        iLvl = 0
                    End If
                End If
                If IsNothing(xLevelLists(iLvl)) Then
                    xLevelLists(iLvl) = New SortedDictionary(Of String, OSMRelation)
                End If
                sName = xRel.Name("en")
                If Len(sName) = 0 Then
                    sName = xRel.ID.ToString
                End If
                While xLevelLists(iLvl).ContainsKey(sName)
                    sName = sName & "*"
                End While
                xLevelLists(iLvl).Add(sName, xRel)
                xFile = New XmlTextWriter(sFolder & "\r" & xRelID & ".htm", Nothing)
                xFile.Formatting = Formatting.Indented
                xFile.Indentation = 1
                xFile.IndentChar = Chr(9) ' vbTab
                RelationReport(xFile, xRel)
                xFile.Close()
                '                Exit Sub
            End If
        Next
        For i As Integer = 0 To 12
            If Not IsNothing(xLevelLists(i)) Then
                MakeLevelIndex(i, xLevelLists(i), sFolder & "\level_" & i & ".htm")
            End If
        Next
        xFile = New XmlTextWriter(sFolder & "\index.htm", Nothing)
        xFile.Formatting = Formatting.Indented
        xFile.Indentation = 1
        xFile.IndentChar = Chr(9) ' vbTab
        xFile.WriteDocType("html", Nothing, Nothing, Nothing)
        xFile.WriteStartElement("html")
        xFile.WriteStartElement("meta")
        xFile.WriteAttributeString("http-equiv", "Content-Type")
        xFile.WriteAttributeString("content", "text/html; charset=UTF-8")
        xFile.WriteEndElement()
        xFile.WriteStartElement("head")
        xFile.WriteElementString("title", "OSM Boundary Relations")

        xFile.WriteEndElement() ' head
        xFile.WriteStartElement("body")

        xFile.WriteElementString("h1", "OSM Boundary Relations")
        xFile.WriteElementString("h2", "Index for all admin levels")
        For i As Integer = 0 To 12
            If Not IsNothing(xLevelLists(i)) Then
                xFile.WriteStartElement("h3")
                PutAnchor(xFile, "level_" & i & ".htm", "Admin Level " & i)
                xFile.WriteEndElement()
                '                xFile.WriteElementString("br", "")
            End If
        Next

        xFile.WriteEndElement() ' body
        xFile.WriteEndElement() ' html
        xFile.Close()
        myLog.Close()
        OpenBrowserAt(sFolder & "\index.htm")
    End Sub
    Private Sub MakeLevelIndex(Level As Integer, RelList As SortedDictionary(Of String, OSMRelation), sFile As String)
        Dim xRel As OSMRelation
        Dim xFile As XmlTextWriter
        xFile = New XmlTextWriter(sFile, Nothing)
        xFile.Formatting = Formatting.Indented
        xFile.Indentation = 1
        xFile.IndentChar = Chr(9) ' vbTab
        xFile.WriteDocType("html", Nothing, Nothing, Nothing)
        xFile.WriteStartElement("html")
        xFile.WriteStartElement("meta")
        xFile.WriteAttributeString("http-equiv", "Content-Type")
        xFile.WriteAttributeString("content", "text/html; charset=UTF-8")
        xFile.WriteEndElement()
        xFile.WriteStartElement("head")
        xFile.WriteElementString("title", "OSM Level " & Level & " Boundary Relations")

        xFile.WriteEndElement() ' head
        xFile.WriteStartElement("body")

        xFile.WriteElementString("h1", "OSM Boundary Relations")
        xFile.WriteElementString("h2", "Index admin level " & Level)
        xFile.WriteElementString("br", "")

        xFile.WriteStartElement("table")
        xFile.WriteAttributeString("border", "1")
        xFile.WriteStartElement("thead")
        xFile.WriteStartElement("tr")
        xFile.WriteEndElement() ' tr
        xFile.WriteElementString("th", "ID")
        xFile.WriteElementString("th", "Name")
        xFile.WriteEndElement() ' thead
        xFile.WriteStartElement("tbody")

        For Each sKey As String In RelList.Keys
            xRel = RelList(sKey)
            xFile.WriteStartElement("tr")
            xFile.WriteStartElement("td")
            PutAnchor(xFile, "r" & xRel.ID.ToString & ".htm", xRel.ID.ToString)
            xFile.WriteEndElement() ' td
            xFile.WriteElementString("td", xRel.Tag("name"))
            xFile.WriteEndElement() ' tr
        Next

        xFile.WriteEndElement() ' tbody
        xFile.WriteEndElement() ' table

        xFile.WriteEndElement() ' body
        xFile.WriteEndElement() ' html
        xFile.Close()
    End Sub

    Public Sub RelationReport(sFile As String, xRel As OSMRelation)
        Dim xFile As XmlTextWriter
        xFile = New XmlTextWriter(sFile, Encoding.UTF8)
        xFile.Formatting = Formatting.Indented
        xFile.Indentation = 1
        xFile.IndentChar = Chr(9) ' vbTab
        Dim sFolder As String
        sFolder = System.IO.Path.GetDirectoryName(sFile)
        myLog = New myLogger(sFolder & "\issues.txt")
        RelationReport(xFile, xRel)
        xFile.Close()
        myLog.Close()
    End Sub

    Public Sub RelationReport(xFile As XmlTextWriter, xRel As OSMRelation)
        Dim xMbr As OSMRelationMember, xMbr2 As OSMRelationMember
        Dim sType As String
        Dim sList As String
        Dim xList As List(Of OSMRelationMember)
        Dim xNeighbours As List(Of OSMRelationMember)
        Dim xRel2 As OSMRelation
        Dim xRing As OSMResolver.Ring
        Dim xWay As OSMWay
        Dim sName As String
        Dim bCheckLevels As Boolean
        Dim bIsDuplicate As Boolean
        Dim xResolver As OSMResolver

        If xFile Is Nothing Then
            Exit Sub
        End If
        If xRel Is Nothing Then
            Exit Sub
        End If
        xResolver = New OSMResolver(xRel)
        Select Case xRel.Tag("type")
            Case "boundary", "multipolygon"
                xResolver.Mode = OSMResolver.ResolverMode.Polygon
            Case "waterway"
                xResolver.Mode = OSMResolver.ResolverMode.Linear
            Case Else
                myLog.Write(xRel, Nothing, "not boundary or multipolygon!")
                Exit Sub
        End Select
        bCheckLevels = False
        If xResolver.Mode = OSMResolver.ResolverMode.Polygon Then
            Select Case xRel.Tag("boundary")
                Case "administrative"
                    bCheckLevels = True
                Case "political"
                Case "ceremonial"
                Case Else
                    myLog.Write(xRel, Nothing, "boundary type not administrative, political or ceremonial")
            End Select
        End If

        Dim iRelLevel As Integer, iWayLevel As Integer
        If bCheckLevels Then
            Try
                iRelLevel = CInt(xRel.Tag("admin_level"))
            Catch
                myLog.Write(xRel, Nothing, "Missing or erroneous admin_level: " & xRel.Tag("admin_level"))
                iRelLevel = 0
            End Try
        Else
            iRelLevel = 0
        End If

        xFile.WriteDocType("html", Nothing, Nothing, Nothing)
        xFile.WriteStartElement("html")
        xFile.WriteStartElement("meta")
        xFile.WriteAttributeString("http-equiv", "Content-Type")
        xFile.WriteAttributeString("content", "text/html; charset=UTF-8")
        xFile.WriteEndElement()
        xFile.WriteStartElement("head")
        sName = xRel.Tag("name")
        If Len(sName) = 0 Then sName = $"Unnamed relation {xRel.ID}"
        xFile.WriteElementString("title", sName)

        xFile.WriteEndElement() ' head
        xFile.WriteStartElement("body")

        xFile.WriteStartElement("h1")
        xFile.WriteString("ID: ")
        PutAnchor(xFile, xRel.BrowseURL, xRel.ID.ToString)
        xFile.WriteString(" (" & xRel.Tag("type") & ")")
        xFile.WriteEndElement()

        xFile.WriteElementString("h2", "Boundary type: " & xRel.Tag("boundary"))
        xFile.WriteElementString("h2", "Name: " & xRel.Tag("name"))
        xFile.WriteStartElement("h2")
        xFile.WriteString("Level: " & xRel.Tag("admin_level"))
        If iRelLevel > 0 Then
            xFile.WriteString(" ")
            PutAnchor(xFile, "level_" & iRelLevel & ".htm", "(Level index)")
        End If
        xFile.WriteEndElement()
        xFile.WriteElementString("h3", "Official name: " & xRel.Tag("official_name"))
        xFile.WriteElementString("h3", "Designation: " & xRel.Tag("designation"))
        xFile.WriteElementString("h3", "Source: " & xRel.Tag("source"))
        xFile.WriteElementString("h3", "Version " & xRel.Version & ", last modified: " & xRel.Timestamp & " by: " & xRel.User)

        If xRel.Tags.Count > 0 Then
            xFile.WriteStartElement("table")
            xFile.WriteAttributeString("border", "1")
            xFile.WriteStartElement("thead")
            xFile.WriteStartElement("tr")
            xFile.WriteElementString("th", "Tag")
            xFile.WriteElementString("th", "Value")
            xFile.WriteEndElement() ' tr
            xFile.WriteEndElement() ' thead
            xFile.WriteStartElement("tbody")

            For Each t In xRel.Tags.Keys
                xFile.WriteStartElement("tr")
                xFile.WriteElementString("td", t)
                xFile.WriteElementString("td", xRel.Tag(t))
                xFile.WriteEndElement() ' tr
            Next

            xFile.WriteEndElement() ' tbody
            xFile.WriteEndElement() ' table
        End If

        If xResolver.Mode = OSMResolver.ResolverMode.Polygon Then
            For i = 0 To xResolver.Rings.Count - 1
                xRing = xResolver.Rings(i)
                xFile.WriteStartElement("h4")
                If xRing.isClosed Then
                    xFile.WriteString(String.Format("Ring {0} ({1}) closed, {2} ways, {4} nodes, length {3:F0}m, end node ", i, xRing.Role, xRing.Ways.Count, xRing.Length, xRing.NodeList.Count))
                    PutAnchor(xFile, xRing.Head.BrowseURL, xRing.Head.ID.ToString())
                Else
                    xFile.WriteString(String.Format("Ring {0} ({1}) not closed, {2} ways, {4} nodes, length {3:F0}m, from node ", i, xRing.Role, xRing.Ways.Count, xRing.Length, xRing.NodeList.Count))
                    PutAnchor(xFile, xRing.Head.BrowseURL, xRing.Head.ID.ToString())
                    xFile.WriteString(" to ")
                    PutAnchor(xFile, xRing.Tail.BrowseURL, xRing.Tail.ID.ToString())
                    myLog.Write(xRel, Nothing, "Ring " & i & " (" & xRing.Role & ") is not closed")
                End If
                xFile.WriteEndElement()
                If xRing.Role <> "outer" AndAlso xRing.Role <> "inner" AndAlso xRing.Role <> "" Then
                    xFile.WriteElementString("h4", String.Format("Ring {0} has unexpected role ({1}) ", i, xRing.Role))
                    myLog.Write(xRel, Nothing, String.Format("Ring {0} has unexpected role ({1}) ", i, xRing.Role))
                End If
            Next
        Else
            For i = 0 To xResolver.Rings.Count - 1
                xRing = xResolver.Rings(i)
                xFile.WriteStartElement("h4")
                If xRing.isClosed Then
                    xFile.WriteString(String.Format("Linestring {0} ({2}) closed, {3} ways, length {4:F0}m, end node {1}", i, xRing.Head.ID, xRing.Role, xRing.Ways.Count, xRing.Length))
                    myLog.Write(xRel, Nothing, "Linestring " & i & " (" & xRing.Role & ") is a closed loop - probable error")
                Else
                    xFile.WriteString(String.Format("Linestring {0} ({3}) not closed, {4} ways, length {5:F0}m, from node {1} to {2}", i, xRing.Head.ID, xRing.Tail.ID, xRing.Role, xRing.Ways.Count, xRing.Length))

                End If
                xFile.WriteEndElement()
                If xRing.Role <> "main_stream" AndAlso xRing.Role <> "side_stream" AndAlso xRing.Role <> "" Then
                    xFile.WriteElementString("h4", String.Format("Linestring {0} has unexpected role ({1}) ", i, xRing.Role))
                    myLog.Write(xRel, Nothing, String.Format("Linestring {0} has unexpected role ({1}) ", i, xRing.Role))
                End If
            Next
        End If

        If xResolver.Mode = OSMResolver.ResolverMode.Polygon Then
            If Not xResolver.checkGeometry() Then
                xFile.WriteElementString("h3", "Geometry error!!!")
            End If
        End If

        For Each xMbr In xRel.Members
            If xMbr.Type = OSMObject.ObjectType.Node Then
                If xMbr.Role = "admin_centre" Then
                    xFile.WriteStartElement("h3")
                    xFile.WriteString("Admin Centre: ")
                    PutAnchor(xFile, xMbr.Member.BrowseURL, xMbr.Member.ID.ToString())
                    xFile.WriteString(" " & xMbr.Member.Name("en") & " (" & xMbr.Member("place") & ")")
                    xFile.WriteEndElement()
                ElseIf xMbr.Role = "label" Then
                    xFile.WriteElementString("h3", "Label: " & xMbr.Member.ID.ToString)
                End If
            End If
        Next

        xNeighbours = xRel.MemberOf()
        If xNeighbours.Count > 0 Then
            xFile.WriteElementString("h3", "Member of: ")
            For Each xMbr In xNeighbours
                sName = xMbr.Member.Name("en")
                If Len(sName) = 0 Then
                    sName = xMbr.Member.ID.ToString
                End If
                If Len(xMbr.Role) > 0 Then
                    sName = sName & " (" & xMbr.Role & ")"
                End If
                PutAnchor(xFile, "r" & xMbr.Member.ID.ToString & ".htm", sName)
                xFile.WriteElementString("br", "")
            Next
        End If

        xFile.WriteElementString("h3", "Contains: ")
        For Each xMbr In xRel.Members
            Dim sTmp As String
            If xMbr.Type <> OSMObject.ObjectType.Way Then
                sName = xMbr.Member.Name("en")
                xFile.WriteStartElement("h4")
                Select Case xMbr.Type
                    Case OSMObject.ObjectType.Node
                        sTmp = "N "
                    Case OSMObject.ObjectType.Way
                        sTmp = "W "
                    Case OSMObject.ObjectType.Relation
                        sTmp = "R "
                    Case Else
                        sTmp = "?"
                End Select
                xFile.WriteString(sTmp)
                ' If xMbr.Type = OSMObject.ObjectType.Relation Then
                PutAnchor(xFile, xMbr.Member.BrowseURL, xMbr.Member.ID.ToString)
                ' Else
                ' xFile.WriteString(xMbr.Member.ID.ToString)
                'End If
                xFile.WriteString(" ")
                PutAnchor(xFile, "r" & xMbr.Member.ID.ToString & ".htm", sName)
                If Len(xMbr.Role) > 0 Then
                    xFile.WriteString(" (")
                    xFile.WriteString(xMbr.Role)
                    xFile.WriteString(")")
                End If
                xFile.WriteEndElement()
            End If
        Next

        xFile.WriteElementString("br", "")
        xFile.WriteStartElement("table")
        xFile.WriteAttributeString("border", "1")
        xFile.WriteStartElement("thead")
        xFile.WriteStartElement("tr")
        xFile.WriteElementString("th", "Ring")
        xFile.WriteElementString("th", "ID")
        xFile.WriteElementString("th", "lvl")
        xFile.WriteElementString("th", "Way")
        xFile.WriteElementString("th", "Ref")
        xFile.WriteElementString("th", "Name")
        xFile.WriteElementString("th", "Role")
        xFile.WriteElementString("th", "Nodes")
        xFile.WriteElementString("th", "Length")
        xFile.WriteElementString("th", "Source")
        xFile.WriteElementString("th", "Other rels")
        xFile.WriteElementString("th", "Last mod")
        xFile.WriteElementString("th", "By")
        xFile.WriteEndElement() ' tr
        xFile.WriteEndElement() ' thead
        xFile.WriteStartElement("tbody")
#If False Then
        bIsDuplicate = False
        For Each xMbr2 In xRel.Members
            If xMbr IsNot xMbr2 Then
                If xMbr.Member.ID = xMbr2.Member.ID Then
                    bIsDuplicate = True
                    WriteIssueLog(xRel, xMbr.Member, "Member occurs more than once")
                    Exit For
                End If
            End If
            If bIsDuplicate Then
                xFile.WriteString("* ")
            End If
        Next
#End If
        For i = 0 To xResolver.Rings.Count - 1
            xRing = xResolver.Rings(i)
            For Each xWay In xRing.Ways
                xFile.WriteStartElement("tr")
                xFile.WriteElementString("td", i.ToString)
                xFile.WriteStartElement("td")
                PutAnchor(xFile, xWay.BrowseURL, xWay.ID.ToString, "bbeater_osm")
                xFile.WriteEndElement()
                If bCheckLevels Then
                    Try
                        iWayLevel = Integer.Parse(xWay("admin_level"))
                    Catch
                        myLog.Write(xRel, xWay, "Missing or erroneous admin_level: " & xWay("admin_level"))
                        iWayLevel = 0
                    End Try
                Else
                    iWayLevel = 0
                End If

                If iRelLevel = 0 Then
                    xFile.WriteElementString("td", xWay("admin_level"))
                Else
                    If iRelLevel < iWayLevel Then
                        xFile.WriteStartElement("td")
                        xFile.WriteAttributeString("style", "color:red")
                        xFile.WriteString(xWay("admin_level"))
                        xFile.WriteEndElement()
                        myLog.Write(xRel, xWay, "Incorrect admin_level for this relation: " & xRel.Tag("admin_level"))
                    Else
                        xFile.WriteElementString("td", xWay("admin_level"))
                    End If
                End If
                If Len(xWay("highway")) > 0 Then
                    sType = "h:" & xWay("highway")
                ElseIf Len(xWay("waterway")) > 0 Then
                    sType = "w:" & xWay("waterway")
                Else
                    sType = ""
                End If
                xFile.WriteElementString("td", sType)
                xFile.WriteElementString("td", xWay("ref"))
                xFile.WriteElementString("td", xWay("name"))
                xFile.WriteElementString("td", xRing.Role)
                xFile.WriteElementString("td", xWay.Nodes.Count.ToString)
                xFile.WriteElementString("td", xWay.Length.ToString("0"))
                xFile.WriteElementString("td", xWay("source"))
                xFile.WriteStartElement("td")

                ' list of relations this way is also a member of
                xList = xWay.MemberOf()
                sList = ""
                For Each xMbr2 In xList
                    xRel2 = DirectCast(xMbr2.Member, OSMRelation)
                    If xRel2.ID <> xRel.ID Then
                        If Len(sList) > 0 Then xFile.WriteString(", ")
                        sList = "xx"
                        sName = xRel2.Name("en")
                        If Len(sName) = 0 Then
                            sName = xRel2.ID.ToString
                        End If
                        PutAnchor(xFile, "r" & xRel2.ID.ToString & ".htm", sName)
                    End If
                Next
                xFile.WriteEndElement()
                xFile.WriteElementString("td", xWay.Timestamp.ToString())
                xFile.WriteElementString("td", xWay.User)
                xFile.WriteEndElement() ' tr

            Next
        Next

        For Each xMbr In xRel.Members
            If xMbr.Type = OSMObject.ObjectType.Way Then
                xWay = DirectCast(xMbr.Member, OSMWay)

            End If
        Next

        xFile.WriteEndElement() ' tbody
        xFile.WriteEndElement() ' table

        xFile.WriteEndElement() ' body
        xFile.WriteEndElement() ' html
    End Sub
    Private Sub PutAnchor(xFile As XmlTextWriter, sURL As String, Optional sText As String = "", Optional sTarget As String = "")
        xFile.WriteStartElement("a")
        xFile.WriteAttributeString("href", sURL)
        If Len(sTarget) > 0 Then
            xFile.WriteAttributeString("target", sTarget)
        End If
        If Len(sText) > 0 Then
            xFile.WriteString(sText)
        Else
            xFile.WriteString(sURL)
        End If
        xFile.WriteEndElement()
    End Sub
End Class
