Imports System.Windows.Forms

Public Class frmChoosePlace
    Public SelectedPlace As System.Xml.XmlNode
    Public xDoc As System.Xml.XmlDocument

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim sID As String
        If lvPlaces.SelectedItems.Count < 1 Then Exit Sub
        sID = lvPlaces.SelectedItems(0).Text
        SelectedPlace = xDoc.SelectSingleNode($"/searchresults/place[@place_id='{sID}']")
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmChoosePlace_Load(sender As Object, e As EventArgs) Handles Me.Load
        If xDoc Is Nothing Then Exit Sub
        lvPlaces.Items.Clear()
        For Each xPlace As System.Xml.XmlNode In xDoc.SelectNodes("/searchresults/place")
            With lvPlaces.Items.Add(xPlace.SelectSingleNode("@place_id").InnerText)
                .SubItems.Add(xPlace.SelectSingleNode("@type").InnerText)
                .SubItems.Add(xPlace.SelectSingleNode("@display_name").InnerText)
            End With
        Next
    End Sub
End Class
