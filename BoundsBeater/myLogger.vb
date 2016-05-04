Imports System.IO
Imports OSMLibrary

Public Class myLogger
    Implements IDisposable

    Private _IssueFileWriter As StreamWriter
    Private _sFile As String
    ''' <summary>
    ''' Creates new issue log in the specified file
    ''' </summary>
    ''' <param name="sFile">Output file name for issue log</param>
    Public Sub New(sFile As String)
        Dim e As System.Text.Encoding = New System.Text.UTF8Encoding(True)
        _IssueFileWriter = New StreamWriter(sFile, False, e)
        _sFile = sFile
    End Sub
    ''' <summary>
    ''' Writes an entry to the issue log
    ''' </summary>
    ''' <param name="xRel">The relation being analysed</param>
    ''' <param name="xMbr">The member of the relation with the issue</param>
    ''' <param name="sMessage">Description of the issue</param>
    Public Sub Write(xRel As OSMRelation, xMbr As OSMObject, sMessage As String)
        Dim sName As String
        Debug.Assert(_IssueFileWriter IsNot Nothing)
        If Not (_IssueFileWriter Is Nothing) Then Exit Sub

        With _IssueFileWriter
            .Write("Relation " & xRel.ID & " (" & xRel.Name("en") & "): ")
            If Not (xMbr Is Nothing) Then
                sName = xMbr.Name("en")
                .Write("Way " & xMbr.ID)
                If Len(sName) > 0 Then
                    .Write(" (" & sName & ")")
                End If
                .Write(": ")
            End If
            .WriteLine(sMessage)
        End With
    End Sub
    ''' <summary>
    ''' Closes the issue log
    ''' </summary>
    Public Sub Close()
        _IssueFileWriter?.Close()
        _IssueFileWriter = Nothing
    End Sub

#Region " IDisposable Support "
    ' Do not change or add Overridable to these methods. 
    ' Put cleanup code in Dispose(ByVal disposing As Boolean). 
    Public Sub Dispose(ByVal disposing As Boolean)
        _IssueFileWriter?.Close()
        _IssueFileWriter = Nothing
    End Sub
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Protected Overrides Sub Finalize()
        Dispose(False)
        MyBase.Finalize()
    End Sub
#End Region
End Class
