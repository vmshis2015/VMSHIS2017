Imports VB = Microsoft.VisualBasic
Imports System.IO
Imports Crownwood.Magic.Menus
Imports System.Threading
Imports System.Data.SqlClient
Imports DbMetal
Imports VietBa.HISLink.CommonLibrary
Public Class frmManSysApp
    Inherits System.Windows.Forms.Form
    Const CtrlMask As Byte = 8
    Dim mv_oNode As TreeNode
    Dim mv_oTTT As ToolTip
    Dim mv_sUID As String = String.Empty
    Dim mv_GroupID As Integer = 0
    Dim mv_GroupName As String = String.Empty
    Dim mv_oDT As DataTable
    Dim mv_oDTRolesForUser As DataTable
    Dim mv_oDTRolesForGroup As DataTable
    'Khai báo các Context menu cho từng loại Node của cây
    '-------------------------------------------------------------------------------------------------------
    Dim mv_oContextMenuForRootFunction As New ContextMenu 'Menu ngữ cảnh cho node gốc chức năng
    Dim mv_oContextMenuForLeafFunction As New ContextMenu 'Menu ngữ cảnh cho node lá chức năng
    '-------------------------------------------------------------------------------------------------------
    Dim mv_oContextMenuForRootGroup As New ContextMenu 'Menu ngữ cảnh cho node gốc nhóm người dùng
    Dim mv_oContextMenuForLeafGroup As New ContextMenu 'Menu ngữ cảnh cho node lá nhóm người dùng
    '-------------------------------------------------------------------------------------------------------
    Dim mv_oContextMenuForRootUser As New ContextMenu 'Menu ngữ cảnh cho node gốc người dùng
    Dim mv_oContextMenuForLeafUser As New ContextMenu 'Menu ngữ cảnh cho node lá người dùng
    '-------------------------------------------------------------------------------------------------------
    Dim mv_oContextMenuForRootRole As New ContextMenu 'Menu ngữ cảnh cho node gốc quyền
    Dim mv_oContextMenuForSubSystem As New ContextMenu 'Menu ngữ cảnh cho node gốc phân hệ
    '-------------------------------------------------------------------------------------------------------
    Dim mv_oContextMenuForMenuLevel1 As New ContextMenu 'Menu ngữ cảnh cho node gốc Menu cấp 1
    Dim mv_oContextMenuForLeafRole As New ContextMenu 'Menu ngữ cảnh cho node lá quyền
    '-------------------------------------------------------------------------------------------------------
    Dim mv_oContextMenuForLeafRoleWithoutFunction As New ContextMenu 'Menu ngữ cảnh cho node lá quyền chưa gắn hàm
    Dim mv_oContextMenuForLeafRoleHasChild As New ContextMenu 'Menu ngữ cảnh cho node lá quyền có con
    '-------------------------------------------------------------------------------------------------------
    Dim mv_oContextMenuForParam As New ContextMenu 'Menu ngữ cảnh cho node Tham số
    '-------------------------------------------------------------------------------------------------------
    Dim mv_oContextMenuForTbr As New ContextMenu 'Menu ngữ cảnh cho ToolBar của phân hệ
    'Khai báo các Menu dùng trong chương trình

    Dim mnuSystem As New ToolStripMenuItem("System")
    Dim mnuGroupUser As New ToolStripMenuItem("Groups")
    Dim mnuUser As New ToolStripMenuItem("Users")
    Dim mnuFunction As New ToolStripMenuItem("Functions")
    Dim mnuRole As New ToolStripMenuItem("Roles")
    Dim mnuParameter As New ToolStripMenuItem("Parameters")
    Dim mnuUtility As New ToolStripMenuItem("Tiện ích")
    Dim mnuUpdateVersion As New ToolStripMenuItem("Cập nhật phiên bản")
    Dim mnuHelp As New ToolStripMenuItem("Help")
    'Khai báo các biến dùng chung

    Dim mv_intTabIndex As Integer = 0
    Dim mv_intGroupTabIndex As Integer = 0
    Dim mv_arrNode As New ArrayList 'Biến chứa các Node để phục vụ kiểm tra ngăn ko cho kéo Node cấp cao vào Node cấp dưới hơn
    Dim mv_bFound As Boolean = False
    Dim mv_bLoading As Boolean = True 'Biến xác định xem toàn bộ Control trên Form đã được Load hay chưa?
    Dim mv_objThreadFlash As Thread 'KHai báo Thread để chạy cùng với Form Flash
    Dim mv_oFlashForm As New frmSplash

    Public mv_dsIconPathForToolBarButton As New DataSet 'Data Set chứa đường dẫn tới ảnh của các Button
    Public mv_objImageListForToolBarButton As New ImageList
    Public mv_objImgForToolBarButton As New ArrayList
    Friend WithEvents pnlGroupUser As System.Windows.Forms.Panel
    Friend WithEvents Splitter3 As System.Windows.Forms.Splitter
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents tvwRolesForGroup As System.Windows.Forms.TreeView
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents chkIsAdmin As System.Windows.Forms.CheckBox
    Friend WithEvents cmdRemoveRightOfGroup As System.Windows.Forms.Button
    Friend WithEvents cmdGetRightForGroup As System.Windows.Forms.Button
    Friend WithEvents grdRoleForGroup As System.Windows.Forms.DataGridView
    Friend WithEvents pnl1 As System.Windows.Forms.Panel
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents TabCtr As System.Windows.Forms.TabControl
    Friend WithEvents TabPageRole As System.Windows.Forms.TabPage
    Friend WithEvents tvwRoleForUser As System.Windows.Forms.TreeView
    Friend WithEvents TabPageUser As System.Windows.Forms.TabPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkAllRole As System.Windows.Forms.CheckBox
    Friend WithEvents cmdMoveRight As System.Windows.Forms.Button
    Friend WithEvents cmdGetRight As System.Windows.Forms.Button
    Friend WithEvents grdGroup As System.Windows.Forms.DataGridView
    Friend WithEvents grdUser As System.Windows.Forms.DataGrid
    Friend WithEvents Sys_Users As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn33 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents grdRolesForUsers As System.Windows.Forms.DataGrid
    Friend WithEvents SysRFU As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn10 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn11 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn12 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn13 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn36 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn39 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn40 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn41 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn42 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn43 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn44 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents GroupID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IsAdmin As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents iRoleID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents sRoleName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grdFunction As System.Windows.Forms.DataGrid
    Friend WithEvents SysFunctions As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn23 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn24 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn25 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn26 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn27 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn34 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents mnuMain As System.Windows.Forms.MenuStrip
    'Mảng chứa các đường dẫn ảnh 
    Dim mv_oDSForToolBarButton As New DataSet ' Dataset chứa danh sách các Button của mỗi phân hệ
    '-------------------------------------------------------------------------------------------------------
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'AddHandler grdFunction.MouseDown, AddressOf grdFunction_MouseDown
        'AddHandler grdFunction.DoubleClick, AddressOf grdFunction_DoubleClick
        'AddHandler grdFunction.CurrentCellChanged, AddressOf grdFunction_CurrentCellChanged

        'Add any initialization after the InitializeComponent() call
        'Me.Opacity = 0
        gv_bIncreateOrDecrete = True
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
    Friend WithEvents imgAdminnistration As System.Windows.Forms.ImageList
    Friend WithEvents imlQuanTri As System.Windows.Forms.ImageList
    Friend WithEvents ToolBarButton1 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton2 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton3 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton4 As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbrAdminSystem As System.Windows.Forms.ToolBar
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents pnl As System.Windows.Forms.Panel
    Friend WithEvents grbRoleInfor As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblRoleName As System.Windows.Forms.Label
    Friend WithEvents lblFunctionName As System.Windows.Forms.Label
    Friend WithEvents lblDLLName As System.Windows.Forms.Label
    Friend WithEvents lblFormName As System.Windows.Forms.Label
    Friend WithEvents cmdGetFunctionForRole As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn19 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn20 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn21 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn22 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents txtFunctionID As System.Windows.Forms.TextBox
    Friend WithEvents grdChildRole As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridTextBoxColumn28 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn29 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn30 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn31 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn32 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents SysRoles As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents grdParamList As System.Windows.Forms.DataGrid
    Friend WithEvents SysParams As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn14 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn15 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn16 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn17 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents cmdDelFunctionForRole As System.Windows.Forms.Button
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel2 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents StatusBarPanel3 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents ToolBarButton5 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton6 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton7 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton8 As System.Windows.Forms.ToolBarButton
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents colEngName As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents picIcon As System.Windows.Forms.PictureBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents CboValue As System.Windows.Forms.ComboBox
    Friend WithEvents CboName As System.Windows.Forms.ComboBox
    Friend WithEvents tbrSearchTree As System.Windows.Forms.ToolBarButton
    Friend WithEvents grbSubSystemInfor As System.Windows.Forms.GroupBox
    Friend WithEvents picSubSystem As System.Windows.Forms.PictureBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents DataGridTextBoxColumn18 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn35 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents tbrforSubSys As System.Windows.Forms.ToolBar
    Friend WithEvents lblLoading As System.Windows.Forms.Label
    Friend WithEvents label5 As System.Windows.Forms.RichTextBox
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents tvwAdminSystem As System.Windows.Forms.TreeView
    Friend WithEvents DataGridTextBoxColumn37 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents DataGridTextBoxColumn38 As System.Windows.Forms.DataGridTextBoxColumn
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmManSysApp))
        Me.imgAdminnistration = New System.Windows.Forms.ImageList(Me.components)
        Me.imlQuanTri = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolBarButton1 = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton2 = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton3 = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton4 = New System.Windows.Forms.ToolBarButton()
        Me.tbrAdminSystem = New System.Windows.Forms.ToolBar()
        Me.ToolBarButton5 = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton6 = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton7 = New System.Windows.Forms.ToolBarButton()
        Me.tbrSearchTree = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton8 = New System.Windows.Forms.ToolBarButton()
        Me.StatusBar1 = New System.Windows.Forms.StatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel2 = New System.Windows.Forms.StatusBarPanel()
        Me.StatusBarPanel3 = New System.Windows.Forms.StatusBarPanel()
        Me.pnl = New System.Windows.Forms.Panel()
        Me.grdFunction = New System.Windows.Forms.DataGrid()
        Me.SysFunctions = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn34 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn23 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn24 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn25 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn26 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn27 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.pnlGroupUser = New System.Windows.Forms.Panel()
        Me.Splitter3 = New System.Windows.Forms.Splitter()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.tvwRolesForGroup = New System.Windows.Forms.TreeView()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.grdRoleForGroup = New System.Windows.Forms.DataGridView()
        Me.iRoleID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.sRoleName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.chkIsAdmin = New System.Windows.Forms.CheckBox()
        Me.cmdRemoveRightOfGroup = New System.Windows.Forms.Button()
        Me.cmdGetRightForGroup = New System.Windows.Forms.Button()
        Me.grbRoleInfor = New System.Windows.Forms.GroupBox()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.picIcon = New System.Windows.Forms.PictureBox()
        Me.CboValue = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CboName = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblFormName = New System.Windows.Forms.Label()
        Me.lblDLLName = New System.Windows.Forms.Label()
        Me.lblFunctionName = New System.Windows.Forms.Label()
        Me.lblRoleName = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdDelFunctionForRole = New System.Windows.Forms.Button()
        Me.txtFunctionID = New System.Windows.Forms.TextBox()
        Me.cmdGetFunctionForRole = New System.Windows.Forms.Button()
        Me.grbSubSystemInfor = New System.Windows.Forms.GroupBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.picSubSystem = New System.Windows.Forms.PictureBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.lblLoading = New System.Windows.Forms.Label()
        Me.tbrforSubSys = New System.Windows.Forms.ToolBar()
        Me.grdGroup = New System.Windows.Forms.DataGridView()
        Me.GroupID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IsAdmin = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pnl1 = New System.Windows.Forms.Panel()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TabCtr = New System.Windows.Forms.TabControl()
        Me.TabPageRole = New System.Windows.Forms.TabPage()
        Me.tvwRoleForUser = New System.Windows.Forms.TreeView()
        Me.TabPageUser = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.grdRolesForUsers = New System.Windows.Forms.DataGrid()
        Me.SysRFU = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn10 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn11 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn12 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn13 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn36 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn39 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chkAllRole = New System.Windows.Forms.CheckBox()
        Me.cmdMoveRight = New System.Windows.Forms.Button()
        Me.cmdGetRight = New System.Windows.Forms.Button()
        Me.grdUser = New System.Windows.Forms.DataGrid()
        Me.Sys_Users = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn40 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn41 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn42 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn43 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn44 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.label5 = New System.Windows.Forms.RichTextBox()
        Me.grdParamList = New System.Windows.Forms.DataGrid()
        Me.SysParams = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn18 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn14 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn15 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn16 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn37 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn17 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.grdChildRole = New System.Windows.Forms.DataGrid()
        Me.SysRoles = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn35 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn38 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn28 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colEngName = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn29 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn30 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn31 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn32 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn8 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn9 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn19 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn20 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn21 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn22 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.tvwAdminSystem = New System.Windows.Forms.TreeView()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn33 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.mnuMain = New System.Windows.Forms.MenuStrip()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBarPanel3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl.SuspendLayout()
        CType(Me.grdFunction, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlGroupUser.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.grdRoleForGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel7.SuspendLayout()
        Me.grbRoleInfor.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.grbSubSystemInfor.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.picSubSystem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.grdGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TabCtr.SuspendLayout()
        Me.TabPageRole.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.grdRolesForUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.grdUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdParamList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdChildRole, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'imgAdminnistration
        '
        Me.imgAdminnistration.ImageStream = CType(resources.GetObject("imgAdminnistration.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgAdminnistration.TransparentColor = System.Drawing.Color.Transparent
        Me.imgAdminnistration.Images.SetKeyName(0, "")
        Me.imgAdminnistration.Images.SetKeyName(1, "")
        Me.imgAdminnistration.Images.SetKeyName(2, "")
        Me.imgAdminnistration.Images.SetKeyName(3, "")
        Me.imgAdminnistration.Images.SetKeyName(4, "")
        Me.imgAdminnistration.Images.SetKeyName(5, "")
        Me.imgAdminnistration.Images.SetKeyName(6, "")
        Me.imgAdminnistration.Images.SetKeyName(7, "")
        Me.imgAdminnistration.Images.SetKeyName(8, "")
        Me.imgAdminnistration.Images.SetKeyName(9, "")
        Me.imgAdminnistration.Images.SetKeyName(10, "")
        Me.imgAdminnistration.Images.SetKeyName(11, "")
        Me.imgAdminnistration.Images.SetKeyName(12, "")
        Me.imgAdminnistration.Images.SetKeyName(13, "")
        Me.imgAdminnistration.Images.SetKeyName(14, "")
        Me.imgAdminnistration.Images.SetKeyName(15, "")
        Me.imgAdminnistration.Images.SetKeyName(16, "")
        Me.imgAdminnistration.Images.SetKeyName(17, "")
        '
        'imlQuanTri
        '
        Me.imlQuanTri.ImageStream = CType(resources.GetObject("imlQuanTri.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlQuanTri.TransparentColor = System.Drawing.Color.Transparent
        Me.imlQuanTri.Images.SetKeyName(0, "")
        Me.imlQuanTri.Images.SetKeyName(1, "")
        Me.imlQuanTri.Images.SetKeyName(2, "")
        Me.imlQuanTri.Images.SetKeyName(3, "")
        Me.imlQuanTri.Images.SetKeyName(4, "")
        Me.imlQuanTri.Images.SetKeyName(5, "")
        Me.imlQuanTri.Images.SetKeyName(6, "")
        Me.imlQuanTri.Images.SetKeyName(7, "")
        Me.imlQuanTri.Images.SetKeyName(8, "")
        Me.imlQuanTri.Images.SetKeyName(9, "")
        Me.imlQuanTri.Images.SetKeyName(10, "")
        Me.imlQuanTri.Images.SetKeyName(11, "")
        Me.imlQuanTri.Images.SetKeyName(12, "")
        '
        'ToolBarButton1
        '
        Me.ToolBarButton1.ImageIndex = 0
        Me.ToolBarButton1.Name = "ToolBarButton1"
        '
        'ToolBarButton2
        '
        Me.ToolBarButton2.ImageIndex = 1
        Me.ToolBarButton2.Name = "ToolBarButton2"
        Me.ToolBarButton2.ToolTipText = "Người dùng Ctr+N"
        '
        'ToolBarButton3
        '
        Me.ToolBarButton3.ImageIndex = 4
        Me.ToolBarButton3.Name = "ToolBarButton3"
        Me.ToolBarButton3.ToolTipText = "Chức năng Ctr+C"
        '
        'ToolBarButton4
        '
        Me.ToolBarButton4.ImageIndex = 5
        Me.ToolBarButton4.Name = "ToolBarButton4"
        Me.ToolBarButton4.ToolTipText = "Quyền - Role Ctr+R"
        '
        'tbrAdminSystem
        '
        Me.tbrAdminSystem.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.ToolBarButton1, Me.ToolBarButton2, Me.ToolBarButton3, Me.ToolBarButton4, Me.ToolBarButton5, Me.ToolBarButton6, Me.ToolBarButton7, Me.tbrSearchTree, Me.ToolBarButton8})
        Me.tbrAdminSystem.ButtonSize = New System.Drawing.Size(23, 22)
        Me.tbrAdminSystem.DropDownArrows = True
        Me.tbrAdminSystem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbrAdminSystem.ImageList = Me.imgAdminnistration
        Me.tbrAdminSystem.Location = New System.Drawing.Point(0, 24)
        Me.tbrAdminSystem.Name = "tbrAdminSystem"
        Me.tbrAdminSystem.ShowToolTips = True
        Me.tbrAdminSystem.Size = New System.Drawing.Size(908, 28)
        Me.tbrAdminSystem.TabIndex = 2
        '
        'ToolBarButton5
        '
        Me.ToolBarButton5.ImageIndex = 8
        Me.ToolBarButton5.Name = "ToolBarButton5"
        Me.ToolBarButton5.ToolTipText = "Tham số hệ thống Ctr+T"
        '
        'ToolBarButton6
        '
        Me.ToolBarButton6.ImageIndex = 11
        Me.ToolBarButton6.Name = "ToolBarButton6"
        Me.ToolBarButton6.ToolTipText = "Chuyển Role lên trên(Ctr+Up)"
        '
        'ToolBarButton7
        '
        Me.ToolBarButton7.ImageIndex = 12
        Me.ToolBarButton7.Name = "ToolBarButton7"
        Me.ToolBarButton7.ToolTipText = "Chuyển Role xuống dưới(Ctr+Down)"
        '
        'tbrSearchTree
        '
        Me.tbrSearchTree.ImageIndex = 14
        Me.tbrSearchTree.Name = "tbrSearchTree"
        Me.tbrSearchTree.ToolTipText = "Tìm kiếm trên cây"
        '
        'ToolBarButton8
        '
        Me.ToolBarButton8.ImageIndex = 13
        Me.ToolBarButton8.Name = "ToolBarButton8"
        Me.ToolBarButton8.ToolTipText = "Thoát khỏi hệ thống"
        '
        'StatusBar1
        '
        Me.StatusBar1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusBar1.Location = New System.Drawing.Point(0, 483)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1, Me.StatusBarPanel2, Me.StatusBarPanel3})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(908, 22)
        Me.StatusBar1.SizingGrip = False
        Me.StatusBar1.TabIndex = 3
        Me.StatusBar1.TabStop = True
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanel1.Icon = CType(resources.GetObject("StatusBarPanel1.Icon"), System.Drawing.Icon)
        Me.StatusBarPanel1.Name = "StatusBarPanel1"
        Me.StatusBarPanel1.Text = "  System Management Application"
        Me.StatusBarPanel1.Width = 223
        '
        'StatusBarPanel2
        '
        Me.StatusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.StatusBarPanel2.Icon = CType(resources.GetObject("StatusBarPanel2.Icon"), System.Drawing.Icon)
        Me.StatusBarPanel2.Name = "StatusBarPanel2"
        Me.StatusBarPanel2.Width = 31
        '
        'StatusBarPanel3
        '
        Me.StatusBarPanel3.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel3.Icon = CType(resources.GetObject("StatusBarPanel3.Icon"), System.Drawing.Icon)
        Me.StatusBarPanel3.Name = "StatusBarPanel3"
        Me.StatusBarPanel3.Width = 654
        '
        'pnl
        '
        Me.pnl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnl.Controls.Add(Me.grdFunction)
        Me.pnl.Controls.Add(Me.pnlGroupUser)
        Me.pnl.Controls.Add(Me.grbRoleInfor)
        Me.pnl.Controls.Add(Me.grbSubSystemInfor)
        Me.pnl.Controls.Add(Me.grdGroup)
        Me.pnl.Controls.Add(Me.pnl1)
        Me.pnl.Controls.Add(Me.grdUser)
        Me.pnl.Controls.Add(Me.label5)
        Me.pnl.Controls.Add(Me.grdParamList)
        Me.pnl.Controls.Add(Me.grdChildRole)
        Me.pnl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl.Location = New System.Drawing.Point(252, 52)
        Me.pnl.Name = "pnl"
        Me.pnl.Size = New System.Drawing.Size(656, 431)
        Me.pnl.TabIndex = 5
        '
        'grdFunction
        '
        Me.grdFunction.BackgroundColor = System.Drawing.Color.White
        Me.grdFunction.CaptionBackColor = System.Drawing.Color.WhiteSmoke
        Me.grdFunction.CaptionForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdFunction.DataMember = ""
        Me.grdFunction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdFunction.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdFunction.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdFunction.Location = New System.Drawing.Point(0, 0)
        Me.grdFunction.Name = "grdFunction"
        Me.grdFunction.RowHeaderWidth = 5
        Me.grdFunction.Size = New System.Drawing.Size(652, 227)
        Me.grdFunction.TabIndex = 27
        Me.grdFunction.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.SysFunctions})
        '
        'SysFunctions
        '
        Me.SysFunctions.DataGrid = Me.grdFunction
        Me.SysFunctions.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn34, Me.DataGridTextBoxColumn23, Me.DataGridTextBoxColumn24, Me.DataGridTextBoxColumn25, Me.DataGridTextBoxColumn26, Me.DataGridTextBoxColumn27})
        Me.SysFunctions.HeaderForeColor = System.Drawing.SystemColors.ControlText
        '
        'DataGridTextBoxColumn34
        '
        Me.DataGridTextBoxColumn34.Format = ""
        Me.DataGridTextBoxColumn34.FormatInfo = Nothing
        Me.DataGridTextBoxColumn34.Width = 0
        '
        'DataGridTextBoxColumn23
        '
        Me.DataGridTextBoxColumn23.Format = ""
        Me.DataGridTextBoxColumn23.FormatInfo = Nothing
        Me.DataGridTextBoxColumn23.HeaderText = "Mã chức năng"
        Me.DataGridTextBoxColumn23.MappingName = "PK_iID"
        Me.DataGridTextBoxColumn23.Width = 75
        '
        'DataGridTextBoxColumn24
        '
        Me.DataGridTextBoxColumn24.Format = ""
        Me.DataGridTextBoxColumn24.FormatInfo = Nothing
        Me.DataGridTextBoxColumn24.HeaderText = "Tên chức năng"
        Me.DataGridTextBoxColumn24.MappingName = "sFunctionName"
        Me.DataGridTextBoxColumn24.Width = 75
        '
        'DataGridTextBoxColumn25
        '
        Me.DataGridTextBoxColumn25.Format = ""
        Me.DataGridTextBoxColumn25.FormatInfo = Nothing
        Me.DataGridTextBoxColumn25.HeaderText = "Tên thư viện"
        Me.DataGridTextBoxColumn25.MappingName = "sDLLName"
        Me.DataGridTextBoxColumn25.Width = 75
        '
        'DataGridTextBoxColumn26
        '
        Me.DataGridTextBoxColumn26.Format = ""
        Me.DataGridTextBoxColumn26.FormatInfo = Nothing
        Me.DataGridTextBoxColumn26.HeaderText = "Tên hàm"
        Me.DataGridTextBoxColumn26.MappingName = "sFormName"
        Me.DataGridTextBoxColumn26.Width = 75
        '
        'DataGridTextBoxColumn27
        '
        Me.DataGridTextBoxColumn27.Format = ""
        Me.DataGridTextBoxColumn27.FormatInfo = Nothing
        Me.DataGridTextBoxColumn27.HeaderText = "Trạng thái "
        Me.DataGridTextBoxColumn27.MappingName = "bEnable"
        Me.DataGridTextBoxColumn27.Width = 75
        '
        'pnlGroupUser
        '
        Me.pnlGroupUser.Controls.Add(Me.Splitter3)
        Me.pnlGroupUser.Controls.Add(Me.Panel5)
        Me.pnlGroupUser.Controls.Add(Me.Panel6)
        Me.pnlGroupUser.Location = New System.Drawing.Point(535, 10)
        Me.pnlGroupUser.Name = "pnlGroupUser"
        Me.pnlGroupUser.Size = New System.Drawing.Size(92, 64)
        Me.pnlGroupUser.TabIndex = 23
        '
        'Splitter3
        '
        Me.Splitter3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter3.Location = New System.Drawing.Point(0, -230)
        Me.Splitter3.Name = "Splitter3"
        Me.Splitter3.Size = New System.Drawing.Size(92, 22)
        Me.Splitter3.TabIndex = 3
        Me.Splitter3.TabStop = False
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.TabControl2)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(92, 0)
        Me.Panel5.TabIndex = 2
        '
        'TabControl2
        '
        Me.TabControl2.Controls.Add(Me.TabPage3)
        Me.TabControl2.Controls.Add(Me.TabPage4)
        Me.TabControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl2.ImageList = Me.imgAdminnistration
        Me.TabControl2.Location = New System.Drawing.Point(0, 0)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(92, 0)
        Me.TabControl2.TabIndex = 10
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.tvwRolesForGroup)
        Me.TabPage3.ImageIndex = 5
        Me.TabPage3.Location = New System.Drawing.Point(4, 25)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(84, 0)
        Me.TabPage3.TabIndex = 0
        Me.TabPage3.Text = "Cấp quyền theo Role"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'tvwRolesForGroup
        '
        Me.tvwRolesForGroup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvwRolesForGroup.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tvwRolesForGroup.ForeColor = System.Drawing.Color.Navy
        Me.tvwRolesForGroup.HideSelection = False
        Me.tvwRolesForGroup.ImageIndex = 0
        Me.tvwRolesForGroup.ImageList = Me.imgAdminnistration
        Me.tvwRolesForGroup.Location = New System.Drawing.Point(0, 0)
        Me.tvwRolesForGroup.Name = "tvwRolesForGroup"
        Me.tvwRolesForGroup.SelectedImageIndex = 0
        Me.tvwRolesForGroup.Size = New System.Drawing.Size(84, 0)
        Me.tvwRolesForGroup.TabIndex = 7
        '
        'TabPage4
        '
        Me.TabPage4.ImageIndex = 1
        Me.TabPage4.Location = New System.Drawing.Point(4, 25)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(84, 0)
        Me.TabPage4.TabIndex = 1
        Me.TabPage4.Text = "Cấp theo nhóm người dùng"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.grdRoleForGroup)
        Me.Panel6.Controls.Add(Me.Panel7)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel6.Location = New System.Drawing.Point(0, -208)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(92, 272)
        Me.Panel6.TabIndex = 0
        '
        'grdRoleForGroup
        '
        Me.grdRoleForGroup.AllowUserToAddRows = False
        Me.grdRoleForGroup.AllowUserToDeleteRows = False
        Me.grdRoleForGroup.BackgroundColor = System.Drawing.SystemColors.Control
        Me.grdRoleForGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdRoleForGroup.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.iRoleID, Me.sRoleName, Me.Column3, Me.Column4, Me.Column5})
        Me.grdRoleForGroup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdRoleForGroup.Location = New System.Drawing.Point(0, 40)
        Me.grdRoleForGroup.Name = "grdRoleForGroup"
        Me.grdRoleForGroup.RowHeadersWidth = 5
        Me.grdRoleForGroup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdRoleForGroup.Size = New System.Drawing.Size(92, 232)
        Me.grdRoleForGroup.TabIndex = 2
        '
        'iRoleID
        '
        Me.iRoleID.DataPropertyName = "iRoleID"
        Me.iRoleID.HeaderText = "Mã Role"
        Me.iRoleID.Name = "iRoleID"
        Me.iRoleID.ReadOnly = True
        '
        'sRoleName
        '
        Me.sRoleName.DataPropertyName = "sRoleName"
        Me.sRoleName.HeaderText = "Tên Role"
        Me.sRoleName.Name = "sRoleName"
        Me.sRoleName.ReadOnly = True
        Me.sRoleName.Width = 200
        '
        'Column3
        '
        Me.Column3.DataPropertyName = "sFunctionName"
        Me.Column3.HeaderText = "Tên chức năng"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 120
        '
        'Column4
        '
        Me.Column4.DataPropertyName = "sDLLName"
        Me.Column4.HeaderText = "Tên thư viện"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column5
        '
        Me.Column5.DataPropertyName = "sFormName"
        Me.Column5.HeaderText = "Tên hàm"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.chkIsAdmin)
        Me.Panel7.Controls.Add(Me.cmdRemoveRightOfGroup)
        Me.Panel7.Controls.Add(Me.cmdGetRightForGroup)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(92, 40)
        Me.Panel7.TabIndex = 1
        '
        'chkIsAdmin
        '
        Me.chkIsAdmin.Dock = System.Windows.Forms.DockStyle.Right
        Me.chkIsAdmin.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIsAdmin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.chkIsAdmin.Location = New System.Drawing.Point(-36, 0)
        Me.chkIsAdmin.Name = "chkIsAdmin"
        Me.chkIsAdmin.Size = New System.Drawing.Size(128, 40)
        Me.chkIsAdmin.TabIndex = 10
        Me.chkIsAdmin.Text = "Tất cả các quyền"
        '
        'cmdRemoveRightOfGroup
        '
        Me.cmdRemoveRightOfGroup.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveRightOfGroup.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.cmdRemoveRightOfGroup.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdRemoveRightOfGroup.Image = CType(resources.GetObject("cmdRemoveRightOfGroup.Image"), System.Drawing.Image)
        Me.cmdRemoveRightOfGroup.Location = New System.Drawing.Point(52, 4)
        Me.cmdRemoveRightOfGroup.Name = "cmdRemoveRightOfGroup"
        Me.cmdRemoveRightOfGroup.Size = New System.Drawing.Size(36, 32)
        Me.cmdRemoveRightOfGroup.TabIndex = 9
        Me.cmdRemoveRightOfGroup.UseVisualStyleBackColor = False
        '
        'cmdGetRightForGroup
        '
        Me.cmdGetRightForGroup.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.cmdGetRightForGroup.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.cmdGetRightForGroup.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdGetRightForGroup.Image = CType(resources.GetObject("cmdGetRightForGroup.Image"), System.Drawing.Image)
        Me.cmdGetRightForGroup.Location = New System.Drawing.Point(8, 4)
        Me.cmdGetRightForGroup.Name = "cmdGetRightForGroup"
        Me.cmdGetRightForGroup.Size = New System.Drawing.Size(36, 32)
        Me.cmdGetRightForGroup.TabIndex = 8
        Me.cmdGetRightForGroup.UseVisualStyleBackColor = False
        '
        'grbRoleInfor
        '
        Me.grbRoleInfor.Controls.Add(Me.LinkLabel1)
        Me.grbRoleInfor.Controls.Add(Me.GroupBox2)
        Me.grbRoleInfor.Controls.Add(Me.GroupBox1)
        Me.grbRoleInfor.Controls.Add(Me.cmdDelFunctionForRole)
        Me.grbRoleInfor.Controls.Add(Me.txtFunctionID)
        Me.grbRoleInfor.Controls.Add(Me.cmdGetFunctionForRole)
        Me.grbRoleInfor.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grbRoleInfor.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.grbRoleInfor.Location = New System.Drawing.Point(0, 227)
        Me.grbRoleInfor.Name = "grbRoleInfor"
        Me.grbRoleInfor.Size = New System.Drawing.Size(652, 200)
        Me.grbRoleInfor.TabIndex = 7
        Me.grbRoleInfor.TabStop = False
        '
        'LinkLabel1
        '
        Me.LinkLabel1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.Location = New System.Drawing.Point(16, 16)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(148, 23)
        Me.LinkLabel1.TabIndex = 18
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Tìm chức năng cho Role"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.picIcon)
        Me.GroupBox2.Controls.Add(Me.CboValue)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.CboName)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Maroon
        Me.GroupBox2.Location = New System.Drawing.Point(468, 44)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(180, 152)
        Me.GroupBox2.TabIndex = 17
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Phím tắt và biểu tượng"
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Navy
        Me.Label7.Location = New System.Drawing.Point(12, 74)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(68, 23)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Biểu tượng"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'picIcon
        '
        Me.picIcon.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.picIcon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picIcon.Location = New System.Drawing.Point(84, 70)
        Me.picIcon.Name = "picIcon"
        Me.picIcon.Size = New System.Drawing.Size(36, 28)
        Me.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picIcon.TabIndex = 4
        Me.picIcon.TabStop = False
        '
        'CboValue
        '
        Me.CboValue.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CboValue.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboValue.Location = New System.Drawing.Point(84, 104)
        Me.CboValue.Name = "CboValue"
        Me.CboValue.Size = New System.Drawing.Size(88, 24)
        Me.CboValue.TabIndex = 3
        Me.CboValue.Visible = False
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Navy
        Me.Label6.Location = New System.Drawing.Point(12, 42)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(68, 23)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "Phím tắt"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CboName
        '
        Me.CboName.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CboName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboName.Location = New System.Drawing.Point(84, 42)
        Me.CboName.Name = "CboName"
        Me.CboName.Size = New System.Drawing.Size(92, 24)
        Me.CboName.Sorted = True
        Me.CboName.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lblFormName)
        Me.GroupBox1.Controls.Add(Me.lblDLLName)
        Me.GroupBox1.Controls.Add(Me.lblFunctionName)
        Me.GroupBox1.Controls.Add(Me.lblRoleName)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Maroon
        Me.GroupBox1.Location = New System.Drawing.Point(4, 44)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(468, 152)
        Me.GroupBox1.TabIndex = 16
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Thông tin"
        '
        'lblFormName
        '
        Me.lblFormName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFormName.BackColor = System.Drawing.Color.Snow
        Me.lblFormName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFormName.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblFormName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblFormName.Location = New System.Drawing.Point(76, 112)
        Me.lblFormName.Name = "lblFormName"
        Me.lblFormName.Size = New System.Drawing.Size(384, 24)
        Me.lblFormName.TabIndex = 13
        Me.lblFormName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDLLName
        '
        Me.lblDLLName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDLLName.BackColor = System.Drawing.Color.Snow
        Me.lblDLLName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDLLName.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblDLLName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDLLName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblDLLName.Location = New System.Drawing.Point(76, 84)
        Me.lblDLLName.Name = "lblDLLName"
        Me.lblDLLName.Size = New System.Drawing.Size(384, 24)
        Me.lblDLLName.TabIndex = 12
        Me.lblDLLName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFunctionName
        '
        Me.lblFunctionName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFunctionName.BackColor = System.Drawing.Color.Snow
        Me.lblFunctionName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFunctionName.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblFunctionName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFunctionName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblFunctionName.Location = New System.Drawing.Point(76, 56)
        Me.lblFunctionName.Name = "lblFunctionName"
        Me.lblFunctionName.Size = New System.Drawing.Size(384, 24)
        Me.lblFunctionName.TabIndex = 11
        Me.lblFunctionName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRoleName
        '
        Me.lblRoleName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRoleName.BackColor = System.Drawing.Color.Snow
        Me.lblRoleName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRoleName.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblRoleName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRoleName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblRoleName.Location = New System.Drawing.Point(76, 28)
        Me.lblRoleName.Name = "lblRoleName"
        Me.lblRoleName.Size = New System.Drawing.Size(384, 24)
        Me.lblRoleName.TabIndex = 10
        Me.lblRoleName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Navy
        Me.Label4.Location = New System.Drawing.Point(8, 112)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 23)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Tên hàm:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Navy
        Me.Label3.Location = New System.Drawing.Point(8, 84)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 23)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Thư viện:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Navy
        Me.Label2.Location = New System.Drawing.Point(8, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 23)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Chức năng:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Navy
        Me.Label1.Location = New System.Drawing.Point(8, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Tên Role:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmdDelFunctionForRole
        '
        Me.cmdDelFunctionForRole.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdDelFunctionForRole.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.cmdDelFunctionForRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDelFunctionForRole.Image = CType(resources.GetObject("cmdDelFunctionForRole.Image"), System.Drawing.Image)
        Me.cmdDelFunctionForRole.Location = New System.Drawing.Point(330, 18)
        Me.cmdDelFunctionForRole.Name = "cmdDelFunctionForRole"
        Me.cmdDelFunctionForRole.Size = New System.Drawing.Size(28, 24)
        Me.cmdDelFunctionForRole.TabIndex = 15
        Me.cmdDelFunctionForRole.UseVisualStyleBackColor = False
        '
        'txtFunctionID
        '
        Me.txtFunctionID.Location = New System.Drawing.Point(412, 16)
        Me.txtFunctionID.Name = "txtFunctionID"
        Me.txtFunctionID.Size = New System.Drawing.Size(80, 20)
        Me.txtFunctionID.TabIndex = 14
        Me.txtFunctionID.Visible = False
        '
        'cmdGetFunctionForRole
        '
        Me.cmdGetFunctionForRole.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdGetFunctionForRole.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.cmdGetFunctionForRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdGetFunctionForRole.Image = CType(resources.GetObject("cmdGetFunctionForRole.Image"), System.Drawing.Image)
        Me.cmdGetFunctionForRole.Location = New System.Drawing.Point(294, 18)
        Me.cmdGetFunctionForRole.Name = "cmdGetFunctionForRole"
        Me.cmdGetFunctionForRole.Size = New System.Drawing.Size(28, 24)
        Me.cmdGetFunctionForRole.TabIndex = 7
        Me.cmdGetFunctionForRole.UseVisualStyleBackColor = False
        '
        'grbSubSystemInfor
        '
        Me.grbSubSystemInfor.Controls.Add(Me.TabControl1)
        Me.grbSubSystemInfor.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.grbSubSystemInfor.Location = New System.Drawing.Point(28, 80)
        Me.grbSubSystemInfor.Name = "grbSubSystemInfor"
        Me.grbSubSystemInfor.Size = New System.Drawing.Size(496, 156)
        Me.grbSubSystemInfor.TabIndex = 21
        Me.grbSubSystemInfor.TabStop = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.ImageList = Me.imgAdminnistration
        Me.TabControl1.Location = New System.Drawing.Point(3, 16)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(490, 137)
        Me.TabControl1.TabIndex = 3
        '
        'TabPage1
        '
        Me.TabPage1.BackgroundImage = CType(resources.GetObject("TabPage1.BackgroundImage"), System.Drawing.Image)
        Me.TabPage1.Controls.Add(Me.picSubSystem)
        Me.TabPage1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage1.ImageIndex = 9
        Me.TabPage1.Location = New System.Drawing.Point(4, 23)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(482, 110)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Ảnh nền"
        '
        'picSubSystem
        '
        Me.picSubSystem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picSubSystem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picSubSystem.Location = New System.Drawing.Point(105, 0)
        Me.picSubSystem.Name = "picSubSystem"
        Me.picSubSystem.Size = New System.Drawing.Size(276, 111)
        Me.picSubSystem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picSubSystem.TabIndex = 2
        Me.picSubSystem.TabStop = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.lblLoading)
        Me.TabPage2.Controls.Add(Me.tbrforSubSys)
        Me.TabPage2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage2.ImageIndex = 7
        Me.TabPage2.Location = New System.Drawing.Point(4, 23)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(482, 110)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "ToolBar"
        '
        'lblLoading
        '
        Me.lblLoading.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLoading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLoading.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoading.ForeColor = System.Drawing.Color.Navy
        Me.lblLoading.Location = New System.Drawing.Point(0, 42)
        Me.lblLoading.Name = "lblLoading"
        Me.lblLoading.Size = New System.Drawing.Size(482, 68)
        Me.lblLoading.TabIndex = 1
        Me.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tbrforSubSys
        '
        Me.tbrforSubSys.DropDownArrows = True
        Me.tbrforSubSys.Location = New System.Drawing.Point(0, 0)
        Me.tbrforSubSys.Name = "tbrforSubSys"
        Me.tbrforSubSys.ShowToolTips = True
        Me.tbrforSubSys.Size = New System.Drawing.Size(482, 42)
        Me.tbrforSubSys.TabIndex = 0
        '
        'grdGroup
        '
        Me.grdGroup.AllowUserToAddRows = False
        Me.grdGroup.AllowUserToDeleteRows = False
        Me.grdGroup.BackgroundColor = System.Drawing.Color.White
        Me.grdGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdGroup.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GroupID, Me.GroupName, Me.Column8, Me.IsAdmin})
        Me.grdGroup.Location = New System.Drawing.Point(226, 8)
        Me.grdGroup.Name = "grdGroup"
        Me.grdGroup.RowHeadersWidth = 5
        Me.grdGroup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdGroup.Size = New System.Drawing.Size(104, 32)
        Me.grdGroup.TabIndex = 26
        '
        'GroupID
        '
        Me.GroupID.DataPropertyName = "GroupID"
        Me.GroupID.HeaderText = "GroupID"
        Me.GroupID.Name = "GroupID"
        Me.GroupID.ReadOnly = True
        Me.GroupID.Visible = False
        '
        'GroupName
        '
        Me.GroupName.DataPropertyName = "GroupName"
        Me.GroupName.HeaderText = "GroupName"
        Me.GroupName.Name = "GroupName"
        Me.GroupName.ReadOnly = True
        Me.GroupName.Width = 200
        '
        'Column8
        '
        Me.Column8.DataPropertyName = "Desc"
        Me.Column8.HeaderText = "Desc"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Width = 250
        '
        'IsAdmin
        '
        Me.IsAdmin.DataPropertyName = "IsAdmin"
        Me.IsAdmin.HeaderText = "IsAdmin?"
        Me.IsAdmin.Name = "IsAdmin"
        Me.IsAdmin.ReadOnly = True
        Me.IsAdmin.Visible = False
        '
        'pnl1
        '
        Me.pnl1.Controls.Add(Me.Splitter2)
        Me.pnl1.Controls.Add(Me.Panel2)
        Me.pnl1.Controls.Add(Me.Panel1)
        Me.pnl1.Location = New System.Drawing.Point(6, 10)
        Me.pnl1.Name = "pnl1"
        Me.pnl1.Size = New System.Drawing.Size(49, 30)
        Me.pnl1.TabIndex = 25
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter2.Location = New System.Drawing.Point(0, -264)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(49, 22)
        Me.Splitter2.TabIndex = 3
        Me.Splitter2.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.TabCtr)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(49, 0)
        Me.Panel2.TabIndex = 2
        '
        'TabCtr
        '
        Me.TabCtr.Controls.Add(Me.TabPageRole)
        Me.TabCtr.Controls.Add(Me.TabPageUser)
        Me.TabCtr.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabCtr.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabCtr.ImageList = Me.imgAdminnistration
        Me.TabCtr.Location = New System.Drawing.Point(0, 0)
        Me.TabCtr.Name = "TabCtr"
        Me.TabCtr.SelectedIndex = 0
        Me.TabCtr.Size = New System.Drawing.Size(49, 0)
        Me.TabCtr.TabIndex = 10
        '
        'TabPageRole
        '
        Me.TabPageRole.Controls.Add(Me.tvwRoleForUser)
        Me.TabPageRole.ImageIndex = 5
        Me.TabPageRole.Location = New System.Drawing.Point(4, 25)
        Me.TabPageRole.Name = "TabPageRole"
        Me.TabPageRole.Size = New System.Drawing.Size(41, 0)
        Me.TabPageRole.TabIndex = 0
        Me.TabPageRole.Text = "Cấp quyền theo Role"
        '
        'tvwRoleForUser
        '
        Me.tvwRoleForUser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvwRoleForUser.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tvwRoleForUser.ForeColor = System.Drawing.Color.Navy
        Me.tvwRoleForUser.HideSelection = False
        Me.tvwRoleForUser.ImageIndex = 0
        Me.tvwRoleForUser.ImageList = Me.imgAdminnistration
        Me.tvwRoleForUser.Location = New System.Drawing.Point(0, 0)
        Me.tvwRoleForUser.Name = "tvwRoleForUser"
        Me.tvwRoleForUser.SelectedImageIndex = 0
        Me.tvwRoleForUser.Size = New System.Drawing.Size(41, 0)
        Me.tvwRoleForUser.TabIndex = 7
        '
        'TabPageUser
        '
        Me.TabPageUser.ImageIndex = 1
        Me.TabPageUser.Location = New System.Drawing.Point(4, 25)
        Me.TabPageUser.Name = "TabPageUser"
        Me.TabPageUser.Size = New System.Drawing.Size(41, 0)
        Me.TabPageUser.TabIndex = 1
        Me.TabPageUser.Text = "Cấp theo người dùng"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.grdRolesForUsers)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, -242)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(49, 272)
        Me.Panel1.TabIndex = 0
        '
        'grdRolesForUsers
        '
        Me.grdRolesForUsers.BackgroundColor = System.Drawing.Color.White
        Me.grdRolesForUsers.CaptionBackColor = System.Drawing.Color.WhiteSmoke
        Me.grdRolesForUsers.CaptionFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdRolesForUsers.CaptionForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdRolesForUsers.DataMember = ""
        Me.grdRolesForUsers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdRolesForUsers.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdRolesForUsers.Location = New System.Drawing.Point(0, 40)
        Me.grdRolesForUsers.Name = "grdRolesForUsers"
        Me.grdRolesForUsers.RowHeaderWidth = 5
        Me.grdRolesForUsers.Size = New System.Drawing.Size(49, 232)
        Me.grdRolesForUsers.TabIndex = 2
        Me.grdRolesForUsers.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.SysRFU})
        '
        'SysRFU
        '
        Me.SysRFU.DataGrid = Me.grdRolesForUsers
        Me.SysRFU.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn10, Me.DataGridTextBoxColumn11, Me.DataGridTextBoxColumn12, Me.DataGridTextBoxColumn13, Me.DataGridTextBoxColumn36, Me.DataGridTextBoxColumn39})
        Me.SysRFU.HeaderForeColor = System.Drawing.SystemColors.ControlText
        '
        'DataGridTextBoxColumn10
        '
        Me.DataGridTextBoxColumn10.Format = ""
        Me.DataGridTextBoxColumn10.FormatInfo = Nothing
        Me.DataGridTextBoxColumn10.Width = 0
        '
        'DataGridTextBoxColumn11
        '
        Me.DataGridTextBoxColumn11.Format = ""
        Me.DataGridTextBoxColumn11.FormatInfo = Nothing
        Me.DataGridTextBoxColumn11.HeaderText = "Mã Role"
        Me.DataGridTextBoxColumn11.MappingName = "iRoleID"
        Me.DataGridTextBoxColumn11.Width = 75
        '
        'DataGridTextBoxColumn12
        '
        Me.DataGridTextBoxColumn12.Format = ""
        Me.DataGridTextBoxColumn12.FormatInfo = Nothing
        Me.DataGridTextBoxColumn12.HeaderText = "Tên Role"
        Me.DataGridTextBoxColumn12.MappingName = "sRoleName"
        Me.DataGridTextBoxColumn12.NullText = "Chưa gán"
        Me.DataGridTextBoxColumn12.Width = 200
        '
        'DataGridTextBoxColumn13
        '
        Me.DataGridTextBoxColumn13.Format = ""
        Me.DataGridTextBoxColumn13.FormatInfo = Nothing
        Me.DataGridTextBoxColumn13.HeaderText = "Tên chức năng"
        Me.DataGridTextBoxColumn13.MappingName = "sFunctionName"
        Me.DataGridTextBoxColumn13.NullText = "Chưa gán"
        Me.DataGridTextBoxColumn13.Width = 150
        '
        'DataGridTextBoxColumn36
        '
        Me.DataGridTextBoxColumn36.Format = ""
        Me.DataGridTextBoxColumn36.FormatInfo = Nothing
        Me.DataGridTextBoxColumn36.HeaderText = "Tên thư viện"
        Me.DataGridTextBoxColumn36.MappingName = "sDLLName"
        Me.DataGridTextBoxColumn36.NullText = "Chưa gán"
        Me.DataGridTextBoxColumn36.Width = 80
        '
        'DataGridTextBoxColumn39
        '
        Me.DataGridTextBoxColumn39.Format = ""
        Me.DataGridTextBoxColumn39.FormatInfo = Nothing
        Me.DataGridTextBoxColumn39.HeaderText = "Tên hàm"
        Me.DataGridTextBoxColumn39.MappingName = "sFormName"
        Me.DataGridTextBoxColumn39.NullText = "Chưa gán"
        Me.DataGridTextBoxColumn39.Width = 80
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkAllRole)
        Me.Panel3.Controls.Add(Me.cmdMoveRight)
        Me.Panel3.Controls.Add(Me.cmdGetRight)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(49, 40)
        Me.Panel3.TabIndex = 1
        '
        'chkAllRole
        '
        Me.chkAllRole.Dock = System.Windows.Forms.DockStyle.Right
        Me.chkAllRole.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllRole.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.chkAllRole.Location = New System.Drawing.Point(-79, 0)
        Me.chkAllRole.Name = "chkAllRole"
        Me.chkAllRole.Size = New System.Drawing.Size(128, 40)
        Me.chkAllRole.TabIndex = 10
        Me.chkAllRole.Text = "Tất cả các quyền"
        '
        'cmdMoveRight
        '
        Me.cmdMoveRight.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.cmdMoveRight.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.cmdMoveRight.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdMoveRight.Image = CType(resources.GetObject("cmdMoveRight.Image"), System.Drawing.Image)
        Me.cmdMoveRight.Location = New System.Drawing.Point(30, 4)
        Me.cmdMoveRight.Name = "cmdMoveRight"
        Me.cmdMoveRight.Size = New System.Drawing.Size(36, 32)
        Me.cmdMoveRight.TabIndex = 9
        Me.cmdMoveRight.UseVisualStyleBackColor = False
        '
        'cmdGetRight
        '
        Me.cmdGetRight.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.cmdGetRight.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.cmdGetRight.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdGetRight.Image = CType(resources.GetObject("cmdGetRight.Image"), System.Drawing.Image)
        Me.cmdGetRight.Location = New System.Drawing.Point(-14, 4)
        Me.cmdGetRight.Name = "cmdGetRight"
        Me.cmdGetRight.Size = New System.Drawing.Size(36, 32)
        Me.cmdGetRight.TabIndex = 8
        Me.cmdGetRight.UseVisualStyleBackColor = False
        '
        'grdUser
        '
        Me.grdUser.BackgroundColor = System.Drawing.Color.White
        Me.grdUser.CaptionBackColor = System.Drawing.Color.WhiteSmoke
        Me.grdUser.CaptionFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdUser.CaptionForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdUser.DataMember = ""
        Me.grdUser.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdUser.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdUser.Location = New System.Drawing.Point(228, 46)
        Me.grdUser.Name = "grdUser"
        Me.grdUser.RowHeadersVisible = False
        Me.grdUser.RowHeaderWidth = 5
        Me.grdUser.Size = New System.Drawing.Size(100, 32)
        Me.grdUser.TabIndex = 24
        Me.grdUser.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.Sys_Users})
        '
        'Sys_Users
        '
        Me.Sys_Users.DataGrid = Me.grdUser
        Me.Sys_Users.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn40, Me.DataGridTextBoxColumn41, Me.DataGridTextBoxColumn42, Me.DataGridTextBoxColumn43, Me.DataGridTextBoxColumn44})
        Me.Sys_Users.HeaderFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Sys_Users.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.Sys_Users.SelectionBackColor = System.Drawing.Color.MidnightBlue
        '
        'DataGridTextBoxColumn40
        '
        Me.DataGridTextBoxColumn40.Format = ""
        Me.DataGridTextBoxColumn40.FormatInfo = Nothing
        Me.DataGridTextBoxColumn40.Width = 75
        '
        'DataGridTextBoxColumn41
        '
        Me.DataGridTextBoxColumn41.Format = ""
        Me.DataGridTextBoxColumn41.FormatInfo = Nothing
        Me.DataGridTextBoxColumn41.HeaderText = "Tên đăng nhập"
        Me.DataGridTextBoxColumn41.MappingName = "PK_sUID"
        Me.DataGridTextBoxColumn41.Width = 120
        '
        'DataGridTextBoxColumn42
        '
        Me.DataGridTextBoxColumn42.Format = ""
        Me.DataGridTextBoxColumn42.FormatInfo = Nothing
        Me.DataGridTextBoxColumn42.HeaderText = "Tên đầy đủ"
        Me.DataGridTextBoxColumn42.MappingName = "sFullName"
        Me.DataGridTextBoxColumn42.Width = 200
        '
        'DataGridTextBoxColumn43
        '
        Me.DataGridTextBoxColumn43.Format = ""
        Me.DataGridTextBoxColumn43.FormatInfo = Nothing
        Me.DataGridTextBoxColumn43.HeaderText = "Phòng ban"
        Me.DataGridTextBoxColumn43.MappingName = "sDepart"
        Me.DataGridTextBoxColumn43.Width = 120
        '
        'DataGridTextBoxColumn44
        '
        Me.DataGridTextBoxColumn44.Format = ""
        Me.DataGridTextBoxColumn44.FormatInfo = Nothing
        Me.DataGridTextBoxColumn44.HeaderText = "Mô tả thêm"
        Me.DataGridTextBoxColumn44.MappingName = "sDesc"
        Me.DataGridTextBoxColumn44.Width = 150
        '
        'label5
        '
        Me.label5.BackColor = System.Drawing.Color.WhiteSmoke
        Me.label5.Location = New System.Drawing.Point(108, 38)
        Me.label5.Name = "label5"
        Me.label5.ReadOnly = True
        Me.label5.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.label5.Size = New System.Drawing.Size(100, 36)
        Me.label5.TabIndex = 22
        Me.label5.Text = ""
        '
        'grdParamList
        '
        Me.grdParamList.BackgroundColor = System.Drawing.Color.White
        Me.grdParamList.CaptionBackColor = System.Drawing.Color.WhiteSmoke
        Me.grdParamList.CaptionFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdParamList.CaptionForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdParamList.DataMember = ""
        Me.grdParamList.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdParamList.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdParamList.Location = New System.Drawing.Point(108, 8)
        Me.grdParamList.Name = "grdParamList"
        Me.grdParamList.RowHeaderWidth = 5
        Me.grdParamList.Size = New System.Drawing.Size(112, 32)
        Me.grdParamList.TabIndex = 20
        Me.grdParamList.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.SysParams})
        '
        'SysParams
        '
        Me.SysParams.DataGrid = Me.grdParamList
        Me.SysParams.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn18, Me.DataGridTextBoxColumn14, Me.DataGridTextBoxColumn15, Me.DataGridTextBoxColumn16, Me.DataGridTextBoxColumn37, Me.DataGridTextBoxColumn17})
        Me.SysParams.HeaderFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SysParams.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.SysParams.SelectionBackColor = System.Drawing.Color.MidnightBlue
        '
        'DataGridTextBoxColumn18
        '
        Me.DataGridTextBoxColumn18.Format = ""
        Me.DataGridTextBoxColumn18.FormatInfo = Nothing
        Me.DataGridTextBoxColumn18.Width = 0
        '
        'DataGridTextBoxColumn14
        '
        Me.DataGridTextBoxColumn14.Format = ""
        Me.DataGridTextBoxColumn14.FormatInfo = Nothing
        Me.DataGridTextBoxColumn14.HeaderText = "Tên tham số"
        Me.DataGridTextBoxColumn14.MappingName = "sName"
        Me.DataGridTextBoxColumn14.Width = 120
        '
        'DataGridTextBoxColumn15
        '
        Me.DataGridTextBoxColumn15.Format = ""
        Me.DataGridTextBoxColumn15.FormatInfo = Nothing
        Me.DataGridTextBoxColumn15.HeaderText = "Giá trị tham số"
        Me.DataGridTextBoxColumn15.MappingName = "sValue"
        Me.DataGridTextBoxColumn15.Width = 101
        '
        'DataGridTextBoxColumn16
        '
        Me.DataGridTextBoxColumn16.Format = ""
        Me.DataGridTextBoxColumn16.FormatInfo = Nothing
        Me.DataGridTextBoxColumn16.HeaderText = "Kiểu dữ liệu"
        Me.DataGridTextBoxColumn16.MappingName = "sDataType"
        Me.DataGridTextBoxColumn16.Width = 101
        '
        'DataGridTextBoxColumn37
        '
        Me.DataGridTextBoxColumn37.Format = ""
        Me.DataGridTextBoxColumn37.FormatInfo = Nothing
        Me.DataGridTextBoxColumn37.HeaderText = "Trạng thái"
        Me.DataGridTextBoxColumn37.MappingName = "iStatus"
        Me.DataGridTextBoxColumn37.Width = 60
        '
        'DataGridTextBoxColumn17
        '
        Me.DataGridTextBoxColumn17.Format = ""
        Me.DataGridTextBoxColumn17.FormatInfo = Nothing
        Me.DataGridTextBoxColumn17.HeaderText = "Ý nghĩa"
        Me.DataGridTextBoxColumn17.MappingName = "sDesc"
        Me.DataGridTextBoxColumn17.Width = 150
        '
        'grdChildRole
        '
        Me.grdChildRole.BackgroundColor = System.Drawing.Color.White
        Me.grdChildRole.CaptionBackColor = System.Drawing.Color.WhiteSmoke
        Me.grdChildRole.CaptionForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdChildRole.DataMember = ""
        Me.grdChildRole.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdChildRole.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdChildRole.Location = New System.Drawing.Point(440, 8)
        Me.grdChildRole.Name = "grdChildRole"
        Me.grdChildRole.RowHeaderWidth = 5
        Me.grdChildRole.Size = New System.Drawing.Size(10, 10)
        Me.grdChildRole.TabIndex = 18
        Me.grdChildRole.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.SysRoles})
        '
        'SysRoles
        '
        Me.SysRoles.DataGrid = Me.grdChildRole
        Me.SysRoles.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn35, Me.DataGridTextBoxColumn38, Me.DataGridTextBoxColumn28, Me.colEngName, Me.DataGridTextBoxColumn29, Me.DataGridTextBoxColumn30, Me.DataGridTextBoxColumn31, Me.DataGridTextBoxColumn32})
        Me.SysRoles.HeaderFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SysRoles.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.SysRoles.RowHeaderWidth = 5
        Me.SysRoles.SelectionBackColor = System.Drawing.Color.MidnightBlue
        '
        'DataGridTextBoxColumn35
        '
        Me.DataGridTextBoxColumn35.Format = ""
        Me.DataGridTextBoxColumn35.FormatInfo = Nothing
        Me.DataGridTextBoxColumn35.Width = 0
        '
        'DataGridTextBoxColumn38
        '
        Me.DataGridTextBoxColumn38.Format = ""
        Me.DataGridTextBoxColumn38.FormatInfo = Nothing
        Me.DataGridTextBoxColumn38.MappingName = "iRole"
        Me.DataGridTextBoxColumn38.Width = 0
        '
        'DataGridTextBoxColumn28
        '
        Me.DataGridTextBoxColumn28.Format = ""
        Me.DataGridTextBoxColumn28.FormatInfo = Nothing
        Me.DataGridTextBoxColumn28.HeaderText = "Tên Role"
        Me.DataGridTextBoxColumn28.MappingName = "sRoleName"
        Me.DataGridTextBoxColumn28.Width = 150
        '
        'colEngName
        '
        Me.colEngName.Format = ""
        Me.colEngName.FormatInfo = Nothing
        Me.colEngName.HeaderText = "Eng Name"
        Me.colEngName.MappingName = "sEngRoleName"
        Me.colEngName.Width = 150
        '
        'DataGridTextBoxColumn29
        '
        Me.DataGridTextBoxColumn29.Format = ""
        Me.DataGridTextBoxColumn29.FormatInfo = Nothing
        Me.DataGridTextBoxColumn29.HeaderText = "Tên chức năng"
        Me.DataGridTextBoxColumn29.MappingName = "sFunctionName"
        Me.DataGridTextBoxColumn29.Width = 150
        '
        'DataGridTextBoxColumn30
        '
        Me.DataGridTextBoxColumn30.Format = ""
        Me.DataGridTextBoxColumn30.FormatInfo = Nothing
        Me.DataGridTextBoxColumn30.HeaderText = "Tên DLL"
        Me.DataGridTextBoxColumn30.MappingName = "sDLLName"
        Me.DataGridTextBoxColumn30.Width = 101
        '
        'DataGridTextBoxColumn31
        '
        Me.DataGridTextBoxColumn31.Format = ""
        Me.DataGridTextBoxColumn31.FormatInfo = Nothing
        Me.DataGridTextBoxColumn31.HeaderText = "Tên Form"
        Me.DataGridTextBoxColumn31.MappingName = "sFormName"
        Me.DataGridTextBoxColumn31.Width = 120
        '
        'DataGridTextBoxColumn32
        '
        Me.DataGridTextBoxColumn32.Format = ""
        Me.DataGridTextBoxColumn32.FormatInfo = Nothing
        Me.DataGridTextBoxColumn32.HeaderText = "Mô tả thêm"
        Me.DataGridTextBoxColumn32.MappingName = "sDesc"
        Me.DataGridTextBoxColumn32.Width = 150
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "Tên đăng nhập"
        Me.DataGridTextBoxColumn6.MappingName = "PK_sUID"
        Me.DataGridTextBoxColumn6.NullText = "Không biết"
        Me.DataGridTextBoxColumn6.Width = 101
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = ""
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "Tên đầy đủ"
        Me.DataGridTextBoxColumn7.MappingName = "sFullName"
        Me.DataGridTextBoxColumn7.NullText = "Không biết"
        Me.DataGridTextBoxColumn7.Width = 150
        '
        'DataGridTextBoxColumn8
        '
        Me.DataGridTextBoxColumn8.Format = ""
        Me.DataGridTextBoxColumn8.FormatInfo = Nothing
        Me.DataGridTextBoxColumn8.HeaderText = "Phòng ban"
        Me.DataGridTextBoxColumn8.MappingName = "sDepart"
        Me.DataGridTextBoxColumn8.NullText = "Không biết"
        Me.DataGridTextBoxColumn8.Width = 101
        '
        'DataGridTextBoxColumn9
        '
        Me.DataGridTextBoxColumn9.Format = ""
        Me.DataGridTextBoxColumn9.FormatInfo = Nothing
        Me.DataGridTextBoxColumn9.HeaderText = "Mô tả thêm"
        Me.DataGridTextBoxColumn9.MappingName = "sDesc"
        Me.DataGridTextBoxColumn9.NullText = "Không biết"
        Me.DataGridTextBoxColumn9.Width = 150
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Nothing
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "Mã chức năng"
        Me.DataGridTextBoxColumn5.MappingName = "PK_iID "
        Me.DataGridTextBoxColumn5.Width = 75
        '
        'DataGridTextBoxColumn19
        '
        Me.DataGridTextBoxColumn19.Format = ""
        Me.DataGridTextBoxColumn19.FormatInfo = Nothing
        Me.DataGridTextBoxColumn19.HeaderText = "Tên chức năng"
        Me.DataGridTextBoxColumn19.MappingName = "sFunctionName "
        Me.DataGridTextBoxColumn19.Width = 200
        '
        'DataGridTextBoxColumn20
        '
        Me.DataGridTextBoxColumn20.Format = ""
        Me.DataGridTextBoxColumn20.FormatInfo = Nothing
        Me.DataGridTextBoxColumn20.HeaderText = "Tên thư viện"
        Me.DataGridTextBoxColumn20.MappingName = "sDLLName "
        Me.DataGridTextBoxColumn20.Width = 150
        '
        'DataGridTextBoxColumn21
        '
        Me.DataGridTextBoxColumn21.Format = ""
        Me.DataGridTextBoxColumn21.FormatInfo = Nothing
        Me.DataGridTextBoxColumn21.HeaderText = "Tên hàm"
        Me.DataGridTextBoxColumn21.MappingName = "sFormName "
        Me.DataGridTextBoxColumn21.Width = 150
        '
        'DataGridTextBoxColumn22
        '
        Me.DataGridTextBoxColumn22.Format = ""
        Me.DataGridTextBoxColumn22.FormatInfo = Nothing
        Me.DataGridTextBoxColumn22.HeaderText = "Trạng thái"
        Me.DataGridTextBoxColumn22.MappingName = "bEnable "
        Me.DataGridTextBoxColumn22.Width = 101
        '
        'tvwAdminSystem
        '
        Me.tvwAdminSystem.AllowDrop = True
        Me.tvwAdminSystem.Dock = System.Windows.Forms.DockStyle.Left
        Me.tvwAdminSystem.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tvwAdminSystem.HideSelection = False
        Me.tvwAdminSystem.Location = New System.Drawing.Point(0, 52)
        Me.tvwAdminSystem.Name = "tvwAdminSystem"
        Me.tvwAdminSystem.Size = New System.Drawing.Size(252, 431)
        Me.tvwAdminSystem.TabIndex = 7
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(252, 52)
        Me.Splitter1.MinExtra = 100
        Me.Splitter1.MinSize = 100
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(2, 431)
        Me.Splitter1.TabIndex = 6
        Me.Splitter1.TabStop = False
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "Mô tả thêm"
        Me.DataGridTextBoxColumn4.MappingName = "sDesc"
        Me.DataGridTextBoxColumn4.Width = 150
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "Phòng ban"
        Me.DataGridTextBoxColumn3.MappingName = "sDepart"
        Me.DataGridTextBoxColumn3.Width = 120
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "Tên đầy đủ"
        Me.DataGridTextBoxColumn2.MappingName = "sFullName"
        Me.DataGridTextBoxColumn2.Width = 200
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "Tên đăng nhập"
        Me.DataGridTextBoxColumn1.MappingName = "PK_sUID"
        Me.DataGridTextBoxColumn1.Width = 120
        '
        'DataGridTextBoxColumn33
        '
        Me.DataGridTextBoxColumn33.Format = ""
        Me.DataGridTextBoxColumn33.FormatInfo = Nothing
        Me.DataGridTextBoxColumn33.Width = 0
        '
        'mnuMain
        '
        Me.mnuMain.Location = New System.Drawing.Point(0, 0)
        Me.mnuMain.Name = "mnuMain"
        Me.mnuMain.Size = New System.Drawing.Size(908, 24)
        Me.mnuMain.TabIndex = 8
        Me.mnuMain.Text = "MenuStrip1"
        '
        'frmManSysApp
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(908, 505)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnl)
        Me.Controls.Add(Me.tvwAdminSystem)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.tbrAdminSystem)
        Me.Controls.Add(Me.mnuMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.mnuMain
        Me.Name = "frmManSysApp"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Chương trình quản trị hệ thống"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StatusBarPanel3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl.ResumeLayout(False)
        CType(Me.grdFunction, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlGroupUser.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.TabControl2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        CType(Me.grdRoleForGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel7.ResumeLayout(False)
        Me.grbRoleInfor.ResumeLayout(False)
        Me.grbRoleInfor.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.grbSubSystemInfor.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.picSubSystem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.grdGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.TabCtr.ResumeLayout(False)
        Me.TabPageRole.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.grdRolesForUsers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        CType(Me.grdUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdParamList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdChildRole, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "Các hàm khởi tạo dữ liệu khi chương trình bắt đầu chạy"
    Private Sub Flashing()
        mv_oFlashForm.ShowDialog()
    End Sub
    Private Sub Initialize(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim sv_clsBuildTreeView As New clsBuildTreeView
        'Xây cây TreeView
        Try
            MessageBox.Show("Step 1")
            GC.Collect()
            GetRegValueForOptions()
            tvwAdminSystem.BeginUpdate()
            sv_clsBuildTreeView.BuildTreeView(tvwAdminSystem, imgAdminnistration)
            'Tạo các ContextMenu
            MessageBox.Show("Step 2")
            CreateContextMenu()
            MessageBox.Show("Step 3")
            CreateDefautlMenu()
            'Đưa các chức năng vào DataGrid Chức năng
            With grdFunction
                .TableStyles(0).MappingName = "Sys_Functions"
                .DataSource = gv_dsFunction.Tables(0).DefaultView
                .ContextMenu = mv_oContextMenuForLeafFunction
                .CaptionText = "Tổng số " & gv_dsFunction.Tables(0).Rows.Count & " chức năng"
            End With
            gv_dsFunction.Tables(0).DefaultView.AllowNew = False
            gv_dsFunction.Tables(0).DefaultView.AllowDelete = False
            mv_oDTRolesForUser = gv_dsRolesForUsers.Tables(0).Clone
            mv_oDTRolesForUser.DefaultView.AllowNew = False
            mv_oDTRolesForUser.DefaultView.AllowEdit = False
            mv_oDTRolesForUser.DefaultView.AllowDelete = False
            mv_bLoading = False
            'Chọn mặc định SelectedNode của TreeView là UserNode
            tvwAdminSystem.SelectedNode = tvwAdminSystem.Nodes(0).Nodes(1)
            'Gọi sự kiện Click của TreeView để hiện DataGrid_User
            tvwAdminSystem_Click(sender, e)
            BuildMainMenu(tvwAdminSystem.SelectedNode, tvwAdminSystem.SelectedNode.Tag)
            'Tạo cấu trúc cho bảng lưu trữ quyền của các Users
            'ms_MakeTableStructure()
            If Not mv_bLoading Then StatusBar1.Panels(1).Text = gv_sBranchName
            If Not mv_bLoading Then StatusBar1.Panels(2).Text = "Bạn đang chọn Node người dùng"
            gv_oNode = tvwAdminSystem.Nodes(0).Nodes(3).Clone
            TabPage2.ContextMenu = mv_oContextMenuForTbr
            tvwAdminSystem.EndUpdate()
            gv_dsParam.Tables(0).DefaultView.AllowDelete = False
            gv_dsRole.Tables(0).DefaultView.AllowDelete = False
            gv_dsRolesForUsers.Tables(0).DefaultView.AllowDelete = False
            gv_dsUser.Tables(0).DefaultView.AllowDelete = False
            gv_dsParam.Tables(0).DefaultView.AllowNew = False
            gv_dsRole.Tables(0).DefaultView.AllowNew = False
            gv_dsRolesForUsers.Tables(0).DefaultView.AllowNew = False
            gv_dsUser.Tables(0).DefaultView.AllowNew = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub CreateDefautlMenu()
        mnuSystem.DropDownItems.Clear()
        mnuHelp.DropDownItems.Clear()
        mnuUtility.DropDownItems.Clear()
        mnuUpdateVersion.DropDownItems.Clear()
        mnuSystem.DropDownItems.AddRange(New ToolStripItem() {New ToolStripMenuItem("Thay đổi mật khẩu", Nothing, New EventHandler(AddressOf ChangeAdminPWD)), _
                                                           New ToolStripMenuItem("Backup-Restore DataBase", Nothing, New EventHandler(AddressOf BackUpAndRestoreDB)), _
                                                           New ToolStripMenuItem("Gen code", Nothing, New EventHandler(AddressOf GenCode)), _
                                                           New ToolStripSeparator(), _
                                                           New ToolStripMenuItem("Cập nhật đơn vị làm việc", Nothing, New EventHandler(AddressOf UpdateBranchInfor)), _
                                                           New ToolStripMenuItem("Thêm mới đơn vị làm việc", Nothing, New EventHandler(AddressOf InsertBranchInfor)), _
                                                            New ToolStripSeparator(), _
                                                            New ToolStripMenuItem("Quản lý các Message của hệ thống", Nothing, New EventHandler(AddressOf MsgMan)), _
                                                           New ToolStripMenuItem("Exit", Nothing, New EventHandler(AddressOf ExitApp))})
        mnuHelp.DropDownItems.AddRange(New ToolStripItem() {New ToolStripMenuItem("Help", Nothing, New EventHandler(AddressOf _Help)), _
                                                         New ToolStripMenuItem("About", Nothing, New EventHandler(AddressOf About))})
        mnuUtility.DropDownItems.AddRange(New ToolStripItem() {New ToolStripMenuItem("Tùy chọn cấu hình", Nothing, New EventHandler(AddressOf _Option)), _
                                                            New ToolStripSeparator(), _
                                                            New ToolStripMenuItem("Xuất Role ra file Exel", Nothing, New EventHandler(AddressOf _RoleToExelFile)), _
                                                            New ToolStripMenuItem("Cập nhật danh sách Icon", Nothing, New EventHandler(AddressOf _InsertImgAndIcon)), _
                                                            New ToolStripSeparator(), _
                                                            New ToolStripMenuItem("Nhập cấu hình từ file XML", Nothing, New EventHandler(AddressOf _ConfigurationnInput)), _
                                                            New ToolStripMenuItem("Xuất cấu hình ra file XML", Nothing, New EventHandler(AddressOf _ConfigurationnOutput))})
        mnuUpdateVersion.DropDownItems.AddRange(New ToolStripItem() {New ToolStripMenuItem("Cập nhật phiên bản", Nothing, New EventHandler(AddressOf _UpdateVersion)), _
                                                        New ToolStripMenuItem("Cập nhật phiên bản theo lô", Nothing, New EventHandler(AddressOf _UpdateVersionBatch))})

    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Tạo các ContextMenu để sau này gắn cho TreeView
    'Đầu vào          :
    'Đầu ra            :Các ContextMenu
    'Người tạo       : CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub CreateContextMenu()
        mv_oContextMenuForRootGroup.MenuItems.Clear()
        '---------------------------------CONTEXT MENU FOR Group USERS------------------------------------
        'Với các RootUser thì chỉ có 1 quyền là thêm mới User
        mv_oContextMenuForRootGroup.MenuItems.AddRange(New IconMenuItem() {New IconMenuItem("New Group", Application.StartupPath & "\Icons\Insert.ico", New EventHandler(AddressOf InsertGroupUser_Click))})
        'Với các Node User thì có 3 quyền là cập nhật,xóa User và xóa mật khẩu
        mv_oContextMenuForLeafGroup.MenuItems.Clear()
        mv_oContextMenuForLeafGroup.MenuItems.AddRange(New IconMenuItem() {New IconMenuItem("Update Group", Application.StartupPath & "\Icons\Update.ico", New EventHandler(AddressOf UpdateGroupUser_Click)), _
                                                                                                                           New IconMenuItem("Delete Group", Application.StartupPath & "\Icons\DeleteUser.ico", New EventHandler(AddressOf DeleteGroupUser_Click))})
        '---------------------------------CONTEXT MENU FOR USERS------------------------------------
        mv_oContextMenuForRootUser.MenuItems.Clear()
        '---------------------------------CONTEXT MENU FOR USERS------------------------------------
        'Với các RootUser thì chỉ có 1 quyền là thêm mới User
        mv_oContextMenuForRootUser.MenuItems.AddRange(New IconMenuItem() {New IconMenuItem("Thêm mới người dùng", Application.StartupPath & "\Icons\Insert.ico", New EventHandler(AddressOf InsertUser_Click))})
        'Với các Node User thì có 3 quyền là cập nhật,xóa User và xóa mật khẩu
        mv_oContextMenuForLeafUser.MenuItems.Clear()
        mv_oContextMenuForLeafUser.MenuItems.AddRange(New IconMenuItem() {New IconMenuItem("Cập nhật người dùng", Application.StartupPath & "\Icons\Update.ico", New EventHandler(AddressOf UpdateUser_Click)), _
                                                                                                                           New IconMenuItem("Xóa người dùng", Application.StartupPath & "\Icons\DeleteUser.ico", New EventHandler(AddressOf DeleteUser_Click)), _
                                                                                                                           New IconMenuItem("-"), _
                                                                                                                           New IconMenuItem("Xóa mật khẩu", Application.StartupPath & "\Icons\Delete.ico", New EventHandler(AddressOf ClearPassword_Click)), _
                                                                                                                           New IconMenuItem("Xóa tất cả các người dùng' Password", Application.StartupPath & "\Icons\DeleteAllUsers.ico", New EventHandler(AddressOf ClearAllPassword_Click)), _
 New IconMenuItem("-"), _
 New IconMenuItem("Delegate User", Application.StartupPath & "\Icons\DeleteAllUsers.ico", New EventHandler(AddressOf DelegateUser_Click))})
        '-------------------------------------------------------------------------------------------------------

        '---------------------------------CONTEXT MENU FOR FUNCTIONS------------------------------------
        'Với các RootFunction 
        mv_oContextMenuForRootFunction.MenuItems.Clear()
        mv_oContextMenuForRootFunction.MenuItems.AddRange(New IconMenuItem() {New IconMenuItem("Thêm chức năng", Application.StartupPath & "\Icons\Insert.ico", New EventHandler(AddressOf InsertFunction_Click)), _
                                                                                                                                New IconMenuItem("Kích hoạt tất cả các chức năng", Application.StartupPath & "\Icons\UnlockAll.ico", New EventHandler(AddressOf ActivateAllFunctions_Click)), _
                                                                                                                                New IconMenuItem("-"), _
                                                                                                                                New IconMenuItem("Khóa tất cả các chức năng", Application.StartupPath & "\Icons\LockAll.ico", New EventHandler(AddressOf LockAllFunctions_Click))})
        'Với các LeafFunction
        mv_oContextMenuForLeafFunction.MenuItems.Clear()
        mv_oContextMenuForLeafFunction.MenuItems.AddRange(New IconMenuItem() {New IconMenuItem("Cập nhật chức năng", Application.StartupPath & "\Icons\Update.ico", New EventHandler(AddressOf UpdateFunction_Click)), _
                                                                                                                                New IconMenuItem("Khóa chức năng", New EventHandler(AddressOf LockFunction_Click)), _
                                                                                                                                New IconMenuItem("-"), _
                                                                                                                                New IconMenuItem("Xóa chức năng", Application.StartupPath & "\Icons\Delete.ico", New EventHandler(AddressOf DeleteFunction_Click))})
        '-------------------------------------------------------------------------------------------------------
        '---------------------------------CONTEXT MENU FOR ROLES------------------------------------
        'Với các RootRole
        mv_oContextMenuForRootRole.MenuItems.Clear()
        mv_oContextMenuForRootRole.MenuItems.AddRange(New IconMenuItem() {New IconMenuItem("Thêm phân hệ", Application.StartupPath & "\Icons\Insert.ico", New EventHandler(AddressOf InsertSubSystemClick))})
        'Với các SubSystemRole
        mv_oContextMenuForMenuLevel1.MenuItems.Clear()
        mv_oContextMenuForMenuLevel1.MenuItems.AddRange(New IconMenuItem() {New IconMenuItem("Xóa phân hệ", Application.StartupPath & "\Icons\Delete.ico", New EventHandler(AddressOf DeleteRole_Click)), _
                                                                        New IconMenuItem("Cập nhật phân hệ", Application.StartupPath & "\Icons\Update.ico", New EventHandler(AddressOf UpdateSubSystem_Click)), _
                                                                        New IconMenuItem("Cập nhật ToolBar", Application.StartupPath & "\Icons\InsertToolBar.ico", New EventHandler(AddressOf ShowCollection)), _
                                                                        New IconMenuItem("-"), _
                                                                        New IconMenuItem("Thêm Menu cấp 1", Application.StartupPath & "\Icons\InsertMenu.ico", New EventHandler(AddressOf InsertMenuLevel1_Click)), _
                                                                        New IconMenuItem("Chọn Icon cho phân hệ", Application.StartupPath & "\Icons\ChooseICO.ico", New EventHandler(AddressOf UpdateIcon)), _
                                                                        New IconMenuItem("Chọn ảnh nền cho phân hệ", Application.StartupPath & "\Icons\ChooseImg.ico", New EventHandler(AddressOf UpdateImgPath))})

        'Với các LeafRole thì được chia làm 2 loại.
        'Các LeafRole có ChildNode>0
        mv_oContextMenuForLeafRoleHasChild.MenuItems.Clear()
        mv_oContextMenuForLeafRoleHasChild.MenuItems.AddRange(New IconMenuItem() {New IconMenuItem("Thêm mới Role", Application.StartupPath & "\Icons\Insert.ico", New EventHandler(AddressOf InsertRole_Click)), _
                                                                                                                                        New IconMenuItem("Cập nhật Role", Application.StartupPath & "\Icons\Update.ico", New EventHandler(AddressOf UpdateRole_Click))})
        'Các LeafRole có ChildNode=0 và đã được gắn chức năng
        mv_oContextMenuForLeafRole.MenuItems.Clear()
        mv_oContextMenuForLeafRole.MenuItems.AddRange(New IconMenuItem() {New IconMenuItem("Cập nhật Role", Application.StartupPath & "\Icons\Update.ico", New EventHandler(AddressOf UpdateRole_Click)), _
                                                                                                                        New IconMenuItem("Xóa Role", Application.StartupPath & "\Icons\Delete.ico", New EventHandler(AddressOf DeleteRole_Click)), _
                                                                                                                        New IconMenuItem("-"), _
                                                                                                                        New IconMenuItem("Chuyển tới chức năng", Application.StartupPath & "\Icons\Find.ico", New EventHandler(AddressOf gotoFuntion))})

        'Các LeafRole có ChildNode=0 và chưa được gắn chức năng
        mv_oContextMenuForLeafRoleWithoutFunction.MenuItems.Clear()
        mv_oContextMenuForLeafRoleWithoutFunction.MenuItems.AddRange(New IconMenuItem() {New IconMenuItem("Thêm mới Role", Application.StartupPath & "\Icons\Insert.ico", New EventHandler(AddressOf InsertRole_Click)), _
                                                                                                                     New IconMenuItem("Cập nhật Role", Application.StartupPath & "\Icons\Update.ico", New EventHandler(AddressOf UpdateRole_Click)), _
                                                                                                                     New IconMenuItem("-"), _
                                                                                                                     New IconMenuItem("Xóa Role", Application.StartupPath & "\Icons\Delete.ico", New EventHandler(AddressOf DeleteRole_Click))})


        'Tạo ContextMenu cho Node tham số
        mv_oContextMenuForParam.MenuItems.Clear()
        mv_oContextMenuForParam.MenuItems.AddRange(New MenuItem() {New IconMenuItem("Thêm mới tham số", Application.StartupPath & "\Icons\Insert.ico", New EventHandler(AddressOf InsertParam)), _
                                                                                                                      New IconMenuItem("Cập nhật tham số", Application.StartupPath & "\Icons\Update.ico", New EventHandler(AddressOf UpdateParam)), _
                                                                                                                      New IconMenuItem("Xóa tham số", Application.StartupPath & "\Icons\Delete.ico", New EventHandler(AddressOf DeleteParam)), _
                                                                                                                      New IconMenuItem("Khóa tham số", Application.StartupPath & "\Icons\Lock.ico", New EventHandler(AddressOf ActivateStatus))})
        mv_oContextMenuForTbr.MenuItems.Clear()
        mv_oContextMenuForTbr.MenuItems.Add(New IconMenuItem("Collection", New EventHandler(AddressOf ShowCollection)))
    End Sub
#End Region

#Region "Các sự kiện của Form"

    Private Sub QTHT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddHandler grdFunction.MouseDown, AddressOf grdFunction_MouseDown
        AddHandler grdFunction.DoubleClick, AddressOf grdFunction_DoubleClick
        AddHandler grdFunction.CurrentCellChanged, AddressOf grdFunction_CurrentCellChanged
        gv_oTTT.SetToolTip(picIcon, "Click vào đây để chọn Icon cho chức năng")
        EnvisibleAllControls()
        If File.Exists(Application.StartupPath & "\Introduction.rtf") Then
            label5.LoadFile(Application.StartupPath & "\Introduction.rtf")
        End If
        If LoginSystem(sender, e) Then
        Else
            Application.Exit()
        End If

    End Sub
    Private Function LoginSystem(ByVal sender As System.Object, ByVal e As System.EventArgs) As Boolean
        Dim clsUtil As New clsUtility
        If VNS.Libs.globalVariables.SqlConn Is Nothing Then
            Return False
        End If
        If Not clsUtil.CheckTable("Sys_MANAGEMENTUNIT") Then
            Dim sv_oForm As New Frm_AddBranch
            sv_oForm.mv_bInsert = True
            sv_oForm.ShowDialog()
        End If
        If Not clsUtil.CheckTable("Sys_MANAGEMENTUNIT", "PK_sBranchID=N'" & gv_sBranchID & "'") Then
            MessageBox.Show("Không tồn tại chi nhánh làm việc có mã: " & gv_sBranchID & " trong CSDL. Đề nghị bạn chỉnh lại thông tin file cấu hình cho đúng", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If
        If Not clsUtil.CheckTable("Sys_ADMINISTRATOR", "FP_sBranchID=N'" & gv_sBranchID & "'") Then
            Dim sv_oForm As New InsertUser("Thêm quản trị hệ thống")
            Dim sv_sUserName As String
            Dim sv_bSucess As Boolean = False
            Try
                sv_oForm.ps_iStatus = globalModule.Status.Insert
                sv_oForm.mv_bAdmin = True
                sv_oForm.ShowDialog()
                sv_bSucess = sv_oForm.pb_Success
                sv_sUserName = sv_oForm.ps_UserName
                If sv_bSucess Then
                    MessageBox.Show("Thêm Admin thành công!", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Catch ex As Exception

            End Try
        End If
        Dim sv_oLoginForm As New frmLogin
        sv_oLoginForm.ShowDialog()
        MessageBox.Show("ShowDialog OK")
        Try
            If gv_bLoginSuccess Then
                'Hiện form Flash
                'mv_objThreadFlash = New Thread(AddressOf Flashing)
                'mv_objThreadFlash.Start()
                Application.DoEvents()
                MessageBox.Show("Step 0")
                Initialize(sender, e)
                Application.DoEvents()
                'If mv_oFlashForm.thre.ThreadState = ThreadState.Suspended Then
                '    mv_oFlashForm.thre.Resume()
                'ElseIf mv_oFlashForm.thre.ThreadState = ThreadState.Running Then
                '    mv_oFlashForm.thre.Abort()
                'End If
                'mv_oFlashForm.Close()
                tbrAdminSystem.Buttons(0).Enabled = False
            Else
                MessageBox.Show("gv_bLoginSuccess False")
                tbrAdminSystem.Buttons(0).Enabled = True
                'Me.Close()
            End If

            getShortCutKey()
            Splitter2.Height = 3
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            mv_oFlashForm.thre.Abort()
            mv_oFlashForm.Close()
        End Try
    End Function
    Private Sub QTHT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Escape
                    If MessageBox.Show("Bạn có thực sự muốn thoát khỏi chương trình Quản trị hệ thống không?", gv_sAnnouncement, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Me.Close()
                    Else
                    End If
                Case Keys.F12
                    Dim sv_oForm As New InsertUser("Thêm quản trị hệ thống")
                    Dim sv_sUserName As String
                    Dim sv_bSucess As Boolean = False
                    Try
                        sv_oForm.ps_iStatus = globalModule.Status.Insert
                        sv_oForm.mv_bAdmin = True
                        sv_oForm.ShowDialog()
                        sv_bSucess = sv_oForm.pb_Success
                        sv_sUserName = sv_oForm.ps_UserName
                        If sv_bSucess Then
                            MessageBox.Show("Thêm Admin thành công!", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'Dim sv_oNode As New TreeNode(sv_sUserName)
                            'With sv_oNode
                            '    .Tag = "LEAFUSER#"
                            '    .SelectedImageIndex = ImageIndex.NodeUser
                            '    .ImageIndex = ImageIndex.NodeUser
                            '    .ForeColor = Color.Navy
                            'End With

                            ''Thêm vào DataSet chức năng này
                            'tvwAdminSystem.SelectedNode.Nodes.Add(sv_oNode)
                            'tvwAdminSystem.SelectedNode.ExpandAll()
                        End If
                    Catch ex As Exception

                    End Try
                Case Keys.N
                    If e.Modifiers = Keys.Control Then
                        tvwAdminSystem.SelectedNode = tvwAdminSystem.Nodes(0).Nodes(1)
                    End If
                Case Keys.C
                    If e.Modifiers = Keys.Control Then
                        tvwAdminSystem.SelectedNode = tvwAdminSystem.Nodes(0).Nodes(2)
                    End If
                Case Keys.R
                    If e.Modifiers = Keys.Control Then
                        tvwAdminSystem.SelectedNode = tvwAdminSystem.Nodes(0).Nodes(3)
                    End If
                Case Keys.T
                    If e.Modifiers = Keys.Control Then
                        tvwAdminSystem.SelectedNode = tvwAdminSystem.Nodes(0).Nodes(4)
                    End If
                Case Keys.S
                    If e.Modifiers = Keys.Control Then
                        SearchStart()
                    End If
            End Select
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "EVENT_FOR_PARAMS"
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Thêm mới một tham số
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : CuongDV
    'Ngày tạo         :06/03/2006
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub InsertParam(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim sv_oForm As New frm_AddParameter("Thêm mới tham số")
            Dim sv_sParamName As String
            Dim sv_iID As Integer
            Dim sv_bSucess As Boolean = False
            Try
                sv_oForm.mv_iStatus = 1
                sv_oForm.ShowDialog()

            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try

    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Cập nhật tham số
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : CuongDV
    'Ngày tạo         :06/03/2006
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub UpdateParam(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim sv_oForm As New frm_AddParameter("Cập nhật tham số")
            Dim sv_sParamName As String
            Dim sv_iID As Integer
            Dim sv_bEnable As Boolean
            Try
                sv_oForm.mv_iStatus = 0
                If gv_bCallContextMenuFromTreeView Then
                    sv_oForm.mv_sParamName = sv_sParamName
                    sv_sParamName = tvwAdminSystem.SelectedNode.Text.Trim
                Else
                    sv_oForm.mv_sParamName = grdParamList.Item(grdParamList.CurrentRowIndex, 1).ToString
                    sv_sParamName = grdParamList.Item(grdParamList.CurrentRowIndex, 1).ToString
                End If
                sv_oForm.mv_sParamName = sv_sParamName
                sv_oForm.ShowDialog()
                sv_sParamName = sv_oForm.mv_sParamName
                If sv_oForm.mv_bSuccess Then
                    With tvwAdminSystem.SelectedNode
                        .Text = sv_sParamName
                    End With
                    'Nhảy đến nút kế tiếp hoặc nút trước đó để cho người dùng thấy hiệu quả của thao tác
                    If Not tvwAdminSystem.SelectedNode.NextNode Is Nothing Then
                        tvwAdminSystem.SelectedNode = tvwAdminSystem.SelectedNode.NextNode
                    ElseIf Not tvwAdminSystem.SelectedNode.PrevNode Is Nothing Then
                        tvwAdminSystem.SelectedNode = tvwAdminSystem.SelectedNode.PrevNode
                    End If
                End If
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Xóa tham số
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : CuongDV
    'Ngày tạo         :06/03/2006
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub DeleteParam(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim sv_clsParam As New cls_Parameter
            Dim sParamName As String
            Dim sv_oNode As TreeNode
            Try
                sParamName = tvwAdminSystem.SelectedNode.Text.Trim
                sv_oNode = tvwAdminSystem.SelectedNode
                If MessageBox.Show("Bạn có muốn xóa tham số " & sParamName & " không?", gv_sAnnouncement, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If sv_clsParam.DeleteParam(sParamName, tvwAdminSystem.SelectedNode) Then
                        MessageBox.Show("Đã xóa thành công tham số " & sParamName, gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try

    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Cập nhật trạng thái tham số
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : CuongDV
    'Ngày tạo         :06/03/2006
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub ActivateStatus(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim sv_clsParam As New cls_Parameter
            Dim sName As String
            sName = tvwAdminSystem.SelectedNode.Text.Trim
            If tvwAdminSystem.SelectedNode.ForeColor.Equals(gv_LockedParamColor) Then  'Nếu đang bị khóa thì kích hoạt
                If sv_clsParam.bUpdateParamStatus(sName, True) Then
                    With tvwAdminSystem.SelectedNode
                        .ForeColor = System.Drawing.Color.Navy
                        .SelectedImageIndex = ImageIndex.NodeParam
                        .ImageIndex = ImageIndex.NodeParam
                    End With
                    'Nhảy đến nút kế tiếp hoặc nút trước đó để cho người dùng thấy hiệu quả của thao tác
                    If Not tvwAdminSystem.SelectedNode.NextNode Is Nothing Then
                        tvwAdminSystem.SelectedNode = tvwAdminSystem.SelectedNode.NextNode
                    ElseIf Not tvwAdminSystem.SelectedNode.PrevNode Is Nothing Then
                        tvwAdminSystem.SelectedNode = tvwAdminSystem.SelectedNode.PrevNode
                    End If
                    'Change to DataSet
                    For Each sv_oDR As DataRow In gv_dsParam.Tables(0).Rows
                        If sv_oDR.Item("sName").ToString.ToUpper = sName.ToUpper Then
                            sv_oDR.Item("iStatus") = 1
                            Exit For
                        End If
                    Next
                    If gv_bAnnouceAfterActivatingParam Then
                        MessageBox.Show("Đã kích hoạt tham số " & sName, gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            Else 'Nếu đang kích hoạt thì bị khóa
                If sv_clsParam.bUpdateParamStatus(sName, False) Then
                    With tvwAdminSystem.SelectedNode
                        .ForeColor = gv_LockedParamColor
                    End With
                    'Nhảy đến nút kế tiếp hoặc nút trước đó để cho người dùng thấy hiệu quả của thao tác
                    If Not tvwAdminSystem.SelectedNode.NextNode Is Nothing Then
                        tvwAdminSystem.SelectedNode = tvwAdminSystem.SelectedNode.NextNode
                    ElseIf Not tvwAdminSystem.SelectedNode.PrevNode Is Nothing Then
                        tvwAdminSystem.SelectedNode = tvwAdminSystem.SelectedNode.PrevNode
                    End If
                    'Change to DataSet
                    For Each sv_oDR As DataRow In gv_dsParam.Tables(0).Rows
                        If sv_oDR.Item("sName").ToString.ToUpper = sName.ToUpper Then
                            sv_oDR.Item("iStatus") = 0
                            Exit For
                        End If
                    Next
                    If gv_bAnnouceAfterLockingParam Then
                        MessageBox.Show("Đã khóa tham số " & sName, gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub
#End Region

#Region "EVENT_FOR_FUNCTIONS"
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Thêm mới một chức năng với các thông tin cơ bản
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub InsertFunction_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_oForm As New InsertFunction("Thêm mới chức năng")
        Dim sv_sFunctionName As String
        Dim sv_iID As Integer
        Dim sv_bSucess As Boolean = False
        Try
            sv_oForm.ps_iStatus = 1
            sv_oForm.ShowDialog()
            'sv_bSucess = sv_oForm.pb_Success
            'sv_sFunctionName = sv_oForm.ps_FunctionName
            'sv_iID = sv_oForm.ps_iID
            'If sv_bSucess Then
            '    Dim sv_oNode As New TreeNode(sv_sFunctionName)
            '    With sv_oNode
            '        .Tag = "LEAFFUNCTIONS#" & sv_iID
            '        .SelectedImageIndex = ImageIndex.NodeFuntion
            '        .ImageIndex = ImageIndex.NodeFuntion
            '        .NodeFont = New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
            '    End With

            '    'Thêm vào DataSet chức năng này
            '    tvwAdminSystem.SelectedNode.Nodes.Add(sv_oNode)
            '    tvwAdminSystem.SelectedNode.ExpandAll()
            '    tvwAdminSystem.SelectedNode = sv_oNode
            'End If
        Catch ex As Exception

        End Try

    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Cập nhật các thuộc tính cơ bản của chức năng
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub UpdateFunction_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_oForm As New InsertFunction("Cập nhật chức năng")
        Dim sv_sFunctionName As String
        Dim sv_iID As Integer
        Dim sv_bEnable As Boolean
        Try
            sv_oForm.ps_iStatus = 0
            sv_sFunctionName = tvwAdminSystem.SelectedNode.Text.Trim
            If gv_bCallContextMenuFromTreeView Then
                sv_iID = CInt(tvwAdminSystem.SelectedNode.Tag.ToString.Substring(tvwAdminSystem.SelectedNode.Tag.ToString.IndexOf("#") + 1))
                sv_oForm.ps_FunctionName = sv_sFunctionName
            Else
                sv_iID = CInt(grdFunction.Item(grdFunction.CurrentRowIndex, 1))
                sv_oForm.ps_FunctionName = grdFunction.Item(grdFunction.CurrentRowIndex, 2).ToString
            End If
            sv_oForm.ps_iID = sv_iID
            sv_oForm.ShowDialog()
            sv_sFunctionName = sv_oForm.ps_FunctionName
            sv_bEnable = sv_oForm.pb_Enable
            If sv_oForm.pb_Success Then
                If InStr(tvwAdminSystem.SelectedNode.Tag.ToString.ToUpper, "LEAFFUNCTIONS#") > 0 Then
                    If sv_sFunctionName.Trim.ToUpper.Equals(tvwAdminSystem.SelectedNode.Text.Trim.ToUpper) Then
                        With tvwAdminSystem.SelectedNode
                            .ForeColor = IIf(sv_bEnable, Color.Navy, Color.DarkGray)
                        End With
                    Else
                        With tvwAdminSystem.SelectedNode
                            .Text = sv_sFunctionName
                            .ForeColor = IIf(sv_bEnable, Color.Navy, Color.DarkGray)
                        End With
                    End If
                    'Nhảy đến nút kế tiếp hoặc nút trước đó để cho người dùng thấy hiệu quả của thao tác
                    If Not tvwAdminSystem.SelectedNode.NextNode Is Nothing Then
                        tvwAdminSystem.SelectedNode = tvwAdminSystem.SelectedNode.NextNode
                    ElseIf Not tvwAdminSystem.SelectedNode.PrevNode Is Nothing Then
                        tvwAdminSystem.SelectedNode = tvwAdminSystem.SelectedNode.PrevNode
                    End If
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Khóa hoặc kích hoạt các chức năng-->MenuItem tương ứng sẽ có thuộc tính Enable=False or True
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Sub LockFunction_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_oFunction As New clsFunction
        Dim sv_oDR As DataRow
        Dim sv_iRole As Integer
        Try
            sv_iRole = CInt(tvwAdminSystem.SelectedNode.Tag.ToString.Substring(tvwAdminSystem.SelectedNode.Tag.ToString.IndexOf("#") + 1))
            If tvwAdminSystem.SelectedNode.ForeColor.Equals(gv_LockedFunctionColor) Then 'Nếu đang bị khóa thì kích hoạt
                If sv_oFunction.bLockFunction(sv_iRole, False) Then
                    With tvwAdminSystem.SelectedNode
                        .ForeColor = System.Drawing.Color.Navy
                        .SelectedImageIndex = ImageIndex.NodeFuntion
                        .ImageIndex = ImageIndex.NodeFuntion
                    End With
                    If gv_bAnnouceAfterActivatingFunction Then
                        MessageBox.Show("Đã kích hoạt " & tvwAdminSystem.SelectedNode.Text & " thành công!", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    'Nhảy đến nút kế tiếp hoặc nút trước đó để cho người dùng thấy hiệu quả của thao tác
                    If Not tvwAdminSystem.SelectedNode.NextNode Is Nothing Then
                        tvwAdminSystem.SelectedNode = tvwAdminSystem.SelectedNode.NextNode
                    ElseIf Not tvwAdminSystem.SelectedNode.PrevNode Is Nothing Then
                        tvwAdminSystem.SelectedNode = tvwAdminSystem.SelectedNode.PrevNode
                    End If
                    'Change to DataSet
                    For Each sv_oDR In gv_dsFunction.Tables(0).Rows
                        If sv_oDR.Item("Pk_iID") = sv_iRole Then
                            sv_oDR.Item("bEnable") = 1
                            Exit For
                        End If
                    Next
                    For Each dr As DataRow In gv_dsRole.Tables(0).Rows
                        If dr("FK_iFunctionID") = sv_iRole Then
                            dr("bEnable") = 1
                            Exit For
                        End If
                    Next
                    gv_dsRole.Tables(0).AcceptChanges()
                    gv_bChangeToolBar = True
                    TabControl1.SelectedIndex = 0
                End If
            Else 'Nếu đang kích hoạt thì bị khóa
                If sv_oFunction.bLockFunction(sv_iRole, True) Then
                    With tvwAdminSystem.SelectedNode
                        .ForeColor = gv_LockedFunctionColor
                        .SelectedImageIndex = ImageIndex.LockFunction
                        .ImageIndex = ImageIndex.LockFunction
                    End With
                    If gv_bAnnouceAfterLockingFunction Then
                        MessageBox.Show("Đã khóa " & tvwAdminSystem.SelectedNode.Text & " thành công!", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    'Nhảy đến nút kế tiếp hoặc nút trước đó để cho người dùng thấy hiệu quả của thao tác
                    If Not tvwAdminSystem.SelectedNode.NextNode Is Nothing Then
                        tvwAdminSystem.SelectedNode = tvwAdminSystem.SelectedNode.NextNode
                    ElseIf Not tvwAdminSystem.SelectedNode.PrevNode Is Nothing Then
                        tvwAdminSystem.SelectedNode = tvwAdminSystem.SelectedNode.PrevNode
                    End If
                    'Change to DataSet
                    For Each sv_oDR In gv_dsFunction.Tables(0).Rows
                        If sv_oDR.Item("Pk_iID") = sv_iRole Then
                            sv_oDR.Item("bEnable") = 0
                            Exit For
                        End If
                    Next
                    For Each dr As DataRow In gv_dsRole.Tables(0).Rows
                        If dr("FK_iFunctionID") = sv_iRole Then
                            dr("bEnable") = 0
                            Exit For
                        End If
                    Next
                    gv_dsRole.Tables(0).AcceptChanges()
                    gv_bChangeToolBar = True
                    TabControl1.SelectedIndex = 0
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Kích hoạt  tất cả các chức năng-->MenuItem tương ứng sẽ có thuộc tính Enable=True
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Sub ActivateAllFunctions_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_oFunction As New clsFunction
        Dim sv_oNode As TreeNode
        Dim sv_oDR As DataRow
        Try
            For Each sv_oNode In tvwAdminSystem.Nodes(0).Nodes(2).Nodes
                If sv_oNode.ForeColor.Equals(gv_LockedFunctionColor) Then  'Nếu đang bị khóa thì kích hoạt
                    'tvwAdminSystem.SelectedNode = sv_oNode
                    If sv_oFunction.bLockFunction(CInt(sv_oNode.Tag.ToString.Substring(sv_oNode.Tag.ToString.IndexOf("#") + 1)), False) Then
                        With sv_oNode
                            .ForeColor = System.Drawing.Color.Navy
                            .SelectedImageIndex = ImageIndex.NodeFuntion
                            .ImageIndex = ImageIndex.NodeFuntion
                        End With
                    End If
                End If
            Next
            For Each sv_oDR In gv_dsFunction.Tables(0).Rows
                sv_oDR.Item("bEnable") = 1
            Next
            Dim t As New Thread(AddressOf ActivateAllRoles)
            t.Start()
        Catch ex As Exception

        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Khóa tất cả các chức năng-->MenuItem tương ứng sẽ có thuộc tính Enable=False
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Sub LockAllFunctions_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_oFunction As New clsFunction
        Dim sv_oNode As TreeNode
        Dim sv_oDR As DataRow
        Try
            For Each sv_oNode In tvwAdminSystem.Nodes(0).Nodes(2).Nodes
                If Not sv_oNode.ForeColor.Equals(System.Drawing.Color.DarkGray) Then 'Nếu đang kích hoạt thì sẽ khóa
                    If sv_oFunction.bLockFunction(CInt(sv_oNode.Tag.ToString.Substring(sv_oNode.Tag.ToString.IndexOf("#") + 1))) Then
                        With sv_oNode
                            .ForeColor = gv_LockedFunctionColor
                            .SelectedImageIndex = ImageIndex.LockFunction
                            .ImageIndex = ImageIndex.LockFunction
                        End With
                    End If
                End If
            Next
            For Each sv_oDR In gv_dsFunction.Tables(0).Rows
                sv_oDR.Item("bEnable") = 0
            Next
            Dim t As New Thread(AddressOf LockAllRoles)
            t.Start()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ActivateAllRoles()
        For Each dr As DataRow In gv_dsRole.Tables(0).Rows
            dr("bEnable") = 1
        Next
        gv_bChangeToolBar = True
        TabControl1.SelectedIndex = 0
    End Sub
    Private Sub LockAllRoles()
        For Each dr As DataRow In gv_dsRole.Tables(0).Rows
            If dr("FK_iFunctionID") <> -1 Then
                dr("bEnable") = 0
            End If
        Next
        gv_bChangeToolBar = True
        TabControl1.SelectedIndex = 0
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Xóa chức năng khi không cần
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Sub DeleteFunction_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_iFunctionID As Integer
        Dim sv_clsFunction As New clsFunction
        Dim sFunctioName As String
        Dim sv_oNode As TreeNode
        Try
            sFunctioName = tvwAdminSystem.SelectedNode.Text
            sv_oNode = tvwAdminSystem.SelectedNode
            sv_iFunctionID = CInt(tvwAdminSystem.SelectedNode.Tag.ToString.Substring(tvwAdminSystem.SelectedNode.Tag.ToString.IndexOf("#") + 1))
            If gv_bCannotDeleteFunction Then
                Dim dr() As DataRow = gv_dsRole.Tables(0).Select("FK_iFunctionID=" & sv_iFunctionID)
                If dr.GetLength(0) > 0 Then
                    If MessageBox.Show("Có " & dr.GetLength(0) & " Role đã gắn chức năng này. Bạn có muốn xem danh sách các Role gắn với chức năng này không" & vbCrLf & "Bạn có thể vào chức năng tùy chọn-->Mục Chức năng để tùy chọn lại mục xóa chức năng", gv_sAnnouncement, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Dim DT As DataTable = gv_dsRole.Tables(0).Clone
                        For Each dr1 As DataRow In dr
                            Dim dr2 As DataRow = DT.NewRow
                            For Each col As DataColumn In DT.Columns
                                dr2(col.ColumnName) = dr1(col.ColumnName)
                            Next
                            DT.Rows.Add(dr2)
                        Next
                        DT.AcceptChanges()
                        Dim sv_oForm As New frm_ListOfRoleFunction
                        sv_oForm.DV = DT.DefaultView
                        sv_oForm.ShowDialog()
                    End If
                    Return
                End If
            End If
            If MessageBox.Show("Bạn có muốn xóa chức năng này không?", gv_sAnnouncement, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If sv_clsFunction.DeleteFunction(sv_iFunctionID, tvwAdminSystem.SelectedNode) Then
                    'RemoveNode ra khỏi TreeView
                    'If Not tvwAdminSystem.SelectedNode.PrevNode Is Nothing Then
                    '    tvwAdminSystem.SelectedNode = tvwAdminSystem.SelectedNode.PrevNode
                    '    tvwAdminSystem.SelectedNode.NextNode.Remove()
                    'ElseIf Not tvwAdminSystem.SelectedNode.NextNode Is Nothing Then
                    '    tvwAdminSystem.SelectedNode = tvwAdminSystem.SelectedNode.NextNode
                    '    tvwAdminSystem.SelectedNode.PrevNode.Remove()
                    'Else '1 cha,1 con
                    '    tvwAdminSystem.SelectedNode = tvwAdminSystem.SelectedNode.Parent
                    '    tvwAdminSystem.SelectedNode.Nodes(0).Remove()
                    'End If
                    'Cập nhật lại TreeView ở phía phải
                    tvwRoleForUser.Nodes.Clear()
                    tvwRoleForUser.Nodes.Add(tvwAdminSystem.Nodes(0).Nodes(3).Clone)
                    With tvwRoleForUser
                        .TopNode.ImageIndex = ImageIndex.RootRole
                        .TopNode.SelectedImageIndex = ImageIndex.RootRole
                        .ImageIndex = ImageIndex.LeafRole
                        .SelectedImageIndex = ImageIndex.LeafRole
                    End With
                    tvwRoleForUser.Nodes(0).Expand()
                    gv_bRoleHasChanged = True
                    'Xóa chức năng có trong Role này
                    MessageBox.Show("Đã xóa thành công chức năng " & sFunctioName, gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "EVENTS FOR USERS"
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Thêm mới người dùng
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub InsertUser_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_oForm As New InsertUser("Thêm mới người dùng")
        Dim sv_sUserName As String
        Dim sv_bSucess As Boolean = False
        Try
            sv_oForm.ps_iStatus = globalModule.Status.Insert
            sv_oForm.ShowDialog()
            'sv_bSucess = sv_oForm.pb_Success
            'sv_sUserName = sv_oForm.ps_UserName
            'If sv_bSucess Then
            '    Dim sv_oNode As New TreeNode(sv_sUserName)
            '    With sv_oNode
            '        .Tag = "LEAFUSER#"
            '        .SelectedImageIndex = ImageIndex.NodeUser
            '        .ImageIndex = ImageIndex.NodeUser
            '        .ForeColor = Color.Navy
            '        .NodeFont = New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
            '    End With

            '    'Thêm vào DataSet chức năng này
            '    tvwAdminSystem.SelectedNode.Nodes.Add(sv_oNode)
            '    tvwAdminSystem.SelectedNode.ExpandAll()
            '    tvwAdminSystem.SelectedNode = sv_oNode
            'End If
        Catch ex As Exception

        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Xóa mật khẩu người dùng
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub ClearPassword_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_oUser As New clsUser
        Dim sv_sUID As String
        Try
            sv_sUID = tvwAdminSystem.SelectedNode.Text.ToString.Trim
            If MessageBox.Show("Bạn có thực sự muốn xóa mật khẩu của người dùng " & sv_sUID & " không?", gv_sAnnouncement, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If sv_oUser.bChangePassword(sv_sUID, String.Empty) Then
                    Dim sv_oEncrypt As New VietBaIT.Encrypt(gv_sSymmetricAlgorithmName)
                    For Each dr As DataRow In gv_dsUser.Tables(0).Rows
                        If dr("PK_sUID").ToString.ToUpper = sv_sUID.ToUpper Then
                            dr("sPWd") = sv_oEncrypt.Mahoa(String.Empty)
                            Exit For
                        End If
                    Next
                    gv_dsUser.Tables(0).AcceptChanges()
                    MessageBox.Show("Đã xóa mật khẩu của người dùng " & sv_sUID, gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Lỗi xóa mật khẩu của người dùng " & sv_sUID, gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Xóa mật khẩu của tất cả các người dùng
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub ClearAllPassword_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_oUser As New clsUser
        Dim sv_sUID As String
        Try
            If MessageBox.Show("Bạn có thực sự muốn xóa mật khẩu của tất cả các người dùng không?", gv_sAnnouncement, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If sv_oUser.bClearAllPassword(String.Empty) Then
                    MessageBox.Show("Đã xóa mật khẩu của tất cả người dùng", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Lỗi xóa mật khẩu của người dùng", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
            sv_oUser = Nothing
        Catch ex As Exception

        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : 
    'Ngày tạo         :
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub DelegateUser_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim MainUser As String = ""
        If gv_bCallContextMenuFromTreeView Then
            MainUser = tvwAdminSystem.SelectedNode.Text.Trim
        Else
            MainUser = grdUser.Item(grdUser.CurrentRowIndex, 1).ToString
        End If
        Dim SU As New SelectUser("Main User: " & MainUser)
        SU.CurrUserList = MainUser
        SU.ShowDialog()
        If Not SU.bCancel Then
            Dim DeletageUserList As String = SU.NewCurrUL
            Dim arrUL As String() = SU.NewCurrUL.Split(",")
            Dim sv_oclsUser As New clsUser
            'Cập nhật Members of Groups
            'Đầu tiên xóa tất cả các Member
            sv_oclsUser.DeleteAllDelegateOfUser(MainUser)
            'Xóa trong Dataset
            Dim intCount As Integer = gv_dsDelegate.Tables(0).Select("MainUser='" & MainUser & "'").GetLength(0)
            Do While intCount > 0
                For Each dr As DataRow In gv_dsDelegate.Tables(0).Rows
                    If dr("MainUser").ToString.Trim.ToUpper = MainUser.Trim.ToUpper Then
                        intCount -= 1
                        gv_dsDelegate.Tables(0).Rows.Remove(dr)
                        gv_dsDelegate.Tables(0).AcceptChanges()
                        Exit For
                    End If
                Next
            Loop
            'Thêm lại DelegateUser
            For i As Integer = 0 To arrUL.Length - 1
                If arrUL(i).Trim <> "" Then
                    Dim ID As Integer = sv_oclsUser.InsertDelegate(MainUser, arrUL(i))
                    If ID <> -1 Then
                        'Thêm vào Dataset
                        Dim dr As DataRow = gv_dsDelegate.Tables(0).NewRow
                        dr("ID") = ID
                        dr("MainUser") = MainUser
                        dr("DelegateUser") = arrUL(i)
                        gv_dsDelegate.Tables(0).Rows.Add(dr)
                        gv_dsDelegate.Tables(0).AcceptChanges()
                    End If
                End If
            Next
        End If


    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : 
    'Ngày tạo         :
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub UpdateUser_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_oForm As New InsertUser("Cập nhật người dùng")
        Dim sv_bSucess As Boolean = False
        Try
            sv_oForm.ps_iStatus = globalModule.Status.Update
            If gv_bCallContextMenuFromTreeView Then
                sv_oForm.ps_UserName = tvwAdminSystem.SelectedNode.Text.Trim
            Else
                sv_oForm.ps_UserName = grdUser.Item(grdUser.CurrentRowIndex, 1).ToString
            End If
            sv_oForm.ShowDialog()
            sv_bSucess = sv_oForm.pb_Success
            If sv_bSucess Then
            Else
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub DeleteUser_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_oUser As New clsUser
        Dim sv_sUID As String
        Dim sv_oDR As DataRow
        Try
            sv_sUID = tvwAdminSystem.SelectedNode.Text.ToString.Trim
            If MessageBox.Show("Bạn có thực sự muốn xóa người dùng " & sv_sUID & " không?", gv_sAnnouncement, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If sv_oUser.bDeleteUser(sv_sUID) Then
                    MessageBox.Show("Người dùng " & sv_sUID & " đã được xóa khỏi CSDL", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Xóa Node ra khỏi cây
                    tvwAdminSystem.Nodes.Remove(tvwAdminSystem.SelectedNode)
                    'Xóa người dùng ra khỏi DataSet để Grid cập nhật lại được danh sách người dùng

                    For Each sv_oDR In gv_dsUser.Tables(0).Rows
                        If sv_oDR.Item("PK_sUID") = sv_sUID Then
                            gv_dsUser.Tables(0).Rows.Remove(sv_oDR)
                            Exit For
                        End If
                    Next
                    Dim arrdr As DataRow() = gv_dsRolesForUsers.Tables(0).Select("sUID='" & sv_sUID & "'")
                    For Each sv_oDR In arrdr
                        gv_dsRolesForUsers.Tables(0).Rows.Remove(sv_oDR)
                    Next
                    arrdr = gv_dsGroupUser.Tables(0).Select("UserID='" & sv_sUID & "'")
                    For Each sv_oDR In arrdr
                        gv_dsGroupUser.Tables(0).Rows.Remove(sv_oDR)
                    Next
                    gv_dsRolesForUsers.Tables(0).AcceptChanges()
                    gv_dsGroupUser.Tables(0).AcceptChanges()
                    gv_dsUser.Tables(0).AcceptChanges()
                Else
                    MessageBox.Show("Lỗi xóa User " & sv_sUID, gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region
#Region "EVENTS FOR Group USERS"
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Thêm mới người dùng
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub InsertGroupUser_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_oForm As New InsertGroup("New Group")
        Dim sv_bSucess As Boolean = False
        Try
            sv_oForm.ps_iStatus = globalModule.Status.Insert
            sv_oForm.ShowDialog()
            'sv_bSucess = sv_oForm.pb_Success
            'sv_sUserName = sv_oForm.ps_UserName
            'If sv_bSucess Then
            '    Dim sv_oNode As New TreeNode(sv_sUserName)
            '    With sv_oNode
            '        .Tag = "LEAFUSER#"
            '        .SelectedImageIndex = ImageIndex.NodeUser
            '        .ImageIndex = ImageIndex.NodeUser
            '        .ForeColor = Color.Navy
            '        .NodeFont = New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
            '    End With

            '    'Thêm vào DataSet chức năng này
            '    tvwAdminSystem.SelectedNode.Nodes.Add(sv_oNode)
            '    tvwAdminSystem.SelectedNode.ExpandAll()
            '    tvwAdminSystem.SelectedNode = sv_oNode
            'End If
        Catch ex As Exception

        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : 
    'Ngày tạo         :
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub UpdateGroupUser_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_oForm As New InsertGroup("Update Group")
        Dim sv_bSucess As Boolean = False
        Try
            sv_oForm.ps_iStatus = globalModule.Status.Update
            If gv_bCallContextMenuFromTreeView Then
                sv_oForm.ps_GroupID = CInt(tvwAdminSystem.SelectedNode.Tag.ToString.Substring(tvwAdminSystem.SelectedNode.Tag.ToString.IndexOf("#") + 1))

            Else
                sv_oForm.ps_GroupID = CInt(grdGroup.Item("GroupID", grdGroup.CurrentRow.Index).Value)
            End If
            sv_oForm.ShowDialog()
            sv_bSucess = sv_oForm.pb_Success
            If sv_bSucess Then
            Else
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub DeleteGroupUser_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_oUser As New clsUser
        Dim sv_GroupID As Integer
        Dim sv_oDR As DataRow
        Try
            sv_GroupID = getGroupID(tvwAdminSystem.SelectedNode)
            If sv_GroupID = -1 Then Return
            If MessageBox.Show("Bạn có thực sự muốn xóa nhóm người dùng " & tvwAdminSystem.SelectedNode.Text & " không?", gv_sAnnouncement, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                If sv_oUser.bDeleteGroup(sv_GroupID) Then
                    MessageBox.Show("Nhóm người dùng " & tvwAdminSystem.SelectedNode.Text & " đã được xóa khỏi CSDL", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Xóa Node ra khỏi cây
                    tvwAdminSystem.Nodes.Remove(tvwAdminSystem.SelectedNode)
                    'Xóa nhóm người dùng ra khỏi DataSet để Grid cập nhật lại được danh sách người dùng
                    For Each sv_oDR In gv_dsGroupUser.Tables(0).Rows
                        If CInt(sv_oDR.Item("GroupID")) = sv_GroupID Then
                            gv_dsGroupUser.Tables(0).Rows.Remove(sv_oDR)
                            Exit For
                        End If
                    Next
                    Dim arrdr As DataRow() = gv_dsGroupMember.Tables(0).Select("GroupID='" & sv_GroupID & "'")
                    For Each sv_oDR In arrdr
                        gv_dsGroupMember.Tables(0).Rows.Remove(sv_oDR)
                    Next
                    arrdr = gv_dsGroupRoles.Tables(0).Select("GroupID='" & sv_GroupID & "'")
                    For Each sv_oDR In arrdr
                        gv_dsGroupRoles.Tables(0).Rows.Remove(sv_oDR)
                    Next
                    gv_dsGroupMember.Tables(0).AcceptChanges()
                    gv_dsGroupRoles.Tables(0).AcceptChanges()
                    gv_dsGroupUser.Tables(0).AcceptChanges()
                Else
                    MessageBox.Show("Lỗi xóa nhóm người dùng " & tvwAdminSystem.SelectedNode.Text, gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region
#Region "EVENTS FOR ROLE"
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub InsertSubSystemClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_oForm As New InsertRole("Thêm mới Phân hệ")
        Dim sv_bSucess As Boolean = False
        Dim sv_sSubSystemName As String
        Dim sv_iRoleID As Integer
        sv_oForm.pi_Status = globalModule.Status.InsertSubSystemNode
        sv_oForm.pi_Order = tvwAdminSystem.SelectedNode.GetNodeCount(False) + 1
        sv_oForm.ShowDialog()
        sv_sSubSystemName = sv_oForm.ps_RoleName
        sv_iRoleID = sv_oForm.pi_RoleID
        If sv_oForm.pb_Success Then
            'Dim sv_oNode As New TreeNode(sv_sSubSystemName)
            'With sv_oNode
            '    .Tag = "LEAFROLES|-2#" & sv_iRoleID.ToString
            '    .SelectedImageIndex = ImageIndex.NodeRole
            '    .ImageIndex = ImageIndex.NodeRole
            '    .NodeFont = New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
            'End With
            'tvwAdminSystem.SelectedNode.Nodes.Add(sv_oNode)
            'tvwAdminSystem.SelectedNode.Expand()
            'tvwAdminSystem.SelectedNode = sv_oNode
            gv_bRoleHasChanged = True
        End If
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Chèn mới một Role với các thuộc tính cơ bản
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub InsertRole_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_oForm As New InsertRole("Thêm mới Role")
        Dim sv_iParentRoleID As Integer
        Dim sv_sRoleName As String
        Dim sv_iRoleID As Integer
        Dim sv_bSucess As Boolean = False
        Try
            'Lấy về mã ParentRoleID
            sv_iParentRoleID = CInt(tvwAdminSystem.SelectedNode.Tag.ToString.Substring(tvwAdminSystem.SelectedNode.Tag.ToString.IndexOf("#") + 1))
            'Gan cac gia tri khoi tao cho Form them moi
            sv_oForm.pi_Order = tvwAdminSystem.SelectedNode.GetNodeCount(False) + 1
            sv_oForm.pi_ParentRoleID = sv_iParentRoleID
            sv_oForm.pi_Status = globalModule.Status.Insert
            sv_oForm.mv_bMenuLevel1 = False
            sv_oForm.ShowDialog()
            sv_sRoleName = sv_oForm.ps_RoleName
            sv_iRoleID = sv_oForm.pi_RoleID
            If sv_oForm.pb_Success Then
                Dim sv_bHasShortCutKey As Boolean = False
                For i As Integer = 0 To CboValue.Items.Count - 1
                    If CboValue.Items(i).ToString.Trim = CStr(IIF_VN(sv_oForm.mv_intShortCutKey)) Then
                        CboValue.SelectedIndex = i
                        sv_bHasShortCutKey = True
                        Exit For
                    End If
                Next
                If Not sv_bHasShortCutKey Then
                    CboValue.SelectedIndex = -1
                End If
                If Not IIF_VN(sv_oForm.mv_sIconPath).Trim.ToUpper.Equals("UNKNOWN") And Not IsDBNull(sv_oForm.mv_sIconPath) And Not System.IO.File.Exists(s_IsNothingOrDBNull(sv_oForm.mv_sIconPath)) Then
                    picIcon.Image = Image.FromFile(sv_oForm.mv_sIconPath)
                Else
                    picIcon.Image = Nothing
                End If

                'With tvwAdminSystem.SelectedNode
                '    .Text = sv_sRoleName
                'End With
                gv_bRoleHasChanged = True
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub InsertMenuLevel1_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_oForm As New InsertRole("Thêm mới Role")
        Dim sv_iParentRoleID As Integer
        Dim sv_sRoleName As String
        Dim sv_iRoleID As Integer
        Dim sv_bSucess As Boolean = False
        Try
            'Lấy về mã ParentRoleID
            sv_iParentRoleID = CInt(tvwAdminSystem.SelectedNode.Tag.ToString.Substring(tvwAdminSystem.SelectedNode.Tag.ToString.IndexOf("#") + 1))
            'Gan cac gia tri khoi tao cho Form them moi
            sv_oForm.pi_Order = tvwAdminSystem.SelectedNode.GetNodeCount(False) + 1
            sv_oForm.pi_ParentRoleID = sv_iParentRoleID
            sv_oForm.pi_Status = globalModule.Status.Insert
            sv_oForm.mv_bMenuLevel1 = True
            sv_oForm.ShowDialog()
            sv_sRoleName = sv_oForm.ps_RoleName
            sv_iRoleID = sv_oForm.pi_RoleID
            If sv_oForm.pb_Success Then
                Dim sv_bHasShortCutKey As Boolean = False
                For i As Integer = 0 To CboValue.Items.Count - 1
                    If CboValue.Items(i).ToString.Trim = CStr(IIF_VN(sv_oForm.mv_intShortCutKey)) Then
                        CboValue.SelectedIndex = i
                        sv_bHasShortCutKey = True
                        Exit For
                    End If
                Next
                If Not sv_bHasShortCutKey Then
                    CboValue.SelectedIndex = -1
                End If
                If Not IIF_VN(sv_oForm.mv_sIconPath).Trim.ToUpper.Equals("UNKNOWN") And Not IsDBNull(sv_oForm.mv_sIconPath) And Not System.IO.File.Exists(s_IsNothingOrDBNull(sv_oForm.mv_sIconPath)) Then
                    picIcon.Image = Image.FromFile(sv_oForm.mv_sIconPath)
                Else
                    picIcon.Image = Nothing
                End If

                'With tvwAdminSystem.SelectedNode
                '    .Text = sv_sRoleName
                'End With
                gv_bRoleHasChanged = True
            End If
        Catch ex As Exception

        End Try

    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Cập nhật đường dẫn file ảnh nền cho phân hệ
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub UpdateImgPath(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_iParentRoleID As Integer
        Dim sv_clsRole As New clsRole
        Dim fileDiag As New OpenFileDialog
        Try
            'Lấy về mã ParentRoleID
            fileDiag.Filter = "All files|*.ico;*.bmp;*.gif;*.jpg|Icon|*.ico|Gif|*.gif|Jpg|*.Jpg|Bmp|*.Bmp"
            If fileDiag.ShowDialog = DialogResult.OK Then
                sv_iParentRoleID = CInt(tvwAdminSystem.SelectedNode.Tag.ToString.Substring(tvwAdminSystem.SelectedNode.Tag.ToString.IndexOf("#") + 1))
                If sv_clsRole.bUpdateImgPath(sv_iParentRoleID, "sImgPath", fileDiag.FileName) Then
                    gv_dsRole.Tables(0).Select("iRole=" & sv_iParentRoleID)(0)("sImgPath") = fileDiag.FileName
                    gv_dsRole.Tables(0).AcceptChanges()
                    picSubSystem.Image = Image.FromFile(fileDiag.FileName)
                    'MessageBox.Show("Cập nhật ảnh nền cho phân hệ thành công!", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Cập nhật Icon cho phân hệ
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : CuongDV
    'Ngày tạo         :02/04/2006
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub UpdateIcon(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_iParentRoleID As Integer
        Dim sv_clsRole As New clsRole
        Dim fileDiag As New OpenFileDialog
        Try
            fileDiag.Filter = "All files|*.ico;*.bmp;*.gif;*.jpg|Icon|*.ico|Gif|*.gif|Jpg|*.Jpg|Bmp|*.Bmp"
            'Lấy về mã ParentRoleID
            If fileDiag.ShowDialog = DialogResult.OK Then
                sv_iParentRoleID = CInt(tvwAdminSystem.SelectedNode.Tag.ToString.Substring(tvwAdminSystem.SelectedNode.Tag.ToString.IndexOf("#") + 1))
                If sv_clsRole.bUpdateImgPath(sv_iParentRoleID, "sIConPath", fileDiag.FileName) Then
                    MessageBox.Show("Cập nhật Icon cho phân hệ thành công!", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Cập nhật các thuộc tính cơ bản của Role
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub UpdateRole_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_oForm As New InsertRole("Cập nhật Role")

        Dim sv_iParentRoleID As Integer
        Dim sv_sRoleName As String
        Dim sv_iRoleID As Integer
        Dim sv_bSucess As Boolean = False
        'Lấy về mã ParentRoleID
        sv_iRoleID = CInt(tvwAdminSystem.SelectedNode.Tag.ToString.Substring(tvwAdminSystem.SelectedNode.Tag.ToString.IndexOf("#") + 1))
        'Gan cac gia tri khoi tao cho Form them moi
        sv_oForm.pi_RoleID = sv_iRoleID
        sv_oForm.pi_Status = globalModule.Status.Update
        sv_oForm.mv_bMenuLevel1 = False
        sv_oForm.ShowDialog()
        sv_sRoleName = sv_oForm.ps_RoleName
        If sv_oForm.pb_Success Then
            Dim sv_bHasShortCutKey As Boolean = False
            For i As Integer = 0 To CboValue.Items.Count - 1
                If CboValue.Items(i).ToString.Trim = CStr(IIF_VN(sv_oForm.mv_intShortCutKey)) Then
                    CboValue.SelectedIndex = i
                    sv_bHasShortCutKey = True
                    Exit For
                End If
            Next
            If Not sv_bHasShortCutKey Then
                CboValue.SelectedIndex = -1
            End If
            If Not IIF_VN(sv_oForm.mv_sIconPath).Trim.ToUpper.Equals("UNKNOWN") And Not IsDBNull(sv_oForm.mv_sIconPath) And System.IO.File.Exists(s_IsNothingOrDBNull(sv_oForm.mv_sIconPath)) Then
                Try
                    picIcon.Image = Image.FromFile(sv_oForm.mv_sIconPath)
                Catch ex As Exception

                End Try

            Else
                picIcon.Image = Nothing
            End If
            With tvwAdminSystem.SelectedNode
                .Text = sv_sRoleName
            End With
            'Kiểm tra xem có thay đổi số TT hay không
            Dim sv_iCurrOrder, sv_iOldOrder, sv_iMaxChild, sv_iMinChild As Integer
            sv_iOldOrder = sv_oForm.mv_currOrder
            sv_iCurrOrder = sv_oForm.mv_iOrder
            sv_iMinChild = 1
            sv_iMaxChild = tvwAdminSystem.SelectedNode.Parent.GetNodeCount(False)
            If sv_iOldOrder = sv_iCurrOrder Then
            Else
                If sv_iOldOrder > sv_iCurrOrder Then
                    For i As Integer = 0 To sv_iOldOrder - sv_iCurrOrder - 1
                        cmdUp_Click(sender, e)
                    Next
                Else
                    For i As Integer = 0 To Math.Min(sv_iCurrOrder, sv_iMaxChild) - sv_iOldOrder - 1
                        cmdDown_Click(sender, e)
                    Next
                End If
            End If
            UpdateAllOrder(tvwAdminSystem.SelectedNode.Parent.FirstNode)
            tvwAdminSystem_Click(sender, e)
            gv_bRoleHasChanged = True
        End If
    End Sub
    Private Sub UpdateSubSystem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_oForm As New InsertRole("Cập nhật phân hệ")

        Dim sv_iParentRoleID As Integer
        Dim sv_sRoleName As String
        Dim sv_iRoleID As Integer
        Dim sv_bSucess As Boolean = False
        'Lấy về mã ParentRoleID
        sv_iRoleID = CInt(tvwAdminSystem.SelectedNode.Tag.ToString.Substring(tvwAdminSystem.SelectedNode.Tag.ToString.IndexOf("#") + 1))
        'Gan cac gia tri khoi tao cho Form them moi
        sv_oForm.pi_RoleID = sv_iRoleID
        sv_oForm.pi_Status = globalModule.Status.Update
        sv_oForm.mv_bMenuLevel1 = True
        sv_oForm.ShowDialog()
        sv_sRoleName = sv_oForm.ps_RoleName
        If sv_oForm.pb_Success Then
            Dim sv_bHasShortCutKey As Boolean = False
            For i As Integer = 0 To CboValue.Items.Count - 1
                If CboValue.Items(i).ToString.Trim = CStr(IIF_VN(sv_oForm.mv_intShortCutKey)) Then
                    CboValue.SelectedIndex = i
                    sv_bHasShortCutKey = True
                    Exit For
                End If
            Next
            If Not sv_bHasShortCutKey Then
                CboValue.SelectedIndex = -1
            End If
            If Not IIF_VN(sv_oForm.mv_sIconPath).Trim.ToUpper.Equals("UNKNOWN") And Not IsDBNull(sv_oForm.mv_sIconPath) And System.IO.File.Exists(s_IsNothingOrDBNull(sv_oForm.mv_sIconPath)) Then
                Try
                    picIcon.Image = Image.FromFile(sv_oForm.mv_sIconPath)
                Catch ex As Exception

                End Try
            Else
                picIcon.Image = Nothing
            End If
            With tvwAdminSystem.SelectedNode
                If Not tvwAdminSystem.SelectedNode.Parent Is Nothing Then
                    If Not tvwAdminSystem.SelectedNode.Parent.Tag Is Nothing Then
                        If tvwAdminSystem.SelectedNode.Parent.Tag = "ROOTROLE#-2" Then
                            If sv_oForm.mv_bChangeIconPath Then
                                If bIsValidPath(sv_oForm.mv_sIconPath) Then
                                    gv_oMainForm.tvwAdminSystem.ImageList.Images.Add(Image.FromFile(sv_oForm.mv_sIconPath))
                                    .SelectedImageIndex = gv_oMainForm.tvwAdminSystem.ImageList.Images.Count - 1
                                    .ImageIndex = gv_oMainForm.tvwAdminSystem.ImageList.Images.Count - 1
                                End If
                            End If
                        End If
                    End If
                End If
                .Text = sv_sRoleName
            End With
            'Kiểm tra xem có thay đổi số TT hay không
            Dim sv_iCurrOrder, sv_iOldOrder, sv_iMaxChild, sv_iMinChild As Integer
            sv_iOldOrder = sv_oForm.mv_currOrder
            sv_iCurrOrder = sv_oForm.mv_iOrder
            sv_iMinChild = 1
            sv_iMaxChild = tvwAdminSystem.SelectedNode.Parent.GetNodeCount(False)
            If sv_iOldOrder = sv_iCurrOrder Then
            Else
                If sv_iOldOrder > sv_iCurrOrder Then
                    For i As Integer = 0 To sv_iOldOrder - sv_iCurrOrder - 1
                        cmdUp_Click(sender, e)
                    Next
                Else
                    For i As Integer = 0 To Math.Min(sv_iCurrOrder, sv_iMaxChild) - sv_iOldOrder - 1
                        cmdDown_Click(sender, e)
                    Next
                End If
            End If
            UpdateAllOrder(tvwAdminSystem.SelectedNode.Parent.FirstNode)
            tvwAdminSystem_Click(sender, e)
            gv_bRoleHasChanged = True
        End If
    End Sub
    Private Sub UpdateAllOrder(ByVal pv_oNode As TreeNode)
        Dim oNode As TreeNode
        Dim i As Integer = 1
        Dim sv_iRoleID As Integer
        Dim sv_oRole As New clsRole
        Try
            oNode = pv_oNode
            Do
                sv_iRoleID = CInt(oNode.Tag.ToString.Substring(oNode.Tag.ToString.IndexOf("#") + 1))
                sv_oRole.UpdateOrder1(sv_iRoleID, i)
                i += 1
                oNode = oNode.NextNode
            Loop Until oNode Is Nothing
        Catch ex As Exception

        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Xóa Role khi không thích có nó
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub DeleteRole_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_iRoleID As Integer
        Dim sv_clsRole As New clsRole
        Try
            sv_iRoleID = CInt(tvwAdminSystem.SelectedNode.Tag.ToString.Substring(tvwAdminSystem.SelectedNode.Tag.ToString.IndexOf("#") + 1))
            If MessageBox.Show("Bạn có muốn xóa Role này hay không?", gv_sAnnouncement, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                UpdateOrderOfAllNextNodes(tvwAdminSystem.SelectedNode)
                sv_clsRole.DeleteRole(sv_iRoleID)
                'RemoveNode ra khỏi TreeView
                tvwAdminSystem.SelectedNode.Remove()
                'Cập nhật lại TreeView ở phía phải
                tvwRoleForUser.Nodes.Clear()
                tvwRoleForUser.Nodes.Add(tvwAdminSystem.Nodes(0).Nodes(3).Clone)
                With tvwRoleForUser
                    .TopNode.ImageIndex = ImageIndex.RootRole
                    .TopNode.SelectedImageIndex = ImageIndex.RootRole
                    .ImageIndex = ImageIndex.LeafRole
                    .SelectedImageIndex = ImageIndex.LeafRole
                End With
                tvwRoleForUser.Nodes(0).Expand()
                gv_bRoleHasChanged = True
                MessageBox.Show("Xóa Role thành công", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception

        End Try

    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Xóa Role khi không thích có nó
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub gotoFuntion(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_iFunctionID As Integer
        Dim sv_CurrSelectedNode As TreeNode = tvwAdminSystem.SelectedNode
        Dim sv_bFound As Boolean = False
        Try
            sv_iFunctionID = CInt(txtFunctionID.Text)
            For Each Node As TreeNode In tvwAdminSystem.Nodes(0).Nodes(2).Nodes
                Dim FuntionID As Integer
                tvwAdminSystem.SelectedNode = Node
                FuntionID = Node.Tag.ToString.Substring(Node.Tag.ToString.LastIndexOf("#") + 1)
                If sv_iFunctionID = FuntionID Then
                    sv_bFound = True
                    Exit For
                End If
            Next
            If Not sv_bFound Then tvwAdminSystem.SelectedNode = sv_CurrSelectedNode
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "Các sự kiện trên cây QTHT"
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Hiển thị ContextMenu cho Treeview tùy vào từng Node khi được nhấn chuột phải
    'Đầu vào          :Node được nhấn, giá trị Tag của Node
    'Đầu ra            :Hiển thị ContextMenu
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub ms_ChooseContextMenu(ByVal pv_oNode As TreeNode, ByVal pv_sTag As String)
        'Kiểm tra nếu MenuItem là dấu phân cách thì chỉ cho phép cập nhật và xóa NodeRole
        If pv_oNode.Text.Trim.Equals("-") Then
            tvwAdminSystem.ContextMenu = mv_oContextMenuForLeafRole
            Return
        End If
        'Kiểm tra xem cần tạo loại ContextMenu tương ứng nào
        Select Case pv_sTag.ToUpper
            Case "ROOTGROUP" 'Neu la nut goc Nguoi Dung
                tvwAdminSystem.ContextMenu = mv_oContextMenuForRootGroup
                Return

            Case "ROOTUSER" 'Neu la nut goc Nguoi Dung
                tvwAdminSystem.ContextMenu = mv_oContextMenuForRootUser
                Return
            Case "LEAFUSER#"
                gv_bCallContextMenuFromTreeView = True
                tvwAdminSystem.ContextMenu = mv_oContextMenuForLeafUser
                If gv_bCannotDeletePWDOfAllUIDs Then
                    mv_oContextMenuForLeafUser.MenuItems(4).Enabled = False
                Else
                    mv_oContextMenuForLeafUser.MenuItems(4).Enabled = True
                End If
                If gv_bCannotDeletePWDOfUID Then
                    mv_oContextMenuForLeafUser.MenuItems(3).Enabled = False
                Else
                    mv_oContextMenuForLeafUser.MenuItems(3).Enabled = True
                End If
                If gv_bCannotDeleteUID Then
                    mv_oContextMenuForLeafUser.MenuItems(1).Enabled = False
                Else
                    mv_oContextMenuForLeafUser.MenuItems(1).Enabled = True
                End If
                Return
            Case "ROOTFUNCTION"
                tvwAdminSystem.ContextMenu = mv_oContextMenuForRootFunction
                Return
            Case "ROOTROLE#-2"
                tvwAdminSystem.ContextMenu = mv_oContextMenuForRootRole
                Return
            Case "ROOTPARAM"
                tvwAdminSystem.ContextMenu = mv_oContextMenuForParam
                mv_oContextMenuForParam.MenuItems(0).Enabled = True
                mv_oContextMenuForParam.MenuItems(1).Enabled = False
                mv_oContextMenuForParam.MenuItems(2).Enabled = False
                mv_oContextMenuForParam.MenuItems(3).Enabled = False
                Return
            Case "NODEPARAM"
                tvwAdminSystem.ContextMenu = mv_oContextMenuForParam
                mv_oContextMenuForParam.MenuItems(0).Enabled = False
                mv_oContextMenuForParam.MenuItems(1).Enabled = True
                mv_oContextMenuForParam.MenuItems(2).Enabled = True
                mv_oContextMenuForParam.MenuItems(3).Enabled = True
                If pv_oNode.ForeColor.Equals(gv_LockedParamColor) Then
                    mv_oContextMenuForParam.MenuItems(3).Text = "Kích hoạt tham số"
                Else
                    mv_oContextMenuForParam.MenuItems(3).Text = "Khóa tham số"
                End If
                Return
            Case Else 'Nếu không phải là các DefaultNode
                'Nếu là các Node Phân hệ(Node con của Node ROLES)
                If pv_sTag = "QTHT" Then Return
                If pv_oNode.Parent.Text.ToUpper.Equals("ROLES") Then
                    tvwAdminSystem.ContextMenu = mv_oContextMenuForMenuLevel1
                    If pv_oNode.GetNodeCount(True) > 0 Then
                        mv_oContextMenuForMenuLevel1.MenuItems(0).Enabled = False
                    Else
                        mv_oContextMenuForMenuLevel1.MenuItems(0).Enabled = True
                    End If
                    Return
                End If
                If InStr(pv_sTag.ToUpper, "LEAFGROUP#") > 0 Then
                    gv_bCallContextMenuFromTreeView = True
                    tvwAdminSystem.ContextMenu = mv_oContextMenuForLeafGroup
                    Return
                End If
                '----------------------------Nếu là các node Role------------------------------------
                If InStr(pv_sTag.ToUpper, "LEAFROLES") > 0 Then
                    grdFunction.ContextMenu = Nothing
                    If pv_oNode.GetNodeCount(True) > 0 Then
                        tvwAdminSystem.ContextMenu = mv_oContextMenuForLeafRoleHasChild
                        Return
                    End If
                    Dim sv_sFunctionAndRoleID As String
                    sv_sFunctionAndRoleID = txtFunctionID.Text 'pv_sTag.Substring(pv_sTag.IndexOf("|") + 1)
                    If sv_sFunctionAndRoleID.Trim = "Chưa gán" OrElse sv_sFunctionAndRoleID.Trim = "" Then
                        sv_sFunctionAndRoleID = "-1"
                    End If

                    'Kiểm tra xem Node đã được gắn chức năng hay chưa
                    If sv_sFunctionAndRoleID = "-1" OrElse sv_sFunctionAndRoleID.Trim = "" Then 'Chưa được gắn chức năng
                        tvwAdminSystem.ContextMenu = mv_oContextMenuForLeafRoleWithoutFunction
                        Return
                    Else 'Đã được gắn chức năng
                        tvwAdminSystem.ContextMenu = mv_oContextMenuForLeafRole
                        Return
                    End If
                End If
                '----------------------------Nếu là các node chức năng------------------------------------
                If InStr(pv_sTag.ToUpper, "LEAFFUNCTIONS#") > 0 Then
                    gv_bCallContextMenuFromTreeView = True
                    If pv_oNode.ForeColor.Equals(gv_LockedFunctionColor) Then
                        mv_oContextMenuForLeafFunction.MenuItems(1).Text = "Kích hoạt chức năng"
                    Else
                        mv_oContextMenuForLeafFunction.MenuItems(1).Text = "Khóa chức năng"
                    End If
                    grdFunction.ContextMenu = mv_oContextMenuForLeafFunction
                    tvwAdminSystem.ContextMenu = mv_oContextMenuForLeafFunction
                    Return
                End If
        End Select
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Tạo MainMenu cho chương trình QTHT tùy thuộc vào việc người dùng chọn Node nào trên cây
    'Đầu vào          :Node được nhấn, giá trị Tag của Node
    'Đầu ra            :Tùy biến MainMenu
    'Người tạo       :CuongDV
    'Ngày tạo         :07/03/2006
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub BuildMainMenu(ByVal pv_oNode As TreeNode, ByVal pv_sTag As String)
        mnuMain.Items.Clear()
        If pv_sTag = "QTHT" Then

            mnuMain.Items.AddRange(New ToolStripItem() {mnuSystem, mnuUtility, mnuUpdateVersion, mnuHelp})
            Return
        End If
        If pv_oNode.Text.Trim.Equals("-") Then
            mnuMain.Items.Clear()
            mnuRole.DropDownItems.Clear()
            mnuRole.DropDownItems.AddRange(New ToolStripItem() {New ToolStripMenuItem("Cập nhật Role", Nothing, New EventHandler(AddressOf UpdateRole_Click)), _
                                                                                                                       New ToolStripMenuItem("Xóa Role", Nothing, New EventHandler(AddressOf DeleteRole_Click)), _
                                                                                                                       New ToolStripSeparator(), _
                                                                                                                       New ToolStripMenuItem("Chuyển tới chức năng", Nothing, New EventHandler(AddressOf gotoFuntion))})
            mnuMain.Items.AddRange(New ToolStripItem() {mnuSystem, mnuGroupUser, mnuUser, mnuFunction, mnuRole, mnuParameter, mnuUtility, mnuUpdateVersion, mnuHelp})
            mnuMain.Items(1).Enabled = False
            mnuMain.Items(2).Enabled = False
            mnuMain.Items(3).Enabled = False
            mnuMain.Items(4).Enabled = False
            mnuMain.Items(5).Enabled = False
            Me.Controls.Add(mnuMain)
            Return
        End If

        Select Case pv_sTag.ToUpper
            Case "ROOTGROUP" 'Neu la nut goc nhóm Nguoi Dung
                mnuMain.Items.Clear()
                mnuGroupUser.DropDownItems.Clear()
                mnuGroupUser.DropDownItems.AddRange(New ToolStripItem() {New ToolStripMenuItem("New Group", Nothing, New EventHandler(AddressOf InsertGroupUser_Click))})
                mnuMain.Items.AddRange(New ToolStripItem() {mnuSystem, mnuGroupUser, mnuUser, mnuFunction, mnuRole, mnuParameter, mnuUtility, mnuUpdateVersion, mnuHelp})
                mnuMain.Items(1).Enabled = True
                mnuMain.Items(2).Enabled = False
                mnuMain.Items(3).Enabled = False
                mnuMain.Items(4).Enabled = False
                mnuMain.Items(5).Enabled = False
                Me.Controls.Add(mnuMain)
                Return

            Case "ROOTUSER" 'Neu la nut goc Nguoi Dung
                mnuMain.Items.Clear()
                mnuUser.DropDownItems.Clear()
                mnuUser.DropDownItems.AddRange(New ToolStripItem() {New ToolStripMenuItem("Thêm mới người dùng", Nothing, New EventHandler(AddressOf InsertUser_Click))})
                mnuMain.Items.AddRange(New ToolStripItem() {mnuSystem, mnuGroupUser, mnuUser, mnuFunction, mnuRole, mnuParameter, mnuUtility, mnuUpdateVersion, mnuHelp})
                mnuMain.Items(1).Enabled = False
                mnuMain.Items(2).Enabled = True
                mnuMain.Items(3).Enabled = False
                mnuMain.Items(4).Enabled = False
                mnuMain.Items(5).Enabled = False
                Me.Controls.Add(mnuMain)
                Return
            Case "LEAFUSER#"
                mnuMain.Items.Clear()
                mnuUser.DropDownItems.Clear()
                mnuUser.DropDownItems.AddRange(New ToolStripItem() {New ToolStripMenuItem("Cập nhật người dùng", Nothing, New EventHandler(AddressOf UpdateUser_Click)), _
                                                                                                                          New ToolStripMenuItem("Xóa người dùng", Nothing, New EventHandler(AddressOf DeleteUser_Click)), _
                                                                                                                          New ToolStripSeparator(), _
                                                                                                                          New ToolStripMenuItem("Xóa mật khẩu", Nothing, New EventHandler(AddressOf ClearPassword_Click)), _
                                                                                                                          New ToolStripMenuItem("Clear All Users' Password", Nothing, New EventHandler(AddressOf ClearAllPassword_Click))})
                If gv_bCannotDeletePWDOfAllUIDs Then
                    mnuUser.DropDownItems(4).Enabled = False
                End If
                If gv_bCannotDeletePWDOfUID Then
                    mnuUser.DropDownItems(3).Enabled = False
                End If
                If gv_bCannotDeleteUID Then
                    mnuUser.DropDownItems(1).Enabled = False
                End If
                mnuMain.Items.AddRange(New ToolStripItem() {mnuSystem, mnuGroupUser, mnuUser, mnuFunction, mnuRole, mnuParameter, mnuUtility, mnuUpdateVersion, mnuHelp})
                mnuMain.Items(1).Enabled = False
                mnuMain.Items(2).Enabled = True
                mnuMain.Items(3).Enabled = False
                mnuMain.Items(4).Enabled = False
                mnuMain.Items(5).Enabled = False
                Me.Controls.Add(mnuMain)
                Return
            Case "ROOTFUNCTION"
                mnuMain.Items.Clear()
                mnuFunction.DropDownItems.Clear()
                mnuFunction.DropDownItems.AddRange(New ToolStripItem() {New ToolStripMenuItem("Thêm chức năng", Nothing, New EventHandler(AddressOf InsertFunction_Click)), _
                                                                                                                       New ToolStripMenuItem("Kích hoạt tất cả các chức năng", Nothing, New EventHandler(AddressOf ActivateAllFunctions_Click)), _
                                                                                                                       New ToolStripSeparator(), _
                                                                                                                       New ToolStripMenuItem("Khóa tất cả các chức năng", Nothing, New EventHandler(AddressOf LockAllFunctions_Click))})
                mnuMain.Items.AddRange(New ToolStripItem() {mnuSystem, mnuGroupUser, mnuUser, mnuFunction, mnuRole, mnuParameter, mnuUtility, mnuUpdateVersion, mnuHelp})
                mnuMain.Items(1).Enabled = False
                mnuMain.Items(2).Enabled = False
                mnuMain.Items(3).Enabled = True
                mnuMain.Items(4).Enabled = False
                mnuMain.Items(5).Enabled = False

                Me.Controls.Add(mnuMain)
                Return
            Case "ROOTROLE#-2"
                mnuMain.Items.Clear()
                mnuRole.DropDownItems.Clear()
                mnuRole.DropDownItems.AddRange(New ToolStripItem() {New ToolStripMenuItem("Thêm phân hệ", Nothing, New EventHandler(AddressOf InsertSubSystemClick))})
                mnuMain.Items.AddRange(New ToolStripItem() {mnuSystem, mnuGroupUser, mnuUser, mnuFunction, mnuRole, mnuParameter, mnuUtility, mnuUpdateVersion, mnuHelp})
                mnuMain.Items(1).Enabled = False
                mnuMain.Items(2).Enabled = False
                mnuMain.Items(3).Enabled = False
                mnuMain.Items(4).Enabled = True
                mnuMain.Items(5).Enabled = False

                Me.Controls.Add(mnuMain)
                Return
            Case "ROOTPARAM"
                mnuMain.Items.Clear()
                mnuParameter.DropDownItems.Clear()
                mnuParameter.DropDownItems.AddRange(New ToolStripItem() {New ToolStripMenuItem("Thêm mới tham số", Nothing, New EventHandler(AddressOf InsertParam)), _
                                                                                                                           New ToolStripMenuItem("Cập nhật tham số", Nothing, New EventHandler(AddressOf UpdateParam)), _
                                                                                                                           New ToolStripMenuItem("Xóa tham số", Nothing, New EventHandler(AddressOf DeleteParam)), _
                                                                                                                           New ToolStripMenuItem("Khóa tham số", Nothing, New EventHandler(AddressOf ActivateStatus))})
                mnuMain.Items.AddRange(New ToolStripItem() {mnuSystem, mnuGroupUser, mnuUser, mnuFunction, mnuRole, mnuParameter, mnuUtility, mnuUpdateVersion, mnuHelp})
                mnuMain.Items(1).Enabled = False
                mnuMain.Items(2).Enabled = False
                mnuMain.Items(3).Enabled = False
                mnuMain.Items(4).Enabled = False
                mnuMain.Items(5).Enabled = True
                mnuParameter.DropDownItems(0).Enabled = True
                mnuParameter.DropDownItems(1).Enabled = False
                mnuParameter.DropDownItems(2).Enabled = False
                mnuParameter.DropDownItems(3).Enabled = False
                Me.Controls.Add(mnuMain)
                Return
            Case "NODEPARAM"
                mnuMain.Items.Clear()
                mnuParameter.DropDownItems.Clear()
                If pv_oNode.ForeColor.Equals(gv_LockedFunctionColor) Then
                    mnuParameter.DropDownItems.AddRange(New ToolStripItem() {New ToolStripMenuItem("Thêm mới tham số", Nothing, New EventHandler(AddressOf InsertParam)), _
                                                                                                                           New ToolStripMenuItem("Cập nhật tham số", Nothing, New EventHandler(AddressOf UpdateParam)), _
                                                                                                                           New ToolStripMenuItem("Xóa tham số", Nothing, New EventHandler(AddressOf DeleteParam)), _
                                                                                                                           New ToolStripMenuItem("Kích hoạt tham số", Nothing, New EventHandler(AddressOf ActivateStatus))})
                Else
                    mnuParameter.DropDownItems.AddRange(New ToolStripItem() {New ToolStripMenuItem("Thêm mới tham số", Nothing, New EventHandler(AddressOf InsertParam)), _
                                                                                                                           New ToolStripMenuItem("Cập nhật tham số", Nothing, New EventHandler(AddressOf UpdateParam)), _
                                                                                                                           New ToolStripMenuItem("Xóa tham số", Nothing, New EventHandler(AddressOf DeleteParam)), _
                                                                                                                           New ToolStripMenuItem("Khóa tham số", Nothing, New EventHandler(AddressOf ActivateStatus))})
                End If


                mnuMain.Items.AddRange(New ToolStripItem() {mnuSystem, mnuGroupUser, mnuUser, mnuFunction, mnuRole, mnuParameter, mnuUtility, mnuUpdateVersion, mnuHelp})
                mnuMain.Items(1).Enabled = False
                mnuMain.Items(2).Enabled = False
                mnuMain.Items(3).Enabled = False
                mnuMain.Items(4).Enabled = False
                mnuMain.Items(4).Enabled = True
                mnuParameter.DropDownItems(0).Enabled = False
                mnuParameter.DropDownItems(1).Enabled = True
                mnuParameter.DropDownItems(2).Enabled = True
                mnuParameter.DropDownItems(3).Enabled = True
                Me.Controls.Add(mnuMain)
                Return
            Case Else 'Nếu không phải là các DefaultNode
                'Nếu là các Node Phân hệ(Node con của Node ROLES)
                If pv_oNode.Parent.Text.ToUpper.Equals("MENU") Then
                    mnuMain.Items.Clear()
                    mnuRole.DropDownItems.Clear()
                    mnuRole.DropDownItems.AddRange(New ToolStripItem() {New ToolStripMenuItem("Thêm Menu cấp 1", Nothing, New EventHandler(AddressOf InsertRole_Click)), _
                                                                                                                               New ToolStripMenuItem("Chọn ảnh nền cho phân hệ", Nothing, New EventHandler(AddressOf UpdateImgPath))})

                    mnuMain.Items.AddRange(New ToolStripItem() {mnuSystem, mnuGroupUser, mnuUser, mnuFunction, mnuRole, mnuParameter, mnuUtility, mnuUpdateVersion, mnuHelp})
                    mnuMain.Items(1).Enabled = False
                    mnuMain.Items(2).Enabled = False
                    mnuMain.Items(3).Enabled = False
                    mnuMain.Items(4).Enabled = True
                    mnuMain.Items(5).Enabled = False
                    Me.Controls.Add(mnuMain)
                    Return
                End If
                If InStr(pv_sTag.ToUpper, "LEAFGROUP#") > 0 Then
                    mnuMain.Items.Clear()
                    mnuGroupUser.DropDownItems.Clear()
                    mnuGroupUser.DropDownItems.AddRange(New ToolStripItem() {New ToolStripMenuItem("Update Groups", Nothing, New EventHandler(AddressOf UpdateGroupUser_Click)), _
                                                                                                                              New ToolStripMenuItem("Delete Group", Nothing, New EventHandler(AddressOf DeleteGroupUser_Click))})
                    mnuMain.Items.AddRange(New ToolStripItem() {mnuSystem, mnuGroupUser, mnuUser, mnuFunction, mnuRole, mnuParameter, mnuUtility, mnuUpdateVersion, mnuHelp})
                    mnuMain.Items(1).Enabled = True
                    mnuMain.Items(2).Enabled = False
                    mnuMain.Items(3).Enabled = False
                    mnuMain.Items(4).Enabled = False
                    mnuMain.Items(5).Enabled = False
                    Me.Controls.Add(mnuMain)
                    Return
                End If
                '----------------------------Nếu là các node Role------------------------------------
                If InStr(pv_sTag.ToUpper, "LEAFROLES") > 0 Then
                    If pv_oNode.GetNodeCount(True) > 0 Then
                        mnuMain.Items.Clear()
                        mnuRole.DropDownItems.Clear()
                        mnuRole.DropDownItems.AddRange(New ToolStripItem() {New ToolStripMenuItem("Thêm mới Role", Nothing, New EventHandler(AddressOf InsertRole_Click)), _
                                                                                                                         New ToolStripMenuItem("Cập nhật Role", Nothing, New EventHandler(AddressOf UpdateRole_Click))})

                        mnuMain.Items.AddRange(New ToolStripItem() {mnuSystem, mnuGroupUser, mnuUser, mnuFunction, mnuRole, mnuParameter, mnuUtility, mnuUpdateVersion, mnuHelp})
                        mnuMain.Items(1).Enabled = False
                        mnuMain.Items(2).Enabled = False
                        mnuMain.Items(3).Enabled = False
                        mnuMain.Items(4).Enabled = True
                        mnuMain.Items(5).Enabled = False
                        Me.Controls.Add(mnuMain)
                        Return
                    End If
                    Dim sv_sFunctionAndRoleID As String
                    sv_sFunctionAndRoleID = txtFunctionID.Text 'pv_sTag.Substring(pv_sTag.IndexOf("|") + 1)
                    If sv_sFunctionAndRoleID.Trim = "Chưa gán" OrElse sv_sFunctionAndRoleID.Trim = "" Then
                        sv_sFunctionAndRoleID = "-1"
                    End If
                    'Kiểm tra xem Node đã được gắn chức năng hay chưa
                    If sv_sFunctionAndRoleID = "-1" OrElse sv_sFunctionAndRoleID.Trim = "" Then 'Chưa được gắn chức năng
                        mnuMain.Items.Clear()
                        mnuRole.DropDownItems.Clear()
                        mnuRole.DropDownItems.AddRange(New ToolStripItem() {New ToolStripMenuItem("Thêm mới Role", Nothing, New EventHandler(AddressOf InsertRole_Click)), _
                                                                                                                     New ToolStripMenuItem("Cập nhật Role", Nothing, New EventHandler(AddressOf UpdateRole_Click)), _
                                                                                                                     New ToolStripSeparator(), _
                                                                                                                     New ToolStripMenuItem("Xóa Role", Nothing, New EventHandler(AddressOf DeleteRole_Click))})
                        mnuMain.Items.AddRange(New ToolStripItem() {mnuSystem, mnuGroupUser, mnuUser, mnuFunction, mnuRole, mnuParameter, mnuUtility, mnuUpdateVersion, mnuHelp})
                        mnuMain.Items(1).Enabled = False
                        mnuMain.Items(2).Enabled = False
                        mnuMain.Items(3).Enabled = False
                        mnuMain.Items(4).Enabled = True
                        mnuMain.Items(5).Enabled = False
                        Me.Controls.Add(mnuMain)
                        Return
                    Else 'Đã được gắn chức năng
                        mnuMain.Items.Clear()
                        mnuRole.DropDownItems.Clear()
                        mnuRole.DropDownItems.AddRange(New ToolStripItem() {New ToolStripMenuItem("Cập nhật Role", Nothing, New EventHandler(AddressOf UpdateRole_Click)), _
                                                                                                                     New ToolStripMenuItem("Xóa Role", Nothing, New EventHandler(AddressOf DeleteRole_Click)), _
                                                                                                                     New ToolStripSeparator(), _
                                                                                                                     New ToolStripMenuItem("Chuyển tới chức năng", Nothing, New EventHandler(AddressOf gotoFuntion))})
                        mnuMain.Items.AddRange(New ToolStripItem() {mnuSystem, mnuGroupUser, mnuUser, mnuFunction, mnuRole, mnuParameter, mnuUtility, mnuUpdateVersion, mnuHelp})
                        mnuMain.Items(1).Enabled = False
                        mnuMain.Items(2).Enabled = False
                        mnuMain.Items(3).Enabled = False
                        mnuMain.Items(4).Enabled = True
                        mnuMain.Items(5).Enabled = False
                        Me.Controls.Add(mnuMain)
                        Return
                    End If

                End If
                '----------------------------Nếu là các node chức năng------------------------------------
                If InStr(pv_sTag.ToUpper, "LEAFFUNCTIONS#") > 0 Then
                    gv_bCallContextMenuFromTreeView = True
                    If pv_oNode.ForeColor.Equals(gv_LockedFunctionColor) Then
                        mv_oContextMenuForLeafFunction.MenuItems(1).Text = "Kích hoạt chức năng"
                    Else
                        mv_oContextMenuForLeafFunction.MenuItems(1).Text = "Khóa chức năng"
                    End If
                    mnuMain.Items.Clear()
                    mnuFunction.DropDownItems.Clear()
                    mnuFunction.DropDownItems.AddRange(New ToolStripItem() {New ToolStripMenuItem("Cập nhật chức năng", Nothing, New EventHandler(AddressOf UpdateFunction_Click)), _
                                                                                                                              New ToolStripMenuItem("Khóa chức năng", Nothing, New EventHandler(AddressOf LockFunction_Click)), _
                                                                                                                              New ToolStripSeparator(), _
                                                                                                                              New ToolStripMenuItem("Xóa chức năng", Nothing, New EventHandler(AddressOf DeleteRole_Click))})

                    If pv_oNode.ForeColor.Equals(gv_LockedFunctionColor) Then
                        mnuFunction.DropDownItems(1).Text = "Kích hoạt chức năng"
                    Else
                        mnuFunction.DropDownItems(1).Text = "Khóa chức năng"
                    End If
                    mnuMain.Items.AddRange(New ToolStripItem() {mnuSystem, mnuGroupUser, mnuUser, mnuFunction, mnuRole, mnuParameter, mnuUtility, mnuUpdateVersion, mnuHelp})
                    mnuMain.Items(1).Enabled = False
                    mnuMain.Items(2).Enabled = False
                    mnuMain.Items(3).Enabled = True
                    mnuMain.Items(4).Enabled = False
                    mnuMain.Items(5).Enabled = False

                    Me.Controls.Add(mnuMain)
                    Return
                End If
        End Select
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Xử lý sự kiện MouseDown của TreeView để lấy về đúng SelectedNode hiện thời. Nếu không ta bắt buộc
    'phải nhấn trái chuột vào Node thì Node đó mới trở thành SelectedNode của TreeView
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :10/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------

    Private Sub tvwFunction_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvwAdminSystem.MouseDown
        Try
            If Not mv_bLoading Then
                If Not tvwAdminSystem.SelectedNode Is tvwAdminSystem.GetNodeAt(e.X, e.Y) Then tvwAdminSystem.SelectedNode = tvwAdminSystem.GetNodeAt(e.X, e.Y)
                tvwAdminSystem.ContextMenu = Nothing
                'Kiểm tra xem mức của Node đã đến cấp 5 chưa. Nếu là Node cấp 5 thì Node đó chỉ được phép
                'cập nhật hoặc xóa
                If mf_iGetLevelNode(tvwAdminSystem.SelectedNode) <= gv_intRoleLevel Then
                Else
                    tvwAdminSystem.ContextMenu = mv_oContextMenuForLeafRole
                    Return
                End If
                'Tạo ContextMenu cho TreeView theo từng Node được kích hoạt trên cây
                ms_ChooseContextMenu(tvwAdminSystem.SelectedNode, tvwAdminSystem.SelectedNode.Tag)
                ' Tùy biến MainMenu cho TreeView theo từng Node được kích hoạt trên cây
                'BuildMainMenu(tvwAdminSystem.SelectedNode, tvwAdminSystem.SelectedNode.Tag)

                If Not tvwAdminSystem.SelectedNode Is tvwAdminSystem.TopNode Then
                    mv_oNode = tvwAdminSystem.SelectedNode
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub
    'Với sự kiện DoubleClick của TreeView thì chỉ xử lý cho các LeafNode
    Private Sub tvwAdminSystem_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tvwAdminSystem.DoubleClick
        Try
            If Not mv_bLoading Then
                If InStr(tvwAdminSystem.SelectedNode.Tag.ToUpper, "LEAFFUNCTIONS#") > 0 Then
                    UpdateFunction_Click(sender, e)
                ElseIf InStr(tvwAdminSystem.SelectedNode.Tag.ToUpper, "LEAFGROUP#") > 0 Then
                    UpdateGroupUser_Click(sender, e)
                ElseIf InStr(tvwAdminSystem.SelectedNode.Tag.ToUpper, "LEAFUSER#") > 0 Then
                    UpdateUser_Click(sender, e)
                ElseIf InStr(tvwAdminSystem.SelectedNode.Tag.ToUpper, "NODEPARAM") > 0 Then
                    UpdateParam(sender, e)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub tvwAdminSystem_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwAdminSystem.AfterSelect
        Try
            If Not mv_bLoading Then
                gv_oNode = tvwAdminSystem.Nodes(0).Nodes(3).Clone
                tvwAdminSystem_Click(sender, New System.EventArgs)
                ' Tùy biến MainMenu cho TreeView theo từng Node được kích hoạt trên cây
                BuildMainMenu(tvwAdminSystem.SelectedNode, tvwAdminSystem.SelectedNode.Tag)
            End If
        Catch ex As Exception

        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Xử lý sự kiện Click của TreeView
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub tvwAdminSystem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tvwAdminSystem.Click
        If Not mv_bLoading Then
            'Kiểm tra xem cần tạo loại ContextMenu tương ứng nào
            Select Case tvwAdminSystem.SelectedNode.Tag.ToString.ToUpper
                Case "QTHT"
                    label5.Visible = True
                    label5.Dock = DockStyle.Fill
                    label5.BringToFront()
                    grbSubSystemInfor.Visible = False
                    grbRoleInfor.Visible = False
                    '                EnvisibleAllControls()
                    gv_intCurrRoleID = 0
                    If Not mv_bLoading Then If Not mv_bLoading Then StatusBar1.Panels(2).Text = " Bạn đang chọn Quản trị hệ thống"
                Case "ROOTGROUP" 'Đang chọn node nhóm Người dùng
                    gv_bCanUpdateByDblClickOnGrid = True
                    If Not pnl.Controls.Contains(grdGroup) Then
                        pnl.Controls.Add(grdGroup)
                    End If
                    gv_dsGroupUser.Tables(0).DefaultView.RowFilter = "1=1"
                    VisibleWhenClickGroupUser()
                    If Not mv_bLoading Then If Not mv_bLoading Then StatusBar1.Panels(2).Text = " Bạn đang chọn Node Nhóm người dùng "
                    'tbrAdminSystem.Buttons(5).Enabled = False
                    'tbrAdminSystem.Buttons(6).Enabled = False
                    gv_intCurrRoleID = 0
                Case "ROOTUSER" 'Đang chọn node Người dùng
                    gv_bCanUpdateByDblClickOnGrid = True
                    If Not pnl.Controls.Contains(grdUser) Then
                        pnl.Controls.Add(grdUser)
                    End If
                    gv_dsUser.Tables(0).DefaultView.RowFilter = "1=1"
                    VisibleWhenClickUser()
                    If Not mv_bLoading Then If Not mv_bLoading Then StatusBar1.Panels(2).Text = " Bạn đang chọn Node Users "
                    tbrAdminSystem.Buttons(5).Enabled = False
                    tbrAdminSystem.Buttons(6).Enabled = False
                    gv_intCurrRoleID = 0
                Case "ROOTFUNCTION" 'Đang chọn node Chức năng
                    gv_bCanUpdateByDblClickOnGrid = True
                    If Not pnl.Controls.Contains(grdFunction) Then
                        pnl.Controls.Add(grdFunction)
                    End If
                    WhichIsVisibleNow(grdFunction)
                    If Not mv_bLoading Then StatusBar1.Panels(2).Text = " Bạn đang chọn Node Functions "
                    tbrAdminSystem.Buttons(5).Enabled = False
                    tbrAdminSystem.Buttons(6).Enabled = False
                    gv_intCurrRoleID = 0
                Case "ROOTROLE#-2" 'Đang chọn node Roles
                    VisibleWhenClickRole(tvwAdminSystem.SelectedNode)
                    If Not mv_bLoading Then StatusBar1.Panels(2).Text = " Bạn đang chọn Node Roles "
                    tbrAdminSystem.Buttons(5).Enabled = False
                    tbrAdminSystem.Buttons(6).Enabled = False
                    gv_intCurrRoleID = 0
                Case "ROOTPARAM" 'Đang chọn node Parameters
                    VisibleWhenClickParam()
                    If Not mv_bLoading Then StatusBar1.Panels(2).Text = " Bạn đang chọn Node Parameters "
                    tbrAdminSystem.Buttons(5).Enabled = False
                    tbrAdminSystem.Buttons(6).Enabled = False
                    gv_intCurrRoleID = 0
                Case "NODEPARAM" 'Đang chọn node ParameterNode
                    VisibleWhenClickParam()
                    If Not mv_bLoading Then StatusBar1.Panels(2).Text = " Bạn đang chọn Tham số:" & tvwAdminSystem.SelectedNode.Text
                    tbrAdminSystem.Buttons(5).Enabled = False
                    tbrAdminSystem.Buttons(6).Enabled = False
                    gv_intCurrRoleID = 0
                Case Else 'Nếu không phải là các DefaultNode
                    '--------------Kiem tra neu la Node Seperator thi khong xu ly gi ca---------------------
                    If tvwAdminSystem.SelectedNode.Text.Trim.Equals("-") Then
                        cmdGetFunctionForRole.Enabled = False
                        cmdDelFunctionForRole.Enabled = False
                        gv_intCurrRoleID = 0
                        lblRoleName.Text = "DẤU PHÂN CÁCH GIỮA CÁC MENU ITEMS"
                        If Not mv_bLoading Then StatusBar1.Panels(2).Text = " Bạn đang chọn Role phân cách(Seperator) "
                        lblFunctionName.Text = ""
                        lblDLLName.Text = ""
                        lblFormName.Text = ""
                        '---------------------------------------------------------------------------
                        ModifyToolBarButton()
                        '---------------------------------------------------------------------------
                        Return
                    Else
                        cmdGetFunctionForRole.Enabled = True
                    End If
                    '----------------------------Nếu là các node Phân hệ------------------------------------
                    If InStr(tvwAdminSystem.SelectedNode.Tag.ToString.ToUpper, "LEAFROLES") > 0 Then
                        Dim iRoleID As Integer
                        iRoleID = CInt(tvwAdminSystem.SelectedNode.Tag.ToString.Substring(tvwAdminSystem.SelectedNode.Tag.ToString.IndexOf("#") + 1))
                        If gv_intCurrRoleID <> iRoleID Then
                            gv_bCanUpdateByDblClickOnGrid = False
                            If tvwAdminSystem.SelectedNode.GetNodeCount(True) > 0 Or tvwAdminSystem.SelectedNode.Parent.Tag.ToString.Trim.Equals("ROOTROLE#-2") Then
                                VisibleWhenClickRole(tvwAdminSystem.SelectedNode)
                            Else
                                VisibleWhenNodeHasNoChild()
                            End If
                            If Not mv_bLoading Then StatusBar1.Panels(2).Text = " Bạn đang chọn Role " & tvwAdminSystem.SelectedNode.Text
                        End If
                        If txtFunctionID.Text = "-1" Then
                            cmdDelFunctionForRole.Enabled = False
                            cmdGetFunctionForRole.Enabled = True
                        Else
                            cmdDelFunctionForRole.Enabled = True
                            cmdGetFunctionForRole.Enabled = True
                        End If
                        If grdFunction.VisibleRowCount > 0 Then
                        Else
                            cmdDelFunctionForRole.Enabled = False
                            cmdGetFunctionForRole.Enabled = False
                        End If
                        'Không cho gắn chức năng trên RootRole và Role Menu cấp 1
                        If tvwAdminSystem.SelectedNode.Parent.Text.ToUpper.Equals("ROLES") Or tvwAdminSystem.SelectedNode.Parent.Parent.Text.ToUpper.Equals("ROLES") Then
                            cmdDelFunctionForRole.Enabled = False
                            cmdGetFunctionForRole.Enabled = False
                        End If

                        Return
                    End If
                    '----------------------------Nếu là các node chức năng------------------------------------
                    If InStr(tvwAdminSystem.SelectedNode.Tag.ToString.ToUpper, "LEAFFUNCTIONS#") > 0 Then
                        gv_intCurrRoleID = 0
                        gv_bCanUpdateByDblClickOnGrid = True
                        WhichIsVisibleNow(grdFunction)
                        If Not mv_bLoading Then StatusBar1.Panels(2).Text = " Bạn đang chọn Chức năng " & tvwAdminSystem.SelectedNode.Text
                        tbrAdminSystem.Buttons(5).Enabled = False
                        tbrAdminSystem.Buttons(6).Enabled = False
                        Return
                    End If
                    If InStr(tvwAdminSystem.SelectedNode.Tag.ToString.ToUpper, "LEAFGROUP#") > 0 Then
                        gv_dsGroupUser.Tables(0).DefaultView.RowFilter = "GroupID<>" & CInt(tvwAdminSystem.SelectedNode.Tag.ToString().Substring(tvwAdminSystem.SelectedNode.Tag.ToString().IndexOf("#") + 1)) & ""
                        gv_intCurrRoleID = 0
                        gv_bCanUpdateByDblClickOnGrid = False
                        VisibleWhenClickLeafGroup(CInt(tvwAdminSystem.SelectedNode.Tag.ToString().Substring(tvwAdminSystem.SelectedNode.Tag.ToString().IndexOf("#") + 1)), tvwAdminSystem.SelectedNode.Text.Trim)
                        If Not mv_bLoading Then StatusBar1.Panels(2).Text = " Bạn đang chọn nhóm người dùng: " & tvwAdminSystem.SelectedNode.Text
                        tbrAdminSystem.Buttons(5).Enabled = False
                        tbrAdminSystem.Buttons(6).Enabled = False
                        Return
                    End If
                    If InStr(tvwAdminSystem.SelectedNode.Tag.ToString.ToUpper, "LEAFUSER#") > 0 Then
                        gv_dsUser.Tables(0).DefaultView.RowFilter = "PK_sUID<>'" & tvwAdminSystem.SelectedNode.Text.Trim & "'"
                        gv_intCurrRoleID = 0
                        gv_bCanUpdateByDblClickOnGrid = False
                        VisibleWhenClickLeafUser(tvwAdminSystem.SelectedNode.Text.Trim)
                        If Not mv_bLoading Then StatusBar1.Panels(2).Text = " Bạn đang chọn User " & tvwAdminSystem.SelectedNode.Text
                        tbrAdminSystem.Buttons(5).Enabled = False
                        tbrAdminSystem.Buttons(6).Enabled = False
                        Return
                    End If
            End Select
        End If
    End Sub
    Private Sub ModifyToolBarButton()
        If Not tvwAdminSystem.SelectedNode.PrevNode Is Nothing And tvwAdminSystem.SelectedNode.NextNode Is Nothing Then
            tbrAdminSystem.Buttons(5).Enabled = True
            tbrAdminSystem.Buttons(6).Enabled = False
        End If
        If tvwAdminSystem.SelectedNode.PrevNode Is Nothing And Not tvwAdminSystem.SelectedNode.NextNode Is Nothing Then
            tbrAdminSystem.Buttons(5).Enabled = False
            tbrAdminSystem.Buttons(6).Enabled = True
        End If
        If Not tvwAdminSystem.SelectedNode.PrevNode Is Nothing And Not tvwAdminSystem.SelectedNode.NextNode Is Nothing Then
            tbrAdminSystem.Buttons(5).Enabled = True
            tbrAdminSystem.Buttons(6).Enabled = True
        End If
        If tvwAdminSystem.SelectedNode.PrevNode Is Nothing And tvwAdminSystem.SelectedNode.NextNode Is Nothing Then
            tbrAdminSystem.Buttons(5).Enabled = False
            tbrAdminSystem.Buttons(6).Enabled = False
        End If
    End Sub
#End Region

#Region "Các hàm phục vụ cho việc xác định ẩn hiện Grid hoặc các Control khi nhấn vào các Nút tương ứng trên cây QTHT"
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Hiển thị các Control tương ứng phía bên phải khi nhấn vào các LeafUser hoặc RootUser không có con
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub VisibleWhenClickLeafUser(ByVal pv_sUID As String)
        Dim sv_oCtr As Control
        Dim sv_iRoleID As Integer
        Dim sv_oDR As DataRow
        Dim sv_oDRRolesForUsers As DataRow()
        Try
            'Lấy về tên UserName
            mv_sUID = pv_sUID
            'Gắn các RoleNode vào cây để chuẩn bị gán quyền cho từng Users
            'Kiểm tra để xem nhánh Role chính đã bị thay đổi chưa. Nếu chưa thay đổi thì không cần Clone lại để gắn vào nữa
            If gv_bRoleHasChanged And tvwRoleForUser.GetNodeCount(False) > 0 Then
                tvwRoleForUser.Nodes.Clear()
                tvwRoleForUser.Nodes.Add(tvwAdminSystem.Nodes(0).Nodes(3).Clone)
            ElseIf Not gv_bRoleHasChanged And tvwRoleForUser.GetNodeCount(False) = 0 Then
                tvwRoleForUser.Nodes.Clear()
                tvwRoleForUser.Nodes.Add(tvwAdminSystem.Nodes(0).Nodes(3).Clone)
            End If
            gv_bRoleHasChanged = False
            With tvwRoleForUser
                .TopNode.ImageIndex = ImageIndex.RootRole
                .TopNode.SelectedImageIndex = ImageIndex.RootRole
                .ImageIndex = ImageIndex.LeafRole
                .SelectedImageIndex = ImageIndex.LeafRole
            End With
            tvwRoleForUser.SelectedNode = tvwRoleForUser.Nodes(0)
            tvwRoleForUser.Nodes(0).Expand()
            'Lọc về các quyền của User
            Try
                sv_oDRRolesForUsers = gv_dsRolesForUsers.Tables(0).Select("sUID='" & mv_sUID & "'", "iParentRoleID,iRoleID,iOrder")
                mv_oDTRolesForUser = gv_dsRolesForUsers.Tables(0).Clone
                mv_oDTRolesForUser.Rows.Clear()
                For Each sv_oDR In sv_oDRRolesForUsers
                    mv_oDTRolesForUser.ImportRow(sv_oDR)
                Next
                With grdRolesForUsers
                    .TableStyles(0).MappingName = "Sys_RFU"
                    .DataSource = mv_oDTRolesForUser.DefaultView
                    .CaptionText = "Người dùng " & mv_sUID & " - Tổng số quyền được cấp: " & mv_oDTRolesForUser.Rows.Count
                End With
                mv_oDTRolesForUser.DefaultView.AllowNew = False
                mv_oDTRolesForUser.DefaultView.AllowDelete = False
                mv_oDTRolesForUser.DefaultView.AllowEdit = False
            Catch ex As Exception

            End Try

            'Hiển thị thông tin về quyền của User
            For Each sv_oCtr In pnl.Controls
                If Not sv_oCtr Is Nothing Then
                    If sv_oCtr.Equals(pnl1) Then
                        sv_oCtr.Visible = True
                        sv_oCtr.Dock = DockStyle.Fill
                        If Not TabCtr.TabPages(1).Controls.Contains(grdUser) Then
                            TabCtr.TabPages(1).Controls.Add(grdUser)
                        End If
                        With grdUser
                            .Visible = True
                            .Dock = DockStyle.Fill
                        End With
                    Else
                        sv_oCtr.Visible = False
                    End If
                End If
            Next
            'Hiển thị SecurityLevel của User
            Dim sv_oUser As New clsUser
            chkAllRole.Checked = sv_oUser.iGetSecurityLevel(mv_sUID)
            sv_oUser = Nothing
        Catch ex As Exception
        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Hiển thị các Control tương ứng phía bên phải khi nhấn vào các LeafUser hoặc RootUser không có con
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub VisibleWhenClickLeafGroup(ByVal pv_GroupID As Integer, ByVal groupName As String)
        Dim sv_oCtr As Control
        Dim sv_iRoleID As Integer
        Dim sv_oDR As DataRow
        Dim sv_oDRRolesForGroups As DataRow()
        Try
            'Lấy về tên GroupID va GroupName
            mv_GroupID = pv_GroupID
            mv_GroupName = groupName
            'Gắn các RoleNode vào cây để chuẩn bị gán quyền cho từng GroupUsers
            'Kiểm tra để xem nhánh Role chính đã bị thay đổi chưa. Nếu chưa thay đổi thì không cần Clone lại để gắn vào nữa
            If gv_bRoleHasChanged And tvwRolesForGroup.GetNodeCount(False) > 0 Then
                tvwRolesForGroup.Nodes.Clear()
                tvwRolesForGroup.Nodes.Add(tvwAdminSystem.Nodes(0).Nodes(3).Clone)
            ElseIf Not gv_bRoleHasChanged And tvwRolesForGroup.GetNodeCount(False) = 0 Then
                tvwRolesForGroup.Nodes.Clear()
                tvwRolesForGroup.Nodes.Add(tvwAdminSystem.Nodes(0).Nodes(3).Clone)
            End If
            gv_bRoleHasChanged = False
            With tvwRolesForGroup
                .TopNode.ImageIndex = ImageIndex.RootRole
                .TopNode.SelectedImageIndex = ImageIndex.RootRole
                .ImageIndex = ImageIndex.LeafRole
                .SelectedImageIndex = ImageIndex.LeafRole
            End With
            tvwRolesForGroup.SelectedNode = tvwRolesForGroup.Nodes(0)
            tvwRolesForGroup.Nodes(0).Expand()
            'Lọc về các quyền của GroupUser
            Try
                sv_oDRRolesForGroups = gv_dsGroupRoles.Tables(0).Select("GroupID=" & mv_GroupID, "iParentRoleID,iRoleID,iOrder")
                mv_oDTRolesForGroup = gv_dsGroupRoles.Tables(0).Clone
                mv_oDTRolesForGroup.Rows.Clear()
                For Each sv_oDR In sv_oDRRolesForGroups
                    mv_oDTRolesForGroup.ImportRow(sv_oDR)
                Next
                With grdRoleForGroup
                    .AutoGenerateColumns = False
                    .DataSource = mv_oDTRolesForGroup.DefaultView
                    '.Caption = "Nhóm người dùng " & mv_GroupName & " - Tổng số quyền được cấp: " & mv_oDTRolesForGroup.Rows.Count
                End With
                mv_oDTRolesForGroup.DefaultView.AllowNew = False
                mv_oDTRolesForGroup.DefaultView.AllowDelete = False
                mv_oDTRolesForGroup.DefaultView.AllowEdit = False
            Catch ex As Exception

            End Try

            'Hiển thị thông tin về quyền của GroupUser
            For Each sv_oCtr In pnl.Controls
                If Not sv_oCtr Is Nothing Then
                    If sv_oCtr.Equals(pnlGroupUser) Then
                        sv_oCtr.Visible = True
                        sv_oCtr.Dock = DockStyle.Fill
                        If Not TabPage4.Controls.Contains(grdGroup) Then
                            TabPage4.Controls.Add(grdGroup)
                        End If
                        With grdGroup
                            .Visible = True
                            .Dock = DockStyle.Fill
                        End With
                    Else
                        sv_oCtr.Visible = False
                    End If
                End If
            Next
            'Hiển thị SecurityLevel của GroupUser
            Dim sv_oUser As New clsUser
            chkIsAdmin.Checked = sv_oUser.iGetSecurityLevelOfGroup(mv_GroupID)
            sv_oUser = Nothing
        Catch ex As Exception
        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Hiển thị danh sách các tham số khi nhấn vào node Tham số hệ thống
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub VisibleWhenClickParam()
        Try
            With grdParamList
                .TableStyles(0).MappingName = "Sys_Params"
                .DataSource = gv_dsParam.Tables(0).DefaultView
                .CaptionText = "Tổng số " & gv_dsParam.Tables(0).Rows.Count & " tham số"
            End With
            gv_dsParam.Tables(0).DefaultView.AllowNew = False
            WhichIsVisibleNow(grdParamList)
            'pnl.Controls.Add(grdUser)
        Catch ex As Exception

        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Hiển thị các Control tương ứng phía bên phải khi nhấn vào  RootUser
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub VisibleWhenClickGroupUser()
        Try
            With grdGroup
                .AutoGenerateColumns = False
                '.TableStyles(0).MappingName = "Sys_Groups"
                .DataSource = gv_dsGroupUser.Tables(0).DefaultView
                '.CaptionText = "Tổng số " & gv_dsGroupUser.Tables(0).Rows.Count & " nhóm người dùng"
            End With
            gv_dsGroupUser.Tables(0).DefaultView.AllowNew = False
            WhichIsVisibleNow(grdGroup)
            'pnl.Controls.Add(grdUser)
        Catch ex As Exception

        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Hiển thị các Control tương ứng phía bên phải khi nhấn vào  RootUser
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub VisibleWhenClickUser()
        Try
            With grdUser
                .TableStyles(0).MappingName = "Sys_USERS"
                .DataSource = gv_dsUser.Tables(0).DefaultView
                .CaptionText = "Tổng số " & gv_dsUser.Tables(0).Rows.Count & " người dùng"
            End With
            gv_dsUser.Tables(0).DefaultView.AllowNew = False
            WhichIsVisibleNow(grdUser)
            'pnl.Controls.Add(grdUser)
        Catch ex As Exception

        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Hiển thị các Control tương ứng phía bên phải khi nhấn vào các LeafRole  có con
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub VisibleWhenClickRole(ByVal pv_oNode As TreeNode)
        Dim sv_oDT As DataTable
        Dim sv_oDR As DataRow()
        Dim sv_oDRElement As DataRow
        Try

            If pv_oNode.GetNodeCount(True) > 0 Then 'Nếu không là Node lá
                sv_oDR = gv_dsRole.Tables(0).Select("iParentRole=" & CInt(pv_oNode.Tag.ToString.Substring(pv_oNode.Tag.ToString.IndexOf("#") + 1)))
                sv_oDT = gv_dsRole.Tables(0).Clone
                For Each sv_oDRElement In sv_oDR
                    sv_oDT.ImportRow(sv_oDRElement)
                Next
                With grdChildRole
                    .CaptionText = "Tổng số " & sv_oDT.Rows.Count & " Role con"
                    .TableStyles(0).MappingName = "Sys_ROLES"
                    .DataSource = sv_oDT.DefaultView
                End With
                sv_oDT.DefaultView.AllowNew = False
                sv_oDT.DefaultView.AllowEdit = False
                sv_oDT.DefaultView.AllowDelete = False

            Else 'Nếu là Node Lá
            End If
            '-------------------------------------------------------------------------------------------------
            ModifyToolBarButton()
            '-------------------------------------------------------------------------------------------------
            If pv_oNode.Tag.ToString.Trim.Equals("ROOTROLE#-2") Then
                gv_intCurrRoleID = CInt(pv_oNode.Tag.ToString.Substring(pv_oNode.Tag.ToString.IndexOf("#") + 1))
                WhichIsVisibleNow(grdChildRole)
            Else
                'WhichIsVisibleNow(grdChildRole, pnlMove)
                WhichIsVisibleNow(grdChildRole)
                If pv_oNode.Parent.Tag.ToString.Trim.Equals("ROOTROLE#-2") Then
                    'grdChildRole.Dock = DockStyle.Top
                    grbSubSystemInfor.Visible = True
                    grbSubSystemInfor.Dock = DockStyle.Bottom
                    grbSubSystemInfor.Height = CInt(pnl.Height / 2)
                    grdChildRole.Dock = DockStyle.Fill
                    picSubSystem.Image = Nothing
                    If gv_intSubSysID = CInt(pv_oNode.Tag.ToString.Substring(pv_oNode.Tag.ToString.IndexOf("#") + 1)) Then
                    Else
                        TabControl1.SelectedIndex = 0
                        gv_bChangeToolBar = True
                    End If
                    gv_intCurrRoleID = CInt(pv_oNode.Tag.ToString.Substring(pv_oNode.Tag.ToString.IndexOf("#") + 1))
                    gv_intSubSysID = CInt(pv_oNode.Tag.ToString.Substring(pv_oNode.Tag.ToString.IndexOf("#") + 1))
                    gv_sSubSysName = pv_oNode.Text
                    Try
                        Dim sv_iParentRoleID As Integer = CInt(tvwAdminSystem.SelectedNode.Tag.ToString.Substring(tvwAdminSystem.SelectedNode.Tag.ToString.IndexOf("#") + 1))
                        Dim sPath As String
                        sPath = gv_dsRole.Tables(0).Select("iRole=" & sv_iParentRoleID)(0)("sImgPath")
                        If File.Exists(sPath) Then
                            picSubSystem.Image = Image.FromFile(sPath)
                        Else
                            picSubSystem.Image = Nothing
                        End If

                    Catch ex As Exception

                    End Try
                Else
                    gv_intCurrRoleID = CInt(pv_oNode.Tag.ToString.Substring(pv_oNode.Tag.ToString.IndexOf("#") + 1))
                End If
            End If
            tvwAdminSystem.Focus()
        Catch ex As Exception

        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Thủ tục xác định xem Control nào thuộc Panel sẽ được hiển thị hoặc bị ẩn khi xảy ra sự kiện
    '                       Click của TreeView
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub WhichIsVisibleNow(ByVal pv_oCtr As Control, Optional ByVal pSubObjectToDisplay As Object = Nothing)
        Dim sv_oCtr As Control
        Try
            For Each sv_oCtr In pnl.Controls
                If sv_oCtr.Equals(pv_oCtr) Then
                    sv_oCtr.Dock = DockStyle.Fill
                    sv_oCtr.Visible = True
                    sv_oCtr.BringToFront()
                Else
                    sv_oCtr.Visible = False
                End If
            Next
            If Not pSubObjectToDisplay Is Nothing Then
                CType(pSubObjectToDisplay, Panel).Visible = True
                CType(pSubObjectToDisplay, Panel).BringToFront()
                '-------------------------------------------------------------------------------------------------
                ModifyToolBarButton()
                '-------------------------------------------------------------------------------------------------
            End If

        Catch ex As Exception
        End Try
    End Sub


    Private Sub EnvisibleAllControls()
        Dim sv_oCtr As Control
        For Each sv_oCtr In pnl.Controls
            If Not sv_oCtr.Name.Equals(label5.Name) Then
                sv_oCtr.Visible = False
                sv_oCtr.SendToBack()
            Else
                sv_oCtr.Visible = True
                sv_oCtr.BringToFront()
                sv_oCtr.Dock = DockStyle.Fill
            End If
        Next
    End Sub

    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Hiển thị các Control tương ứng phía bên phải khi nhấn vào các LeafRole không có con
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub VisibleWhenNodeHasNoChild()
        Dim sv_oCtr As Control
        Dim sv_iRoleID As Integer
        Dim sv_oDR As DataRow
        Try
            For Each sv_oCtr In pnl.Controls
                If sv_oCtr.Equals(grbRoleInfor) Or sv_oCtr.Equals(grdFunction) Then
                    sv_oCtr.Visible = True
                    If sv_oCtr.Equals(grbRoleInfor) Then
                        sv_oCtr.Dock = DockStyle.Bottom
                    Else
                        sv_oCtr.Dock = DockStyle.Fill
                    End If
                Else
                    sv_oCtr.Visible = False
                End If
            Next
            ModifyToolBarButton()
            'Lấy thông tin của Role để đưa vào phần hiển thị thuộc tính
            sv_iRoleID = CInt(tvwAdminSystem.SelectedNode.Tag.ToString.Substring(tvwAdminSystem.SelectedNode.Tag.ToString.IndexOf("#") + 1))
            gv_intCurrRoleID = CInt(tvwAdminSystem.SelectedNode.Tag.ToString.Substring(tvwAdminSystem.SelectedNode.Tag.ToString.IndexOf("#") + 1))
            For Each sv_oDR In gv_dsRole.Tables(0).Rows
                If sv_oDR.Item("iRole") = sv_iRoleID Then
                    txtFunctionID.Text = IIF_VN(sv_oDR.Item("FK_iFunctionID"))
                    lblRoleName.Text = IIF_VN(sv_oDR.Item("sRoleName"))
                    lblFunctionName.Text = IIF_VN(sv_oDR.Item("sFunctionName"))
                    lblDLLName.Text = IIF_VN(sv_oDR.Item("sDLLName"))
                    lblFormName.Text = IIF_VN(sv_oDR.Item("sFormName"))
                    Dim sv_bHasShortCutKey As Boolean = False
                    For i As Integer = 0 To CboValue.Items.Count - 1
                        If CboValue.Items(i).ToString.Trim = CStr(IIF_VN(sv_oDR.Item("intShortCutKey"))) Then
                            CboValue.SelectedIndex = i
                            sv_bHasShortCutKey = True
                            Exit For
                        End If
                    Next
                    If Not sv_bHasShortCutKey Then
                        CboValue.SelectedIndex = -1
                    End If
                    If Not IIF_VN(sv_oDR.Item("sIconPath")).Trim.ToUpper.Equals("UNKNOWN") And Not IsDBNull(sv_oDR.Item("sIconPath")) And File.Exists(IIF_VN(sv_oDR.Item("sIconPath"))) Then
                        picIcon.Image = Image.FromFile(sv_oDR.Item("sIconPath"))
                    Else
                        picIcon.Image = Nothing
                    End If
                    'Select chức năng tương ứng trên DataGrid
                    If txtFunctionID.Text.Trim <> "-1" Then
                        SearchFunctionOnGrid(lblFunctionName.Text, lblDLLName.Text.Trim.ToUpper, lblFormName.Text.Trim.ToUpper)
                    Else
                        If grdFunction.VisibleRowCount > 0 Then
                            grdFunction.UnSelect(grdFunction.CurrentRowIndex)
                            'For i As Integer = 0 To CType(grdFunction.DataSource, DataView).Table.Rows.Count - 1
                            '    grdFunction.UnSelect(i)
                            'Next
                            'grdFunction.CurrentCell = New DataGridCell(0, 0)
                            'grdFunction.Select(0)
                        End If
                    End If
                    tvwAdminSystem.Focus()
                    Exit For
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Các hàm và sự kiện khác"
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Lấy về cấp(Level) của Node để xem liệu Node đó có được phép đẻ con nữa không^_^
    '                      : Ở đây chương trình chỉ cho phép tạo các Node đến Level5
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Function mf_iGetLevelNode(ByVal pv_oNode As TreeNode) As Integer
        Dim sv_sSplit As String()
        Try
            sv_sSplit = pv_oNode.FullPath.Split("\")
            Return sv_sSplit.Length - 3 'Trừ đi Quản trị hệ thống\Roles\SubSystem\...

        Catch ex As Exception

        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Sự kiện nhấn các nút trên ToolBar
    'Đầu vào          :
    'Đầu ra            :Chọn Node tương ứng trên cây hoặc thực hiện một số chức năng cơ bản khác
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub tbrAdminSystem_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles tbrAdminSystem.ButtonClick
        Try
            Select Case e.Button.ImageIndex
                Case tbrAdminSystem.Buttons(0).ImageIndex
                    LoginSystem(sender, New System.EventArgs)
                Case tbrAdminSystem.Buttons(1).ImageIndex ' Sự kiện bấm vào Node người dùng
                    tvwAdminSystem.SelectedNode = tvwAdminSystem.Nodes(0).Nodes(1)
                Case tbrAdminSystem.Buttons(2).ImageIndex 'Sự kiện bấm vào Node Chức năng
                    tvwAdminSystem.SelectedNode = tvwAdminSystem.Nodes(0).Nodes(2)
                Case tbrAdminSystem.Buttons(3).ImageIndex 'Sự kiện bấm vào Node Roles
                    tvwAdminSystem.SelectedNode = tvwAdminSystem.Nodes(0).Nodes(3)
                Case tbrAdminSystem.Buttons(4).ImageIndex 'Sự kiện bấm vào Node Tham số
                    tvwAdminSystem.SelectedNode = tvwAdminSystem.Nodes(0).Nodes(4)
                Case tbrAdminSystem.Buttons(5).ImageIndex 'Sự kiện nhấn nút Up để chuyển Role lên phía trên
                    cmdUp_Click(sender, e)
                Case tbrAdminSystem.Buttons(6).ImageIndex 'Sự kiện nhấn nút Down để chuyển Role xuống phía dưới
                    cmdDown_Click(sender, e)
                Case tbrAdminSystem.Buttons(7).ImageIndex 'Sự kiện nhấn nút tìm kiếm trên TreeView
                    SearchStart()
                Case tbrAdminSystem.Buttons(8).ImageIndex 'Sự kiện nhấn nút thoát khỏi CT
                    Application.Exit()
            End Select
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BackUpAndRestoreDB(ByVal sender As Object, ByVal e As EventArgs)
        MessageBox.Show("Bạn có thể chạy chức năng này bằng tiện ích BackupAndRestore.exe. Trong trường hợp RestoreDB bạn phải thoát khỏi mọi ứng dụng đang chạy trên CSDL muốn Restore", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
        'Dim sv_oBackUpForm As New frm_BackupAndRestore
        'Try
        '    sv_oBackUpForm.ShowDialog()
        'Catch ex As Exception

        'End Try
    End Sub
    Private Sub GenCode(ByVal sender As Object, ByVal e As EventArgs)
        'Dim GenCode As New CodeGenerator
        'GenCode.ConnStr = globalModule.mv_sConnString
        'GenCode.ShowDialog()
    End Sub
    Private Sub UpdateBranchInfor(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_oForm As New Frm_AddBranch
        sv_oForm.mv_bInsert = False
        sv_oForm.ShowDialog()
    End Sub
    Private Sub InsertBranchInfor(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_oForm As New Frm_AddBranch
        sv_oForm.mv_bInsert = True
        sv_oForm.ShowDialog()
    End Sub
    Private Sub MsgMan(ByVal sender As Object, ByVal e As EventArgs)
        Dim sv_oForm As New frmMessageList
        sv_oForm.ShowDialog()
    End Sub
    Private Sub ChangeAdminPWD(ByVal sender As Object, ByVal e As EventArgs)

        Try
            Dim sv_oForm As New frm_ChangePwd
            sv_oForm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ExitApp(ByVal sender As Object, ByVal e As EventArgs)

        Try
            Application.Exit()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _Option(ByVal sender As Object, ByVal e As EventArgs)

        Try
            Dim sv_oForm As New frm_Options
            sv_oForm.ShowDialog()
            tvwAdminSystem.AllowDrop = gv_bEnableDragAndDrop
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _InsertImgAndIcon(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim sv_oForm As New frm_ListImgsAndIcons
            sv_oForm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _UpdateVersion(ByVal sender As Object, ByVal e As EventArgs)

        Try
            Dim sv_oForm As New frm_ListUpdateVersion
            sv_oForm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub _UpdateVersionBatch(ByVal sender As Object, ByVal e As EventArgs)

        Try
            Dim sv_oForm As New frm_UpdateVersionBatch
            sv_oForm.ShowDialog()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub _RoleToExelFile(ByVal sender As Object, ByVal e As EventArgs)

        Try
            Dim sv_oForm As New Frm_XuatMenuExcel
            sv_oForm.mv_oNode = tvwAdminSystem.Nodes(0).Nodes(3).Clone
            sv_oForm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _ConfigurationnOutput(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim sv_oForm As New frm_ConfigurationOutput
            sv_oForm.mv_bOutIn = True
            sv_oForm.mv_oUserNode = tvwAdminSystem.Nodes(0).Nodes(1).Clone
            sv_oForm.mv_oRoleNode = tvwAdminSystem.Nodes(0).Nodes(3).Clone
            sv_oForm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _ConfigurationnInput(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim sv_oForm As New frm_GetXMLFile
            sv_oForm.ShowDialog()
            If Not sv_oForm.mv_bCancel Then
                tvwAdminSystem.Nodes.Clear()
                mv_bLoading = True
                Initialize(sender, e)
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub _Help(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If File.Exists(Application.StartupPath & "\Help\HelpFile.chm") Then
                Help.ShowHelp(Me, Application.StartupPath & "\Help\HelpFile.chm")
            Else
                MessageBox.Show("Chưa có file Help sau " & Application.StartupPath & "\Help\HelpFile.chm", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub About(ByVal sender As Object, ByVal e As EventArgs)

        Try
            Dim sv_oFrmAbout As New AboutBox1
            sv_oFrmAbout.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub SearchFunctionOnGrid(ByVal pv_sFunctionName As String, ByVal pv_sDLLName As String, ByVal pv_sFormName As String)
        Dim j As Integer = -1
        Try
            If grdFunction.VisibleRowCount > 0 Then
                grdFunction.UnSelect(grdFunction.CurrentRowIndex)
                For i As Integer = 0 To CType(grdFunction.DataSource, DataView).Table.Rows.Count - 1
                    If grdFunction.Item(i, 2).ToString.ToUpper.Equals(pv_sFunctionName.ToUpper) And grdFunction.Item(i, 3).ToString.ToUpper.Equals(pv_sDLLName.ToUpper) And grdFunction.Item(i, 4).ToString.ToUpper.Equals(pv_sFormName.ToUpper) Then
                        j = i
                        Exit For
                    Else
                        grdFunction.UnSelect(i)
                    End If
                Next
                If j <> -1 Then
                    grdFunction.Select(j)
                    grdFunction.CurrentCell = New DataGridCell(j, 0)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tvwRoleForUser_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvwRoleForUser.DoubleClick
        Try
            If gv_bCanDblClickToGetRolesForUser Then
                Me.Cursor = Cursors.WaitCursor
                'Tìm kiếm tất cả các ChildRole cho User
                ms_FindAllChildRoles(tvwRoleForUser.SelectedNode)
                'Tìm kiếm tất cả các ParentRole cho User
                ms_FindAllParentRoles(tvwRoleForUser.SelectedNode)
                grdRolesForUsers.CaptionText = "Người dùng " & mv_sUID & " - Tổng số quyền được cấp: " & mv_oDTRolesForUser.Rows.Count
                Me.Cursor = Cursors.Default
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub tvwRolesForGroup_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvwRolesForGroup.DoubleClick
        Try
            If gv_bCanDblClickToGetRolesForUser Then
                Me.Cursor = Cursors.WaitCursor
                'Tìm kiếm tất cả các ChildRole cho User
                ms_FindAllChildRolesOfGroup(tvwRolesForGroup.SelectedNode)
                'Tìm kiếm tất cả các ParentRole cho User
                ms_FindAllParentRolesOfGroup(tvwRolesForGroup.SelectedNode)
                'grdRolesForUsers.CaptionText = "Người dùng " & mv_sUID & " - Tổng số quyền được cấp: " & mv_oDTRolesForUser.Rows.Count
                Me.Cursor = Cursors.Default
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    'Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    '    Try
    '        'Thực hiện trình diễn Form
    '        gs_SlideMe(Me, 10 / 100, Me.Timer1, gv_bIncreateOrDecrete)
    '    Catch ex As Exception
    '    End Try
    'End Sub
    Private Sub frm_QTHT_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            'Điều khiển thuộc tính Cancel theo độ mờ dần của Form. Nếu không Form sẽ bị đóng ngay khi nhấn nút
            'X của Form
            'e.Cancel = IIf(Me.Opacity > 0, True, False)
            'Gọi tiếp sự kiện đóng Form để trình diễn độ mờ khi Opacity>0
            MenuItem2_Click(sender, e)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            gv_bIncreateOrDecrete = False
            If Timer1.Enabled = False Then
                Timer1.Enabled = True
            End If
        Catch ex As Exception
            Me.Close()
        End Try
    End Sub
    Private Sub lblLoading_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblLoading.DoubleClick
        ShowCollection(sender, e)
    End Sub
#End Region

#Region "Các hàm phục vụ cho việc gán quyền cho một User"
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Duyệt đệ quy để gán quyền cho một User khi chọn quyền trên cây. Thủ tục này lấy về
    '                        Tất cả các ChildRole của Role hiện thời
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub ms_FindAllChildRolesOfGroup(ByVal pv_oNode As TreeNode)
        Dim sv_iRoleID As Integer
        Dim sv_iParentID As Integer
        Dim sv_oClsRole As New clsRole
        Dim sv_sRoleName As String = String.Empty
        Dim sv_sFunctionName As String = String.Empty
        Dim sv_sDLLName As String = String.Empty
        Dim sv_sFormName As String = String.Empty
        Dim sv_oDRRole() As DataRow
        Try
            'Lấy về mã Role cha(Là RoleID của Node hiện thời)
            sv_iParentID = CInt(pv_oNode.Tag.ToString.Substring(pv_oNode.Tag.ToString.IndexOf("#") + 1))
            'Kiểm tra xem có là LeafRole không
            If pv_oNode.GetNodeCount(True) > 0 Then
                Dim sv_oNode As TreeNode
                For Each sv_oNode In pv_oNode.Nodes
                    sv_iRoleID = CInt(sv_oNode.Tag.ToString.Substring(sv_oNode.Tag.ToString.IndexOf("#") + 1))
                    If sv_oClsRole.bAddRoleForGroupUser(mv_GroupID, sv_iRoleID, sv_iParentID, gv_sBranchID) Then
                        'Nếu thêm thành công quyền mới cho KH thì cũng thêm mới vào DataSet để Refesh lại DataGrid
                        sv_oDRRole = gv_dsRole.Tables(0).Select("iRole=" & sv_iRoleID)
                        sv_sRoleName = IIf(IsDBNull(sv_oDRRole(0).Item("sRoleName")), "Không biết", sv_oDRRole(0).Item("sRoleName"))
                        sv_sFunctionName = IIf(IsDBNull(sv_oDRRole(0).Item("sFunctionName")), "Không biết", sv_oDRRole(0).Item("sFunctionName"))
                        sv_sDLLName = IIf(IsDBNull(sv_oDRRole(0).Item("sDLLName")), "Không biết", sv_oDRRole(0).Item("sDLLName"))
                        sv_sFormName = IIf(IsDBNull(sv_oDRRole(0).Item("sFormName")), "Không biết", sv_oDRRole(0).Item("sFormName"))
                        Dim sv_oDR As DataRow
                        sv_oDR = gv_dsGroupRoles.Tables(0).NewRow
                        With sv_oDR
                            .Item("CHON") = "F"
                            .Item("GroupID") = mv_GroupID
                            .Item("iRoleID") = sv_iRoleID
                            .Item("iParentRoleID") = sv_iParentID
                            .Item("sRoleName") = sv_sRoleName
                            .Item("sDLLName") = sv_sDLLName
                            .Item("sFunctionName") = sv_sFunctionName
                            .Item("sFormName") = sv_sFormName
                        End With

                        'Thêm quyền vào DataSet
                        gv_dsGroupRoles.Tables(0).Rows.Add(sv_oDR)
                        gv_dsGroupRoles.Tables(0).AcceptChanges()
                        '
                        Dim sv_oDRRolesForGroupUsers As DataRow()
                        sv_oDRRolesForGroupUsers = gv_dsGroupRoles.Tables(0).Select("GroupID=" & mv_GroupID, "iParentRoleID ASC")
                        mv_oDTRolesForGroup = gv_dsGroupRoles.Tables(0).Clone
                        mv_oDTRolesForGroup.Rows.Clear()
                        For Each sv_oDR In sv_oDRRolesForGroupUsers
                            mv_oDTRolesForGroup.ImportRow(sv_oDR)
                        Next
                        mv_oDTRolesForGroup.DefaultView.AllowNew = False
                        mv_oDTRolesForGroup.AcceptChanges()
                        With grdRoleForGroup
                            .AutoGenerateColumns = False
                            '.TableStyles(0).MappingName = "Sys_GroupRoles"
                            .DataSource = mv_oDTRolesForGroup.DefaultView
                            '.CaptionText = "Nhóm người dùng " & mv_GroupName
                        End With
                        mv_oDTRolesForGroup.DefaultView.AllowNew = False
                        mv_oDTRolesForGroup.DefaultView.AllowDelete = False
                        mv_oDTRolesForGroup.DefaultView.AllowEdit = False
                    Else
                    End If
                    ms_FindAllChildRolesOfGroup(sv_oNode)
                Next
                'Nếu là LeafNode thì thêm vào DataSet luôn
            Else 'Không cần xử lý vì thủ tục tìm các ParentRole sẽ làm nhánh này
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
    Private Sub ms_FindAllParentRolesOfGroup(ByVal pv_oNode As TreeNode)
        Dim sv_iRoleID As Integer
        Dim sv_iParentID As Integer
        Dim sv_oClsRole As New clsRole
        Dim sv_oParentNode As TreeNode

        Dim sv_sRoleName As String = String.Empty
        Dim sv_sFunctionName As String = String.Empty
        Dim sv_sDLLName As String = String.Empty
        Dim sv_sFormName As String = String.Empty
        Dim sv_oDRRole() As DataRow
        Try
            'Lấy về mã Role con(Là RoleID của Node hiện thời)
            sv_iRoleID = CInt(pv_oNode.Tag.ToString.Substring(pv_oNode.Tag.ToString.IndexOf("#") + 1))
            'Tìm Node cấp trên của Node đó. Nếu chưa phải là node gốc thì tiếp tục tìm cha
            If Not pv_oNode.Parent Is Nothing Then
                If Not pv_oNode.Parent.Text.ToUpper.Equals("ROLES") Then
                    sv_iParentID = CInt(pv_oNode.Parent.Tag.ToString.Substring(pv_oNode.Parent.Tag.ToString.IndexOf("#") + 1))
                    If sv_oClsRole.bAddRoleForGroupUser(mv_GroupID, sv_iRoleID, sv_iParentID, gv_sBranchID) Then
                        'Nếu thêm thành công quyền mới cho KH thì cũng thêm mới vào DataSet để Refesh lại DataGrid
                        sv_oDRRole = gv_dsRole.Tables(0).Select("iRole=" & sv_iRoleID)
                        sv_sRoleName = IIf(IsDBNull(sv_oDRRole(0).Item("sRoleName")), "Không biết", sv_oDRRole(0).Item("sRoleName"))
                        sv_sFunctionName = IIf(IsDBNull(sv_oDRRole(0).Item("sFunctionName")), "Không biết", sv_oDRRole(0).Item("sFunctionName"))
                        sv_sDLLName = IIf(IsDBNull(sv_oDRRole(0).Item("sDLLName")), "Không biết", sv_oDRRole(0).Item("sDLLName"))
                        sv_sFormName = IIf(IsDBNull(sv_oDRRole(0).Item("sFormName")), "Không biết", sv_oDRRole(0).Item("sFormName"))
                        Dim sv_oDR As DataRow
                        sv_oDR = gv_dsGroupRoles.Tables(0).NewRow
                        With sv_oDR
                            .Item("CHON") = "F"
                            .Item("GroupID") = mv_GroupID
                            .Item("iRoleID") = sv_iRoleID
                            .Item("iParentRoleID") = sv_iParentID
                            .Item("sRoleName") = sv_sRoleName
                            .Item("sDLLName") = sv_sDLLName
                            .Item("sFunctionName") = sv_sFunctionName
                            .Item("sFormName") = sv_sFormName
                        End With
                        'Thêm quyền vào DataSet
                        gv_dsGroupRoles.Tables(0).Rows.Add(sv_oDR)
                        gv_dsGroupRoles.Tables(0).AcceptChanges()
                        Dim sv_oDRRolesForGroupUsers As DataRow()
                        sv_oDRRolesForGroupUsers = gv_dsGroupRoles.Tables(0).Select("GroupID=" & mv_GroupID, "iParentRoleID ASC")
                        mv_oDTRolesForGroup = gv_dsGroupRoles.Tables(0).Clone
                        mv_oDTRolesForGroup.Rows.Clear()
                        For Each sv_oDR In sv_oDRRolesForGroupUsers
                            mv_oDTRolesForGroup.ImportRow(sv_oDR)
                        Next
                        mv_oDTRolesForGroup.DefaultView.AllowNew = False
                        mv_oDTRolesForGroup.DefaultView.AllowEdit = False
                        mv_oDTRolesForGroup.DefaultView.AllowDelete = False
                        mv_oDTRolesForGroup.AcceptChanges()
                        With grdRoleForGroup
                            .AutoGenerateColumns = False
                            '.TableStyles(0).MappingName = "Sys_GroupRoles"
                            .DataSource = mv_oDTRolesForGroup.DefaultView
                            '.CaptionText = "Nhóm người dùng " & mv_GroupName
                        End With
                    End If
                    ms_FindAllParentRolesOfGroup(pv_oNode.Parent)
                Else 'Nếu cha là ROLES thì con là Node phân hệ-->Mã cha mặc định=-2
                    sv_iRoleID = CInt(pv_oNode.Tag.ToString.Substring(pv_oNode.Tag.ToString.IndexOf("#") + 1))
                    sv_iParentID = -2
                    If sv_oClsRole.bAddRoleForGroupUser(mv_GroupID, sv_iRoleID, sv_iParentID, gv_sBranchID) Then
                        'Nếu thêm thành công quyền mới cho KH thì cũng thêm mới vào DataSet để Refesh lại DataGrid
                        sv_oDRRole = gv_dsRole.Tables(0).Select("iRole=" & sv_iRoleID)
                        sv_sRoleName = IIf(IsDBNull(sv_oDRRole(0).Item("sRoleName")), "Không biết", sv_oDRRole(0).Item("sRoleName"))
                        sv_sFunctionName = IIf(IsDBNull(sv_oDRRole(0).Item("sFunctionName")), "Không biết", sv_oDRRole(0).Item("sFunctionName"))
                        sv_sDLLName = IIf(IsDBNull(sv_oDRRole(0).Item("sDLLName")), "Không biết", sv_oDRRole(0).Item("sDLLName"))
                        sv_sFormName = IIf(IsDBNull(sv_oDRRole(0).Item("sFormName")), "Không biết", sv_oDRRole(0).Item("sFormName"))
                        Dim sv_oDR As DataRow
                        sv_oDR = gv_dsGroupRoles.Tables(0).NewRow
                        With sv_oDR
                            .Item("CHON") = "F"
                            .Item("GroupID") = mv_GroupID
                            .Item("iRoleID") = sv_iRoleID
                            .Item("iParentRoleID") = sv_iParentID
                            .Item("sRoleName") = sv_sRoleName
                            .Item("sDLLName") = sv_sDLLName
                            .Item("sFunctionName") = sv_sFunctionName
                            .Item("sFormName") = sv_sFormName
                        End With
                        'Thêm quyền vào DataSet
                        gv_dsGroupRoles.Tables(0).Rows.Add(sv_oDR)
                        gv_dsGroupRoles.Tables(0).AcceptChanges()
                        Dim sv_oDRRolesForGroupUsers As DataRow()
                        sv_oDRRolesForGroupUsers = gv_dsGroupRoles.Tables(0).Select("GroupID=" & mv_GroupID, "iParentRoleID ASC")
                        mv_oDTRolesForGroup = gv_dsGroupRoles.Tables(0).Clone
                        mv_oDTRolesForGroup.Rows.Clear()
                        For Each sv_oDR In sv_oDRRolesForGroupUsers
                            mv_oDTRolesForGroup.ImportRow(sv_oDR)
                        Next
                        mv_oDTRolesForGroup.DefaultView.AllowNew = False
                        mv_oDTRolesForGroup.DefaultView.AllowEdit = False
                        mv_oDTRolesForGroup.DefaultView.AllowDelete = False
                        mv_oDTRolesForGroup.AcceptChanges()
                        With grdRoleForGroup
                            .AutoGenerateColumns = False
                            '.TableStyles(0).MappingName = "Sys_RFU"
                            .DataSource = mv_oDTRolesForGroup.DefaultView
                            '.CaptionText = "Nhóm người dùng " & mv_GroupName
                        End With
                    End If
                    Return
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Function bDeleteRoleOfGroupUser(ByVal pv_iRole As Integer) As Boolean
        Dim fv_oUser As New clsRole
        Try
            Return fv_oUser.bDeleteRoleOfGroupUser(mv_GroupID, pv_iRole)
        Catch ex As Exception
            Return False
        End Try
    End Function


    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Sự kiện nhấn nút Remove để xóa một quyền của User. Sẽ xóa tất cả các quyền con của quyền đó
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub cmdRemoveRightOfGroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveRightOfGroup.Click
        Dim sv_oDR As DataRow
        Dim s As String
        Try
            'Kiểm tra xem User có quyền nào hay không?
            If MessageBox.Show("Bạn có muốn xóa quyền " & grdRoleForGroup.Item("sRoleName", grdRoleForGroup.CurrentRow.Index).Value.ToString & " và tất cả các quyền con của nó không?", "Xóa quyền của nhóm người dùng " & mv_GroupName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Me.Cursor = Cursors.WaitCursor
                's = grdRoleForGroup.CaptionText
                'grdRoleForGroup.CaptionText = "Đang xóa quyền của nhóm người dùng..."
                grdRoleForGroup.Refresh()
                If grdRoleForGroup.Visible = True AndAlso grdRoleForGroup.RowCount > 0 Then
                    'Nếu có thì lấy về mã quyền cần xóa
                    Dim sRoleName As String = String.Empty
                    Dim sv_iRole As Integer = CInt(grdRoleForGroup.Item("iRoleID", grdRoleForGroup.CurrentRow.Index).Value)
                    sRoleName = grdRoleForGroup.Item("sRoleName", grdRoleForGroup.CurrentRow.Index).Value.ToString
                    'Gọi hàm xóa quyền!
                    'gv_objTrans = VNS.Libs.globalVariables.SqlConn.BeginTransaction
                    If ms_DeleteAllChildRolesOfGroup(sv_iRole) Then
                        'Lọc các quyền còn lại để đưa vào Grid
                        Dim sv_oDRRolesForGroupUsers As DataRow()
                        sv_oDRRolesForGroupUsers = gv_dsGroupRoles.Tables(0).Select("GroupID=" & mv_GroupID, "iParentRoleID ASC")
                        mv_oDTRolesForGroup = gv_dsGroupRoles.Tables(0).Clone
                        mv_oDTRolesForGroup.Rows.Clear()
                        For Each sv_oDR In sv_oDRRolesForGroupUsers
                            mv_oDTRolesForGroup.ImportRow(sv_oDR)
                        Next
                        mv_oDTRolesForGroup.DefaultView.AllowNew = False
                        mv_oDTRolesForGroup.DefaultView.AllowEdit = False
                        mv_oDTRolesForGroup.DefaultView.AllowDelete = False
                        mv_oDTRolesForGroup.AcceptChanges()
                        With grdRoleForGroup
                            .AutoGenerateColumns = False
                            '.TableStyles(0).MappingName = "Sys_GroupRoles"
                            .DataSource = mv_oDTRolesForGroup.DefaultView
                            '.CaptionText = "Nhóm người dùng " & mv_GroupName & " - Tổng số quyền được cấp: " & mv_oDTRolesForGroup.Rows.Count
                        End With
                        mv_oDTRolesForGroup.DefaultView.Sort = "iParentRoleID,iRoleID,iOrder"
                        MessageBox.Show("Đã xóa quyền " & sRoleName & " và tất cả các quyền con của nó", "Xóa quyền của người dùng " & mv_GroupName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'gv_objTrans.Commit()
                    Else
                        MessageBox.Show("Lỗi xóa quyền " & sRoleName & " của nhóm người dùng " & mv_GroupName, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Cursor = Cursors.Default
                    End If
                End If
                Me.Cursor = Cursors.Default

            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Function ms_DeleteAllChildRolesOfGroup(ByVal pv_iRole As Integer) As Boolean
        Dim sv_iParentID As Integer
        Dim sv_oClsRole As New clsRole
        Dim sv_oDR() As DataRow
        Dim sv_iCount As Integer = 0
        Try
            'Lấy về quyền cần xóa và các quyền con của quyền cần xóa
            sv_oDR = gv_dsGroupRoles.Tables(0).Select("GroupID=" & mv_GroupID & " AND ((iRoleID=" & pv_iRole & ") OR (iParentRoleID=" & pv_iRole & "))")
            For sv_iCount = 0 To sv_oDR.Length - 1
                Dim sv_iRoleID As Integer
                sv_iRoleID = sv_oDR(sv_iCount).Item("iRoleID")
                'Xóa quyền ra khỏi CSDL
                bDeleteRoleOfGroupUser(sv_iRoleID)
                'Xóa quyền ra khỏi Grid
                gv_dsGroupRoles.Tables(0).Rows.Remove(sv_oDR(sv_iCount))
                'Gọi đệ quy để xóa các ChildRole của Role hiện thời
                'Kiểm tra để đảm bảo rằng thủ tục đệ quy sẽ không được gọi 2 lần đối với Role ban đầu truyền vào
                If sv_iRoleID <> pv_iRole Then
                    ms_DeleteAllChildRolesOfGroup(sv_iRoleID)
                End If
            Next
            gv_dsGroupRoles.Tables(0).AcceptChanges()
            Return True
        Catch ex As Exception
            'gv_objTrans.Rollback()
            Return False
        End Try
    End Function
    Private Sub cmdGetRightForGroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetRightForGroup.Click
        Dim sv_oCtr As Control
        Dim sv_iRoleID As Integer
        Dim sv_oDR As DataRow
        Dim sv_oDRRolesForGroupUserSet As DataRow()
        Dim sv_oDRRolesForGroupUserGet As DataRow()
        Dim sv_GroupGet As Integer
        Dim sv_intSecurityGet As Integer
        Dim sv_intSecuritySet As Integer
        Dim sv_oUser As New clsUser
        Dim sv_oClsRole As New clsRole
        Dim s As String
        Try
            Me.Cursor = Cursors.WaitCursor
            's = grdRoleForGroup.CaptionText
            'grdRoleForGroup.CaptionText = "Đang nhận quyền cho nhóm người dùng..."
            grdRoleForGroup.Refresh()
            Select Case mv_intGroupTabIndex
                Case 0 'Phân quyền theo Role
                    tvwRolesForGroup_DoubleClick(sender, e)
                Case 1 'Phân quyền theo nhóm người dùng

                    Try
                        mv_oDTRolesForGroup.Rows.Clear()
                        'Lấy về tên UserName cần lấy dữ liệu
                        sv_GroupGet = CInt(grdGroup.Item("GroupID", grdGroup.CurrentRow.Index).Value)
                        'Lấy về mức Security của User cần Get
                        sv_intSecurityGet = sv_oUser.iGetSecurityLevelOfGroup(sv_GroupGet)
                        sv_intSecuritySet = sv_oUser.iGetSecurityLevelOfGroup(mv_GroupID)
                        sv_oUser.UpdateSecurityOfGroup(mv_GroupID, sv_intSecurityGet)
                        'Lọc về các quyền của GroupUser
                        Try
                            'Lấy về toàn bộ quyền của người cho
                            sv_oDRRolesForGroupUserGet = gv_dsGroupRoles.Tables(0).Select("GroupID=" & sv_GroupGet, "iOrder,iParentRoleID,iRoleID")
                            If sv_oDRRolesForGroupUserGet.GetLength(0) > 0 Then
                                'Lấy về toàn bộ quyền của người nhận
                                sv_oDRRolesForGroupUserSet = gv_dsGroupRoles.Tables(0).Select("GroupID=" & mv_GroupID, "iOrder,iParentRoleID,iRoleID")
                                For Each sv_oDR In sv_oDRRolesForGroupUserSet
                                    mv_oDTRolesForGroup.ImportRow(sv_oDR)
                                Next
                                'Gán thêm quyền của người cho cho nhóm người nhận
                                'gv_objTrans = VNS.Libs.globalVariables.SqlConn.BeginTransaction
                                For Each sv_oDR In sv_oDRRolesForGroupUserGet
                                    If sv_oClsRole.bAddRoleForGroupUser(mv_GroupID, CInt(sv_oDR("iRoleID")), CInt(sv_oDR("iParentRoleID")), gv_sBranchID) Then
                                        'Nếu thêm thành công quyền mới cho KH thì cũng thêm mới vào DataSet để Refesh lại DataGrid
                                        Dim sv_oDR1 As DataRow
                                        sv_oDR1 = gv_dsGroupRoles.Tables(0).NewRow
                                        With sv_oDR1
                                            .Item("GroupID") = mv_GroupID
                                            .Item("iRoleID") = CInt(sv_oDR("iRoleID"))
                                            .Item("iParentRoleID") = sv_oDR("iParentRoleID")
                                            .Item("sRoleName") = sv_oDR("sRoleName")
                                            .Item("sDLLName") = sv_oDR("sDLLName")
                                            .Item("sFunctionName") = sv_oDR("sFunctionName")
                                            .Item("sFormName") = sv_oDR("sFormName")
                                        End With
                                        'Thêm quyền vào DataSet
                                        gv_dsGroupRoles.Tables(0).Rows.Add(sv_oDR1)
                                        gv_dsGroupRoles.Tables(0).AcceptChanges()
                                        mv_oDTRolesForGroup.ImportRow(sv_oDR)
                                    End If
                                Next
                                'gv_objTrans.Commit()
                            Else
                                sv_oDRRolesForGroupUserSet = gv_dsGroupRoles.Tables(0).Select("GroupID=" & mv_GroupID, "iOrder,iParentRoleID,iRoleID")
                                For Each sv_oDR In sv_oDRRolesForGroupUserSet
                                    mv_oDTRolesForGroup.ImportRow(sv_oDR)
                                Next
                            End If
                            With grdRoleForGroup
                                .AutoGenerateColumns = False
                                '.TableStyles(0).MappingName = "Sys_GroupRoles"
                                .DataSource = mv_oDTRolesForGroup.DefaultView
                                '.CaptionText = "Nhóm người dùng " & mv_GroupName & " - Tổng số quyền được cấp: " & mv_oDTRolesForGroup.Rows.Count
                            End With
                            mv_oDTRolesForGroup.DefaultView.Sort = "iParentRoleID,iRoleID,iOrder"
                            mv_oDTRolesForGroup.DefaultView.AllowNew = False
                            mv_oDTRolesForGroup.DefaultView.AllowDelete = False
                            mv_oDTRolesForGroup.DefaultView.AllowEdit = False
                        Catch ex As Exception
                        End Try
                        'Hiển thị SecurityLevel của User
                        chkIsAdmin.Checked = sv_oUser.iGetSecurityLevelOfGroup(mv_GroupID)
                    Catch ex As Exception
                    End Try
            End Select
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            'gv_objTrans.Rollback()
            Me.Cursor = Cursors.Default
        End Try

    End Sub


#End Region
#Region "Các hàm phục vụ cho việc gán quyền cho một User"
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Duyệt đệ quy để gán quyền cho một User khi chọn quyền trên cây. Thủ tục này lấy về
    '                        Tất cả các ChildRole của Role hiện thời
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub ms_FindAllChildRoles(ByVal pv_oNode As TreeNode)
        Dim sv_iRoleID As Integer
        Dim sv_iParentID As Integer
        Dim sv_oClsRole As New clsRole
        Dim sv_sRoleName As String = String.Empty
        Dim sv_sFunctionName As String = String.Empty
        Dim sv_sDLLName As String = String.Empty
        Dim sv_sFormName As String = String.Empty
        Dim sv_oDRRole() As DataRow
        Try
            If IsNothing(pv_oNode) Then
                MessageBox.Show("Bạn chưa chọn Role để gán. Mời bạn chọn Role trên cây Role phía trên")
                Return
            End If

            'Lấy về mã Role cha(Là RoleID của Node hiện thời)
            sv_iParentID = CInt(pv_oNode.Tag.ToString.Substring(pv_oNode.Tag.ToString.IndexOf("#") + 1))
            'Kiểm tra xem có là LeafRole không
            If pv_oNode.GetNodeCount(True) > 0 Then
                Dim sv_oNode As TreeNode
                For Each sv_oNode In pv_oNode.Nodes
                    sv_iRoleID = CInt(sv_oNode.Tag.ToString.Substring(sv_oNode.Tag.ToString.IndexOf("#") + 1))
                    If sv_oClsRole.bAddRoleForUser(mv_sUID, sv_iRoleID, sv_iParentID, gv_sBranchID) Then
                        'Nếu thêm thành công quyền mới cho KH thì cũng thêm mới vào DataSet để Refesh lại DataGrid
                        sv_oDRRole = gv_dsRole.Tables(0).Select("iRole=" & sv_iRoleID)
                        sv_sRoleName = IIf(IsDBNull(sv_oDRRole(0).Item("sRoleName")), "Không biết", sv_oDRRole(0).Item("sRoleName"))
                        sv_sFunctionName = IIf(IsDBNull(sv_oDRRole(0).Item("sFunctionName")), "Không biết", sv_oDRRole(0).Item("sFunctionName"))
                        sv_sDLLName = IIf(IsDBNull(sv_oDRRole(0).Item("sDLLName")), "Không biết", sv_oDRRole(0).Item("sDLLName"))
                        sv_sFormName = IIf(IsDBNull(sv_oDRRole(0).Item("sFormName")), "Không biết", sv_oDRRole(0).Item("sFormName"))
                        Dim sv_oDR As DataRow
                        sv_oDR = gv_dsRolesForUsers.Tables(0).NewRow
                        With sv_oDR
                            .Item("CHON") = "F"
                            .Item("sUID") = mv_sUID
                            .Item("iRoleID") = sv_iRoleID
                            .Item("iParentRoleID") = sv_iParentID
                            .Item("sRoleName") = sv_sRoleName
                            .Item("sDLLName") = sv_sDLLName
                            .Item("sFunctionName") = sv_sFunctionName
                            .Item("sFormName") = sv_sFormName
                        End With
                        'Thêm quyền vào DataSet
                        gv_dsRolesForUsers.Tables(0).Rows.Add(sv_oDR)
                        gv_dsRolesForUsers.Tables(0).AcceptChanges()
                        '
                        Dim sv_oDRRolesForUsers As DataRow()
                        sv_oDRRolesForUsers = gv_dsRolesForUsers.Tables(0).Select("sUID='" & mv_sUID & "'", "iParentRoleID ASC")
                        mv_oDTRolesForUser = gv_dsRolesForUsers.Tables(0).Clone
                        mv_oDTRolesForUser.Rows.Clear()
                        For Each sv_oDR In sv_oDRRolesForUsers
                            mv_oDTRolesForUser.ImportRow(sv_oDR)
                        Next
                        mv_oDTRolesForUser.DefaultView.AllowNew = False
                        mv_oDTRolesForUser.AcceptChanges()
                        With grdRolesForUsers
                            .TableStyles(0).MappingName = "Sys_RFU"
                            .DataSource = mv_oDTRolesForUser.DefaultView
                            .CaptionText = "Người dùng " & mv_sUID
                        End With
                        mv_oDTRolesForUser.DefaultView.AllowNew = False
                        mv_oDTRolesForUser.DefaultView.AllowDelete = False
                        mv_oDTRolesForUser.DefaultView.AllowEdit = False
                    Else
                    End If
                    ms_FindAllChildRoles(sv_oNode)
                Next
                'Nếu là LeafNode thì thêm vào DataSet luôn
            Else 'Không cần xử lý vì thủ tục tìm các ParentRole sẽ làm nhánh này
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
    Private Sub ms_FindAllParentRoles(ByVal pv_oNode As TreeNode)
        Dim sv_iRoleID As Integer
        Dim sv_iParentID As Integer
        Dim sv_oClsRole As New clsRole
        Dim sv_oParentNode As TreeNode

        Dim sv_sRoleName As String = String.Empty
        Dim sv_sFunctionName As String = String.Empty
        Dim sv_sDLLName As String = String.Empty
        Dim sv_sFormName As String = String.Empty
        Dim sv_oDRRole() As DataRow
        Try
            'Lấy về mã Role con(Là RoleID của Node hiện thời)
            sv_iRoleID = CInt(pv_oNode.Tag.ToString.Substring(pv_oNode.Tag.ToString.IndexOf("#") + 1))
            'Tìm Node cấp trên của Node đó. Nếu chưa phải là node gốc thì tiếp tục tìm cha
            If Not pv_oNode.Parent Is Nothing Then
                If Not pv_oNode.Parent.Text.ToUpper.Equals("ROLES") Then
                    sv_iParentID = CInt(pv_oNode.Parent.Tag.ToString.Substring(pv_oNode.Parent.Tag.ToString.IndexOf("#") + 1))
                    If sv_oClsRole.bAddRoleForUser(mv_sUID, sv_iRoleID, sv_iParentID, gv_sBranchID) Then
                        'Nếu thêm thành công quyền mới cho KH thì cũng thêm mới vào DataSet để Refesh lại DataGrid
                        sv_oDRRole = gv_dsRole.Tables(0).Select("iRole=" & sv_iRoleID)
                        sv_sRoleName = IIf(IsDBNull(sv_oDRRole(0).Item("sRoleName")), "Không biết", sv_oDRRole(0).Item("sRoleName"))
                        sv_sFunctionName = IIf(IsDBNull(sv_oDRRole(0).Item("sFunctionName")), "Không biết", sv_oDRRole(0).Item("sFunctionName"))
                        sv_sDLLName = IIf(IsDBNull(sv_oDRRole(0).Item("sDLLName")), "Không biết", sv_oDRRole(0).Item("sDLLName"))
                        sv_sFormName = IIf(IsDBNull(sv_oDRRole(0).Item("sFormName")), "Không biết", sv_oDRRole(0).Item("sFormName"))
                        Dim sv_oDR As DataRow
                        sv_oDR = gv_dsRolesForUsers.Tables(0).NewRow
                        With sv_oDR
                            .Item("CHON") = "F"
                            .Item("sUID") = mv_sUID
                            .Item("iRoleID") = sv_iRoleID
                            .Item("iParentRoleID") = sv_iParentID
                            .Item("sRoleName") = sv_sRoleName
                            .Item("sDLLName") = sv_sDLLName
                            .Item("sFunctionName") = sv_sFunctionName
                            .Item("sFormName") = sv_sFormName
                        End With
                        'Thêm quyền vào DataSet
                        gv_dsRolesForUsers.Tables(0).Rows.Add(sv_oDR)
                        gv_dsRolesForUsers.Tables(0).AcceptChanges()
                        Dim sv_oDRRolesForUsers As DataRow()
                        sv_oDRRolesForUsers = gv_dsRolesForUsers.Tables(0).Select("sUID='" & mv_sUID & "'", "iParentRoleID ASC")
                        mv_oDTRolesForUser = gv_dsRolesForUsers.Tables(0).Clone
                        mv_oDTRolesForUser.Rows.Clear()
                        For Each sv_oDR In sv_oDRRolesForUsers
                            mv_oDTRolesForUser.ImportRow(sv_oDR)
                        Next
                        mv_oDTRolesForUser.DefaultView.AllowNew = False
                        mv_oDTRolesForUser.DefaultView.AllowEdit = False
                        mv_oDTRolesForUser.DefaultView.AllowDelete = False
                        mv_oDTRolesForUser.AcceptChanges()
                        With grdRolesForUsers
                            .TableStyles(0).MappingName = "Sys_RFU"
                            .DataSource = mv_oDTRolesForUser.DefaultView
                            .CaptionText = "Người dùng " & mv_sUID
                        End With
                    End If
                    ms_FindAllParentRoles(pv_oNode.Parent)
                Else 'Nếu cha là ROLES thì con là Node phân hệ-->Mã cha mặc định=-2
                    sv_iRoleID = CInt(pv_oNode.Tag.ToString.Substring(pv_oNode.Tag.ToString.IndexOf("#") + 1))
                    sv_iParentID = -2
                    If sv_oClsRole.bAddRoleForUser(mv_sUID, sv_iRoleID, sv_iParentID, gv_sBranchID) Then
                        'Nếu thêm thành công quyền mới cho KH thì cũng thêm mới vào DataSet để Refesh lại DataGrid
                        sv_oDRRole = gv_dsRole.Tables(0).Select("iRole=" & sv_iRoleID)
                        sv_sRoleName = IIf(IsDBNull(sv_oDRRole(0).Item("sRoleName")), "Không biết", sv_oDRRole(0).Item("sRoleName"))
                        sv_sFunctionName = IIf(IsDBNull(sv_oDRRole(0).Item("sFunctionName")), "Không biết", sv_oDRRole(0).Item("sFunctionName"))
                        sv_sDLLName = IIf(IsDBNull(sv_oDRRole(0).Item("sDLLName")), "Không biết", sv_oDRRole(0).Item("sDLLName"))
                        sv_sFormName = IIf(IsDBNull(sv_oDRRole(0).Item("sFormName")), "Không biết", sv_oDRRole(0).Item("sFormName"))
                        Dim sv_oDR As DataRow
                        sv_oDR = gv_dsRolesForUsers.Tables(0).NewRow
                        With sv_oDR
                            .Item("CHON") = "F"
                            .Item("sUID") = mv_sUID
                            .Item("iRoleID") = sv_iRoleID
                            .Item("iParentRoleID") = sv_iParentID
                            .Item("sRoleName") = sv_sRoleName
                            .Item("sDLLName") = sv_sDLLName
                            .Item("sFunctionName") = sv_sFunctionName
                            .Item("sFormName") = sv_sFormName
                        End With
                        'Thêm quyền vào DataSet
                        gv_dsRolesForUsers.Tables(0).Rows.Add(sv_oDR)
                        gv_dsRolesForUsers.Tables(0).AcceptChanges()
                        Dim sv_oDRRolesForUsers As DataRow()
                        sv_oDRRolesForUsers = gv_dsRolesForUsers.Tables(0).Select("sUID='" & mv_sUID & "'", "iParentRoleID ASC")
                        mv_oDTRolesForUser = gv_dsRolesForUsers.Tables(0).Clone
                        mv_oDTRolesForUser.Rows.Clear()
                        For Each sv_oDR In sv_oDRRolesForUsers
                            mv_oDTRolesForUser.ImportRow(sv_oDR)
                        Next
                        mv_oDTRolesForUser.DefaultView.AllowNew = False
                        mv_oDTRolesForUser.DefaultView.AllowEdit = False
                        mv_oDTRolesForUser.DefaultView.AllowDelete = False
                        mv_oDTRolesForUser.AcceptChanges()
                        With grdRolesForUsers
                            .TableStyles(0).MappingName = "Sys_RFU"
                            .DataSource = mv_oDTRolesForUser.DefaultView
                            .CaptionText = "Người dùng " & mv_sUID
                        End With
                    End If
                    Return
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Function bDeleteRoleOfUser(ByVal pv_iRole As Integer) As Boolean
        Dim fv_oUser As New clsRole
        Try
            Return fv_oUser.bDeleteRoleOfUser(mv_sUID, pv_iRole)
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function _bDeleteBranch(ByVal pv_iRole As Integer) As Boolean
        Dim fv_oUser As New clsRole
        Try
            Return _bDeleteRoleByID(pv_iRole)
        Catch ex As Exception
            Return False
        End Try
    End Function


    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Xóa Role của một User trong CSDL
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function _bDeleteRoleByID(ByVal pv_iRoleID As Integer) As Boolean
        Dim sv_oCmd As SqlCommand
        Dim sv_sSql As String
        Try
            sv_sSql = "DELETE from Sys_ROLES WHERE iRole=" & pv_iRoleID & " AND FP_sBranchID=N'" & gv_sBranchID & "'"
            sv_oCmd = New SqlCommand(sv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.Transaction = gv_objTrans
            sv_oCmd.ExecuteNonQuery()
            sv_oCmd = Nothing
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Sự kiện nhấn nút Remove để xóa một quyền của User. Sẽ xóa tất cả các quyền con của quyền đó
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub picMoveRole_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoveRight.Click
        Dim sv_oDR As DataRow
        Dim s As String
        Try
            'Kiểm tra xem User có quyền nào hay không?
            If MessageBox.Show("Bạn có muốn xóa quyền " & grdRolesForUsers.Item(grdRolesForUsers.CurrentRowIndex, 1).ToString & " và tất cả các quyền con của nó không?", "Xóa quyền của người dùng " & mv_sUID, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Me.Cursor = Cursors.WaitCursor
                s = grdRolesForUsers.CaptionText
                grdRolesForUsers.CaptionText = "Đang xóa quyền của người dùng..."
                grdRolesForUsers.Refresh()
                If grdRolesForUsers.Visible = True AndAlso grdRolesForUsers.VisibleRowCount > 0 Then
                    'Nếu có thì lấy về mã quyền cần xóa
                    Dim sRoleName As String = String.Empty
                    Dim sv_iRole As Integer = grdRolesForUsers.Item(grdRolesForUsers.CurrentRowIndex, 1)
                    sRoleName = grdRolesForUsers.Item(grdRolesForUsers.CurrentRowIndex, 2).ToString
                    'Gọi hàm xóa quyền!
                    'gv_objTrans = VNS.Libs.globalVariables.SqlConn.BeginTransaction
                    If ms_DeleteAllChildRoles(sv_iRole) Then
                        'Lọc các quyền còn lại để đưa vào Grid
                        Dim sv_oDRRolesForUsers As DataRow()
                        sv_oDRRolesForUsers = gv_dsRolesForUsers.Tables(0).Select("sUID='" & mv_sUID & "'", "iParentRoleID ASC")
                        mv_oDTRolesForUser = gv_dsRolesForUsers.Tables(0).Clone
                        mv_oDTRolesForUser.Rows.Clear()
                        For Each sv_oDR In sv_oDRRolesForUsers
                            mv_oDTRolesForUser.ImportRow(sv_oDR)
                        Next
                        mv_oDTRolesForUser.DefaultView.AllowNew = False
                        mv_oDTRolesForUser.DefaultView.AllowEdit = False
                        mv_oDTRolesForUser.DefaultView.AllowDelete = False
                        mv_oDTRolesForUser.AcceptChanges()
                        With grdRolesForUsers
                            .TableStyles(0).MappingName = "Sys_RFU"
                            .DataSource = mv_oDTRolesForUser.DefaultView
                            .CaptionText = "Người dùng " & mv_sUID & " - Tổng số quyền được cấp: " & mv_oDTRolesForUser.Rows.Count
                        End With
                        mv_oDTRolesForUser.DefaultView.Sort = "iParentRoleID,iRoleID,iOrder"
                        MessageBox.Show("Đã xóa quyền " & sRoleName & " và tất cả các quyền con của nó", "Xóa quyền của người dùng " & mv_sUID, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'gv_objTrans.Commit()
                    Else
                        MessageBox.Show("Lỗi xóa quyền " & sRoleName & " của người dùng " & mv_sUID, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Cursor = Cursors.Default
                    End If
                End If
                Me.Cursor = Cursors.Default

            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Function ms_DeleteAllChildRoles(ByVal pv_iRole As Integer) As Boolean
        Dim sv_iParentID As Integer
        Dim sv_oClsRole As New clsRole
        Dim sv_oDR() As DataRow
        Dim sv_iCount As Integer = 0
        Try

            'Lấy về quyền cần xóa và các quyền con của quyền cần xóa
            sv_oDR = gv_dsRolesForUsers.Tables(0).Select("sUID='" & mv_sUID & "' AND ((iRoleID=" & pv_iRole & ") OR (iParentRoleID=" & pv_iRole & "))")
            For sv_iCount = 0 To sv_oDR.Length - 1
                Dim sv_iRoleID As Integer
                sv_iRoleID = sv_oDR(sv_iCount).Item("iRoleID")
                'Xóa quyền ra khỏi CSDL
                bDeleteRoleOfUser(sv_iRoleID)
                'Xóa quyền ra khỏi Grid
                gv_dsRolesForUsers.Tables(0).Rows.Remove(sv_oDR(sv_iCount))
                'Gọi đệ quy để xóa các ChildRole của Role hiện thời
                'Kiểm tra để đảm bảo rằng thủ tục đệ quy sẽ không được gọi 2 lần đối với Role ban đầu truyền vào
                If sv_iRoleID <> pv_iRole Then
                    ms_DeleteAllChildRoles(sv_iRoleID)
                End If
            Next
            gv_dsRolesForUsers.Tables(0).AcceptChanges()
            Return True
        Catch ex As Exception
            'gv_objTrans.Rollback()
            Return False
        End Try
    End Function
    Private Sub picGetRole_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetRight.Click
        Dim sv_oCtr As Control
        Dim sv_iRoleID As Integer
        Dim sv_oDR As DataRow
        Dim sv_oDRRolesForUserSet As DataRow()
        Dim sv_oDRRolesForUserGet As DataRow()
        Dim sv_sUserGet As String
        Dim sv_intSecurityGet As Integer
        Dim sv_intSecuritySet As Integer
        Dim sv_oUser As New clsUser
        Dim sv_oClsRole As New clsRole
        Dim s As String
        Try
            Me.Cursor = Cursors.WaitCursor
            s = grdRolesForUsers.CaptionText
            grdRolesForUsers.CaptionText = "Đang nhận quyền cho người dùng..."
            grdRolesForUsers.Refresh()
            Select Case mv_intTabIndex
                Case 0 'Phân quyền theo Role
                    tvwRoleForUser_DoubleClick(sender, e)
                Case 1 'Phân quyền theo người dùng

                    Try
                        mv_oDTRolesForUser.Rows.Clear()
                        'Lấy về tên UserName cần lấy dữ liệu
                        sv_sUserGet = grdUser.Item(grdUser.CurrentRowIndex, 1)
                        'Lấy về mức Security của User cần Get
                        sv_intSecurityGet = sv_oUser.iGetSecurityLevel(sv_sUserGet)
                        sv_intSecuritySet = sv_oUser.iGetSecurityLevel(mv_sUID)
                        sv_oUser.UpdateSecurity(mv_sUID, sv_intSecurityGet)
                        'Lọc về các quyền của User
                        Try
                            'Lấy về toàn bộ quyền của người cho
                            sv_oDRRolesForUserGet = gv_dsRolesForUsers.Tables(0).Select("sUID='" & sv_sUserGet & "'", "iOrder,iParentRoleID,iRoleID")
                            If sv_oDRRolesForUserGet.GetLength(0) > 0 Then
                                'Lấy về toàn bộ quyền của người nhận
                                sv_oDRRolesForUserSet = gv_dsRolesForUsers.Tables(0).Select("sUID='" & mv_sUID & "'", "iOrder,iParentRoleID,iRoleID")
                                For Each sv_oDR In sv_oDRRolesForUserSet
                                    mv_oDTRolesForUser.ImportRow(sv_oDR)
                                Next
                                'Gán thêm quyền của người cho cho người nhận
                                'gv_objTrans = VNS.Libs.globalVariables.SqlConn.BeginTransaction
                                For Each sv_oDR In sv_oDRRolesForUserGet
                                    If sv_oClsRole.bAddRoleForUser(mv_sUID, CInt(sv_oDR("iRoleID")), CInt(sv_oDR("iParentRoleID")), gv_sBranchID) Then
                                        'Nếu thêm thành công quyền mới cho KH thì cũng thêm mới vào DataSet để Refesh lại DataGrid
                                        Dim sv_oDR1 As DataRow
                                        sv_oDR1 = gv_dsRolesForUsers.Tables(0).NewRow
                                        With sv_oDR1
                                            .Item("sUID") = mv_sUID
                                            .Item("iRoleID") = CInt(sv_oDR("iRoleID"))
                                            .Item("iParentRoleID") = sv_oDR("iParentRoleID")
                                            .Item("sRoleName") = sv_oDR("sRoleName")
                                            .Item("sDLLName") = sv_oDR("sDLLName")
                                            .Item("sFunctionName") = sv_oDR("sFunctionName")
                                            .Item("sFormName") = sv_oDR("sFormName")
                                        End With
                                        'Thêm quyền vào DataSet
                                        gv_dsRolesForUsers.Tables(0).Rows.Add(sv_oDR1)
                                        gv_dsRolesForUsers.Tables(0).AcceptChanges()
                                        mv_oDTRolesForUser.ImportRow(sv_oDR)
                                    End If
                                Next
                                'gv_objTrans.Commit()
                            Else
                                sv_oDRRolesForUserSet = gv_dsRolesForUsers.Tables(0).Select("sUID='" & mv_sUID & "'", "iOrder,iParentRoleID,iRoleID")
                                For Each sv_oDR In sv_oDRRolesForUserSet
                                    mv_oDTRolesForUser.ImportRow(sv_oDR)
                                Next
                            End If
                            With grdRolesForUsers
                                .TableStyles(0).MappingName = "Sys_RFU"
                                .DataSource = mv_oDTRolesForUser.DefaultView
                                .CaptionText = "Người dùng " & mv_sUID & " - Tổng số quyền được cấp: " & mv_oDTRolesForUser.Rows.Count
                            End With
                            mv_oDTRolesForUser.DefaultView.Sort = "iParentRoleID,iRoleID,iOrder"
                            mv_oDTRolesForUser.DefaultView.AllowNew = False
                            mv_oDTRolesForUser.DefaultView.AllowDelete = False
                            mv_oDTRolesForUser.DefaultView.AllowEdit = False
                        Catch ex As Exception
                        End Try
                        'Hiển thị SecurityLevel của User
                        chkAllRole.Checked = sv_oUser.iGetSecurityLevel(mv_sUID)
                    Catch ex As Exception
                    End Try
            End Select
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            'gv_objTrans.Rollback()
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Private Sub chkIsAdmin_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIsAdmin.CheckedChanged
        Try
            Dim sv_oUser As New clsUser
            sv_oUser.UpdateSecurityForGroup(mv_GroupID, IIf(chkIsAdmin.Checked, 1, 0))
            For Each dr As DataRow In gv_dsGroupUser.Tables(0).Rows
                If CInt(dr("GroupID")) = mv_GroupID Then
                    dr("IsAdmin") = IIf(chkIsAdmin.Checked, 1, 0)
                    Exit For
                End If
            Next
            gv_dsGroupUser.Tables(0).AcceptChanges()
            cmdGetRightForGroup.Enabled = Not chkIsAdmin.Checked
            cmdRemoveRightOfGroup.Enabled = cmdGetRightForGroup.Enabled
        Catch ex As Exception

        End Try
    End Sub
    Private Sub chkAllRole_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllRole.CheckedChanged
        Try
            Dim sv_oUser As New clsUser
            sv_oUser.UpdateSecurity(mv_sUID, IIf(chkAllRole.Checked, 1, 0))
            For Each dr As DataRow In gv_dsUser.Tables(0).Rows
                If dr("PK_sUID").ToString.ToUpper = mv_sUID.ToUpper Then
                    dr("iSecurityLevel") = IIf(chkAllRole.Checked, 1, 0)
                    Exit For
                End If
            Next
            gv_dsUser.Tables(0).AcceptChanges()
            cmdGetRight.Enabled = Not chkAllRole.Checked
            cmdMoveRight.Enabled = cmdGetRight.Enabled
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Các hàm phục vụ cho việc cập nhật lại chức năng cho một Role"
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Sự kiện nhấn nút để xóa chức năng cho một Role
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :07/03/2006
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub cmdDelFunctionForRole_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelFunctionForRole.Click
        Dim sv_iRoleID As Integer
        Dim sv_iFunctionID As Integer
        Dim sv_oRole As New clsRole
        Dim sv_oDR As DataRow
        sv_iRoleID = CInt(tvwAdminSystem.SelectedNode.Tag.ToString.Substring(tvwAdminSystem.SelectedNode.Tag.ToString.IndexOf("#") + 1))
        sv_iFunctionID = -1 'Không được gán chức năng mặc định=-1
        Try
            If sv_oRole.bUpdateFunctionForRole(sv_iRoleID, -1) Then
                For Each sv_oDR In gv_dsRole.Tables(0).Rows
                    If sv_oDR.Item("iRole") = sv_iRoleID Then
                        sv_oDR.Item("FK_iFunctionID") = -1
                        sv_oDR.Item("sFunctionName") = "Chưa gán"
                        sv_oDR.Item("sDLLName") = "Chưa gán"
                        sv_oDR.Item("sFormName") = "Chưa gán"
                        lblRoleName.Text = IIF_VN(sv_oDR.Item("sRoleName"))
                        lblFunctionName.Text = "Chưa gán"
                        lblDLLName.Text = "Chưa gán"
                        lblFormName.Text = "Chưa gán"
                        Exit For
                    End If
                Next
                'Xóa FunctionID cho Role đó
                tvwAdminSystem.SelectedNode.Tag = "LEAFROLES|" & "-1" & "#" & sv_iRoleID
                txtFunctionID.Text = "-1"
                'Thiết lập lại ContextMenu cho TreeView
                tvwAdminSystem.ContextMenu = mv_oContextMenuForLeafRoleWithoutFunction
                gv_dsRole.Tables(0).AcceptChanges()
                cmdDelFunctionForRole.Enabled = False
            Else
                MessageBox.Show("Lỗi khi Xóa chức năng khỏi Role", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception

        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Sự kiện nhấn nút để cập nhật chức năng cho một Role
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub cmdGetFunctionForRole_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetFunctionForRole.Click
        Dim sv_iRoleID As Integer
        Dim sv_iFunctionID As Integer
        Dim sv_oRole As New clsRole
        Dim sv_oDR As DataRow
        sv_iRoleID = CInt(tvwAdminSystem.SelectedNode.Tag.ToString.Substring(tvwAdminSystem.SelectedNode.Tag.ToString.IndexOf("#") + 1))
        sv_iFunctionID = CInt(grdFunction.Item(grdFunction.CurrentRowIndex, 1))
        Try
            If sv_oRole.bUpdateFunctionForRole(sv_iRoleID, sv_iFunctionID) Then
                For Each sv_oDR In gv_dsRole.Tables(0).Rows
                    If sv_oDR.Item("iRole") = sv_iRoleID Then
                        sv_oDR.Item("FK_iFunctionID") = sv_iFunctionID
                        sv_oDR.Item("sFunctionName") = grdFunction.Item(grdFunction.CurrentRowIndex, 2).ToString
                        sv_oDR.Item("sDLLName") = grdFunction.Item(grdFunction.CurrentRowIndex, 3).ToString
                        sv_oDR.Item("sFormName") = grdFunction.Item(grdFunction.CurrentRowIndex, 4).ToString
                        lblRoleName.Text = IIF_VN(sv_oDR.Item("sRoleName"))
                        lblFunctionName.Text = IIF_VN(sv_oDR.Item("sFunctionName"))
                        lblDLLName.Text = IIF_VN(sv_oDR.Item("sDLLName"))
                        lblFormName.Text = IIF_VN(sv_oDR.Item("sFormName"))
                        Exit For
                    End If
                Next
                'Gán lại FunctionID cho Role đó
                tvwAdminSystem.SelectedNode.Tag = "LEAFROLES|" & sv_iFunctionID & "#" & sv_iRoleID
                txtFunctionID.Text = sv_iFunctionID.ToString
                'Thiết lập lại ContextMenu cho TreeView
                tvwAdminSystem.ContextMenu = mv_oContextMenuForLeafRole
                cmdDelFunctionForRole.Enabled = True
            Else
                MessageBox.Show("Lỗi khi gắn chức năng cho Role", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "Các hàm phục vụ cho việc sắp xếp lại thứ tự các phân hệ hoặc các Role"
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Sự kiện nhấn nút để cập nhật chức năng cho một Role
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub cmdUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim PreNode As TreeNode
        Dim CurrNode As TreeNode
        Dim arrPreNode() As TreeNode
        Dim arrCurrNode() As TreeNode
        Dim PreTag As Object
        Dim CurrTag As Object
        Dim PreText As String
        Dim CurrText As String
        Dim iCurrRoleID, iChangeRoleID As Integer
        Try
            iCurrRoleID = CInt(tvwAdminSystem.SelectedNode.Tag.ToString.Substring(tvwAdminSystem.SelectedNode.Tag.ToString.IndexOf("#") + 1))
            iChangeRoleID = CInt(tvwAdminSystem.SelectedNode.PrevNode.Tag.ToString.Substring(tvwAdminSystem.SelectedNode.PrevNode.Tag.ToString.IndexOf("#") + 1))
            PreNode = tvwAdminSystem.SelectedNode.PrevNode.Clone
            CurrNode = tvwAdminSystem.SelectedNode.Clone

            tvwAdminSystem.SelectedNode.Nodes.Clear()
            tvwAdminSystem.SelectedNode.PrevNode.Nodes.Clear()

            For Each oNode As TreeNode In CurrNode.Nodes
                tvwAdminSystem.SelectedNode.PrevNode.Nodes.Add(oNode)
            Next
            For Each oNode As TreeNode In PreNode.Nodes
                tvwAdminSystem.SelectedNode.Nodes.Add(oNode)
            Next
            PreTag = PreNode.Tag
            CurrTag = CurrNode.Tag
            PreText = PreNode.Text
            CurrText = CurrNode.Text
            'Hoán chuyển vị trí 2 Node cho nhau
            tvwAdminSystem.SelectedNode.PrevNode.Tag = CurrTag
            tvwAdminSystem.SelectedNode.PrevNode.Text = CurrText
            tvwAdminSystem.SelectedNode.Tag = PreTag
            tvwAdminSystem.SelectedNode.Text = PreText
            'Chọn lại Selected Node là PreNode
            tvwAdminSystem.SelectedNode = tvwAdminSystem.SelectedNode.PrevNode
            ChangeOrder(iCurrRoleID, iChangeRoleID)
            tvwAdminSystem_Click(sender, e)
            ModifyToolBarButton()
            gv_bRoleHasChanged = True
        Catch ex As Exception
            gv_bRoleHasChanged = False
        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Sự kiện nhấn nút để cập nhật chức năng cho một Role
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub cmdDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim _NextNode As TreeNode
        Dim CurrNode As TreeNode
        Dim NextTag As Object
        Dim CurrTag As Object
        Dim NextText As String
        Dim CurrText As String
        Dim iCurrRoleID, iChangeRoleID As Integer
        Try
            iCurrRoleID = CInt(tvwAdminSystem.SelectedNode.Tag.ToString.Substring(tvwAdminSystem.SelectedNode.Tag.ToString.IndexOf("#") + 1))
            iChangeRoleID = CInt(tvwAdminSystem.SelectedNode.NextNode.Tag.ToString.Substring(tvwAdminSystem.SelectedNode.NextNode.Tag.ToString.IndexOf("#") + 1))
            _NextNode = tvwAdminSystem.SelectedNode.NextNode.Clone
            CurrNode = tvwAdminSystem.SelectedNode.Clone

            tvwAdminSystem.SelectedNode.Nodes.Clear()
            tvwAdminSystem.SelectedNode.NextNode.Nodes.Clear()

            For Each oNode As TreeNode In CurrNode.Nodes
                tvwAdminSystem.SelectedNode.NextNode.Nodes.Add(oNode)
            Next
            For Each oNode As TreeNode In _NextNode.Nodes
                tvwAdminSystem.SelectedNode.Nodes.Add(oNode)
            Next

            NextTag = _NextNode.Tag
            CurrTag = CurrNode.Tag
            NextText = _NextNode.Text
            CurrText = CurrNode.Text
            'Hoán chuyển vị trí 2 Node cho nhau
            tvwAdminSystem.SelectedNode.NextNode.Tag = CurrTag
            tvwAdminSystem.SelectedNode.NextNode.Text = CurrText
            tvwAdminSystem.SelectedNode.Tag = NextTag
            tvwAdminSystem.SelectedNode.Text = NextText
            'Chọn lại Selected Node là PreNode
            tvwAdminSystem.SelectedNode = tvwAdminSystem.SelectedNode.NextNode
            ChangeOrder(iCurrRoleID, iChangeRoleID)
            tvwAdminSystem_Click(sender, e)
            ModifyToolBarButton()
            gv_bRoleHasChanged = True
        Catch ex As Exception
            gv_bRoleHasChanged = False
        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Thực hiện hoán đổi 2 vị trí của Role cho nhau trong CSDL
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub ChangeOrder(ByVal pv_iCurrRoleID As Integer, ByVal pv_iChangeRoleID As Integer)
        Dim sv_ds1 As New DataSet
        Dim sv_ds2 As New DataSet
        Dim sv_oRole As New clsRole
        Dim iOrder1, iOrder2 As Integer
        Try
            sv_ds1 = sv_oRole.dsGetRoleInfor(pv_iCurrRoleID)
            sv_ds2 = sv_oRole.dsGetRoleInfor(pv_iChangeRoleID)
            If Not sv_ds1 Is Nothing And Not sv_ds2 Is Nothing Then
                iOrder1 = CInt(sv_ds1.Tables(0).Rows(0)("iOrder"))
                iOrder2 = CInt(sv_ds2.Tables(0).Rows(0)("iOrder"))
                For Each DR As DataRow In gv_dsRole.Tables(0).Rows
                    If DR("iRole") = pv_iCurrRoleID Then
                        DR("iOrder") = iOrder2
                        Exit For
                    End If
                Next
                For Each DR As DataRow In gv_dsRole.Tables(0).Rows
                    If DR("iRole") = pv_iChangeRoleID Then
                        DR("iOrder") = iOrder1
                        Exit For
                    End If
                Next
                sv_oRole.UpdateOrder1(pv_iCurrRoleID, iOrder2)
                sv_oRole.UpdateOrder1(pv_iChangeRoleID, iOrder1)
            Else
                MessageBox.Show("Lỗi lấy thông tin để cập nhật Role. Liên hệ với người lập trình", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Lỗi lấy thông tin để cập nhật Role. Liên hệ với người lập trình", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
#End Region

#Region " Các hàm phục vụ cho việc kéo thả trên cây quản trị"
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Sự kiện nhấn nút để cập nhật chức năng cho một Role
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub tvwAdminSystem_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvwAdminSystem.DragEnter
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", False) Then
            If InStr(CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode).Tag.ToString.ToUpper, "LEAFROLES") > 0 Then
                e.Effect = DragDropEffects.Copy
            End If
        End If
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Sự kiện nhấn nút để cập nhật chức năng cho một Role
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub tvwAdminSystem_ItemDrag(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles tvwAdminSystem.ItemDrag
        Try
            If e.Button = MouseButtons.Left Then
                If InStr(CType(e.Item, TreeNode).Tag.ToString.ToUpper, "LEAFROLES") > 0 Then
                    'invoke the drag and drop operation
                    DoDragDrop(e.Item, DragDropEffects.Move Or DragDropEffects.Copy)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Sự kiện kéo thả Role giữa các phân hệ làm việc
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub tvwAdminSystem_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvwAdminSystem.DragDrop
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
            'Lấy về tất cả các Node con của Node được kéo thả
            mv_arrNode.Clear()
            getAllChildNode(DragNode)
            If Not DestinationNode Is Nothing Then
                'Kiểm tra đảm bảo chỉ thực hiện kéo thả giữa các Role Node và Node nguồn không được là Node phân hệ
                If InStr(DragNode.Tag.ToString.ToUpper, "LEAFROLES") > 0 And InStr(DestinationNode.Tag.ToString.ToUpper, "LEAFROLES") > 0 And Not bIsSubSystemNode(DragNode) Then
                    'Kiểm tra xem node đích và Node nguồn có là một hay không?
                    If Not DestinationNode Is DragNode Then
                        'Ngăn không cho kéo Node vào chính cha của nó
                        If Not DestinationNode Is DragNode.Parent Then
                            'Kiểm tra đảm bảo không được kéo Node ông cha vào Node Con cháu
                            If Not mv_arrNode.Contains(DestinationNode) Then
                                'Kiểm tra đảm bảo DestinationNode không là LeafNode
                                If bIsLeafRoleHasChild(DestinationNode) Then
                                    If gv_bAnnouceBeforeDropRole Then
                                        If MessageBox.Show("Bạn có thực sự muốn thả Role " & DragNode.Text & " vào Role " & DestinationNode.Text & " không?", gv_sAnnouncement, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                                            Return
                                        Else
                                            'Lấy về mã Role và Mã ParentRole
                                            intSourceRoleID = CInt(DragNode.Tag.ToString.Substring(DragNode.Tag.ToString.IndexOf("#") + 1))
                                            intSourceParentRoleID = CInt(DestinationNode.Tag.ToString.Substring(DestinationNode.Tag.ToString.IndexOf("#") + 1))
                                            If txtFunctionID.Text.Trim = String.Empty Then
                                                txtFunctionID.Text = "-1"
                                            End If
                                            iFuntionID = CInt(txtFunctionID.Text)
                                            'Gán ChildNode cho DesNode
                                            TemNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode", False), TreeNode).Clone

                                            '----------------------------------------------------------------------------
                                            If (e.KeyState And CtrlMask) <> CtrlMask Then 'Cut
                                                'Cập nhật vào CSDL
                                                UpdateAfterDragAndDrop(intSourceRoleID, intSourceParentRoleID, DestinationNode, DestinationNode.GetNodeCount(False) + 1, GetDataRow(intSourceRoleID), True)
                                                'RecursiveNode(DragNode, False)
                                                DragNode.Remove()
                                            Else 'Copy
                                                'Cập nhật vào CSDL
                                                UpdateAfterDragAndDrop(intSourceRoleID, intSourceParentRoleID, DestinationNode, DestinationNode.GetNodeCount(False) + 1, GetDataRow(intSourceRoleID), False)
                                                'Cập nhật lại FunctionID và RoleID cho Role đích
                                                With TemNode
                                                    .Tag = "LEAFROLES|" & iFuntionID & "#" & _clsRole.iGetNewestRole
                                                End With
                                                'Gọi thủ tục cập nhật đệ quy khi Node được kéo thả là một ParentNode
                                                RecursiveNode(TemNode, False)
                                            End If

                                            DestinationNode.Nodes.Add(TemNode)
                                            gv_bRoleHasChanged = True
                                            '-----------------------------------------------------------------------------
                                            If Not mv_bLoading Then StatusBar1.Panels(2).Text = "  Đã kéo thả Role thành công  "
                                            tvwAdminSystem.SelectedNode = DestinationNode
                                            DestinationNode.Expand()
                                        End If
                                    Else
                                        'Lấy về mã Role và Mã ParentRole
                                        intSourceRoleID = CInt(DragNode.Tag.ToString.Substring(DragNode.Tag.ToString.IndexOf("#") + 1))
                                        intSourceParentRoleID = CInt(DestinationNode.Tag.ToString.Substring(DestinationNode.Tag.ToString.IndexOf("#") + 1))
                                        If txtFunctionID.Text.Trim = String.Empty Then
                                            txtFunctionID.Text = "-1"
                                        End If
                                        iFuntionID = CInt(txtFunctionID.Text)
                                        'Gán ChildNode cho DesNode
                                        TemNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode", False), TreeNode).Clone

                                        '----------------------------------------------------------------------------
                                        If (e.KeyState And CtrlMask) <> CtrlMask Then 'Cut
                                            'Cập nhật vào CSDL
                                            UpdateAfterDragAndDrop(intSourceRoleID, intSourceParentRoleID, DestinationNode, DestinationNode.GetNodeCount(False) + 1, GetDataRow(intSourceRoleID), True)
                                            'RecursiveNode(DragNode, False)
                                            DragNode.Remove()
                                        Else 'Copy
                                            'Cập nhật vào CSDL
                                            UpdateAfterDragAndDrop(intSourceRoleID, intSourceParentRoleID, DestinationNode, DestinationNode.GetNodeCount(False) + 1, GetDataRow(intSourceRoleID), False)
                                            'Cập nhật lại FunctionID và RoleID cho Role đích
                                            With TemNode
                                                .Tag = "LEAFROLES|" & iFuntionID & "#" & _clsRole.iGetNewestRole
                                            End With
                                            'Gọi thủ tục cập nhật đệ quy khi Node được kéo thả là một ParentNode
                                            RecursiveNode(TemNode, False)
                                        End If

                                        DestinationNode.Nodes.Add(TemNode)
                                        gv_bRoleHasChanged = True
                                        '-----------------------------------------------------------------------------
                                        If Not mv_bLoading Then StatusBar1.Panels(2).Text = "  Đã kéo thả Role thành công  "
                                        tvwAdminSystem.SelectedNode = DestinationNode
                                        DestinationNode.Expand()
                                    End If
                                Else
                                    If Not mv_bLoading Then StatusBar1.Panels(2).Text = "  Không thực hiện kéo thả vì Node nguồn là LeafNode  "
                                End If
                            Else
                                If Not mv_bLoading Then StatusBar1.Panels(2).Text = "  Không thực hiện kéo thả vì Node được kéo thả có cấp cao hơn Node đích"
                            End If
                        Else
                            If Not mv_bLoading Then StatusBar1.Panels(2).Text = "  Đã kéo thả Role thành công  "
                        End If
                    Else
                        If Not mv_bLoading Then StatusBar1.Panels(2).Text = "  Không được kéo thả Node phân hệ  "
                    End If
                End If
            End If
        End If
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
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Kiểm tra xem Node có phải là Node lá và có con không
    'Đầu vào          :Node cần kiểm tra
    'Đầu ra            :True= đúng. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Function bIsLeafRoleHasChild(ByVal pv_oNode As TreeNode) As Boolean
        If InStr(pv_oNode.Tag.ToString.ToUpper, "LEAFROLES") > 0 And bhasNoFunction(pv_oNode) Then
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
    Private Sub TabCtr_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabCtr.SelectedIndexChanged
        mv_intTabIndex = TabCtr.SelectedTab.TabIndex
    End Sub
    Private Sub TabControl2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl2.SelectedIndexChanged
        mv_intGroupTabIndex = TabControl2.SelectedTab.TabIndex
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
    Private Sub UpdateOrderOfAllNextNodes(ByVal pv_oNode As TreeNode)
        Try
            If Not pv_oNode.NextNode Is Nothing Then
                Dim intRoleID As Integer
                intRoleID = CInt(pv_oNode.NextNode.Tag.ToString.Substring(pv_oNode.NextNode.Tag.ToString.IndexOf("#") + 1))
                For Each DR As DataRow In gv_dsRole.Tables(0).Rows
                    If DR("iRole") = intRoleID Then
                        DR("iOrder") = DR("iOrder") - 1
                        Dim sv_oRole As New clsRole
                        sv_oRole.UpdateOrder1(intRoleID, DR("iOrder"))
                        sv_oRole = Nothing
                        Exit For
                    End If
                Next
                UpdateOrderOfAllNextNodes(pv_oNode.NextNode)
            End If

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Các hàm phục vụ cho việc cập nhật các ShortcutKey và Icon cho Roles"
    Private Sub getShortCutKey()
        Dim en As Type
        Try
            en = GetType(Shortcut)
            Dim i As Integer = 0
            CboName.Items.Clear()
            CboValue.Items.Clear()
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
    Private Sub CboName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboValue.SelectedIndexChanged, CboName.SelectedIndexChanged
        If CboValue.Items.Count = CboName.Items.Count Then
            If sender.Equals(CboName) Then
                CboValue.SelectedIndex = CboName.SelectedIndex
            Else
                CboName.SelectedIndex = CboValue.SelectedIndex
            End If
            Dim _Role As New clsRole
            Dim sv_iRole As Integer
            sv_iRole = CInt(tvwAdminSystem.SelectedNode.Tag.ToString.Substring(tvwAdminSystem.SelectedNode.Tag.ToString.IndexOf("#") + 1))
            If _Role.bUpdateField("Sys_ROLES", "intShortCutKey=" & CInt(CboValue.SelectedItem), "iRole=" & sv_iRole) Then
                'Cập nhật vào DataSet
                For Each dr As DataRow In gv_dsRole.Tables(0).Rows
                    If CInt(dr("iRole")) = sv_iRole Then
                        dr("intShortCutKey") = CInt(CboValue.SelectedItem)
                        Exit For
                    End If
                Next
                gv_dsRole.Tables(0).AcceptChanges()
            Else
                MessageBox.Show("Lỗi cập nhật phím tắt cho Role.", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            _Role = Nothing
        End If
    End Sub
    Private Sub picIcon_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picIcon.MouseDown
        Dim openDiag As New OpenFileDialog
        Try
            If e.Button = MouseButtons.Left Then
                openDiag.Title = "Chọn biểu tượng cho Menu"
                openDiag.Filter = "Icon|*.ico|Gif|*.gif|Jpg|*.Jpg|Bmp|*.Bmp|PNG|*.PNG"
                If openDiag.ShowDialog = DialogResult.OK Then
                    picIcon.Image = Image.FromFile(openDiag.FileName)
                    Dim _Role As New clsRole
                    Dim sv_iRole As Integer
                    sv_iRole = CInt(tvwAdminSystem.SelectedNode.Tag.ToString.Substring(tvwAdminSystem.SelectedNode.Tag.ToString.IndexOf("#") + 1))
                    'If _Role.bUpdateField("Sys_ROLES", "sIconPath=N'" & openDiag.FileName.Substring(openDiag.FileName.LastIndexOf("/") + 1) & "'", "iRole=" & sv_iRole) Then
                    If _Role.bUpdateField("Sys_ROLES", "sIconPath=N'" & Path.GetFileName(openDiag.FileName) & "'", "iRole=" & sv_iRole) Then
                        'Cập nhật vào DataSet
                        For Each dr As DataRow In gv_dsRole.Tables(0).Rows
                            If CInt(dr("iRole")) = sv_iRole Then
                                dr("sIconPath") = Path.GetFileName(openDiag.FileName)
                                Exit For
                            End If
                        Next
                        gv_dsRole.Tables(0).AcceptChanges()
                    Else
                        MessageBox.Show("Lỗi cập nhật Icon cho Role.", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    _Role = Nothing
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Icon bạn vừa chọn không sử dụng được. Mời bạn chọn lại!", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
    Private Sub cmdIcon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picIcon.Click

    End Sub
#End Region

#Region "Các hàm phục vụ tìm kiếm trên cây quản trị"
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Xóa một nhánh Menu của một phân hệ
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub _DeleteBranch(ByVal pv_oNode As TreeNode, ByRef intCount As Integer)
        Dim sv_oDR As DataRow
        Dim s As String
        Try
            'Kiểm tra xem User có quyền nào hay không?
            Me.Cursor = Cursors.WaitCursor
            s = grdRolesForUsers.CaptionText
            StatusBarPanel3.Text = "Đang xóa quyền của người dùng..."
            'Nếu có thì lấy về mã Role cần xóa
            Dim sv_iRole As Integer = CInt(pv_oNode.Tag.ToString.Substring(pv_oNode.Tag.ToString.IndexOf("#") + 1))
            'Gọi hàm xóa Role!
            gv_objTrans = VNS.Libs.globalVariables.SqlConn.BeginTransaction
            If _DeleteAllChildRoles(sv_iRole, intCount) Then
                MessageBox.Show("Đã xóa Role " & pv_oNode.Text & " và tất cả các Role con của nó", "Tổng số " & intCount.ToString & " Role bị xóa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                gv_dsRole.Tables(0).AcceptChanges()
                gv_objTrans.Commit()
            Else
                gv_objTrans.Rollback()
                MessageBox.Show("Lỗi xóa Role", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Cursor = Cursors.Default
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function _DeleteAllChildRoles(ByVal pv_iRole As Integer, ByRef intCount As Integer) As Boolean
        Dim sv_iParentID As Integer
        Dim sv_oClsRole As New clsRole
        Dim sv_oDR() As DataRow
        Dim sv_iCount As Integer = 0
        Try
            'Lấy về quyền cần xóa và các quyền con của quyền cần xóa
            sv_oDR = gv_dsRole.Tables(0).Select("iRole=" & pv_iRole & " OR iParentRole=" & pv_iRole)
            For sv_iCount = 0 To sv_oDR.Length - 1
                Dim sv_iRoleID As Integer
                sv_iRoleID = sv_oDR(sv_iCount).Item("iRole")
                'Xóa quyền ra khỏi CSDL
                If Not _bDeleteBranch(sv_iRoleID) Then
                    Return False
                End If
                intCount += 1
                'Xóa quyền ra khỏi Grid
                gv_dsRole.Tables(0).Rows.Remove(sv_oDR(sv_iCount))
                'Gọi đệ quy để xóa các ChildRole của Role hiện thời
                'Kiểm tra để đảm bảo rằng thủ tục đệ quy sẽ không được gọi 2 lần đối với Role ban đầu truyền vào
                If sv_iRoleID <> pv_iRole Then
                    _DeleteAllChildRoles(sv_iRoleID, intCount)
                End If
            Next
            Return True
        Catch ex As Exception
            gv_objTrans.Rollback()
            Return False
        End Try
    End Function
    Private Sub SearchFunctionOnTreeView(ByVal pv_sFunctionName As String)
        For Each oNode As TreeNode In tvwAdminSystem.Nodes(0).Nodes(2).Nodes
            If oNode.Text.ToUpper = pv_sFunctionName.ToUpper Then
                tvwAdminSystem.SelectedNode = oNode
                Return
            End If
        Next
    End Sub
    Private Sub SearchParamOnTreeView(ByVal pv_sParamName As String)
        For Each oNode As TreeNode In tvwAdminSystem.Nodes(0).Nodes(4).Nodes
            If oNode.Text.ToUpper = pv_sParamName.ToUpper Then
                tvwAdminSystem.SelectedNode = oNode
                Return
            End If
        Next
    End Sub
    Private Sub tvwAdminSystem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tvwAdminSystem.KeyDown
        Select Case e.KeyCode
            Case Keys.F3
                SearchStart()
            Case Keys.Down
                If e.Modifiers = Keys.Control Then
                    If InStr(tvwAdminSystem.SelectedNode.Tag.ToString.ToUpper, "LEAFROLES") > 0 Then
                        cmdDown_Click(sender, New System.EventArgs)
                    End If
                End If
            Case Keys.Up
                If e.Modifiers = Keys.Control Then
                    If InStr(tvwAdminSystem.SelectedNode.Tag.ToString.ToUpper, "LEAFROLES") > 0 Then
                        cmdUp_Click(sender, New System.EventArgs)
                    End If
                End If
            Case Keys.D
                If e.Modifiers = Keys.Control Then
                    If InStr(tvwAdminSystem.SelectedNode.Tag.ToString.ToUpper, "LEAFROLES") > 0 Then
                        If Not tvwAdminSystem.SelectedNode.Parent.Tag.ToString.Trim.Equals("ROOTROLE#-2") Then
                            If MessageBox.Show("Bạn có muốn xóa Role " & tvwAdminSystem.SelectedNode.Text & " và các Role con của nó không?", gv_sAnnouncement, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                                Dim intRoleCount As Integer
                                intRoleCount = 0
                                UpdateOrderOfAllNextNodes(tvwAdminSystem.SelectedNode)
                                _DeleteBranch(tvwAdminSystem.SelectedNode, intRoleCount)
                                gv_bRoleHasChanged = True
                                gv_bChangeToolBar = True
                                tvwAdminSystem.SelectedNode.Remove()
                                If gv_bChangeToolBar Then
                                    Application.DoEvents()
                                    RefreshToolBar()
                                    gv_bChangeToolBar = False
                                End If
                            End If
                        End If
                    ElseIf InStr(tvwAdminSystem.SelectedNode.Tag.ToString.ToUpper, "LEAFFUNCTIONS#") > 0 Then
                        DeleteFunction_Click(sender, New System.EventArgs)
                    ElseIf InStr(tvwAdminSystem.SelectedNode.Tag.ToString.ToUpper, "NODEPARAM") > 0 Then
                        DeleteParam(sender, New System.EventArgs)
                    ElseIf InStr(tvwAdminSystem.SelectedNode.Tag.ToString.ToUpper, "LEAFUSER#") > 0 Then
                        DeleteUser_Click(sender, New System.EventArgs)
                    End If
                End If
            Case Else
        End Select
    End Sub
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
        Dim oNode As TreeNode = tvwAdminSystem.SelectedNode
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
        If sSeachType = "QTHT" Then
            sv_oForm.chkSearchBranch.Visible = False
        Else
            sv_oForm.chkSearchBranch.Visible = True
        End If
        sv_oForm.ShowDialog()
        If sv_oForm.mv_bHasSearch Then
            SearchTree(sv_oForm.chkSearchBranch.Checked, sSeachType, sv_oForm.mv_bSearchLike, sv_oForm.mv_sValue)
            If Not sv_oForm.chkSearchBranch.Checked Then
                sNotFound = "Không tìm được Node nào trên cây có giá trị: "
            End If
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
                SearchNode(tvwAdminSystem.Nodes(0), pv_bSearchLike, pv_sValue)
            Else
                Select Case pv_sSearchType
                    Case "QTHT"
                        SearchNode(tvwAdminSystem.Nodes(0), pv_bSearchLike, pv_sValue)
                    Case "USER"
                        SearchNode(tvwAdminSystem.Nodes(0).Nodes(1), pv_bSearchLike, pv_sValue)
                    Case "FUNCTION"
                        SearchNode(tvwAdminSystem.Nodes(0).Nodes(2), pv_bSearchLike, pv_sValue)
                    Case "ROLE"
                        SearchNode(tvwAdminSystem.Nodes(0).Nodes(3), pv_bSearchLike, pv_sValue)
                    Case "PARAM"
                        SearchNode(tvwAdminSystem.Nodes(0).Nodes(4), pv_bSearchLike, pv_sValue)
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
                    tvwAdminSystem.SelectedNode = oNode
                    mv_bFound = True
                    Exit Sub
                End If
            Else
                If oNode.Text.Trim.ToUpper = pv_sValue.ToUpper And Not mv_bFound Then
                    tvwAdminSystem.SelectedNode = oNode
                    mv_bFound = True
                    Exit Sub
                End If
            End If
            If Not mv_bFound Then SearchNode(oNode, pv_sSearchLike, pv_sValue)
        Next
    End Sub
#End Region

#Region "Các hàm phục vụ cho việc cập nhật biểu tượng, ảnh nền cho mỗi phân hệ"
    Private Sub picSubSystem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picSubSystem.Click
        Dim sv_iRoleID As Integer
        Dim sv_clsRole As New clsRole
        Dim fileDiag As New OpenFileDialog
        Try
            'Lấy về mã ParentRoleID
            If fileDiag.ShowDialog = DialogResult.OK Then
                sv_iRoleID = CInt(tvwAdminSystem.SelectedNode.Tag.ToString.Substring(tvwAdminSystem.SelectedNode.Tag.ToString.IndexOf("#") + 1))
                If sv_clsRole.bUpdateImgPath(sv_iRoleID, "sImgPath", fileDiag.FileName) Then
                    picSubSystem.Image = Image.FromFile(fileDiag.FileName)
                    gv_dsRole.Tables(0).Select("iRole=" & sv_iRoleID)(0)("sImgPath") = fileDiag.FileName
                    gv_dsRole.Tables(0).AcceptChanges()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Các hàm xây dựng ToolBar cho từng phân hệ"
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Thêm mới một tham số
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : CuongDV
    'Ngày tạo         :06/03/2006
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Sub ShowCollection(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If TabControl1.SelectedIndex <> 1 Then
                TabControl1.SelectedIndex = 1
            End If
            Dim sv_oForm As New frm_TbrProperty
            sv_oForm.ShowDialog()
            If gv_bChangeToolBar Then
                Application.DoEvents()
                RefreshToolBar()
                gv_bChangeToolBar = False
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Try
            Select Case CType(sender, TabControl).SelectedIndex
                Case 0
                Case 1
                    Try
                        If gv_bChangeToolBar Then
                            Application.DoEvents()
                            RefreshToolBar()
                            gv_bChangeToolBar = False
                        End If
                    Catch ex As Exception

                    End Try
            End Select
        Catch ex As Exception

        End Try
    End Sub
    Private Sub RefreshToolBar()
        Dim _Img As New ImageList
        Try
            '-------------------------------------------------------------------------
            lblLoading.Text = "Removing old ToolBar..."
            lblLoading.Refresh()
            RemoveHandler tbrforSubSys.ButtonClick, AddressOf _ClickTbr
            'Lấy về danh sách các nút của ToolBar
            ms_GetAllToolBarButton(gv_intSubSysID)
            'Xóa các Icon cũ các nút của ToolBar
            If mv_dsIconPathForToolBarButton.Tables.Count > 0 Then
                mv_dsIconPathForToolBarButton.Tables(0).Clear()
            End If
            'Xóa mảng lưu trữ các đường dẫn Icon của các nút của ToolBar
            mv_objImgForToolBarButton.Clear()
            Try
                For i As Integer = 0 To _Img.Images.Count - 1
                    _Img.Images.RemoveAt(0)
                Next
            Catch ex As Exception
            End Try
            'Lấy về đường dẫn các Icon cho các nút của ToolBar
            mv_dsIconPathForToolBarButton = ms_dsIconList("Sys_TOOLBARBUTTON")
            For i As Integer = 0 To mv_dsIconPathForToolBarButton.Tables(0).Rows.Count - 1
                If File.Exists(mv_dsIconPathForToolBarButton.Tables(0).Rows(i)("sIconPath")) Then
                    Try
                        _Img.Images.Add(Image.FromFile(mv_dsIconPathForToolBarButton.Tables(0).Rows(i)("sIconPath")))
                    Catch ex As Exception
                    End Try
                    mv_objImgForToolBarButton.Add(mv_dsIconPathForToolBarButton.Tables(0).Rows(i)("sIconPath"))
                End If
            Next
            tbrforSubSys.ImageList = Nothing
            tbrforSubSys.ImageList = _Img
            lblLoading.Text = "Building new ToolBar..."
            lblLoading.Refresh()
            BuildToolBarButtons(gv_intSubSysID)
            Application.DoEvents()
            lblLoading.Text = "DoubleClick here to show Toolbar Collection"
            lblLoading.Refresh()
            AddHandler tbrforSubSys.ButtonClick, AddressOf _ClickTbr
            '-------------------------------------------------------------------------
        Catch ex As Exception

        End Try
    End Sub
    '----------------------------------------------------------------------------------------------------
    'Mục đích       :Thực hiện khi chọn một chức năng trên Menu
    'Đầu vào         :
    'Đầu ra           :
    'Người tạo      :CuongDV
    'Ngày tạo       :
    'Last Modified:
    '----------------------------------------------------------------------------------------------------
    Private Sub _ClickTbr(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs)
        Try
            If CType(e.Button, ToolBarBtn).DropDownMenu Is Nothing Then
                ms_InvokeForm(CType(e.Button, ToolBarBtn).mv_sAssemblyName, CType(e.Button, ToolBarBtn).mv_sDLLName, CType(e.Button, ToolBarBtn).mv_sFormName, CType(e.Button, ToolBarBtn).mv_sParameterList, CType(e.Button, ToolBarBtn).mv_sRoleName)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    '----------------------------------------------------------------------------------------------------
    'Mục đích       :Thực hiện chức năng tương ứng với hàm được gọi trong Assembly
    'Đầu vào         :Tên Assembly,tên DLL,tên Form
    'Đầu ra           :Menu của phân hệ được xây dựng 
    'Người tạo      :CuongDV
    'Ngày tạo       :
    'Last Modified:
    '----------------------------------------------------------------------------------------------------
    Public Sub ms_InvokeForm(ByVal pv_sAssemblyName As String, _
                                                ByVal pv_sDLLName As String, _
                                                ByVal pv_sFormName As String, ByVal pv_sParameterList As String, ByVal pv_sText As String)
        Dim s As String
        Try
            s = "Bạn đang thực hiện chức năng: " & pv_sText & vbCrLf
            If pv_sFormName.Trim.Equals(String.Empty) Then
                MessageBox.Show("Chưa gán Role cho nút này hoặc Role ứng với nút này chưa được gán chức năng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            s &= "Các thông số cụ thể như sau: " & vbCrLf
            s &= "1.Tên DLL: " & pv_sDLLName & vbCrLf
            s &= "2.Tên Form: " & pv_sFormName & vbCrLf
            s &= "3.Danh sách tham số đầu vào của Form: " & IIf(pv_sParameterList.Trim.Equals(String.Empty), "Không có", pv_sParameterList)
            MessageBox.Show(s, "Chức năng", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Chưa gán Role cho nút này hoặc Role ứng với nút này chưa được gán chức năng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
    '----------------------------------------------------------------------------------------------------
    'Mục đích       :Lấy về danh sách các Icon 
    'Đầu vào         :
    'Đầu ra           :
    'Người tạo      :CuongDV
    'Ngày tạo       :
    'Last Modified:
    '----------------------------------------------------------------------------------------------------
    Private Function ms_dsIconList(Optional ByVal pv_sTableName As String = "Sys_ROLES") As DataSet
        Dim sv_oDA As SqlDataAdapter
        Dim fv_ds As New DataSet
        Dim fv_sSql As String
        Try
            fv_sSql = "SELECT distinct sIconPath  FROM " & pv_sTableName & " WHERE sIconPath is not NUll AND  sIconPath<>N'UNKNOWN'"
            sv_oDA = New SqlDataAdapter(fv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oDA.Fill(fv_ds, "Sys_ROLES")
            Return fv_ds
        Catch ex As Exception

        End Try
    End Function
    '----------------------------------------------------------------------------------------------------
    'Mục đích       :Lấy về danh sách quyền của User
    'Đầu vào         :Tên đăng nhập
    'Đầu ra           :Danh sách các quyền
    'Người tạo      :CuongDV
    'Ngày tạo       :
    'Last Modified:
    '----------------------------------------------------------------------------------------------------
    Private Sub ms_GetAllToolBarButton(ByVal pv_intSubSysID As Integer)
        Dim sv_oDA As SqlDataAdapter
        Dim sv_oUser As New clsUser
        Dim sv_sSql As String
        Try
            mv_oDSForToolBarButton.Clear()
            sv_sSql = "SELECT RFU.intOrder,RFU.intDisplayText, RFU.intStyle,RFU.sIconPath,RFU.FP_intRoleID,RFU.intRolePerformed,RFU.sText,RFU.sTTT,R.iOrder,R.intShortCutKey,R.sRoleName,R.sEngRoleName,F.sDLLName,F.sFormName,F.sAssemblyName,sParameterList,ISNull(F.bEnable,1) bEnable, R.sImgPath " & _
                             " from Sys_ToolBarButton RFU LEFT JOIN Sys_ROLES R " & _
                             " ON R.iRole=RFU.intRolePerformed LEFT JOIN Sys_FUNCTIONS F ON R.FK_iFunctionID=F.PK_iID " & _
                             " WHERE RFU.FP_intRoleID=" & pv_intSubSysID & " ORDER BY RFU.intOrder"
            sv_oDA = New SqlDataAdapter(sv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oDA.Fill(mv_oDSForToolBarButton, "Sys_ROLES")
        Catch ex As Exception

        End Try
    End Sub
    '----------------------------------------------------------------------------------------------------
    'Mục đích       :Xây dựng ToolBarButton 
    'Đầu vào         :Mã phân hệ
    'Đầu ra           :Menu của phân hệ được xây dựng 
    'Người tạo      :CuongDV
    'Ngày tạo       :
    'Last Modified:
    '----------------------------------------------------------------------------------------------------
    Public Sub BuildToolBarButtons(ByVal pv_iSubSystemID As Integer)
        Dim sv_oDVCTbrBtn As New DVC_ToolBarButton
        Dim sv_oDR As DataRow()
        Try
            Try
                For i As Integer = 0 To tbrforSubSys.Buttons.Count - 1
                    tbrforSubSys.Buttons.RemoveAt(0)
                Next
            Catch ex As Exception

            End Try
            sv_oDVCTbrBtn.mv_DS = gv_dsRole
            For i As Integer = 0 To mv_oDSForToolBarButton.Tables(0).Rows.Count - 1
                Dim sv_otbrBtn As ToolBarBtn
                'Lấy về ImageIndex của Menu

                sv_otbrBtn = sv_oDVCTbrBtn.GetToolBarButton(IsNull_VN(mv_oDSForToolBarButton.Tables(0).Rows(i)("sRoleName")), IsNull_VN(mv_oDSForToolBarButton.Tables(0).Rows(i)("sText")), IsNull_VN(mv_oDSForToolBarButton.Tables(0).Rows(i)("sTTT")), _
                                                                        IsNull_VN(mv_oDSForToolBarButton.Tables(0).Rows(i)("intRolePerformed")), IsNull_VN(mv_oDSForToolBarButton.Tables(0).Rows(i)("sDLLName")), IsNull_VN(mv_oDSForToolBarButton.Tables(0).Rows(i)("sFormName")), _
                                                                        IsNull_VN(mv_oDSForToolBarButton.Tables(0).Rows(i)("sAssemblyName")), IsNull_VN(mv_oDSForToolBarButton.Tables(0).Rows(i)("sParameterList")), mv_objImageListForToolBarButton, mv_oDSForToolBarButton.Tables(0).Rows(i)("sIconPath"), intGetImgIndexForToolBarButton(IsNull_VN(mv_oDSForToolBarButton.Tables(0).Rows(i)("sIconPath"))), mv_oDSForToolBarButton.Tables(0).Rows(i)("intStyle"), tbrforSubSys, IIf(mv_oDSForToolBarButton.Tables(0).Rows(i)("intDisplayText") = 0, False, True))

                'Kiểm tra xem Menu có được kích hoạt hay không
                If Not CBool(mv_oDSForToolBarButton.Tables(0).Rows(i)("bEnable")) Then
                    sv_otbrBtn.Enabled = False
                Else
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
    '----------------------------------------------------------------------------------------------------
    'Mục đích       :Lấy về ImageIndex Icon của Menu trong Image List
    'Đầu vào         :Tên file Icon
    'Đầu ra           :Index tìm thấy
    'Người tạo      :CuongDV
    'Ngày tạo       :
    'Last Modified:
    '----------------------------------------------------------------------------------------------------
    Private Function intGetImgIndexForToolBarButton(ByVal pv_sIconPath As String)
        Try
            For i As Integer = 0 To mv_objImgForToolBarButton.Count - 1
                If mv_objImgForToolBarButton(i) = pv_sIconPath Then
                    Return i
                End If
            Next
            Return -1
        Catch ex As Exception
            Return -1
        End Try
    End Function
    Private Function IsNull_VN(ByVal pv_obj As Object) As String
        Return IIf(IsDBNull(pv_obj), " ", pv_obj.ToString)
    End Function
#End Region

#Region "Các hàm xử lý sự kiện CurrentCellChanged trên các DataGrid"
    Private Sub grdFunction_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            grdFunction.Select(grdFunction.CurrentRowIndex)
            grdFunction.CurrentCell = New DataGridCell(grdFunction.CurrentRowIndex, 0)
            If InStr(tvwAdminSystem.SelectedNode.Tag, "FUNCTION") > 0 Then
                SearchFunctionOnTreeView(grdFunction.Item(grdFunction.CurrentRowIndex, 2).ToString)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub grdParamList_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdParamList.CurrentCellChanged
        Try
            grdParamList.Select(grdParamList.CurrentRowIndex)
            grdParamList.CurrentCell = New DataGridCell(grdParamList.CurrentRowIndex, 0)
            If InStr(tvwAdminSystem.SelectedNode.Tag, "PARAM") > 0 Then
                SearchParamOnTreeView(grdParamList.Item(grdParamList.CurrentRowIndex, 1).ToString)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub grdRolesForUsers_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdRolesForUsers.CurrentCellChanged
        Try
            grdRolesForUsers.Select(grdRolesForUsers.CurrentRowIndex)
            grdRolesForUsers.CurrentCell = New DataGridCell(grdRolesForUsers.CurrentRowIndex, 0)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdUser_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdUser.CurrentCellChanged
        Try
            grdUser.Select(grdUser.CurrentRowIndex)
            grdUser.CurrentCell = New DataGridCell(grdUser.CurrentRowIndex, 0)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdChildRole_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdChildRole.CurrentCellChanged
        Try
            grdChildRole.Select(grdChildRole.CurrentRowIndex)
            grdChildRole.CurrentCell = New DataGridCell(grdChildRole.CurrentRowIndex, 0)
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Các hàm xử lý sự kiện DoubleClick trên các DataGrid"
    Private Sub grdUser_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdUser.DoubleClick
        Try
            If gv_bCanUpdateByDblClickOnGrid Then
                gv_bCallContextMenuFromTreeView = False
                UpdateUser_Click(sender, e)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub grdFunction_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If gv_bCanUpdateByDblClickOnGrid Then
                gv_bCallContextMenuFromTreeView = False
                UpdateFunction_Click(sender, e)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub grdParamList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdParamList.DoubleClick
        Try
            UpdateParam(sender, e)
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Các hàm xử lý sự kiện MouseDown trên các DataGrid"
    Private Sub grdUser_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdUser.MouseDown
        Try
            Dim hti As DataGrid.HitTestInfo = grdUser.HitTest(e.X, e.Y)
            If hti.Row >= 0 Then
                grdUser.UnSelect(grdUser.CurrentRowIndex)
                grdUser.CurrentRowIndex = hti.Row
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdFunction_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            Dim hti As DataGrid.HitTestInfo = grdFunction.HitTest(e.X, e.Y)
            If hti.Row >= 0 Then
                grdFunction.UnSelect(grdFunction.CurrentRowIndex)
                grdFunction.CurrentRowIndex = hti.Row
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdParamList_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdParamList.MouseDown
        Try
            Dim hti As DataGrid.HitTestInfo = grdParamList.HitTest(e.X, e.Y)
            If hti.Row >= 0 Then
                grdParamList.UnSelect(grdParamList.CurrentRowIndex)
                grdParamList.CurrentRowIndex = hti.Row
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdChildRole_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdChildRole.MouseDown
        Try
            Dim hti As DataGrid.HitTestInfo = grdChildRole.HitTest(e.X, e.Y)
            If hti.Row >= 0 Then
                grdChildRole.UnSelect(grdChildRole.CurrentRowIndex)
                grdChildRole.CurrentRowIndex = hti.Row
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub grdRolesForUsers_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdRolesForUsers.KeyDown
        Try
            If grdRolesForUsers.VisibleRowCount > 0 Then
                If e.KeyCode = Keys.Delete Then
                    cmdMoveRight.PerformClick()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim sv_oForm As New frmSearchFunctions
        sv_oForm.ShowDialog()
        If sv_oForm.mv_bHasSearch Then
            SearchFunctionForRole(sv_oForm.mv_sDLL, sv_oForm.mv_sFunction)
        End If
    End Sub
    Private Sub SearchFunctionForRole(ByVal pv_sDLLName As String, ByVal pv_sFormName As String)
        Dim j As Integer = -1
        Try
            If grdFunction.VisibleRowCount > 0 Then
                For i As Integer = 0 To CType(grdFunction.DataSource, DataView).Table.Rows.Count - 1
                    If pv_sDLLName.Trim <> "" And pv_sFormName.Trim <> "" Then
                        If InStr(grdFunction.Item(i, 3).ToString.ToUpper, pv_sDLLName.ToUpper) > 0 And InStr(grdFunction.Item(i, 4).ToString.ToUpper, pv_sFormName.ToUpper) > 0 Then
                            j = i
                            Exit For
                        Else
                        End If
                    ElseIf pv_sDLLName.Trim <> "" And pv_sFormName.Trim = "" Then
                        If InStr(grdFunction.Item(i, 3).ToString.ToUpper, pv_sDLLName.ToUpper) > 0 Then
                            j = i
                            Exit For
                        Else
                        End If
                    Else
                        If InStr(grdFunction.Item(i, 4).ToString.ToUpper, pv_sFormName.ToUpper) > 0 Then
                            j = i
                            Exit For
                        Else
                        End If
                    End If

                Next
                If j <> -1 Then
                    grdFunction.UnSelect(grdFunction.CurrentRowIndex)
                    grdFunction.Select(j)
                    grdFunction.CurrentCell = New DataGridCell(j, 0)
                Else

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdChildRole_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdChildRole.DoubleClick
        Try
            Dim iRole As Integer
            iRole = CInt(grdChildRole.Item(grdChildRole.CurrentRowIndex, 1))
            For Each node As TreeNode In tvwAdminSystem.SelectedNode.Nodes
                Dim irole1 As Integer
                irole1 = CInt(node.Tag.ToString.Substring(node.Tag.ToString.IndexOf("#") + 1))
                If irole1 = iRole Then
                    tvwAdminSystem.SelectedNode = node
                    Exit Sub
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
    Private Sub grdGroup_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdGroup.DoubleClick
        Try
            If gv_bCanUpdateByDblClickOnGrid Then
                gv_bCallContextMenuFromTreeView = False
                UpdateGroupUser_Click(sender, e)
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
