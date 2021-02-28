Imports System.IO
Imports System.Xml

''' <summary>
''' Scans an OSM XML file and produces a CSV report on the boundary relations found.
''' </summary>
Public Class OSMFileReporter
    Const dQuote As String = """"
    Const FieldSep As String = ","
    Private inDoc As String
    Private outDoc As StreamWriter
    Private StartOfLine As Boolean

    Private Headers As String() = {
        "id", "type", "name", "boundary", "admin_level",
        "designation", "ref:gss", "parish_type", "council_name", "council_style",
        "website", "wikidata", "wikipedia",
        "waycount", "nodecount", "relcount", "subareacount"
        }

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="inFile">Name of the input file</param>
    ''' <param name="outFile">Output file stream</param>
    Sub New(inFile As String, outFile As StreamWriter)
        inDoc = inFile
        outDoc = outFile
    End Sub

    ''' <summary>
    ''' Process the file and produce the report
    ''' </summary>
    Public Function Run() As Boolean
        If LCase(Right(inDoc, 4)) = ".pbf" Then
            Return Utilities.LoadPBFAsXML(inDoc, AddressOf RunXML)
        Else
            Return RunXML(New XmlTextReader(inDoc))
        End If
    End Function

    Private Function RunXML(xrdr As XmlTextReader) As Boolean
        Dim xDoc As XmlDocument
        Dim xEl As XmlElement
        WriteHeader()

        While Not xrdr.EOF
            Select Case xrdr.NodeType
                Case XmlNodeType.Element
                    xDoc = New XmlDocument()
                    Select Case xrdr.Name
                        Case "relation"
                            xEl = DirectCast(xDoc.ReadNode(xrdr), XmlElement)
                            ProcessRelation(xEl)
                        Case Else
                            xrdr.Read()
                    End Select
                Case Else
                    xrdr.Read()
            End Select
        End While
        Return True
    End Function

    ''' <summary>
    ''' Writes a header line to the output file with the column headers
    ''' </summary>
    Private Sub WriteHeader()
        StartOfLine = True
        For Each hdr In Headers
            WriteField(hdr)
        Next
        writeEOL
    End Sub

    ''' <summary>
    ''' Processes a single relation and writes a single line to the output file
    ''' </summary>
    ''' <param name="xEl"></param>
    Private Sub ProcessRelation(xEl As XmlElement)
        Dim x As XmlElement
        Dim s As String

        s = GetTag(xEl, "type")
        If s <> "boundary" And s <> "multipolygon" Then Return
        s = GetTag(xEl, "boundary")
        If s <> "administrative" And s <> "ceremonial" And s <> "historical" And s <> "traditional" Then Return

        StartOfLine = True
        For Each hdr In Headers
            s = ""
            Select Case hdr
                Case "id"
                    s = xEl.GetAttribute("id")
                Case "waycount"
                    s = CountMembers(xEl, "way").ToString
                Case "nodecount"
                    s = CountMembers(xEl, "node").ToString
                Case "relcount"
                    s = CountMembers(xEl, "relation").ToString
                Case "subareacount"
                    s = CountSubareas(xEl).ToString
                Case Else
                    s = GetTag(xEl, hdr)
            End Select
            WriteField(s)
        Next
        WriteEOL()
    End Sub

    Private Function CountMembers(xEl As XmlElement, MemberType As String) As Integer
        Dim i As Integer = 0
        i = xEl.SelectNodes("member[@type='" & MemberType & "']").Count
        Return i
    End Function
    Private Function CountSubareas(xEl As XmlElement) As Integer
        Dim i As Integer = 0
        i = xEl.SelectNodes("member[@type='relation'][@role='subarea']").Count
        Return i
    End Function
    Private Function GetTag(xEl As XmlElement, Tag As String) As String
        Dim x As XmlNode
        x = xEl.SelectSingleNode("tag[@k='" & Tag & "']")
        If x Is Nothing Then Return ""
        Return DirectCast(x, XmlElement).GetAttribute("v")
    End Function

    Private Function NodeText(x As XmlElement) As String
        If x Is Nothing Then Return ""
        Return x.InnerText
    End Function

    Private Sub WriteField(sField As String)
        If Not StartOfLine Then outDoc.Write(FieldSep)
        outDoc.Write(QuotedIfNeeded(sField))
        StartOfLine = False
    End Sub

    Private Function QuotedIfNeeded(s As String) As String
        If (InStr(s, dQuote) > 0) Or (InStr(s, FieldSep) > 0) Then
            Return Quoted(s)
        Else
            Return s
        End If
    End Function

    Private Function Quoted(s As String) As String
        Return dQuote & Replace(s, dQuote, dQuote & dQuote) & dQuote
    End Function

    Private Sub WriteEOL()
        outDoc.WriteLine()
    End Sub
End Class
