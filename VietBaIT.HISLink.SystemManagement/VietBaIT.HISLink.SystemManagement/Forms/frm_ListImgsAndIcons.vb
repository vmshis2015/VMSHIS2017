Imports System.Data.SqlClient
Imports System.IO
Public Class frm_ListImgsAndIcons
    Inherits System.Windows.Forms.Form
    Public Shared mv_DSImgAndIcon As New DataSet ' Biến chứa danh sách các File Version

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
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ToolBarButton6 As System.Windows.Forms.ToolBarButton
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel5 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents DataGridTextBoxColumn10 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Sys_IMGANDICON As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents ToolBarButton2 As System.Windows.Forms.ToolBarButton
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frm_ListImgsAndIcons))
        Me.StatusBar1 = New System.Windows.Forms.StatusBar
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel
        Me.StatusBarPanel5 = New System.Windows.Forms.StatusBarPanel
        Me.ToolBar1 = New System.Windows.Forms.ToolBar
        Me.ToolBarButton1 = New System.Windows.Forms.ToolBarButton
        Me.ToolBarButton6 = New System.Windows.Forms.ToolBarButton
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.grdList = New System.Windows.Forms.DataGrid
        Me.Sys_IMGANDICON = New System.Windows.Forms.DataGridTableStyle
        Me.DataGridTextBoxColumn10 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.ToolBarButton2 = New System.Windows.Forms.ToolBarButton
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 311)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanel2, Me.StatusBarPanel5})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(514, 22)
        Me.StatusBar1.SizingGrip = False
        Me.StatusBar1.TabIndex = 0
        Me.StatusBar1.Text = "StatusBar1"
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanel1.Text = "Thêm mới : Ctr+I"
        Me.StatusBarPanel1.Width = 98
        '
        'StatusBarPanel5
        '
        Me.StatusBarPanel5.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanel5.Text = "Thoát : Esc"
        Me.StatusBarPanel5.Width = 71
        '
        'ToolBar1
        '
        Me.ToolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.ToolBarButton1, Me.ToolBarButton2, Me.ToolBarButton6})
        Me.ToolBar1.DropDownArrows = True
        Me.ToolBar1.ImageList = Me.ImageList1
        Me.ToolBar1.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.ShowToolTips = True
        Me.ToolBar1.Size = New System.Drawing.Size(514, 28)
        Me.ToolBar1.TabIndex = 1
        '
        'ToolBarButton1
        '
        Me.ToolBarButton1.ImageIndex = 0
        Me.ToolBarButton1.Tag = "0"
        Me.ToolBarButton1.ToolTipText = "Thêm mới (Ctr+I)"
        '
        'ToolBarButton6
        '
        Me.ToolBarButton6.ImageIndex = 6
        Me.ToolBarButton6.Tag = "2"
        Me.ToolBarButton6.ToolTipText = "Thoát (Escape)"
        '
        'ImageList1
        '
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
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
        Me.grdList.Size = New System.Drawing.Size(514, 283)
        Me.grdList.TabIndex = 2
        Me.grdList.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.Sys_IMGANDICON})
        '
        'Sys_IMGANDICON
        '
        Me.Sys_IMGANDICON.DataGrid = Me.grdList
        Me.Sys_IMGANDICON.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn10, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2})
        Me.Sys_IMGANDICON.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.Sys_IMGANDICON.MappingName = ""
        Me.Sys_IMGANDICON.RowHeaderWidth = 5
        Me.Sys_IMGANDICON.SelectionBackColor = System.Drawing.Color.MediumSlateBlue
        '
        'DataGridTextBoxColumn10
        '
        Me.DataGridTextBoxColumn10.Format = ""
        Me.DataGridTextBoxColumn10.FormatInfo = Nothing
        Me.DataGridTextBoxColumn10.MappingName = ""
        Me.DataGridTextBoxColumn10.NullText = ""
        Me.DataGridTextBoxColumn10.Width = 0
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "Tên File"
        Me.DataGridTextBoxColumn1.MappingName = "sFileName"
        Me.DataGridTextBoxColumn1.NullText = ""
        Me.DataGridTextBoxColumn1.Width = 101
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "Đường dẫn đầy đủ"
        Me.DataGridTextBoxColumn2.MappingName = "sFilePath"
        Me.DataGridTextBoxColumn2.NullText = ""
        Me.DataGridTextBoxColumn2.Width = 301
        '
        'ToolBarButton2
        '
        Me.ToolBarButton2.ImageIndex = 2
        Me.ToolBarButton2.Tag = "1"
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanel2.Text = "Xóa: Ctr+D"
        Me.StatusBarPanel2.Width = 70
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.MappingName = "ID"
        Me.DataGridTextBoxColumn3.NullText = ""
        Me.DataGridTextBoxColumn3.Width = 0
        '
        'frm_ListImgsAndIcons
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(514, 333)
        Me.Controls.Add(Me.grdList)
        Me.Controls.Add(Me.ToolBar1)
        Me.Controls.Add(Me.StatusBar1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frm_ListImgsAndIcons"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Danh sách các File ảnh và Icon cần cập nhật"
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frm_ListImgsAndIcons_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mv_DSImgAndIcon = LoadData()
        With grdList
            .TableStyles(0).MappingName = "Sys_IMGANDICON"
            .DataSource = mv_DSImgAndIcon.Tables(0).DefaultView
        End With
        mv_DSImgAndIcon.Tables(0).DefaultView.AllowNew = False
        mv_DSImgAndIcon.Tables(0).DefaultView.AllowDelete = False
    End Sub
    Private Function LoadData() As DataSet
        Dim fv_DS As New DataSet
        Dim fv_DA As SqlDataAdapter
        fv_DA = New SqlDataAdapter("SELECT * from Sys_IMGANDICON ", VNS.Libs.globalVariables.SqlConn)
        Try
            fv_DA.Fill(fv_DS, "Sys_IMGANDICON")
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
            Case 1 'Delete
                DeleteVersion()
            Case 2
                Me.Close()
        End Select
    End Sub
    Private Sub DeleteVersion()
        Dim VerID As Integer
        Dim sFileName As String
        Try
            If grdList.VisibleRowCount > 0 Then
                VerID = CInt(grdList.Item(grdList.CurrentRowIndex, 1))
                sFileName = grdList.Item(grdList.CurrentRowIndex, 2)
                If MessageBox.Show("Bạn có thực sự muốn xóa file này không?", gv_sAnnouncement, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If bDeleteImgAndIcon(VerID) Then
                        MessageBox.Show("Đã xóa file " & sFileName & " thành công!", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        For Each dr As DataRow In mv_DSImgAndIcon.Tables(0).Rows
                            If dr("ID") = VerID Then
                                mv_DSImgAndIcon.Tables(0).Rows.Remove(dr)
                                Exit For
                            End If
                        Next
                        mv_DSImgAndIcon.Tables(0).AcceptChanges()
                        Return
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub InsertVerion()
        Dim s As String = ""
        Dim sv_oForm As New frm_addImgAndIcon
        sv_oForm.ShowDialog()
        If Not sv_oForm.mv_bCancel Then
            For i As Integer = 0 To sv_oForm.lstImgAndIcon.Items.Count - 1
                '---------------------------------------------------------
                Try
                    Dim fs As FileStream
                    Dim intRar As Integer
                    fs = New FileStream(sv_oForm.lstImgAndIcon.Items(i), FileMode.Open, FileAccess.Read)
                    Dim rd As BinaryReader = New BinaryReader(fs)
                    Dim arrData() As Byte = rd.ReadBytes(CInt(fs.Length))
                    fs.Flush()
                    fs.Close()
                    '---------------------------------------------------------
                    Dim ID As Integer
                    ID = InsertData(sv_oForm.lstImgAndIcon.Items(i), arrData)
                    If ID <> -1 Then
                        Dim Dr As DataRow
                        Dr = mv_DSImgAndIcon.Tables(0).NewRow
                        With Dr
                            .Item("ID") = ID
                            .Item("sFilePath") = sv_oForm.lstImgAndIcon.Items(i)
                            .Item("sFileName") = Path.GetFileName(sv_oForm.lstImgAndIcon.Items(i).ToString)
                        End With
                        mv_DSImgAndIcon.Tables(0).Rows.Add(Dr)
                    End If
                Catch ex As Exception
                    s &= ex.Message & vbCrLf
                End Try
            Next
            If s <> "" Then
                MessageBox.Show("Một số file bị cấm truy cập " & vbCrLf & s, gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            mv_DSImgAndIcon.Tables(0).AcceptChanges()
        End If
    End Sub
    Private Function InsertData(ByVal pv_sFilePath As String, ByVal pv_arrData() As Byte) As Integer
        Dim sv_oCmd As New SqlCommand
        Dim sv_sSql As String
        If bIsExisted(Path.GetFileName(pv_sFilePath)) Then
            Return -1
        End If
        sv_sSql = "INSERT INTO Sys_IMGANDICON (sFilePath,sFileName,Data) VALUES(@sFilePath,@sFileName,@Data)"
        Try
            Dim cmd As New SqlCommand(sv_sSql, VNS.Libs.globalVariables.SqlConn)
            With cmd
                .Parameters.Add(New SqlParameter("@sFilePath", SqlDbType.NVarChar)).Value = pv_sFilePath
                .Parameters.Add(New SqlParameter("@sFileName", SqlDbType.NVarChar)).Value = pv_sFilePath.Substring(pv_sFilePath.LastIndexOf("\") + 1)
                .Parameters.Add(New SqlParameter("@Data", SqlDbType.Image)).Value = pv_arrData
                .ExecuteNonQuery()
            End With
            Dim DA As New SqlDataAdapter("SELECT MAX(ID) from Sys_IMGANDICON", VNS.Libs.globalVariables.SqlConn)
            Dim DS As New DataSet
            DA.Fill(DS, "Sys_IMGANDICON")
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    Return DS.Tables(0).Rows(0)(0)
                Else
                    Return -1
                End If
            Else
                Return -1
            End If
        Catch ex As Exception
            Return -1
        End Try
    End Function
    Private Function bIsExisted(ByVal pv_sFileName As String) As Boolean
        Dim sv_oDS As New DataSet
        Dim sv_oDA As SqlDataAdapter
        Dim sv_sSql As String
        Try
            sv_sSql = "SELECT * from Sys_IMGANDICON WHERE sFileName =N'" & pv_sFileName & "'"
            sv_oDA = New SqlDataAdapter(sv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oDA.Fill(sv_oDS, "Sys_IMGANDICON")
            If sv_oDS.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
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
        dr = mv_DSImgAndIcon.Tables(0).NewRow
        With dr
            .Item("sFileName") = pv_sFileName ' 
            .Item("sRarFileName") = pv_sRarFileName '
            .Item("sVersion") = pv_sVersion '
            .Item("intRar") = pv_intRar '
            .Item("intPatch") = pv_intPatch '
            .Item("dblCapacity") = pv_dblCapacity '
            .Item("sDesc") = pv_sDesc '
        End With
        mv_DSImgAndIcon.Tables(0).Rows.Add(dr)
        mv_DSImgAndIcon.Tables(0).AcceptChanges()
        '-------------------------------------------
    End Sub
    Public Function bDeleteImgAndIcon(ByVal _ID As Integer) As Boolean
        Dim sv_oCmd As New SqlCommand
        Dim sv_DA As SqlDataAdapter
        Dim sv_sSql As String
        sv_sSql = "DELETE from Sys_IMGANDICON WHERE  ID=" & _ID
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

    Private Sub frm_ListImgsAndIcons_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
