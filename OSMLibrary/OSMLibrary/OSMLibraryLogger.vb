Imports System.Diagnostics

Public Class OSMLibraryLogger
        ''' <summary>
        ''' Class constructor
        ''' </summary>
        Shared Sub New()
            With My.Application.Log.DefaultFileLogWriter
                .BaseFileName = "OSMAPILog"
                .LogFileCreationSchedule = Logging.LogFileCreationScheduleOption.Daily
                .CustomLocation = My.Settings.LogDirectory
                If Len(.CustomLocation) = 0 Then
                    .Location = Logging.LogFileLocation.ExecutableDirectory
                Else
                    .Location = Logging.LogFileLocation.Custom
                End If
                .AutoFlush = True
                Debug.Print($"Logging to { .FullLogFileName}")
            End With
        End Sub

        Public Shared Sub WriteEntry(Message As String,
                               Optional Severity As TraceEventType = TraceEventType.Information,
                               Optional ID As Integer = 0)
            My.Application.Log.WriteEntry(GetTimestamp() & " " & Message, Severity, ID)
        End Sub

        Public Shared Sub WriteException(e As Exception,
                                     Optional Severity As TraceEventType = TraceEventType.Error,
                                     Optional Extra As String = Nothing,
                                     Optional ID As Integer = 0)
            My.Application.Log.WriteException(e, Severity, Extra, ID)
        End Sub

        Private Shared Function GetTimestamp() As String
            Return Now().ToString("yyyy-MM-dd hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
        End Function
    End Class
    