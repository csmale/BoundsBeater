﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
        Me.btnClose = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnCommit = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lvTagList
        '
        Me.lvTagList.CheckBoxes = True
        Me.lvTagList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colTag, Me.colOSM, Me.colSource, Me.colOSMNew})
        Me.lvTagList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvTagList.FullRowSelect = True
        Me.lvTagList.Location = New System.Drawing.Point(3, 53)
        Me.lvTagList.Name = "lvTagList"
        Me.lvTagList.Size = New System.Drawing.Size(796, 200)
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
        Me.TableLayoutPanel1.Controls.Add(Me.lvTagList, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.FlowLayoutPanel1, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(802, 349)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.btnCommit)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnClose)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(3, 259)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(796, 87)
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
        'frmReview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(802, 349)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "frmReview"
        Me.Text = "OSM Reviewer"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lvTagList As ListView
    Friend WithEvents colTag As ColumnHeader
    Friend WithEvents colOSM As ColumnHeader
    Friend WithEvents colSource As ColumnHeader
    Friend WithEvents colOSMNew As ColumnHeader
    Friend WithEvents btnClose As Button
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents btnCommit As Button
End Class
