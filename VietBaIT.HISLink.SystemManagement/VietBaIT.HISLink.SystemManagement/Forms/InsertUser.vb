Public Class InsertUser
    Inherits System.Windows.Forms.Form
    Private mv_iStatus As Status
    Private mv_bSuccess As Boolean = False
    Private mv_sUserName As String = "UnKnown"
    Public CurrGroupList As String = ""
    Public CurrDelegateList As String = ""
    Public NewGroupList As String = ""
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdCreate As System.Windows.Forms.Button
    Friend WithEvents cmdRemoveGroup As System.Windows.Forms.Button
    Friend WithEvents cmdAddGroup As System.Windows.Forms.Button
    Friend WithEvents lstGroupList As System.Windows.Forms.ListBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents cmdRemoveDelegate As System.Windows.Forms.Button
    Friend WithEvents cmdAddDelegate As System.Windows.Forms.Button
    Friend WithEvents lstDelegate As System.Windows.Forms.ListBox
    Public mv_bAdmin As Boolean = False

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub
    Public Sub New(ByVal pv_sTitle As String)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.Text = pv_sTitle
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
    Friend WithEvents grbInfor As System.Windows.Forms.GroupBox
    Protected Friend WithEvents txtDepart As System.Windows.Forms.TextBox
    Protected Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Protected Friend WithEvents txtFullName As System.Windows.Forms.TextBox
    Protected Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Protected Friend WithEvents lblMoTa As System.Windows.Forms.Label
    Protected Friend WithEvents lblDonVi As System.Windows.Forms.Label
    Protected Friend WithEvents lblTenNguoiDung As System.Windows.Forms.Label
    Protected Friend WithEvents lblTenDangNhap As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(InsertUser))
        Me.grbInfor = New System.Windows.Forms.GroupBox
        Me.txtDepart = New System.Windows.Forms.TextBox
        Me.txtDesc = New System.Windows.Forms.TextBox
        Me.txtFullName = New System.Windows.Forms.TextBox
        Me.txtUserName = New System.Windows.Forms.TextBox
        Me.lblMoTa = New System.Windows.Forms.Label
        Me.lblDonVi = New System.Windows.Forms.Label
        Me.lblTenNguoiDung = New System.Windows.Forms.Label
        Me.lblTenDangNhap = New System.Windows.Forms.Label
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.lblName = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.cmdRemoveGroup = New System.Windows.Forms.Button
        Me.cmdAddGroup = New System.Windows.Forms.Button
        Me.lstGroupList = New System.Windows.Forms.ListBox
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.cmdRemoveDelegate = New System.Windows.Forms.Button
        Me.cmdAddDelegate = New System.Windows.Forms.Button
        Me.lstDelegate = New System.Windows.Forms.ListBox
        Me.cmdClose = New System.Windows.Forms.Button
        Me.cmdCreate = New System.Windows.Forms.Button
        Me.grbInfor.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.SuspendLayout()
        '
        'grbInfor
        '
        Me.grbInfor.Controls.Add(Me.txtDepart)
        Me.grbInfor.Controls.Add(Me.txtDesc)
        Me.grbInfor.Controls.Add(Me.txtFullName)
        Me.grbInfor.Controls.Add(Me.txtUserName)
        Me.grbInfor.Controls.Add(Me.lblMoTa)
        Me.grbInfor.Controls.Add(Me.lblDonVi)
        Me.grbInfor.Controls.Add(Me.lblTenNguoiDung)
        Me.grbInfor.Controls.Add(Me.lblTenDangNhap)
        Me.grbInfor.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grbInfor.Location = New System.Drawing.Point(6, 88)
        Me.grbInfor.Name = "grbInfor"
        Me.grbInfor.Size = New System.Drawing.Size(387, 162)
        Me.grbInfor.TabIndex = 0
        Me.grbInfor.TabStop = False
        Me.grbInfor.Text = "Thông tin người dùng"
        '
        'txtDepart
        '
        Me.txtDepart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDepart.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepart.Location = New System.Drawing.Point(112, 68)
        Me.txtDepart.MaxLength = 100
        Me.txtDepart.Name = "txtDepart"
        Me.txtDepart.Size = New System.Drawing.Size(259, 22)
        Me.txtDepart.TabIndex = 2
        '
        'txtDesc
        '
        Me.txtDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.Location = New System.Drawing.Point(112, 92)
        Me.txtDesc.MaxLength = 255
        Me.txtDesc.Multiline = True
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDesc.Size = New System.Drawing.Size(259, 58)
        Me.txtDesc.TabIndex = 3
        '
        'txtFullName
        '
        Me.txtFullName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFullName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFullName.Location = New System.Drawing.Point(112, 44)
        Me.txtFullName.MaxLength = 100
        Me.txtFullName.Name = "txtFullName"
        Me.txtFullName.Size = New System.Drawing.Size(259, 22)
        Me.txtFullName.TabIndex = 1
        '
        'txtUserName
        '
        Me.txtUserName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUserName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtUserName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserName.Location = New System.Drawing.Point(112, 20)
        Me.txtUserName.MaxLength = 50
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(259, 22)
        Me.txtUserName.TabIndex = 0
        '
        'lblMoTa
        '
        Me.lblMoTa.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMoTa.Location = New System.Drawing.Point(4, 96)
        Me.lblMoTa.Name = "lblMoTa"
        Me.lblMoTa.Size = New System.Drawing.Size(104, 16)
        Me.lblMoTa.TabIndex = 26
        Me.lblMoTa.Text = "Mô tả thêm"
        Me.lblMoTa.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDonVi
        '
        Me.lblDonVi.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDonVi.Location = New System.Drawing.Point(4, 72)
        Me.lblDonVi.Name = "lblDonVi"
        Me.lblDonVi.Size = New System.Drawing.Size(104, 16)
        Me.lblDonVi.TabIndex = 24
        Me.lblDonVi.Text = "Phòng ban"
        Me.lblDonVi.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTenNguoiDung
        '
        Me.lblTenNguoiDung.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTenNguoiDung.Location = New System.Drawing.Point(4, 48)
        Me.lblTenNguoiDung.Name = "lblTenNguoiDung"
        Me.lblTenNguoiDung.Size = New System.Drawing.Size(104, 16)
        Me.lblTenNguoiDung.TabIndex = 22
        Me.lblTenNguoiDung.Text = "Tên đầy đủ"
        Me.lblTenNguoiDung.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTenDangNhap
        '
        Me.lblTenDangNhap.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTenDangNhap.Location = New System.Drawing.Point(4, 24)
        Me.lblTenDangNhap.Name = "lblTenDangNhap"
        Me.lblTenDangNhap.Size = New System.Drawing.Size(104, 16)
        Me.lblTenDangNhap.TabIndex = 20
        Me.lblTenDangNhap.Text = "Tên người dùng"
        Me.lblTenDangNhap.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(3, 6)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(422, 348)
        Me.TabControl1.TabIndex = 18
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.lblName)
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Controls.Add(Me.PictureBox1)
        Me.TabPage1.Controls.Add(Me.grbInfor)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(414, 322)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Thông tin người dùng"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'lblName
        '
        Me.lblName.BackColor = System.Drawing.Color.Transparent
        Me.lblName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.Location = New System.Drawing.Point(62, 23)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(331, 31)
        Me.lblName.TabIndex = 4
        Me.lblName.Text = "NGƯỜI DÙNG"
        Me.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Location = New System.Drawing.Point(2, 63)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(462, 2)
        Me.GroupBox2.TabIndex = 11
        Me.GroupBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(7, 9)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(53, 51)
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.cmdRemoveGroup)
        Me.TabPage2.Controls.Add(Me.cmdAddGroup)
        Me.TabPage2.Controls.Add(Me.lstGroupList)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(414, 322)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Thuộc nhóm"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'cmdRemoveGroup
        '
        Me.cmdRemoveGroup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveGroup.Image = CType(resources.GetObject("cmdRemoveGroup.Image"), System.Drawing.Image)
        Me.cmdRemoveGroup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdRemoveGroup.Location = New System.Drawing.Point(91, 276)
        Me.cmdRemoveGroup.Name = "cmdRemoveGroup"
        Me.cmdRemoveGroup.Size = New System.Drawing.Size(80, 27)
        Me.cmdRemoveGroup.TabIndex = 11
        Me.cmdRemoveGroup.Text = "Remove"
        Me.cmdRemoveGroup.UseVisualStyleBackColor = True
        '
        'cmdAddGroup
        '
        Me.cmdAddGroup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdAddGroup.Image = CType(resources.GetObject("cmdAddGroup.Image"), System.Drawing.Image)
        Me.cmdAddGroup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdAddGroup.Location = New System.Drawing.Point(7, 276)
        Me.cmdAddGroup.Name = "cmdAddGroup"
        Me.cmdAddGroup.Size = New System.Drawing.Size(80, 27)
        Me.cmdAddGroup.TabIndex = 9
        Me.cmdAddGroup.Text = "Add"
        Me.cmdAddGroup.UseVisualStyleBackColor = True
        '
        'lstGroupList
        '
        Me.lstGroupList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstGroupList.FormattingEnabled = True
        Me.lstGroupList.Location = New System.Drawing.Point(8, 6)
        Me.lstGroupList.Name = "lstGroupList"
        Me.lstGroupList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.lstGroupList.Size = New System.Drawing.Size(400, 264)
        Me.lstGroupList.Sorted = True
        Me.lstGroupList.TabIndex = 8
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.cmdRemoveDelegate)
        Me.TabPage3.Controls.Add(Me.cmdAddDelegate)
        Me.TabPage3.Controls.Add(Me.lstDelegate)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(414, 322)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Delegate User"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'cmdRemoveDelegate
        '
        Me.cmdRemoveDelegate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveDelegate.Image = CType(resources.GetObject("cmdRemoveDelegate.Image"), System.Drawing.Image)
        Me.cmdRemoveDelegate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdRemoveDelegate.Location = New System.Drawing.Point(91, 283)
        Me.cmdRemoveDelegate.Name = "cmdRemoveDelegate"
        Me.cmdRemoveDelegate.Size = New System.Drawing.Size(80, 27)
        Me.cmdRemoveDelegate.TabIndex = 14
        Me.cmdRemoveDelegate.Text = "Remove"
        Me.cmdRemoveDelegate.UseVisualStyleBackColor = True
        '
        'cmdAddDelegate
        '
        Me.cmdAddDelegate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdAddDelegate.Image = CType(resources.GetObject("cmdAddDelegate.Image"), System.Drawing.Image)
        Me.cmdAddDelegate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdAddDelegate.Location = New System.Drawing.Point(7, 283)
        Me.cmdAddDelegate.Name = "cmdAddDelegate"
        Me.cmdAddDelegate.Size = New System.Drawing.Size(80, 27)
        Me.cmdAddDelegate.TabIndex = 13
        Me.cmdAddDelegate.Text = "Add"
        Me.cmdAddDelegate.UseVisualStyleBackColor = True
        '
        'lstDelegate
        '
        Me.lstDelegate.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstDelegate.FormattingEnabled = True
        Me.lstDelegate.Location = New System.Drawing.Point(8, 13)
        Me.lstDelegate.Name = "lstDelegate"
        Me.lstDelegate.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.lstDelegate.Size = New System.Drawing.Size(400, 264)
        Me.lstDelegate.Sorted = True
        Me.lstDelegate.TabIndex = 12
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdClose.Location = New System.Drawing.Point(317, 360)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(82, 27)
        Me.cmdClose.TabIndex = 20
        Me.cmdClose.Text = "Cancel"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'cmdCreate
        '
        Me.cmdCreate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCreate.Image = CType(resources.GetObject("cmdCreate.Image"), System.Drawing.Image)
        Me.cmdCreate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdCreate.Location = New System.Drawing.Point(227, 360)
        Me.cmdCreate.Name = "cmdCreate"
        Me.cmdCreate.Size = New System.Drawing.Size(82, 27)
        Me.cmdCreate.TabIndex = 19
        Me.cmdCreate.Text = "Create"
        Me.cmdCreate.UseVisualStyleBackColor = True
        '
        'InsertUser
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(427, 399)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdCreate)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "InsertUser"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Thêm người dùng"
        Me.grbInfor.ResumeLayout(False)
        Me.grbInfor.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Properties"
    Public Property ps_UserName() As String
        Get
            Return mv_sUserName
        End Get
        Set(ByVal Value As String)
            mv_sUserName = Value
        End Set
    End Property
    Public Property ps_iStatus() As Status
        Get
            Return mv_iStatus
        End Get
        Set(ByVal Value As Status)
            mv_iStatus = Value
        End Set
    End Property
    Public Property pb_Success() As Boolean
        Get
            Return mv_bSuccess
        End Get
        Set(ByVal Value As Boolean)
            mv_bSuccess = Value
        End Set
    End Property
