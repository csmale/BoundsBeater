<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLibPrefs
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtXapiBaseUrl = New System.Windows.Forms.TextBox()
        Me.txtApiTimeout = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtBrowseBaseUrl = New System.Windows.Forms.TextBox()
        Me.txtOsmApiBaseUrl = New System.Windows.Forms.TextBox()
        Me.txtUserAgent = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtOsmUser = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtOsmPassword = New System.Windows.Forms.TextBox()
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(277, 311)
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
        Me.Label1.Size = New System.Drawing.Size(115, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "OSM API Base URL"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(138, 15)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "OSM Library User Agent"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 100)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 15)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "XAPI Base URL"
        '
        'txtXapiBaseUrl
        '
        Me.txtXapiBaseUrl.Location = New System.Drawing.Point(12, 117)
        Me.txtXapiBaseUrl.Name = "txtXapiBaseUrl"
        Me.txtXapiBaseUrl.Size = New System.Drawing.Size(399, 20)
        Me.txtXapiBaseUrl.TabIndex = 6
        '
        'txtApiTimeout
        '
        Me.txtApiTimeout.Location = New System.Drawing.Point(12, 160)
        Me.txtApiTimeout.Name = "txtApiTimeout"
        Me.txtApiTimeout.Size = New System.Drawing.Size(100, 20)
        Me.txtApiTimeout.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 144)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(101, 15)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "API Timeout (ms)"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 188)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(138, 15)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "OSM Browse Base URL"
        '
        'txtBrowseBaseUrl
        '
        Me.txtBrowseBaseUrl.Location = New System.Drawing.Point(12, 204)
        Me.txtBrowseBaseUrl.Name = "txtBrowseBaseUrl"
        Me.txtBrowseBaseUrl.Size = New System.Drawing.Size(399, 20)
        Me.txtBrowseBaseUrl.TabIndex = 10
        '
        'txtOsmApiBaseUrl
        '
        Me.txtOsmApiBaseUrl.Location = New System.Drawing.Point(12, 29)
        Me.txtOsmApiBaseUrl.Name = "txtOsmApiBaseUrl"
        Me.txtOsmApiBaseUrl.Size = New System.Drawing.Size(399, 20)
        Me.txtOsmApiBaseUrl.TabIndex = 11
        '
        'txtUserAgent
        '
        Me.txtUserAgent.Location = New System.Drawing.Point(12, 73)
        Me.txtUserAgent.Name = "txtUserAgent"
        Me.txtUserAgent.Size = New System.Drawing.Size(399, 20)
        Me.txtUserAgent.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 231)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(101, 15)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "OSM User Name"
        '
        'txtOsmUser
        '
        Me.txtOsmUser.Location = New System.Drawing.Point(12, 248)
        Me.txtOsmUser.Name = "txtOsmUser"
        Me.txtOsmUser.Size = New System.Drawing.Size(182, 20)
        Me.txtOsmUser.TabIndex = 14
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(239, 230)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(61, 15)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Password"
        '
        'txtOsmPassword
        '
        Me.txtOsmPassword.Location = New System.Drawing.Point(240, 247)
        Me.txtOsmPassword.Name = "txtOsmPassword"
        Me.txtOsmPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtOsmPassword.Size = New System.Drawing.Size(174, 20)
        Me.txtOsmPassword.TabIndex = 16
        '
        'frmLibPrefs
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(435, 352)
        Me.Controls.Add(Me.txtOsmPassword)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtOsmUser)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtUserAgent)
        Me.Controls.Add(Me.txtOsmApiBaseUrl)
        Me.Controls.Add(Me.txtBrowseBaseUrl)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtApiTimeout)
        Me.Controls.Add(Me.txtXapiBaseUrl)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLibPrefs"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "OSM Library Preferences"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents txtXapiBaseUrl As Windows.Forms.TextBox
    Friend WithEvents txtApiTimeout As Windows.Forms.TextBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents txtBrowseBaseUrl As Windows.Forms.TextBox
    Friend WithEvents txtOsmApiBaseUrl As Windows.Forms.TextBox
    Friend WithEvents txtUserAgent As Windows.Forms.TextBox
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents txtOsmUser As Windows.Forms.TextBox
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents txtOsmPassword As Windows.Forms.TextBox
End Class
