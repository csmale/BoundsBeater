Imports System.Net
Imports System.IO
Imports System.Security
Imports System.Xml

''' <summary>
''' OSMApi wraps OSM API v0.6
''' </summary>
Public Class OSMApi
        ''' <summary>
        ''' Base URL for the OSM API.
        ''' </summary>
        ''' <returns></returns>
        Public Property BaseURL As String = My.Settings.OSMBaseApiURL
        ''' <summary>
        ''' Credentials for authentication to the OSM API.
        ''' </summary>
        ''' <returns></returns>
        Public Property Credentials As NetworkCredential = New NetworkCredential(My.Settings.APIUser, My.Settings.APIPassword)
        ''' <summary>
        ''' User Agent string for HTTP headers.
        ''' </summary>
        ''' <returns></returns>
        Public Property UserAgent As String = My.Settings.OSMLibUserAgent
        ''' <summary>
        ''' Base URL for the XAPI service.
        ''' </summary>
        ''' <returns></returns>
        Public Property XapiBaseURL As String = My.Settings.OSMXapiBaseApiURL
        Private Shared sLogFile As String = "OSMAPI.log"
        ''' <summary>
        ''' Description of the most recent error from the OSM API.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property LastError As String = ""
        ''' <summary>
        ''' Timeout for the http requests
        ''' </summary>
        ''' <returns>Timeout in milliseconds</returns>
        Public Property Timeout As Integer = My.Settings.OSMApiTimeout
        ''' <summary>
        ''' Container to persist cookies between requests within this instance of OSMApi
        ''' </summary>
        Public ReadOnly CookieContainer As CookieContainer = New CookieContainer
        ''' <summary>
        ''' Credential cache to help with redirects
        ''' </summary>
        Public ReadOnly CredentialCache As New CredentialCache
        ''' <summary>
        ''' Creates an OSMApi object.
        ''' </summary>
        Public Sub New()
            MyBase.New()
        End Sub
        ''' <summary>
        ''' Creates an OSMApi object initialised with the given Base URL.
        ''' </summary>
        ''' <param name="URL"></param>
        Public Sub New(URL As String)
            MyBase.New()
            BaseURL = URL
        End Sub
        ''' <summary>
        ''' Creates an OSMApi object initialised with the given Base URL and credentials.
        ''' </summary>
        ''' <param name="URL"></param>
        ''' <param name="Credentials"></param>
        Public Sub New(URL As String, Credentials As NetworkCredential)
            MyBase.New()
            Me.BaseURL = URL
            Me.Credentials = Credentials
        End Sub
        ''' <summary>
        ''' Creates an OSMApi object initialised with the given credentials.
        ''' </summary>
        ''' <param name="Credentials">Credentials to be used.</param>
        Public Sub New(Credentials As NetworkCredential)
            MyBase.New()
            Me.Credentials = Credentials
        End Sub
        ''' <summary>
        ''' Constructs an API URL to access the given OSM object
        ''' </summary>
        ''' <param name="xType"></param>
        ''' <param name="lRef"></param>
        ''' <param name="bFull"></param>
        ''' <returns></returns>
        Public Function GetUrl(xType As OSMObject.ObjectType, lRef As Long, bFull As Boolean) As String
            Dim sTmp As String
            sTmp = BaseURL & OSMObject.ObjectTypeString(xType) & "/" & CStr(lRef)
            If bFull And (xType <> OSMObject.ObjectType.Node) Then sTmp &= "/full"
            Return sTmp
        End Function
        ''' <summary>
        ''' Constructs an API URL to access multiple OSM objects of the same type in a single call
        ''' </summary>
        ''' <param name="xType"></param>
        ''' <param name="lRef"></param>
        ''' <returns></returns>
        Public Function GetURLMulti(xType As OSMObject.ObjectType, lRef As Long()) As String
            Dim sTmp As String
            Dim bFirst As Boolean = True
            Dim sType As String = OSMObject.ObjectTypeString(xType) & "s"
            sTmp = BaseURL & sType & "?" & sType & "="
            For Each l As Long In lRef
                If Not bFirst Then sTmp = sTmp & ","
                sTmp = sTmp & l.ToString
                bFirst = False
            Next
            Return sTmp
        End Function

        Public Function GetURLHistory(xType As OSMObject.ObjectType, lRef As Long) As String
            Dim sTmp As String
            sTmp = BaseURL & OSMObject.ObjectTypeString(xType) & "/" & CStr(lRef) & "/history"
            Return sTmp
        End Function

        Public Function GetURLVersion(xType As OSMObject.ObjectType, lRef As Long, lVersion As Long) As String
            Dim sTmp As String
            sTmp = BaseURL & OSMObject.ObjectTypeString(xType) & "/" & CStr(lRef) & "/" & CStr(lVersion)
            Return sTmp
        End Function

        Public Function GetPermissions() As List(Of String)
            Dim l As New List(Of String)
            Dim sContentType As String = ""
            Dim iStatusCode As Integer = 0
            Dim sTmp As String
            sTmp = DoOSMRequest(BaseURL & "/permissions", sContentType, iStatusCode)
            Dim xDoc As New XmlDocument()
            xDoc.LoadXml(sTmp)
            For Each x As XmlElement In xDoc.SelectNodes("/osm/permissions/permission")
                l.Add(x.GetAttribute("name"))
            Next
            Return l
        End Function

        Private Function GetNewRequest(sURL As String) As WebRequest
            Dim req As HttpWebRequest = DirectCast(HttpWebRequest.Create(sURL), HttpWebRequest)
            req.CookieContainer = CookieContainer
            req.UserAgent = UserAgent
            req.Credentials = CredentialCache
            req.Timeout = Timeout
            req.AutomaticDecompression = DecompressionMethods.Deflate Or DecompressionMethods.GZip
            req.AllowAutoRedirect = False
            Return req
        End Function
        Private Sub AddAuthHeader(req As WebRequest)
            Dim cre As String
            Dim bytes As Byte()
            Dim base64 As String
            cre = $"{Credentials.UserName}:{Credentials.Password}"
            bytes = System.Text.Encoding.ASCII.GetBytes(cre)
            base64 = Convert.ToBase64String(bytes)
            req.Headers.Add("Authorization”, "Basic " + base64)
        End Sub
        Public Function DoOSMRequest(sURL As String, ByRef sContentType As String, ByRef iStatusCode As Integer, Optional sPayload As String = "", Optional Verb As String = "POST") As String
            Dim req As HttpWebRequest = Nothing
            Dim resp As HttpWebResponse = Nothing
            Dim sResp As String
            Dim iStartTime As Integer
            Dim iEndTime As Integer
            Dim sMethod As String

            Dim Uri As New Uri(sURL)
            Dim AuthUri As New Uri($"http://{Uri.Host}/")
            Dim MaxRedirect As Integer = 3
            Dim nRedirect As Integer = 0
            Dim bFinished As Boolean

            _LastError = ""

            If Len(sPayload) = 0 Then
                sMethod = "GET"
            Else
                sMethod = Verb
            End If
            If Credentials IsNot Nothing Then
                If CredentialCache.GetCredential(AuthUri, "Basic") Is Nothing Then
                    CredentialCache.Add(AuthUri, "Basic", Credentials)
                End If
            End If

            bFinished = False
            req = DirectCast(GetNewRequest(sURL), HttpWebRequest)
            Do While Not bFinished
                Debug.Print($"Accessing {sMethod} {sURL}")
                OSMLibraryLogger.WriteEntry($"Accessing {sMethod} {sURL}", TraceEventType.Information)
                Try
                    If Credentials IsNot Nothing Then
                        AddAuthHeader(req)
                        req.PreAuthenticate = True
                    End If
                    req.Method = sMethod
                    If Len(sPayload) > 0 Then
                        ' req.KeepAlive = False
                        ' req.Pipelined = False
                        Dim b() As Byte
                        b = System.Text.Encoding.UTF8.GetBytes(sPayload)
                        req.ContentLength = b.Length
                        If Len(sContentType) > 0 Then
                            req.ContentType = sContentType
                        Else
                            req.ContentType = "application/xml; charset=utf-8"
                        End If
                        With req.GetRequestStream
                            .Write(b, 0, b.Length)
                            .Close()
                        End With
                    End If
                Catch e As WebException
                    OSMLibraryLogger.WriteException(e, TraceEventType.Error, "sending request")
                    _LastError = e.Message
                    Return Nothing
                End Try

                ' get the response
                ' note that just about anything apart from "200 OK" will throw an exception. OSM actually communicates valueable stuff with
                ' status codes 405-499!
                iStartTime = Environment.TickCount
                Try
                    resp = DirectCast(req.GetResponse, HttpWebResponse)
                Catch e As WebException
                    resp = DirectCast(e.Response, HttpWebResponse)
                    If resp Is Nothing Then
                        OSMLibraryLogger.WriteException(e, TraceEventType.Error, "No response.")
                        Throw New WebException(e.Message, e)
                    End If
                End Try

                iEndTime = Environment.TickCount

                Debug.Print($"HTTP status {CInt(resp.StatusCode)} ({resp.StatusDescription}) in {(iEndTime - iStartTime)}ms from {sURL}")
                OSMLibraryLogger.WriteEntry($"HTTP status {CInt(resp.StatusCode)} ({resp.StatusDescription}) in {(iEndTime - iStartTime)}ms from {sURL}")

                If resp.StatusCode = HttpStatusCode.Moved Or resp.StatusCode = HttpStatusCode.MovedPermanently Then
                    nRedirect = nRedirect + 1
                    If nRedirect >= MaxRedirect Then
                        _LastError = "Too many redirects"
                        Throw New WebException("Too many redirects")
                    End If
                    req = DirectCast(GetNewRequest(resp.GetResponseHeader("Location")), HttpWebRequest)
                    AuthUri = New Uri(resp.GetResponseHeader("Location"))
                    If Credentials IsNot Nothing Then
                        If CredentialCache.GetCredential(AuthUri, "Basic") Is Nothing Then
                            CredentialCache.Add(AuthUri, "Basic", Credentials)
                        End If
                    End If
                Else
                    Exit Do
                End If
            Loop

            iStatusCode = resp.StatusCode
            sContentType = resp.ContentType

            If resp.StatusCode <> HttpStatusCode.OK Then
                _LastError = resp.StatusDescription
                Throw New WebException(resp.StatusDescription)
            End If

            Dim rs As Stream
            Dim rsr As StreamReader
            rs = resp.GetResponseStream
            rsr = New StreamReader(rs)
            sResp = rsr.ReadToEnd
            OSMLibraryLogger.WriteEntry($"Response {Len(sResp)} bytes")
            rs.Close()
            resp.Close()
            resp.Dispose()
            Return sResp
        End Function

        Public Function GetOSM(sUrl As String) As OSMDoc
            Dim sResp As String
            Dim sContentType As String = ""
            Dim iStatusCode As Integer = 0

            Try
                sResp = DoOSMRequest(sUrl, sContentType, iStatusCode)
            Catch e As Exception
                Return Nothing
            End Try

            If iStatusCode <> 200 Then
                Return Nothing
            End If

            Dim xDoc As New OSMDoc
            If xDoc.LoadXML(sResp) Then
                Return xDoc
            Else
                _LastError = "XML processing error"
                Return Nothing
            End If
        End Function
        Public Function GetOSMDoc(xType As OSMObject.ObjectType, lRef As Long, Optional bFull As Boolean = False) As OSMDoc
            Dim sURL As String
            sURL = GetUrl(xType, lRef, bFull)
            Return GetOSM(sURL)
        End Function
        Public Function GetOSMDocVersion(xType As OSMObject.ObjectType, lRef As Long, lVer As Long) As OSMDoc
            Dim sURL As String
            sURL = GetURLVersion(xType, lRef, lVer)
            Return GetOSM(sURL)
        End Function
        Public Function GetOSMDocMulti(xType As OSMObject.ObjectType, alRef As Long()) As OSMDoc
            Dim sURL As String
            sURL = GetURLMulti(xType, alRef)
            Return GetOSM(sURL)
        End Function
        Public Function GetOSMObjectHistory(xType As OSMObject.ObjectType, lRef As Long) As OSMDoc
            Dim sURL As String, xDoc As OSMDoc
            sURL = GetURLHistory(xType, lRef)
            xDoc = GetOSM(sURL)
            Return xDoc
        End Function

        'changeset functions
        Public Function OpenChangeset(xChg As OSMUpdateableChangeset) As Long

        End Function
        Public Sub CloseChangeset(xchg As OSMUpdateableChangeset)

        End Sub
        ' search through XAPI
        ' setting value must end in the question mark, e.g. http://www.overpass-api.de/api/xapi?
        Public Function GetXapi(sXapiParams As String) As OSMDoc
            Dim sUrl As String = XapiBaseURL & sXapiParams
            Return GetOSM(sUrl)
        End Function
    End Class
