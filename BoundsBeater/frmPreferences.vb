Imports System.Windows.Forms

Public Class frmPreferences

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        My.Settings.MaxCacheAge = Integer.Parse(txtMaxCacheAge.Text)
        My.Settings.OSMCache = txtOSMCacheFile.Text
        My.Settings.BoundaryXML = txtUKBoundsFile.Text
        My.Settings.xapiAPI = txtXapiBaseUrl.Text
        My.Settings.Save()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmPreferences_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtMaxCacheAge.Text = My.Settings.MaxCacheAge.ToString()
        txtOSMCacheFile.Text = My.Settings.OSMCache
        txtUKBoundsFile.Text = My.Settings.BoundaryXML
        txtXapiBaseUrl.Text = My.Settings.xapiAPI
    End Sub

    Private Sub btnBrowseOSMCacheFile_Click(sender As Object, e As EventArgs) Handles btnBrowseOSMCacheFile.Click
        With ofd
            .Filter = "OSM Files (*.osm)|*.osm|All Files (*.*)|*.*"
            .FilterIndex = 0
            .CheckFileExists = True
            .CheckPathExists = True
            .RestoreDirectory = True
            .ShowReadOnly = False
            .FileName = System.IO.Path.GetFileName(txtOSMCacheFile.Text)
            .InitialDirectory = Environment.ExpandEnvironmentVariables(System.IO.Path.GetDirectoryName(txtOSMCacheFile.Text))
            If .ShowDialog() = DialogResult.OK Then
                txtOSMCacheFile.Text = .FileName
            End If
        End With
    End Sub

    Private Sub btnBrowseBoundsXML_Click(sender As Object, e As EventArgs) Handles btnBrowseBoundsXML.Click
        With ofd
            .Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*"
            .FilterIndex = 0
            .CheckFileExists = True
            .CheckPathExists = True
            .RestoreDirectory = True
            .ShowReadOnly = False
            .FileName = System.IO.Path.GetFileName(txtUKBoundsFile.Text)
            .InitialDirectory = Environment.ExpandEnvironmentVariables(System.IO.Path.GetDirectoryName(txtUKBoundsFile.Text))
            If .ShowDialog() = DialogResult.OK Then
                txtUKBoundsFile.Text = .FileName
            End If
        End With
    End Sub

    Private Sub btnLibPrefs_Click(sender As Object, e As EventArgs) Handles btnLibPrefs.Click
        With New OSMLibrary.frmLibPrefs
            .ShowDialog()
        End With
    End Sub
End Class
