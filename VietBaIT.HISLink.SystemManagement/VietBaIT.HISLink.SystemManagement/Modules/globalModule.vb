Imports System.Data.SqlClient
Imports clsRegistry
Imports System.IO
Imports VNS.Libs
Imports VNS.Properties

Public Module globalModule
    Public gv_intSubSysID As Integer = 0 'Biến dùng để lưu trữ ID của phân hệ phục vụ trong việc hiển thị ToolBar của phân hệ
    Public gv_intCurrRoleID As Integer = 0 'Biến dùng để lưu trữ ID của phân hệ phục vụ trong việc hiển thị ToolBar của phân hệ
    Public gv_sSubSysName As String  'Biến dùng để lưu trữ tên của phân hệ phục vụ trong việc hiển thị ToolBar của phân hệ
    Public gv_intRolePerformed As Integer = 0 'Biến dùng lưu trữ Node đang được chọn hiện thời để tránh trường hợp
    ' mỗi lần click vào Node trên TreeView là phải load lại Panel bên phải
    Public gv_bIncreateOrDecrete As Boolean = True
    Public gv_sAnnouncement As String = "Thông báo" ' Biến làm Title cho các MessageBox
    Public gv_dsParam As New DataSet ' Biến lưu trữ danh sách các tham số của đơn vị trong CSDL
    Public gv_dsFunction As New DataSet ' Biến lưu trữ toàn bộ Function của đơn vị trong CSDL
    Public gv_dsTbrBtn As New DataSet ' Biến lưu trữ toàn bộ Function của đơn vị trong CSDL
    Public gv_dsRole As New DataSet ' Biến lưu trữ toàn bộ quyền của chi nhánh trong CSDL
    Public gv_dsUser As New DataSet ' Biến lưu trữ toàn bộ Users của chi nhánh trong CSDL
    Public gv_dsGroupUser As New DataSet ' Biến lưu trữ toàn bộ Users của chi nhánh trong CSDL
    Public gv_dsGroupMember As New DataSet ' Biến lưu trữ toàn bộ Users của chi nhánh trong CSDL
    Public gv_dsDelegate As New DataSet ' Biến lưu trữ toàn bộ Users của chi nhánh trong CSDL
    Public gv_dsGroupRoles As New DataSet ' Biến lưu trữ quyền của một Group
    Public gv_dsRolesForUsers As New DataSet ' Biến lưu trữ quyền của một User
    Public gv_bCallContextMenuFromTreeView As Boolean = True
    Public gv_bCanUpdateByDblClickOnGrid As Boolean = False ' Biến xác định có được phép gọi chức năng cập nhật khi DoubleClick on DataGrid hay không?

    Public gv_sBranchID As String = "HIS" 'Mã chi nhánh
    Public gv_sBranchName As String = "  Trung tâm CNTT-EVN  " ' Tên chi nhánh
    Public gv_sAddress As String = "Hà Nội"
    Public gv_sPhone As String = "0904 648006"
    Public gv_sEmail As String = "trangdm@daosen.com.vn"
    Public gv_swebsite As String = "www.daosen.com.vn"

    Public gv_sComName As String = System.Environment.MachineName ' Chứa tên hoặc ID của máy chủ CSDL
    Public gv_sDBName As String = "Assembly" ' Tên CSDL
    Public gv_sUID As String = "CUONGDV" ' Chứa tên đăng nhập QTHT
    Public gv_sPWD As String = "CUONGDV" ' Chứa mật khẩu công khai của QTHT
    Public gv_bLoginSuccess As Boolean = False 'Biến xác định xem có đăng nhập được vào CSDL với tài khoản nhập vào không?
    Public gv_sSymmetricAlgorithmName As String = "Rijndael" 'Tên thuật toán mã hóa đối xứng
    Public gv_ConnectSuccess As Boolean = False 'Biến xác định xem có kết nối được vào CSDL để làm việc không?
    Public gv_sSecretUID As String 'User bí mật
    Public gv_sSecretPWD As String 'Mật khẩu bí mật
    Public gv_arrKeyhasSearched As New ArrayList 'Lưu trữ danh sách các từ khóa tìm kiếm trên TreeView
    Public gv_arrDLLhasSearched As New ArrayList 'Lưu trữ danh sách các DLL tìm kiếm trên Grid
    Public gv_arrFunctionhasSearched As New ArrayList 'Lưu trữ danh sách các Function tìm kiếm trên Grid

    'Tháng Năm hệ thống
    Public gv_intCurrMonth As Integer
    Public gv_intCurrYear As Integer
    Public gv_oNode As TreeNode
    Public gv_bChangeToolBar As Boolean = True 'Biến xác định xem ToolBar có gì thay đổi không để build lại
    Private gv_mtx As System.Threading.Mutex 'Thread cho phép chỉ 1 ứng dụng chạy tại một thời điểm
    Public gv_oMainForm As New frmManSysApp_new
    Public gv_objTrans As SqlTransaction
    Public gv_oTTT As New ToolTip
    '-------------------------------------------------------------------------------------
    'Các biến chung phần tùy chọn
    Public gv_sDefaultIconPathForSubSystem As String 'Đường dẫn tới Icon mặc định cho phân hệ
    Public gv_sDefaultImgPathForSubSystem As String 'Đường dẫn tới ảnh mặc định cho phân hệ
    Public gv_sDefaultIconPathForRole As String 'Đường dẫn tới Icon mặc định cho Role
    Public gv_intRoleLevel As Integer 'Cấp cao nhất của Role
    Public gv_bEnableDragAndDrop As Boolean
    Public gv_bAnnouceBeforeDropRole As Boolean
    '-------------------------------------------------------------------------------------
    Public gv_bCannotDeletePWDOfUID As Boolean 'Cấm xóa mật khẩu người dùng
    Public gv_bCannotDeletePWDOfAllUIDs As Boolean 'Cấm xóa mật khẩu tất cả các người dùng
    Public gv_bCannotDeleteUID As Boolean 'Cấm Xóa người dùng
    Public gv_bMixedRolesOfUsers As Boolean 'Trộn quyền người dùng
    Public gv_bCanDblClickToGetRolesForUser As Boolean 'Dbl Click trên để lấy Role cho User
    '-------------------------------------------------------------------------------------
    Public gv_bAnnounceAfterInsertingSuccessfully As Boolean
    Public gv_bAnnounceAfterUpdatingSuccessfully As Boolean
    Public gv_bAnnounceAfterDeletingSuccessfully As Boolean
    Public gv_bAskingBeforeDeleting As Boolean
    Public gv_bCloseFormAfterDML As Boolean
    '-------------------------------------------------------------------------------------
    Public gv_LockedFunctionColor As Color
    Public gv_bAnnouceAfterActivatingFunction As Boolean
    Public gv_bAnnouceAfterLockingFunction As Boolean
    Public gv_bCannotDeleteFunction As Boolean
    '-------------------------------------------------------------------------------------
    Public gv_LockedParamColor As Color
    Public gv_bAnnouceAfterActivatingParam As Boolean
    Public gv_bAnnouceAfterLockingParam As Boolean

    '------------------------------------------------------------------------------------
    Public gv_DSRoleForOutIn As New DataSet
    Public gv_sXMLFilePath As String = String.Empty
    Public mv_sConnString As String
    Public gv_bRoleHasChanged As Boolean = False ' Biến xác định có cần Load lại cây phân quyền cho người dùng mỗi lần chọn một User hay không

    Public Enum NodeStatus
        SubSystemNode = -1
        MenuLevel1Node = 0
        OtherNode = 1
    End Enum
    Public Enum ImageIndex
        RootNode = 0
        RootUser = 1
        NodeUser = 2
        RootFuntion = 3
        NodeFuntion = 4
        RootRole = 5
        NodeRole = 6
        LeafRole = 7
        RootParam = 8
        NodeParam = 9
        Lock = 13
        LockFunction = 17
    End Enum
    Public Enum Status
        Insert = 1
        Update = 0
        Delete = -1
        InsertSubSystemNode = 2
        InsertMenuLevel1Node = 3
    End Enum
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Hàm Main được gọi mỗi khi khởi tạo ứng dụng. Áp dụng cơ chế của lớp Mutex nhằm chỉ cho
    '                 phép chạy một khởi tạo của ứng dụng tại một thời điểm trên một máy
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Sub Main()
        Try
            Dim bCreated As Boolean
            'gv_mtx = New System.Threading.Mutex(False, "Sys_DVC", bCreated)
            'If bCreated = True Then
            gv_oMainForm.ShowDialog()
            'Else
            'MessageBox.Show("Một khởi tạo của chương trình đang chạy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'End
            'End If
        Catch ex As Exception
        End Try
    End Sub
    Public Function getGroupID(ByVal oNode As TreeNode) As Integer
        Try
            Return oNode.Tag.ToString.Substring(oNode.Tag.ToString.IndexOf("#") + 1)
        Catch ex As Exception
            Return -1
        End Try
    End Function
    Public Function getMaxID(ByVal pv_sFieldName As String, ByVal pv_sTableName As String) As Integer
        Dim fv_dt As New DataTable
        Dim fv_da As New SqlDataAdapter("SELECT MAX(" & pv_sFieldName & ") FROM " & pv_sTableName, VNS.Libs.globalVariables.SqlConn)
        Try
            fv_da.Fill(fv_dt)
            If fv_dt.Rows.Count > 0 Then
                Return IIf(IsDBNull(fv_dt.Rows(0)(0)), 0, fv_dt.Rows(0)(0)) + 1
            Else
                Return 1
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return -1
        End Try
    End Function
    Public Function getExactlyMaxID(ByVal pv_sFieldName As String, ByVal pv_sTableName As String) As Integer
        Dim fv_dt As New DataTable
        Dim fv_da As New SqlDataAdapter("SELECT MAX(" & pv_sFieldName & ") FROM " & pv_sTableName, VNS.Libs.globalVariables.SqlConn)
        Try
            fv_da.Fill(fv_dt)
            If fv_dt.Rows.Count > 0 Then
                Return IIf(IsDBNull(fv_dt.Rows(0)(0)), 0, fv_dt.Rows(0)(0))
            Else
                Return 1
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return -1
        End Try
    End Function
    Public Function InitializeConnection() As Boolean
        Dim sv_oEncrypt As New VietBaIT.Encrypt(gv_sSymmetricAlgorithmName)
        Try
            If bGetConfigInfor(gv_sSecretUID, gv_sSecretPWD) Then
                If gv_sSecretUID Is Nothing Or gv_sSecretPWD Is Nothing Then
                    Return False
                End If
                Dim sv_sConnectionString As String = "workstation id=" & gv_sComName & ";packet size=4096;data source=" & gv_sComName & ";persist security info=False;initial catalog=" & gv_sDBName & ";uid=" & gv_sSecretUID & ";pwd=" & gv_sSecretPWD
                mv_sConnString = sv_sConnectionString
                If IsNothing(VNS.Libs.globalVariables.SqlConn) Then
                    VNS.Libs.globalVariables.SqlConnectionString = mv_sConnString
                    VNS.Libs.globalVariables.SqlConn = New SqlConnection(sv_sConnectionString)
                    VNS.Libs.globalVariables.SqlConn.Open()
                    GetBranchInfor(VNS.Libs.globalVariables.Branch_ID)
                ElseIf VNS.Libs.globalVariables.SqlConn.State = ConnectionState.Closed Then
                    VNS.Libs.globalVariables.SqlConn.Open()
                End If
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show("Không kết nối được vào CSDL. Liên hệ với quản trị hệ thống ", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Đọc file cấu hình để lấy về mã đơn vị quản lý, tên CSDL, UserName, Password
    'Đầu vào          :
    'Đầu ra            :Thành công=True. Không thành công=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function bGetConfigInfor(ByRef pv_sUID As String, ByRef pv_sPWD As String) As Boolean
        Dim fv_DS As New DataSet
        Dim fv_sUID As String
        Dim fv_sPWD As String
        Try

            globalVariables.m_strPropertiesFolder = Application.StartupPath + "\Properties\"
            PropertyLib._ConfigProperties = PropertyLib.GetConfigProperties()
            globalVariables.ServerName = PropertyLib._ConfigProperties.DataBaseServer
            globalVariables.sUName = PropertyLib._ConfigProperties.UID
            globalVariables.sPwd = PropertyLib._ConfigProperties.PWD
            globalVariables.sDbName = PropertyLib._ConfigProperties.DataBaseName
            globalVariables.sMenuStyle = "DOCKING" 'Utility.sDbnull(dsConfigXML.Tables[0].Rows[0]["INTERFAC
            gv_sComName = PropertyLib._ConfigProperties.DataBaseServer
            gv_sBranchID = PropertyLib._ConfigProperties.MaDvi
            fv_sUID = PropertyLib._ConfigProperties.UID
            fv_sPWD = PropertyLib._ConfigProperties.PWD
            gv_sDBName = PropertyLib._ConfigProperties.DataBaseName

            'If File.Exists(Application.StartupPath & "\Config.XML") Then
            '    ' Tiến hành đọc File cấu hình vào DataSet
            '    fv_DS.ReadXml(Application.StartupPath & "\Config.XML")
            '    If fv_DS.Tables(0).Rows.Count > 0 Then
            '        ' Đọc dữ liệu vào các biến toàn cục
            '        'Địa chỉ máy chủ CSDL
            '        gv_sComName = fv_DS.Tables(0).Rows(0)("SERVERADDRESS")
            '        gv_sBranchID = fv_DS.Tables(0).Rows(0)("BranchID").ToString.ToUpper
            '        fv_sUID = fv_DS.Tables(0).Rows(0)("USERNAME")
            '        fv_sPWD = fv_DS.Tables(0).Rows(0)("PASSWORD")
            '        gv_sDBName = fv_DS.Tables(0).Rows(0)("DATABASE_ID")
            'Tiến hành kết nối bằng tài khoản công khai vừa đọc trong file Config để lấy về tài khoản đăng nhập CSDL
            Dim fv_oSQLCon As SqlConnection
            Dim fv_sSqlConstr = "workstation id=" & gv_sComName & ";packet size=4096;data source=" & gv_sComName & ";persist security info=False;initial catalog=" & gv_sDBName & ";uid=" & fv_sUID & ";pwd=" & fv_sPWD
            fv_oSQLCon = New SqlConnection(fv_sSqlConstr)
            'Mở CSDL
            Try
                fv_oSQLCon.Open()
                'Lấy tài khoản bí mật để đăng nhập CSDL
                'GetSecretAccount(fv_oSQLCon, pv_sUID, pv_sPWD)
                pv_sUID = fv_sUID
                pv_sPWD = fv_sPWD

            Catch SQLex As Exception
                MessageBox.Show("Không đăng nhập được vào CSDL " & gv_sDBName & " bằng tài khoản công khai(UID=" & fv_sUID & ";PWD=" & fv_sPWD & "). Hãy cấu hình lại File Config.XML sau đó đăng nhập lại.", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End Try
            '    Else
            '        MessageBox.Show("Không có dữ liệu trong File cấu hình! Bạn hãy xem lại", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        Return False
            '    End If
            'Else
            '    MessageBox.Show("Không tồn tại File cấu hình có tên: Config.XML!", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Return False
            'End If
            Return True
        Catch ex As Exception

        End Try
    End Function
    Private Sub GetSecretAccount(ByVal pv_Conn As SqlConnection, ByRef pv_sUID As String, ByRef pv_sPWD As String)
        Dim sv_Ds As New DataSet
        Dim sv_DA As SqlDataAdapter
        Try
            sv_DA = New SqlDataAdapter("SELECT * from Sys_SECURITY", pv_Conn)
            sv_DA.Fill(sv_Ds, "Sys_SECURITY")
            If sv_Ds.Tables(0).Rows.Count > 0 Then
                pv_sUID = sv_Ds.Tables(0).Rows(0)("sUID")
                pv_sPWD = sv_Ds.Tables(0).Rows(0)("sPWD")
            Else
                MessageBox.Show("Không tồn tại tài khoản đăng nhập trong bảng Sys_SECURITY! Đề nghị với DBAdmin tạo tài khoản đăng nhập trong bảng đó.", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Bạn cần gán lại quyền truy cập vào bảng Sys_SECURITY cho tài khoản công khai! Đề nghị với DBAdmin thực hiện công việc này bằng tiện ích CreateUser.exe", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
    Public Function IIF_VN(ByVal pv_oObject As Object) As String
        If IsDBNull(pv_oObject) Or pv_oObject Is Nothing Then
            Return "Chưa gán"
        Else
            Return pv_oObject.ToString
        End If
    End Function
    Public Function ValidData(ByVal pv_sValue As String) As String
        If Not pv_sValue.Trim.Equals(String.Empty) Then
            Return pv_sValue.Trim.Replace("'", "''")
        Else
            Return " "
        End If
    End Function
    '---------------------------------------------------------------------------------------------------
    'Mục đích  :Tạo Effect mờ dần lúc Form hiện lên hoặc đóng đi
    'Đầu vào   : -Form cần tạo Effect(pv_oForm)
    '                 -Độ mờ dần(pv_fStepLevel)
    '                 -Đối tượng điều khiển sự mờ dần của Form(pv_oTmr as Timer)
    '                 -Biến xác định là tăng độ mờ hay giảm độ mờ của Form.Mặc định là tăng Opacity(pv_bIncreteOrDecrete)
    '                 -Biến xác định khoảng thời gian của mỗi lần tăng(giảm) độ mờ(pv_iTimerInterval)
    'Đầu ra: Form được trình diễn
    'Người Tạo :CuongDV
    'Return      :None
    'Ngày Tạo  :22/02/2005
    '---------------------------------------------------------------------------------------------------
    Public Sub gs_SlideMe(ByVal pv_oForm As Form, ByVal pv_fStepLevel As Double, ByVal pv_oTmr As Timer, Optional ByVal pv_bIncreteOrDecrete As Boolean = True, Optional ByVal pv_iTimerInterval As Integer = 10)
        Try
            'Thiết lập khoảng thời gian chờ của đối tượng điều khiển Timer
            pv_oTmr.Interval = pv_iTimerInterval
            Select Case pv_bIncreteOrDecrete
                Case True 'Tăng độ mờ
                    If pv_oForm.Opacity < 1 Then
                        pv_oForm.Opacity += pv_fStepLevel
                    Else
                        'Nếu Opacity=1(Tức là Form đã hiện hoàn toàn) thì ngừng không tăng nữa
                        pv_oTmr.Enabled = False
                    End If
                Case False 'Giảm độ mờ
                    If pv_oForm.Opacity > 0 Then
                        pv_oForm.Opacity -= pv_fStepLevel
                    Else
                        'Nếu Opacity<=0(Tức là Form đã mờ hoàn toàn) thì ngừng không giảm nữa và thực hiện đóng Form
                        pv_oTmr.Enabled = False
                        pv_oForm.Close()
                    End If
            End Select
        Catch ex As Exception
        End Try
    End Sub
    Public Sub GetRegValueForOptions()
        Try
            Dim clsReg As New clsRegistry.clsRegistry
            gv_sDefaultIconPathForSubSystem = s_IsNothingOrDBNull(VBITJSC.RegConfiguration.GetSettings("DVC_COMPANY", "Sys_MAN_DVC", "DefaultIconPathForSubSystem"))
            gv_sDefaultImgPathForSubSystem = s_IsNothingOrDBNull(VBITJSC.RegConfiguration.GetSettings("DVC_COMPANY", "Sys_MAN_DVC", "DefaultImgPathForSubSystem"))
            gv_sDefaultIconPathForRole = s_IsNothingOrDBNull(VBITJSC.RegConfiguration.GetSettings("DVC_COMPANY", "Sys_MAN_DVC", "DefaultIconPathForSubRole"))
            gv_intRoleLevel = Int_IsNothingOrDBNull(VBITJSC.RegConfiguration.GetSettings("DVC_COMPANY", "Sys_MAN_DVC", "RoleLevel"), 5)
            gv_bEnableDragAndDrop = b_IsNothingOrDBNull(VBITJSC.RegConfiguration.GetSettings("DVC_COMPANY", "Sys_MAN_DVC", "EnableDragAndDrop"))
            gv_bAnnouceBeforeDropRole = b_IsNothingOrDBNull(VBITJSC.RegConfiguration.GetSettings("DVC_COMPANY", "Sys_MAN_DVC", "AnnouceBeforeDropRole"), False)
            '-------------------------------------------------------------------------------------
            gv_bCannotDeletePWDOfUID = b_IsNothingOrDBNull(VBITJSC.RegConfiguration.GetSettings("DVC_COMPANY", "Sys_MAN_DVC", "CannotDeletePWDOfUID"))
            gv_bCannotDeletePWDOfAllUIDs = b_IsNothingOrDBNull(VBITJSC.RegConfiguration.GetSettings("DVC_COMPANY", "Sys_MAN_DVC", "CannotDeletePWDOfAllUIDs"))
            gv_bCannotDeleteUID = b_IsNothingOrDBNull(VBITJSC.RegConfiguration.GetSettings("DVC_COMPANY", "Sys_MAN_DVC", "CannotDeleteUID"))
            gv_bMixedRolesOfUsers = b_IsNothingOrDBNull(VBITJSC.RegConfiguration.GetSettings("DVC_COMPANY", "Sys_MAN_DVC", "MixedRolesOfUsers"))
            gv_bCanDblClickToGetRolesForUser = b_IsNothingOrDBNull(VBITJSC.RegConfiguration.GetSettings("DVC_COMPANY", "Sys_MAN_DVC", "CanDblClickToGetRolesForUser"))
            '-------------------------------------------------------------------------------------
            gv_bAnnounceAfterInsertingSuccessfully = b_IsNothingOrDBNull(VBITJSC.RegConfiguration.GetSettings("DVC_COMPANY", "Sys_MAN_DVC", "AnnounceAfterInsertingSuccessfully"), False)
            gv_bAnnounceAfterUpdatingSuccessfully = b_IsNothingOrDBNull(VBITJSC.RegConfiguration.GetSettings("DVC_COMPANY", "Sys_MAN_DVC", "AnnounceAfterUpdatingSuccessfully"), False)
            gv_bAnnounceAfterDeletingSuccessfully = b_IsNothingOrDBNull(VBITJSC.RegConfiguration.GetSettings("DVC_COMPANY", "Sys_MAN_DVC", "AnnounceAfterDeletingSuccessfully"), False)
            gv_bAskingBeforeDeleting = b_IsNothingOrDBNull(VBITJSC.RegConfiguration.GetSettings("DVC_COMPANY", "Sys_MAN_DVC", "AskingBeforeDeleting"))
            gv_bCloseFormAfterDML = b_IsNothingOrDBNull(VBITJSC.RegConfiguration.GetSettings("DVC_COMPANY", "Sys_MAN_DVC", "CloseFormAfterDML"), False)
            '-------------------------------------------------------------------------------------
            gv_LockedFunctionColor = Color.FromArgb(intGetArgb(VBITJSC.RegConfiguration.GetSettings("DVC_COMPANY", "Sys_MAN_DVC", "LockedFunctionColor")))
            gv_bAnnouceAfterActivatingFunction = b_IsNothingOrDBNull(VBITJSC.RegConfiguration.GetSettings("DVC_COMPANY", "Sys_MAN_DVC", "AnnouceAfterActivatingFunction"), False)
            gv_bAnnouceAfterLockingFunction = b_IsNothingOrDBNull(VBITJSC.RegConfiguration.GetSettings("DVC_COMPANY", "Sys_MAN_DVC", "AnnouceAfterLockingFunction"), False)
            gv_bCannotDeleteFunction = b_IsNothingOrDBNull(VBITJSC.RegConfiguration.GetSettings("DVC_COMPANY", "Sys_MAN_DVC", "CannotDeleteFunction"), True)
            '-------------------------------------------------------------------------------------
            gv_LockedParamColor = Color.FromArgb(intGetArgb(VBITJSC.RegConfiguration.GetSettings("DVC_COMPANY", "Sys_MAN_DVC", "LockedParamColor")))
            gv_bAnnouceAfterActivatingParam = b_IsNothingOrDBNull(VBITJSC.RegConfiguration.GetSettings("DVC_COMPANY", "Sys_MAN_DVC", "AnnouceAfterActivatingParam"), False)
            gv_bAnnouceAfterLockingParam = b_IsNothingOrDBNull(VBITJSC.RegConfiguration.GetSettings("DVC_COMPANY", "Sys_MAN_DVC", "AnnouceAfterLockingParam"), False)
        Catch ex As Exception
            AppLogger.NLogAction.Log.Trace("GetRegValueForOptions.Exception-->" + ex.Message)
        End Try
       

    End Sub
    Public Function intGetArgb(ByVal pv_obj As Object)
        If Not IsNumeric(pv_obj) OrElse IsDBNull(pv_obj) Or pv_obj Is Nothing Then
            Return Color.Red.ToArgb
        Else
            Return CInt(pv_obj)
        End If
    End Function
    Public Function b_IsNothingOrDBNull(ByVal pv_oObject As Object, Optional ByVal pv_bValue As Boolean = True) As String
        If IsDBNull(pv_oObject) OrElse pv_oObject Is Nothing Then
            Return pv_bValue
        Else
            Return CBool(pv_oObject)
        End If
    End Function
    Public Function IsNothingOrDBNull(ByVal pv_oObject As Object) As Boolean
        Return IsDBNull(pv_oObject) OrElse pv_oObject Is Nothing
    End Function
    Public Function Int_IsNothingOrDBNull(ByVal pv_oObject As Object, ByVal pv_bValue As Integer) As String
        If IsDBNull(pv_oObject) OrElse pv_oObject Is Nothing Then
            Return pv_bValue
        Else
            Return CInt(pv_oObject)
        End If
    End Function
    Public Function s_IsNothingOrDBNull(ByVal pv_oObject As Object) As String
        If IsDBNull(pv_oObject) Or pv_oObject Is Nothing Then
            Return ""
        Else
            Return pv_oObject.ToString
        End If
    End Function
    Private Sub GetBranchInfor(ByVal pv_sBranchID As String)
        Dim sv_Ds As New DataSet
        Dim sv_DA As SqlDataAdapter
        Try
            sv_DA = New SqlDataAdapter("SELECT * from Sys_ManagementUnit WHERE PK_sBranchID=N'" & pv_sBranchID & "'", VNS.Libs.globalVariables.SqlConn)
            sv_DA.Fill(sv_Ds, "Sys_ManagementUnit")
            If sv_Ds.Tables(0).Rows.Count > 0 Then
                VNS.Libs.globalVariables.Branch_Name = sDBnull(sv_Ds.Tables(0).Rows(0)("sName"))
                VNS.Libs.globalVariables.Branch_Address = sDBnull(sv_Ds.Tables(0).Rows(0)("sAddress"))
                VNS.Libs.globalVariables.Branch_Phone = sDBnull(sv_Ds.Tables(0).Rows(0)("sPhone"))
                VNS.Libs.globalVariables.Branch_Email = sDBnull(sv_Ds.Tables(0).Rows(0)("sEMAIL"))
                VNS.Libs.globalVariables.Branch_Website = sDBnull(sv_Ds.Tables(0).Rows(0)("WebSite"))

            Else
                Return
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function sDBnull(ByVal pv_obj As Object, Optional ByVal Reval As String = "") As String
        If IsDBNull(pv_obj) Or IsNothing(pv_obj) Then
            Return Reval
        Else
            Return pv_obj.ToString
        End If
    End Function
    Public Function dsGetAllTbrBtnForOutputConfig(ByVal pv_sBranchID As String) As DataSet
        Dim sv_Ds As New DataSet
        Dim sv_DA As SqlDataAdapter
        Try
            sv_DA = New SqlDataAdapter("SELECT * from Sys_ToolBarButton WHERE FP_sBranchID=N'" & pv_sBranchID & "'", VNS.Libs.globalVariables.SqlConn)
            sv_DA.Fill(sv_Ds, "Sys_ToolBarButton")
            If sv_Ds.Tables.Count > 0 Then
                Return sv_Ds
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function bIsValidPath(ByVal pv_sPath As String) As Boolean
        If pv_sPath.Trim.Equals(String.Empty) Then
            Return False
        Else
            If IO.File.Exists(pv_sPath) Then
                Return True
            Else
                Return False
            End If
        End If
    End Function
End Module
