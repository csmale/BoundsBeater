Imports System.Net
Imports System.IO
Imports System.Security

Public Class OSMApi
    Public BaseURL As String = My.Settings.OSMBaseApiURL
    Public Credentials As NetworkCredential
    Public UserAgent As String = My.Settings.OSMLibUserAgent
    Public XapiBaseURL As String = My.Settings.OSMXapiBaseApiURL
    Private sLogFile As String = "OSMAPI.log"
    Public LastError As String

    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(URL As String)
        MyBase.New()
        BaseURL = URL
    End Sub
    Public Sub New(URL As String, Credentials As NetworkCredential)
        MyBase.New()
        Me.BaseURL = URL
        Me.Credentials = Credentials
    End Sub
    Public Sub New(Credentials As NetworkCredential)
        MyBase.New()
        Me.Credentials = Credentials
    End Sub
    Public Function GetURL(xType As OSMObject.ObjectType, lRef As Long, bFull As Boolean) As String
        Dim sTmp As String
        sTmp = BaseURL & OSMObject.ObjectTypeString(xType) & "/" & CStr(lRef)
        If bFull And (xType <> OSMObject.ObjectType.Node) Then sTmp &= "/full"
        Return sTmp
    End Function
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

    Private Function DoOSMRequest(sURL As String, ByRef sContentType As String, ByRef iStatusCode As Integer, Optional sPayload As String = "") As String
        Dim req As HttpWebRequest = Nothing
        Dim resp As HttpWebResponse = Nothing
        Dim sResp As String
        Dim iStartTime As Integer
        Dim iEndTime As Integer

        Debug.Print("Retrieving " & sURL)
        Try
            req = WebRequest.Create(sURL)
            req.UserAgent = UserAgent
            req.Credentials = Credentials
            req.Timeout = 30000
            If Len(sPayload) = 0 Then
                req.Method = "GET"
            Else
                req.Method = "PUT"
                req.ContentType = "application/xml"
                req.ContentLength = Len(sPayload)
                With req.GetRequestStream
                    ' .Write(sPayload.ToArray)
                End With

            End If
        Catch e As WebException
            Debug.Print(e.Message)
            If IsNothing(req) Then Return Nothing
        End Try


        iStartTime = Environment.TickCount
        Try
            resp = req.GetResponse
        Catch e As WebException
            Debug.Print(e.Message)

            'if the object has been deleted, we get a 410 Gone, plus a free WebException for our troubles.
            If IsNothing(resp) Then
                Throw New OSMWebException("Unknown error", e)
            Else
                If resp.StatusCode = 410 Then
                    Throw New OSMWebException("Object deleted", e)
                End If
            End If

            Return Nothing
        End Try
        iEndTime = Environment.TickCount
        iStatusCode = resp.StatusCode
        sContentType = resp.ContentType

        Debug.Print("HTTP status " & resp.StatusCode & "(" & resp.StatusDescription & ") in " & (iEndTime - iStartTime) & "ms")
        If resp.StatusCode <> 200 Then
            Debug.Print("HTTP status " & resp.StatusCode & "(" & resp.StatusDescription & ") on " & sURL)
            Throw New OSMWebException("Bad HTTP Status " & resp.StatusCode.ToString())
            Return Nothing
        End If
        If Left(resp.ContentType, 8) <> "text/xml" And resp.ContentType <> "application/osm3s+xml" Then
            Debug.Print("HTTP returned content type " & resp.ContentType & " on " & sURL)
            Throw New OSMWebException("Unexpected content type " & resp.ContentType)
            Return Nothing
        End If
        Dim rs As Stream
        Dim rsr As StreamReader
        rs = resp.GetResponseStream
        rsr = New StreamReader(rs)
        sResp = rsr.ReadToEnd
        rs.Close()
        resp.Close()
        Return sResp
    End Function

    Public Function GetOSM(sUrl As String) As OSMDoc
        Dim req As HttpWebRequest = Nothing
        Dim resp As HttpWebResponse = Nothing
        Dim sResp As String
        Dim iStartTime As Integer
        Dim iEndTime As Integer

        LastError = ""
        Debug.Print("Retrieving " & sUrl)
        Try
            req = WebRequest.Create(sUrl)
            req.Method = "GET"
        Catch e As WebException
            Debug.Print(e.Message)
            LastError = e.Message
            Return Nothing
        End Try

        req.UserAgent = UserAgent
        req.Credentials = Credentials
        req.Timeout = 30000
        req.AutomaticDecompression = DecompressionMethods.Deflate Or DecompressionMethods.GZip
        iStartTime = Environment.TickCount
        Try
            resp = req.GetResponse
        Catch e As WebException
            Debug.Print(e.Message)
            'if the object has been deleted, we get a 410 Gone, plus a free WebException for our troubles.
            If IsNothing(resp) Then
                LastError = e.Message
                Throw New OSMWebException("Unknown error", e)
            Else
                If resp.StatusCode = 404 Then
                    LastError = "Not found"
                    Throw New OSMWebException("Object not found", e)
                ElseIf resp.StatusCode = 410 Then
                    LastError = "Deleted"
                    Throw New OSMWebException("Object deleted", e)
                End If
                Return Nothing
            End If
        End Try
        iEndTime = Environment.TickCount
        Debug.Print("HTTP status " & resp.StatusCode & "(" & resp.StatusDescription & ") in " & (iEndTime - iStartTime) & "ms")
        If resp.StatusCode <> 200 Then
            Debug.Print("HTTP status " & resp.StatusCode & "(" & resp.StatusDescription & ") on " & sUrl)
            LastError = resp.StatusDescription
            Throw New OSMWebException("Bad HTTP Status " & resp.StatusCode.ToString())
            Return Nothing
        End If
        If Left(resp.ContentType, 8) <> "text/xml" And resp.ContentType <> "application/osm3s+xml" Then
            Debug.Print("HTTP returned content type " & resp.ContentType & " on " & sUrl)
            LastError = "Unexpected content type " & resp.ContentType
            Throw New OSMWebException("Unexpected content type " & resp.ContentType)
            Return Nothing
        End If
        Dim rs As Stream
        Dim rsr As StreamReader
        rs = resp.GetResponseStream
        rsr = New StreamReader(rs)
        sResp = rsr.ReadToEnd
        rs.Close()
        resp.Close()
        Dim xDoc As New OSMDoc
        If xDoc.LoadXML(sResp) Then
            Return xDoc
        Else
            LastError = "XML processing error"
            Return Nothing
        End If
    End Function
    Public Function GetOSMDoc(xType As OSMObject.ObjectType, lRef As Long, Optional bFull As Boolean = False) As OSMDoc
        Dim sURL As String
        sURL = GetURL(xType, lRef, bFull)
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
    Public Function CloseChangeset(xchg As OSMUpdateableChangeset)

    End Function
    ' search through XAPI
    ' setting value must end in the question mark, e.g. http://www.overpass-api.de/api/xapi?
    Public Function GetXapi(sXapiParams As String) As OSMDoc
        Dim sUrl As String = XapiBaseURL & sXapiParams
        Return GetOSM(sUrl)
    End Function
End Class
