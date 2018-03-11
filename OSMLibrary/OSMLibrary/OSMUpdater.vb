Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Xml

''' <summary>
''' Handles updating OSM online by using the API
''' </summary>
Public Class OSMUpdater
        Public ReadOnly Property api As OSMApi
        Public ReadOnly Property credentials As NetworkCredential
        Public ReadOnly Property url As String = My.Settings.OSMBaseApiURL
        Public ReadOnly Property ChangesetID As Long = 0
        Public ReadOnly Property LastCreatedID As Long = 0

        ''' <summary>
        ''' Constructor using specified URL
        ''' </summary>
        ''' <param name="u">Base URL to OSM API</param>
        ''' <param name="creds">Login to OSM</param>
        Public Sub New(u As String, creds As NetworkCredential)
            url = u
            api = New OSMApi(url, creds)
        End Sub

        ''' <summary>
        ''' Constructor using default URL
        ''' </summary>
        ''' <param name="creds">Login to OSM</param>
        Public Sub New(creds As NetworkCredential)
            api = New OSMApi(url, creds)
        End Sub

        Public Function Open(sComment As String) As Long
            If ChangesetID <> 0 Then
                Throw New Exception("Changeset is already open")
            End If
            Dim sTmp As String
            Dim sPayload As String
            Dim cs As New OSMChangeset
            cs.Tags("created_by") = New OSMTag("created_by", "OSMUpdater")
            cs.Tags("version") = New OSMTag("version", "0.1")
            cs.Tags("comment") = New OSMTag("comment", sComment)

            Dim sb As New StringBuilder()
            Dim sw As New StringWriter(sb)
            Dim writer As XmlTextWriter = New XmlTextWriter(sw)
            ' write the XML to the writer here
            writer.WriteStartElement("osm")
            cs.Serialize(writer)
            writer.WriteEndElement()
            sPayload = sb.ToString
            ' At this stage the StringBuilder will contain the generated XML.

            Dim ContentType As String = ""
            Dim Status As Integer = 0
            Dim iNew As Long = 0
            Try
                sTmp = api.DoOSMRequest(api.BaseURL & "/changeset/create", ContentType, Status, sPayload, "PUT")
            Catch e As Exception
                MsgBox(e.Message)
                Return 0
            End Try
            If Left(ContentType, 10) <> "text/plain" Then Return 0
            If Not Long.TryParse(sTmp, iNew) Then Return 0
            _ChangesetID = iNew

            'Dim l As List(Of String) = api.GetPermissions()

            Return ChangesetID
        End Function

        Public Function Close() As Boolean
            Dim ContentType As String = ""
            Dim Status As Integer = 0
            If ChangesetID <> 0 Then
                api.DoOSMRequest(api.BaseURL & $"/changeset/{ChangesetID}/close", ContentType, Status, " ", "PUT")
                _ChangesetID = 0
            End If
        End Function

        Public Function Create(O As OSMObject) As Boolean
            If ChangesetID = 0 Then Return False
            Dim sPayload As String, sTmp As String
            Dim sb As New StringBuilder()
            Dim sw As New StringWriter(sb)
            ' write the XML to the writer here
            Dim writer As XmlTextWriter = New XmlTextWriter(sw) With {
            .Formatting = Formatting.Indented,
            .Indentation = 4
        }
            writer.WriteStartElement("osm")
            O.Serialize(writer)
            writer.WriteEndElement()
            sPayload = sb.ToString
            Dim ContentType As String = "text/xml; charset=utf-8"
            Dim Status As Integer = 0
            Dim iNew As Long = 0
            Try
                sTmp = api.DoOSMRequest(api.BaseURL & $"/relation/create", ContentType, Status, sPayload, "PUT")
            Catch e As Exception
                MsgBox(e.Message)
                Return False
            End Try
            If Status <> 200 Then Return 0
            If Left(ContentType, 10) <> "text/plain" Then Return 0
            If Not Long.TryParse(sTmp, iNew) Then Return 0
            _LastCreatedID = iNew
            Return True
        End Function

        Public Function Update(o As OSMObject) As Boolean
            If ChangesetID = 0 Then Return False
            Dim sPayload As String, sTmp As String
            Dim sb As New StringBuilder()
            Dim sw As New StringWriter(sb)
            ' write the XML to the writer here
            Dim writer As XmlTextWriter = New XmlTextWriter(sw) With {
            .Formatting = Formatting.Indented,
            .Indentation = 4
        }
            writer.WriteStartElement("osm")
            o.Serialize(writer)
            writer.WriteEndElement()
            sPayload = sb.ToString
            Dim ContentType As String = ""
            Dim Status As Integer = 0
            Dim iNew As Long = 0
            Try
                sTmp = api.DoOSMRequest(api.BaseURL & $"/relation/{o.ID}", ContentType, Status, sPayload, "PUT")
            Catch e As Exception
                MsgBox(e.Message)
                Return False
            End Try
            Dim iNewVer As Long
            If Long.TryParse(sTmp, iNewVer) Then
                o.Version = iNewVer
            Else
                MsgBox($"No new version in reply: {sTmp}")
                Return False
            End If
            Return True
        End Function

        Public Function Delete(o As OSMObject) As Boolean
            If ChangesetID = 0 Then Return False
            Return True
        End Function
#If False Then
    PUT /api/0.6/changeset/create
*body is osm/changeset
*returns changeset ID as plain text

PUT /api/0.6/changeset/#id
to update the changeset tags

PUT /api/0.6/changeset/#id/close

GET /api/0.6/changeset/#id/download
*returns osmChange doc for this changeset

PUT /api/0.6/[node|way|relation]/create
create a new one

PUT /api/0.6/[node|way|relation]/#id
update existing thing
must include full data
version number must match latest in database
*returns new version number

DELETE /api/0.6/[node|way|relation]/#id
delete existing thing
*must include xml data
ID and version must match
lat/lon must be present
#End If
        ''' <summary>
        ''' Upload an osmChange file
        ''' </summary>
        ''' <param name="sXML">The osmChange data</param>
        ''' <returns></returns>
        Public Function UploadOSC(sXML As String) As Boolean
            If ChangesetID = 0 Then Return False
            Dim xDoc As New XmlDocument
            xDoc.LoadXml(sXML)
            Return UploadOSC(xDoc)
        End Function

        Public Function UploadOSC(XML As XmlDocument) As Boolean
            If ChangesetID = 0 Then Return False
            If XML.DocumentElement.Name <> "osmChange" Then
                Return False
            End If
            For Each x As XmlElement In XML.SelectNodes("relation|way|node")
                Dim cs As XmlNode = x.GetAttributeNode("changeset")
                If cs IsNot Nothing Then
                    cs.InnerText = ChangesetID.ToString
                End If
            Next
            Dim sPayload As String = XML.InnerText
            Dim ContentType As String = ""
            Dim Status As Integer = 0
            Dim sTmp As String
            Try
                sTmp = api.DoOSMRequest(api.BaseURL & $"/changeset/{ChangesetID}/upload", ContentType, Status, sPayload, "POST")
            Catch e As Exception
                MsgBox(e.Message)
                Return False
            End Try
            Return True
        End Function
    End Class

