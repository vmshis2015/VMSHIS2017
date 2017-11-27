
'-------------------------------------------------------------------------------------------------------
'Mục đích: Lớp User nhằm xử lý tất cả các nghiệp vụ liên quan đến người dùng
'Người tạo: CuongDV
'Ngày tạo :09/03/2005
'-------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient
Public Class clsUser
    Dim mv_sSql As String

    Public Sub New()
        Try
            If Not gv_ConnectSuccess Then
                gv_ConnectSuccess = InitializeConnection()
            End If
        Catch ex As Exception

        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Thêm mới người dùng
    'Đầu vào          :Thông tin người dùng
    'Đầu ra            :Thành công=True. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function InsertUser(ByVal pv_sBranchID As String, ByVal pv_sUID As String, ByVal pv_sFullName As String, ByVal pv_spwd As String, _
                                            ByVal pv_intSecurityLevel As Integer, ByVal pv_sDepart As String, ByVal pv_sDesc As String, Optional ByVal pv_bAdmin As Boolean = False) As Boolean
        Dim sv_oCmd As SqlCommand
        If pv_bAdmin Then
            If bIsExistedAmin(pv_sUID) Then
                Return False
            End If
        Else
            If bIsExisted(pv_sUID, pv_sBranchID) Then
                Return False
            End If
        End If
        If pv_bAdmin Then
            mv_sSql = "INSERT INTO Sys_ADMINISTRATOR (PK_sAdminID,FP_sBranchID,PK_sCreator,sPWD,iMonth,iYear) VALUES(N'" & _
                        pv_sUID & "',N'" & pv_sBranchID & "',N'Administrator',N'" & pv_spwd & "',10,2006)"
        Else
            mv_sSql = "INSERT INTO Sys_USERS(PK_sUID,FP_sBranchID,sPWD,sFullName,sDepart,iSecurityLevel,sDesc) VALUES(N'" & _
                        pv_sUID & "',N'" & pv_sBranchID & "',N'" & pv_spwd & "',N'" & pv_sFullName & "',N'" & pv_sDepart & "'," & pv_intSecurityLevel & ",N'" & pv_sDesc & "')"
        End If
        Try
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Thêm mới người dùng
    'Đầu vào          :Thông tin người dùng
    'Đầu ra            :Thành công=True. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function InsertGroup(ByVal pv_sBranchID As String, ByVal pv_GroupName As String, ByVal pv_sDesc As String, Optional ByVal pv_bAdmin As Boolean = False) As Integer
        Dim sv_oCmd As SqlCommand

        If bIsExistedGroup(pv_GroupName.Trim, pv_sBranchID) Then
            Return -1
        End If

        mv_sSql = "INSERT INTO Sys_GROUPS(BranchID,GroupName,[Desc],IsAdmin) VALUES(N'" & pv_sBranchID & "',N'" & pv_GroupName & "',N'" & pv_sDesc & "'," & IIf(pv_bAdmin, 1, 0) & ")"
        Try
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return getExactlyMaxID("GroupID", "Sys_Groups")
        Catch ex As Exception
            Return -1
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Thêm mới người dùng
    'Đầu vào          :Thông tin người dùng
    'Đầu ra            :Thành công=True. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function InsertMemberOfGroup(ByVal pv_sBranchID As String, ByVal GroupID As Integer, ByVal UID As String) As Boolean
        Dim sv_oCmd As SqlCommand


        mv_sSql = "INSERT INTO Sys_GROUPUser(BranchID,GroupID,UserID) VALUES(N'" & pv_sBranchID & "'," & GroupID & ",N'" & UID & "')"
        Try
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Thêm mới người dùng
    'Đầu vào          :Thông tin người dùng
    'Đầu ra            :Thành công=True. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function InsertDelegate(ByVal MainUser As String, ByVal DelegateUser As String) As Integer
        Dim sv_oCmd As SqlCommand


        mv_sSql = "INSERT INTO Sys_DelegateUser(BranchID,MainUser,DelegateUser) VALUES(N'" & gv_sBranchID & "',N'" & MainUser & "',N'" & DelegateUser & "')"
        Try
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return getExactlyMaxID("ID", "Sys_DelegateUser")
        Catch ex As Exception
            Return -1
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Cập nhật thông tin người dùng
    'Đầu vào          :Thông tin
    'Đầu ra            :Thành công=True. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function UpdateUser(ByVal pv_sBranchID As String, ByVal pv_sUID As String, ByVal pv_sFullName As String, _
                                             ByVal pv_sDepart As String, ByVal pv_sDesc As String) As Boolean
        Dim sv_oCmd As SqlCommand
        mv_sSql = "UPDATE Sys_USERS SET sFullName=N'" & pv_sFullName & "',sDepart=N'" & pv_sDepart & _
                         "',sDesc=N'" & pv_sDesc & "' WHERE PK_sUID=N'" & pv_sUID & "'" & " AND FP_sBranchID=N'" & pv_sBranchID & "'"
        Try
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Cập nhật thông tin người dùng
    'Đầu vào          :Thông tin
    'Đầu ra            :Thành công=True. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function UpdateGroup(ByVal pv_sBranchID As String, ByVal GroupID As Integer, ByVal GroupName As String, ByVal pv_sDesc As String, ByVal IsAdmin As Short) As Boolean
        Dim sv_oCmd As SqlCommand
        If bIsExistedGroupWithName(GroupID, GroupName, pv_sBranchID) Then
            Return False
        End If
        mv_sSql = "UPDATE Sys_GROUPS SET GROUPNAME=N'" & GroupName & "',[Desc]=N'" & pv_sDesc & "',IsAdmin=" & IsAdmin & " WHERE GroupID=" & GroupID & " AND BranchID=N'" & pv_sBranchID & "'"
        Try
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Xóa người dùng
    'Đầu vào          :Thông tin
    'Đầu ra            :Thành công=True. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function bDeleteUser(ByVal pv_sUID As String) As Boolean
        Dim sv_oCmd As SqlCommand
        mv_sSql = "DELETE FROM  Sys_USERS  WHERE PK_sUID=N'" & pv_sUID & "'" & " AND FP_sBranchID=N'" & gv_sBranchID & "'"
        Try
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            mv_sSql = "DELETE FROM  Sys_GroupUser  WHERE UserID=N'" & pv_sUID & "'" & " AND BranchID=N'" & gv_sBranchID & "'"
            sv_oCmd.CommandText = mv_sSql
            sv_oCmd.ExecuteNonQuery()
            mv_sSql = "DELETE FROM  Sys_RolesForUsers  WHERE sUID=N'" & pv_sUID & "'" & " AND FP_sBranchID=N'" & gv_sBranchID & "'"
            sv_oCmd.CommandText = mv_sSql
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Xóa người dùng
    'Đầu vào          :Thông tin
    'Đầu ra            :Thành công=True. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function bDeleteGroup(ByVal GroupID As Integer) As Boolean
        Dim sv_oCmd As SqlCommand
        mv_sSql = "DELETE FROM  Sys_Groups  WHERE GroupID=" & GroupID & " AND BranchID=N'" & gv_sBranchID & "'"
        Try
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            mv_sSql = "DELETE FROM  Sys_GroupUser  WHERE GroupID=" & GroupID & " AND BranchID=N'" & gv_sBranchID & "'"
            sv_oCmd.CommandText = mv_sSql
            sv_oCmd.ExecuteNonQuery()
            mv_sSql = "DELETE FROM  Sys_GroupRoles  WHERE GroupID=" & GroupID & " AND BranchID=N'" & gv_sBranchID & "'"
            sv_oCmd.CommandText = mv_sSql
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :  Lấy thông tin người dùng
    'Đầu vào          :Mã người dùng
    'Đầu ra            :Thông tin người dùng
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function dsGetGroupUserInfor(ByVal pv_GroupID As Integer) As DataSet
        Dim sv_DS As New DataSet
        Dim sv_DA As SqlDataAdapter
        mv_sSql = "SELECT * FROM  Sys_GROUPS  WHERE GroupID=" & pv_GroupID & " AND BranchID=N'" & gv_sBranchID & "'"
        Try
            sv_DA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_DA.Fill(sv_DS, "Sys_GROUPS")
            Return sv_DS
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :  Lấy thông tin người dùng
    'Đầu vào          :Mã người dùng
    'Đầu ra            :Thông tin người dùng
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function dsGetMemberOfGroup(ByVal pv_GroupID As Integer) As DataSet
        Dim sv_DS As New DataSet
        Dim sv_DA As SqlDataAdapter
        mv_sSql = "SELECT * FROM  Sys_GROUPUser  WHERE GroupID=" & pv_GroupID & " AND BranchID=N'" & gv_sBranchID & "'"
        Try
            sv_DA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_DA.Fill(sv_DS, "Sys_GROUPUser")
            Return sv_DS
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :  Lấy thông tin người dùng
    'Đầu vào          :Mã người dùng
    'Đầu ra            :Thông tin người dùng
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function DeleteMemberOfGroup(ByVal pv_GroupID As Integer, ByVal UID As String) As Boolean

        mv_sSql = "DELETE  FROM  Sys_GROUPUser  WHERE GroupID=" & pv_GroupID & " AND UserID=N'" & UID & "' AND BranchID=N'" & gv_sBranchID & "'"
        Try
            Dim Cmd As New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            Cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :  Lấy thông tin người dùng
    'Đầu vào          :Mã người dùng
    'Đầu ra            :Thông tin người dùng
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function DeleteMemberOfGroup(ByVal pv_GroupName As String, ByVal UID As String) As Boolean

        mv_sSql = "DELETE  FROM  Sys_GROUPUser  WHERE GroupID in(Select GroupID from Sys_Groups Where upper(GroupName)=N'" & pv_GroupName.Trim.ToUpper() & "' AND BranchID=N'" & gv_sBranchID & "') AND UserID=N'" & UID & "' AND BranchID=N'" & gv_sBranchID & "'"
        Try
            Dim Cmd As New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            Cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :  Lấy thông tin người dùng
    'Đầu vào          :Mã người dùng
    'Đầu ra            :Thông tin người dùng
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function DeleteAllMemberOfGroup(ByVal pv_GroupID As Integer) As Boolean

        mv_sSql = "DELETE  FROM  Sys_GROUPUser  WHERE GroupID=" & pv_GroupID & " AND BranchID=N'" & gv_sBranchID & "'"
        Try
            Dim Cmd As New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            Cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :  Lấy thông tin người dùng
    'Đầu vào          :Mã người dùng
    'Đầu ra            :Thông tin người dùng
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function DeleteAllDelegateOfUser(ByVal UID As String) As Boolean

        mv_sSql = "DELETE  FROM  Sys_Delegate  WHERE MainUser=N'" & UID & "' AND BranchID=N'" & gv_sBranchID & "'"
        Try
            Dim Cmd As New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            Cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :  Lấy thông tin người dùng
    'Đầu vào          :Mã người dùng
    'Đầu ra            :Thông tin người dùng
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function DeleteAllGroupOfMember(ByVal UID As String) As Boolean

        mv_sSql = "DELETE  FROM  Sys_GROUPUser  WHERE upper(UserID)=N'" & UID.Trim.ToUpper & "' AND BranchID=N'" & gv_sBranchID & "'"
        Try
            Dim Cmd As New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            Cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :  Lấy thông tin người dùng
    'Đầu vào          :Mã người dùng
    'Đầu ra            :Thông tin người dùng
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function dsGetUserInfor(ByVal pv_sUID As String) As DataSet
        Dim sv_DS As New DataSet
        Dim sv_DA As SqlDataAdapter
        mv_sSql = "SELECT * FROM  Sys_USERS  WHERE PK_sUID=N'" & pv_sUID & "'" & " AND FP_sBranchID=N'" & gv_sBranchID & "'"
        Try
            sv_DA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_DA.Fill(sv_DS, "Sys_USERS")
            Return sv_DS
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Thay đổi mật khẩu người dùng
    'Đầu vào          :Mã người dùng, mật khẩu mới
    'Đầu ra            :Thành công=True. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function bChangePassword(ByVal pv_sUID As String, ByVal pv_sNewPWD As String) As Boolean
        Dim sv_oCmd As SqlCommand
        Dim sv_oEncrypt As New VietBaIT.Encrypt(gv_sSymmetricAlgorithmName)
        mv_sSql = "UPDATE Sys_USERS SET sPWD=N'" & sv_oEncrypt.Mahoa(pv_sNewPWD) & "' WHERE PK_sUID=N'" & pv_sUID & "'" & " AND FP_sBranchID=N'" & gv_sBranchID & "'"
        Try
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Thay đổi mật khẩu người dùng
    'Đầu vào          :Mã người dùng, mật khẩu mới
    'Đầu ra            :Thành công=True. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function bChangePasswordForAdmin(ByVal pv_sUID As String, ByVal pv_sNewPWD As String) As Boolean
        Dim sv_oCmd As SqlCommand
        Dim sv_oEncrypt As New VietBaIT.Encrypt(gv_sSymmetricAlgorithmName)
        mv_sSql = "UPDATE Sys_ADMINISTRATOR SET sPWD=N'" & pv_sNewPWD & "' WHERE PK_sAdminID=N'" & pv_sUID & "'" & " AND FP_sBranchID=N'" & gv_sBranchID & "'"
        Try
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Xóa mật khẩu của tất cả người dùng
    'Đầu vào          :Mật khẩu mới
    'Đầu ra            :Thành công=True. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function bClearAllPassword(ByVal pv_sNewPWD As String) As Boolean
        Dim sv_oCmd As SqlCommand
        Dim sv_oEncrypt As New VietBaIT.Encrypt(gv_sSymmetricAlgorithmName)
        mv_sSql = "UPDATE Sys_USERS SET sPWD=N'" & sv_oEncrypt.Mahoa(pv_sNewPWD) & "'" & " WHERE FP_sBranchID=N'" & gv_sBranchID & "'"
        Try
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Kiểm tra xem Account mà người dùng nhập vào có tồn tại trong CSDL hay không?
    'Đầu vào          :UserName, Password
    'Đầu ra            :Thành công=True. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function bLoginSuccess(ByVal pv_sUID As Integer, ByVal pv_sPWD As String) As Boolean
        Dim sv_oDS As New DataSet
        Dim sv_oDA As SqlDataAdapter
        mv_sSql = "SELECT *  from Sys_USERS  WHERE PK_sUID=N'" & pv_sUID & "' AND sPWD=N'" & pv_sPWD & "'" & " AND FP_sBranchID=N'" & gv_sBranchID & "'"
        Try
            sv_oDA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oDA.Fill(sv_oDS, "Sys_USERS")
            If sv_oDS.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Lấy về tất cả các User trong CSDL
    'Đầu vào          :
    'Đầu ra            :Danh sách Users
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function dsGetAllUser() As DataSet
        Dim sv_oDS As New DataSet
        Dim sv_oDA As SqlDataAdapter
        Try
            mv_sSql = "SELECT * from Sys_USERS WHERE FP_sBranchID=N'" & gv_sBranchID & "' order by PK_sUID ASC"
            sv_oDA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oDA.Fill(sv_oDS, "Sys_USERS")
            Return sv_oDS
        Catch ex As Exception
            AppLogger.NLogAction.Log.Trace("dsGetAllUser.Exception-->" + ex.Message)
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Lấy về tất cả các User trong CSDL
    'Đầu vào          :
    'Đầu ra            :Danh sách Users
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function dsGetAllGroupUser() As DataSet
        Dim sv_oDS As New DataSet
        Dim sv_oDA As SqlDataAdapter
        Try
            mv_sSql = "SELECT * from Sys_Groups WHERE BranchID=N'" & gv_sBranchID & "' order by GroupName ASC"
            sv_oDA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oDA.Fill(sv_oDS, "Sys_Groups")
            Return sv_oDS
        Catch ex As Exception
            AppLogger.NLogAction.Log.Trace("dsGetAllGroupUser.Exception-->" + ex.Message)
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Lấy về tất cả các User trong CSDL
    'Đầu vào          :
    'Đầu ra            :Danh sách Users
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function dsGetDelegate() As DataSet
        Dim sv_oDS As New DataSet
        Dim sv_oDA As SqlDataAdapter
        Try
            mv_sSql = "SELECT * from Sys_DelegateUser P WHERE BranchID=N'" & gv_sBranchID & "' order by MainUser ASC"
            sv_oDA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oDA.Fill(sv_oDS, "Sys_DelegateUser")
            Return sv_oDS
        Catch ex As Exception
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Lấy về tất cả các User trong CSDL
    'Đầu vào          :
    'Đầu ra            :Danh sách Users
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function dsGetAllGroupMember() As DataSet
        Dim sv_oDS As New DataSet
        Dim sv_oDA As SqlDataAdapter
        Try
            mv_sSql = "SELECT *,(select TOP 1 GroupName from Sys_Groups where GroupID=P.GroupID) as GroupName from Sys_GroupUser P WHERE BranchID=N'" & gv_sBranchID & "' order by UserID ASC"
            sv_oDA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oDA.Fill(sv_oDS, "Sys_GroupUser")
            Return sv_oDS
        Catch ex As Exception
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Kiểm tra sự tồn tại của một UserName trong CSDL
    'Đầu vào          :UserName
    'Đầu ra            :Tồn tại=True. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function bIsExisted(ByVal pv_sUID As String, ByVal pv_sBranchID As String) As Boolean
        Dim sv_oDS As New DataSet
        Dim sv_oDA As SqlDataAdapter
        Try
            mv_sSql = "SELECT * from Sys_USERS WHERE PK_sUID =N'" & pv_sUID & "'" & " AND FP_sBranchID=N'" & pv_sBranchID & "'"
            sv_oDA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oDA.Fill(sv_oDS, "Sys_USERS")
            If sv_oDS.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Kiểm tra sự tồn tại của một UserName trong CSDL
    'Đầu vào          :UserName
    'Đầu ra            :Tồn tại=True. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function bIsExistedGroup(ByVal GroupName As String, ByVal pv_sBranchID As String) As Boolean
        Dim sv_oDS As New DataSet
        Dim sv_oDA As SqlDataAdapter
        Try
            mv_sSql = "SELECT * from Sys_Groups WHERE upper(GroupName) =N'" & GroupName.ToUpper & "'" & " AND BranchID=N'" & pv_sBranchID & "'"
            sv_oDA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oDA.Fill(sv_oDS, "Sys_Groups")
            If sv_oDS.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Kiểm tra sự tồn tại của một UserName trong CSDL
    'Đầu vào          :UserName
    'Đầu ra            :Tồn tại=True. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function bIsExistedGroupWithName(ByVal GroupID As Integer, ByVal GroupName As String, ByVal pv_sBranchID As String) As Boolean
        Dim sv_oDS As New DataSet
        Dim sv_oDA As SqlDataAdapter
        Try
            mv_sSql = "SELECT * from Sys_Groups WHERE upper(GroupName) =N'" & GroupName.ToUpper & "'" & " AND BranchID=N'" & pv_sBranchID & "' AND GroupID<>" & GroupID
            sv_oDA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oDA.Fill(sv_oDS, "Sys_Groups")
            If sv_oDS.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Kiểm tra sự tồn tại của một Admin trong CSDL
    'Đầu vào          :UserName của Admin
    'Đầu ra            :Tồn tại=True. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function bIsExistedAmin(ByVal pv_sUID As String) As Boolean
        Dim sv_oDS As New DataSet
        Dim sv_oDA As SqlDataAdapter
        Try

            mv_sSql = "SELECT * from Sys_Administrator WHERE PK_sAdminID =N'" & pv_sUID & "'" & " AND FP_sBranchID=N'" & gv_sBranchID & "'"
            sv_oDA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)

            sv_oDA.Fill(sv_oDS, "Sys_Administrator")

            If sv_oDS.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            AppLogger.NLogAction.Log.Trace("Check bIsExistedAmin.Exception-->" + ex.Message)
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Thiết lập mức phân quyền cho User(Tất cả các quyền=1. Theo quyền chọn trên cây:=0)
    'Đầu vào          :UserName, Mức phân quyền
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Sub UpdateSecurity(ByVal pv_sUID As String, ByVal pv_iSecurityLevel As Byte)
        Dim sv_oCmd As SqlCommand
        Try
            mv_sSql = "UPDATE Sys_Users set iSecurityLevel =" & pv_iSecurityLevel & " WHERE upper(PK_sUID) =N'" & pv_sUID & "'" & " AND FP_sBranchID=N'" & gv_sBranchID & "'"
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
        Catch ex As Exception
        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Thiết lập mức phân quyền cho User(Tất cả các quyền=1. Theo quyền chọn trên cây:=0)
    'Đầu vào          :UserName, Mức phân quyền
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Sub UpdateSecurityOfGroup(ByVal pv_GroupID As Integer, ByVal pv_iSecurityLevel As Byte)
        Dim sv_oCmd As SqlCommand
        Try
            mv_sSql = "UPDATE Sys_Groups set IsAdmin =" & pv_iSecurityLevel & " WHERE GroupID=" & pv_GroupID & " AND BranchID=N'" & gv_sBranchID & "'"
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
        Catch ex As Exception
        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Thiết lập mức phân quyền cho User(Tất cả các quyền=1. Theo quyền chọn trên cây:=0)
    'Đầu vào          :UserName, Mức phân quyền
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Sub UpdateSecurityForGroup(ByVal pv_GroupID As Integer, ByVal pv_iSecurityLevel As Byte)
        Dim sv_oCmd As SqlCommand
        Try
            mv_sSql = "UPDATE Sys_Groups set IsAdmin =" & pv_iSecurityLevel & " WHERE GroupID=" & pv_GroupID & " AND BranchID=N'" & gv_sBranchID & "'"
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
        Catch ex As Exception
        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Lấy về mức phân quyền của User
    'Đầu vào          :UserName
    'Đầu ra            :Mức phân quyền
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function iGetSecurityLevel(ByVal pv_sUID As String) As Integer
        Dim sv_oDS As New DataSet
        Dim sv_oDA As SqlDataAdapter
        mv_sSql = "SELECT iSecurityLevel from Sys_Users  WHERE upper(PK_sUID)=N'" & pv_sUID & "'  AND FP_sBranchID=N'" & gv_sBranchID & "'"
        Try
            sv_oDA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oDA.Fill(sv_oDS, "Sys_Users")
            If sv_oDS.Tables(0).Rows.Count > 0 Then
                Return sv_oDS.Tables(0).Rows(0)("iSecurityLevel")
            Else
                Return 0
            End If
        Catch ex As Exception
            Return 0
        End Try
    End Function

    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Lấy về mức phân quyền của User
    'Đầu vào          :UserName
    'Đầu ra            :Mức phân quyền
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function iGetSecurityLevelOfGroup(ByVal GroupID As Integer) As Integer
        Dim sv_oDS As New DataSet
        Dim sv_oDA As SqlDataAdapter
        mv_sSql = "SELECT IsAdmin from Sys_Groups  WHERE GroupID=" & GroupID & "'  AND BranchID=N'" & gv_sBranchID & "'"
        Try
            sv_oDA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oDA.Fill(sv_oDS, "Sys_Groups")
            If sv_oDS.Tables(0).Rows.Count > 0 Then
                Return sv_oDS.Tables(0).Rows(0)("IsAdmin")
            Else
                Return 0
            End If
        Catch ex As Exception
            Return 0
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Kiểm tra sự tồn tại của một Account Admin trong CSDL
    'Đầu vào          :UserName của Admin, Password
    'Đầu ra            :Thành công=True. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function bLoginSuccessAdmin(ByVal pv_sUID As String, ByVal pv_sPWD As String) As Boolean
        Dim sv_oDS As New DataSet
        Dim sv_oDA As SqlDataAdapter
        mv_sSql = "SELECT * from Sys_Administrator  WHERE PK_sAdminID=N'" & pv_sUID & "' AND sPWD=N'" & pv_sPWD & "'" & " AND FP_sBranchID=N'" & gv_sBranchID & "'"
        Try
            sv_oDA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oDA.Fill(sv_oDS, "Sys_Administrator")
            If sv_oDS.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
