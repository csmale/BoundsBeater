Imports System.Windows.Forms

Public Class frmEntityDoc

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmEntityDoc_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim x As String = My.Settings.CHDConnectionString
        Dim z As New OleDb.OleDbConnection(x)
        z.Open()
        '' Dim a As New OleDb.OleDbDataReader(z)
    End Sub
End Class
