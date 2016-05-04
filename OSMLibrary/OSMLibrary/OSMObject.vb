Imports System.Xml
Imports System.Drawing

Public MustInherit Class OSMObject
    Const sBrowseURLBase As String = "http://www.openstreetmap.org/browse/"
    Public Enum ObjectType
        Node
        Way
        Relation
        Changeset
    End Enum
    Public ID As ULong
    Public Version As ULong
    Public UID As ULong
    Public User As String
    Public Changeset As ULong
    Public Timestamp As Date
    Public Cached As Date
    Public Visible As Boolean = True
    '    Public Versions As New Dictionary(Of Long, OSMObject)
    Public Versions As New LinkedList(Of OSMObject)
    Private __Tags As New OSMTagList
    Public Doc As OSMDoc
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Overridable ReadOnly Property Type As ObjectType
        Get
            Return Nothing
        End Get
    End Property
    Friend __Bbox As BBox
    Friend __Placeholder As Boolean = True
    Friend __HistoryLoaded As Boolean = False

    Public MustOverride ReadOnly Property JSON As String
    Public MustOverride ReadOnly Property GeoJSON As String
    Public MustOverride ReadOnly Property Bbox As BBox
    Public MustOverride ReadOnly Property Centroid As PointF

    Public Sub New()
        Cached = Now
    End Sub
    Public Sub New(x As OSMDoc)
        Cached = Now
        Doc = x
    End Sub
    ''' <summary>
    ''' Indicates if the object is a placeholder, i.e. has not been fully populated.
    ''' </summary>
    ''' <returns>True if the object is a placeholder</returns>
    Public ReadOnly Property IsPlaceholder As Boolean
        Get
            Return __Placeholder
        End Get
    End Property
    ''' <summary>
    ''' Indicates if the history of the object has been loaded, and not just the latest version.
    ''' </summary>
    ''' <returns>True if the object history is available</returns>
    Public ReadOnly Property HistoryLoaded As Boolean
        Get
            Return __HistoryLoaded
        End Get
    End Property
    ''' <summary>
    ''' Escapes a string for use in JSON, including adding double quotes at the
    ''' start and end.
    ''' </summary>
    ''' <param name="s">String to be escaped</param>
    ''' <returns>Escaped string</returns>
    Public Shared Function JSONString(s As String) As String
        Return """" & Replace(s, """", "\""") & """"
    End Function
    Public ReadOnly Property TagJSON As String
        Get
            Dim xTag As OSMTag
            Dim bFirst As Boolean = True
            Dim sJSON As New System.Text.StringBuilder
            For Each xTag In __Tags.Tags.Values
                If Not bFirst Then
                    sJSON.Append(",")
                End If
                bFirst = False
                sJSON.Append(JSONString(xTag.Key))
                sJSON.Append(":")
                sJSON.Append(JSONString(xTag.Value))
            Next
            Return sJSON.ToString
        End Get
    End Property
    Public ReadOnly Property TagGeoJSON As String
        Get
            Dim xTag As OSMTag
            Dim bFirst As Boolean = True
            Dim sJSON As New System.Text.StringBuilder
            If __Tags.Tags.Count = 0 Then
                Return ""
            End If
            sJSON.Append("{ ")
            sJSON.Append("_id")
            sJSON.Append(":")
            sJSON.Append(JSONString(ObjectTypeChar(Me.Type) & ID.ToString))
            For Each xTag In __Tags.Tags.Values
                sJSON.Append(",")
                bFirst = False
                sJSON.Append(JSONString(xTag.Key))
                sJSON.Append(":")
                sJSON.Append(JSONString(xTag.Value))
            Next
            sJSON.Append("}")
            Return sJSON.ToString
        End Get
    End Property
    Public ReadOnly Property Tags As Dictionary(Of String, OSMTag)
        Get
            Return __Tags.Tags
        End Get
    End Property
    Default ReadOnly Property Tag(Key As String) As String
        Get
            If __Tags.Tags.ContainsKey(Key) Then
                Return __Tags.Tags(Key).Value
            Else
                Return ""
            End If
        End Get
    End Property
    Public ReadOnly Property Name(Optional Lang As String = "en") As String
        Get
            Dim sTmp As String
            sTmp = Tag("name:" & Lang)
            If Len(sTmp) = 0 Then
                sTmp = Tag("name")
            End If
            Return sTmp
        End Get
    End Property
    ''' <summary>
    ''' Returns a string representing the object type
    ''' </summary>
    ''' <param name="t">An <c>ObjectType</c> value</param>
    ''' <returns>A string: way, node, relation, changeset or unknown</returns>
    Public Shared ReadOnly Property ObjectTypeString(t As ObjectType) As String
        Get
            Select Case t
                Case OSMObject.ObjectType.Way
                    Return "way"
                Case OSMObject.ObjectType.Node
                    Return "node"
                Case OSMObject.ObjectType.Relation
                    Return "relation"
                Case OSMObject.ObjectType.Changeset
                    Return "changeset"
            End Select
            Return "unknown"
        End Get
    End Property
    ''' <summary>
    ''' Returns a single character string representing the object type
    ''' </summary>
    ''' <param name="t">An <c>ObjectType</c> value</param>
    ''' <returns>A string: w, n, r, c or ?</returns>
    Public Shared ReadOnly Property ObjectTypeChar(t As ObjectType) As String
        Get
            Select Case t
                Case OSMObject.ObjectType.Way
                    Return "w"
                Case OSMObject.ObjectType.Node
                    Return "n"
                Case OSMObject.ObjectType.Relation
                    Return "r"
                Case OSMObject.ObjectType.Changeset
                    Return "c"
            End Select
            Return "?"
        End Get
    End Property
    ''' <summary>
    ''' Loads the generic fields (applicable to all object types) from the given XML
    ''' </summary>
    ''' <param name="x">The XML node containing the object data</param>
    Public Sub LoadGenericXML(x As Xml.XmlNode)
        Dim sTmp As String
        ID = CLng(x.Attributes("id").InnerText)
        UID = CLng(GetAttr(x, "uid", "0"))
        If Not IsNothing(Doc) And UID <> 0 Then
            If Not Doc.Users.ContainsKey(UID) Then
                Doc.Users(UID) = New OSMUser(UID, GetAttr(x, "user", "unknown"))
            End If
        End If
        User = GetAttr(x, "user", "")
        Version = CLng(GetAttr(x, "version", "0"))
        Changeset = CLng(GetAttr(x, "changeset", "0"))
        Try
            Timestamp = Date.Parse(GetAttr(x, "timestamp", "0"))
        Catch
        End Try
        Visible = (GetAttr(x, "visible", "true") <> "false")
        sTmp = GetAttr(x, "cached", "")
        If Len(sTmp) > 0 Then
            Cached = Date.Parse(sTmp)
        End If
        Dim xTag As XmlNode
        For Each xTag In x.SelectNodes("tag")
            __Tags.Add(New OSMTag(xTag))
        Next
        __Placeholder = False
    End Sub
    ''' <summary>
    ''' Returns an XML attribute, or a default value if not found
    ''' </summary>
    ''' <param name="x">The XML node</param>
    ''' <param name="Attr">The name of the attribute to be returned</param>
    ''' <param name="DefaultValue">(Optional)The default value if the attribute cannot be found.
    ''' If not found, then an empty string is assumed.</param>
    ''' <returns>The attribute value, or the default value</returns>
    Public Function GetAttr(x As Xml.XmlNode, Attr As String, Optional DefaultValue As String = "") As String
        Dim a As XmlAttribute
        If x Is Nothing Then
            Return DefaultValue
        End If
        a = x.Attributes(Attr)
        If a Is Nothing Then
            Return DefaultValue
        Else
            Return a.InnerText
        End If
    End Function
    ''' <summary>
    ''' Returns the URL on www.openstreetmap.org for a page with full details of the object
    ''' </summary>
    ''' <returns>The URL to view the object on www.openstreetmap.org</returns>
    Public ReadOnly Property BrowseURL() As String
        Get
            Dim sType As String
            If TypeOf Me Is OSMNode Then
                sType = "node"
            ElseIf TypeOf Me Is OSMWay Then
                sType = "way"
            ElseIf TypeOf Me Is OSMRelation Then
                sType = "relation"
            ElseIf TypeOf Me Is OSMChangeset Then
                sType = "changeset"
            Else
                sType = "??"
            End If
            Return sBrowseURLBase & sType & "/" & ID.ToString()
        End Get
    End Property
    ''' <summary>
    ''' Returns a string representing the object, contining the name (if present), the ID, and
    ''' a URL
    ''' </summary>
    ''' <returns>A string containing the name, ID and URL</returns>
    Public Shadows ReadOnly Property ToString() As String
        Get
            Dim sTmp As String
            Dim sName As String
            sName = Tag("name:en")
            If Len(sName) = 0 Then
                sName = Tag("name")
            End If
            If Len(sName) > 0 Then
                sTmp = sName & " (" & ID.ToString & ") " & BrowseURL()
            Else
                sTmp = ID.ToString() & " " & BrowseURL()
            End If
            Return sTmp
        End Get
    End Property
    ''' <summary>
    ''' Returns a <c>List(Of OSMRelationMember)</c>, a list of relations of which this
    ''' object is a member
    ''' </summary>
    ''' <returns>A list of relations of which this object is a member</returns>
    Public ReadOnly Property MemberOf() As List(Of OSMRelationMember)
        Get
            Dim xList As New List(Of OSMRelationMember)
            Dim xNewMbr As OSMRelationMember

            Dim iWanted As ObjectType
            iWanted = Me.Type

            ' for each relation in the file, see if this object is present in the list of members. if so, add to the result list
            If Not IsNothing(Doc) Then
                For Each xRel As OSMRelation In Doc.Relations.Values
                    For Each xMbr As OSMRelationMember In xRel.Members
                        If xMbr.Type = iWanted And xMbr.Member.ID = Me.ID Then
                            xNewMbr = New OSMRelationMember(xRel, xMbr.Role)
                            xList.Add(xNewMbr)
                        End If
                    Next
                Next
            End If
            Return xList
        End Get
    End Property
    Public Sub Serialize(x As XmlTextWriter)
        Dim sType As String
        If TypeOf Me Is OSMNode Then
            sType = "node"
        ElseIf TypeOf Me Is OSMWay Then
            sType = "way"
        ElseIf TypeOf Me Is OSMRelation Then
            sType = "relation"
        ElseIf TypeOf Me Is OSMChangeset Then
            sType = "changeset"
        Else
            Exit Sub
        End If
        x.WriteStartElement(sType)
        x.WriteAttributeString("id", ID.ToString)
        x.WriteAttributeString("visible", IIf(Visible, "true", "false"))
        x.WriteAttributeString("timestamp", Timestamp.ToUniversalTime.ToString("o"))
        x.WriteAttributeString("version", Version.ToString)
        x.WriteAttributeString("changeset", Changeset.ToString)
        x.WriteAttributeString("cached", Cached.ToUniversalTime.ToString("o"))
        If sType = "node" Then
            x.WriteAttributeString("lat", DirectCast(Me, OSMNode).Lat.ToString)
            x.WriteAttributeString("lon", DirectCast(Me, OSMNode).Lon.ToString)
        End If
        If IsNothing(Doc) Then
            x.WriteAttributeString("user", "No document")
        Else
            If Doc.Users.ContainsKey(UID) Then
                x.WriteAttributeString("user", Doc.Users(UID).Name)
            Else
                x.WriteAttributeString("user", "User unknown in document")
            End If
        End If
        x.WriteAttributeString("uid", UID.ToString)
        SerializeMe(x)
        SerializeTags(x)
        SerializeEnd(x)
        x.WriteEndElement()
    End Sub
    Public Overridable Sub SerializeMe(x As XmlTextWriter)
    End Sub
    Public Sub SerializeTags(x As XmlTextWriter)
        For Each k As String In Tags.Keys
            x.WriteStartElement("tag")
            x.WriteAttributeString("k", k)
            x.WriteAttributeString("v", Tags(k).Value)
            x.WriteEndElement()
        Next
    End Sub
    Public Overridable Sub SerializeEnd(x As XmlTextWriter)
    End Sub
    Public Overridable ReadOnly Property VersionByNumber(v As ULong) As OSMObject
        Get
            Dim vn As LinkedListNode(Of OSMObject)
            vn = Versions.First
            While vn IsNot Nothing
                If vn.Value.Version = v Then
                    Return vn.Value
                End If
                vn = vn.Next
            End While
            Return Nothing
        End Get
    End Property
    Public Overridable ReadOnly Property VersionByDate(d As DateTime) As OSMObject
        Get
            Dim vn As LinkedListNode(Of OSMObject)
            Dim nn As LinkedListNode(Of OSMObject)
            If Versions.Count = 0 Then
                Return Nothing
            End If
            vn = Versions.First
            If vn Is Nothing Then
                Return Nothing
            End If
            If vn.Value.Timestamp > d Then
                Return Nothing ' object did not exist yet
            End If
            nn = vn.Next
            While nn IsNot Nothing
                If nn.Value.Timestamp > d Then
                    Return vn.Value
                End If
                vn = nn
                nn = nn.Next
            End While
            Return vn.Value
        End Get
    End Property
    Public Sub InsertVersion(obj As OSMObject)
        Dim vn As LinkedListNode(Of OSMObject)
        vn = Versions.First
        While vn IsNot Nothing
            If obj.Version < vn.Value.Version Then
                Versions.AddBefore(vn, obj)
                Exit Sub
            End If
            vn = vn.Next
        End While
        Versions.AddLast(obj)
    End Sub
End Class
