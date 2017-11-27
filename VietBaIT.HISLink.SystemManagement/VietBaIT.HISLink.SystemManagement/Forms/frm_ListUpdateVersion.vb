Imports System.Data.SqlClient
Public Class frm_ListUpdateVersion
    Inherits System.Windows.Forms.Form
    Public Shared mv_DSVersion As New DataSet ' Biến chứa danh sách các File Version

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents ToolBar1 As System.Windows.Forms.ToolBar
    Friend WithEvents grdList As System.Windows.Forms.DataGrid
    Friend WithEvents ToolBarButton1 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton2 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton3 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton4 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton5 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ToolBarButton6 As System.Windows.Forms.ToolBarButton
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel3 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel4 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel5 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel6 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents Sys_VERSION As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents ToolBarButton7 As System.Windows.Forms.ToolBarButton
    Friend WithEvents DataGridTextBoxColumn11 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn12 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn10 As System.Windows.Forms.DataGridTextBoxColumn
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_ListUpdateVersion))
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel3 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel4 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel6 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel5 = New System.Windows.Forms.StatusBarPanel()
        Me.ToolBar1 = New System.Windows.Forms.ToolBar()
        Me.ToolBarButton1 = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton2 = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton3 = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton4 = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton5 = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton7 = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton6 = New System.Windows.Forms.ToolBarButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.grdList = New System.Windows.Forms.DataGrid()
        Me.Sys_VERSION = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn10 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn8 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn9 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn11 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn12 = New System.Windows.Forms.DataGridTextBoxColumn()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 422)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanel2, Me.StatusBarPanel3, Me.StatusBarPanel4, Me.StatusBarPanel6, Me.StatusBarPanel5})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(664, 22)
        Me.StatusBar1.SizingGrip = False
        Me.StatusBar1.TabIndex = 0
        Me.StatusBar1.Text = "StatusBar1"
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Text = "Thêm mới : Ctr+I"
        Me.StatusBarPanel1.Width = 110
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Text = "Cập nhật : Ctr+U"
        Me.StatusBarPanel2.Width = 110
        '
        'StatusBarPanel3
        '
        Me.StatusBarPanel3.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel3.Name = "StatusBarPanel3"
        Me.StatusBarPanel3.Text = "Xóa : Ctr+D"
        Me.StatusBarPanel3.Width = 110
        '
        'StatusBarPanel4
        '
        Me.StatusBarPanel4.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel4.Name = "StatusBarPanel4"
        Me.StatusBarPanel4.Text = "In danh sách : Ctr+P"
        Me.StatusBarPanel4.Width = 110
        '
        'StatusBarPanel6
        '
        Me.StatusBarPanel6.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel6.Name = "StatusBarPanel6"
        Me.StatusBarPanel6.Text = "In chi tiết : Alt+P"
        Me.StatusBarPanel6.Width = 110
        '
        'StatusBarPanel5
        '
        Me.StatusBarPanel5.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel5.Name = "StatusBarPanel5"
        Me.StatusBarPanel5.Text = "Thoát : Esc"
        Me.StatusBarPanel5.Width = 110
        '
        'ToolBar1
        '
        Me.ToolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.ToolBarButton1, Me.ToolBarButton2, Me.ToolBarButton3, Me.ToolBarButton4, Me.ToolBarButton5, Me.ToolBarButton7, Me.ToolBarButton6})
        Me.ToolBar1.DropDownArrows = True
        Me.ToolBar1.ImageList = Me.ImageList1
        Me.ToolBar1.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.ShowToolTips = True
        Me.ToolBar1.Size = New System.Drawing.Size(664, 28)
        Me.ToolBar1.TabIndex = 1
        '
        'ToolBarButton1
        '
        Me.ToolBarButton1.ImageIndex = 0
        Me.ToolBarButton1.Name = "ToolBarButton1"
        Me.ToolBarButton1.Tag = "0"
        Me.ToolBarButton1.ToolTipText = "Thêm mới (Ctr+I)"
        '
        'ToolBarButton2
        '
        Me.ToolBarButton2.ImageIndex = 1
        Me.ToolBarButton2.Name = "ToolBarButton2"
        Me.ToolBarButton2.Tag = "1"
        Me.ToolBarButton2.ToolTipText = "Cập nhật (Ctr+U)"
        '
        'ToolBarButton3
        '
        Me.ToolBarButton3.ImageIndex = 2
        Me.ToolBarButton3.Name = "ToolBarButton3"
        Me.ToolBarButton3.Tag = "2"
        Me.ToolBarButton3.ToolTipText = "Xóa (Ctr+D)"
        '
        'ToolBarButton4
        '
        Me.ToolBarButton4.ImageIndex = 3
        Me.ToolBarButton4.Name = "ToolBarButton4"
        Me.ToolBarButton4.Tag = "3"
        Me.ToolBarButton4.ToolTipText = "In danh sách (Ctr+P)"
        '
        'ToolBarButton5
        '
        Me.ToolBarButton5.ImageIndex = 4
        Me.ToolBarButton5.Name = "ToolBarButton5"
        Me.ToolBarButton5.Tag = "4"
        Me.ToolBarButton5.ToolTipText = "In chi tiết lịch sử từng phiên bản (Alt+P)"
        '
        'ToolBarButton7
        '
        Me.ToolBarButton7.ImageIndex = 6
        Me.ToolBarButton7.Name = "ToolBarButton7"
        Me.ToolBarButton7.Tag = "6"
        '
        'ToolBarButton6
        '
        Me.ToolBarButton6.ImageIndex = 5
        Me.ToolBarButton6.Name = "ToolBarButton6"
        Me.ToolBarButton6.Tag = "5"
        Me.ToolBarButton6.ToolTipText = "Thoát (Escape)"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "")
        Me.ImageList1.Images.SetKeyName(1, "")
        Me.ImageList1.Images.SetKeyName(2, "")
        Me.ImageList1.Images.SetKeyName(3, "")
        Me.ImageList1.Images.SetKeyName(4, "")
        Me.ImageList1.Images.SetKeyName(5, "")
        Me.ImageList1.Images.SetKeyName(6, "OK.PNG")
        '
        'grdList
        '
        Me.grdList.BackgroundColor = System.Drawing.Color.White
        Me.grdList.CaptionVisible = False
        Me.grdList.DataMember = ""
        Me.grdList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdList.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdList.Location = New System.Drawing.Point(0, 28)
        Me.grdList.Name = "grdList"
        Me.grdList.RowHeaderWidth = 5
        Me.grdList.Size = New System.Drawing.Size(664, 394)
        Me.grdList.TabIndex = 2
        Me.grdList.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.Sys_VERSION})
        '
        'Sys_VERSION
        '
        Me.Sys_VERSION.DataGrid = Me.grdList
        Me.Sys_VERSION.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn10, Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn7, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn6, Me.DataGridTextBoxColumn8, Me.DataGridTextBoxColumn9, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn11, Me.DataGridTextBoxColumn12})
        Me.Sys_VERSION.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.Sys_VERSION.RowHeaderWidth = 5
        Me.Sys_VERSION.SelectionBackColor = System.Drawing.Color.MediumSlateBlue
        '
        'DataGridTextBoxColumn10
        '
        Me.DataGridTextBoxColumn10.Format = ""
        Me.DataGridTextBoxColumn10.FormatInfo = Nothing
        Me.DataGridTextBoxColumn10.NullText = ""
        Me.DataGridTextBoxColumn10.Width = 0
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.MappingName = "PK_intID"
        Me.DataGridTextBoxColumn1.Width = 0
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "Tên File"
        Me.DataGridTextBoxColumn2.MappingName = "sFileName"
        Me.DataGridTextBoxColumn2.NullText = ""
        Me.DataGridTextBoxColumn2.Width = 110
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = ""
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "Tên File nén"
        Me.DataGridTextBoxColumn7.MappingName = "sRarFileName"
        Me.DataGridTextBoxColumn7.NullText = ""
        Me.DataGridTextBoxColumn7.Width = 110
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "Số phiên bản"
        Me.DataGridTextBoxColumn3.MappingName = "sVersion"
        Me.DataGridTextBoxColumn3.NullText = ""
        Me.DataGridTextBoxColumn3.Width = 110
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "Dung lượng(Byte)"
        Me.DataGridTextBoxColumn6.MappingName = "dblCapacity"
        Me.DataGridTextBoxColumn6.NullText = ""
        Me.DataGridTextBoxColumn6.Width = 110
        '
        'DataGridTextBoxColumn8
        '
        Me.DataGridTextBoxColumn8.Format = ""
        Me.DataGridTextBoxColumn8.FormatInfo = Nothing
        Me.DataGridTextBoxColumn8.MappingName = "intRar"
        Me.DataGridTextBoxColumn8.NullText = ""
        Me.DataGridTextBoxColumn8.Width = 0
        '
        'DataGridTextBoxColumn9
        '
        Me.DataGridTextBoxColumn9.Format = ""
        Me.DataGridTextBoxColumn9.FormatInfo = Nothing
        Me.DataGridTextBoxColumn9.MappingName = "intPatch"
        Me.DataGridTextBoxColumn9.NullText = ""
        Me.DataGridTextBoxColumn9.Width = 0
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "Ngày cập nhật"
        Me.DataGridTextBoxColumn4.MappingName = "tUpdatedDate"
        Me.DataGridTextBoxColumn4.NullText = ""
        Me.DataGridTextBoxColumn4.Width = 101
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "Mô tả thêm"
        Me.DataGridTextBoxColumn5.MappingName = "sDesc"
        Me.DataGridTextBoxColumn5.NullText = ""
        Me.DataGridTextBoxColumn5.Width = 250
        '
        'DataGridTextBoxColumn11
        '
        Me.DataGridTextBoxColumn11.Format = ""
        Me.DataGridTextBoxColumn11.FormatInfo = Nothing
        Me.DataGridTextBoxColumn11.HeaderText = "Cập nhật"
        Me.DataGridTextBoxColumn11.MappingName = "bytConfirmed"
        Me.DataGridTextBoxColumn11.ReadOnly = True
        Me.DataGridTextBoxColumn11.Width = 75
        '
        'DataGridTextBoxColumn12
        '
        Me.DataGridTextBoxColumn12.Format = ""
        Me.DataGridTextBoxColumn12.FormatInfo = Nothing
        Me.DataGridTextBoxColumn12.HeaderText = "Folder"
        Me.DataGridTextBoxColumn12.MappingName = "sFolder"
        Me.DataGridTextBoxColumn12.Width = 75
        '
        'frm_ListUpdateVersion
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(664, 444)
        Me.Controls.Add(Me.grdList)
        Me.Controls.Add(Me.ToolBar1)
        Me.Controls.Add(Me.StatusBar1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frm_ListUpdateVersion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Danh sách các File cần cập nhật"
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub frm_ListUpdateVersion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mv_DSVersion = LoadData()
        With grdList
            .TableStyles(0).MappingName = "Sys_VERSION"
            .DataSource = mv_DSVersion.Tables(0).DefaultView
        End With
        mv_DSVersion.Tables(0).DefaultView.AllowNew = False
        mv_DSVersion.Tables(0).DefaultView.AllowDelete = False
    End Sub
    Private Function LoadData() As DataSet
        Dim fv_DS As New DataSet
        Dim fv_DA As SqlDataAdapter
        fv_DA = New SqlDataAdapter("SELECT * from Sys_VERSION ORDER BY PK_intID DESC", VNS.Libs.globalVariables.SqlConn)
        Try
            fv_DA.Fill(fv_DS, "Sys_VERSION")
            If fv_DS Is Nothing Then
                Return New DataSet
            Else
                Return fv_DS
            End If
        Catch ex As Exception
            Return New DataSet
        End Try
    End Function

    Private Sub grdList_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdList.CurrentCellChanged
        Try
            grdList.Select(grdList.CurrentRowIndex)
            grdList.CurrentCell = New DataGridCell(grdList.CurrentRowIndex, 0)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar1.ButtonClick
        Select Case e.Button.Tag
            Case 0 'Insert
                InsertVerion()
                Return
            Case 1 'Update
                UpdateVerion()
                Return
            Case 2 'Delete
                DeleteVersion()
            Case 3 'Print
            Case 4 'Print Detail
            Case 5 'Escape
                Me.Close()
            Case 6 'Confirm or not
                ConfirmVersion()
        End Select
    End Sub
    Private Sub DeleteVersion()
        Dim VerID As Integer
        Dim sFileName As String
        Try
            If grdList.VisibleRowCount > 0 Then
                VerID = CInt(grdList.Item(grdList.CurrentRowIndex, 1))
                sFileName = s_IsNothingOrDBNull(grdList.Item(grdList.CurrentRowIndex, 2))
                If sFileName.Trim <> String.Empty Then
                    If MessageBox.Show("Bạn có thực sự muốn xóa phiên bản này không?", gv_sAnnouncement, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        If bDeleteVersion(VerID) Then
                            'MessageBox.Show("Đã xóa phiên bản " & sFileName & " thành công!", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            For Each dr As DataRow In mv_DSVersion.Tables(0).Rows
                                If dr("PK_intID") = VerID Then
                                    mv_DSVersion.Tables(0).Rows.Remove(dr)
                                    Exit For
                                End If
                            Next
                            mv_DSVersion.Tables(0).AcceptChanges()
                            Return
                        End If
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ConfirmVersion()
        Dim VerID As Integer
        Dim sFileName As String
        Try
            If grdList.VisibleRowCount > 0 Then
                VerID = CInt(grdList.Item(grdList.CurrentRowIndex, 1))
                sFileName = s_IsNothingOrDBNull(grdList.Item(grdList.CurrentRowIndex, 2))
                Dim newval As String = sDBnull(grdList.Item(grdList.CurrentRowIndex, 10), "0")
                Dim newbyte As Byte = 0
                If newval = "0" Then
                    newbyte = 1
                Else
                    newbyte = 0

                End If
                If sFileName.Trim <> String.Empty Then
                    If bupdateVersion(VerID, newbyte) Then
                        'MessageBox.Show("Đã xóa phiên bản " & sFileName & " thành công!", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        For Each dr As DataRow In mv_DSVersion.Tables(0).Rows
                            If dr("PK_intID") = VerID Then
                                dr("bytConfirmed") = newbyte
                                Exit For
                            End If
                        Next
                        mv_DSVersion.Tables(0).AcceptChanges()
                        Return
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub InsertVerion()
        Dim sv_oForm As New frm_UpdateVersion
        sv_oForm.mv_intStatus = 1
        sv_oForm.ShowDialog()
    End Sub
    Private Sub UpdateVerion()
        Dim sv_oForm As New frm_UpdateVersion
        Try
            If grdList.VisibleRowCount > 0 Then
                sv_oForm.mv_intStatus = 0
                sv_oForm.mv_intID = CInt(grdList.Item(grdList.CurrentRowIndex, 1))
                sv_oForm.mv_intPatch = grdList.Item(grdList.CurrentRowIndex, 7)
                sv_oForm.mv_intRar = grdList.Item(grdList.CurrentRowIndex, 6)
                sv_oForm.mv_sDesc = grdList.Item(grdList.CurrentRowIndex, 9)
                sv_oForm.mv_dblCapacity = grdList.Item(grdList.CurrentRowIndex, 5)
                sv_oForm.mv_sFileName = grdList.Item(grdList.CurrentRowIndex, 2)
                sv_oForm.mv_sRarFileName = grdList.Item(grdList.CurrentRowIndex, 3)
                sv_oForm.mv_sFileVersion = grdList.Item(grdList.CurrentRowIndex, 4)
                sv_oForm.mv_sFolderName = grdList.Item(grdList.CurrentRowIndex, 11)
                sv_oForm.ShowDialog()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Sub InsertVersion(ByVal pv_sFileName As String, ByVal pv_sRarFileName As String, _
                                    ByVal pv_sVersion As String, ByVal pv_intRar As Integer, _
                                    ByVal pv_intPatch As Integer, ByVal pv_dblCapacity As Integer, _
                                    ByVal pv_sDesc As String)
        '-------------------------------------------
        Dim dr As DataRow
        dr = mv_DSVersion.Tables(0).NewRow
        With dr
            .Item("sFileName") = pv_sFileName ' 
            .Item("sRarFileName") = pv_sRarFileName '
            .Item("sVersion") = pv_sVersion '
            .Item("intRar") = pv_intRar '
            .Item("intPatch") = pv_intPatch '
            .Item("dblCapacity") = pv_dblCapacity '
            .Item("sDesc") = pv_sDesc '
        End With
        mv_DSVersion.Tables(0).Rows.Add(dr)
        mv_DSVersion.Tables(0).AcceptChanges()
        '-------------------------------------------
    End Sub
    Public Function bDeleteVersion(ByVal _ID As Integer) As Boolean
        Dim sv_oCmd As New SqlCommand
        Dim sv_DA As SqlDataAdapter
        Dim sv_sSql As String
        sv_sSql = "DELETE from Sys_VERSION WHERE  PK_intID=" & _ID
        Try
            With sv_oCmd
                .Connection = VNS.Libs.globalVariables.SqlConn
                .CommandType = CommandType.Text
                .CommandText = sv_sSql
                .ExecuteNonQuery()
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function bupdateVersion(ByVal _ID As Integer, ByVal bytConfirmed As Byte) As Boolean
        Dim sv_oCmd As New SqlCommand
        Dim sv_DA As SqlDataAdapter
        Dim sv_sSql As String
        sv_sSql = "update  Sys_VERSION set bytConfirmed=" & bytConfirmed & " WHERE  PK_intID=" & _ID
        Try
            With sv_oCmd
                .Connection = VNS.Libs.globalVariables.SqlConn
                .CommandType = CommandType.Text
                .CommandText = sv_sSql
                .ExecuteNonQuery()
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub frm_ListUpdateVersion_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F8
                DeleteVersion()
            Case Keys.F4
                InsertVerion()
            Case Keys.F6
                UpdateVerion()
            Case Keys.I
                If e.Modifiers = Keys.Control Then
                    InsertVerion()
                End If
            Case Keys.U
                If e.Modifiers = Keys.Control Then
                    UpdateVerion()
                End If
            Case Keys.D
                If e.Modifiers = Keys.Control Then
                    DeleteVersion()
                End If
            Case Keys.Escape
                Me.Close()
        End Select
    End Sub
  
End Class
