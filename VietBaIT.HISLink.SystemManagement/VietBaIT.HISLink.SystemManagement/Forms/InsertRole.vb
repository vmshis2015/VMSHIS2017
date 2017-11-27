Imports System.IO
Public Class InsertRole
    Inherits System.Windows.Forms.Form
    Dim mv_iParentRoleID As Integer
    Dim mv_sRoleName As String
    Dim mv_iRoleID As Integer
    Dim mv_bSuccess As Boolean = False
    Dim mv_iStatus As Status
    Public mv_iOrder As Integer
    Public mv_currOrder As Integer
    Public mv_intShortCutKey As Integer = -1
    Public mv_sIconPath As String = ""
    Public mv_bMenuLevel1 As Boolean = False
    Friend WithEvents chkIsTabview As System.Windows.Forms.CheckBox
    Friend WithEvents txtParamList As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkMultiview As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
    Public mv_bChangeIconPath As Boolean = False

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub
    Public Sub New(ByVal pv_sTitle As String)
        MyBase.New()
        Me.Text = pv_sTitle
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
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents txtOrder As System.Windows.Forms.TextBox
    Friend WithEvents txtRoleName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtEngRoleName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CboName As System.Windows.Forms.ComboBox
    Friend WithEvents CboValue As System.Windows.Forms.ComboBox
    Friend WithEvents txtIconPath As System.Windows.Forms.TextBox
    Friend WithEvents picIcon1 As System.Windows.Forms.PictureBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(InsertRole))
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtIconPath = New System.Windows.Forms.TextBox()
        Me.CboValue = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.picIcon1 = New System.Windows.Forms.PictureBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CboName = New System.Windows.Forms.ComboBox()
        Me.txtEngRoleName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.txtOrder = New System.Windows.Forms.TextBox()
        Me.txtRoleName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.chkIsTabview = New System.Windows.Forms.CheckBox()
        Me.txtParamList = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chkMultiview = New System.Windows.Forms.CheckBox()
        Me.chkEnabled = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.picIcon1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSave.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSave.Location = New System.Drawing.Point(251, 270)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(80, 25)
        Me.cmdSave.TabIndex = 6
        Me.cmdSave.Text = "&Ghi"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkMultiview)
        Me.GroupBox1.Controls.Add(Me.chkEnabled)
        Me.GroupBox1.Controls.Add(Me.chkIsTabview)
        Me.GroupBox1.Controls.Add(Me.txtParamList)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtIconPath)
        Me.GroupBox1.Controls.Add(Me.CboValue)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.picIcon1)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.CboName)
        Me.GroupBox1.Controls.Add(Me.txtEngRoleName)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtDesc)
        Me.GroupBox1.Controls.Add(Me.txtOrder)
        Me.GroupBox1.Controls.Add(Me.txtRoleName)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(412, 260)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Thông tin Role"
        '
        'txtIconPath
        '
        Me.txtIconPath.Location = New System.Drawing.Point(7, 165)
        Me.txtIconPath.Name = "txtIconPath"
        Me.txtIconPath.Size = New System.Drawing.Size(8, 22)
        Me.txtIconPath.TabIndex = 39
        Me.txtIconPath.Visible = False
        '
        'CboValue
        '
        Me.CboValue.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboValue.Location = New System.Drawing.Point(252, 75)
        Me.CboValue.Name = "CboValue"
        Me.CboValue.Size = New System.Drawing.Size(139, 24)
        Me.CboValue.TabIndex = 38
        Me.CboValue.Visible = False
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(261, 100)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(76, 23)
        Me.Label7.TabIndex = 37
        Me.Label7.Text = "Biểu tượng"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'picIcon1
        '
        Me.picIcon1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picIcon1.Location = New System.Drawing.Point(344, 100)
        Me.picIcon1.Name = "picIcon1"
        Me.picIcon1.Size = New System.Drawing.Size(28, 24)
        Me.picIcon1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picIcon1.TabIndex = 36
        Me.picIcon1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picIcon1, "Click vào đây để chọn Icon cho chức năng")
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(7, 101)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(103, 23)
        Me.Label6.TabIndex = 35
        Me.Label6.Text = "Phím tắt"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CboName
        '
        Me.CboName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboName.Location = New System.Drawing.Point(113, 101)
        Me.CboName.Name = "CboName"
        Me.CboName.Size = New System.Drawing.Size(105, 24)
        Me.CboName.Sorted = True
        Me.CboName.TabIndex = 3
        '
        'txtEngRoleName
        '
        Me.txtEngRoleName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEngRoleName.Location = New System.Drawing.Point(113, 49)
        Me.txtEngRoleName.MaxLength = 100
        Me.txtEngRoleName.Name = "txtEngRoleName"
        Me.txtEngRoleName.Size = New System.Drawing.Size(282, 22)
        Me.txtEngRoleName.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 49)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(103, 16)
        Me.Label3.TabIndex = 33
        Me.Label3.Text = "Tên tiếng Anh"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDesc
        '
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.Location = New System.Drawing.Point(109, 165)
        Me.txtDesc.MaxLength = 255
        Me.txtDesc.Multiline = True
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDesc.Size = New System.Drawing.Size(282, 60)
        Me.txtDesc.TabIndex = 5
        '
        'txtOrder
        '
        Me.txtOrder.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtOrder.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOrder.Location = New System.Drawing.Point(113, 74)
        Me.txtOrder.Name = "txtOrder"
        Me.txtOrder.Size = New System.Drawing.Size(103, 22)
        Me.txtOrder.TabIndex = 2
        Me.txtOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRoleName
        '
        Me.txtRoleName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoleName.Location = New System.Drawing.Point(113, 24)
        Me.txtRoleName.MaxLength = 100
        Me.txtRoleName.Name = "txtRoleName"
        Me.txtRoleName.Size = New System.Drawing.Size(282, 22)
        Me.txtRoleName.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(4, 165)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(101, 16)
        Me.Label4.TabIndex = 31
        Me.Label4.Text = "Mô tả thêm"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 74)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(103, 16)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Số thứ tự"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(7, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 16)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Tên tiếng Việt"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdClose.Location = New System.Drawing.Point(336, 270)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(80, 25)
        Me.cmdClose.TabIndex = 7
        Me.cmdClose.Text = "Th&oát"
        '
        'chkIsTabview
        '
        Me.chkIsTabview.AutoSize = True
        Me.chkIsTabview.Checked = True
        Me.chkIsTabview.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIsTabview.Location = New System.Drawing.Point(200, 231)
        Me.chkIsTabview.Name = "chkIsTabview"
        Me.chkIsTabview.Size = New System.Drawing.Size(75, 20)
        Me.chkIsTabview.TabIndex = 18
        Me.chkIsTabview.TabStop = False
        Me.chkIsTabview.Text = "TabView"
        Me.chkIsTabview.UseVisualStyleBackColor = True
        '
        'txtParamList
        '
        Me.txtParamList.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtParamList.Location = New System.Drawing.Point(109, 131)
        Me.txtParamList.MaxLength = 100
        Me.txtParamList.Name = "txtParamList"
        Me.txtParamList.Size = New System.Drawing.Size(282, 22)
        Me.txtParamList.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(5, 133)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(101, 16)
        Me.Label5.TabIndex = 41
        Me.Label5.Text = "Tham số"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkMultiview
        '
        Me.chkMultiview.AutoSize = True
        Me.chkMultiview.Checked = True
        Me.chkMultiview.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMultiview.Location = New System.Drawing.Point(293, 231)
        Me.chkMultiview.Name = "chkMultiview"
        Me.chkMultiview.Size = New System.Drawing.Size(79, 20)
        Me.chkMultiview.TabIndex = 19
        Me.chkMultiview.TabStop = False
        Me.chkMultiview.Text = "Multiview"
        Me.chkMultiview.UseVisualStyleBackColor = True
        '
        'chkEnabled
        '
        Me.chkEnabled.AutoSize = True
        Me.chkEnabled.Checked = True
        Me.chkEnabled.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkEnabled.Location = New System.Drawing.Point(113, 231)
        Me.chkEnabled.Name = "chkEnabled"
        Me.chkEnabled.Size = New System.Drawing.Size(81, 20)
        Me.chkEnabled.TabIndex = 42
        Me.chkEnabled.TabStop = False
        Me.chkEnabled.Text = "Enabled?"
        Me.chkEnabled.UseVisualStyleBackColor = True
        '
        'InsertRole
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(428, 306)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "InsertRole"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Thêm quyền"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.picIcon1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Properties"
    Public Property pi_Order() As Status
        Get
            Return mv_iOrder
        End Get
        Set(ByVal Value As Status)
            mv_iOrder = Value
        End Set
    End Property
    Public Property pi_Status() As Status
        Get
            Return mv_iStatus
        End Get
        Set(ByVal Value As Status)
            mv_iStatus = Value
        End Set
    End Property
    Public Property ps_RoleName() As String
        Get
            Return mv_sRoleName
        End Get
        Set(ByVal Value As String)
            mv_sRoleName = Value
        End Set
    End Property
    Public Property pi_ParentRoleID() As Integer
        Get
            Return mv_iParentRoleID
        End Get
        Set(ByVal Value As Integer)
            mv_iParentRoleID = Value
        End Set
    End Property
    Public Property pi_RoleID() As Integer
        Get
            Return mv_iRoleID
        End Get
        Set(ByVal Value As Integer)
            mv_iRoleID = Value
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

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            mv_bSuccess = False
            If mf_bCheckValidData() Then
                Dim sv_sRoleName As String
                Dim sv_sEngRoleName As String
                Dim sv_iOrder As Integer
                Dim sv_sDesc As String
                Dim sv_sParamList As String
                Dim sv_oRole As New clsRole
                sv_sRoleName = ValidData(txtRoleName.Text)
                sv_sEngRoleName = ValidData(txtEngRoleName.Text)
                sv_iOrder = CInt(txtOrder.Text.Trim)
                sv_sParamList = txtParamList.Text.Trim.Replace("'", "''")
                sv_sDesc = ValidData(txtDesc.Text)
                mv_sRoleName = sv_sRoleName
                Try
                    Select Case mv_iStatus
                        Case globalModule.Status.InsertSubSystemNode
                            If sv_oRole.InsertSubSystemRole(sv_sRoleName, sv_sEngRoleName, sv_iOrder, CInt(CboValue.SelectedItem), txtIconPath.Text.Trim, gv_sDefaultImgPathForSubSystem, sv_sDesc, IIf(chkIsTabview.Checked, 1, 0), IIf(chkMultiview.Checked, 1, 0), IIf(chkEnabled.Checked, 1, 0)) Then
                                mv_bSuccess = True
                                mv_iRoleID = sv_oRole.iGetNewestRole
                                Dim sv_oDR As DataRow
                                sv_oDR = gv_dsRole.Tables(0).NewRow
                                With sv_oDR
                                    .Item("iRole") = mv_iRoleID
                                    .Item("FP_sBranchID") = gv_sBranchID
                                    .Item("iParentRole") = -2
                                    .Item("sRoleName") = sv_sRoleName
                                    .Item("sEngRoleName") = sv_sEngRoleName
                                    .Item("iOrder") = sv_iOrder
                                    .Item("FK_iFunctionID") = -1
                                    .Item("sDesc") = sv_sDesc
                                    .Item("intShortCutKey") = CInt(CboValue.SelectedItem)
                                    .Item("sImgPath") = gv_sDefaultImgPathForSubSystem
                                    .Item("sIconPath") = txtIconPath.Text.Trim
                                End With
                                gv_dsRole.Tables(0).Rows.Add(sv_oDR)
                                gv_dsRole.Tables(0).AcceptChanges()
                                If mv_bSuccess Then
                                    Dim sv_oNode As New TreeNode(sv_sRoleName)
                                    With sv_oNode
                                        .Tag = "LEAFROLES|-2#" & mv_iRoleID.ToString
                                        If bIsValidPath(txtIconPath.Text.Trim) Then
                                            gv_oMainForm.tvwAdminSystem.ImageList.Images.Add(Image.FromFile(txtIconPath.Text.Trim))
                                            .SelectedImageIndex = gv_oMainForm.tvwAdminSystem.ImageList.Images.Count - 1
                                            .ImageIndex = gv_oMainForm.tvwAdminSystem.ImageList.Images.Count - 1
                                        Else
                                            .SelectedImageIndex = ImageIndex.NodeRole
                                            .ImageIndex = ImageIndex.NodeRole
                                        End If
                                        .NodeFont = New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
                                    End With
                                    gv_oMainForm.tvwAdminSystem.SelectedNode.Nodes.Add(sv_oNode)
                                    gv_oMainForm.tvwAdminSystem.SelectedNode.Expand()
                                    txtRoleName.Text = ""
                                    txtEngRoleName.Text = ""
                                    txtOrder.Text = (CInt(txtOrder.Text) + 1).ToString
                                    txtDesc.Text = ""
                                    txtRoleName.Focus()
                                End If
                                If gv_bAnnounceAfterInsertingSuccessfully Then
                                    MessageBox.Show(IIf(mv_iStatus = 1, "Thêm mới Role thành công!", "Cập nhật Role thành công").ToString, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                                If gv_bCloseFormAfterDML Then
                                    Me.Close()
                                Else
                                    If gv_sDefaultIconPathForRole <> Nothing AndAlso Not gv_sDefaultIconPathForSubSystem.Trim.Equals(String.Empty) And File.Exists(gv_sDefaultIconPathForSubSystem) Then
                                        txtIconPath.Text = gv_sDefaultIconPathForSubSystem
                                        picIcon1.Image = Image.FromFile(gv_sDefaultIconPathForSubSystem)
                                    End If
                                End If
                            End If
                        Case globalModule.Status.Insert
                            If sv_oRole.InsertRole(mv_iParentRoleID, sv_sRoleName, sv_sEngRoleName, sv_iOrder, CInt(CboValue.SelectedItem), txtIconPath.Text.Trim, sv_sDesc, sv_sParamList, IIf(chkIsTabview.Checked, 1, 0), IIf(chkMultiview.Checked, 1, 0), IIf(chkEnabled.Checked, 1, 0)) Then
                                mv_bSuccess = True
                                mv_iRoleID = sv_oRole.iGetNewestRole
                                Dim sv_oDR As DataRow
                                sv_oDR = gv_dsRole.Tables(0).NewRow
                                With sv_oDR
                                    .Item("FK_iFunctionID") = -1
                                    .Item("FP_sBranchID") = gv_sBranchID
                                    .Item("iRole") = mv_iRoleID
                                    .Item("iParentRole") = mv_iParentRoleID
                                    .Item("sRoleName") = sv_sRoleName
                                    .Item("sParameterList") = sv_sParamList
                                    .Item("bEnabled") = IIf(chkEnabled.Checked, 1, 0)
                                    .Item("isTabView") = IIf(chkIsTabview.Checked, 1, 0)
                                    .Item("ismultiView") = IIf(chkMultiview.Checked, 1, 0)
                                    .Item("sEngRoleName") = sv_sEngRoleName
                                    .Item("iOrder") = sv_iOrder
                                    .Item("sDesc") = sv_sDesc
                                    .Item("sImgPath") = gv_sDefaultImgPathForSubSystem
                                    .Item("intShortCutKey") = CInt(CboValue.SelectedItem)
                                    .Item("sIconPath") = txtIconPath.Text.Trim
                                End With
                                gv_dsRole.Tables(0).Rows.Add(sv_oDR)
                                gv_dsRole.Tables(0).AcceptChanges()
                                If mv_bSuccess Then
                                    Dim sv_oNode As New TreeNode(sv_sRoleName)
                                    With sv_oNode
                                        .Tag = "LEAFROLES|-1#" & mv_iRoleID.ToString
                                        .SelectedImageIndex = ImageIndex.LeafRole
                                        .ImageIndex = ImageIndex.LeafRole
                                        .NodeFont = New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
                                    End With
                                    gv_oMainForm.tvwAdminSystem.SelectedNode.Nodes.Add(sv_oNode)
                                    gv_oMainForm.tvwAdminSystem.SelectedNode.ExpandAll()
                                End If
                                txtRoleName.Text = ""
                                txtEngRoleName.Text = ""
                                txtOrder.Text = (CInt(txtOrder.Text) + 1).ToString
                                txtDesc.Text = ""
                                txtParamList.Clear()
                                txtRoleName.Focus()
                                If gv_bAnnounceAfterInsertingSuccessfully Then
                                    MessageBox.Show(IIf(mv_iStatus = 1, "Thêm mới Role thành công!", "Cập nhật Role thành công").ToString, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                                If gv_bCloseFormAfterDML Then
                                    Me.Close()
                                Else
                                    If gv_sDefaultIconPathForRole <> Nothing AndAlso Not gv_sDefaultIconPathForRole.Trim.Equals(String.Empty) And File.Exists(gv_sDefaultIconPathForRole) Then
                                        txtIconPath.Text = gv_sDefaultIconPathForRole
                                        picIcon1.Image = Image.FromFile(gv_sDefaultIconPathForRole)
                                    End If
                                End If
                            End If
                        Case globalModule.Status.Update
                            If sv_oRole.UpdateRole(mv_iRoleID, sv_sRoleName, sv_sEngRoleName, sv_iOrder, CInt(CboValue.SelectedItem), txtIconPath.Text.Trim, sv_sDesc, sv_sParamList, IIf(chkIsTabview.Checked, 1, 0), IIf(chkMultiview.Checked, 1, 0), IIf(chkEnabled.Checked, 1, 0)) Then
                                mv_bSuccess = True
                                Dim sv_oDR As DataRow
                                For Each sv_oDR In gv_dsRole.Tables(0).Rows
                                    If sv_oDR.Item("iRole") = mv_iRoleID Then
                                        With sv_oDR
                                            .Item("sRoleName") = sv_sRoleName
                                            .Item("sEngRoleName") = sv_sEngRoleName
                                            .Item("iOrder") = sv_iOrder
                                            .Item("sDesc") = sv_sDesc
                                            .Item("bEnabled") = IIf(chkEnabled.Checked, 1, 0)
                                            .Item("sParameterList") = sv_sParamList
                                            .Item("isTabView") = IIf(chkIsTabview.Checked, 1, 0)
                                            .Item("ismultiView") = IIf(chkMultiview.Checked, 1, 0)
                                            .Item("intShortCutKey") = CInt(CboValue.SelectedItem)
                                            If .Item("sIconPath").ToString.Trim.ToUpper.Equals(txtIconPath.Text.Trim.ToUpper) Then
                                            Else
                                                mv_bChangeIconPath = True
                                            End If
                                            .Item("sIconPath") = txtIconPath.Text.Trim
                                        End With
                                        gv_dsRole.Tables(0).AcceptChanges()
                                        Exit For
                                    End If
                                Next
                                mv_iOrder = sv_iOrder
                                If gv_bAnnounceAfterUpdatingSuccessfully Then
                                    MessageBox.Show(IIf(mv_iStatus = 1, "Thêm mới Role thành công!", "Cập nhật Role thành công").ToString, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                                Me.Close()
                            End If
                    End Select

                Catch ex As Exception
                    MessageBox.Show("Có lỗi khi thêm Role mới. Liên hệ với người lập trình", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Try
                mv_intShortCutKey = CInt(CboValue.SelectedItem)
                mv_sIconPath = txtIconPath.Text.Trim
            End If
        Catch ex As Exception
            mv_bSuccess = False
        End Try
    End Sub
    Public Function mf_bCheckValidData() As Boolean
        Try
            If txtRoleName.Text.Trim.Equals(String.Empty) Then
                MessageBox.Show("Bạn phải nhập tên Role", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtRoleName.Focus()
                Return False
            End If
            If txtOrder.Text.Trim.Equals(String.Empty) Then
                MessageBox.Show("Bạn phải nhập số thứ tự", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtOrder.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show("Lỗi cần gặp người lập trình", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End Try
    End Function

    Private Sub InsertRole_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            mv_bChangeIconPath = False
            mv_bSuccess = False
            getShortCutKey()
            Select Case mv_iStatus
                Case globalModule.Status.Update
                    'Me.Text = "Cập nhật Role"
                    If mv_bMenuLevel1 Then
                        Label6.Enabled = False
                        CboName.Enabled = False
                    Else
                        Label6.Enabled = True
                        CboName.Enabled = True
                    End If
                    GetDataForUpdate()
                Case Else
                    Me.Text = "Thêm Role"
                    txtOrder.Text = mv_iOrder
                    txtOrder.Enabled = False
                    Dim sv_bHasShortCutKey As Boolean = False
                    For i As Integer = 0 To CboValue.Items.Count - 1
                        If CboValue.Items(i).ToString.Trim = "0" Then
                            CboValue.SelectedIndex = i
                            sv_bHasShortCutKey = True
                            Exit For
                        End If
                    Next
                    If Not sv_bHasShortCutKey Then
                        CboValue.SelectedIndex = -1
                    End If
                    Select Case mv_iStatus
                        Case globalModule.Status.InsertSubSystemNode
                            Label6.Enabled = False
                            CboName.Enabled = False
                            If Not gv_sDefaultIconPathForSubSystem.Trim.Equals(String.Empty) And File.Exists(gv_sDefaultIconPathForSubSystem) Then
                                txtIconPath.Text = gv_sDefaultIconPathForSubSystem
                                picIcon1.Image = Image.FromFile(gv_sDefaultIconPathForSubSystem)
                            End If

                        Case globalModule.Status.Insert
                            If mv_bMenuLevel1 Then
                                Label6.Enabled = False
                                CboName.Enabled = False
                            Else
                                Label6.Enabled = True
                                CboName.Enabled = True
                            End If
                            If Not gv_sDefaultIconPathForRole.Trim.Equals(String.Empty) And File.Exists(gv_sDefaultIconPathForRole) Then
                                txtIconPath.Text = gv_sDefaultIconPathForRole
                                picIcon1.Image = Image.FromFile(gv_sDefaultIconPathForRole)
                            End If
                    End Select

            End Select
            txtRoleName.Focus()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub CboName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboValue.SelectedIndexChanged, CboName.SelectedIndexChanged
        If CboValue.Items.Count = CboName.Items.Count Then
            If sender.Equals(CboName) Then
                CboValue.SelectedIndex = CboName.SelectedIndex
            Else
                CboName.SelectedIndex = CboValue.SelectedIndex
            End If
        End If
    End Sub

    Private Sub getShortCutKey()
        Dim en As Type
        Try
            en = GetType(Shortcut)
            Dim i As Integer = 0
            For Each s As String In [Enum].GetNames(en)
                CboName.Items.Add(s & "-" & [Enum].GetValues(en).GetValue(i))
                i += 1
            Next
            For j As Integer = 0 To CboName.Items.Count - 1
                CboValue.Items.Add(CboName.Items(j).ToString.Substring(CboName.Items(j).ToString.IndexOf("-") + 1))
                CboName.Items(j) = CboName.Items(j).ToString.Substring(0, CboName.Items(j).ToString.IndexOf("-"))
            Next
        Catch ex As Exception
        End Try
    End Sub
    Private Sub GetDataForUpdate()
        Dim sv_ds As New DataSet
        Dim sv_oRole As New clsRole
        Try
            sv_ds = sv_oRole.dsGetRoleInfor(mv_iRoleID)
            If Not sv_ds Is Nothing Then
                txtRoleName.Text = IIF_VN(sv_ds.Tables(0).Rows(0)("sRoleName"))
                txtEngRoleName.Text = IIF_VN(sv_ds.Tables(0).Rows(0)("sEngRoleName"))
                txtOrder.Text = IIF_VN(sv_ds.Tables(0).Rows(0)("iOrder"))
                txtParamList.Text = sv_ds.Tables(0).Rows(0)("sParameterList")
                mv_currOrder = CInt(txtOrder.Text.Trim)
                chkIsTabview.Checked = VNS.Libs.Utility.Int32Dbnull(sv_ds.Tables(0).Rows(0)("IsTabview"), 0) = 1
                chkMultiview.Checked = VNS.Libs.Utility.Int32Dbnull(sv_ds.Tables(0).Rows(0)("isMultiview"), 0) = 1
                chkEnabled.Checked = VNS.Libs.Utility.Int32Dbnull(sv_ds.Tables(0).Rows(0)("bEnabled"), 0) = 1
                txtDesc.Text = IIF_VN(sv_ds.Tables(0).Rows(0)("sDesc"))
                Dim sv_bHasShortCutKey As Boolean = False
                For i As Integer = 0 To CboValue.Items.Count - 1
                    If CboValue.Items(i).ToString.Trim = CStr(IIF_VN(sv_ds.Tables(0).Rows(0).Item("intShortCutKey"))) Then
                        CboValue.SelectedIndex = i
                        sv_bHasShortCutKey = True
                        Exit For
                    End If
                Next
                If Not sv_bHasShortCutKey Then
                    CboValue.SelectedIndex = -1
                End If
                txtIconPath.Text = IIF_VN(sv_ds.Tables(0).Rows(0).Item("sIconPath"))
                If Not IIF_VN(sv_ds.Tables(0).Rows(0).Item("sIconPath")).Trim.ToUpper.Equals("UNKNOWN") And Not IsDBNull(sv_ds.Tables(0).Rows(0).Item("sIconPath")) And System.IO.File.Exists(IIF_VN(sv_ds.Tables(0).Rows(0).Item("sIconPath"))) Then
                    picIcon1.Image = Image.FromFile(sv_ds.Tables(0).Rows(0).Item("sIconPath"))
                Else
                    picIcon1.Image = Nothing
                End If
            Else
                MessageBox.Show("Lỗi lấy thông tin để cập nhật Role. Liên hệ với người lập trình", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        mv_bSuccess = False
        Me.Close()
    End Sub

    Private Sub _KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Try
            If Asc(e.KeyChar) = Keys.Enter Then e.Handled = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub InsertRole_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub picIcon_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picIcon1.MouseDown
        Dim openDiag As New OpenFileDialog
        Try
            If e.Button = MouseButtons.Left Then
                openDiag.Title = "Chọn biểu tượng cho Menu"
                openDiag.Filter = "Icon|*.ico|Gif|*.gif|Jpg|*.Jpg|Bmp|*.Bmp|PNG|*.PNG"
                If openDiag.ShowDialog = DialogResult.OK Then
                    txtIconPath.Text = openDiag.FileName
                    picIcon1.Image = Image.FromFile(openDiag.FileName)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Icon bạn vừa chọn không sử dụng được. Mời bạn chọn lại!", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
End Class
