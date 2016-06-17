<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmReview
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lvTagList = New System.Windows.Forms.ListView()
        Me.colTag = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colOSM = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colSource = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colOSMNew = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colSourceNew = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnClose = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lvTagList
        '
        Me.lvTagList.CheckBoxes = True
        Me.lvTagList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colTag, Me.colOSM, Me.colSource, Me.colOSMNew, Me.colSourceNew})
        Me.lvTagList.FullRowSelect = True
        Me.lvTagList.Location = New System.Drawing.Point(6, 39)
        Me.lvTagList.Name = "lvTagList"
        Me.lvTagList.Size = New System.Drawing.Size(611, 298)
        Me.lvTagList.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvTagList.TabIndex = 0
        Me.lvTagList.UseCompatibleStateImageBehavior = False
        Me.lvTagList.View = System.Windows.Forms.View.Details
        '
        'colTag
        '
        Me.colTag.Text = "Tag"
        Me.colTag.Width = 120
        '
        'colOSM
        '
        Me.colOSM.Text = "OSM"
        Me.colOSM.Width = 120
        '
        'colSource
        '
        Me.colSource.Text = "Source"
        Me.colSource.Width = 120
        '
        'colOSMNew
        '
        Me.colOSMNew.Text = "New OSM"
        Me.colOSMNew.Width = 120
        '
        'colSourceNew
        '
        Me.colSourceNew.Text = "Source New"
        Me.colSourceNew.Width = 120
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(726, 321)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(64, 25)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmReview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(802, 349)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.lvTagList)
        Me.Name = "frmReview"
        Me.Text = "OSM Reviewer"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lvTagList As ListView
    Friend WithEvents colTag As ColumnHeader
    Friend WithEvents colOSM As ColumnHeader
    Friend WithEvents colSource As ColumnHeader
    Friend WithEvents colOSMNew As ColumnHeader
    Friend WithEvents btnClose As Button
    Friend WithEvents colSourceNew As ColumnHeader
End Class