#End Region

    Private Sub InsertFunction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Select Case mv_iStatus
                Case globalModule.Status.Update 'Cập nhật
                    cmdCreate.Text = "Apply"
                    GetDataForUpdate()
                Case Else
                    cmdCreate.Text = "Create"
            End Select
            If mv_bAdmin Then
                Me.Text = "Thêm mới quản trị hệ thống"
                grbInfor.Text = "Thông tin Admin"
                lblName.Text = "Quản trị hệ thống"
                TabControl1.TabPages.Remove(TabPage2)
                TabControl1.TabPages.Remove(TabPage3)
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
    Private Sub GetDataForUpdate()
        Dim sv_oclsUser As New clsUser
        Dim sv_DS As New DataSet
        Try
            CurrGroupList = ""
            sv_DS = sv_oclsUser.dsGetUserInfor(mv_sUserName)
            If sv_DS.Tables(0).Rows.Count > 0 Then
                txtUserName.Enabled = False
                txtUserName.Text = sv_DS.Tables(0).Rows(0)("PK_sUID")
                txtFullName.Text = sv_DS.Tables(0).Rows(0)("sFullName")
                txtDepart.Text = sv_DS.Tables(0).Rows(0)("sDepart")
                txtDesc.Text = sv_DS.Tables(0).Rows(0)("sDesc")
                Dim sv_DSGroupOfMember As DataRow() = gv_dsGroupMember.Tables(0).Select("UserID='" & mv_sUserName & "'")
                If Not IsNothing(sv_DSGroupOfMember) AndAlso sv_DSGroupOfMember.GetLength(0) > 0 Then
                    lstGroupList.Items.Clear()
                    For Each dr As DataRow In sv_DSGroupOfMember
                        CurrGroupList &= dr("GroupID").ToString & "#" & dr("GroupName") & ","
                        lstGroupList.Items.Add(dr("GroupID").ToString & "#" & dr("GroupName"))
                    Next
                    cmdRemoveGroup.Enabled = lstGroupList.Items.Count > 0
                End If

                Dim sv_DSDelegate As DataRow() = gv_dsDelegate.Tables(0).Select("MainUser='" & mv_sUserName & "'")
                If Not IsNothing(sv_DSDelegate) AndAlso sv_DSDelegate.GetLength(0) > 0 Then
                    lstDelegate.Items.Clear()
                    For Each dr As DataRow In sv_DSDelegate
                        CurrDelegateList &= dr("DelegateUser").ToString
                        lstDelegate.Items.Add(dr("DelegateUser").ToString)
                    Next
                    cmdRemoveDelegate.Enabled = lstDelegate.Items.Count > 0
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function mf_bCheckValidData() As Boolean
        Try
            If txtUserName.Text.Trim.Equals(String.Empty) Then
                MessageBox.Show("Bạn phải nhập UserName(tên truy cập)", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtUserName.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show("Lỗi cần gặp người lập trình" & ex.Message, gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End Try
    End Function
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreate.Click
        Dim sv_oclsUser As New clsUser
        Dim sv_sUserName As String = ""
        Dim sv_sFullName As String = ""
        Dim sv_sDepart As String = ""
        Dim sv_sDesc As String = ""
        Dim sv_oEncrypt As New VietBaIT.Encrypt(gv_sSymmetricAlgorithmName)
        Try
            If mf_bCheckValidData() Then
                sv_sUserName = ValidData(txtUserName.Text)
                sv_sFullName = ValidData(txtFullName.Text)
                sv_sDepart = ValidData(txtDepart.Text)
                sv_sDesc = ValidData(txtDesc.Text)
                mv_sUserName = sv_sUserName
                Select Case mv_iStatus
                    Case globalModule.Status.Update
                        If sv_oclsUser.UpdateUser(gv_sBranchID, sv_sUserName, sv_sFullName, sv_sDepart, sv_sDesc) Then
                            mv_bSuccess = True
                            Dim sv_oDR As DataRow
                            For Each sv_oDR In gv_dsUser.Tables(0).Rows
                                If sv_oDR.Item("PK_sUID").ToString.Trim.ToUpper.Equals(sv_sUserName.ToUpper) Then
                                    With sv_oDR
                                        .Item("sFullName") = sv_sFullName
                                        .Item("sDepart") = sv_sDepart
                                        .Item("sDesc") = sv_sDesc
                                    End With
                                    Exit For
                                End If
                            Next
                            gv_dsUser.Tables(0).AcceptChanges()
                            'Chỉnh lại tên trên Chiview
                            gv_oMainForm.tvwAdminSystem.SelectedNode.Text = sv_sUserName
                            gv_dsGroupUser.Tables(0).AcceptChanges()
                            'Đầu tiên xóa tất cả các Delegate
                            sv_oclsUser.DeleteAllDelegateOfUser(sv_sUserName)
                            'Xóa trong Dataset
                            Dim intCount As Integer = gv_dsDelegate.Tables(0).Select("MainUser='" & sv_sUserName & "'").GetLength(0)
                            Do While intCount > 0
                                For Each dr As DataRow In gv_dsDelegate.Tables(0).Rows
                                    If dr("MainUser").ToString.Trim.ToUpper = sv_sUserName.Trim.ToUpper Then
                                        intCount -= 1
                                        gv_dsDelegate.Tables(0).Rows.Remove(dr)
                                        gv_dsDelegate.Tables(0).AcceptChanges()
                                        Exit For
                                    End If
                                Next
                            Loop
                            Dim ID As Integer
                            'Thêm lại DelegateUser
                            For i As Integer = 0 To lstDelegate.Items.Count - 1
                                If lstDelegate.Items(i).ToString.Trim <> "" Then
                                    ID = sv_oclsUser.InsertDelegate(sv_sUserName, lstDelegate.Items(i).ToString)
                                    If ID <> -1 Then
                                        'Thêm vào Dataset
                                        Dim dr As DataRow = gv_dsDelegate.Tables(0).NewRow
                                        dr("ID") = ID
                                        dr("MainUser") = sv_sUserName
                                        dr("DelegateUser") = lstDelegate.Items(i).ToString.Trim
                                        gv_dsDelegate.Tables(0).Rows.Add(dr)
                                        gv_dsDelegate.Tables(0).AcceptChanges()
                                    End If
                                End If
                            Next
                            'Cập nhật Groups of Member
                            'Đầu tiên xóa tất cả các Group of Member
                            sv_oclsUser.DeleteAllGroupOfMember(sv_sUserName)
                            'Xóa trong Dataset
                            Dim intCount1 As Integer = gv_dsGroupMember.Tables(0).Select("UserID='" & sv_sUserName & "'").GetLength(0)
                            Do While intCount1 > 0
                                For Each dr As DataRow In gv_dsGroupMember.Tables(0).Rows
                                    If dr("UserID").ToString.ToUpper = sv_sUserName.ToUpper Then
                                        intCount1 -= 1
                                        gv_dsGroupMember.Tables(0).Rows.Remove(dr)
                                        gv_dsGroupMember.Tables(0).AcceptChanges()
                                        Exit For
                                    End If
                                Next
                            Loop
                            'Thêm lại Member
                            For i As Integer = 0 To lstGroupList.Items.Count - 1
                                If sv_oclsUser.InsertMemberOfGroup(gv_sBranchID, CInt(lstGroupList.Items(i).ToString().Substring(0, lstGroupList.Items(i).ToString().IndexOf("#"))), sv_sUserName) Then
                                    'Thêm vào Dataset
                                    Dim dr As DataRow = gv_dsGroupMember.Tables(0).NewRow
                                    dr("BranchID") = gv_sBranchID
                                    dr("GroupName") = lstGroupList.Items(i).ToString().Substring(lstGroupList.Items(i).ToString().IndexOf("#") + 1)
                                    dr("GroupID") = CInt(lstGroupList.Items(i).ToString().Substring(0, lstGroupList.Items(i).ToString().IndexOf("#")))
                                    dr("UserID") = sv_sUserName
                                    gv_dsGroupMember.Tables(0).Rows.Add(dr)
                                    gv_dsGroupMember.Tables(0).AcceptChanges()
                                End If
                            Next
                            Me.Close()
                        Else
                            MessageBox.Show("Lỗi cập nhật người dùng", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            mv_bSuccess = False
                            Return
                        End If
                    Case globalModule.Status.Insert
                        If sv_oclsUser.InsertUser(gv_sBranchID, sv_sUserName, sv_sFullName, sv_oEncrypt.Mahoa(""), 0, sv_sDepart, sv_sDesc, mv_bAdmin) Then
                            mv_bSuccess = True
                            If Not mv_bAdmin Then
                                Dim sv_oDR As DataRow
                                sv_oDR = gv_dsUser.Tables(0).NewRow
                                With sv_oDR
                                    .Item("spwd") = sv_oEncrypt.Mahoa("")
                                    .Item("iSecurityLevel") = 0
                                    .Item("PK_sUID") = sv_sUserName
                                    .Item("sFullName") = sv_sFullName
                                    .Item("sDepart") = sv_sDepart
                                    .Item("sDesc") = sv_sDesc
                                End With
                                gv_dsUser.Tables(0).Rows.Add(sv_oDR)
                                gv_dsUser.Tables(0).AcceptChanges()
                            End If
                            If Not mv_bAdmin Then
                                'Đầu tiên xóa tất cả các Delegate
                                sv_oclsUser.DeleteAllDelegateOfUser(sv_sUserName)
                                'Xóa trong Dataset
                                Dim intCount As Integer = gv_dsDelegate.Tables(0).Select("MainUser='" & sv_sUserName & "'").GetLength(0)
                                Do While intCount > 0
                                    For Each dr As DataRow In gv_dsDelegate.Tables(0).Rows
                                        If dr("MainUser").ToString.Trim.ToUpper = sv_sUserName.Trim.ToUpper Then
                                            intCount -= 1
                                            gv_dsDelegate.Tables(0).Rows.Remove(dr)
                                            gv_dsDelegate.Tables(0).AcceptChanges()
                                            Exit For
                                        End If
                                    Next
                                Loop
                                Dim ID As Integer
                                'Thêm lại DelegateUser
                                For i As Integer = 0 To lstDelegate.Items.Count - 1
                                    If lstDelegate.Items(i).ToString.Trim <> "" Then
                                        ID = sv_oclsUser.InsertDelegate(sv_sUserName, lstDelegate.Items(i).ToString)
                                        If ID <> -1 Then
                                            'Thêm vào Dataset
                                            Dim dr As DataRow = gv_dsDelegate.Tables(0).NewRow
                                            dr("ID") = ID
                                            dr("MainUser") = sv_sUserName
                                            dr("DelegateUser") = lstDelegate.Items(i).ToString.Trim
                                            gv_dsDelegate.Tables(0).Rows.Add(dr)
                                            gv_dsDelegate.Tables(0).AcceptChanges()
                                        End If
                                    End If
                                Next
                                'Cập nhật Groups of Member
                                'Đầu tiên xóa tất cả các Group of Member
                                sv_oclsUser.DeleteAllGroupOfMember(sv_sUserName)
                                'Xóa trong Dataset
                                Dim intCount1 As Integer = gv_dsGroupMember.Tables(0).Select("UserID='" & sv_sUserName & "'").GetLength(0)
                                Do While intCount1 > 0
                                    For Each dr As DataRow In gv_dsGroupMember.Tables(0).Rows
                                        If dr("UserID").ToString.ToUpper = sv_sUserName.ToUpper Then
                                            intCount1 -= 1
                                            gv_dsGroupMember.Tables(0).Rows.Remove(dr)
                                            gv_dsGroupMember.Tables(0).AcceptChanges()
                                            Exit For
                                        End If
                                    Next
                                Loop
                                'Thêm lại Member
                                For i As Integer = 0 To lstGroupList.Items.Count - 1
                                    If sv_oclsUser.InsertMemberOfGroup(gv_sBranchID, CInt(lstGroupList.Items(i).ToString().Substring(0, lstGroupList.Items(i).ToString().IndexOf("#"))), sv_sUserName) Then
                                        'Thêm vào Dataset
                                        Dim dr As DataRow = gv_dsGroupMember.Tables(0).NewRow
                                        dr("BranchID") = gv_sBranchID
                                        dr("GroupName") = lstGroupList.Items(i).ToString().Substring(lstGroupList.Items(i).ToString().IndexOf("#") + 1)
                                        dr("GroupID") = CInt(lstGroupList.Items(i).ToString().Substring(0, lstGroupList.Items(i).ToString().IndexOf("#")))
                                        dr("UserID") = sv_sUserName
                                        gv_dsGroupMember.Tables(0).Rows.Add(dr)
                                        gv_dsGroupMember.Tables(0).AcceptChanges()
                                    End If
                                Next
                            End If
                            If mv_bAdmin Then
                                Me.Close()
                            Else
                                If mv_bSuccess Then

                                    Dim sv_oNode As New TreeNode(sv_sUserName)
                                    With sv_oNode
                                        .Tag = "LEAFUSER#"
                                        .SelectedImageIndex = ImageIndex.NodeUser
                                        .ImageIndex = ImageIndex.NodeUser
                                        .ForeColor = Color.Navy
                                        .NodeFont = New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
                                    End With

                                    'Thêm vào DataSet chức năng này
                                    gv_oMainForm.tvwAdminSystem.SelectedNode.Nodes.Add(sv_oNode)
                                    gv_oMainForm.tvwAdminSystem.SelectedNode.ExpandAll()
                                End If
                                If gv_bCloseFormAfterDML Then
                                    Me.Close()
                                Else
                                    txtUserName.Text = ""
                                    txtFullName.Text = ""
                                    txtDepart.Text = ""
                                    txtDesc.Text = ""
                                    txtUserName.Focus()
                                End If
                            End If
                        Else
                            MessageBox.Show("Đã tồn tại " & IIf(mv_bAdmin, " quản trị", " người dùng") & " có UserName như vậy", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            txtUserName.Focus()
                            mv_bSuccess = False
                        End If
                End Select
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            mv_bSuccess = False
            Return
        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub InsertUser_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Enter
                    ProcessTabKey(True)
                Case Keys.Escape
                    Me.Close()
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdRemoveMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveGroup.Click
        Try
            If lstGroupList.Items.Count > 0 AndAlso Not IsNothing(lstGroupList.SelectedItem) Then
                Dim mv_GroupID As Integer = lstGroupList.SelectedItem.ToString.Substring(0, lstGroupList.SelectedItem.ToString.IndexOf("#"))
                Dim objUser As New clsUser()
                'If objUser.DeleteMemberOfGroup(mv_GroupID, mv_sUserName) Then
                lstGroupList.Items.RemoveAt(lstGroupList.SelectedIndex)
                'Xóa khỏi Dataset
                'For Each dr As DataRow In gv_dsGroupMember.Tables(0).Rows
                '    If CInt(dr("GroupID")) = mv_GroupID AndAlso dr("UserID").ToString.ToUpper = mv_sUserName.ToUpper Then
                '        gv_dsGroupMember.Tables(0).Rows.Remove(dr)
                '        gv_dsGroupMember.Tables(0).AcceptChanges()
                '        Exit For
                '    End If
                'Next
                'End If
                cmdRemoveGroup.Enabled = lstGroupList.Items.Count > 0
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdAddGroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddGroup.Click
        Dim SU As New SelectGroup
        SU.CurrGroupList = CurrGroupList
        SU.ShowDialog()
        If Not SU.bCancel Then
            CurrGroupList &= SU.NewCurrGL
            Dim arrUL As String() = SU.NewCurrGL.Split(",")
            For i As Integer = 0 To arrUL.Length - 1
                If arrUL(i).Trim <> "" Then
                    lstGroupList.Items.Add(arrUL(i))
                End If
            Next
        End If
    End Sub

    Private Sub cmdAddDelegate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddDelegate.Click
        Dim SU As New SelectUser("Main User: " & mv_sUserName)
        CurrDelegateList &= "," & mv_sUserName
        SU.CurrUserList = CurrDelegateList
        SU.ShowDialog()
        If Not SU.bCancel Then
            CurrDelegateList &= SU.NewCurrUL
            CurrDelegateList &= "," & mv_sUserName
            Dim arrUL As String() = SU.NewCurrUL.Split(",")
            For i As Integer = 0 To arrUL.Length - 1
                If arrUL(i).Trim <> "" Then
                    lstDelegate.Items.Add(arrUL(i))
                End If
            Next
        End If
    End Sub

    Private Sub cmdRemoveDelegate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveDelegate.Click
        Try
            If lstDelegate.Items.Count > 0 AndAlso Not IsNothing(lstDelegate.SelectedItem) Then
                lstDelegate.Items.RemoveAt(lstDelegate.SelectedIndex)
                cmdRemoveDelegate.Enabled = lstDelegate.Items.Count > 0
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
