<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPreferences
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtOSMCacheFile = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtUKBoundsFile = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtXapiBaseUrl = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtMaxCacheAge = New System.Windows.Forms.TextBox()
        Me.btnBrowseOSMCacheFile = New System.Windows.Forms.Button()
        Me.btnBrowseBoundsXML = New System.Windows.Forms.Button()
        Me.ofd = New System.Windows.Forms.OpenFileDialog()
        Me.btnLibPrefs = New System.Windows.Forms.Button()
        Me.txtNominatimURL = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(277, 247)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "OSM Cache File"
        '
        'txtOSMCacheFile
        '
        Me.txtOSMCacheFile.Location = New System.Drawing.Point(16, 30)
        Me.txtOSMCacheFile.Name = "txtOSMCacheFile"
        Me.txtOSMCacheFile.ReadOnly = True
        Me.txtOSMCacheFile.Size = New System.Drawing.Size(372, 20)
        Me.txtOSMCacheFile.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "UK Boundaries File"
        '
        'txtUKBoundsFile
        '
        Me.txtUKBoundsFile.Location = New System.Drawing.Point(16, 75)
        Me.txtUKBoundsFile.Name = "txtUKBoundsFile"
        Me.txtUKBoundsFile.ReadOnly = True
        Me.txtUKBoundsFile.Size = New System.Drawing.Size(372, 20)
        Me.txtUKBoundsFile.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 107)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(83, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "XAPI Base URL"
        '
        'txtXapiBaseUrl
        '
        Me.txtXapiBaseUrl.Location = New System.Drawing.Point(19, 123)
        Me.txtXapiBaseUrl.Name = "txtXapiBaseUrl"
        Me.txtXapiBaseUrl.Size = New System.Drawing.Size(404, 20)
        Me.txtXapiBaseUrl.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(19, 201)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(107, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Maximum Cache Age"
        '
        'txtMaxCacheAge
        '
        Me.txtMaxCacheAge.Location = New System.Drawing.Point(19, 217)
        Me.txtMaxCacheAge.Name = "txtMaxCacheAge"
        Me.txtMaxCacheAge.Size = New System.Drawing.Size(100, 20)
        Me.txtMaxCacheAge.TabIndex = 8
        '
        'btnBrowseOSMCacheFile
        '
        Me.btnBrowseOSMCacheFile.Location = New System.Drawing.Point(395, 30)
        Me.btnBrowseOSMCacheFile.Name = "btnBrowseOSMCacheFile"
        Me.btnBrowseOSMCacheFile.Size = New System.Drawing.Size(28, 23)
        Me.btnBrowseOSMCacheFile.TabIndex = 9
        Me.btnBrowseOSMCacheFile.Text = "..."
        Me.btnBrowseOSMCacheFile.UseVisualStyleBackColor = True
        '
        'btnBrowseBoundsXML
        '
        Me.btnBrowseBoundsXML.Location = New System.Drawing.Point(395, 71)
        Me.btnBrowseBoundsXML.Name = "btnBrowseBoundsXML"
        Me.btnBrowseBoundsXML.Size = New System.Drawing.Size(28, 23)
        Me.btnBrowseBoundsXML.TabIndex = 10
        Me.btnBrowseBoundsXML.Text = "..."
        Me.btnBrowseBoundsXML.UseVisualStyleBackColor = True
        '
        'ofd
        '
        Me.ofd.FileName = "filename"
        '
        'btnLibPrefs
        '
        Me.btnLibPrefs.Location = New System.Drawing.Point(22, 253)
        Me.btnLibPrefs.Name = "btnLibPrefs"
        Me.btnLibPrefs.Size = New System.Drawing.Size(115, 23)
        Me.btnLibPrefs.TabIndex = 11
        Me.btnLibPrefs.Text = "OSM Library Prefs"
        Me.btnLibPrefs.UseVisualStyleBackColor = True
        '
        'txtNominatimURL
        '
        Me.txtNominatimURL.Location = New System.Drawing.Point(19, 170)
        Me.txtNominatimURL.Name = "txtNominatimURL"
        Me.txtNominatimURL.Size = New System.Drawing.Size(404, 20)
        Me.txtNominatimURL.TabIndex = 12
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 153)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(174, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Nominatim URL for Search function"
        '
        'frmPreferences
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(435, 284)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtNominatimURL)
        Me.Controls.Add(Me.btnLibPrefs)
        Me.Controls.Add(Me.btnBrowseBoundsXML)
        Me.Controls.Add(Me.btnBrowseOSMCacheFile)
        Me.Controls.Add(Me.txtMaxCacheAge)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtXapiBaseUrl)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtUKBoundsFile)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtOSMCacheFile)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPreferences"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "BoundsBeater Preferences"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As Label
    Friend WithEvents txtOSMCacheFile As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtUKBoundsFile As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtXapiBaseUrl As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtMaxCacheAge As TextBox
    Friend WithEvents btnBrowseOSMCacheFile As Button
    Friend WithEvents btnBrowseBoundsXML As Button
    Friend WithEvents ofd As OpenFileDialog
    Friend WithEvents btnLibPrefs As Button
    Friend WithEvents txtNominatimURL As TextBox
    Friend WithEvents Label5 As Label
End Class
