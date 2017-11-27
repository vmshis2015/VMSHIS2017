Public Class frm_TbrProperty
    Inherits System.Windows.Forms.Form
    Public mv_intParentID As Integer
    Public mv_DT As DataTable
    Public mv_DV As New DataView
    Public mv_bUpdating As Boolean = False

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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmdDown As System.Windows.Forms.Button
    Friend WithEvents cmdUp As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdRemove As System.Windows.Forms.Button
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents txtText As System.Windows.Forms.TextBox
    Friend WithEvents txtFuntion As System.Windows.Forms.TextBox
    Friend WithEvents txtImgPath As System.Windows.Forms.TextBox
    Friend WithEvents txtTTT As System.Windows.Forms.TextBox
    Friend WithEvents cboStyle As System.Windows.Forms.ComboBox
    Friend WithEvents cmdGetFuntion As System.Windows.Forms.Button
    Friend WithEvents cmdGetImg As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents chkDisplayText As System.Windows.Forms.CheckBox
    Friend WithEvents lstButton As System.Windows.Forms.ListBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frm_TbrProperty))
        Me.lstButton = New System.Windows.Forms.ListBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chkDisplayText = New System.Windows.Forms.CheckBox
        Me.txtName = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtDesc = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.cmdGetImg = New System.Windows.Forms.Button
        Me.cmdGetFuntion = New System.Windows.Forms.Button
        Me.cboStyle = New System.Windows.Forms.ComboBox
        Me.txtTTT = New System.Windows.Forms.TextBox
        Me.txtImgPath = New System.Windows.Forms.TextBox
        Me.txtFuntion = New System.Windows.Forms.TextBox
        Me.txtText = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.cmdDown = New System.Windows.Forms.Button
        Me.cmdUp = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.cmdRemove = New System.Windows.Forms.Button
        Me.cmdOK = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdClose = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstButton
        '
        Me.lstButton.Location = New System.Drawing.Point(3, 3)
        Me.lstButton.Name = "lstButton"
        Me.lstButton.Size = New System.Drawing.Size(129, 251)
        Me.lstButton.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkDisplayText)
        Me.GroupBox1.Controls.Add(Me.txtName)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtDesc)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cmdGetImg)
        Me.GroupBox1.Controls.Add(Me.cmdGetFuntion)
        Me.GroupBox1.Controls.Add(Me.cboStyle)
        Me.GroupBox1.Controls.Add(Me.txtTTT)
        Me.GroupBox1.Controls.Add(Me.txtImgPath)
        Me.GroupBox1.Controls.Add(Me.txtFuntion)
        Me.GroupBox1.Controls.Add(Me.txtText)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Location = New System.Drawing.Point(171, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(312, 285)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Thuộc tính"
        '
        'chkDisplayText
        '
        Me.chkDisplayText.Location = New System.Drawing.Point(96, 219)
        Me.chkDisplayText.Name = "chkDisplayText"
        Me.chkDisplayText.Size = New System.Drawing.Size(141, 24)
        Me.chkDisplayText.TabIndex = 15
        Me.chkDisplayText.Text = "Hiển thị Text"
        '
        'txtName
        '
        Me.txtName.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.Location = New System.Drawing.Point(96, 24)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(204, 21)
        Me.txtName.TabIndex = 0
        Me.txtName.Text = ""
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 27)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(87, 18)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Tên nút"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDesc
        '
        Me.txtDesc.Location = New System.Drawing.Point(96, 174)
        Me.txtDesc.Multiline = True
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDesc.Size = New System.Drawing.Size(207, 45)
        Me.txtDesc.TabIndex = 6
        Me.txtDesc.Text = ""
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(9, 174)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(84, 18)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Mô tả thêm"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdGetImg
        '
        Me.cmdGetImg.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdGetImg.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGetImg.Image = CType(resources.GetObject("cmdGetImg.Image"), System.Drawing.Image)
        Me.cmdGetImg.Location = New System.Drawing.Point(276, 123)
        Me.cmdGetImg.Name = "cmdGetImg"
        Me.cmdGetImg.Size = New System.Drawing.Size(24, 21)
        Me.cmdGetImg.TabIndex = 8
        Me.cmdGetImg.TabStop = False
        Me.ToolTip1.SetToolTip(Me.cmdGetImg, "Click vào đây để chọn Icon cho nút (Nhấn phím I)")
        '
        'cmdGetFuntion
        '
        Me.cmdGetFuntion.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdGetFuntion.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGetFuntion.Image = CType(resources.GetObject("cmdGetFuntion.Image"), System.Drawing.Image)
        Me.cmdGetFuntion.Location = New System.Drawing.Point(276, 72)
        Me.cmdGetFuntion.Name = "cmdGetFuntion"
        Me.cmdGetFuntion.Size = New System.Drawing.Size(24, 21)
        Me.cmdGetFuntion.TabIndex = 7
        Me.cmdGetFuntion.TabStop = False
        Me.ToolTip1.SetToolTip(Me.cmdGetFuntion, "Click vào đây để chọn chức năng thực hiện cho nút (Nhấn phím C)")
        '
        'cboStyle
        '
        Me.cboStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStyle.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStyle.Items.AddRange(New Object() {"Button", "Separator"})
        Me.cboStyle.Location = New System.Drawing.Point(96, 96)
        Me.cboStyle.Name = "cboStyle"
        Me.cboStyle.Size = New System.Drawing.Size(180, 23)
        Me.cboStyle.TabIndex = 3
        '
        'txtTTT
        '
        Me.txtTTT.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTTT.Location = New System.Drawing.Point(96, 147)
        Me.txtTTT.Name = "txtTTT"
        Me.txtTTT.Size = New System.Drawing.Size(204, 21)
        Me.txtTTT.TabIndex = 5
        Me.txtTTT.Text = ""
        '
        'txtImgPath
        '
        Me.txtImgPath.Enabled = False
        Me.txtImgPath.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImgPath.Location = New System.Drawing.Point(96, 123)
        Me.txtImgPath.Name = "txtImgPath"
        Me.txtImgPath.Size = New System.Drawing.Size(177, 21)
        Me.txtImgPath.TabIndex = 4
        Me.txtImgPath.Text = ""
        '
        'txtFuntion
        '
        Me.txtFuntion.Enabled = False
        Me.txtFuntion.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFuntion.Location = New System.Drawing.Point(96, 72)
        Me.txtFuntion.Name = "txtFuntion"
        Me.txtFuntion.Size = New System.Drawing.Size(177, 21)
        Me.txtFuntion.TabIndex = 2
        Me.txtFuntion.Text = ""
        '
        'txtText
        '
        Me.txtText.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtText.Location = New System.Drawing.Point(96, 48)
        Me.txtText.Name = "txtText"
        Me.txtText.Size = New System.Drawing.Size(204, 21)
        Me.txtText.TabIndex = 1
        Me.txtText.Text = ""
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(9, 147)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(84, 18)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "ToolTipText"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(9, 123)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 18)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Ảnh"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 99)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 18)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Kiểu nút"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 75)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 18)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Chức năng"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 18)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Text"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Navy
        Me.Label8.Location = New System.Drawing.Point(99, 240)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(201, 36)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Nhấn phím Enter để cập nhật thuộc tính sau khi gõ"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdDown
        '
        Me.cmdDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDown.Image = CType(resources.GetObject("cmdDown.Image"), System.Drawing.Image)
        Me.cmdDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdDown.Location = New System.Drawing.Point(138, 132)
        Me.cmdDown.Name = "cmdDown"
        Me.cmdDown.Size = New System.Drawing.Size(27, 33)
        Me.cmdDown.TabIndex = 3
        '
        'cmdUp
        '
        Me.cmdUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdUp.Image = CType(resources.GetObject("cmdUp.Image"), System.Drawing.Image)
        Me.cmdUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdUp.Location = New System.Drawing.Point(138, 96)
        Me.cmdUp.Name = "cmdUp"
        Me.cmdUp.Size = New System.Drawing.Size(27, 33)
        Me.cmdUp.TabIndex = 4
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(6, 291)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(474, 3)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        '
        'cmdAdd
        '
        Me.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAdd.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdAdd.Location = New System.Drawing.Point(3, 261)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(60, 23)
        Me.cmdAdd.TabIndex = 1
        Me.cmdAdd.Text = "Thêm"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmdRemove
        '
        Me.cmdRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdRemove.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRemove.Image = CType(resources.GetObject("cmdRemove.Image"), System.Drawing.Image)
        Me.cmdRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdRemove.Location = New System.Drawing.Point(69, 261)
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.Size = New System.Drawing.Size(60, 23)
        Me.cmdRemove.TabIndex = 2
        Me.cmdRemove.Text = "Xóa"
        Me.cmdRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmdOK
        '
        Me.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOK.Image = CType(resources.GetObject("cmdOK.Image"), System.Drawing.Image)
        Me.cmdOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdOK.Location = New System.Drawing.Point(285, 303)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(90, 24)
        Me.cmdOK.TabIndex = 5
        Me.cmdOK.Text = "Chấp nhận"
        Me.cmdOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmdClose
        '
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdClose.Location = New System.Drawing.Point(384, 303)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(90, 24)
        Me.cmdClose.TabIndex = 9
        Me.cmdClose.Text = "Th&oát"
        '
        'frm_TbrProperty
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(483, 338)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdRemove)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.cmdUp)
        Me.Controls.Add(Me.cmdDown)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lstButton)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_TbrProperty"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Danh sách các Nút trên Toolbar"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub
    Private Sub LoadButton()
        Try
            Dim cls_TbrBtn As New clsTbrButton
            gv_dsTbrBtn = cls_TbrBtn.dsGetButton(False)
            mv_DV = gv_dsTbrBtn.Tables(0).DefaultView
            'mv_DV.RowFilter = "FP_intRoleID= " & gv_intSubSysID
            mv_DV.Sort = "intOrder ASC "
            If mv_DV.Count > 0 Then
                For i As Integer = 0 To mv_DV.Count - 1
                    lstButton.Items.Add(i.ToString & "-" & mv_DV.Item(i)("sName"))
                Next
            End If
            cls_TbrBtn = Nothing
        Catch ex As Exception

        End Try
    End Sub
    Private Sub frm_TbrProperty_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        gv_oTTT.SetToolTip(cmdGetImg, "Click vào đây để chọn Icon cho nút")
        Me.Text = "ToolBar - " & gv_sSubSysName
        LoadButton()
        cboStyle.SelectedIndex = 0
        If lstButton.Items.Count <= 0 Then
            cmdUp.Enabled = False
            cmdDown.Enabled = False
            cmdRemove.Enabled = False
            cmdAdd.Focus()
            cmdGetFuntion.Enabled = False
            cmdGetImg.Enabled = False
        Else
            lstButton.SelectedIndex = 0
        End If
        mv_bUpdating = False
        txtFuntion.BackColor = Color.WhiteSmoke
        txtImgPath.BackColor = Color.WhiteSmoke
    End Sub

    Private Sub frm_TbrProperty_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            processtabkey(True)
        End If
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
        If e.KeyCode = Keys.F Then
            If e.Modifiers = Keys.Control Then
                cmdGetFuntion.PerformClick()
            End If
        End If
        If e.KeyCode = Keys.C Then
            cmdGetFuntion.PerformClick()
        End If
        If e.KeyCode = Keys.I Then
            cmdGetImg.PerformClick()
        End If
    End Sub

    Private Sub cmdGetFuntion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetFuntion.Click
        Dim sv_oForm As New Frm_XuatMenuExcel
        sv_oForm.Text = "Chọn chức năng cho từng Nút của ToolBar"
        sv_oForm.mv_bToolBar = True
        sv_oForm.mv_oNode = gv_oNode
        sv_oForm.ShowDialog()
        If Not sv_oForm.mv_bCancel Then
            mv_bUpdating = True
            txtFuntion.Text = sv_oForm.mv_sRoleName
            txtTTT.Text = txtFuntion.Text
            UpdateDataRow(lstButton.SelectedItem.ToString.Substring(lstButton.SelectedItem.ToString.IndexOf("-") + 1), "sTTT", txtTTT.Text.Trim)
            UpdateDataRow(lstButton.SelectedItem.ToString.Substring(lstButton.SelectedItem.ToString.IndexOf("-") + 1), "sRoleName", txtFuntion.Text.Trim)
            UpdateDataRow(lstButton.SelectedItem.ToString.Substring(lstButton.SelectedItem.ToString.IndexOf("-") + 1), "intRolePerformed", gv_intRolePerformed, True, True, txtFuntion.Text.Trim)
            mv_bUpdating = False
        End If
    End Sub

    Private Sub cmdGetImg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetImg.Click
        Dim fileDiag As New OpenFileDialog
        Try
            fileDiag.Title = "Chọn Icon cho các nút trên ToolBar"
            fileDiag.Filter = "Icon|*.ico|Gif|*.gif|Jpg|*.Jpg|Bmp|*.Bmp|PNG|*.PNG"
            If fileDiag.ShowDialog = DialogResult.OK Then
                txtImgPath.Text = fileDiag.FileName
                UpdateDataRow(lstButton.SelectedItem.ToString.Substring(lstButton.SelectedItem.ToString.IndexOf("-") + 1), "sIconPath", txtImgPath.Text.Trim)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Try
            If lstButton.Items.Count - 1 <= 50 Then
                Dim DR As DataRow
                DR = gv_dsTbrBtn.Tables(0).NewRow
                With DR
                    .Item("FP_intRoleID") = gv_intSubSysID
                    .Item("FP_sBranchID") = gv_sBranchID
                    .Item("sText") = "Button" & intGetMax()
                    .Item("sIconPath") = ""
                    .Item("sName") = "Button" & intGetMax()
                    .Item("sDesc") = ""
                    .Item("sTTT") = ""
                    .Item("intStyle") = 0
                    .Item("intDisplayText") = 0
                    .Item("sRoleName") = "Chưa gán"
                    .Item("intRolePerformed") = -10
                    .Item("intOrder") = lstButton.Items.Count
                End With
                gv_dsTbrBtn.Tables(0).Rows.Add(DR)
                gv_dsTbrBtn.Tables(0).AcceptChanges()
                Dim clsTbrBtn As New clsTbrButton
                If clsTbrBtn.InsertButton(gv_intSubSysID, gv_sBranchID, "Button" & intGetMax.ToString, "", "", "", -10, lstButton.Items.Count, "Button" & intGetMax.ToString, cboStyle.SelectedIndex, "Chưa gán", IIf(chkDisplayText.Checked, 1, 0)) Then
                End If
                clsTbrBtn = Nothing
                lstButton.Items.Add(lstButton.Items.Count & "-" & "Button" & intGetMax())
                lstButton.SelectedIndex = lstButton.Items.Count - 1
                txtName.Focus()
            Else
                MessageBox.Show("Chương trình khuyến cáo bạn không nên dùng quá số Nút là 50", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            If lstButton.Items.Count > 0 Then
                cmdRemove.Enabled = True
                cmdGetFuntion.Enabled = True
                cmdGetImg.Enabled = True
            Else
                cmdGetFuntion.Enabled = False
                cmdGetImg.Enabled = False
            End If
            gv_bChangeToolBar = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemove.Click
        Dim _index As Integer
        Dim k As Integer = 0
        Try
            If MessageBox.Show("Bạn có thực sự muốn xóa nút này không?", gv_sAnnouncement, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                _index = lstButton.SelectedIndex
                If lstButton.Items.Count > 0 Then
                    Dim clsTbrBtn As New clsTbrButton
                    If clsTbrBtn.bDelete(lstButton.SelectedItem.ToString.Substring(lstButton.SelectedItem.ToString.IndexOf("-") + 1)) Then
                        For Each dr As DataRow In gv_dsTbrBtn.Tables(0).Rows
                            If lstButton.SelectedIndex & "-" & dr("sName") = lstButton.SelectedItem And dr("FP_intRoleID") = gv_intSubSysID Then
                                gv_dsTbrBtn.Tables(0).Rows.Remove(dr)
                                Exit For
                            End If
                        Next
                        For Each dr As DataRow In gv_dsTbrBtn.Tables(0).Rows
                            If CInt(dr("intOrder")) > _index And dr("FP_intRoleID") = gv_intSubSysID Then
                                If clsTbrBtn.UpdateButton(dr("sName"), "intOrder", dr("intOrder") - 1, True) Then
                                    k += 1
                                    dr("intOrder") = dr("intOrder") - 1
                                    For j As Integer = 0 To lstButton.Items.Count - 1
                                        If lstButton.Items(j).ToString = j & "-" & dr("sName") And j <> _index Then
                                            lstButton.Items(j) = dr("intOrder") & "-" & lstButton.Items(j).ToString.Substring(lstButton.Items(j).ToString.IndexOf("-") + 1)
                                        End If
                                    Next
                                End If
                            End If
                        Next
                        gv_dsTbrBtn.Tables(0).AcceptChanges()
                        lstButton.Items.RemoveAt(lstButton.SelectedIndex)
                        If lstButton.Items.Count > 0 Then
                            If _index <= lstButton.Items.Count - 1 Then
                                lstButton.SelectedIndex = _index
                            Else
                                lstButton.SelectedIndex = _index - 1
                            End If
                        End If
                    End If
                End If
                If lstButton.Items.Count = 0 Then
                    cmdRemove.Enabled = False
                    cmdGetFuntion.Enabled = False
                    cmdGetImg.Enabled = False
                End If
                gv_bChangeToolBar = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Try
            If Asc(e.KeyChar) = Keys.Enter Then e.Handled = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtText_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtText.TextChanged

    End Sub
    Private Sub UpdateDataRow(ByVal pv_sName As String, ByVal pv_sFieldName As String, ByVal pv_sValue As String, Optional ByVal pv_bIntValue As Boolean = False, Optional ByVal pv_bUpdatesRoleName As Boolean = False, Optional ByVal pv_sRoleName As String = "")
        Try
            Dim clsTbrBtn As New clsTbrButton
            If clsTbrBtn.UpdateButton(pv_sName, pv_sFieldName, pv_sValue, pv_bIntValue, pv_bUpdatesRoleName, pv_sRoleName) Then
                For Each dr As DataRow In gv_dsTbrBtn.Tables(0).Rows
                    If lstButton.SelectedIndex & "-" & dr("sName") = lstButton.SelectedIndex & "-" & pv_sName And dr("FP_intRoleID") = gv_intSubSysID Then
                        If pv_bUpdatesRoleName Then
                            dr("sRoleName") = pv_sRoleName
                            dr("intRolePerformed") = CInt(pv_sValue)
                        Else
                            If pv_bIntValue Then
                                dr(pv_sFieldName) = CInt(pv_sValue)
                            Else
                                dr(pv_sFieldName) = pv_sValue
                            End If
                        End If
                        Exit For
                    End If
                Next
                gv_dsTbrBtn.Tables(0).AcceptChanges()
                lstButton.Items(lstButton.SelectedIndex) = lstButton.SelectedIndex & "-" & txtName.Text.Trim
            Else
                txtName.Focus()
            End If
            gv_bChangeToolBar = True
            clsTbrBtn = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyDown

        Try
            If e.KeyCode = Keys.Enter Then
                If Not txtName.Text.Trim.Equals(String.Empty) Then
                    UpdateDataRow(lstButton.SelectedItem.ToString.Substring(lstButton.SelectedItem.ToString.IndexOf("-") + 1), "sName", txtName.Text.Trim)
                Else
                    MessageBox.Show("Tên không thể bỏ trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtName.Focus()
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub lstButton_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstButton.SelectedIndexChanged
        Try
            If Not mv_bUpdating Then
                For Each dr As DataRow In gv_dsTbrBtn.Tables(0).Rows
                    If lstButton.SelectedIndex & "-" & dr("sName") = lstButton.SelectedItem And dr("FP_intRoleID") = gv_intSubSysID Then
                        txtName.Text = dr("sName")
                        txtFuntion.Text = dr("sRoleName")
                        txtText.Text = dr("sText")
                        cboStyle.SelectedIndex = dr("intStyle")
                        txtImgPath.Text = dr("sIconPath")
                        txtTTT.Text = dr("sTTT")
                        txtDesc.Text = dr("sDesc")
                        chkDisplayText.Checked = IIf(dr("intDisplayText") = 0, False, True)
                        Exit For
                    End If
                Next
                If lstButton.Items.Count <= 1 Then
                    cmdUp.Enabled = False
                    cmdDown.Enabled = False
                    Return
                End If
                If lstButton.SelectedIndex = 0 Then
                    cmdUp.Enabled = False
                    cmdDown.Enabled = True
                    Return
                End If
                If lstButton.SelectedIndex = lstButton.Items.Count - 1 Then
                    cmdUp.Enabled = True
                    cmdDown.Enabled = False
                    Return
                End If
                If lstButton.SelectedIndex > 0 And lstButton.SelectedIndex < lstButton.Items.Count - 1 Then
                    cmdUp.Enabled = True
                    cmdDown.Enabled = True
                    Return
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkDisplayText_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDisplayText.CheckedChanged
        Try
            UpdateDataRow(lstButton.SelectedItem.ToString.Substring(lstButton.SelectedItem.ToString.IndexOf("-") + 1), "intDisplayText", IIf(chkDisplayText.Checked, 1, 0), True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cboStyle_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboStyle.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                UpdateDataRow(lstButton.SelectedItem.ToString.Substring(lstButton.SelectedItem.ToString.IndexOf("-") + 1), "intStyle", cboStyle.SelectedIndex, True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtText_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtText.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                UpdateDataRow(lstButton.SelectedItem.ToString.Substring(lstButton.SelectedItem.ToString.IndexOf("-") + 1), "sText", txtText.Text.Trim)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtFuntion_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFuntion.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                UpdateDataRow(lstButton.SelectedItem.ToString.Substring(lstButton.SelectedItem.ToString.IndexOf("-") + 1), "intRolePerformed", gv_intRolePerformed, True, True, txtFuntion.Text.Trim)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtImgPath_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtImgPath.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                UpdateDataRow(lstButton.SelectedItem.ToString.Substring(lstButton.SelectedItem.ToString.IndexOf("-") + 1), "sIconPath", txtImgPath.Text.Trim)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtTTT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTTT.KeyDown
        If e.KeyCode = Keys.Enter Then
            Try
                UpdateDataRow(lstButton.SelectedItem.ToString.Substring(lstButton.SelectedItem.ToString.IndexOf("-") + 1), "sTTT", txtTTT.Text.Trim)
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub txtDesc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDesc.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                UpdateDataRow(lstButton.SelectedItem.ToString.Substring(lstButton.SelectedItem.ToString.IndexOf("-") + 1), "sDesc", txtDesc.Text.Trim)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function intGetMax() As Integer
        Dim s As String = "$"
        For i As Integer = 0 To lstButton.Items.Count - 1
            s &= lstButton.Items(i).ToString & "$"
        Next
        For i As Integer = 0 To 1000
            If InStr(s.ToUpper, "BUTTON" & i, CompareMethod.Text) > 0 Then
            Else
                Return i
            End If
        Next

    End Function

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged

    End Sub

    Private Sub txtFuntion_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFuntion.TextChanged

    End Sub

    Private Sub lstButton_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstButton.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    cmdRemove.PerformClick()
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Me.Close()
    End Sub

    Private Sub cmdUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUp.Click
        Dim _Index As Integer
        Dim s As String
        _Index = lstButton.SelectedIndex
        s = lstButton.Items(_Index)
        UpdateDataRow(lstButton.SelectedItem.ToString.Substring(lstButton.SelectedItem.ToString.IndexOf("-") + 1), "intOrder", _Index - 1, True)
        UpdateDataRow(lstButton.Items(_Index - 1).ToString.Substring(lstButton.SelectedItem.ToString.IndexOf("-") + 1), "intOrder", _Index, True)
        lstButton.Items(_Index) = _Index & "-" & lstButton.Items(_Index - 1).ToString.Substring(lstButton.Items(_Index - 1).ToString.IndexOf("-") + 1)
        lstButton.Items(_Index - 1) = (_Index - 1).ToString & "-" & s.Substring(s.IndexOf("-") + 1)
        lstButton.SelectedIndex = _Index - 1
    End Sub

    Private Sub cmdDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDown.Click
        Dim _Index As Integer
        Dim s As String
        _Index = lstButton.SelectedIndex
        s = lstButton.Items(_Index)
        UpdateDataRow(lstButton.SelectedItem.ToString.Substring(lstButton.SelectedItem.ToString.IndexOf("-") + 1), "intOrder", _Index + 1, True)
        UpdateDataRow(lstButton.Items(_Index + 1).ToString.Substring(lstButton.SelectedItem.ToString.IndexOf("-") + 1), "intOrder", _Index, True)
        lstButton.Items(_Index) = _Index.ToString & "-" & lstButton.Items(_Index + 1).ToString.Substring(lstButton.Items(_Index + 1).ToString.IndexOf("-") + 1)
        lstButton.Items(_Index + 1) = (_Index + 1).ToString & "-" & s.Substring(s.IndexOf("-") + 1)
        lstButton.SelectedIndex = _Index + 1
    End Sub

    Private Sub txtImgPath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtImgPath.TextChanged

    End Sub
    Private Sub Saveall()
        Dim clsTbrBtn As New clsTbrButton
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtTTT_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTTT.TextChanged

    End Sub
End Class
