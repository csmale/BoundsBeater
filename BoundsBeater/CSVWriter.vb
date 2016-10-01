Imports System.IO
Imports System.Text

Public Class CSVWriter
    Public AlwaysQuote As Boolean = True
    Public BackslashEscape As Boolean = False
    Public FieldSeparator As Char = ","c
    Public QuoteChar As Char = """"c

    Private ReadOnly _out As StreamWriter
    Private ReadOnly _sb As New StringBuilder

    Public ReadOnly Property Stream As StreamWriter
        Get
            Return _out
        End Get
    End Property

    Public Sub New(out As StreamWriter)
        _out = out
    End Sub
    Public Sub New(Path As String)
        _out = New StreamWriter(Path)
    End Sub
    Public Sub New(Path As String, Encoding As System.Text.Encoding)
        _out = New StreamWriter(Path, False, Encoding)
    End Sub

    Public Sub WriteLine(Fields() As Object)
        Dim bNotFirst As Boolean = False
        _sb.Clear()
        For Each f In Fields
            If bNotFirst Then _sb.Append(FieldSeparator)
            _sb.Append(DoField(f))
            bNotFirst = True
        Next
        _out.WriteLine(_sb.ToString())
    End Sub

    Public Sub Close()
        _out?.Close()
    End Sub

    Private Function DoField(f As Object) As String
        Dim sTmp As String
        If f Is Nothing Then sTmp = "" Else sTmp = f.ToString
        If BackslashEscape Then
            sTmp = Replace(sTmp, QuoteChar, "\" & QuoteChar)
        Else
            sTmp = Replace(sTmp, QuoteChar, QuoteChar & QuoteChar)
        End If
        If AlwaysQuote OrElse InStr(sTmp, FieldSeparator) > 0 Then sTmp = QuoteChar & sTmp & QuoteChar
        Return sTmp
    End Function
End Class
