Imports System.IO

Public Class FileLogger
    Private _FileName As String
    Public Function Open(sFile As String) As Boolean

    End Function
    Public Sub Close()

    End Sub
    Public Sub Log(Message As String)

    End Sub

    Private Sub Info(ByVal info As Object)
        'Gets folder & file information of the log file   

        Dim fileName As String = _FileName

        'Check for existence of logger file            
        If File.Exists(_FileName) Then
            Try
                Dim fs As FileStream = New FileStream(fileName, FileMode.Append, FileAccess.Write)
                Dim sw As StreamWriter = New StreamWriter(fs)
                sw.WriteLine(vbCrLf & "--- " + vbCrLf + DateTime.Now + " " + info.ToString)
                sw.Close()
                fs.Close()
            Catch dirEx As DirectoryNotFoundException
                LogInfo(dirEx)
            Catch ex As FileNotFoundException
                LogInfo(ex)
            Catch Ex As Exception
                LogInfo(Ex)
            End Try
        Else
            'If file doesn't exist create one               
            Try
                Dim fileStream As FileStream = File.Create(fileName)
                Dim sw As StreamWriter = New StreamWriter(fileStream)
                sw.WriteLine(vbCrLf & "--- " + vbCrLf + DateTime.Now + info.ToString)
                sw.Close()
                fileStream.Close()
            Catch fileEx As FileNotFoundException
                LogInfo(fileEx)
            Catch dirEx As DirectoryNotFoundException
                LogInfo(dirEx)
            Catch ex As Exception
                LogInfo(ex)
            End Try
        End If
    End Sub
    Public Sub LogInfo(ByVal ex As Exception)
        Try
            'Writes error information to the log file including name of the file, line number & error message description               
            Dim trace As Diagnostics.StackTrace = New Diagnostics.StackTrace(ex, True)
            Dim fileNames As String = trace.GetFrame((trace.FrameCount - 1)).GetFileName()
            Dim lineNumber As Int32 = trace.GetFrame((trace.FrameCount - 1)).GetFileLineNumber()
            Info(vbCrLf + "Error In: " + fileNames + vbCrLf + "Line Number:" + lineNumber.ToString() + vbCrLf + "Error Message: " + ex.Message)
        Catch genEx As Exception
            Info(ex.Message)
        End Try
    End Sub
    Public Sub LogInfo(ByVal message As String)
        Try
            'Write general message to the log file       
            Info(message)
        Catch genEx As Exception
            Info(genEx.Message)
        End Try
    End Sub

End Class
