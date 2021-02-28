Imports System.IO
Imports System.Xml

Public Class Utilities
    Public Delegate Function PBFProcessor(x As XmlTextReader) As Boolean
    Public Shared Function LoadPBFAsXML(sFile As String, p As PBFProcessor) As Boolean
        ' run external osmconvert command on the given file which streams the xml output to stdout
        Dim myPath As String = New System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).AbsolutePath
        myPath = Uri.UnescapeDataString(myPath)
        myPath = System.IO.Path.GetDirectoryName(myPath)
        Dim oProcess As New Process()
        Dim sProg As String = Path.Combine(myPath, My.Settings.OSMConvert)
        Dim oStartInfo As New ProcessStartInfo(sProg, """" & sFile & """") With {
            .UseShellExecute = False,
            .RedirectStandardOutput = True,
            .CreateNoWindow = True
        }
        oProcess.StartInfo = oStartInfo
        oProcess.Start()

        Dim oStreamReader As System.IO.StreamReader = oProcess.StandardOutput
        Dim xRdr As New XmlTextReader(oStreamReader)

        Dim bRet As Boolean = p(xRdr)

        If Not oProcess.HasExited Then
            oProcess.Kill()
        End If

        If oProcess.ExitCode <> 0 Then bRet = False
        Return bRet
    End Function
End Class
