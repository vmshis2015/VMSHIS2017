Public Class Frm_XuatMenuExcel
    Inherits System.Windows.Forms.Form
    Public dsQuanTri As New DataSet
    Dim mv_iCurrRow As Integer = 0
    Dim Excel As Object
    Dim WB As Object
    Dim WS As Object
    Dim mv_bFound As Boolean = False
    Dim mv_intLevel As Integer = 0
    Dim bDangXuatFile As Boolean = False
    Public mv_oNode As TreeNode
    Public mv_bToolBar As Boolean = False
    Public mv_bCancel As Boolean = True
    Public mv_sRoleName As String = String.Empty

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
    Friend WithEvents tvwQT As System.Windows.Forms.TreeView
    Friend WithEvents cmdExcel As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents chkVisible As System.Windows.Forms.CheckBox
    Friend WithEvents pgrBar As System.Windows.Forms.ProgressBar
    Friend WithEvents imlQuanTri As System.Windows.Forms.ImageList
    Friend WithEvents lblPercent As System.Windows.Forms.Label
    Friend WithEvents imgAdminnistration As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Frm_XuatMenuExcel))
        Me.tvwQT = New System.Windows.Forms.TreeView
        Me.imlQuanTri = New System.Windows.Forms.ImageList(Me.components)
        Me.cmdExcel = New System.Windows.Forms.Button
        Me.cmdClose = New System.Windows.Forms.Button
        Me.chkVisible = New System.Windows.Forms.CheckBox
        Me.pgrBar = New System.Windows.Forms.ProgressBar
        Me.lblPercent = New System.Windows.Forms.Label
        Me.imgAdminnistration = New System.Windows.Forms.ImageList(Me.components)
        Me.SuspendLayout()
        '
        'tvwQT
        '
        Me.tvwQT.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvwQT.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tvwQT.FullRowSelect = True
        Me.tvwQT.HideSelection = False
        Me.tvwQT.ImageList = Me.imgAdminnistration
        Me.tvwQT.Location = New System.Drawing.Point(0, 3)
        Me.tvwQT.Name = "tvwQT"
        Me.tvwQT.Size = New System.Drawing.Size(547, 303)
        Me.tvwQT.TabIndex = 1
        '
        'imlQuanTri
        '
        Me.imlQuanTri.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.imlQuanTri.ImageSize = New System.Drawing.Size(16, 16)
        Me.imlQuanTri.ImageStream = CType(resources.GetObject("imlQuanTri.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlQuanTri.TransparentColor = System.Drawing.Color.Transparent
        '
        'cmdExcel
        '
        Me.cmdExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdExcel.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExcel.Location = New System.Drawing.Point(375, 315)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(84, 23)
        Me.cmdExcel.TabIndex = 2
        Me.cmdExcel.Text = "&Xuất Menu"
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.Location = New System.Drawing.Point(465, 315)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.TabIndex = 3
        Me.cmdClose.Text = "&Thoát"
        '
        'chkVisible
        '
        Me.chkVisible.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkVisible.Checked = True
        Me.chkVisible.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVisible.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkVisible.Location = New System.Drawing.Point(3, 315)
        Me.chkVisible.Name = "chkVisible"
        Me.chkVisible.Size = New System.Drawing.Size(252, 24)
        Me.chkVisible.TabIndex = 4
        Me.chkVisible.Text = "Hiển thị quá trình xuất ra file Excel"
        '
        'pgrBar
        '
        Me.pgrBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgrBar.Location = New System.Drawing.Point(3, 342)
        Me.pgrBar.Name = "pgrBar"
        Me.pgrBar.Size = New System.Drawing.Size(483, 18)
        Me.pgrBar.Step = 1
        Me.pgrBar.TabIndex = 0
        '
        'lblPercent
        '
        Me.lblPercent.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPercent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPercent.Location = New System.Drawing.Point(486, 342)
        Me.lblPercent.Name = "lblPercent"
        Me.lblPercent.Size = New System.Drawing.Size(60, 18)
        Me.lblPercent.TabIndex = 5
        Me.lblPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'imgAdminnistration
        '
        Me.imgAdminnistration.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.imgAdminnistration.ImageSize = New System.Drawing.Size(16, 16)
        Me.imgAdminnistration.ImageStream = CType(resources.GetObject("imgAdminnistration.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgAdminnistration.TransparentColor = System.Drawing.Color.Transparent
        '
        'Frm_XuatMenuExcel
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(547, 360)
        Me.Controls.Add(Me.lblPercent)
        Me.Controls.Add(Me.chkVisible)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdExcel)
        Me.Controls.Add(Me.tvwQT)
        Me.Controls.Add(Me.pgrBar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_XuatMenuExcel"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Xuất danh sách Menu ra file Excel"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tvwQT.ImageList = gv_oMainForm.tvwAdminSystem.ImageList
        BuildTreeView()
        If mv_bToolBar Then
            cmdExcel.Text = "Chọn"
            chkVisible.Visible = False
            pgrBar.Visible = False
            lblPercent.Visible = False
        End If
    End Sub

    Private Sub BuildTreeView()
        Try
            tvwQT.Nodes.Clear()
            tvwQT.Nodes.Add(mv_oNode.Clone)
            With tvwQT
                .TopNode.ImageIndex = ImageIndex.RootRole
                .TopNode.SelectedImageIndex = ImageIndex.RootRole
                .ImageIndex = ImageIndex.LeafRole
                .SelectedImageIndex = ImageIndex.LeafRole
            End With
            tvwQT.SelectedNode = tvwQT.Nodes(0)
            tvwQT.Nodes(0).Expand()
        Catch ex As Exception

        End Try

    End Sub

#Region "Excel"
    Public Sub XuatFileExcel()
        Dim intColumn, intRow, intColumnValue As Integer
        Excel = CreateObject("Excel.Application")
        Excel.Visible = chkVisible.Checked
        Dim strAppPath As String
        strAppPath = "C:\MENU.XLS"
        Try
            mv_iCurrRow = 0
            cmdClose.Enabled = False
            pgrBar.Visible = True
            lblPercent.Visible = True
            chkVisible.Enabled = False
            cmdExcel.Enabled = False
            pgrBar.Maximum = gv_dsRole.Tables(0).Rows.Count
            pgrBar.Minimum = 0
            pgrBar.Step = 1
            pgrBar.Value = pgrBar.Minimum
            bDangXuatFile = True
            With Excel
                .SheetsInNewWorkbook = 1
                .Workbooks.Add()
                .Worksheets(1).Select()
                If gv_dsRole.Tables(0).Rows.Count > 0 Then
                    Dim Roots() As DataRow
                    Dim i As Integer
                    Roots = gv_dsRole.Tables(0).Select("FP_sBranchID='" & gv_sBranchID & "' and (iParentRole IS NULL OR iParentRole='-2')", "iOrder")
                    If UBound(Roots) >= 0 Then
                        For i = 0 To UBound(Roots)
                            Dim intLevel As Integer = 0
                            Dim RoleLabel As String
                            If IsDBNull(Roots(i).Item("FK_iFunctionID")) Then
                                RoleLabel = "1"
                            Else
                                If CStr(Roots(i).Item("FK_iFunctionID")).Trim = String.Empty Then
                                    RoleLabel = "1"
                                Else
                                    RoleLabel = "0"
                                End If
                            End If
                            'Đưa dữ liệu vào File Excel
                            'For displaying the column name in the the excel file.
                            If mv_iCurrRow = 0 Then
                                For intColumn = 0 To gv_dsRole.Tables(0).Columns.Count - 1
                                    .Cells(1, intColumn + 1).Value = gv_dsRole.Tables(0).Columns(intColumn).ColumnName.ToString
                                Next
                            End If
                            mv_iCurrRow += 1
                            pgrBar.Value += 1
                            'Me.Text = "Đã xuất được khoảng " & CInt(pgrBar.Value * 100 / pgrBar.Maximum) & " %"
                            lblPercent.Text = CInt(pgrBar.Value * 100 / pgrBar.Maximum).ToString & " %"
                            lblPercent.Refresh()
                            'Thêm hàng phân hệ
                            For intColumnValue = 0 To gv_dsRole.Tables(0).Columns.Count - 1
                                .Cells(mv_iCurrRow + 2, intColumnValue + 1).Value = Roots(i).ItemArray(intColumnValue).ToString
                            Next
                            DuyetDeQuy_Excel(gv_dsRole.Tables(0), Excel, Roots(i).Item("iRole"))
                        Next
                        pgrBar.Value = pgrBar.Maximum
                        Me.Text = "Xuất Menu ra file Excel"
                        If Not Excel.Visible Then Excel.Visible = True
                    End If
                End If
                .ActiveWorkbook().SaveAs(strAppPath)
                .ActiveWorkbook.Close()
            End With
            Excel.Quit()
            Excel = Nothing
            GC.Collect()
            bDangXuatFile = False
            cmdClose.Enabled = True
            chkVisible.Enabled = True
            cmdExcel.Enabled = True
            pgrBar.Visible = False
            lblPercent.Visible = False
        Catch ex As Exception
            Excel.Quit()
            Excel = Nothing
            GC.Collect()
            bDangXuatFile = False
            cmdClose.Enabled = True
            chkVisible.Enabled = True
            cmdExcel.Enabled = True
            pgrBar.Visible = False
            lblPercent.Visible = False
            'MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub DuyetDeQuy_Excel(ByVal Table As DataTable, ByRef ex As Object, ByVal Ma_Role As String)
        Dim Roles() As DataRow
        Roles = Table.Select("FP_sBranchID='" & gv_sBranchID & "' AND iParentRole='" & Ma_Role & "'", "iOrder")
        With Excel

            Dim i As Integer
            If UBound(Roles) >= 0 Then
                For i = 0 To UBound(Roles)
                    'Dim b As Boolean = False
                    Dim b As Integer
                    If IsDBNull(Roles(i).Item("FK_iFunctionID")) Then
                        b = 1
                    ElseIf CStr(Roles(i).Item("FK_iFunctionID")).Trim = String.Empty Then
                        b = 1
                    Else
                        b = 0
                    End If
                    mv_iCurrRow += 1
                    pgrBar.Value += 1
                    Dim intRole As Integer
                    Dim level As Integer
                    lblPercent.Text = CInt(pgrBar.Value * 100 / pgrBar.Maximum).ToString & " %"
                    lblPercent.Refresh()
                    mv_intLevel = 0
                    intGetlevel(CInt(Roles(i)("iParentRole")))
                    For intColumnValue As Integer = 0 To Table.Columns.Count - 1
                        If intColumnValue = 4 Or intColumnValue = 5 Then
                            .Cells(mv_iCurrRow + 2, intColumnValue + 1).Value = sGetSpace(mv_intLevel) & Roles(i).ItemArray(intColumnValue).ToString
                        Else
                            .Cells(mv_iCurrRow + 2, intColumnValue + 1).Value = Roles(i).ItemArray(intColumnValue).ToString
                        End If
                    Next
                    DuyetDeQuy_Excel(gv_dsRole.Tables(0), ex, Roles(i).Item("iRole"))
                Next
            End If
        End With
    End Sub
    Private Sub intGetlevel(ByVal pv_intParentRoleID As Integer)
        Dim Roles() As DataRow
        mv_intLevel += 1
        Roles = gv_dsRole.Tables(0).Select("iRole='" & pv_intParentRoleID & "'", "iOrder")
        If IsDBNull(Roles(0)("iParentRole")) Or Roles(0)("iParentRole") = "-2" Then
            Return
        Else
            If Roles(0)("iParentRole").ToString.Trim.Equals(String.Empty) Then
                Return
            End If
        End If
        intGetlevel(CInt(Roles(0)("iParentRole")))
    End Sub
    Private Function sGetSpace(ByVal intTime As Integer) As String

        Select Case intTime
            Case 1
                Return "       "
            Case 2
                Return "              "
            Case 3
                Return "                     "
            Case 4
                Return "                            "
            Case 5
                Return "                                   "
            Case Else
                Return "                                          "

        End Select
    End Function
#End Region


    Private Sub cmdExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcel.Click
        If mv_bToolBar Then
            mv_sRoleName = tvwQT.SelectedNode.Text
            gv_intRolePerformed = CInt(tvwQT.SelectedNode.Tag.ToString.Substring(tvwQT.SelectedNode.Tag.ToString.IndexOf("#") + 1))
            mv_bCancel = False
            Me.Close()
        Else
            Try
                Dim t As New Threading.Thread(AddressOf XuatFileExcel)
                t.Start()
            Catch ex As Exception
            End Try
        End If

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkVisible.CheckedChanged
        If bDangXuatFile Then
            Excel.Visible = chkVisible.Checked
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        mv_bCancel = True
        Me.Close()
    End Sub

    Private Sub tvwQT_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwQT.AfterSelect

    End Sub

    Private Sub tvwQT_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvwQT.MouseDown
        If Not tvwQT.SelectedNode Is tvwQT.GetNodeAt(e.X, e.Y) Then tvwQT.SelectedNode = tvwQT.GetNodeAt(e.X, e.Y)
    End Sub

    Private Sub tvwQT_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvwQT.DoubleClick
        cmdExcel.PerformClick()
    End Sub


    Private Sub Frm_XuatMenuExcel_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmdExcel.PerformClick()
        End If
        If e.KeyCode = Keys.Escape Then
            cmdClose.PerformClick()
        End If
        If e.KeyCode = Keys.F3 Then
            SearchStart()
        End If
        If e.KeyCode = Keys.F Then
            If e.Modifiers = Keys.Control Then
                SearchStart()
            End If
        End If
    End Sub
#Region "Tìm kiếm trên cây"
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Bắt đầu thực hiện tìm kiếm trên TreeView
    'Đầu vào          :
    'Đầu ra            :Datarow chứa dữ liệu
    'Người tạo       :CuongDV
    'Ngày tạo         :26/04/2006
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub SearchStart()
        Dim sv_oForm As New frmSearchTree
        mv_bFound = False
        Dim oNode As TreeNode = tvwQT.SelectedNode
        Dim sTag As String = oNode.Tag.ToString.ToUpper
        Dim sSeachType As String = String.Empty
        Dim sStatus As String = String.Empty
        Dim sNotFound As String = ""
        If InStr(sTag, "QTHT") > 0 Then
            sStatus = "Tìm kiếm trên toàn cây"
            sSeachType = "QTHT"
            sNotFound = "Không tìm được Node nào trên cây có giá trị: "
        ElseIf InStr(sTag, "USER") > 0 Then
            sSeachType = "USER"
            sStatus = "Tìm kiếm người dùng"
            sNotFound = "Không tìm thấy người dùng: "
        ElseIf InStr(sTag, "FUNCTION") > 0 Then
            sSeachType = "FUNCTION"
            sNotFound = "Không tìm thấy chức năng: "
            sStatus = "Tìm kiếm chức năng"
        ElseIf InStr(sTag, "ROLE") > 0 Then
            sSeachType = "ROLE"
            sNotFound = "Không tìm thấy Role: "
            sStatus = "Tìm kiếm Role"
        ElseIf InStr(sTag, "PARAM") > 0 Then
            sSeachType = "PARAM"
            sNotFound = "Không tìm thấy tham số: "
            sStatus = "Tìm kiếm tham số"
        End If
        sv_oForm.lblStatus.Text = sStatus
        sv_oForm.mv_sOldStatus = sStatus
        sv_oForm.Text = sStatus
        sv_oForm.chkSearchBranch.Enabled = False
        If sSeachType = "QTHT" Then
            sv_oForm.chkSearchBranch.Visible = False
        Else
            sv_oForm.chkSearchBranch.Visible = True
        End If
        sv_oForm.ShowDialog()
        If sv_oForm.mv_bHasSearch Then
            SearchTree(sv_oForm.chkSearchBranch.Checked, sSeachType, sv_oForm.mv_bSearchLike, sv_oForm.mv_sValue)
            If Not mv_bFound Then
                MessageBox.Show(sNotFound & sv_oForm.mv_sValue, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Tìm kiếm trên treeView
    'Đầu vào          :Kiểu tìm kiếm theo nhánh hay trên toàn TreeView, tìm kiếm trên nhánh nào?,kiểu tìm kiếm % tự hay =,giá trị tìm kiếm
    'Đầu ra            :Datarow chứa dữ liệu
    'Người tạo       :CuongDV
    'Ngày tạo         :26/04/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub SearchTree(ByVal pv_bSeachBranch As Boolean, ByVal pv_sSearchType As String, ByVal pv_bSearchLike As Boolean, ByVal pv_sValue As String)
        Try
            If Not pv_bSeachBranch Then
                SearchNode(tvwQT.Nodes(0), pv_bSearchLike, pv_sValue)
            Else
                Select Case pv_sSearchType
                    Case "QTHT"
                        SearchNode(tvwQT.Nodes(0), pv_bSearchLike, pv_sValue)
                    Case "USER"
                        SearchNode(tvwQT.Nodes(0).Nodes(0), pv_bSearchLike, pv_sValue)
                    Case "FUNCTION"
                        SearchNode(tvwQT.Nodes(0).Nodes(1), pv_bSearchLike, pv_sValue)
                    Case "ROLE"
                        SearchNode(tvwQT.Nodes(0), pv_bSearchLike, pv_sValue)
                    Case "PARAM"
                        SearchNode(tvwQT.Nodes(0).Nodes(3), pv_bSearchLike, pv_sValue)
                End Select
            End If

        Catch ex As Exception

        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Thủ tục đệ quy tìm giá trị trên một Node cha và các con của nó
    'Đầu vào          :Node cần tìm kiếm, kiểu tìm kiếm là tương tự hay giống hệt, giá trị tìm kiếm
    'Đầu ra            :Datarow chứa dữ liệu
    'Người tạo       :CuongDV
    'Ngày tạo         :26/04/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub SearchNode(ByVal pv_oNode As TreeNode, ByVal pv_sSearchLike As String, ByVal pv_sValue As String)
        For Each oNode As TreeNode In pv_oNode.Nodes
            If pv_sSearchLike Then
                If InStr(oNode.Text.ToUpper, pv_sValue.ToUpper) > 0 And Not mv_bFound Then
                    tvwQT.SelectedNode = oNode
                    mv_bFound = True
                    Exit Sub
                End If
            Else
                If oNode.Text.Trim.ToUpper = pv_sValue.ToUpper And Not mv_bFound Then
                    tvwQT.SelectedNode = oNode
                    mv_bFound = True
                    Exit Sub
                End If
            End If
            If Not mv_bFound Then SearchNode(oNode, pv_sSearchLike, pv_sValue)
        Next
    End Sub
#End Region
End Class
