Imports System.Threading
Public Class frm_ConfigurationOutput
    Inherits System.Windows.Forms.Form
    Public mv_oUserNode As TreeNode
    Public mv_oRoleNode As TreeNode
    Dim mv_DSRoleForUsers As New DataSet
    Dim mv_DSRoleExport As New DataSet
    Dim mv_DSFunctions As New DataSet
    Dim mv_DSParams As New DataSet
    Dim mv_DSTbrBtn As New DataSet
    Dim mv_DSXML As New DataSet 'Chứa toàn bộ cấu hình
    Dim mv_DS As New DataSet 'Chứa các cấu hình được chọn
    Const CtrlMask As Byte = 8
    '------------------------------------------------
    Dim DTRoleForUsers As DataTable
    Dim DTFunctions As DataTable
    Dim DTParams As DataTable
    Dim mv_arrNode As New ArrayList 'Biến chứa các Node để phục vụ kiểm tra ngăn ko cho kéo Node cấp cao vào Node cấp dưới hơn
    Dim DTUsers As DataTable
    Dim DTRoles As DataTable
    Public mv_bOutIn As Boolean = False 'Biến xác định xem là Import hay Export
    Public mv_DSForImport As New DataSet 'Toàn bộ dữ liệu có trong File XML dùng để Import vào hệ thống
    Public mv_DTNeededRoles As New DataTable 'Số Role thực sự cần thiết=Số Role được chọn trên nhánh Role Union với Role of User
    Private mv_sInsertUser As String = String.Empty
    Public mv_intAllRoleImport As Integer 'Đếm tất cả các Role sẽ được Import vào CSDL
    Public mv_bCancel As Boolean = True
    Private mv_sFileName As String

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
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents imgAdminnistration As System.Windows.Forms.ImageList
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents tvwRoleOutput As System.Windows.Forms.TreeView
    Friend WithEvents tvwUserOutput As System.Windows.Forms.TreeView
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents grdListFunction_Output As System.Windows.Forms.DataGrid
    Friend WithEvents grdListParam_output As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridBoolColumn2 As System.Windows.Forms.DataGridBoolColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents chkReserved1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkCheckAll1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkReserved2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkCheckAll2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkCheckAll3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkReserved3 As System.Windows.Forms.CheckBox
    Friend WithEvents grbRoleForUserOption As System.Windows.Forms.GroupBox
    Friend WithEvents optIncludingRolesOfUser As System.Windows.Forms.RadioButton
    Friend WithEvents optOnlyUser As System.Windows.Forms.RadioButton
    Friend WithEvents optOnlySelectedRoles As System.Windows.Forms.RadioButton
    Friend WithEvents optAllRoles As System.Windows.Forms.RadioButton
    Friend WithEvents DataGridBoolColumn3 As System.Windows.Forms.DataGridBoolColumn
    Friend WithEvents SysFunctions_OUT As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents SysParams_OUT As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents grdRoleForUser_output As System.Windows.Forms.DataGrid
    Friend WithEvents grdRoleForUser As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridBoolColumn4 As System.Windows.Forms.DataGridBoolColumn
    Friend WithEvents DataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn10 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn11 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn12 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn13 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn14 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn15 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents SysRFU As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridBoolColumn1 As System.Windows.Forms.DataGridBoolColumn
    Friend WithEvents DataGridTextBoxColumn16 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn17 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn18 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn19 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn20 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn21 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn22 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn23 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents cmdXMLOutput As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblPercent As System.Windows.Forms.Label
    Friend WithEvents pgrBar As System.Windows.Forms.ProgressBar
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents tvwDbRole As System.Windows.Forms.TreeView
    Friend WithEvents lblDBRole As System.Windows.Forms.Label
    Friend WithEvents lblXMLRole As System.Windows.Forms.Label
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frm_ConfigurationOutput))
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkReserved3 = New System.Windows.Forms.CheckBox
        Me.chkCheckAll3 = New System.Windows.Forms.CheckBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.grbRoleForUserOption = New System.Windows.Forms.GroupBox
        Me.optAllRoles = New System.Windows.Forms.RadioButton
        Me.optOnlySelectedRoles = New System.Windows.Forms.RadioButton
        Me.optIncludingRolesOfUser = New System.Windows.Forms.RadioButton
        Me.optOnlyUser = New System.Windows.Forms.RadioButton
        Me.grdRoleForUser_output = New System.Windows.Forms.DataGrid
        Me.SysRFU = New System.Windows.Forms.DataGridTableStyle
        Me.DataGridTextBoxColumn23 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridBoolColumn1 = New System.Windows.Forms.DataGridBoolColumn
        Me.DataGridTextBoxColumn16 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn17 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn18 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn19 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn20 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.tvwUserOutput = New System.Windows.Forms.TreeView
        Me.imgAdminnistration = New System.Windows.Forms.ImageList(Me.components)
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.chkReserved1 = New System.Windows.Forms.CheckBox
        Me.chkCheckAll1 = New System.Windows.Forms.CheckBox
        Me.grdListFunction_Output = New System.Windows.Forms.DataGrid
        Me.SysFunctions_OUT = New System.Windows.Forms.DataGridTableStyle
        Me.DataGridTextBoxColumn22 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridBoolColumn3 = New System.Windows.Forms.DataGridBoolColumn
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.lblXMLRole = New System.Windows.Forms.Label
        Me.lblDBRole = New System.Windows.Forms.Label
        Me.tvwDbRole = New System.Windows.Forms.TreeView
        Me.tvwRoleOutput = New System.Windows.Forms.TreeView
        Me.Label1 = New System.Windows.Forms.Label
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.chkReserved2 = New System.Windows.Forms.CheckBox
        Me.chkCheckAll2 = New System.Windows.Forms.CheckBox
        Me.grdListParam_output = New System.Windows.Forms.DataGrid
        Me.SysParams_OUT = New System.Windows.Forms.DataGridTableStyle
        Me.DataGridTextBoxColumn21 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridBoolColumn2 = New System.Windows.Forms.DataGridBoolColumn
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn8 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.cmdSave = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.grdRoleForUser = New System.Windows.Forms.DataGrid
        Me.DataGridBoolColumn4 = New System.Windows.Forms.DataGridBoolColumn
        Me.DataGridTextBoxColumn9 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn10 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn11 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn12 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn13 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn14 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn15 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.cmdXMLOutput = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.lblPercent = New System.Windows.Forms.Label
        Me.pgrBar = New System.Windows.Forms.ProgressBar
        Me.lblStatus = New System.Windows.Forms.Label
        Me.cmdClose = New System.Windows.Forms.Button
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.grbRoleForUserOption.SuspendLayout()
        CType(Me.grdRoleForUser_output, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.grdListFunction_Output, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        CType(Me.grdListParam_output, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdRoleForUser, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.ImageList = Me.imgAdminnistration
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(684, 423)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Panel1)
        Me.TabPage1.Controls.Add(Me.Splitter1)
        Me.TabPage1.Controls.Add(Me.tvwUserOutput)
        Me.TabPage1.ImageIndex = 1
        Me.TabPage1.Location = New System.Drawing.Point(4, 23)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(676, 396)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Người dùng"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkReserved3)
        Me.Panel1.Controls.Add(Me.chkCheckAll3)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.grdRoleForUser_output)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(222, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(454, 396)
        Me.Panel1.TabIndex = 2
        '
        'chkReserved3
        '
        Me.chkReserved3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkReserved3.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.chkReserved3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkReserved3.Location = New System.Drawing.Point(348, 3)
        Me.chkReserved3.Name = "chkReserved3"
        Me.chkReserved3.Size = New System.Drawing.Size(104, 18)
        Me.chkReserved3.TabIndex = 3
        Me.chkReserved3.Text = "Đảo chọn"
        '
        'chkCheckAll3
        '
        Me.chkCheckAll3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkCheckAll3.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.chkCheckAll3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCheckAll3.Location = New System.Drawing.Point(234, 3)
        Me.chkCheckAll3.Name = "chkCheckAll3"
        Me.chkCheckAll3.Size = New System.Drawing.Size(108, 18)
        Me.chkCheckAll3.TabIndex = 2
        Me.chkCheckAll3.Text = "Chọn tất cả"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.grbRoleForUserOption)
        Me.GroupBox2.Controls.Add(Me.optIncludingRolesOfUser)
        Me.GroupBox2.Controls.Add(Me.optOnlyUser)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(0, 291)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(454, 105)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Tùy chọn xuất dữ liệu cho User"
        '
        'grbRoleForUserOption
        '
        Me.grbRoleForUserOption.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grbRoleForUserOption.Controls.Add(Me.optAllRoles)
        Me.grbRoleForUserOption.Controls.Add(Me.optOnlySelectedRoles)
        Me.grbRoleForUserOption.Location = New System.Drawing.Point(6, 48)
        Me.grbRoleForUserOption.Name = "grbRoleForUserOption"
        Me.grbRoleForUserOption.Size = New System.Drawing.Size(441, 51)
        Me.grbRoleForUserOption.TabIndex = 6
        Me.grbRoleForUserOption.TabStop = False
        '
        'optAllRoles
        '
        Me.optAllRoles.Checked = True
        Me.optAllRoles.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optAllRoles.Location = New System.Drawing.Point(201, 18)
        Me.optAllRoles.Name = "optAllRoles"
        Me.optAllRoles.Size = New System.Drawing.Size(231, 24)
        Me.optAllRoles.TabIndex = 6
        Me.optAllRoles.TabStop = True
        Me.optAllRoles.Text = "Xuất tất cả các Role của người dùng"
        '
        'optOnlySelectedRoles
        '
        Me.optOnlySelectedRoles.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optOnlySelectedRoles.Location = New System.Drawing.Point(9, 18)
        Me.optOnlySelectedRoles.Name = "optOnlySelectedRoles"
        Me.optOnlySelectedRoles.Size = New System.Drawing.Size(183, 24)
        Me.optOnlySelectedRoles.TabIndex = 5
        Me.optOnlySelectedRoles.Text = "Chỉ xuất Role được chọn"
        '
        'optIncludingRolesOfUser
        '
        Me.optIncludingRolesOfUser.Checked = True
        Me.optIncludingRolesOfUser.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optIncludingRolesOfUser.Location = New System.Drawing.Point(15, 24)
        Me.optIncludingRolesOfUser.Name = "optIncludingRolesOfUser"
        Me.optIncludingRolesOfUser.Size = New System.Drawing.Size(225, 24)
        Me.optIncludingRolesOfUser.TabIndex = 4
        Me.optIncludingRolesOfUser.TabStop = True
        Me.optIncludingRolesOfUser.Text = "Xuất cả thông tin Role người dùng"
        '
        'optOnlyUser
        '
        Me.optOnlyUser.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optOnlyUser.Location = New System.Drawing.Point(240, 24)
        Me.optOnlyUser.Name = "optOnlyUser"
        Me.optOnlyUser.Size = New System.Drawing.Size(198, 24)
        Me.optOnlyUser.TabIndex = 3
        Me.optOnlyUser.Text = "Chỉ xuất thông tin người dùng"
        '
        'grdRoleForUser_output
        '
        Me.grdRoleForUser_output.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdRoleForUser_output.BackgroundColor = System.Drawing.Color.White
        Me.grdRoleForUser_output.CaptionBackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(255, Byte))
        Me.grdRoleForUser_output.CaptionFont = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdRoleForUser_output.DataMember = ""
        Me.grdRoleForUser_output.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdRoleForUser_output.Location = New System.Drawing.Point(0, 0)
        Me.grdRoleForUser_output.Name = "grdRoleForUser_output"
        Me.grdRoleForUser_output.RowHeaderWidth = 5
        Me.grdRoleForUser_output.Size = New System.Drawing.Size(454, 288)
        Me.grdRoleForUser_output.TabIndex = 0
        Me.grdRoleForUser_output.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.SysRFU})
        '
        'SysRFU
        '
        Me.SysRFU.DataGrid = Me.grdRoleForUser_output
        Me.SysRFU.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn23, Me.DataGridBoolColumn1, Me.DataGridTextBoxColumn16, Me.DataGridTextBoxColumn17, Me.DataGridTextBoxColumn18, Me.DataGridTextBoxColumn19, Me.DataGridTextBoxColumn20})
        Me.SysRFU.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.SysRFU.MappingName = ""
        Me.SysRFU.SelectionBackColor = System.Drawing.Color.MediumSlateBlue
        '
        'DataGridTextBoxColumn23
        '
        Me.DataGridTextBoxColumn23.Format = ""
        Me.DataGridTextBoxColumn23.FormatInfo = Nothing
        Me.DataGridTextBoxColumn23.MappingName = ""
        Me.DataGridTextBoxColumn23.Width = 0
        '
        'DataGridBoolColumn1
        '
        Me.DataGridBoolColumn1.AllowNull = False
        Me.DataGridBoolColumn1.FalseValue = "F"
        Me.DataGridBoolColumn1.HeaderText = "Chon"
        Me.DataGridBoolColumn1.MappingName = "CHON"
        Me.DataGridBoolColumn1.NullText = ""
        Me.DataGridBoolColumn1.NullValue = CType(resources.GetObject("DataGridBoolColumn1.NullValue"), Object)
        Me.DataGridBoolColumn1.TrueValue = "T"
        Me.DataGridBoolColumn1.Width = 40
        '
        'DataGridTextBoxColumn16
        '
        Me.DataGridTextBoxColumn16.Format = ""
        Me.DataGridTextBoxColumn16.FormatInfo = Nothing
        Me.DataGridTextBoxColumn16.HeaderText = "Mã Role"
        Me.DataGridTextBoxColumn16.MappingName = "iROLEID"
        Me.DataGridTextBoxColumn16.NullText = "Chưa gán"
        Me.DataGridTextBoxColumn16.Width = 75
        '
        'DataGridTextBoxColumn17
        '
        Me.DataGridTextBoxColumn17.Format = ""
        Me.DataGridTextBoxColumn17.FormatInfo = Nothing
        Me.DataGridTextBoxColumn17.HeaderText = "Tên Role"
        Me.DataGridTextBoxColumn17.MappingName = "sRoleName"
        Me.DataGridTextBoxColumn17.NullText = "Chưa gán"
        Me.DataGridTextBoxColumn17.Width = 150
        '
        'DataGridTextBoxColumn18
        '
        Me.DataGridTextBoxColumn18.Format = ""
        Me.DataGridTextBoxColumn18.FormatInfo = Nothing
        Me.DataGridTextBoxColumn18.HeaderText = "Tên chức năng"
        Me.DataGridTextBoxColumn18.MappingName = "sFunctionName"
        Me.DataGridTextBoxColumn18.NullText = "Chưa gán"
        Me.DataGridTextBoxColumn18.Width = 150
        '
        'DataGridTextBoxColumn19
        '
        Me.DataGridTextBoxColumn19.Format = ""
        Me.DataGridTextBoxColumn19.FormatInfo = Nothing
        Me.DataGridTextBoxColumn19.HeaderText = "Tên thư viện"
        Me.DataGridTextBoxColumn19.MappingName = "sDLLName"
        Me.DataGridTextBoxColumn19.NullText = "Chưa gán"
        Me.DataGridTextBoxColumn19.Width = 110
        '
        'DataGridTextBoxColumn20
        '
        Me.DataGridTextBoxColumn20.Format = ""
        Me.DataGridTextBoxColumn20.FormatInfo = Nothing
        Me.DataGridTextBoxColumn20.HeaderText = "Tên hàm"
        Me.DataGridTextBoxColumn20.MappingName = "sFormName"
        Me.DataGridTextBoxColumn20.NullText = "Chưa gán"
        Me.DataGridTextBoxColumn20.Width = 120
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(219, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 396)
        Me.Splitter1.TabIndex = 1
        Me.Splitter1.TabStop = False
        '
        'tvwUserOutput
        '
        Me.tvwUserOutput.CheckBoxes = True
        Me.tvwUserOutput.Dock = System.Windows.Forms.DockStyle.Left
        Me.tvwUserOutput.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tvwUserOutput.HideSelection = False
        Me.tvwUserOutput.ImageList = Me.imgAdminnistration
        Me.tvwUserOutput.Location = New System.Drawing.Point(0, 0)
        Me.tvwUserOutput.Name = "tvwUserOutput"
        Me.tvwUserOutput.Size = New System.Drawing.Size(219, 396)
        Me.tvwUserOutput.TabIndex = 0
        '
        'imgAdminnistration
        '
        Me.imgAdminnistration.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.imgAdminnistration.ImageSize = New System.Drawing.Size(16, 16)
        Me.imgAdminnistration.ImageStream = CType(resources.GetObject("imgAdminnistration.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgAdminnistration.TransparentColor = System.Drawing.Color.Transparent
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.chkReserved1)
        Me.TabPage2.Controls.Add(Me.chkCheckAll1)
        Me.TabPage2.Controls.Add(Me.grdListFunction_Output)
        Me.TabPage2.ImageIndex = 4
        Me.TabPage2.Location = New System.Drawing.Point(4, 23)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(676, 396)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Chức năng"
        '
        'chkReserved1
        '
        Me.chkReserved1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkReserved1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkReserved1.Location = New System.Drawing.Point(129, 369)
        Me.chkReserved1.Name = "chkReserved1"
        Me.chkReserved1.Size = New System.Drawing.Size(120, 24)
        Me.chkReserved1.TabIndex = 2
        Me.chkReserved1.Text = "Đảo chọn"
        '
        'chkCheckAll1
        '
        Me.chkCheckAll1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkCheckAll1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCheckAll1.Location = New System.Drawing.Point(6, 369)
        Me.chkCheckAll1.Name = "chkCheckAll1"
        Me.chkCheckAll1.Size = New System.Drawing.Size(120, 24)
        Me.chkCheckAll1.TabIndex = 1
        Me.chkCheckAll1.Text = "Chọn tất cả"
        '
        'grdListFunction_Output
        '
        Me.grdListFunction_Output.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdListFunction_Output.BackgroundColor = System.Drawing.Color.White
        Me.grdListFunction_Output.CaptionVisible = False
        Me.grdListFunction_Output.DataMember = ""
        Me.grdListFunction_Output.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdListFunction_Output.Location = New System.Drawing.Point(0, 0)
        Me.grdListFunction_Output.Name = "grdListFunction_Output"
        Me.grdListFunction_Output.RowHeaderWidth = 5
        Me.grdListFunction_Output.Size = New System.Drawing.Size(676, 363)
        Me.grdListFunction_Output.TabIndex = 0
        Me.grdListFunction_Output.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.SysFunctions_OUT})
        '
        'SysFunctions_OUT
        '
        Me.SysFunctions_OUT.DataGrid = Me.grdListFunction_Output
        Me.SysFunctions_OUT.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn22, Me.DataGridBoolColumn3, Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn4})
        Me.SysFunctions_OUT.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.SysFunctions_OUT.MappingName = ""
        Me.SysFunctions_OUT.SelectionBackColor = System.Drawing.Color.MediumSlateBlue
        '
        'DataGridTextBoxColumn22
        '
        Me.DataGridTextBoxColumn22.Format = ""
        Me.DataGridTextBoxColumn22.FormatInfo = Nothing
        Me.DataGridTextBoxColumn22.MappingName = ""
        Me.DataGridTextBoxColumn22.NullText = ""
        Me.DataGridTextBoxColumn22.Width = 0
        '
        'DataGridBoolColumn3
        '
        Me.DataGridBoolColumn3.AllowNull = False
        Me.DataGridBoolColumn3.FalseValue = "F"
        Me.DataGridBoolColumn3.HeaderText = "Chọn"
        Me.DataGridBoolColumn3.MappingName = "CHON"
        Me.DataGridBoolColumn3.NullText = ""
        Me.DataGridBoolColumn3.NullValue = CType(resources.GetObject("DataGridBoolColumn3.NullValue"), Object)
        Me.DataGridBoolColumn3.TrueValue = "T"
        Me.DataGridBoolColumn3.Width = 40
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "Mã chức năng"
        Me.DataGridTextBoxColumn1.MappingName = "PK_iID"
        Me.DataGridTextBoxColumn1.NullText = ""
        Me.DataGridTextBoxColumn1.Width = 80
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "Tên chức năng"
        Me.DataGridTextBoxColumn2.MappingName = "sFunctionName"
        Me.DataGridTextBoxColumn2.NullText = ""
        Me.DataGridTextBoxColumn2.Width = 150
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "Tên thư viện"
        Me.DataGridTextBoxColumn3.MappingName = "sDLLName"
        Me.DataGridTextBoxColumn3.NullText = ""
        Me.DataGridTextBoxColumn3.Width = 150
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "Tên hàm"
        Me.DataGridTextBoxColumn4.MappingName = "sFormName"
        Me.DataGridTextBoxColumn4.NullText = ""
        Me.DataGridTextBoxColumn4.Width = 150
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.lblXMLRole)
        Me.TabPage3.Controls.Add(Me.lblDBRole)
        Me.TabPage3.Controls.Add(Me.tvwDbRole)
        Me.TabPage3.Controls.Add(Me.tvwRoleOutput)
        Me.TabPage3.Controls.Add(Me.Label1)
        Me.TabPage3.ImageIndex = 7
        Me.TabPage3.Location = New System.Drawing.Point(4, 23)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(676, 396)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Menu-Role"
        '
        'lblXMLRole
        '
        Me.lblXMLRole.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblXMLRole.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblXMLRole.Location = New System.Drawing.Point(336, 6)
        Me.lblXMLRole.Name = "lblXMLRole"
        Me.lblXMLRole.Size = New System.Drawing.Size(324, 23)
        Me.lblXMLRole.TabIndex = 10
        Me.lblXMLRole.Text = "Role load được từ File XML"
        '
        'lblDBRole
        '
        Me.lblDBRole.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDBRole.Location = New System.Drawing.Point(3, 6)
        Me.lblDBRole.Name = "lblDBRole"
        Me.lblDBRole.Size = New System.Drawing.Size(309, 23)
        Me.lblDBRole.TabIndex = 9
        Me.lblDBRole.Text = "Role đang có trong hệ thống"
        '
        'tvwDbRole
        '
        Me.tvwDbRole.AllowDrop = True
        Me.tvwDbRole.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvwDbRole.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tvwDbRole.HideSelection = False
        Me.tvwDbRole.ImageList = Me.imgAdminnistration
        Me.tvwDbRole.Location = New System.Drawing.Point(3, 30)
        Me.tvwDbRole.Name = "tvwDbRole"
        Me.tvwDbRole.Size = New System.Drawing.Size(309, 339)
        Me.tvwDbRole.TabIndex = 1
        '
        'tvwRoleOutput
        '
        Me.tvwRoleOutput.AllowDrop = True
        Me.tvwRoleOutput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvwRoleOutput.CheckBoxes = True
        Me.tvwRoleOutput.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tvwRoleOutput.HideSelection = False
        Me.tvwRoleOutput.ImageList = Me.imgAdminnistration
        Me.tvwRoleOutput.Location = New System.Drawing.Point(336, 30)
        Me.tvwRoleOutput.Name = "tvwRoleOutput"
        Me.tvwRoleOutput.Size = New System.Drawing.Size(336, 339)
        Me.tvwRoleOutput.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 372)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(681, 24)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Cập nhật Role bằng cách kéo một Role từ file XML sang Role đang có trong hệ thống" & _
        ". Role được cập nhật sẽ có màu xanh"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.chkReserved2)
        Me.TabPage4.Controls.Add(Me.chkCheckAll2)
        Me.TabPage4.Controls.Add(Me.grdListParam_output)
        Me.TabPage4.ImageIndex = 9
        Me.TabPage4.Location = New System.Drawing.Point(4, 23)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(676, 396)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Tham số"
        '
        'chkReserved2
        '
        Me.chkReserved2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkReserved2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkReserved2.Location = New System.Drawing.Point(129, 366)
        Me.chkReserved2.Name = "chkReserved2"
        Me.chkReserved2.Size = New System.Drawing.Size(120, 24)
        Me.chkReserved2.TabIndex = 4
        Me.chkReserved2.Text = "Đảo chọn"
        '
        'chkCheckAll2
        '
        Me.chkCheckAll2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkCheckAll2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCheckAll2.Location = New System.Drawing.Point(6, 366)
        Me.chkCheckAll2.Name = "chkCheckAll2"
        Me.chkCheckAll2.Size = New System.Drawing.Size(120, 24)
        Me.chkCheckAll2.TabIndex = 3
        Me.chkCheckAll2.Text = "Chọn tất cả"
        '
        'grdListParam_output
        '
        Me.grdListParam_output.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdListParam_output.BackgroundColor = System.Drawing.Color.White
        Me.grdListParam_output.CaptionVisible = False
        Me.grdListParam_output.DataMember = ""
        Me.grdListParam_output.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdListParam_output.Location = New System.Drawing.Point(0, 0)
        Me.grdListParam_output.Name = "grdListParam_output"
        Me.grdListParam_output.RowHeaderWidth = 5
        Me.grdListParam_output.Size = New System.Drawing.Size(676, 360)
        Me.grdListParam_output.TabIndex = 0
        Me.grdListParam_output.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.SysParams_OUT})
        '
        'SysParams_OUT
        '
        Me.SysParams_OUT.DataGrid = Me.grdListParam_output
        Me.SysParams_OUT.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn21, Me.DataGridBoolColumn2, Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn6, Me.DataGridTextBoxColumn7, Me.DataGridTextBoxColumn8})
        Me.SysParams_OUT.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.SysParams_OUT.MappingName = ""
        Me.SysParams_OUT.SelectionBackColor = System.Drawing.Color.MediumSlateBlue
        '
        'DataGridTextBoxColumn21
        '
        Me.DataGridTextBoxColumn21.Format = ""
        Me.DataGridTextBoxColumn21.FormatInfo = Nothing
        Me.DataGridTextBoxColumn21.MappingName = ""
        Me.DataGridTextBoxColumn21.NullText = ""
        Me.DataGridTextBoxColumn21.Width = 0
        '
        'DataGridBoolColumn2
        '
        Me.DataGridBoolColumn2.AllowNull = False
        Me.DataGridBoolColumn2.FalseValue = "F"
        Me.DataGridBoolColumn2.HeaderText = "Chọn"
        Me.DataGridBoolColumn2.MappingName = "Chon"
        Me.DataGridBoolColumn2.NullText = ""
        Me.DataGridBoolColumn2.NullValue = CType(resources.GetObject("DataGridBoolColumn2.NullValue"), Object)
        Me.DataGridBoolColumn2.TrueValue = "T"
        Me.DataGridBoolColumn2.Width = 40
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "Tên tham số"
        Me.DataGridTextBoxColumn5.MappingName = "sName"
        Me.DataGridTextBoxColumn5.NullText = ""
        Me.DataGridTextBoxColumn5.Width = 150
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "Giá trị tham số"
        Me.DataGridTextBoxColumn6.MappingName = "sValue"
        Me.DataGridTextBoxColumn6.NullText = ""
        Me.DataGridTextBoxColumn6.Width = 120
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = ""
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "Kiểu dữ liệu"
        Me.DataGridTextBoxColumn7.MappingName = "sDataType"
        Me.DataGridTextBoxColumn7.NullText = ""
        Me.DataGridTextBoxColumn7.Width = 101
        '
        'DataGridTextBoxColumn8
        '
        Me.DataGridTextBoxColumn8.Format = ""
        Me.DataGridTextBoxColumn8.FormatInfo = Nothing
        Me.DataGridTextBoxColumn8.HeaderText = "Ý nghĩa"
        Me.DataGridTextBoxColumn8.MappingName = "sDesc"
        Me.DataGridTextBoxColumn8.NullText = ""
        Me.DataGridTextBoxColumn8.Width = 150
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSave.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSave.Location = New System.Drawing.Point(435, 447)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(135, 27)
        Me.cmdSave.TabIndex = 1
        Me.cmdSave.Text = "Xuất theo tùy chọn"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Chỉ xuất các mục mà bạn đánh dấu")
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 429)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(678, 3)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        '
        'grdRoleForUser
        '
        Me.grdRoleForUser.DataMember = ""
        Me.grdRoleForUser.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdRoleForUser.Location = New System.Drawing.Point(0, 0)
        Me.grdRoleForUser.Name = "grdRoleForUser"
        Me.grdRoleForUser.TabIndex = 0
        '
        'DataGridBoolColumn4
        '
        Me.DataGridBoolColumn4.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.DataGridBoolColumn4.AllowNull = False
        Me.DataGridBoolColumn4.FalseValue = "F"
        Me.DataGridBoolColumn4.HeaderText = "Chọn"
        Me.DataGridBoolColumn4.MappingName = "CHON"
        Me.DataGridBoolColumn4.NullText = ""
        Me.DataGridBoolColumn4.NullValue = CType(resources.GetObject("DataGridBoolColumn4.NullValue"), Object)
        Me.DataGridBoolColumn4.TrueValue = "T"
        Me.DataGridBoolColumn4.Width = 30
        '
        'DataGridTextBoxColumn9
        '
        Me.DataGridTextBoxColumn9.Format = ""
        Me.DataGridTextBoxColumn9.FormatInfo = Nothing
        Me.DataGridTextBoxColumn9.HeaderText = "Mã Role"
        Me.DataGridTextBoxColumn9.MappingName = "iROLEID"
        Me.DataGridTextBoxColumn9.NullText = ""
        Me.DataGridTextBoxColumn9.Width = 75
        '
        'DataGridTextBoxColumn10
        '
        Me.DataGridTextBoxColumn10.Format = ""
        Me.DataGridTextBoxColumn10.FormatInfo = Nothing
        Me.DataGridTextBoxColumn10.MappingName = ""
        Me.DataGridTextBoxColumn10.Width = 75
        '
        'DataGridTextBoxColumn11
        '
        Me.DataGridTextBoxColumn11.Format = ""
        Me.DataGridTextBoxColumn11.FormatInfo = Nothing
        Me.DataGridTextBoxColumn11.HeaderText = "Tên Role"
        Me.DataGridTextBoxColumn11.MappingName = "sRoleName"
        Me.DataGridTextBoxColumn11.NullText = "Chưa gán"
        Me.DataGridTextBoxColumn11.Width = 150
        '
        'DataGridTextBoxColumn12
        '
        Me.DataGridTextBoxColumn12.Format = ""
        Me.DataGridTextBoxColumn12.FormatInfo = Nothing
        Me.DataGridTextBoxColumn12.HeaderText = "Tên chức năng"
        Me.DataGridTextBoxColumn12.MappingName = "sFunctionName"
        Me.DataGridTextBoxColumn12.NullText = "Chưa gán"
        Me.DataGridTextBoxColumn12.Width = 150
        '
        'DataGridTextBoxColumn13
        '
        Me.DataGridTextBoxColumn13.Format = ""
        Me.DataGridTextBoxColumn13.FormatInfo = Nothing
        Me.DataGridTextBoxColumn13.HeaderText = "Tên thư viện"
        Me.DataGridTextBoxColumn13.MappingName = "sDLLName"
        Me.DataGridTextBoxColumn13.NullText = "Chưa gán"
        Me.DataGridTextBoxColumn13.Width = 80
        '
        'DataGridTextBoxColumn14
        '
        Me.DataGridTextBoxColumn14.Format = ""
        Me.DataGridTextBoxColumn14.FormatInfo = Nothing
        Me.DataGridTextBoxColumn14.HeaderText = "Tên hàm"
        Me.DataGridTextBoxColumn14.MappingName = "sFormName"
        Me.DataGridTextBoxColumn14.NullText = "Chưa gán"
        Me.DataGridTextBoxColumn14.Width = 80
        '
        'DataGridTextBoxColumn15
        '
        Me.DataGridTextBoxColumn15.Format = ""
        Me.DataGridTextBoxColumn15.FormatInfo = Nothing
        Me.DataGridTextBoxColumn15.MappingName = "sUID"
        Me.DataGridTextBoxColumn15.NullText = ""
        Me.DataGridTextBoxColumn15.Width = 75
        '
        'cmdXMLOutput
        '
        Me.cmdXMLOutput.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdXMLOutput.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdXMLOutput.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdXMLOutput.Image = CType(resources.GetObject("cmdXMLOutput.Image"), System.Drawing.Image)
        Me.cmdXMLOutput.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdXMLOutput.Location = New System.Drawing.Point(327, 447)
        Me.cmdXMLOutput.Name = "cmdXMLOutput"
        Me.cmdXMLOutput.Size = New System.Drawing.Size(105, 27)
        Me.cmdXMLOutput.TabIndex = 4
        Me.cmdXMLOutput.Text = "Full Backup"
        Me.cmdXMLOutput.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.cmdXMLOutput, "Xuất toàn bộ cấu hình hệ thống mà không phụ thuộc vào sự lựa chọn của bạn")
        '
        'lblPercent
        '
        Me.lblPercent.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPercent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPercent.Location = New System.Drawing.Point(264, 462)
        Me.lblPercent.Name = "lblPercent"
        Me.lblPercent.Size = New System.Drawing.Size(60, 18)
        Me.lblPercent.TabIndex = 7
        Me.lblPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pgrBar
        '
        Me.pgrBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgrBar.Location = New System.Drawing.Point(3, 462)
        Me.pgrBar.Name = "pgrBar"
        Me.pgrBar.Size = New System.Drawing.Size(261, 18)
        Me.pgrBar.Step = 1
        Me.pgrBar.TabIndex = 6
        '
        'lblStatus
        '
        Me.lblStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStatus.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(3, 438)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(321, 23)
        Me.lblStatus.TabIndex = 8
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdClose.Location = New System.Drawing.Point(585, 447)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(96, 27)
        Me.cmdClose.TabIndex = 17
        Me.cmdClose.Text = "Th&oát"
        '
        'frm_ConfigurationOutput
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(685, 483)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.lblPercent)
        Me.Controls.Add(Me.pgrBar)
        Me.Controls.Add(Me.cmdXMLOutput)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_ConfigurationOutput"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Xuất cấu hình ra file XML"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.grbRoleForUserOption.ResumeLayout(False)
        CType(Me.grdRoleForUser_output, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.grdListFunction_Output, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        CType(Me.grdListParam_output, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdRoleForUser, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frm_ConfigurationOutput_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mv_bCancel = True = True
        tvwRoleOutput.ImageList = gv_oMainForm.tvwAdminSystem.ImageList
        tvwDbRole.ImageList = gv_oMainForm.tvwAdminSystem.ImageList
        If mv_bOutIn Then
            tvwRoleOutput.Size = New Size(672, 384)
            tvwRoleOutput.Location = New Point(3, 3)
            tvwRoleOutput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            lblDBRole.Visible = False
            Label1.Visible = False
            lblXMLRole.Visible = False
            tvwRoleOutput.BringToFront()
            tvwRoleOutput.AllowDrop = False
            mv_DSXML.Clear()
            mv_DSXML.Tables.Clear()
            mv_DSTbrBtn = dsGetAllTbrBtnForOutputConfig(gv_sBranchID)
            CopyData()
            Application.DoEvents()
            BuildTreeView(tvwRoleOutput)
            Application.DoEvents()
        Else
            'tvwRoleOutput.Size = New Size(336, 357)
            'tvwRoleOutput.Location = New Point(336, 30)
            Label1.Visible = True
            lblDBRole.Visible = True
            lblXMLRole.Visible = True
            tvwRoleOutput.BringToFront()
            tvwRoleOutput.AllowDrop = True
            cmdXMLOutput.Visible = False
            BuildTreeView(tvwDbRole)
            GetDataReadyForImport()
        End If
        pgrBar.Visible = False
        lblPercent.Visible = False
    End Sub

#Region "Các hàm phục vụ cho việc Cập nhật thông tin cấu hình từ File XML"

    Private Sub GetDataReadyForImport()
        Try
            mv_DSForImport.ReadXml(gv_sXMLFilePath)
            For Each _oDT As DataTable In mv_DSForImport.Tables
                If _oDT.Columns.Contains("CHON") Then
                    For Each _dr As DataRow In _oDT.Rows
                        _dr("CHON") = "F"
                    Next
                    _oDT.AcceptChanges()
                End If
            Next
            mv_DSForImport.AcceptChanges()
            '------------------------------------------------------------------------------------------
            'Xây dựng cây User
            Dim sv_oUserNode As New TreeNode("Users")
            With sv_oUserNode
                .Text = "Users"
                .Tag = "ROOTUSER"
                .SelectedImageIndex = ImageIndex.RootUser
                .ImageIndex = ImageIndex.RootUser
                .ForeColor = System.Drawing.Color.DarkBlue
                .NodeFont = New Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point)
            End With
            'Tạo các UserNode
            CreateUserNode(tvwUserOutput, sv_oUserNode, globalModule.ImageIndex.NodeUser, globalModule.ImageIndex.NodeUser)
            sv_oUserNode.Expand()
            tvwUserOutput.Nodes.Add(sv_oUserNode)
            '------------------------------------------------------------------------------------------
            '------------------------------------------------------------------------------------------
            Dim sv_oRoleNode As New TreeNode("Roles")
            With sv_oRoleNode
                .Text = "Roles"
                .Tag = "ROOTROLE#-2"
                .SelectedImageIndex = ImageIndex.RootRole
                .ImageIndex = ImageIndex.RootRole
                .ForeColor = System.Drawing.Color.DarkBlue
                .NodeFont = New Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point)
            End With
            tvwRoleOutput.Nodes.Add(sv_oRoleNode)
            CreateRoleNode(tvwRoleOutput, sv_oRoleNode, globalModule.ImageIndex.LeafRole, globalModule.ImageIndex.LeafRole)
            tvwRoleOutput.SelectedNode = tvwRoleOutput.Nodes(0)
            tvwRoleOutput.Nodes(0).Expand()

            '------------------------------------------------------------------------------------------
            '------------------------------------------------------------------------------------------
            With grdListFunction_Output
                .TableStyles(0).MappingName = mv_DSForImport.Tables("Sys_Functions").TableName
                .DataSource = mv_DSForImport.Tables("Sys_Functions").DefaultView
            End With
            mv_DSForImport.Tables("Sys_Functions").DefaultView.AllowNew = False
            mv_DSForImport.Tables("Sys_Functions").DefaultView.AllowDelete = False
            mv_DSForImport.Tables("Sys_Functions").DefaultView.AllowEdit = True
            '------------------------------------------------------------------------------------------
            With grdListParam_output
                .TableStyles(0).MappingName = mv_DSForImport.Tables("Sys_Params").TableName
                .DataSource = mv_DSForImport.Tables("Sys_Params").DefaultView
            End With
            mv_DSForImport.Tables("Sys_Params").DefaultView.AllowNew = False
            mv_DSForImport.Tables("Sys_Params").DefaultView.AllowDelete = False
            mv_DSForImport.Tables("Sys_Params").DefaultView.AllowEdit = True
            '------------------------------------------------------------------------------------------
            With grdRoleForUser_output
                .TableStyles(0).MappingName = mv_DSForImport.Tables("Sys_RFU").TableName
                .DataSource = mv_DSForImport.Tables("Sys_RFU").DefaultView
            End With
            mv_DSForImport.Tables("Sys_RFU").DefaultView.AllowNew = False
            mv_DSForImport.Tables("Sys_RFU").DefaultView.AllowDelete = False
            mv_DSForImport.Tables("Sys_RFU").DefaultView.AllowEdit = True
        Catch ex As Exception

        End Try
    End Sub
    '----------------------------------------------------------------------------------------------------
    'Mục đích       :
    'Đầu vào         :None
    'Đầu ra           :
    'Người tạo      :CuongDV
    'Ngày tạo       :
    'Last Modified:
    '----------------------------------------------------------------------------------------------------
    Private Sub CreateRoleNode(ByRef pv_otvw As TreeView, ByVal pv_oNode As TreeNode, ByVal pv_oImgIndex As ImageIndex, ByVal pv_oSelectedImageIndex As ImageIndex)
        Dim i As Integer = 0
        Try
            If mv_DSForImport.Tables("Sys_ROLES").Rows.Count > 0 Then
                For i = 0 To mv_DSForImport.Tables("Sys_ROLES").Rows.Count - 1
                    'Kiểm tra nếu mã ParentRole =-2 nghĩa là Node phân hệ 
                    If CInt(mv_DSForImport.Tables("Sys_ROLES").Rows(i)("iParentRole")) = -2 Then
                        AddNewTreeNode(pv_otvw, pv_oNode, mv_DSForImport.Tables("Sys_ROLES").Rows(i)("iRole"), mv_DSForImport.Tables("Sys_ROLES").Rows(i)("sRoleName"), IIf(mv_DSForImport.Tables("Sys_ROLES").Rows(i)("FK_iFunctionID").Equals(DBNull.Value) Or mv_DSForImport.Tables("Sys_ROLES").Rows(i)("FK_iFunctionID").Equals(Nothing), -1, mv_DSForImport.Tables("Sys_ROLES").Rows(i)("FK_iFunctionID")), globalModule.ImageIndex.NodeRole, globalModule.ImageIndex.NodeRole, -2)
                        'Gọi thủ tục đệ quy để tạo các ChildNode cho Node vừa được thêm mới này
                        CreateChildNode(pv_otvw, pv_otvw.SelectedNode, CInt(pv_otvw.SelectedNode.Tag.ToString.Substring(pv_otvw.SelectedNode.Tag.ToString.IndexOf("#") + 1)))
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    '----------------------------------------------------------------------------------------------------
    'Mục đích       :Thêm mới một Node vào TreeView
    'Đầu vào         :
    'Đầu ra           :
    'Người tạo      :CuongDV
    'Ngày tạo       :
    'Last Modified:
    '----------------------------------------------------------------------------------------------------
    Private Sub AddNewTreeNode(ByRef pv_oTvw As TreeView, ByVal pv_oParentNode As TreeNode, ByVal pv_iRole As Integer, ByVal pv_sRoleName As String, ByVal pv_iFunctionID As Integer, ByVal pv_oIndex As ImageIndex, ByVal pv_oSelectedIndex As ImageIndex, Optional ByVal pv_iParentRole As Integer = -1)
        Dim sv_oNode As New TreeNode
        Try
            With sv_oNode
                .Text = pv_sRoleName
                .Tag = "LEAFROLES|" & pv_iFunctionID & "#" & pv_iRole
                .SelectedImageIndex = pv_oSelectedIndex
                .ImageIndex = pv_oIndex
                .NodeFont = New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
            End With
            pv_oParentNode.Nodes.Add(sv_oNode)
            pv_oTvw.SelectedNode = sv_oNode
        Catch ex As Exception

        End Try
    End Sub
    '----------------------------------------------------------------------------------------------------
    'Mục đích       :Thủ tục đệ quy nhằm tạo các cấp menu của mỗi phân hệ
    'Đầu vào         :Cây,Node cha,Mã Role cha
    'Đầu ra           :
    'Người tạo      :CuongDV
    'Ngày tạo       :
    'Last Modified:
    '----------------------------------------------------------------------------------------------------
    Private Sub CreateChildNode(ByVal pv_oTvw As TreeView, ByVal pv_oNode As TreeNode, ByVal pv_iRole As Integer)
        Dim sv_oNode As TreeNode
        Dim arrDR As DataRow()
        Try
            'Tìm tất cả các Role có cha là Node này
            arrDR = mv_DSForImport.Tables("Sys_ROLES").Select("iParentRole=" & pv_iRole, "iOrder")
            If UBound(arrDR) >= 0 Then
                For i As Integer = 0 To arrDR.Length - 1
                    sv_oNode = New TreeNode(arrDR(i).Item("sRoleName"))
                    sv_oNode.Tag = arrDR(i).Item("iRole")
                    sv_oNode.NodeFont = New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
                    AddNewTreeNode(pv_oTvw, pv_oNode, sv_oNode.Tag, sv_oNode.Text, IIf(arrDR(i).Item("FK_iFunctionID").Equals(Nothing) Or arrDR(i).Item("FK_iFunctionID").Equals(DBNull.Value), -1, arrDR(i).Item("FK_iFunctionID")), globalModule.ImageIndex.LeafRole, globalModule.ImageIndex.LeafRole)
                    CreateChildNode(pv_oTvw, pv_oTvw.SelectedNode, sv_oNode.Tag)
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CreateUserNode(ByVal pv_otvw As TreeView, ByVal pv_oNode As TreeNode, ByVal pv_oImgIndex As ImageIndex, ByVal pv_oSelectedImageIndex As ImageIndex)
        Dim i As Integer = 0
        Try
            If mv_DSForImport.Tables("Sys_USERS").Rows.Count > 0 Then
                For i = 0 To mv_DSForImport.Tables("Sys_USERS").Rows.Count - 1
                    Dim sv_oNode As New TreeNode
                    With sv_oNode
                        .Text = mv_DSForImport.Tables("Sys_USERS").Rows(i)("PK_sUID").ToString
                        .Tag = "LEAFUSER#"
                        .ForeColor = System.Drawing.Color.Navy
                        .SelectedImageIndex = pv_oSelectedImageIndex
                        .ImageIndex = pv_oImgIndex
                        .NodeFont = New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
                    End With
                    pv_oNode.Nodes.Add(sv_oNode)
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

    Private Sub BuildTreeView(ByRef pv_oTvw As TreeView)
        Try
            If Not mv_oUserNode Is Nothing Then
                tvwUserOutput.Nodes.Add(mv_oUserNode.Clone)
                tvwUserOutput.SelectedNode = tvwUserOutput.Nodes(0)
                tvwUserOutput.Nodes(0).Expand()
            End If
            '-----------------------------------------------
            If Not mv_oRoleNode Is Nothing Then
                pv_oTvw.Nodes.Add(mv_oRoleNode.Clone)
                pv_oTvw.Nodes(0).Expand()
                '-----------------------------------------------
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RecursiveNode(ByVal oNode As TreeNode)
        For Each _node As TreeNode In oNode.Nodes
            _node.Checked = oNode.Checked
            RecursiveNode(_node)
        Next
    End Sub
    Private Sub tvwUserOutput_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvwUserOutput.MouseDown
        If Not tvwUserOutput.SelectedNode Is tvwUserOutput.GetNodeAt(e.X, e.Y) Then tvwUserOutput.SelectedNode = tvwUserOutput.GetNodeAt(e.X, e.Y)
    End Sub

    Private Sub tvwRoleOutput_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvwRoleOutput.MouseDown
        If Not tvwRoleOutput.SelectedNode Is tvwRoleOutput.GetNodeAt(e.X, e.Y) Then tvwRoleOutput.SelectedNode = tvwRoleOutput.GetNodeAt(e.X, e.Y)
    End Sub
    Private Sub tvwDbRole_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvwDbRole.MouseDown
        If Not tvwDbRole.SelectedNode Is tvwDbRole.GetNodeAt(e.X, e.Y) Then tvwDbRole.SelectedNode = tvwDbRole.GetNodeAt(e.X, e.Y)
    End Sub

    Private Sub tvwUserOutput_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwUserOutput.AfterCheck
        RecursiveNode(e.Node)
    End Sub

    Private Sub tvwRoleOutput_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwRoleOutput.AfterCheck
        Try
            'RecursiveNode(e.Node)
        Catch ex As Exception

        End Try
    End Sub


    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub


    Private Sub TabControl1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TabControl1.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If TabControl1.SelectedIndex = TabControl1.TabCount - 1 Then
                    TabControl1.SelectedIndex = 0
                Else
                    TabControl1.SelectedIndex += 1
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdListFunction_Output_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdListFunction_Output.CurrentCellChanged
        Try
            grdListFunction_Output.Select(grdListFunction_Output.CurrentRowIndex)
            grdListFunction_Output.CurrentCell = New DataGridCell(grdListFunction_Output.CurrentRowIndex, 0)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdListFunction_Output_Mouseup(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdListFunction_Output.MouseUp
        Try
            If e.Button = MouseButtons.Left Then
                If grdListFunction_Output.VisibleRowCount > 0 Then
                    Dim hti As DataGrid.HitTestInfo = grdListFunction_Output.HitTest(e.X, e.Y)
                    If hti.Row >= 0 Then
                        grdListFunction_Output.CurrentRowIndex = hti.Row
                        grdListFunction_Output.Item(hti.Row, 1) = Not grdListFunction_Output.Item(hti.Row, 1)
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdListParam_output_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdListParam_output.CurrentCellChanged
        Try
            grdListParam_output.Select(grdListParam_output.CurrentRowIndex)
            grdListParam_output.CurrentCell = New DataGridCell(grdListParam_output.CurrentRowIndex, 0)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdListParam_output_Mouseup(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdListParam_output.MouseUp
        Try
            If grdListParam_output.VisibleRowCount > 0 Then
                Dim hti As DataGrid.HitTestInfo = grdListParam_output.HitTest(e.X, e.Y)
                If hti.Row >= 0 Then
                    grdListParam_output.Item(hti.Row, 1) = Not grdListParam_output.Item(hti.Row, 1)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ClickUser(Optional ByVal pv_sUserName As String = "")
        Try
            If mv_bOutIn Then
                If pv_sUserName.Trim = String.Empty Then
                    mv_DSRoleForUsers.Tables(0).DefaultView.RowFilter = "1=1"
                Else
                    mv_DSRoleForUsers.Tables(0).DefaultView.RowFilter = "sUID='" & pv_sUserName & "'"
                End If
            Else
                If pv_sUserName.Trim = String.Empty Then
                    mv_DSForImport.Tables("Sys_RFU").DefaultView.RowFilter = "1=1"
                Else
                    mv_DSForImport.Tables("Sys_RFU").DefaultView.RowFilter = "sUID='" & pv_sUserName & "'"
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tvwUserOutput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvwUserOutput.Click
        Try
            Dim sv_sUID As String
            If tvwUserOutput.SelectedNode.Tag = "ROOTUSER" Then
                sv_sUID = String.Empty
                chkCheckAll3.Visible = False
                chkReserved3.Visible = False
            Else
                chkCheckAll3.Visible = True
                chkReserved3.Visible = True
                sv_sUID = tvwUserOutput.SelectedNode.Text
            End If
            ClickUser(sv_sUID)
        Catch ex As Exception

        End Try
    End Sub


    Private Sub grdRoleForUser_output_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdRoleForUser_output.CurrentCellChanged
        Try
            grdRoleForUser_output.Select(grdRoleForUser_output.CurrentRowIndex)
            grdRoleForUser_output.CurrentCell = New DataGridCell(grdRoleForUser_output.CurrentRowIndex, 0)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdRoleForUser_output_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdRoleForUser_output.MouseUp
        Try
            If e.Button = MouseButtons.Left Then
                If grdRoleForUser_output.VisibleRowCount > 0 Then
                    Dim hti As DataGrid.HitTestInfo = grdRoleForUser_output.HitTest(e.X, e.Y)
                    If hti.Row >= 0 Then
                        grdRoleForUser_output.CurrentRowIndex = hti.Row
                        grdRoleForUser_output.Item(hti.Row, 1) = Not grdRoleForUser_output.Item(hti.Row, 1)
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkCheckAll1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCheckAll1.CheckedChanged
        Try
            If mv_bOutIn Then
                For Each dr As DataRow In mv_DSFunctions.Tables(0).Rows
                    dr("CHON") = IIf(chkCheckAll1.Checked, "T", "F")
                Next
                mv_DSFunctions.Tables(0).AcceptChanges()
            Else
                For Each dr As DataRow In mv_DSForImport.Tables("Sys_Functions").Rows
                    dr("CHON") = IIf(chkCheckAll1.Checked, "T", "F")
                Next
                mv_DSForImport.Tables("Sys_Functions").AcceptChanges()
            End If
            grdListFunction_Output.Refresh()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkReserved1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkReserved1.CheckedChanged
        Try
            If mv_bOutIn Then
                For Each dr As DataRow In mv_DSFunctions.Tables(0).Rows
                    dr("CHON") = IIf(dr("CHON") = "T", "F", "T")
                Next
                mv_DSFunctions.Tables(0).AcceptChanges()
            Else
                For Each dr As DataRow In mv_DSForImport.Tables("Sys_Functions").Rows
                    dr("CHON") = IIf(dr("CHON") = "T", "F", "T")
                Next
                mv_DSForImport.Tables("Sys_Functions").AcceptChanges()
            End If
            grdListFunction_Output.Refresh()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkCheckAll2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCheckAll2.CheckedChanged
        Try
            If mv_bOutIn Then
                For Each dr As DataRow In mv_DSParams.Tables(0).Rows
                    dr("CHON") = IIf(chkCheckAll2.Checked, "T", "F")
                Next
                mv_DSParams.Tables(0).AcceptChanges()
            Else
                For Each dr As DataRow In mv_DSForImport.Tables("Sys_Params").Rows
                    dr("CHON") = IIf(chkCheckAll2.Checked, "T", "F")
                Next
                mv_DSForImport.Tables("Sys_Params").AcceptChanges()
            End If
            grdListParam_output.Refresh()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkReserved2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkReserved2.CheckedChanged
        Try
            If mv_bOutIn Then
                For Each dr As DataRow In mv_DSParams.Tables(0).Rows
                    dr("CHON") = IIf(dr("CHON") = "T", "F", "T")
                Next
                mv_DSParams.Tables(0).AcceptChanges()
            Else
                For Each dr As DataRow In mv_DSForImport.Tables("Sys_Params").Rows
                    dr("CHON") = IIf(dr("CHON") = "T", "F", "T")
                Next
                mv_DSForImport.Tables("Sys_Params").AcceptChanges()
            End If
            grdListParam_output.Refresh()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkCheckAll3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCheckAll3.CheckedChanged
        Try
            If mv_bOutIn Then
                For Each dr As DataRow In mv_DSRoleForUsers.Tables(0).Rows
                    If dr("sUID") = tvwUserOutput.SelectedNode.Text Then
                        dr("CHON") = IIf(chkCheckAll3.Checked, "T", "F")
                    End If
                Next
                mv_DSForImport.Tables("Sys_RFU").AcceptChanges()
            Else
                For Each dr As DataRow In mv_DSForImport.Tables("Sys_RFU").Rows
                    If dr("sUID") = tvwUserOutput.SelectedNode.Text Then
                        dr("CHON") = IIf(chkCheckAll3.Checked, "T", "F")
                    End If
                Next
                mv_DSForImport.Tables("Sys_RFU").AcceptChanges()
            End If
            grdRoleForUser_output.Refresh()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkReserved3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkReserved3.CheckedChanged
        Try
            If mv_bOutIn Then
                For Each dr As DataRow In mv_DSRoleForUsers.Tables(0).Rows
                    If dr("sUID") = tvwUserOutput.SelectedNode.Text Then
                        dr("CHON") = IIf(dr("CHON") = "T", "F", "T")
                    End If
                Next
            Else
                For Each dr As DataRow In mv_DSForImport.Tables("Sys_RFU").Rows
                    If dr("sUID") = tvwUserOutput.SelectedNode.Text Then
                        dr("CHON") = IIf(dr("CHON") = "T", "F", "T")
                    End If
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub optIncludingRolesOfUser_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optIncludingRolesOfUser.CheckedChanged, optOnlyUser.CheckedChanged
        Try
            If sender.name = optIncludingRolesOfUser.Name Then
                grbRoleForUserOption.Enabled = True
            Else
                grbRoleForUserOption.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CopyData()
        Try
            mv_DSRoleForUsers.Tables.Add(gv_dsRolesForUsers.Tables(0).Clone)
            For Each dr As DataRow In gv_dsRolesForUsers.Tables(0).Rows
                Dim _DR As DataRow = mv_DSRoleForUsers.Tables(0).NewRow
                For Each dc As DataColumn In gv_dsRolesForUsers.Tables(0).Columns
                    _DR.Item(dc.ColumnName) = dr(dc.ColumnName)
                Next
                mv_DSRoleForUsers.Tables(0).Rows.Add(_DR)
            Next
            mv_DSRoleForUsers.Tables(0).AcceptChanges()
            '----------------------------------------------------------------
            mv_DSRoleExport.Tables.Add(gv_dsRole.Tables(0).Clone)
            For Each dr As DataRow In gv_dsRole.Tables(0).Rows
                Dim _DR As DataRow = mv_DSRoleExport.Tables(0).NewRow
                For Each dc As DataColumn In gv_dsRole.Tables(0).Columns
                    _DR.Item(dc.ColumnName) = dr(dc.ColumnName)
                Next
                mv_DSRoleExport.Tables(0).Rows.Add(_DR)
            Next
            mv_DSRoleExport.Tables(0).AcceptChanges()
            '----------------------------------------------------------------
            mv_DSFunctions.Tables.Add(gv_dsFunction.Tables(0).Clone)
            For Each dr As DataRow In gv_dsFunction.Tables(0).Rows
                Dim _DR As DataRow = mv_DSFunctions.Tables(0).NewRow
                For Each dc As DataColumn In gv_dsFunction.Tables(0).Columns
                    _DR.Item(dc.ColumnName) = dr(dc.ColumnName)
                Next
                mv_DSFunctions.Tables(0).Rows.Add(_DR)

            Next
            mv_DSFunctions.Tables(0).AcceptChanges()

            '----------------------------------------------------------------
            mv_DSParams.Tables.Add(gv_dsParam.Tables(0).Clone)
            For Each dr As DataRow In gv_dsParam.Tables(0).Rows
                Dim _DR As DataRow = mv_DSParams.Tables(0).NewRow
                For Each dc As DataColumn In gv_dsParam.Tables(0).Columns
                    _DR.Item(dc.ColumnName) = dr(dc.ColumnName)
                Next
                mv_DSParams.Tables(0).Rows.Add(_DR)
            Next
            mv_DSParams.Tables(0).AcceptChanges()

            '------------------------------------------------------------------
            '------------------------------------------------------------------
            With grdListFunction_Output
                .TableStyles(0).MappingName = mv_DSFunctions.Tables(0).TableName
                .DataSource = mv_DSFunctions.Tables(0).DefaultView
            End With
            mv_DSFunctions.Tables(0).DefaultView.AllowNew = False
            mv_DSFunctions.Tables(0).DefaultView.AllowDelete = False
            mv_DSFunctions.Tables(0).DefaultView.AllowEdit = True
            With grdListParam_output
                .TableStyles(0).MappingName = mv_DSParams.Tables(0).TableName
                .DataSource = mv_DSParams.Tables(0).DefaultView
            End With
            mv_DSParams.Tables(0).DefaultView.AllowNew = False
            mv_DSParams.Tables(0).DefaultView.AllowDelete = False
            mv_DSParams.Tables(0).DefaultView.AllowEdit = True
            With grdRoleForUser_output
                .TableStyles(0).MappingName = mv_DSRoleForUsers.Tables(0).TableName
                .DataSource = mv_DSRoleForUsers.Tables(0).DefaultView
            End With
            mv_DSRoleForUsers.Tables(0).DefaultView.AllowNew = False
            mv_DSRoleForUsers.Tables(0).DefaultView.AllowDelete = False
            mv_DSRoleForUsers.Tables(0).DefaultView.AllowEdit = True
            '---------------------------------------------------------------
            If mv_DSTbrBtn Is Nothing Then
            Else
                mv_DSXML.Tables.Add(mv_DSTbrBtn.Tables(0).Copy)
            End If
            mv_DSXML.Tables.Add(mv_DSFunctions.Tables(0).Copy)
            mv_DSXML.Tables.Add(mv_DSParams.Tables(0).Copy)
            mv_DSXML.Tables.Add(mv_DSRoleForUsers.Tables(0).Copy)
            mv_DSXML.Tables.Add(gv_dsUser.Tables(0).Copy)
            mv_DSXML.Tables.Add(mv_DSRoleExport.Tables(0).Copy)
        Catch ex As Exception


        End Try
    End Sub

    Private Sub grdListFunction_Output_Navigate(ByVal sender As System.Object, ByVal ne As System.Windows.Forms.NavigateEventArgs) Handles grdListFunction_Output.Navigate

    End Sub


    Private Sub cmdXMLOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdXMLOutput.Click
        Dim SaveDiag As New SaveFileDialog
        SaveDiag.Title = "Chọn thư mục lưu file cấu hình"
        SaveDiag.FileName = "BKSystem.XML"
        SaveDiag.Filter = "XML files|*.xml"
        If SaveDiag.ShowDialog = DialogResult.OK Then
            mv_sFileName = SaveDiag.FileName
            Dim t As Thread
            t = New Thread(AddressOf SaveAllConfig)
            t.ApartmentState = ApartmentState.STA
            t.IsBackground = True
            t.Start()
        End If
    End Sub
    Private Sub SaveAllConfig()
        mv_DSXML.WriteXml(mv_sFileName, XmlWriteMode.WriteSchema)
        MessageBox.Show("Đã lưu file cấu hình thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If mv_bOutIn Then 'Xuất cấu hình được chọn ra file XML
            Dim SaveDiag As New SaveFileDialog
            Dim sv_sPath As String
            SaveDiag.Title = "Chọn thư mục lưu file cấu hình"
            SaveDiag.FileName = "BKSystem.XML"
            SaveDiag.Filter = "XML files|*.xml"
            If SaveDiag.ShowDialog = DialogResult.OK Then
                mv_sFileName = SaveDiag.FileName
                Dim t As Thread
                t = New Thread(AddressOf SaveXMLSelected)
                t.Start()
            End If
        Else 'Cập nhật cấu hình từ File XML
            pgrBar.Visible = True
            lblPercent.Visible = True
            mv_DTNeededRoles.Clear()
            InsertParam()
            InsertFunction()
            InsertUser()
            GetRolesData()
            mv_intAllRoleImport = 0
            GetAllRoleImport(tvwDbRole.Nodes(0))
            '------------------------------------------------------------------------------
            '------------------------------------------------------------------------------
            '**********************************
            lblStatus.Text = "Đang cập nhật Role..."
            pgrBar.Visible = True
            pgrBar.Maximum = mv_intAllRoleImport + 1
            pgrBar.Step = 1
            pgrBar.Value = 0
            pgrBar.Minimum = 0
            '**********************************
            InsertRoles(tvwDbRole.Nodes(0))
            '**********************************
            pgrBar.Visible = False
            '**********************************
            InsertTbrBtnr()
            '------------------------------------------------------------------------------
            '------------------------------------------------------------------------------
            InsertRolesOfUsers(Not optAllRoles.Checked)
            'pgrBar.Visible = False
            lblPercent.Visible = False
            mv_bCancel = False
            gv_bRoleHasChanged = True
            If MessageBox.Show("Qúa trình cập nhật cấu hình thành công. Nhấn OK để quay trở về mà hình quản trị. Nhấn Cancel để quay lại màn hình cập nhật", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = DialogResult.OK Then
                Me.Close()
            End If
        End If
    End Sub
    Private Sub SaveXMLSelected()
        mv_DS.Tables.Clear()
        mv_DS.Clear()
        If Not DTFunctions Is Nothing Then
            DTFunctions.Clear()
        Else
            DTFunctions = mv_DSFunctions.Tables(0).Clone
        End If
        If Not DTUsers Is Nothing Then
            DTUsers.Clear()
        Else
            DTUsers = gv_dsUser.Tables(0).Clone
        End If
        If Not DTRoleForUsers Is Nothing Then
            DTRoleForUsers.Clear()
        Else
            DTRoleForUsers = mv_DSRoleForUsers.Tables(0).Clone
        End If
        If Not DTRoles Is Nothing Then
            DTRoles.Clear()
        Else
            DTRoles = gv_dsRole.Tables(0).Clone
        End If
        If Not DTParams Is Nothing Then
            DTParams.Clear()
        Else
            DTParams = mv_DSParams.Tables(0).Clone
        End If
        lblStatus.Visible = True
        lblStatus.Text = "Lấy thông tin người dùng và quyền người dùng...."
        lblStatus.Refresh()
        'Lấy thông tin người dùng và Role người dùng
        GetUserInfor()
        lblStatus.Text = "Lấy thông tin chức năng...."
        lblStatus.Refresh()
        'Lấy thông tin chức năng
        GetSelectedData(DTFunctions, mv_DSFunctions.Tables(0))
        'Lấy thông tin tham số
        lblStatus.Text = "Lấy thông tin tham số...."
        lblStatus.Refresh()
        GetSelectedData(DTParams, mv_DSParams.Tables(0))
        'Lấy thông tin Roles
        lblStatus.Text = "Lấy thông tin Roles...."
        lblStatus.Refresh()
        GetRolesData()
        lblStatus.Text = "Đã lấy xong toàn bộ dữ liệu"
        lblStatus.Refresh()
        If mv_DSTbrBtn Is Nothing Then
        Else
            mv_DS.Tables.Add(mv_DSTbrBtn.Tables(0).Copy)
        End If
        mv_DS.Tables.Add(DTFunctions.Copy)
        mv_DS.Tables.Add(DTParams.Copy)
        mv_DS.Tables.Add(DTRoleForUsers.Copy)
        mv_DS.Tables.Add(DTUsers.Copy)
        mv_DS.Tables.Add(DTRoles.Copy)
        mv_DS.WriteXml(mv_sFileName, XmlWriteMode.WriteSchema)
        MessageBox.Show("Đã lưu file cấu hình thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
        lblStatus.Visible = False
        mv_bCancel = False
    End Sub
    Private Sub InsertRoles(ByVal pv_oNode As TreeNode)
        Try
            Dim intRoleID As Integer
            Dim intParentRoleID As Integer
            For Each node As TreeNode In pv_oNode.Nodes
                If node.ForeColor.Equals(Color.DarkGreen) Then
                    Dim _clsRole As New clsRole
                    pgrBar.Value += 1
                    lblPercent.Text = CInt(pgrBar.Value * 100 / pgrBar.Maximum).ToString & " %"
                    lblPercent.Refresh()
                    intRoleID = CInt(node.Tag.ToString.Substring(node.Tag.ToString.IndexOf("#") + 1))
                    If node.Parent.Tag = "ROOTROLE#-2" Then
                        intParentRoleID = -2
                    Else
                        intParentRoleID = CInt(node.Parent.Tag.ToString.Substring(node.Parent.Tag.ToString.IndexOf("#") + 1))
                    End If
                    ' .Tag = "LEAFROLES|" & pv_iFunctionID & "#" & pv_iRole
                    Dim dr() As DataRow = mv_DSForImport.Tables("Sys_ROLES").Select("iRole=" & intRoleID)
                    If dr.GetLength(0) > 0 Then
                        'dr(0)("FP_sBranchID")
                        If _clsRole.InsertRoleFromXML(intParentRoleID, gv_sBranchID, dr(0)("sRoleName"), s_IsNothingOrDBNull(dr(0)("sEngRoleName")), dr(0)("iOrder"), Int_IsNothingOrDBNull(dr(0)("FK_iFunctionID"), -1), dr(0)("intShortCutKey"), s_IsNothingOrDBNull(dr(0)("sIconPath")), s_IsNothingOrDBNull(dr(0)("sImgPath")), s_IsNothingOrDBNull(dr(0)("sDesc"))) Then
                            node.Tag = node.Tag.ToString.Substring(0, node.Tag.ToString.IndexOf("#") + 1) & _clsRole.iGetNewestRole
                        End If
                    End If
                    _clsRole = Nothing
                    InsertRoles(node)
                Else
                    InsertRoles(node)
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
    Private Function intGetIndexOfNode(ByVal pv_oNode As TreeNode) As Integer
        Try
            If Not pv_oNode.PrevNode Is Nothing Then

            Else
                Return 1
            End If
        Catch ex As Exception

        End Try
    End Function
    Private Sub InsertRolesOfUsers(ByVal pv_bOnlySelected As Boolean)
        Try
            lblStatus.Text = "Đang cập nhật Roles for Users..."
            pgrBar.Visible = True
            pgrBar.Maximum = mv_DSForImport.Tables("Sys_RFU").Rows.Count
            pgrBar.Step = 1
            pgrBar.Value = 0
            pgrBar.Minimum = 0
            If pv_bOnlySelected Then
                Dim _clsRole As New clsRole
                For Each dr As DataRow In mv_DSForImport.Tables("Sys_RFU").Rows
                    pgrBar.Value += 1
                    lblPercent.Text = CInt(pgrBar.Value * 100 / pgrBar.Maximum).ToString & " %"
                    lblPercent.Refresh()
                    If dr("CHON") = "T" Then
                        If InStr("|" & mv_sInsertUser & "|", "|" & dr("sUID") & "|") > 0 Then
                            _clsRole.bAddRoleForUser(dr("sUID"), dr("iROLEID"), dr("iParentRoleID"), gv_sBranchID)
                            InsertAllParentRole(dr("iParentRoleID"), dr("sUID"), mv_DSForImport.Tables("Sys_RFU"))
                        End If
                    End If
                Next
            Else
                Dim _clsRole As New clsRole
                For Each dr As DataRow In mv_DSForImport.Tables("Sys_RFU").Rows
                    pgrBar.Value += 1
                    lblPercent.Text = CInt(pgrBar.Value * 100 / pgrBar.Maximum).ToString & " %"
                    lblPercent.Refresh()
                    If InStr("|" & mv_sInsertUser & "|", "|" & dr("sUID") & "|") > 0 Then
                        _clsRole.bAddRoleForUser(dr("sUID"), dr("iROLEID"), dr("iParentRoleID"), gv_sBranchID)
                        InsertAllParentRole(dr("iParentRoleID"), dr("sUID"), mv_DSForImport.Tables("Sys_RFU"))
                    End If
                Next
            End If
            lblStatus.Text = "Đã cập nhật xong toàn bộ cấu hình được chọn..."
            pgrBar.Visible = False
        Catch ex As Exception

        End Try
    End Sub
    Private Sub InsertAllParentRole(ByVal pv_iParentRole As Integer, ByVal pv_sUID As String, ByVal pv_DT As DataTable)
        Try
            For Each dr As DataRow In pv_DT.Rows
                If dr("iROLEID") = pv_iParentRole And dr("sUID") = pv_sUID Then
                    Dim _clsRole As New clsRole
                    _clsRole.bAddRoleForUser(dr("sUID"), dr("iROLEID"), dr("iParentRoleID"), gv_sBranchID)
                    _clsRole = Nothing
                    InsertAllParentRole(dr("iParentRoleID"), dr("sUID"), pv_DT)
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GetAllRoleImport(ByVal pv_oNode As TreeNode)
        Try
            For Each node As TreeNode In pv_oNode.Nodes
                If node.ForeColor.Equals(Color.DarkGreen) Then
                    mv_intAllRoleImport += node.GetNodeCount(True) + 1
                Else
                    GetAllRoleImport(node)
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub UpdateRoleID(ByVal pv_oldID As Integer, ByVal pv_NewID As Integer)
        Try
            For Each dr As DataRow In mv_DTNeededRoles.Rows
                If dr("iParentRole") = pv_oldID Then
                    dr("iParentRole") = pv_NewID
                End If
            Next
            mv_DTNeededRoles.AcceptChanges()
            For Each dr As DataRow In mv_DSForImport.Tables("Sys_RFU").Rows
                If dr("iRole") = pv_oldID Then
                    dr("iRole") = pv_NewID
                End If
                If dr("iParentRoleID") = pv_oldID Then
                    dr("iParentRoleID") = pv_NewID
                End If
            Next
            mv_DSForImport.Tables("Sys_RFU").AcceptChanges()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub InsertFunction()
        Try
            lblStatus.Text = "Đang cập nhật chức năng..."
            pgrBar.Visible = True
            pgrBar.Maximum = mv_DSForImport.Tables("Sys_Functions").Rows.Count
            pgrBar.Value = 0
            pgrBar.Minimum = 0
            Dim _clsFunction As New clsFunction
            If Not mv_DSForImport.Tables("Sys_Functions").Columns.Contains("newID") Then
                'mv_DSForImport.Tables("Sys_Functions").Columns.Add(New DataColumn("newID",TypeOf String )) 
            End If
            For Each dr As DataRow In mv_DSForImport.Tables("Sys_Functions").Rows
                pgrBar.Value += 1
                lblPercent.Text = CInt(pgrBar.Value * 100 / pgrBar.Maximum).ToString & " %"
                lblPercent.Refresh()
                If dr("CHON") = "T" Then
                    If _clsFunction.bIsexited(dr("sFunctionName"), gv_sBranchID) Then
                        _clsFunction.bUpdate(gv_sBranchID, dr("sFunctionName"), dr("sDLLname"), dr("sFormName"), dr("sAssemblyName"), dr("bEnable"), dr("sParameterList"), dr("sDesc"))
                    Else
                        _clsFunction.bAddNew(gv_sBranchID, dr("PK_iID"), dr("sFunctionName"), dr("sDLLname"), dr("sFormName"), dr("sAssemblyName"), dr("bEnable"), dr("sDesc"), False, 1)
                       
                        Dim mv_iID As Integer = _clsFunction.GetBiggestID
                        dr("newID") = mv_iID
                        Dim arrdr() As DataRow = mv_DSForImport.Tables("Sys_ROLES").Select("FK_iFunctionID=" & dr("PK_iID"))
                        If arrdr.GetLength(0) > 0 Then
                            arrdr(0)("FK_iFunctionID") = mv_iID
                            mv_DSForImport.Tables("Sys_ROLES").AcceptChanges()
                        End If

                    End If
                End If
            Next
            pgrBar.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub InsertUser()
        Try
            mv_sInsertUser = String.Empty
            lblStatus.Text = "Đang cập nhật người dùng..."
            pgrBar.Visible = True
            pgrBar.Maximum = mv_DSForImport.Tables("Sys_USERS").Rows.Count
            pgrBar.Value = 0
            pgrBar.Minimum = 0
            Dim _clsUser As New clsUser

            For Each _node As TreeNode In tvwUserOutput.Nodes(0).Nodes
                If _node.Checked Then
                    Dim sv_sUID As String = _node.Text
                    Dim dr() As DataRow
                    dr = mv_DSForImport.Tables("Sys_USERS").Select("PK_sUID='" & sv_sUID & "'")
                    If dr.GetLength(0) > 0 Then
                        pgrBar.Value += 1
                        lblPercent.Text = CInt(pgrBar.Value * 100 / pgrBar.Maximum).ToString & " %"
                        lblPercent.Refresh()
                        'dr(0)("FP_sBranchID")
                        If _clsUser.bIsExisted(dr(0)("PK_sUID"), gv_sBranchID) Then
                            _clsUser.UpdateUser(gv_sBranchID, dr(0)("PK_sUID"), dr(0)("sFullName"), dr(0)("sDepart"), dr(0)("sDesc"))
                        Else
                            _clsUser.InsertUser(gv_sBranchID, dr(0)("PK_sUID"), dr(0)("sFullName"), dr(0)("sPwd"), dr(0)("iSecurityLevel"), dr(0)("sDepart"), dr(0)("sDesc"))
                        End If
                        mv_sInsertUser &= sv_sUID & "|"
                    End If
                End If
            Next
            If mv_sInsertUser.Trim <> String.Empty Then
                mv_sInsertUser = mv_sInsertUser.Substring(0, mv_sInsertUser.Length - 1)
            End If
            pgrBar.Visible = False
        Catch ex As Exception

        End Try
    End Sub
    Private Sub InsertTbrBtnr()
        Try
            lblStatus.Text = "Đang cập nhật ToolbarCollection..."
            pgrBar.Visible = True
            pgrBar.Maximum = mv_DSForImport.Tables("Sys_ToolBarButton").Rows.Count
            pgrBar.Value = 0
            pgrBar.Minimum = 0
            Dim _clsTbrButton As New clsTbrButton
            For Each dr As DataRow In mv_DSForImport.Tables("Sys_ToolBarButton").Rows
                If Not _clsTbrButton.bIsExisted(dr("sName"), dr("FP_intRoleID")) Then
                    pgrBar.Value += 1
                    _clsTbrButton.InsertButton(dr("FP_intRoleID"), gv_sBranchID, dr("sText"), dr("sIconPath"), dr("sDesc"), dr("sTTT"), dr("intRolePerformed"), dr("intOrder"), dr("sName"), dr("intStyle"), dr("sRoleName"), dr("intDisplayText"))
                End If
            Next
            pgrBar.Visible = False
        Catch ex As Exception

        End Try
    End Sub
    Private Sub InsertParam()
        Try
            lblStatus.Text = "Đang cập nhật tham số..."
            pgrBar.Visible = True
            pgrBar.Maximum = mv_DSForImport.Tables("Sys_Params").Rows.Count
            pgrBar.Value = 0
            pgrBar.Minimum = 0
            Dim clsParam As New cls_Parameter
            For Each dr As DataRow In mv_DSForImport.Tables("Sys_Params").Rows
                pgrBar.Value += 1
                lblPercent.Text = CInt(pgrBar.Value * 100 / pgrBar.Maximum).ToString & " %"
                lblPercent.Refresh()
                If dr("CHON") = "T" Then
                    If clsParam.bIsexited(dr("sName"), gv_sBranchID) Then
                        'dr("FP_sBranchID")
                        clsParam.bUpdate(gv_sBranchID, dr("sName"), dr("sValue"), dr("sDataType"), dr("sDesc"), dr("iMonth"), dr("iYear"), dr("iStatus"))
                    Else
                        clsParam.bAddNew(dr("ID"), gv_sBranchID, dr("sName"), dr("sValue"), dr("sDataType"), dr("sDesc"), dr("iMonth"), dr("iYear"), 1)
                    End If
                End If
            Next
            pgrBar.Visible = False
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GetUserInfor()
        Dim bhasUser As Boolean = False
        '--------------------------------------------------------------------------------
        For Each _node As TreeNode In tvwUserOutput.Nodes(0).Nodes
            If _node.Checked Then
                bhasUser = True
                Dim sv_sUID As String = _node.Text
                If optIncludingRolesOfUser.Checked Then
                    GetDataFromUserTableToUserTable(DTUsers, gv_dsUser.Tables(0), sv_sUID)
                    If optAllRoles.Checked Then
                        GetDataFromRFUTableToRFUTable(DTRoleForUsers, mv_DSRoleForUsers.Tables(0), sv_sUID)
                    Else
                        GetDataFromRFUTableToRFUTable(DTRoleForUsers, mv_DSRoleForUsers.Tables(0), sv_sUID, True)
                    End If
                Else
                    GetDataFromUserTableToUserTable(DTUsers, gv_dsUser.Tables(0), sv_sUID)
                End If
            End If
        Next

    End Sub
    '----------------------------------------------------------------------------------------------------
    'Mục đích        :Lấy dữ liệu từ một bảng vào một bảng khác
    'Đầu vào         :CÁC THAM SỐ ĐẦU VÀO NHƯ SAU:
    '                :_DesTable:Bảng đích dùng để chứa dữ liệu lấy được từ bảng nguồn
    '                :_SourceTable:Bảng nguồn chứa dữ liệu
    'Đầu ra          :
    'Người tạo       :
    'Ngày tạo        :
    'Last Modified:
    '----------------------------------------------------------------------------------------------------
    Private Sub GetDataFromRFUTableToRFUTable(ByRef _DesTable As DataTable, ByVal _SourceTable As DataTable, ByVal pv_sUID As String, Optional ByVal bOnlySelected As Boolean = False)
        Try
            '-------Nếu chỉ xuất các mục được chọn--------
            If bOnlySelected Then
                For Each dr As DataRow In _SourceTable.Rows
                    If dr("CHON") = "T" And dr("sUID") = pv_sUID Then
                        Dim _DR As DataRow = _DesTable.NewRow
                        For Each dc As DataColumn In _SourceTable.Columns
                            _DR.Item(dc.ColumnName) = dr(dc.ColumnName)
                        Next
                        _DesTable.Rows.Add(_DR)
                        '--------------------------------------------------
                        Dim _DR1() As DataRow
                        _DR1 = gv_dsRole.Tables(0).Select("iRole=" & dr("iROLEID"))
                        If _DR1.GetLength(0) > 0 Then
                            If DTRoles.Select("iRole=" & dr("iROLEID")).GetLength(0) > 0 Then
                            Else
                                Dim _DR2 As DataRow = DTRoles.NewRow
                                For Each dc As DataColumn In gv_dsRole.Tables(0).Columns
                                    _DR2.Item(dc.ColumnName) = _DR1(0)(dc.ColumnName)
                                Next
                                DTRoles.Rows.Add(_DR2)
                                ms_FindAllParentRoles(_DesTable, _SourceTable, CInt(_DR1(0)("iParentRole")))
                            End If
                        End If
                        '--------------------------------------------------
                    End If
                Next
                DTRoles.AcceptChanges()
                _DesTable.AcceptChanges()

            Else
                '-------Nếu xuất tất cả các mục --------
                For Each dr As DataRow In _SourceTable.Rows
                    If dr("sUID") = pv_sUID Then
                        Dim _DR As DataRow = _DesTable.NewRow

                        For Each dc As DataColumn In _SourceTable.Columns
                            _DR.Item(dc.ColumnName) = dr(dc.ColumnName)
                        Next
                        _DesTable.Rows.Add(_DR)
                        '--------------------------------------------------
                        Dim _DR1() As DataRow
                        _DR1 = gv_dsRole.Tables(0).Select("iRole=" & dr("iROLEID"))
                        If _DR1.GetLength(0) > 0 Then
                            If DTRoles.Select("iRole=" & dr("iROLEID")).GetLength(0) > 0 Then
                            Else
                                Dim _DR2 As DataRow = DTRoles.NewRow
                                For Each dc As DataColumn In gv_dsRole.Tables(0).Columns
                                    _DR2.Item(dc.ColumnName) = _DR1(0)(dc.ColumnName)
                                Next
                                DTRoles.Rows.Add(_DR2)
                                ms_FindAllParentRoles(_DesTable, _SourceTable, CInt(_DR1(0)("iParentRole")))
                            End If
                        End If
                        '--------------------------------------------------
                    End If
                Next
                DTRoles.AcceptChanges()
                _DesTable.AcceptChanges()
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub GetDataFromUserTableToUserTable(ByRef _DesTable As DataTable, ByVal _SourceTable As DataTable, ByVal pv_sUID As String)
        Try

            For Each dr As DataRow In _SourceTable.Rows
                If dr("PK_sUID") = pv_sUID Then
                    Dim _DR As DataRow = _DesTable.NewRow
                    For Each dc As DataColumn In _SourceTable.Columns
                        _DR.Item(dc.ColumnName) = dr(dc.ColumnName)
                    Next
                    _DesTable.Rows.Add(_DR)
                End If
            Next
            _DesTable.AcceptChanges()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GetSelectedData(ByRef _DesTable As DataTable, ByVal _SourceTable As DataTable)
        Try
            For Each dr As DataRow In _SourceTable.Rows
                If dr("CHON") = "T" Then
                    Dim _DR As DataRow = _DesTable.NewRow
                    For Each dc As DataColumn In _SourceTable.Columns
                        _DR.Item(dc.ColumnName) = dr(dc.ColumnName)
                    Next
                    _DesTable.Rows.Add(_DR)
                End If
            Next

            _DesTable.AcceptChanges()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub GetRolesData()
        Try
            If mv_bOutIn Then
                For Each _node As TreeNode In tvwRoleOutput.Nodes
                    If _node.Checked Then
                        RecurSiveNodeForRole(_node)
                        ms_FindAllParentRoles(_node)
                    Else
                        RecurSiveNodeForRoleNotSelected(_node)
                    End If
                Next
                DTRoles.AcceptChanges()
            Else
                'lblStatus.Text = "Đang lấy danh sách các Role..."
                'mv_DTNeededRoles = mv_DSForImport.Tables("Sys_ROLES").Clone
                'For Each _node As TreeNode In tvwRoleOutput.Nodes
                '    If _node.Checked Then
                '        RecurSiveNodeForRole(_node)
                '        ms_FindAllParentRoles(_node)
                '    Else
                '        RecurSiveNodeForRoleNotSelected(_node)
                '    End If
                'Next
                ''----Tìm các Role trong Role of user
                'If mv_sInsertUser <> String.Empty Then
                '    For Each dr As DataRow In mv_DSForImport.Tables("Sys_RFU").Rows
                '        If InStr("|" & mv_sInsertUser & "|", "|" & dr("sUID") & "|") > 0 Then
                '            '--------------------------------------------------
                '            Dim _DR1() As DataRow
                '            _DR1 = mv_DTNeededRoles.Select("iRole=" & dr("iROLEID"))
                '            If _DR1.GetLength(0) > 0 Then 'Đã có Role này-->Không thêm nữa
                '            Else 'Chưa có Role-->Thêm vào bảng
                '                Dim _DR() As DataRow = mv_DSForImport.Tables("Sys_ROLES").Select("iRole=" & dr("iROLEID"))
                '                If _DR.GetLength(0) > 0 Then
                '                    Dim _DR2 As DataRow = mv_DTNeededRoles.NewRow
                '                    For Each dc As DataColumn In mv_DSForImport.Tables("Sys_ROLES").Columns
                '                        _DR2.Item(dc.ColumnName) = _DR(0)(dc.ColumnName)
                '                    Next
                '                    mv_DTNeededRoles.Rows.Add(_DR2)
                '                End If
                '            End If
                '        End If
                '    Next
                '    mv_DTNeededRoles.AcceptChanges()
                'End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub RecurSiveNodeForRole(ByVal pv_oNode As TreeNode)
        Try
            If mv_bOutIn Then
                For Each _node As TreeNode In pv_oNode.Nodes
                    Dim intRole As Integer
                    Dim DR() As DataRow
                    intRole = CInt(_node.Tag.ToString.Substring(_node.Tag.ToString.IndexOf("#") + 1))
                    DR = gv_dsRole.Tables(0).Select("iRole=" & intRole)
                    If DR.GetLength(0) > 0 Then
                        If DTRoles.Select("iRole=" & intRole).GetLength(0) > 0 Then
                        Else
                            Dim _DR As DataRow = DTRoles.NewRow
                            For Each dc As DataColumn In gv_dsRole.Tables(0).Columns
                                _DR.Item(dc.ColumnName) = DR(0)(dc.ColumnName)
                            Next
                            DTRoles.Rows.Add(_DR)
                        End If
                    End If
                    RecurSiveNodeForRole(_node)
                Next
            Else 'Lấy Role để Import
                For Each _node As TreeNode In pv_oNode.Nodes
                    Dim intRole As Integer
                    Dim DR() As DataRow
                    intRole = CInt(_node.Tag.ToString.Substring(_node.Tag.ToString.IndexOf("#") + 1))
                    DR = mv_DSForImport.Tables("Sys_ROLES").Select("iRole=" & intRole)
                    If DR.GetLength(0) > 0 Then
                        If mv_DTNeededRoles.Select("iRole=" & intRole).GetLength(0) > 0 Then
                        Else
                            Dim _DR As DataRow = mv_DTNeededRoles.NewRow
                            For Each dc As DataColumn In mv_DSForImport.Tables("Sys_ROLES").Columns
                                _DR.Item(dc.ColumnName) = DR(0)(dc.ColumnName)
                            Next
                            mv_DTNeededRoles.Rows.Add(_DR)
                        End If
                    End If
                    RecurSiveNodeForRole(_node)
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub RecurSiveNodeForRoleNotSelected(ByVal pv_oNode As TreeNode)
        Try
            For Each _node As TreeNode In pv_oNode.Nodes
                If _node.Checked Then
                    RecurSiveNodeForRole(_node)
                    ms_FindAllParentRoles(_node)
                Else
                    RecurSiveNodeForRoleNotSelected(_node)
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Duyệt đệ quy để gán quyền cho một User khi chọn quyền trên cây. Thủ tục này lấy về
    '                        Tất cả các ParentRole của Role hiện thời
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub ms_FindAllParentRoles(ByVal pv_oNode As TreeNode)
        Dim sv_iRoleID As Integer
        Dim sv_oDRRole() As DataRow
        Try
            If mv_bOutIn Then
                If pv_oNode.Text.ToUpper.Equals("ROLES") Then
                    Return
                End If
                'Lấy về mã Role con(Là RoleID của Node hiện thời)
                sv_iRoleID = CInt(pv_oNode.Tag.ToString.Substring(pv_oNode.Tag.ToString.IndexOf("#") + 1))
                'Tìm Node cấp trên của Node đó. Nếu chưa phải là node gốc thì tiếp tục tìm cha
                If Not pv_oNode.Parent.Text.ToUpper.Equals("ROLES") Then
                    If DTRoles.Select("iRole=" & sv_iRoleID).GetLength(0) = 0 Then
                        sv_oDRRole = gv_dsRole.Tables(0).Select("iRole=" & sv_iRoleID)
                        If sv_oDRRole.GetLength(0) > 0 Then
                            If DTRoles.Select("iRole=" & sv_iRoleID).GetLength(0) > 0 Then
                            Else
                                Dim _DR As DataRow = DTRoles.NewRow
                                For Each dc As DataColumn In gv_dsRole.Tables(0).Columns
                                    _DR.Item(dc.ColumnName) = sv_oDRRole(0)(dc.ColumnName)
                                Next
                                DTRoles.Rows.Add(_DR)
                            End If
                        End If
                    End If
                    ms_FindAllParentRoles(pv_oNode.Parent)
                Else 'Nếu cha là ROLES thì con là Node phân hệ-->Mã cha mặc định=-2
                    sv_iRoleID = CInt(pv_oNode.Tag.ToString.Substring(pv_oNode.Tag.ToString.IndexOf("#") + 1))
                    If DTRoles.Select("iRole=" & sv_iRoleID).GetLength(0) = 0 Then
                        sv_oDRRole = gv_dsRole.Tables(0).Select("iRole=" & sv_iRoleID)
                        If sv_oDRRole.GetLength(0) > 0 Then
                            If DTRoles.Select("iRole=" & sv_iRoleID).GetLength(0) > 0 Then

                            Else
                                Dim _DR As DataRow = DTRoles.NewRow
                                For Each dc As DataColumn In gv_dsRole.Tables(0).Columns
                                    _DR.Item(dc.ColumnName) = sv_oDRRole(0)(dc.ColumnName)
                                Next
                                DTRoles.Rows.Add(_DR)
                            End If
                        End If
                    End If
                    Return
                End If
            Else
                If pv_oNode.Text.ToUpper.Equals("ROLES") Then
                    Return
                End If
                'Lấy về mã Role con(Là RoleID của Node hiện thời)
                sv_iRoleID = CInt(pv_oNode.Tag.ToString.Substring(pv_oNode.Tag.ToString.IndexOf("#") + 1))
                'Tìm Node cấp trên của Node đó. Nếu chưa phải là node gốc thì tiếp tục tìm cha
                If Not pv_oNode.Parent.Text.ToUpper.Equals("ROLES") Then
                    If mv_DTNeededRoles.Select("iRole=" & sv_iRoleID).GetLength(0) = 0 Then
                        sv_oDRRole = mv_DSForImport.Tables("Sys_ROLES").Select("iRole=" & sv_iRoleID)
                        If sv_oDRRole.GetLength(0) > 0 Then
                            If mv_DTNeededRoles.Select("iRole=" & sv_iRoleID).GetLength(0) > 0 Then
                            Else
                                Dim _DR As DataRow = mv_DTNeededRoles.NewRow
                                For Each dc As DataColumn In mv_DSForImport.Tables("Sys_ROLES").Columns
                                    _DR.Item(dc.ColumnName) = sv_oDRRole(0)(dc.ColumnName)
                                Next
                                mv_DTNeededRoles.Rows.Add(_DR)
                            End If
                        End If
                    End If
                    ms_FindAllParentRoles(pv_oNode.Parent)
                Else 'Nếu cha là ROLES thì con là Node phân hệ-->Mã cha mặc định=-2
                    sv_iRoleID = CInt(pv_oNode.Tag.ToString.Substring(pv_oNode.Tag.ToString.IndexOf("#") + 1))
                    If mv_DTNeededRoles.Select("iRole=" & sv_iRoleID).GetLength(0) = 0 Then
                        sv_oDRRole = mv_DSForImport.Tables("Sys_ROLES").Select("iRole=" & sv_iRoleID)
                        If sv_oDRRole.GetLength(0) > 0 Then
                            If mv_DTNeededRoles.Select("iRole=" & sv_iRoleID).GetLength(0) > 0 Then

                            Else
                                Dim _DR As DataRow = mv_DTNeededRoles.NewRow
                                For Each dc As DataColumn In mv_DSForImport.Tables("Sys_ROLES").Columns
                                    _DR.Item(dc.ColumnName) = sv_oDRRole(0)(dc.ColumnName)
                                Next
                                mv_DTNeededRoles.Rows.Add(_DR)
                            End If
                        End If
                    End If
                    Return
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Duyệt đệ quy để gán quyền cho một User khi chọn quyền trên cây. Thủ tục này lấy về
    '                        Tất cả các ParentRole của Role hiện thời
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Sub ms_FindAllParentRoles(ByRef DesTable As DataTable, ByVal SourceTable As DataTable, ByVal pv_intParentRole As Integer)
        Dim sv_iRoleID As Integer
        Dim sv_oDRRole() As DataRow
        Try
            'Tự động lấy các Role User liên quan
            'Tự động lấy các Role liên quan
            'Lấy về mã ParentRole
            sv_oDRRole = gv_dsRole.Tables(0).Select("iRole=" & pv_intParentRole)
            If sv_oDRRole.GetLength(0) > 0 Then
                If DTRoles.Select("iRole=" & pv_intParentRole).GetLength(0) > 0 Then
                Else
                    Dim _DR As DataRow = DTRoles.NewRow
                    For Each dc As DataColumn In gv_dsRole.Tables(0).Columns
                        _DR.Item(dc.ColumnName) = sv_oDRRole(0)(dc.ColumnName)
                    Next
                    DTRoles.Rows.Add(_DR)
                    '--------------------------------------------------
                    'Tìm RoleForUser
                    Dim ArrdrRFU() As DataRow
                    ArrdrRFU = SourceTable.Select("iRoleID=" & pv_intParentRole)
                    If ArrdrRFU.GetLength(0) > 0 Then
                        If DesTable.Select("iRoleID=" & pv_intParentRole).GetLength(0) > 0 Then
                        Else
                            Dim _DrRFU As DataRow = DesTable.NewRow
                            For Each dc As DataColumn In SourceTable.Columns
                                _DrRFU.Item(dc.ColumnName) = ArrdrRFU(0)(dc.ColumnName)
                            Next
                            DesTable.Rows.Add(_DrRFU)
                        End If
                    End If
                    '--------------------------------------------------
                    ms_FindAllParentRoles(DesTable, SourceTable, CInt(sv_oDRRole(0)("iParentRole")))
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : 
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub tvwRoleOutput_ItemDrag(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles tvwRoleOutput.ItemDrag
        Try
            If e.Button = MouseButtons.Left Then
                If InStr(CType(e.Item, TreeNode).Tag.ToString.ToUpper, "ROOTROLE") <= 0 Then
                    'invoke the drag and drop operation
                    DoDragDrop(e.Item, DragDropEffects.Move Or DragDropEffects.Copy)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : 
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub tvwRoleOutput_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvwRoleOutput.DragEnter, tvwDbRole.DragEnter
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", False) Then
            If InStr(CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode).Tag.ToString.ToUpper, "ROOTROLE") <= 0 Then
                e.Effect = DragDropEffects.Copy
            End If
        End If
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Sự kiện kéo thả Role giữa các phân hệ làm việc
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub tvwDbRole_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvwDbRole.DragDrop
        Dim DragNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)
        Dim TemNode As TreeNode
        Dim DestinationNode As TreeNode
        Dim intSourceRoleID, intSourceParentRoleID, intDesParentRoleID As Integer
        Dim iFuntionID As Integer ' Mã chức năng của Node được kéo
        Dim _clsRole As New clsRole
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", False) Then
            Dim pt As Point
            pt = CType(sender, TreeView).PointToClient(New Point(e.X, e.Y))
            DestinationNode = CType(sender, TreeView).GetNodeAt(pt)
            If DestinationNode Is Nothing Then
                If tvwDbRole.GetNodeCount(True) > 0 Then
                    DestinationNode = tvwDbRole.Nodes(0)
                End If
            End If
            'Lấy về tất cả các Node con của Node được kéo thả
            mv_arrNode.Clear()
            getAllChildNode(DragNode)
            If Not DestinationNode Is Nothing Then
                'Kiểm tra đảm bảo chỉ thực hiện kéo thả giữa các Role Node và Node nguồn không được là Node phân hệ
                If Not DestinationNode.ForeColor.Equals(Color.DarkGreen) Then
                    'Kiểm tra xem node đích và Node nguồn có là một hay không?
                    If Not DestinationNode Is DragNode Then
                        'Ngăn không cho kéo Node vào chính cha của nó
                        If Not DestinationNode Is DragNode.Parent Then
                            'Kiểm tra đảm bảo nút kéo phân hệ chỉ được thả vào nút Role
                            If (DestinationNode.Tag = "ROOTROLE#-2" And DragNode.Parent.Tag = "ROOTROLE#-2") Or (DestinationNode.Tag <> "ROOTROLE#-2" And DragNode.Parent.Tag <> "ROOTROLE#-2") Then
                                'Kiểm tra đảm bảo DestinationNode không là LeafNode
                                If bIsLeafRoleHasChild(DestinationNode) Then

                                    'Lấy về mã Role và Mã ParentRole
                                    intSourceRoleID = CInt(DragNode.Tag.ToString.Substring(DragNode.Tag.ToString.IndexOf("#") + 1))
                                    intSourceParentRoleID = CInt(DestinationNode.Tag.ToString.Substring(DestinationNode.Tag.ToString.IndexOf("#") + 1))
                                    'If txtFunctionID.Text.Trim = String.Empty Then
                                    '    txtFunctionID.Text = "-1"
                                    'End If
                                    'iFuntionID = CInt(txtFunctionID.Text)
                                    'Gán ChildNode cho DesNode
                                    TemNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode", False), TreeNode).Clone
                                    With TemNode
                                        .ForeColor = Color.DarkGreen
                                        .NodeFont = New Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point)
                                    End With
                                    ChangeColor(TemNode)
                                    '----------------------------------------------------------------------------
                                    'If (e.KeyState And CtrlMask) <> CtrlMask Then 'Cut
                                    '    'Cập nhật vào CSDL
                                    '    UpdateAfterDragAndDrop(intSourceRoleID, intSourceParentRoleID, DestinationNode, DestinationNode.GetNodeCount(False) + 1, GetDataRow(intSourceRoleID), True)
                                    '    'RecursiveNode(DragNode, False)
                                    '    DragNode.Remove()
                                    'Else 'Copy
                                    '    'Cập nhật vào CSDL
                                    '    UpdateAfterDragAndDrop(intSourceRoleID, intSourceParentRoleID, DestinationNode, DestinationNode.GetNodeCount(False) + 1, GetDataRow(intSourceRoleID), False)
                                    '    'Cập nhật lại FunctionID và RoleID cho Role đích
                                    '    With TemNode
                                    '        .Tag = "LEAFROLES|" & iFuntionID & "#" & _clsRole.iGetNewestRole
                                    '    End With
                                    '    'Gọi thủ tục cập nhật đệ quy khi Node được kéo thả là một ParentNode
                                    '    RecursiveNode(TemNode, False)
                                    'End If

                                    DestinationNode.Nodes.Add(TemNode)
                                    '-----------------------------------------------------------------------------
                                    'StatusBar1.Panels(2).Text = "  Đã kéo thả Role thành công  "
                                    tvwDbRole.SelectedNode = DestinationNode
                                    DestinationNode.Expand()

                                Else
                                    'MsgBox("  Không thực hiện kéo thả vì Node nguồn là LeafNode  ")
                                End If
                            Else
                                'MsgBox("  Không thực hiện kéo thả vì Node được kéo thả có cấp cao hơn Node đích")
                            End If
                        Else
                            'MsgBox("  Đã kéo thả Role thành công  ")
                        End If
                    Else

                    End If
                End If
            End If
        End If
    End Sub
    Private Sub getAllChildNode(ByVal pv_oNode As TreeNode)
        Try
            For Each oNode As TreeNode In pv_oNode.Nodes
                mv_arrNode.Add(oNode)
                getAllChildNode(oNode)
            Next
        Catch ex As Exception

        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Thực hiện cập nhật vào DataSet và CSDL sau khi thực hiện kéo thả Role
    'Đầu vào          :Mã Role nguồn, Role đích, Node đích, dữ liệu role nguồn, trạng thái cut Role hay Copy Role
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub UpdateAfterDragAndDrop(ByVal pv_iSourceRoleID As Integer, ByVal pv_iDesRole As Integer, ByVal pv_oDesNode As TreeNode, ByVal pv_intOrder As Integer, ByVal pv_DR As DataRow, ByVal pv_bCut As Boolean)
        Try
            If pv_bCut Then  'Nếu là cut thì cập nhật mã cha của Role bị cut?
                For Each sv_oDR As DataRow In gv_dsRole.Tables(0).Rows
                    If sv_oDR.Item("iRole") = pv_iSourceRoleID Then
                        sv_oDR.Item("iParentRole") = pv_iDesRole
                        sv_oDR.Item("iOrder") = pv_intOrder 'pv_oDesNode.GetNodeCount(False) + 1
                        Exit For
                    End If
                Next
                'Update trong DataSet
                gv_dsRole.Tables(0).AcceptChanges()
            Else 'Nếu là Copy thì ko cần làm gì cả
            End If

            'Thực hiện Update trong CSDL sau khi kéo thả
            Dim _clsRole As New clsRole
            _clsRole.UpdateAfterDragAndDrop(pv_iSourceRoleID, pv_iDesRole, pv_intOrder, pv_DR, pv_bCut)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RecursiveNode(ByVal pv_oSourceNode As TreeNode, ByVal pv_bCut As Boolean)
        Try
            For Each oNode As TreeNode In pv_oSourceNode.Nodes
                Dim _clsRole As New clsRole
                Dim intSourceRoleID As Integer = CInt(oNode.Tag.ToString.Substring(oNode.Tag.ToString.IndexOf("#") + 1))
                Dim intSourceParentRoleID As Integer = CInt(pv_oSourceNode.Tag.ToString.Substring(pv_oSourceNode.Tag.ToString.IndexOf("#") + 1))
                'Cập nhật vào CSDL
                UpdateAfterDragAndDrop(intSourceRoleID, intSourceParentRoleID, pv_oSourceNode, intGetOrderOfNode(pv_oSourceNode, oNode), GetDataRow(intSourceRoleID), pv_bCut)
                'Cập nhật lại Tag cho childNode
                oNode.Tag = "LEAFROLES|" & GetDataRow(_clsRole.iGetNewestRole)("FK_iFunctionID") & "#" & _clsRole.iGetNewestRole
                RecursiveNode(oNode, pv_bCut)
                _clsRole = Nothing
            Next
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ChangeColor(ByVal pv_oNode As TreeNode)
        Try
            For Each node As TreeNode In pv_oNode.Nodes
                With node
                    .ForeColor = pv_oNode.ForeColor
                End With
                ChangeColor(node)
            Next
        Catch ex As Exception

        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Kiểm tra xem Node có phải là Node lá và có con không
    'Đầu vào          :Node cần kiểm tra
    'Đầu ra            :True= đúng. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Function bIsLeafRoleHasChild(ByVal pv_oNode As TreeNode) As Boolean
        If pv_oNode.Tag = "ROOTROLE#-2" Or (InStr(pv_oNode.Tag.ToString.ToUpper, "LEAFROLES") > 0 And bhasNoFunction(pv_oNode)) Then
            Return True
        Else
            Return False
        End If
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Kiểm tra xem Node có phải là Node lá cuối cùng không
    'Đầu vào          :Node cần kiểm tra
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Function bIsLeafRole(ByVal pv_oNode As TreeNode) As Boolean
        If InStr(pv_oNode.Tag.ToString.ToUpper, "LEAFROLES") > 0 And pv_oNode.GetNodeCount(False) = 0 And bhasNoFunction(pv_oNode) Then
            Return True
        Else
            Return False
        End If
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Kiểm tra xem có phải là Node phân hệ hay không
    'Đầu vào          :Node cần kiểm tra
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Function bIsSubSystemNode(ByVal pv_oNode As TreeNode) As Boolean
        If pv_oNode.Parent.Tag = "ROOTROLE#-2" Then
            Return True
        Else
            Return False
        End If
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Kiểm tra xem Node lá đã gắn chức năng hay chưa
    'Đầu vào          :Node cần kiểm tra
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Function bhasNoFunction(ByVal pv_oNode As TreeNode) As Boolean
        Dim fv_iRole As Integer
        Dim fv_iFuntionID As Integer
        Try
            fv_iRole = CInt(pv_oNode.Tag.ToString.Substring(pv_oNode.Tag.ToString.IndexOf("#") + 1))
            For Each fv_oDR As DataRow In gv_dsRole.Tables(0).Rows
                If fv_oDR.Item("iRole") = fv_iRole Then
                    fv_iFuntionID = fv_oDR.Item("FK_iFunctionID")
                    Exit For
                End If
            Next
            If fv_iFuntionID = -1 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return True
        End Try
    End Function
    Private Function intGetOrderOfNode(ByVal pv_oParentNode As TreeNode, ByVal pv_oChildNode As TreeNode) As Integer
        Dim fv_intReVal As Integer = 0
        Try
            For Each oNode As TreeNode In pv_oParentNode.Nodes
                If oNode.Tag = pv_oChildNode.Tag Then
                    Return fv_intReVal + 1
                Else
                    fv_intReVal += 1
                End If
            Next
        Catch ex As Exception

        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Lấy về hàng dữ liệu tương ứng với RoleID được kéo khi thực hiện kéo thả
    'Đầu vào          :
    'Đầu ra            :Datarow chứa dữ liệu
    'Người tạo       :CuongDV
    'Ngày tạo         :10/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Function GetDataRow(ByVal pv_iRoleID As Integer) As DataRow
        Dim fv_DR As DataRow
        For Each fv_DR In gv_dsRole.Tables(0).Rows
            If fv_DR.Item("iRole") = pv_iRoleID Then
                Return fv_DR
            End If
        Next
    End Function
    Private Sub tvwDbRole_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tvwDbRole.KeyDown
        If e.KeyCode = Keys.Delete Then
            If tvwDbRole.SelectedNode.ForeColor.Equals(Color.DarkGreen) Then
                tvwDbRole.SelectedNode.Remove()
            End If
        End If


    End Sub
    Private Function bCheckUnit() As Boolean

    End Function
    Private Sub tvwRoleOutput_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwRoleOutput.AfterSelect

    End Sub
End Class
