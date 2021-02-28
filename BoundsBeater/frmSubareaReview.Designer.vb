<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSubareaReview
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
        Me.components = New System.ComponentModel.Container()
        Me.lvTagList = New System.Windows.Forms.ListView()
        Me.colType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colOSM = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colSource = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colOSMNew = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnClose = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnCommit = New System.Windows.Forms.Button()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.txtChangesetComment = New System.Windows.Forms.TextBox()
        Me.cmsTagAction = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmsiKeepOsm = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsiTakeSource = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsiDeleteTag = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsiCustom = New System.Windows.Forms.ToolStripMenuItem()
        Me.colSeq = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colComments = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.cmsTagAction.SuspendLayout()
        Me.SuspendLayout()
        '
        'lvTagList
        '
        Me.lvTagList.CheckBoxes = True
        Me.lvTagList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colSeq, Me.colType, Me.colID, Me.colOSM, Me.colSource, Me.colOSMNew, Me.colComments})
        Me.lvTagList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvTagList.FullRowSelect = True
        Me.lvTagList.Location = New System.Drawing.Point(3, 103)
        Me.lvTagList.Name = "lvTagList"
        Me.lvTagList.Size = New System.Drawing.Size(1044, 200)
        Me.lvTagList.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvTagList.TabIndex = 0
        Me.lvTagList.UseCompatibleStateImageBehavior = False
        Me.lvTagList.View = System.Windows.Forms.View.Details
        '
        'colType
        '
        Me.colType.Text = "Type"
        Me.colType.Width = 120
        '
        'colOSM
        '
        Me.colOSM.Text = "OSM"
        Me.colOSM.Width = 200
        '
        'colSource
        '
        Me.colSource.Text = "Source"
        Me.colSource.Width = 200
        '
        'colOSMNew
        '
        Me.colOSMNew.Text = "New OSM"
        Me.colOSMNew.Width = 200
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(105, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(96, 25)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lvTagList, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.FlowLayoutPanel1, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.lblTitle, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtChangesetComment, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1050, 349)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.btnCommit)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnClose)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(3, 309)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(1044, 44)
        Me.FlowLayoutPanel1.TabIndex = 1
        '
        'btnCommit
        '
        Me.btnCommit.Location = New System.Drawing.Point(3, 3)
        Me.btnCommit.Name = "btnCommit"
        Me.btnCommit.Size = New System.Drawing.Size(96, 25)
        Me.btnCommit.TabIndex = 2
        Me.btnCommit.Text = "Commit"
        Me.btnCommit.UseVisualStyleBackColor = True
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Location = New System.Drawing.Point(3, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(60, 13)
        Me.lblTitle.TabIndex = 2
        Me.lblTitle.Text = "Reviewing:"
        '
        'txtChangesetComment
        '
        Me.txtChangesetComment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtChangesetComment.Location = New System.Drawing.Point(3, 53)
        Me.txtChangesetComment.Multiline = True
        Me.txtChangesetComment.Name = "txtChangesetComment"
        Me.txtChangesetComment.Size = New System.Drawing.Size(1044, 44)
        Me.txtChangesetComment.TabIndex = 3
        '
        'cmsTagAction
        '
        Me.cmsTagAction.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.cmsTagAction.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmsiKeepOsm, Me.cmsiTakeSource, Me.cmsiDeleteTag, Me.cmsiCustom})
        Me.cmsTagAction.Name = "cmsTagAction"
        Me.cmsTagAction.Size = New System.Drawing.Size(169, 92)
        '
        'cmsiKeepOsm
        '
        Me.cmsiKeepOsm.Name = "cmsiKeepOsm"
        Me.cmsiKeepOsm.Size = New System.Drawing.Size(168, 22)
        Me.cmsiKeepOsm.Text = "Keep OSM Value"
        '
        'cmsiTakeSource
        '
        Me.cmsiTakeSource.Name = "cmsiTakeSource"
        Me.cmsiTakeSource.Size = New System.Drawing.Size(168, 22)
        Me.cmsiTakeSource.Text = "Take Source Value"
        '
        'cmsiDeleteTag
        '
        Me.cmsiDeleteTag.Name = "cmsiDeleteTag"
        Me.cmsiDeleteTag.Size = New System.Drawing.Size(168, 22)
        Me.cmsiDeleteTag.Text = "Delete Tag"
        '
        'cmsiCustom
        '
        Me.cmsiCustom.Name = "cmsiCustom"
        Me.cmsiCustom.Size = New System.Drawing.Size(168, 22)
        Me.cmsiCustom.Text = "Custom Value"
        '
        'colSeq
        '
        Me.colSeq.Text = "Seq"
        Me.colSeq.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'colID
        '
        Me.colID.Text = "ID"
        '
        'colComments
        '
        Me.colComments.Text = "Comments"
        '
        'frmSubareaReview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1050, 349)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "frmSubareaReview"
        Me.Text = "OSM Reviewer"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.cmsTagAction.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lvTagList As ListView
    Friend WithEvents colType As ColumnHeader
    Friend WithEvents colOSM As ColumnHeader
    Friend WithEvents colSource As ColumnHeader
    Friend WithEvents colOSMNew As ColumnHeader
    Friend WithEvents btnClose As Button
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents btnCommit As Button
    Friend WithEvents lblTitle As Label
    Friend WithEvents txtChangesetComment As TextBox
    Friend WithEvents cmsTagAction As ContextMenuStrip
    Friend WithEvents cmsiKeepOsm As ToolStripMenuItem
    Friend WithEvents cmsiTakeSource As ToolStripMenuItem
    Friend WithEvents cmsiCustom As ToolStripMenuItem
    Friend WithEvents cmsiDeleteTag As ToolStripMenuItem
    Friend WithEvents colSeq As ColumnHeader
    Friend WithEvents colID As ColumnHeader
    Friend WithEvents colComments As ColumnHeader
End Class
