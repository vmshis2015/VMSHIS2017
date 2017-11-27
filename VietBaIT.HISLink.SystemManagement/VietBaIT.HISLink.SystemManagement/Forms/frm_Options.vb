Imports System.IO
Public Class frm_Options
    Inherits System.Windows.Forms.Form
    Public mv_ctx As New ContextMenu
    Private mv_bLockFunctionColorHasChanged As Boolean = False
    Private mv_bLockParamColorHasChanged As Boolean = False
    Private mv_bLockedFunctionColor As Color
    Private mv_bLockedParamColor As Color

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
    Friend WithEvents pnlUser As System.Windows.Forms.Panel
    Friend WithEvents pnlFunction As System.Windows.Forms.Panel
    Friend WithEvents pnlRole As System.Windows.Forms.Panel
    Friend WithEvents pnlParameter As System.Windows.Forms.Panel
    Friend WithEvents pnlOption As System.Windows.Forms.Panel
    Friend WithEvents tvwOption As System.Windows.Forms.TreeView
    Friend WithEvents imgAdminnistration As System.Windows.Forms.ImageList
    Friend WithEvents pnlOther As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents chkCannotDeleteUID As System.Windows.Forms.CheckBox
    Friend WithEvents chkCannotDeletePWDOfUIDs As System.Windows.Forms.CheckBox
    Friend WithEvents chkCannotDeletePWD As System.Windows.Forms.CheckBox
    Friend WithEvents optDeleteBeforeMixed As System.Windows.Forms.RadioButton
    Friend WithEvents optMixedRole As System.Windows.Forms.RadioButton
    Friend WithEvents chkCanDblClicktoGetRole As System.Windows.Forms.CheckBox
    Friend WithEvents cboRoleLevel As System.Windows.Forms.ComboBox
    Friend WithEvents txtDefaultIconPathForRole As System.Windows.Forms.TextBox
    Friend WithEvents txtDefaultIconPathForSubSystem As System.Windows.Forms.TextBox
    Friend WithEvents txtDefaultImgPathForSubSystem As System.Windows.Forms.TextBox
    Friend WithEvents chkAskingBeforeDeleting As System.Windows.Forms.CheckBox
    Friend WithEvents chkCloseFormAfterDML As System.Windows.Forms.CheckBox
    Friend WithEvents chkAnnounceAfterDeletingSuccessfully As System.Windows.Forms.CheckBox
    Friend WithEvents chkAnnounceAfterUpdatetingSuccessfully As System.Windows.Forms.CheckBox
    Friend WithEvents chkAnnounceAfterInsertingSuccessfully As System.Windows.Forms.CheckBox
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdgetDefaultIcoForRole As System.Windows.Forms.Button
    Friend WithEvents cmdGetDefaultIcon As System.Windows.Forms.Button
    Friend WithEvents cmdGetDefaultImg As System.Windows.Forms.Button
    Friend WithEvents picDefaultImg As System.Windows.Forms.PictureBox
    Friend WithEvents picDefaultIco As System.Windows.Forms.PictureBox
    Friend WithEvents picDefaultIcoForRole As System.Windows.Forms.PictureBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents GroupBox11 As System.Windows.Forms.GroupBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblLockedParamColor As System.Windows.Forms.Label
    Friend WithEvents lbl1 As System.Windows.Forms.Label
    Friend WithEvents lblLockedFunctionColor As System.Windows.Forms.Label
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents chkAnnounceAfterActivatingFunction As System.Windows.Forms.CheckBox
    Friend WithEvents chkAnnounceAfterLockingFunction As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox12 As System.Windows.Forms.GroupBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents chkAnnounceAfterLockingParam As System.Windows.Forms.CheckBox
    Friend WithEvents chkAnnounceAfterActivatingParam As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox13 As System.Windows.Forms.GroupBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents chkEnableDragAndDrop As System.Windows.Forms.CheckBox
    Friend WithEvents chkAnnounceBeforeDroppingRole As System.Windows.Forms.CheckBox
    Friend WithEvents pnlOutIN As System.Windows.Forms.Panel
    Friend WithEvents GroupBox14 As System.Windows.Forms.GroupBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents GroupBox15 As System.Windows.Forms.GroupBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents pnlUpdateVersion As System.Windows.Forms.Panel
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents GroupBox16 As System.Windows.Forms.GroupBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents chkCannotDeleteFunction As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frm_Options))
        Me.cmdSave = New System.Windows.Forms.Button
        Me.tvwOption = New System.Windows.Forms.TreeView
        Me.imgAdminnistration = New System.Windows.Forms.ImageList(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.pnlUser = New System.Windows.Forms.Panel
        Me.chkCannotDeleteUID = New System.Windows.Forms.CheckBox
        Me.chkCannotDeletePWDOfUIDs = New System.Windows.Forms.CheckBox
        Me.chkCannotDeletePWD = New System.Windows.Forms.CheckBox
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.optDeleteBeforeMixed = New System.Windows.Forms.RadioButton
        Me.optMixedRole = New System.Windows.Forms.RadioButton
        Me.chkCanDblClicktoGetRole = New System.Windows.Forms.CheckBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.pnlFunction = New System.Windows.Forms.Panel
        Me.chkCannotDeleteFunction = New System.Windows.Forms.CheckBox
        Me.chkAnnounceAfterLockingFunction = New System.Windows.Forms.CheckBox
        Me.chkAnnounceAfterActivatingFunction = New System.Windows.Forms.CheckBox
        Me.GroupBox10 = New System.Windows.Forms.GroupBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.lblLockedFunctionColor = New System.Windows.Forms.Label
        Me.GroupBox9 = New System.Windows.Forms.GroupBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.pnlRole = New System.Windows.Forms.Panel
        Me.chkAnnounceBeforeDroppingRole = New System.Windows.Forms.CheckBox
        Me.chkEnableDragAndDrop = New System.Windows.Forms.CheckBox
        Me.GroupBox13 = New System.Windows.Forms.GroupBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.picDefaultIcoForRole = New System.Windows.Forms.PictureBox
        Me.picDefaultIco = New System.Windows.Forms.PictureBox
        Me.picDefaultImg = New System.Windows.Forms.PictureBox
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.cboRoleLevel = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.cmdgetDefaultIcoForRole = New System.Windows.Forms.Button
        Me.txtDefaultIconPathForRole = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.cmdGetDefaultIcon = New System.Windows.Forms.Button
        Me.txtDefaultIconPathForSubSystem = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.cmdGetDefaultImg = New System.Windows.Forms.Button
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtDefaultImgPathForSubSystem = New System.Windows.Forms.TextBox
        Me.pnlParameter = New System.Windows.Forms.Panel
        Me.chkAnnounceAfterLockingParam = New System.Windows.Forms.CheckBox
        Me.chkAnnounceAfterActivatingParam = New System.Windows.Forms.CheckBox
        Me.GroupBox12 = New System.Windows.Forms.GroupBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.lblLockedParamColor = New System.Windows.Forms.Label
        Me.GroupBox11 = New System.Windows.Forms.GroupBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.lbl1 = New System.Windows.Forms.Label
        Me.pnlOther = New System.Windows.Forms.Panel
        Me.chkAskingBeforeDeleting = New System.Windows.Forms.CheckBox
        Me.chkCloseFormAfterDML = New System.Windows.Forms.CheckBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkAnnounceAfterDeletingSuccessfully = New System.Windows.Forms.CheckBox
        Me.chkAnnounceAfterUpdatetingSuccessfully = New System.Windows.Forms.CheckBox
        Me.chkAnnounceAfterInsertingSuccessfully = New System.Windows.Forms.CheckBox
        Me.pnlOption = New System.Windows.Forms.Panel
        Me.pnlOutIN = New System.Windows.Forms.Panel
        Me.RadioButton2 = New System.Windows.Forms.RadioButton
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.GroupBox14 = New System.Windows.Forms.GroupBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Button3 = New System.Windows.Forms.Button
        Me.GroupBox15 = New System.Windows.Forms.GroupBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.pnlUpdateVersion = New System.Windows.Forms.Panel
        Me.CheckBox3 = New System.Windows.Forms.CheckBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown
        Me.Label19 = New System.Windows.Forms.Label
        Me.CheckBox2 = New System.Windows.Forms.CheckBox
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.GroupBox16 = New System.Windows.Forms.GroupBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdClose = New System.Windows.Forms.Button
        Me.pnlUser.SuspendLayout()
        Me.pnlFunction.SuspendLayout()
        Me.pnlRole.SuspendLayout()
        Me.pnlParameter.SuspendLayout()
        Me.pnlOther.SuspendLayout()
        Me.pnlOption.SuspendLayout()
        Me.pnlOutIN.SuspendLayout()
        Me.pnlUpdateVersion.SuspendLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSave.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSave.Location = New System.Drawing.Point(402, 381)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(93, 30)
        Me.cmdSave.TabIndex = 1
        Me.cmdSave.Text = "Chấp nhận"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tvwOption
        '
        Me.tvwOption.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tvwOption.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tvwOption.HideSelection = False
        Me.tvwOption.ImageList = Me.imgAdminnistration
        Me.tvwOption.Location = New System.Drawing.Point(3, 3)
        Me.tvwOption.Name = "tvwOption"
        Me.tvwOption.Size = New System.Drawing.Size(183, 363)
        Me.tvwOption.TabIndex = 3
        '
        'imgAdminnistration
        '
        Me.imgAdminnistration.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.imgAdminnistration.ImageSize = New System.Drawing.Size(16, 16)
        Me.imgAdminnistration.ImageStream = CType(resources.GetObject("imgAdminnistration.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgAdminnistration.TransparentColor = System.Drawing.Color.Transparent
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Location = New System.Drawing.Point(192, 363)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(414, 3)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        '
        'pnlUser
        '
        Me.pnlUser.Controls.Add(Me.chkCannotDeleteUID)
        Me.pnlUser.Controls.Add(Me.chkCannotDeletePWDOfUIDs)
        Me.pnlUser.Controls.Add(Me.chkCannotDeletePWD)
        Me.pnlUser.Controls.Add(Me.GroupBox5)
        Me.pnlUser.Controls.Add(Me.Label4)
        Me.pnlUser.Controls.Add(Me.optDeleteBeforeMixed)
        Me.pnlUser.Controls.Add(Me.optMixedRole)
        Me.pnlUser.Controls.Add(Me.chkCanDblClicktoGetRole)
        Me.pnlUser.Controls.Add(Me.GroupBox4)
        Me.pnlUser.Controls.Add(Me.Label3)
        Me.pnlUser.Location = New System.Drawing.Point(9, 66)
        Me.pnlUser.Name = "pnlUser"
        Me.pnlUser.Size = New System.Drawing.Size(96, 30)
        Me.pnlUser.TabIndex = 5
        '
        'chkCannotDeleteUID
        '
        Me.chkCannotDeleteUID.Checked = True
        Me.chkCannotDeleteUID.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCannotDeleteUID.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCannotDeleteUID.Location = New System.Drawing.Point(27, 102)
        Me.chkCannotDeleteUID.Name = "chkCannotDeleteUID"
        Me.chkCannotDeleteUID.Size = New System.Drawing.Size(345, 30)
        Me.chkCannotDeleteUID.TabIndex = 15
        Me.chkCannotDeleteUID.Text = "Cấm xóa người dùng"
        '
        'chkCannotDeletePWDOfUIDs
        '
        Me.chkCannotDeletePWDOfUIDs.Checked = True
        Me.chkCannotDeletePWDOfUIDs.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCannotDeletePWDOfUIDs.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCannotDeletePWDOfUIDs.Location = New System.Drawing.Point(27, 72)
        Me.chkCannotDeletePWDOfUIDs.Name = "chkCannotDeletePWDOfUIDs"
        Me.chkCannotDeletePWDOfUIDs.Size = New System.Drawing.Size(345, 30)
        Me.chkCannotDeletePWDOfUIDs.TabIndex = 14
        Me.chkCannotDeletePWDOfUIDs.Text = "Cấm xóa mật khẩu tất cả các người dùng"
        '
        'chkCannotDeletePWD
        '
        Me.chkCannotDeletePWD.Checked = True
        Me.chkCannotDeletePWD.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCannotDeletePWD.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCannotDeletePWD.Location = New System.Drawing.Point(27, 39)
        Me.chkCannotDeletePWD.Name = "chkCannotDeletePWD"
        Me.chkCannotDeletePWD.Size = New System.Drawing.Size(345, 30)
        Me.chkCannotDeletePWD.TabIndex = 13
        Me.chkCannotDeletePWD.Text = "Cấm xóa mật khẩu người dùng"
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.Location = New System.Drawing.Point(150, 18)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(6, 3)
        Me.GroupBox5.TabIndex = 12
        Me.GroupBox5.TabStop = False
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.Label4.Location = New System.Drawing.Point(15, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(144, 23)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Người dùng & mật khẩu"
        '
        'optDeleteBeforeMixed
        '
        Me.optDeleteBeforeMixed.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optDeleteBeforeMixed.Location = New System.Drawing.Point(27, 219)
        Me.optDeleteBeforeMixed.Name = "optDeleteBeforeMixed"
        Me.optDeleteBeforeMixed.Size = New System.Drawing.Size(345, 39)
        Me.optDeleteBeforeMixed.TabIndex = 10
        Me.optDeleteBeforeMixed.Text = "Xóa toàn bộ quyền cũ của người nhận trước khi lấy quyền từ người cho (Không nên d" & _
        "ùng)"
        '
        'optMixedRole
        '
        Me.optMixedRole.Checked = True
        Me.optMixedRole.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optMixedRole.Location = New System.Drawing.Point(27, 177)
        Me.optMixedRole.Name = "optMixedRole"
        Me.optMixedRole.Size = New System.Drawing.Size(345, 39)
        Me.optMixedRole.TabIndex = 9
        Me.optMixedRole.TabStop = True
        Me.optMixedRole.Text = "Trộn quyền lấy được từ người cho với quyền có sẵn của người nhận (Nên dùng)"
        '
        'chkCanDblClicktoGetRole
        '
        Me.chkCanDblClicktoGetRole.Checked = True
        Me.chkCanDblClicktoGetRole.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCanDblClicktoGetRole.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCanDblClicktoGetRole.Location = New System.Drawing.Point(27, 264)
        Me.chkCanDblClicktoGetRole.Name = "chkCanDblClicktoGetRole"
        Me.chkCanDblClicktoGetRole.Size = New System.Drawing.Size(345, 36)
        Me.chkCanDblClicktoGetRole.TabIndex = 8
        Me.chkCanDblClicktoGetRole.Text = "Cho phép gán quyền theo Role bằng cách nhấn đúp trên Role"
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Location = New System.Drawing.Point(81, 159)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(12, 3)
        Me.GroupBox4.TabIndex = 7
        Me.GroupBox4.TabStop = False
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.Label3.Location = New System.Drawing.Point(15, 150)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 23)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Gán quyền"
        '
        'pnlFunction
        '
        Me.pnlFunction.Controls.Add(Me.chkCannotDeleteFunction)
        Me.pnlFunction.Controls.Add(Me.chkAnnounceAfterLockingFunction)
        Me.pnlFunction.Controls.Add(Me.chkAnnounceAfterActivatingFunction)
        Me.pnlFunction.Controls.Add(Me.GroupBox10)
        Me.pnlFunction.Controls.Add(Me.Label13)
        Me.pnlFunction.Controls.Add(Me.lblLockedFunctionColor)
        Me.pnlFunction.Controls.Add(Me.GroupBox9)
        Me.pnlFunction.Controls.Add(Me.Label12)
        Me.pnlFunction.Controls.Add(Me.Label11)
        Me.pnlFunction.Location = New System.Drawing.Point(15, 9)
        Me.pnlFunction.Name = "pnlFunction"
        Me.pnlFunction.Size = New System.Drawing.Size(384, 318)
        Me.pnlFunction.TabIndex = 6
        '
        'chkCannotDeleteFunction
        '
        Me.chkCannotDeleteFunction.Checked = True
        Me.chkCannotDeleteFunction.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCannotDeleteFunction.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCannotDeleteFunction.Location = New System.Drawing.Point(23, 216)
        Me.chkCannotDeleteFunction.Name = "chkCannotDeleteFunction"
        Me.chkCannotDeleteFunction.Size = New System.Drawing.Size(345, 36)
        Me.chkCannotDeleteFunction.TabIndex = 20
        Me.chkCannotDeleteFunction.Text = "Không cho phép xóa chức năng khi chức năng này đã được gán cho một Role"
        '
        'chkAnnounceAfterLockingFunction
        '
        Me.chkAnnounceAfterLockingFunction.Checked = True
        Me.chkAnnounceAfterLockingFunction.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAnnounceAfterLockingFunction.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAnnounceAfterLockingFunction.Location = New System.Drawing.Point(21, 183)
        Me.chkAnnounceAfterLockingFunction.Name = "chkAnnounceAfterLockingFunction"
        Me.chkAnnounceAfterLockingFunction.Size = New System.Drawing.Size(345, 30)
        Me.chkAnnounceAfterLockingFunction.TabIndex = 19
        Me.chkAnnounceAfterLockingFunction.Text = "Thông báo mỗi khi khóa chức năng thành công"
        '
        'chkAnnounceAfterActivatingFunction
        '
        Me.chkAnnounceAfterActivatingFunction.Checked = True
        Me.chkAnnounceAfterActivatingFunction.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAnnounceAfterActivatingFunction.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAnnounceAfterActivatingFunction.Location = New System.Drawing.Point(21, 150)
        Me.chkAnnounceAfterActivatingFunction.Name = "chkAnnounceAfterActivatingFunction"
        Me.chkAnnounceAfterActivatingFunction.Size = New System.Drawing.Size(345, 30)
        Me.chkAnnounceAfterActivatingFunction.TabIndex = 18
        Me.chkAnnounceAfterActivatingFunction.Text = "Thông báo mỗi khi kích hoạt chức năng thành công"
        '
        'GroupBox10
        '
        Me.GroupBox10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox10.Location = New System.Drawing.Point(120, 126)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(273, 3)
        Me.GroupBox10.TabIndex = 17
        Me.GroupBox10.TabStop = False
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.Label13.Location = New System.Drawing.Point(12, 117)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(144, 23)
        Me.Label13.TabIndex = 16
        Me.Label13.Text = "Kích hoạt và khóa"
        '
        'lblLockedFunctionColor
        '
        Me.lblLockedFunctionColor.BackColor = System.Drawing.Color.FromArgb(CType(224, Byte), CType(224, Byte), CType(224, Byte))
        Me.lblLockedFunctionColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLockedFunctionColor.Location = New System.Drawing.Point(225, 54)
        Me.lblLockedFunctionColor.Name = "lblLockedFunctionColor"
        Me.lblLockedFunctionColor.Size = New System.Drawing.Size(30, 23)
        Me.lblLockedFunctionColor.TabIndex = 15
        '
        'GroupBox9
        '
        Me.GroupBox9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox9.Location = New System.Drawing.Point(63, 21)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(321, 3)
        Me.GroupBox9.TabIndex = 14
        Me.GroupBox9.TabStop = False
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.Label12.Location = New System.Drawing.Point(12, 12)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(144, 23)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = "Màu sắc"
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(24, 57)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(198, 23)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Màu chức năng lúc bị khóa(Lock)"
        '
        'pnlRole
        '
        Me.pnlRole.Controls.Add(Me.chkAnnounceBeforeDroppingRole)
        Me.pnlRole.Controls.Add(Me.chkEnableDragAndDrop)
        Me.pnlRole.Controls.Add(Me.GroupBox13)
        Me.pnlRole.Controls.Add(Me.Label16)
        Me.pnlRole.Controls.Add(Me.picDefaultIcoForRole)
        Me.pnlRole.Controls.Add(Me.picDefaultIco)
        Me.pnlRole.Controls.Add(Me.picDefaultImg)
        Me.pnlRole.Controls.Add(Me.GroupBox8)
        Me.pnlRole.Controls.Add(Me.Label10)
        Me.pnlRole.Controls.Add(Me.cboRoleLevel)
        Me.pnlRole.Controls.Add(Me.Label9)
        Me.pnlRole.Controls.Add(Me.Label8)
        Me.pnlRole.Controls.Add(Me.cmdgetDefaultIcoForRole)
        Me.pnlRole.Controls.Add(Me.txtDefaultIconPathForRole)
        Me.pnlRole.Controls.Add(Me.Label7)
        Me.pnlRole.Controls.Add(Me.cmdGetDefaultIcon)
        Me.pnlRole.Controls.Add(Me.txtDefaultIconPathForSubSystem)
        Me.pnlRole.Controls.Add(Me.Label6)
        Me.pnlRole.Controls.Add(Me.cmdGetDefaultImg)
        Me.pnlRole.Controls.Add(Me.GroupBox6)
        Me.pnlRole.Controls.Add(Me.Label5)
        Me.pnlRole.Controls.Add(Me.txtDefaultImgPathForSubSystem)
        Me.pnlRole.Location = New System.Drawing.Point(3, 3)
        Me.pnlRole.Name = "pnlRole"
        Me.pnlRole.Size = New System.Drawing.Size(75, 51)
        Me.pnlRole.TabIndex = 7
        '
        'chkAnnounceBeforeDroppingRole
        '
        Me.chkAnnounceBeforeDroppingRole.Checked = True
        Me.chkAnnounceBeforeDroppingRole.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAnnounceBeforeDroppingRole.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAnnounceBeforeDroppingRole.Location = New System.Drawing.Point(20, 312)
        Me.chkAnnounceBeforeDroppingRole.Name = "chkAnnounceBeforeDroppingRole"
        Me.chkAnnounceBeforeDroppingRole.Size = New System.Drawing.Size(345, 24)
        Me.chkAnnounceBeforeDroppingRole.TabIndex = 35
        Me.chkAnnounceBeforeDroppingRole.Text = "Thông báo trước khi thực hiện thả Role"
        '
        'chkEnableDragAndDrop
        '
        Me.chkEnableDragAndDrop.Checked = True
        Me.chkEnableDragAndDrop.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkEnableDragAndDrop.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEnableDragAndDrop.Location = New System.Drawing.Point(20, 282)
        Me.chkEnableDragAndDrop.Name = "chkEnableDragAndDrop"
        Me.chkEnableDragAndDrop.Size = New System.Drawing.Size(188, 24)
        Me.chkEnableDragAndDrop.TabIndex = 34
        Me.chkEnableDragAndDrop.Text = "Cho phép kéo thả Role"
        '
        'GroupBox13
        '
        Me.GroupBox13.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox13.Location = New System.Drawing.Point(86, 264)
        Me.GroupBox13.Name = "GroupBox13"
        Me.GroupBox13.Size = New System.Drawing.Size(0, 3)
        Me.GroupBox13.TabIndex = 33
        Me.GroupBox13.TabStop = False
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.Label16.Location = New System.Drawing.Point(10, 256)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(102, 23)
        Me.Label16.TabIndex = 32
        Me.Label16.Text = "Kéo thả Role"
        '
        'picDefaultIcoForRole
        '
        Me.picDefaultIcoForRole.Location = New System.Drawing.Point(315, 170)
        Me.picDefaultIcoForRole.Name = "picDefaultIcoForRole"
        Me.picDefaultIcoForRole.Size = New System.Drawing.Size(27, 21)
        Me.picDefaultIcoForRole.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picDefaultIcoForRole.TabIndex = 31
        Me.picDefaultIcoForRole.TabStop = False
        '
        'picDefaultIco
        '
        Me.picDefaultIco.Location = New System.Drawing.Point(315, 116)
        Me.picDefaultIco.Name = "picDefaultIco"
        Me.picDefaultIco.Size = New System.Drawing.Size(27, 21)
        Me.picDefaultIco.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picDefaultIco.TabIndex = 30
        Me.picDefaultIco.TabStop = False
        '
        'picDefaultImg
        '
        Me.picDefaultImg.Location = New System.Drawing.Point(312, 32)
        Me.picDefaultImg.Name = "picDefaultImg"
        Me.picDefaultImg.Size = New System.Drawing.Size(70, 62)
        Me.picDefaultImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picDefaultImg.TabIndex = 29
        Me.picDefaultImg.TabStop = False
        '
        'GroupBox8
        '
        Me.GroupBox8.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox8.Location = New System.Drawing.Point(90, 204)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(0, 3)
        Me.GroupBox8.TabIndex = 28
        Me.GroupBox8.TabStop = False
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.Label10.Location = New System.Drawing.Point(9, 196)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(102, 23)
        Me.Label10.TabIndex = 27
        Me.Label10.Text = "Cấp của Role"
        '
        'cboRoleLevel
        '
        Me.cboRoleLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRoleLevel.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRoleLevel.Items.AddRange(New Object() {"       1", "       2", "       3", "       4", "       5", "       6", "       7", "       8", "       9", "      10"})
        Me.cboRoleLevel.Location = New System.Drawing.Point(192, 224)
        Me.cboRoleLevel.Name = "cboRoleLevel"
        Me.cboRoleLevel.Size = New System.Drawing.Size(81, 24)
        Me.cboRoleLevel.TabIndex = 26
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.Label9.Location = New System.Drawing.Point(18, 228)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(171, 16)
        Me.Label9.TabIndex = 25
        Me.Label9.Text = "Số cấp cao nhất khi tạo Role"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.Label8.Location = New System.Drawing.Point(18, 150)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(285, 18)
        Me.Label8.TabIndex = 22
        Me.Label8.Text = "Đường dẫn Icon mặc định cho Role lá"
        '
        'cmdgetDefaultIcoForRole
        '
        Me.cmdgetDefaultIcoForRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdgetDefaultIcoForRole.Image = CType(resources.GetObject("cmdgetDefaultIcoForRole.Image"), System.Drawing.Image)
        Me.cmdgetDefaultIcoForRole.Location = New System.Drawing.Point(264, 170)
        Me.cmdgetDefaultIcoForRole.Name = "cmdgetDefaultIcoForRole"
        Me.cmdgetDefaultIcoForRole.Size = New System.Drawing.Size(24, 20)
        Me.cmdgetDefaultIcoForRole.TabIndex = 21
        Me.cmdgetDefaultIcoForRole.TabStop = False
        Me.ToolTip1.SetToolTip(Me.cmdgetDefaultIcoForRole, "Click vào đây để chọn đường dẫn Icon mặc định cho Role lá")
        '
        'txtDefaultIconPathForRole
        '
        Me.txtDefaultIconPathForRole.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDefaultIconPathForRole.Location = New System.Drawing.Point(18, 170)
        Me.txtDefaultIconPathForRole.Name = "txtDefaultIconPathForRole"
        Me.txtDefaultIconPathForRole.Size = New System.Drawing.Size(246, 21)
        Me.txtDefaultIconPathForRole.TabIndex = 20
        Me.txtDefaultIconPathForRole.Text = ""
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.Label7.Location = New System.Drawing.Point(18, 96)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(273, 18)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "Đường dẫn Icon mặc định cho Role phân hệ"
        '
        'cmdGetDefaultIcon
        '
        Me.cmdGetDefaultIcon.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdGetDefaultIcon.Image = CType(resources.GetObject("cmdGetDefaultIcon.Image"), System.Drawing.Image)
        Me.cmdGetDefaultIcon.Location = New System.Drawing.Point(264, 116)
        Me.cmdGetDefaultIcon.Name = "cmdGetDefaultIcon"
        Me.cmdGetDefaultIcon.Size = New System.Drawing.Size(24, 20)
        Me.cmdGetDefaultIcon.TabIndex = 18
        Me.cmdGetDefaultIcon.TabStop = False
        Me.ToolTip1.SetToolTip(Me.cmdGetDefaultIcon, "Click vào đây để chọn đường dẫn Icon mặc định cho Role phân hệ")
        '
        'txtDefaultIconPathForSubSystem
        '
        Me.txtDefaultIconPathForSubSystem.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDefaultIconPathForSubSystem.Location = New System.Drawing.Point(18, 116)
        Me.txtDefaultIconPathForSubSystem.Name = "txtDefaultIconPathForSubSystem"
        Me.txtDefaultIconPathForSubSystem.Size = New System.Drawing.Size(246, 21)
        Me.txtDefaultIconPathForSubSystem.TabIndex = 17
        Me.txtDefaultIconPathForSubSystem.Text = ""
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.Label6.Location = New System.Drawing.Point(18, 42)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(276, 18)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Đường dẫn ảnh nền mặc định cho Role phân hệ"
        '
        'cmdGetDefaultImg
        '
        Me.cmdGetDefaultImg.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdGetDefaultImg.Image = CType(resources.GetObject("cmdGetDefaultImg.Image"), System.Drawing.Image)
        Me.cmdGetDefaultImg.Location = New System.Drawing.Point(264, 63)
        Me.cmdGetDefaultImg.Name = "cmdGetDefaultImg"
        Me.cmdGetDefaultImg.Size = New System.Drawing.Size(24, 20)
        Me.cmdGetDefaultImg.TabIndex = 15
        Me.cmdGetDefaultImg.TabStop = False
        Me.ToolTip1.SetToolTip(Me.cmdGetDefaultImg, "Click vào đây để chọn đường dẫn ảnh nền mặc định cho Role phân hệ")
        '
        'GroupBox6
        '
        Me.GroupBox6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox6.Location = New System.Drawing.Point(102, 22)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(0, 3)
        Me.GroupBox6.TabIndex = 14
        Me.GroupBox6.TabStop = False
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.Label5.Location = New System.Drawing.Point(9, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(105, 23)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Ảnh nền và Icon"
        '
        'txtDefaultImgPathForSubSystem
        '
        Me.txtDefaultImgPathForSubSystem.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDefaultImgPathForSubSystem.Location = New System.Drawing.Point(18, 63)
        Me.txtDefaultImgPathForSubSystem.Name = "txtDefaultImgPathForSubSystem"
        Me.txtDefaultImgPathForSubSystem.Size = New System.Drawing.Size(246, 21)
        Me.txtDefaultImgPathForSubSystem.TabIndex = 0
        Me.txtDefaultImgPathForSubSystem.Text = ""
        '
        'pnlParameter
        '
        Me.pnlParameter.Controls.Add(Me.chkAnnounceAfterLockingParam)
        Me.pnlParameter.Controls.Add(Me.chkAnnounceAfterActivatingParam)
        Me.pnlParameter.Controls.Add(Me.GroupBox12)
        Me.pnlParameter.Controls.Add(Me.Label14)
        Me.pnlParameter.Controls.Add(Me.lblLockedParamColor)
        Me.pnlParameter.Controls.Add(Me.GroupBox11)
        Me.pnlParameter.Controls.Add(Me.Label15)
        Me.pnlParameter.Controls.Add(Me.lbl1)
        Me.pnlParameter.Location = New System.Drawing.Point(270, 15)
        Me.pnlParameter.Name = "pnlParameter"
        Me.pnlParameter.Size = New System.Drawing.Size(96, 33)
        Me.pnlParameter.TabIndex = 8
        '
        'chkAnnounceAfterLockingParam
        '
        Me.chkAnnounceAfterLockingParam.Checked = True
        Me.chkAnnounceAfterLockingParam.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAnnounceAfterLockingParam.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAnnounceAfterLockingParam.Location = New System.Drawing.Point(21, 183)
        Me.chkAnnounceAfterLockingParam.Name = "chkAnnounceAfterLockingParam"
        Me.chkAnnounceAfterLockingParam.Size = New System.Drawing.Size(345, 30)
        Me.chkAnnounceAfterLockingParam.TabIndex = 23
        Me.chkAnnounceAfterLockingParam.Text = "Thông báo mỗi khi khóa tham số thành công"
        '
        'chkAnnounceAfterActivatingParam
        '
        Me.chkAnnounceAfterActivatingParam.Checked = True
        Me.chkAnnounceAfterActivatingParam.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAnnounceAfterActivatingParam.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAnnounceAfterActivatingParam.Location = New System.Drawing.Point(21, 150)
        Me.chkAnnounceAfterActivatingParam.Name = "chkAnnounceAfterActivatingParam"
        Me.chkAnnounceAfterActivatingParam.Size = New System.Drawing.Size(345, 30)
        Me.chkAnnounceAfterActivatingParam.TabIndex = 22
        Me.chkAnnounceAfterActivatingParam.Text = "Thông báo mỗi khi kích hoạt tham số thành công"
        '
        'GroupBox12
        '
        Me.GroupBox12.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox12.Location = New System.Drawing.Point(120, 126)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(0, 3)
        Me.GroupBox12.TabIndex = 21
        Me.GroupBox12.TabStop = False
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.Label14.Location = New System.Drawing.Point(12, 117)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(144, 23)
        Me.Label14.TabIndex = 20
        Me.Label14.Text = "Kích hoạt và khóa"
        '
        'lblLockedParamColor
        '
        Me.lblLockedParamColor.BackColor = System.Drawing.Color.FromArgb(CType(224, Byte), CType(224, Byte), CType(224, Byte))
        Me.lblLockedParamColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLockedParamColor.Location = New System.Drawing.Point(222, 54)
        Me.lblLockedParamColor.Name = "lblLockedParamColor"
        Me.lblLockedParamColor.Size = New System.Drawing.Size(30, 23)
        Me.lblLockedParamColor.TabIndex = 19
        '
        'GroupBox11
        '
        Me.GroupBox11.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox11.Location = New System.Drawing.Point(75, 21)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(18, 3)
        Me.GroupBox11.TabIndex = 18
        Me.GroupBox11.TabStop = False
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.Label15.Location = New System.Drawing.Point(12, 12)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(144, 23)
        Me.Label15.TabIndex = 17
        Me.Label15.Text = "Màu sắc"
        '
        'lbl1
        '
        Me.lbl1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl1.Location = New System.Drawing.Point(21, 57)
        Me.lbl1.Name = "lbl1"
        Me.lbl1.Size = New System.Drawing.Size(198, 23)
        Me.lbl1.TabIndex = 16
        Me.lbl1.Text = "Màu tham số lúc bị khóa(Lock)"
        '
        'pnlOther
        '
        Me.pnlOther.Controls.Add(Me.chkAskingBeforeDeleting)
        Me.pnlOther.Controls.Add(Me.chkCloseFormAfterDML)
        Me.pnlOther.Controls.Add(Me.GroupBox3)
        Me.pnlOther.Controls.Add(Me.Label2)
        Me.pnlOther.Controls.Add(Me.GroupBox2)
        Me.pnlOther.Controls.Add(Me.Label1)
        Me.pnlOther.Controls.Add(Me.chkAnnounceAfterDeletingSuccessfully)
        Me.pnlOther.Controls.Add(Me.chkAnnounceAfterUpdatetingSuccessfully)
        Me.pnlOther.Controls.Add(Me.chkAnnounceAfterInsertingSuccessfully)
        Me.pnlOther.Location = New System.Drawing.Point(192, 12)
        Me.pnlOther.Name = "pnlOther"
        Me.pnlOther.Size = New System.Drawing.Size(69, 39)
        Me.pnlOther.TabIndex = 9
        '
        'chkAskingBeforeDeleting
        '
        Me.chkAskingBeforeDeleting.Checked = True
        Me.chkAskingBeforeDeleting.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAskingBeforeDeleting.Enabled = False
        Me.chkAskingBeforeDeleting.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAskingBeforeDeleting.Location = New System.Drawing.Point(39, 126)
        Me.chkAskingBeforeDeleting.Name = "chkAskingBeforeDeleting"
        Me.chkAskingBeforeDeleting.Size = New System.Drawing.Size(309, 36)
        Me.chkAskingBeforeDeleting.TabIndex = 9
        Me.chkAskingBeforeDeleting.Text = "Luôn hỏi trước khi  thực hiện thao tác xóa dữ liệu"
        '
        'chkCloseFormAfterDML
        '
        Me.chkCloseFormAfterDML.Checked = True
        Me.chkCloseFormAfterDML.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCloseFormAfterDML.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCloseFormAfterDML.Location = New System.Drawing.Point(39, 201)
        Me.chkCloseFormAfterDML.Name = "chkCloseFormAfterDML"
        Me.chkCloseFormAfterDML.Size = New System.Drawing.Size(309, 39)
        Me.chkCloseFormAfterDML.TabIndex = 8
        Me.chkCloseFormAfterDML.Text = "Không đóng form sau khi Thêm mới, sửa, xóa thành công (Nên dùng)"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Location = New System.Drawing.Point(63, 180)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(0, 3)
        Me.GroupBox3.TabIndex = 7
        Me.GroupBox3.TabStop = False
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.Label2.Location = New System.Drawing.Point(15, 171)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 23)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Hiển thị"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Location = New System.Drawing.Point(78, 21)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(0, 3)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 23)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Thông báo"
        '
        'chkAnnounceAfterDeletingSuccessfully
        '
        Me.chkAnnounceAfterDeletingSuccessfully.Checked = True
        Me.chkAnnounceAfterDeletingSuccessfully.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAnnounceAfterDeletingSuccessfully.Enabled = False
        Me.chkAnnounceAfterDeletingSuccessfully.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAnnounceAfterDeletingSuccessfully.Location = New System.Drawing.Point(39, 99)
        Me.chkAnnounceAfterDeletingSuccessfully.Name = "chkAnnounceAfterDeletingSuccessfully"
        Me.chkAnnounceAfterDeletingSuccessfully.Size = New System.Drawing.Size(309, 24)
        Me.chkAnnounceAfterDeletingSuccessfully.TabIndex = 2
        Me.chkAnnounceAfterDeletingSuccessfully.Text = "Thông báo mỗi khi Xóa dữ liệu thành công"
        '
        'chkAnnounceAfterUpdatetingSuccessfully
        '
        Me.chkAnnounceAfterUpdatetingSuccessfully.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAnnounceAfterUpdatetingSuccessfully.Location = New System.Drawing.Point(39, 72)
        Me.chkAnnounceAfterUpdatetingSuccessfully.Name = "chkAnnounceAfterUpdatetingSuccessfully"
        Me.chkAnnounceAfterUpdatetingSuccessfully.Size = New System.Drawing.Size(309, 24)
        Me.chkAnnounceAfterUpdatetingSuccessfully.TabIndex = 1
        Me.chkAnnounceAfterUpdatetingSuccessfully.Text = "Thông báo mỗi khi cập nhật dữ liệu thành công"
        '
        'chkAnnounceAfterInsertingSuccessfully
        '
        Me.chkAnnounceAfterInsertingSuccessfully.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAnnounceAfterInsertingSuccessfully.Location = New System.Drawing.Point(39, 42)
        Me.chkAnnounceAfterInsertingSuccessfully.Name = "chkAnnounceAfterInsertingSuccessfully"
        Me.chkAnnounceAfterInsertingSuccessfully.Size = New System.Drawing.Size(309, 24)
        Me.chkAnnounceAfterInsertingSuccessfully.TabIndex = 0
        Me.chkAnnounceAfterInsertingSuccessfully.Text = "Thông báo mỗi khi thêm mới dữ liệu thành công"
        '
        'pnlOption
        '
        Me.pnlOption.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlOption.Controls.Add(Me.pnlFunction)
        Me.pnlOption.Controls.Add(Me.pnlRole)
        Me.pnlOption.Controls.Add(Me.pnlUser)
        Me.pnlOption.Controls.Add(Me.pnlParameter)
        Me.pnlOption.Controls.Add(Me.pnlOther)
        Me.pnlOption.Controls.Add(Me.pnlOutIN)
        Me.pnlOption.Controls.Add(Me.pnlUpdateVersion)
        Me.pnlOption.Location = New System.Drawing.Point(189, 6)
        Me.pnlOption.Name = "pnlOption"
        Me.pnlOption.Size = New System.Drawing.Size(420, 354)
        Me.pnlOption.TabIndex = 11
        '
        'pnlOutIN
        '
        Me.pnlOutIN.Controls.Add(Me.RadioButton2)
        Me.pnlOutIN.Controls.Add(Me.RadioButton1)
        Me.pnlOutIN.Controls.Add(Me.GroupBox14)
        Me.pnlOutIN.Controls.Add(Me.Label18)
        Me.pnlOutIN.Controls.Add(Me.Label22)
        Me.pnlOutIN.Controls.Add(Me.Button3)
        Me.pnlOutIN.Controls.Add(Me.GroupBox15)
        Me.pnlOutIN.Controls.Add(Me.Label23)
        Me.pnlOutIN.Controls.Add(Me.TextBox3)
        Me.pnlOutIN.Location = New System.Drawing.Point(6, 5)
        Me.pnlOutIN.Name = "pnlOutIN"
        Me.pnlOutIN.Size = New System.Drawing.Size(81, 118)
        Me.pnlOutIN.TabIndex = 10
        '
        'RadioButton2
        '
        Me.RadioButton2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton2.Location = New System.Drawing.Point(16, 154)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(386, 24)
        Me.RadioButton2.TabIndex = 30
        Me.RadioButton2.Text = "Không cập nhật lại cấu hình nếu đã tồn tại (Không nên dùng)"
        '
        'RadioButton1
        '
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton1.Location = New System.Drawing.Point(18, 124)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(348, 24)
        Me.RadioButton1.TabIndex = 29
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Cập nhật lại cấu hình nếu đã tồn tại(Nên dùng)"
        '
        'GroupBox14
        '
        Me.GroupBox14.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox14.Location = New System.Drawing.Point(92, 104)
        Me.GroupBox14.Name = "GroupBox14"
        Me.GroupBox14.Size = New System.Drawing.Size(0, 3)
        Me.GroupBox14.TabIndex = 28
        Me.GroupBox14.TabStop = False
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.Label18.Location = New System.Drawing.Point(10, 96)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(102, 23)
        Me.Label18.TabIndex = 27
        Me.Label18.Text = "Nhập cấu hình"
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.Label22.Location = New System.Drawing.Point(18, 42)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(308, 18)
        Me.Label22.TabIndex = 16
        Me.Label22.Text = "Đường dẫn mặc định đến thư mục Lưu file xuất XML"
        '
        'Button3
        '
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Image = CType(resources.GetObject("Button3.Image"), System.Drawing.Image)
        Me.Button3.Location = New System.Drawing.Point(264, 63)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(24, 20)
        Me.Button3.TabIndex = 15
        Me.Button3.TabStop = False
        Me.ToolTip1.SetToolTip(Me.Button3, "Click vào đây để chọn đường dẫn ảnh nền mặc định cho Role phân hệ")
        '
        'GroupBox15
        '
        Me.GroupBox15.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox15.Location = New System.Drawing.Point(102, 22)
        Me.GroupBox15.Name = "GroupBox15"
        Me.GroupBox15.Size = New System.Drawing.Size(0, 3)
        Me.GroupBox15.TabIndex = 14
        Me.GroupBox15.TabStop = False
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.Label23.Location = New System.Drawing.Point(9, 12)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(105, 23)
        Me.Label23.TabIndex = 13
        Me.Label23.Text = "Xuất cấu hình"
        '
        'TextBox3
        '
        Me.TextBox3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox3.Location = New System.Drawing.Point(18, 63)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(246, 21)
        Me.TextBox3.TabIndex = 0
        Me.TextBox3.Text = ""
        '
        'pnlUpdateVersion
        '
        Me.pnlUpdateVersion.Controls.Add(Me.CheckBox3)
        Me.pnlUpdateVersion.Controls.Add(Me.Label21)
        Me.pnlUpdateVersion.Controls.Add(Me.NumericUpDown1)
        Me.pnlUpdateVersion.Controls.Add(Me.Label19)
        Me.pnlUpdateVersion.Controls.Add(Me.CheckBox2)
        Me.pnlUpdateVersion.Controls.Add(Me.GroupBox7)
        Me.pnlUpdateVersion.Controls.Add(Me.Label17)
        Me.pnlUpdateVersion.Controls.Add(Me.GroupBox16)
        Me.pnlUpdateVersion.Controls.Add(Me.Label20)
        Me.pnlUpdateVersion.Location = New System.Drawing.Point(6, 5)
        Me.pnlUpdateVersion.Name = "pnlUpdateVersion"
        Me.pnlUpdateVersion.Size = New System.Drawing.Size(408, 331)
        Me.pnlUpdateVersion.TabIndex = 11
        '
        'CheckBox3
        '
        Me.CheckBox3.Checked = True
        Me.CheckBox3.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox3.Location = New System.Drawing.Point(22, 188)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(345, 56)
        Me.CheckBox3.TabIndex = 35
        Me.CheckBox3.Text = "Bắt buộc phải cập nhật phiên bản trước khi thao tác với các chức năng trong chươn" & _
        "g trình khi có phiên bản mới nhất"
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(368, 90)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(34, 18)
        Me.Label21.TabIndex = 34
        Me.Label21.Text = "Kb"
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumericUpDown1.Location = New System.Drawing.Point(270, 88)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {20480, 0, 0, 0})
        Me.NumericUpDown1.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(96, 22)
        Me.NumericUpDown1.TabIndex = 33
        Me.NumericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.NumericUpDown1.Value = New Decimal(New Integer() {10240, 0, 0, 0})
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(36, 88)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(230, 23)
        Me.Label19.TabIndex = 32
        Me.Label19.Text = "Độ lớn tối đa cho phép của phiên bản"
        '
        'CheckBox2
        '
        Me.CheckBox2.Checked = True
        Me.CheckBox2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox2.Location = New System.Drawing.Point(20, 54)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(345, 24)
        Me.CheckBox2.TabIndex = 31
        Me.CheckBox2.Text = "Nén phiên bản trước khi cập nhật vào CSDL"
        '
        'GroupBox7
        '
        Me.GroupBox7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox7.Location = New System.Drawing.Point(92, 158)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(315, 3)
        Me.GroupBox7.TabIndex = 28
        Me.GroupBox7.TabStop = False
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.Label17.Location = New System.Drawing.Point(10, 150)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(102, 23)
        Me.Label17.TabIndex = 27
        Me.Label17.Text = "Phía Client"
        '
        'GroupBox16
        '
        Me.GroupBox16.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox16.Location = New System.Drawing.Point(82, 22)
        Me.GroupBox16.Name = "GroupBox16"
        Me.GroupBox16.Size = New System.Drawing.Size(320, 3)
        Me.GroupBox16.TabIndex = 14
        Me.GroupBox16.TabStop = False
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.Label20.Location = New System.Drawing.Point(9, 12)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(105, 23)
        Me.Label20.TabIndex = 13
        Me.Label20.Text = "Phía Server"
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdClose.Location = New System.Drawing.Point(507, 381)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(93, 30)
        Me.cmdClose.TabIndex = 17
        Me.cmdClose.Text = "Th&oát"
        '
        'frm_Options
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(610, 420)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.pnlOption)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.tvwOption)
        Me.Controls.Add(Me.cmdSave)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frm_Options"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tùy chọn"
        Me.pnlUser.ResumeLayout(False)
        Me.pnlFunction.ResumeLayout(False)
        Me.pnlRole.ResumeLayout(False)
        Me.pnlParameter.ResumeLayout(False)
        Me.pnlOther.ResumeLayout(False)
        Me.pnlOption.ResumeLayout(False)
        Me.pnlOutIN.ResumeLayout(False)
        Me.pnlUpdateVersion.ResumeLayout(False)
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frm_Options_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadTreeView()
        pnlUser.Dock = DockStyle.Fill
        pnlFunction.Dock = DockStyle.Fill
        pnlRole.Dock = DockStyle.Fill
        pnlOutIN.Dock = DockStyle.Fill
        pnlParameter.Dock = DockStyle.Fill
        pnlOther.Dock = DockStyle.Fill
        pnlUser.BringToFront()
        cboRoleLevel.SelectedIndex = 4
        tvwOption.SelectedNode = tvwOption.Nodes(0)
        mv_ctx.MenuItems.Add(New MenuItem("Xóa ảnh nền(Icon)", New EventHandler(AddressOf _DeleteImg)))
        picDefaultIco.ContextMenu = mv_ctx
        picDefaultIcoForRole.ContextMenu = mv_ctx
        picDefaultImg.ContextMenu = mv_ctx
        GetValue()
        mv_bLockedFunctionColor = gv_LockedFunctionColor
        mv_bLockedParamColor = gv_LockedParamColor

    End Sub
    Private Sub _DeleteImg(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If CType(CType(CType(sender, MenuItem).Parent, ContextMenu).SourceControl, PictureBox).Name = picDefaultIco.Name Then
                picDefaultIco.Image = Nothing
                txtDefaultIconPathForSubSystem.Text = ""
            ElseIf CType(CType(CType(sender, MenuItem).Parent, ContextMenu).SourceControl, PictureBox).Name = picDefaultIcoForRole.Name Then
                picDefaultIcoForRole.Image = Nothing
                txtDefaultIconPathForRole.Text = ""
            Else
                picDefaultImg.Image = Nothing
                txtDefaultImgPathForSubSystem.Text = ""
            End If
        Catch ex As Exception

        End Try
        
    End Sub

    Private Sub LoadTreeView()
        Dim _oNode As TreeNode
        _oNode = New TreeNode
        With _oNode
            .Text = "Người dùng"
            .Tag = "USER"
            .SelectedImageIndex = ImageIndex.NodeUser
            .ImageIndex = ImageIndex.NodeUser
            .ForeColor = System.Drawing.Color.DarkBlue
            .NodeFont = New Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point)
        End With
        tvwOption.Nodes.Add(_oNode)
        '-----------------------------------------------------------------
        _oNode = New TreeNode
        With _oNode
            .Text = "Chức năng"
            .Tag = "FUNCTION"
            .SelectedImageIndex = ImageIndex.NodeFuntion
            .ImageIndex = ImageIndex.NodeFuntion
            .ForeColor = System.Drawing.Color.DarkBlue
            .NodeFont = New Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point)
        End With
        tvwOption.Nodes.Add(_oNode)
        '-----------------------------------------------------------------
        _oNode = New TreeNode
        With _oNode
            .Text = "Roles"
            .Tag = "ROLE"
            .SelectedImageIndex = ImageIndex.LeafRole
            .ImageIndex = ImageIndex.LeafRole
            .ForeColor = System.Drawing.Color.DarkBlue
            .NodeFont = New Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point)
        End With
        tvwOption.Nodes.Add(_oNode)
        '-----------------------------------------------------------------
        _oNode = New TreeNode
        With _oNode
            .Text = "Tham số"
            .Tag = "PARAM"
            .SelectedImageIndex = ImageIndex.NodeParam
            .ImageIndex = ImageIndex.NodeParam
            .ForeColor = System.Drawing.Color.DarkBlue
            .NodeFont = New Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point)
        End With
        tvwOption.Nodes.Add(_oNode)
        '-----------------------------------------------------------------

        _oNode = New TreeNode
        With _oNode
            .Text = "Nhập xuất cấu hình"
            .Tag = "OUTIN"
            .SelectedImageIndex = ImageIndex.RootRole
            .ImageIndex = ImageIndex.RootRole
            .ForeColor = System.Drawing.Color.DarkBlue
            .NodeFont = New Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point)
        End With
        tvwOption.Nodes.Add(_oNode)
        '-----------------------------------------------------------------
        _oNode = New TreeNode
        With _oNode
            .Text = "Cập nhật phiên bản"
            .Tag = "UPDATEVERSION"
            .SelectedImageIndex = ImageIndex.RootRole
            .ImageIndex = ImageIndex.RootRole
            .ForeColor = System.Drawing.Color.DarkBlue
            .NodeFont = New Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point)
        End With
        tvwOption.Nodes.Add(_oNode)
        '-----------------------------------------------------------------
        _oNode = New TreeNode
        With _oNode
            .Text = "Tùy chọn khác"
            .Tag = "OTHER"
            .SelectedImageIndex = ImageIndex.RootRole
            .ImageIndex = ImageIndex.RootRole
            .ForeColor = System.Drawing.Color.DarkBlue
            .NodeFont = New Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point)
        End With
        tvwOption.Nodes.Add(_oNode)
        '-----------------------------------------------------------------
        tvwOption.ExpandAll()
    End Sub



    Private Sub tvwOption_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvwOption.Click
        Try
            Select Case tvwOption.SelectedNode.Tag
                Case "USER"
                    pnlUser.BringToFront()
                Case "FUNCTION"
                    pnlFunction.BringToFront()
                Case "ROLE"
                    pnlRole.BringToFront()
                Case "PARAM"
                    pnlParameter.BringToFront()
                Case "OTHER"
                    pnlOther.BringToFront()
                Case "UPDATEVERSION"
                    pnlUpdateVersion.BringToFront()
            End Select

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub tvwOption_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwOption.AfterSelect
        Try
            Select Case tvwOption.SelectedNode.Tag
                Case "USER"
                    pnlUser.BringToFront()
                Case "FUNCTION"
                    pnlFunction.BringToFront()
                Case "ROLE"
                    pnlRole.BringToFront()
                Case "PARAM"
                    pnlParameter.BringToFront()
                Case "OTHER"
                    pnlOther.BringToFront()
                Case "OUTIN"
                    pnlOutIN.BringToFront()
                Case "UPDATEVERSION"
                    pnlUpdateVersion.BringToFront()
            End Select

        Catch ex As Exception

        End Try
    End Sub

    Private Sub tvwOption_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvwOption.MouseDown
        If Not tvwOption.SelectedNode Is tvwOption.GetNodeAt(e.X, e.Y) Then tvwOption.SelectedNode = tvwOption.GetNodeAt(e.X, e.Y)
    End Sub
    Private Sub GetValue()
        Try
            txtDefaultIconPathForSubSystem.Text = gv_sDefaultIconPathForSubSystem
            txtDefaultImgPathForSubSystem.Text = gv_sDefaultImgPathForSubSystem
            txtDefaultIconPathForRole.Text = gv_sDefaultIconPathForRole
            cboRoleLevel.SelectedIndex = gv_intRoleLevel - 1
            chkEnableDragAndDrop.Checked = gv_bEnableDragAndDrop
            chkAnnounceBeforeDroppingRole.Checked = gv_bAnnouceBeforeDropRole
            '-------------------------------------------------------------------------------------
            chkCannotDeletePWD.Checked = gv_bCannotDeletePWDOfUID
            chkCannotDeletePWDOfUIDs.Checked = gv_bCannotDeletePWDOfAllUIDs
            chkCannotDeleteUID.Checked = gv_bCannotDeleteUID
            optMixedRole.Checked = gv_bMixedRolesOfUsers
            optDeleteBeforeMixed.Checked = Not optMixedRole.Checked
            chkCanDblClicktoGetRole.Checked = gv_bCanDblClickToGetRolesForUser
            '-------------------------------------------------------------------------------------
            chkAnnounceAfterInsertingSuccessfully.Checked = gv_bAnnounceAfterInsertingSuccessfully
            chkAnnounceAfterUpdatetingSuccessfully.Checked = gv_bAnnounceAfterUpdatingSuccessfully
            chkAnnounceAfterDeletingSuccessfully.Checked = gv_bAnnounceAfterDeletingSuccessfully
            chkAskingBeforeDeleting.Checked = gv_bAskingBeforeDeleting
            chkCloseFormAfterDML.Checked = Not gv_bCloseFormAfterDML
            chkCannotDeleteFunction.Checked = gv_bCannotDeleteFunction
            If File.Exists(gv_sDefaultImgPathForSubSystem) Then
                picDefaultImg.Image = Image.FromFile(gv_sDefaultImgPathForSubSystem)
            End If
            If File.Exists(gv_sDefaultIconPathForSubSystem) Then
                picDefaultIco.Image = Image.FromFile(gv_sDefaultIconPathForSubSystem)
            End If
            If File.Exists(gv_sDefaultIconPathForRole) Then
                picDefaultIcoForRole.Image = Image.FromFile(gv_sDefaultIconPathForRole)
            End If
            '-------------------------------------------------------------------------------------
            lblLockedFunctionColor.BackColor = gv_LockedFunctionColor
            lblLockedParamColor.BackColor = gv_LockedParamColor
            chkAnnounceAfterActivatingFunction.Checked = gv_bAnnouceAfterActivatingFunction
            chkAnnounceAfterLockingFunction.Checked = gv_bAnnouceAfterLockingFunction
            chkAnnounceAfterActivatingParam.Checked = gv_bAnnouceAfterActivatingParam
            chkAnnounceAfterLockingParam.Checked = gv_bAnnouceAfterLockingParam
        Catch ex As Exception

        End Try

    End Sub
    Private Sub SetValue()
        Try
            gv_sDefaultIconPathForSubSystem = txtDefaultIconPathForSubSystem.Text
            gv_sDefaultImgPathForSubSystem = txtDefaultImgPathForSubSystem.Text
            gv_sDefaultIconPathForRole = txtDefaultIconPathForRole.Text
            gv_bEnableDragAndDrop = chkEnableDragAndDrop.Checked
            gv_bAnnouceBeforeDropRole = chkAnnounceBeforeDroppingRole.Checked
            gv_intRoleLevel = CInt(cboRoleLevel.Text)
            '-------------------------------------------------------------------------------------
            gv_bCannotDeletePWDOfUID = chkCannotDeletePWD.Checked
            gv_bCannotDeletePWDOfAllUIDs = chkCannotDeletePWDOfUIDs.Checked
            gv_bCannotDeleteUID = chkCannotDeleteUID.Checked
            gv_bMixedRolesOfUsers = optMixedRole.Checked
            gv_bCanDblClickToGetRolesForUser = chkCanDblClicktoGetRole.Checked
            '-------------------------------------------------------------------------------------
            gv_bAnnounceAfterInsertingSuccessfully = chkAnnounceAfterInsertingSuccessfully.Checked
            gv_bAnnounceAfterUpdatingSuccessfully = chkAnnounceAfterUpdatetingSuccessfully.Checked
            gv_bAnnounceAfterDeletingSuccessfully = chkAnnounceAfterDeletingSuccessfully.Checked
            gv_bAskingBeforeDeleting = chkAskingBeforeDeleting.Checked
            gv_bCloseFormAfterDML = Not chkCloseFormAfterDML.Checked
            gv_LockedFunctionColor = lblLockedFunctionColor.BackColor
            gv_LockedParamColor = lblLockedParamColor.BackColor

            gv_bAnnouceAfterActivatingFunction = chkAnnounceAfterActivatingFunction.Checked
            gv_bAnnouceAfterLockingFunction = chkAnnounceAfterLockingFunction.Checked

            gv_bCannotDeleteFunction = chkCannotDeleteFunction.Checked
            gv_bAnnouceAfterActivatingParam = chkAnnounceAfterActivatingParam.Checked
            gv_bAnnouceAfterLockingParam = chkAnnounceAfterLockingParam.Checked
            If mv_bLockedFunctionColor.Equals(gv_LockedFunctionColor) Then
            Else
                For Each _oNode As TreeNode In gv_oMainForm.tvwAdminSystem.Nodes(0).Nodes(1).Nodes
                    If _oNode.ForeColor.Equals(mv_bLockedFunctionColor) Then
                        _oNode.ForeColor = gv_LockedFunctionColor
                    End If
                Next
            End If
            If mv_bLockedParamColor.Equals(gv_LockedParamColor) Then
            Else
                For Each _oNode As TreeNode In gv_oMainForm.tvwAdminSystem.Nodes(0).Nodes(3).Nodes
                    If _oNode.ForeColor.Equals(mv_bLockedParamColor) Then
                        _oNode.ForeColor = gv_LockedParamColor
                    End If
                Next
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub WriteReg()
        Dim clsReg As New clsRegistry.clsRegistry
        VBITJSC.RegConfiguration.SaveSettings("DVC_COMPANY", "Sys_MAN_DVC", "DefaultIconPathForSubSystem", gv_sDefaultIconPathForSubSystem)
        VBITJSC.RegConfiguration.SaveSettings("DVC_COMPANY", "Sys_MAN_DVC", "DefaultImgPathForSubSystem", gv_sDefaultImgPathForSubSystem)
        VBITJSC.RegConfiguration.SaveSettings("DVC_COMPANY", "Sys_MAN_DVC", "DefaultIconPathForSubRole", gv_sDefaultIconPathForRole)
        VBITJSC.RegConfiguration.SaveSettings("DVC_COMPANY", "Sys_MAN_DVC", "RoleLevel", gv_intRoleLevel)
        VBITJSC.RegConfiguration.SaveSettings("DVC_COMPANY", "Sys_MAN_DVC", "EnableDragAndDrop", ChangeBoolean(gv_bEnableDragAndDrop))
        VBITJSC.RegConfiguration.SaveSettings("DVC_COMPANY", "Sys_MAN_DVC", "AnnouceBeforeDropRole", ChangeBoolean(gv_bAnnouceBeforeDropRole))

        '-------------------------------------------------------------------------------------
        VBITJSC.RegConfiguration.SaveSettings("DVC_COMPANY", "Sys_MAN_DVC", "CannotDeletePWDOfUID", ChangeBoolean(gv_bCannotDeletePWDOfUID))
        VBITJSC.RegConfiguration.SaveSettings("DVC_COMPANY", "Sys_MAN_DVC", "CannotDeletePWDOfAllUIDs", ChangeBoolean(gv_bCannotDeletePWDOfAllUIDs))
        VBITJSC.RegConfiguration.SaveSettings("DVC_COMPANY", "Sys_MAN_DVC", "CannotDeleteUID", ChangeBoolean(gv_bCannotDeleteUID))
        VBITJSC.RegConfiguration.SaveSettings("DVC_COMPANY", "Sys_MAN_DVC", "MixedRolesOfUsers", ChangeBoolean(gv_bMixedRolesOfUsers))
        VBITJSC.RegConfiguration.SaveSettings("DVC_COMPANY", "Sys_MAN_DVC", "CanDblClickToGetRolesForUser", ChangeBoolean(gv_bCanDblClickToGetRolesForUser))
        '-------------------------------------------------------------------------------------
        VBITJSC.RegConfiguration.SaveSettings("DVC_COMPANY", "Sys_MAN_DVC", "AnnounceAfterInsertingSuccessfully", ChangeBoolean(gv_bAnnounceAfterInsertingSuccessfully))
        VBITJSC.RegConfiguration.SaveSettings("DVC_COMPANY", "Sys_MAN_DVC", "AnnounceAfterUpdatingSuccessfully", ChangeBoolean(gv_bAnnounceAfterUpdatingSuccessfully))
        VBITJSC.RegConfiguration.SaveSettings("DVC_COMPANY", "Sys_MAN_DVC", "AnnounceAfterDeletingSuccessfully", ChangeBoolean(gv_bAnnounceAfterDeletingSuccessfully))
        VBITJSC.RegConfiguration.SaveSettings("DVC_COMPANY", "Sys_MAN_DVC", "AskingBeforeDeleting", ChangeBoolean(gv_bAskingBeforeDeleting))
        VBITJSC.RegConfiguration.SaveSettings("DVC_COMPANY", "Sys_MAN_DVC", "CloseFormAfterDML", ChangeBoolean(gv_bCloseFormAfterDML))

        VBITJSC.RegConfiguration.SaveSettings("DVC_COMPANY", "Sys_MAN_DVC", "LockedFunctionColor", gv_LockedFunctionColor.ToArgb)
        VBITJSC.RegConfiguration.SaveSettings("DVC_COMPANY", "Sys_MAN_DVC", "LockedParamColor", gv_LockedParamColor.ToArgb)
        VBITJSC.RegConfiguration.SaveSettings("DVC_COMPANY", "Sys_MAN_DVC", "CannotDeleteFunction", gv_bCannotDeleteFunction)
        VBITJSC.RegConfiguration.SaveSettings("DVC_COMPANY", "Sys_MAN_DVC", "AnnouceAfterActivatingFunction", gv_bAnnouceAfterActivatingFunction)
        VBITJSC.RegConfiguration.SaveSettings("DVC_COMPANY", "Sys_MAN_DVC", "AnnouceAfterLockingFunction", gv_bAnnouceAfterLockingFunction)
        VBITJSC.RegConfiguration.SaveSettings("DVC_COMPANY", "Sys_MAN_DVC", "AnnouceAfterActivatingParam", gv_bAnnouceAfterActivatingParam)
        VBITJSC.RegConfiguration.SaveSettings("DVC_COMPANY", "Sys_MAN_DVC", "AnnouceAfterLockingParam", gv_bAnnouceAfterLockingParam)
    End Sub
    Private Function ChangeBoolean(ByVal pv_bValue As Boolean) As String
        If pv_bValue Then
            Return "1"
        Else
            Return "0"
        End If
    End Function


    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        SetValue()
        WriteReg()
        Me.Close()
    End Sub

    Private Sub cmdGetDefaultImg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetDefaultImg.Click
        BrowseImg(txtDefaultImgPathForSubSystem, picDefaultImg, gv_sDefaultImgPathForSubSystem)
    End Sub
    Private Sub BrowseImg(ByVal pv_objText As TextBox, ByVal pv_objPic As PictureBox, ByVal pv_objVar As Object, Optional ByVal pv_bImg As Boolean = True)
        Dim fileDiag As New OpenFileDialog
        Try

            If pv_bImg Then
                fileDiag.Title = "Chọn Ảnh nền mặc định cho phân hệ"
                fileDiag.Filter = "All files|*.Gif;*.bmp;*.jpg|Gif|*.gif|Jpg|*.Jpg|Bmp|*.Bmp|PNG|*.PNG"
            Else
                fileDiag.Title = "Chọn Icon cho Role"
                fileDiag.Filter = "Icon|*.ico|Gif|*.gif|Jpg|*.Jpg|Bmp|*.Bmp|PNG|*.PNG"
            End If
            If fileDiag.ShowDialog = DialogResult.OK Then
                pv_objText.Text = fileDiag.FileName
                pv_objVar = fileDiag.FileName
                pv_objPic.Image = Image.FromFile(fileDiag.FileName)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdGetDefaultIcon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetDefaultIcon.Click
        BrowseImg(txtDefaultIconPathForSubSystem, picDefaultIco, gv_sDefaultIconPathForSubSystem, False)
    End Sub

    Private Sub cmdgetDefaultIcoForRole_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdgetDefaultIcoForRole.Click
        BrowseImg(txtDefaultIconPathForRole, picDefaultIcoForRole, gv_sDefaultIconPathForRole, False)
    End Sub

    Private Sub lblColorLockFunction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblLockedFunctionColor.Click
        Dim ClDiag As New ColorDialog
        Try
            If ClDiag.ShowDialog = DialogResult.OK Then
                lblLockedFunctionColor.BackColor = ClDiag.Color
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub lblLockedParamColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblLockedParamColor.Click
        Dim ClDiag As New ColorDialog
        Try
            ClDiag.AnyColor = False
            If ClDiag.ShowDialog = DialogResult.OK Then
                lblLockedParamColor.BackColor = ClDiag.Color
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frm_Options_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            cmdClose.PerformClick()
        End If
    End Sub
End Class
