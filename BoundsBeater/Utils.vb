Imports System.IO

Module Utils
    ''' <summary>
    ''' Opens the default browser at a particular URL
    ''' </summary>
    ''' <param name="sURL">URL of web page to be shown</param>
    Sub OpenBrowserAt(sURL As String)
        Dim oldCursor As Cursor
        oldCursor = System.Windows.Forms.Cursor.Current
        Try
            System.Windows.Forms.Cursor.Current = Cursors.AppStarting
            Process.Start(sURL)
        Catch e As Exception
        Finally
            System.Windows.Forms.Cursor.Current = oldCursor
        End Try
    End Sub
End Module
