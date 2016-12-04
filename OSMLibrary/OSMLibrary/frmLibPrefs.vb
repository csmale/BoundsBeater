Imports System.Windows.Forms
Imports System.Net
Imports System.IO
Imports System.ComponentModel

Public Class frmLibPrefs
    Private bIgnoreKeyPress As Boolean = False

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        With My.Settings
            .OSMApiTimeout = Long.Parse(txtApiTimeout.Text)
            .OSMBrowseBaseURL = txtBrowseBaseUrl.Text
            .OSMBaseApiURL = txtOsmApiBaseUrl.Text
            .OSMLibUserAgent = txtUserAgent.Text
            .OSMXapiBaseApiURL = txtXapiBaseUrl.Text
        End With
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmLibPrefs_Load(sender As Object, e As EventArgs) Handles Me.Load
        With My.Settings
            txtApiTimeout.Text = .OSMApiTimeout.ToString
            txtBrowseBaseUrl.Text = .OSMBrowseBaseURL
            txtOsmApiBaseUrl.Text = .OSMBaseApiURL
            txtUserAgent.Text = .OSMLibUserAgent
            txtXapiBaseUrl.Text = .OSMXapiBaseApiURL
        End With
    End Sub

    Private Sub txtApiTimeout_LostFocus(sender As Object, e As EventArgs) Handles txtApiTimeout.LostFocus
        Dim iTmp As Long
        If Not Long.TryParse(txtApiTimeout.Text, iTmp) OrElse iTmp < 1 Then
            MsgBox("API timeout must be a positive integer", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
            txtApiTimeout.Focus()
        End If
    End Sub

    Private Sub txtBrowseBaseUrl_LostFocus(sender As Object, e As EventArgs) Handles txtBrowseBaseUrl.LostFocus
        If Not IsValidURL(txtBrowseBaseUrl.Text) Then
            MsgBox("Invalid URL")
            txtBrowseBaseUrl.Focus()
        End If
    End Sub

    Private Function IsValidURL(sUrl As String) As Boolean
        Dim h As HttpWebRequest
        If Not (sUrl.StartsWith("http://") OrElse sUrl.StartsWith("https://")) Then
            Return False
        End If
        Try
            h = WebRequest.CreateHttp(sUrl)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    ' User Agent Validation
    ' =====================
    ' User-Agent      = "User-Agent" ":" 1*( product | comment )
    ' product         = token ["/" product-version]
    ' product-version = token
    ' comment         = "(" *( ctext | quoted-pair | comment ) ")"
    ' ctext           = <any TEXT excluding "(" And ")">
    ' token           = 1*<any CHAR except CTLs Or separators>
    'from RFC7230:
    '      token      = 1*tchar
    '      tchar      = "!" / "#" / "$" / "%" / "&" / "'" / "*"
    '                   / "+" / "-" / "." / "^" / "_" / "`" / "|" / "~"
    '                   / DIGIT / ALPHA
    '                   ; any VCHAR, except delimiters

    Private Sub txtXapiBaseUrl_LostFocus(sender As Object, e As EventArgs) Handles txtXapiBaseUrl.LostFocus
        If Not IsValidURL(txtXapiBaseUrl.Text) Then
            MsgBox("Invalid URL")
            txtXapiBaseUrl.Focus()
        End If
    End Sub

    Private Sub txtApiTimeout_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtApiTimeout.KeyPress
        If bIgnoreKeyPress OrElse Char.IsDigit(e.KeyChar) OrElse e.KeyChar = vbBack OrElse e.KeyChar = Chr(127) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtApiTimeout_KeyDown(sender As Object, e As KeyEventArgs) Handles txtApiTimeout.KeyDown
        If e.Control AndAlso (e.KeyCode = Keys.V Or e.KeyCode = Keys.C Or e.KeyCode = Keys.X) Then
            bIgnoreKeyPress = True
        End If
    End Sub

    Private Sub txtBrowseBaseUrl_Leave(sender As Object, e As EventArgs) Handles txtBrowseBaseUrl.Leave
        If Not IsValidURL(txtBrowseBaseUrl.Text) Then
            MsgBox("Invalid URL")
            txtBrowseBaseUrl.Focus()
        End If
    End Sub

    Private Sub txtBrowseBaseUrl_Validating(sender As Object, e As CancelEventArgs) Handles txtBrowseBaseUrl.Validating
        If Not IsValidURL(txtBrowseBaseUrl.Text) Then
            MsgBox("Invalid URL")
            txtBrowseBaseUrl.Focus()
            e.Cancel = True
        End If
    End Sub

    Private Sub txtOsmApiBaseUrl_LostFocus(sender As Object, e As EventArgs) Handles txtOsmApiBaseUrl.LostFocus
        If Not IsValidURL(txtOsmApiBaseUrl.Text) Then
            MsgBox("Invalid URL")
            txtOsmApiBaseUrl.Focus()
        End If
    End Sub

    Private Sub txtUserAgent_LostFocus(sender As Object, e As EventArgs) Handles txtUserAgent.LostFocus
        If txtUserAgent.Text = "" Then
            MsgBox("Invalid User Agent string")
        End If
    End Sub
End Class
