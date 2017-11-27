'-------------------------------------------------------------------------------------------------------
'Mục đích: Lớp Role nhằm xử lý tất cả các nghiệp vụ liên quan đến Role như: Thêm mới Role, Sửa Role, Xóa Role,
'Gắn chức năng cho Role,........................
'Người tạo: CuongDV
'Ngày tạo :09/03/2005
'-------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient
Public Class clsRole
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
    'Mục đích        : Thêm mới một Role vào CSDL
    'Đầu vào          :Thông tin Role
    'Đầu ra            :Thành công=True. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function InsertRole(ByVal pv_iParentRoleID As Integer, ByVal pv_sRoleName As String, ByVal pv_sEngRoleName As String, _
                                            ByVal pv_iOrder As Integer, ByVal pv_intSCK As Integer, ByVal pv_sIconPath As String, ByVal pv_sDesc As String, ByVal pv_sparameterList As String, ByVal IsTabview As Integer, ByVal IsMultiview As Integer, ByVal _isEnabled As Integer) As Boolean
        Dim sv_oCmd As SqlCommand
        mv_sSql = "INSERT INTO Sys_ROLES(iParentRole,FP_sBranchID,sRoleName,sEngRoleName,iOrder,intShortCutKey,sIconPath,sParameterList,isTabView,isMultiview,bEnabled,sDesc) VALUES(" & _
                        pv_iParentRoleID & ",N'" & gv_sBranchID & "',N'" & pv_sRoleName & "',N'" & pv_sEngRoleName & "'," & pv_iOrder & "," & pv_intSCK & ",N'" & pv_sIconPath & "',N'" & pv_sparameterList & "'," & IsTabview & "," & IsMultiview & "," & _isEnabled & ",N'" & pv_sDesc & "')"
        Try
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function InsertRoleFromXML(ByVal pv_iParentRoleID As Integer, ByVal pv_sBranchID As String, ByVal pv_sRoleName As String, ByVal pv_sEngRoleName As String, _
                                           ByVal pv_iOrder As Integer, ByVal pv_intFunctionID As String, ByVal pv_intSCK As Integer, ByVal pv_sIconPath As String, ByVal pv_sImgPath As String, ByVal pv_sDesc As String) As Boolean
        Dim sv_oCmd As SqlCommand
        mv_sSql = "INSERT INTO Sys_ROLES(iParentRole,FP_sBranchID,sRoleName,sEngRoleName,iOrder,FK_iFunctionID,intShortCutKey,sIconPath,sImgPath,sDesc) VALUES(" & _
                        pv_iParentRoleID & ",N'" & pv_sBranchID & "',N'" & pv_sRoleName & "',N'" & pv_sEngRoleName & "'," & pv_iOrder & "," & pv_intFunctionID & "," & pv_intSCK & ",N'" & pv_sIconPath & "',N'" & pv_sImgPath & "',N'" & pv_sDesc & "')"
        Try
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Thêm mới một Role Phân hệ vào CSDL
    'Đầu vào          :Thông tin 
    'Đầu ra            :Thành công=True. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function InsertSubSystemRole(ByVal pv_sSubSystemName As String, ByVal pv_sEngRoleName As String, ByVal pv_iOrder As Integer, ByVal pv_intSCK As Integer, ByVal pv_sIconPath As String, ByVal pv_sImgPath As String, ByVal pv_sDesc As String, ByVal IsTabview As Integer, ByVal IsMultiview As Integer, ByVal _isEnabled As Integer) As Boolean
        Dim sv_oCmd As SqlCommand
        mv_sSql = "INSERT INTO Sys_ROLES(iParentRole,FP_sBranchID,sRoleName,sEngRoleName,iOrder,intShortCutKey,sIconPath,sImgPath,sDesc,isTabView,isMultiview,bEnabled) VALUES(-2,N'" & gv_sBranchID & "',N'" & _
                          pv_sSubSystemName & "',N'" & pv_sEngRoleName & "'," & pv_iOrder & "," & pv_intSCK & ",N'" & pv_sIconPath & "',N'" & pv_sImgPath & "',N'" & pv_sDesc & "'," & IsTabview & "," & IsMultiview & "," & _isEnabled & ")"
        Try
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Thêm mới một Role Menu cấp 1 vào CSDL
    'Đầu vào          :Thông tin
    'Đầu ra            :Thành công=True. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function InsertMenuLevel1Role(ByVal pv_sSubSystemName As String, ByVal pv_iOrder As Integer, ByVal pv_sDesc As String) As Boolean
        Dim sv_oCmd As SqlCommand
        mv_sSql = "INSERT INTO Sys_ROLES(iParentRole,FP_sBranchID,sRoleName,iOrder,sDesc) VALUES(-1,N'" & gv_sBranchID & "',N'" & _
                          pv_sSubSystemName & "'," & pv_iOrder & ",N'" & pv_sDesc & "')"
        Try
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Cập nhật một Role trong CSDL
    'Đầu vào          :Thông tin
    'Đầu ra            :Thành công=True. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function UpdateRole(ByVal pv_iRoleID As Integer, ByVal pv_sRoleName As String, ByVal pv_sEngRoleName As String, _
                                          ByVal pv_iOrder As Integer, ByVal pv_intSCK As Integer, ByVal pv_sIconPath As String, ByVal pv_sDesc As String, ByVal pv_sparameterList As String, ByVal IsTabview As Integer, ByVal IsMultiview As Integer, ByVal _isEnabled As Integer) As Boolean
        Dim sv_oCmd As SqlCommand
        mv_sSql = "UPDATE Sys_ROLES SET sEngRoleName=N'" & pv_sEngRoleName & "',isTabview=" & IsTabview & ",sParameterList=N'" & pv_sparameterList & "',isMultiview=" & IsMultiview & ",bEnabled=" & _isEnabled & ", sRoleName = N'" & pv_sRoleName & "'," & _
                         "sDesc=N'" & pv_sDesc & "', intShortCutKey=" & pv_intSCK & ",sIconPath=N'" & pv_sIconPath & "' WHERE iRole=" & pv_iRoleID & " AND FP_sBranchID=N'" & gv_sBranchID & "'"
        Try
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function UpdateRoleFromXML(ByVal pv_iRoleID As Integer, ByVal pv_intParentRole As Integer, ByVal pv_sRoleName As String, ByVal pv_sEngRoleName As String, _
                                         ByVal pv_iOrder As Integer, ByVal pv_intFuntionID As Integer, ByVal pv_intSCK As Integer, ByVal pv_sIconPath As String, ByVal pv_sImgPath As String, ByVal pv_sDesc As String) As Boolean
        Dim sv_oCmd As SqlCommand
        mv_sSql = "UPDATE Sys_ROLES SET sEngRoleName=N'" & pv_sEngRoleName & "', sRoleName = N'" & pv_sRoleName & "'," & _
                         "sDesc=N'" & pv_sDesc & "', intShortCutKey=" & pv_intSCK & ",sIconPath=N'" & pv_sIconPath & "' WHERE iRole=" & pv_iRoleID & " AND FP_sBranchID=N'" & gv_sBranchID & "'"
        Try
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Cập nhật mã chức năng cho mỗi Role trong CSDL
    'Đầu vào          :Mã Role, Mã chức năng
    'Đầu ra            :Thành công=True. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function bUpdateFunctionForRole(ByVal pv_iRoleID As Integer, ByVal pv_iFunctionID As Integer) As Boolean
        Dim sv_oCmd As SqlCommand
        mv_sSql = "UPDATE Sys_ROLES SET FK_iFunctionID=" & pv_iFunctionID & " WHERE iRole=" & pv_iRoleID & " AND FP_sBranchID=N'" & gv_sBranchID & "'"
        Try
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Xóa một Role trong CSDL
    'Đầu vào          :RoleID
    'Đầu ra            :Xóa thành công=True. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Sub DeleteRole(ByVal pv_iRoleID As Integer)
        Dim sv_oCmd As SqlCommand
        Dim sv_DR() As DataRow

        mv_sSql = "DELETE FROM  Sys_ROLES  WHERE iRole=" & pv_iRoleID & " AND FP_sBranchID=N'" & gv_sBranchID & "'"
        Try
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            'Xóa Role trong DataSet
            For Each dr As DataRow In gv_dsRole.Tables(0).Rows
                If dr("iRole") = pv_iRoleID Then
                    gv_dsRole.Tables(0).Rows.Remove(dr)
                    Exit For
                End If
            Next
_Continue:
            For Each dr As DataRow In gv_dsRolesForUsers.Tables(0).Rows
                If dr("iRoleID") = pv_iRoleID Then
                    gv_dsRolesForUsers.Tables(0).Rows.Remove(dr)
                    GoTo _Continue
                End If
            Next
            gv_dsRole.Tables(0).AcceptChanges()
            gv_dsRolesForUsers.Tables(0).AcceptChanges()
            'Xóa tất cả các Role con của Role bị xóa
            sv_DR = gv_dsRole.Tables(0).Select("iParentRole=" & pv_iRoleID)
            For Each dr As DataRow In sv_DR
                DeleteRole(CInt(dr("iRole")))
            Next
        Catch ex As Exception

        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Lấy thông tin của một Role trong CSDL
    'Đầu vào          :Mã Role(RoleID)
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function dsGetRoleInfor(ByVal pv_iRoleID As Integer) As DataSet
        Dim sv_DS As New DataSet
        Dim sv_DA As SqlDataAdapter
        mv_sSql = "SELECT * from Sys_ROLES WHERE iRole=" & pv_iRoleID & " AND FP_sBranchID=N'" & gv_sBranchID & "'"
        Try
            sv_DA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_DA.Fill(sv_DS, "Sys_ROLES")
            Return sv_DS
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Lấy tất cả các Role trong CSDL
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function dsGetAllRoleOfGroup() As DataSet
        Dim sv_DS As New DataSet
        Dim sv_DA As SqlDataAdapter
        mv_sSql = "SELECT 'F' AS CHON, A.GroupID as GroupID,A.RoleID as iRoleID,A.ParentRoleID as iParentRoleID,A.BranchID,B.iOrder," & _
                        "B.sRoleName as sRoleName,C.sDLLName as sDLLName, B.sParameterList," & _
                        "C.sFunctionName as sFunctionName,C.sFormName as sFormName" & _
                        " from Sys_GroupRoles A inner join Sys_ROLES B on A.RoleID=B.iRole left join " & _
                        "Sys_FUNCTIONS C on B.FK_iFunctionID =C.PK_iID" & _
                        " WHERE  A.BranchID=N'" & gv_sBranchID & "'" & _
                        " AND B.FP_sBranchID=N'" & gv_sBranchID & "'" & _
                        " AND C.FP_sBranchID=N'" & gv_sBranchID & "'" & _
                        " ORDER BY B.iORDER,A.RoleID,A.ParentRoleID ASC "
        Try
            sv_DA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_DA.Fill(sv_DS, "Sys_GroupRoles")
            Return sv_DS
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Lấy tất cả các Role trong CSDL
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function dsGetAllRoleOfUser() As DataSet
        Dim sv_DS As New DataSet
        Dim sv_DA As SqlDataAdapter
        ' Dim sql As String
        mv_sSql = String.Format(" SELECT 'F' AS CHON,B.sParameterList,A.sUID AS sUID,A.iRoleID AS iROLEID,A.iParentRoleID AS iParentRoleID,A.FP_sBranchID,B.iOrder,B.sRoleName AS sRoleName,C.sDLLName AS sDLLName,C.sFunctionName AS sFunctionName,C.sFormName AS sFormName FROM   Sys_ROLESFORUSERS A JOIN Sys_ROLES B ON  A.iRoleID = B.iRole LEFT JOIN Sys_FUNCTIONS C ON  B.FK_iFunctionID = C.PK_iID WHERE  A.FP_sBranchID = '{0}' AND B.FP_sBranchID = '{0}' ORDER BY B.iORDER,A.iRoleID,A.iParentRoleID Asc", gv_sBranchID)
        Try
            sv_DA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)

            sv_DA.Fill(sv_DS, "Sys_RFU")
            Return sv_DS
        Catch ex As Exception
            AppLogger.NLogAction.Log.Trace("dsGetAllRoleOfUser.Exception-->" + ex.Message)
            Return Nothing
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Lấy về tất cả các Role của một User trong CSDL
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function dsGetAllRoleOfUser(ByVal pv_sUID As String) As DataSet
        Dim sv_DS As New DataSet
        Dim sv_DA As SqlDataAdapter
        mv_sSql = "SELECT A.sUID as sUID,A.iRoleID as iROLEID,A.iParentRoleID as iParentRoleID," & _
                        "B.sRoleName as sRoleName,C.sDLLName as sDLLName," & _
                        "C.sFunctionName as sFunctionName,B.sParameterList,C.sFormName as sFormName" & _
                         " from Sys_ROLESFORUSERS A inner join Sys_ROLES B on A.iRoleID=B.iRole left join " & _
                        "Sys_FUNCTIONS C ON  B.FK_iFunctionID =C.PK_iID " & _
                        " WHERE     A.sUID='" & pv_sUID & "' AND A.FP_sBranchID=N'" & gv_sBranchID & "' AND B.FP_sBranchID=N'" & gv_sBranchID & "' AND C.FP_sBranchID=N'" & gv_sBranchID & "' ORDER BY A.iParentRoleID ASC "
        Try
            sv_DA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_DA.Fill(sv_DS, "Sys_RFU")
            Return sv_DS
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Lấy về mã Role lớn nhất hiện thời trong CSDL
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function iGetNewestRole() As Integer
        Dim sv_ds As New DataSet
        Dim sv_DA As SqlDataAdapter
        mv_sSql = "SELECT max(iRole) from Sys_ROLES WHERE  FP_sBranchID=N'" & gv_sBranchID & "'"
        Try
            sv_DA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_DA.Fill(sv_ds, "Sys_ROLES")
            Return CInt(sv_ds.Tables(0).Rows(0)(0))
        Catch ex As Exception
            Return -1
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Thêm một Role cho một User trong CSDL
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function bAddRoleForUser(ByVal pv_sUID As String, ByVal pv_iRole As Integer, ByVal pv_iParentRole As Integer, ByVal pv_sBranchID As String) As Boolean
        Dim sv_oCmd As SqlCommand
        Try
            If bIsExistedRoleForUser(pv_sUID, pv_iRole, pv_sBranchID) Then
                Return False
            Else
                If bIsExistedRoleinTableRole(pv_iRole, pv_sBranchID) Then
                    mv_sSql = "INSERT INTO Sys_ROLESFORUSERS VALUES(N'" & pv_sUID & "'," & pv_iRole & "," & pv_iParentRole & ",N'" & pv_sBranchID & "')"
                    sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
                    'sv_oCmd.Transaction = gv_objTrans
                    sv_oCmd.ExecuteNonQuery()
                    sv_oCmd = Nothing
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            'gv_objTrans.Rollback()
            'Trong trường hợp User đã tồn tại quyền này 
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Thêm một Role cho một User trong CSDL
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function bAddRoleForGroupUser(ByVal GroupID As Integer, ByVal pv_iRole As Integer, ByVal pv_iParentRole As Integer, ByVal pv_sBranchID As String) As Boolean
        Dim sv_oCmd As SqlCommand
        Try
            If bIsExistedRoleForGroupUser(GroupID, pv_iRole, pv_sBranchID) Then
                Return False
            Else
                If bIsExistedRoleinTableRole(pv_iRole, pv_sBranchID) Then
                    mv_sSql = "INSERT INTO Sys_GROUPROLES(GroupID,RoleID,ParentRoleID,BranchID) VALUES(" & GroupID & "," & pv_iRole & "," & pv_iParentRole & ",N'" & pv_sBranchID & "')"
                    sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
                    'sv_oCmd.Transaction = gv_objTrans
                    sv_oCmd.ExecuteNonQuery()
                    sv_oCmd = Nothing
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            'gv_objTrans.Rollback()
            'Trong trường hợp User đã tồn tại quyền này 
            Return False
        End Try
    End Function
    Public Function bIsExistedRoleinTableRole(ByVal pv_iRole As Integer, ByVal pv_sBranchID As String) As Boolean
        Dim sv_oCmd As SqlCommand
        Dim DA As SqlDataAdapter
        Dim DS As New DataSet
        Try
            mv_sSql = "SELECT * from Sys_ROLES WHERE iROLE=" & pv_iRole & " AND FP_sBranchID=N'" & pv_sBranchID & "'"
            DA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            DA.Fill(DS, "Sys_ROLES")
            If DS.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function bIsExistedRoleForUser(ByVal pv_sUID As String, ByVal pv_iRole As Integer, ByVal pv_sBranchID As String) As Boolean
        Dim sv_oCmd As SqlCommand
        Dim DA As SqlDataAdapter
        Dim DS As New DataSet
        Try
            mv_sSql = "SELECT * from Sys_ROLESFORUSERS WHERE sUID=N'" & pv_sUID & "' AND iROLEID=" & pv_iRole & " AND FP_sBranchID=N'" & pv_sBranchID & "'"
            DA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            DA.Fill(DS, "Sys_ROLESFORUSERS")
            If DS.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function bIsExistedRoleForGroupUser(ByVal pv_GroupID As Integer, ByVal pv_iRole As Integer, ByVal pv_sBranchID As String) As Boolean
        Dim sv_oCmd As SqlCommand
        Dim DA As SqlDataAdapter
        Dim DS As New DataSet
        Try
            mv_sSql = "SELECT * from Sys_GroupRoles WHERE GroupID=" & pv_GroupID & " AND ROLEID=" & pv_iRole & " AND BranchID=N'" & pv_sBranchID & "'"
            DA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            DA.Fill(DS, "Sys_GroupRoles")
            If DS.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Cập nhật lại thứ tự cho một Role
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Sub UpdateOrder1(ByVal pv_iRole As Integer, ByVal pv_iOrder As Integer)
        Dim sv_oCmd As SqlCommand
        Try
            mv_sSql = "UPDATE Sys_ROLES SET iOrder=" & pv_iOrder & " WHERE iRole=" & pv_iRole & " AND FP_sBranchID=N'" & gv_sBranchID & "'"
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()

        Catch ex As Exception
        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Thêm mới hoặc cập nhật lại Role sau khi thực hiện kéo thả trên TreeView
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Sub UpdateAfterDragAndDrop(ByVal pv_iRole As Integer, ByVal pv_iDesParentRoleID As Integer, ByVal pv_iOrder As Integer, ByVal pv_DR As DataRow, ByVal pv_bCut As Boolean)
        Dim sv_oCmd As SqlCommand
        Try
            If pv_bCut Then  'Kiểm tra nếu là cut Role thì cập nhật lại mã cha và thứ tự
                mv_sSql = "UPDATE Sys_ROLES SET iOrder=" & pv_iOrder & ",iParentRole=" & pv_iDesParentRoleID & "  WHERE iRole=" & pv_iRole & " AND FP_sBranchID=N'" & gv_sBranchID & "'"
                sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
                sv_oCmd.ExecuteNonQuery()
                mv_sSql = "UPDATE SysRolesForUsers SET iParentRoleID=" & pv_iDesParentRoleID & "  WHERE iRoleID=" & pv_iRole & " AND FP_sBranchID=N'" & gv_sBranchID & "'"
                sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
                sv_oCmd.ExecuteNonQuery()
                Return
            Else 'Nếu là Copy Role thì thêm mới vào CSDL
                'Lấy về thông tin Role cũ
                ' Dim sv_DS As New DataSet
                ' sv_DS = dsGetRoleInfor(pv_iRole)
                ' If sv_DS.Tables(0).Rows.Count > 0 Then
                mv_sSql = "INSERT INTO Sys_ROLES(iParentRole,FP_sBranchID,sRoleName,sEngRoleName,iOrder,sDesc,FK_iFunctionID,sImgPath,sIconPath,intShortCutKey,isTabView,sParameterList,isMultiview,bEnabled) VALUES(" & _
                                  pv_iDesParentRoleID & ",N'" & gv_sBranchID & "',N'" & pv_DR("sRoleName") & "',N'" & _
                                  pv_DR("sEngRoleName") & "'," & pv_iOrder & ",N'" & pv_DR("sDesc") & _
                                  "'," & pv_DR("FK_iFunctionID") & ",N'" & pv_DR("sImgPath") & "',N'" & pv_DR("sIconPath") & "'," & pv_DR("intShortCutKey") & "," & pv_DR("isTabView") & ",N'" & pv_DR("sParameterList") & "'," & pv_DR("isMultiview") & "," & pv_DR("bEnabled") & " )"
                'mv_sSql = "INSERT INTO Sys_ROLES(iParentRole,FP_sBranchID,sRoleName,sEngRoleName,iOrder,sDesc,FK_iFunctionID,tDateCreated,sImgPath,sIconPath,intShortCutKey) VALUES(" & _
                '  pv_iDesParentRoleID & ",N'" & gv_sBranchID & "',N'" & pv_DR("sRoleName") & "',N'" & _
                '  pv_DR("sEngRoleName") & "'," & pv_iOrder & ",N'" & pv_DR("sDesc") & _
                '  "'," & pv_DR("FK_iFunctionID") & ",Format('" & Now.ToShortDateString & "','dd/MM/yyyy'),N'" & pv_DR("sImgPath") & "',N'" & pv_DR("sIconPath") & "'," & pv_DR("intShortCutKey") & " )"
                '---------------------------------------------------------------------------------------------
                sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
                sv_oCmd.ExecuteNonQuery()
                'Thêm vào DataSet Role mới
                Dim sv_oDR As DataRow
                sv_oDR = gv_dsRole.Tables(0).NewRow
                With sv_oDR
                    .Item("iRole") = iGetNewestRole()
                    .Item("iParentRole") = pv_iDesParentRoleID
                    .Item("sRoleName") = pv_DR("sRoleName")
                    .Item("sEngRoleName") = pv_DR("sEngRoleName")
                    .Item("sImgPath") = pv_DR("sImgPath")
                    .Item("sIconPath") = pv_DR("sIconPath")
                    .Item("intShortCutKey") = pv_DR("intShortCutKey")
                    .Item("iOrder") = pv_iOrder
                    .Item("sDesc") = pv_DR("sDesc")
                    .Item("FK_iFunctionID") = pv_DR("FK_iFunctionID")
                    .Item("sFunctionName") = pv_DR("sFunctionName")
                    .Item("sDLLName") = pv_DR("sDLLName")
                    .Item("isTabView") = pv_DR("isTabView")
                    .Item("sParameterList") = pv_DR("sParameterList")
                    .Item("isMultiview") = pv_DR("isMultiview")
                    .Item("bEnabled") = pv_DR("bEnabled")
                    .Item("sFormName") = pv_DR("sFormName")
                End With
                gv_dsRole.Tables(0).Rows.Add(sv_oDR)
                gv_dsRole.Tables(0).AcceptChanges()
                Return
                ' End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Cập nhật đường dẫn ảnh cho mỗi phân hệ
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function bUpdateImgPath(ByVal pv_iRoleID As Integer, ByVal pv_sFieldName As String, ByVal pv_sValue As String) As Boolean
        Dim sv_oCmd As SqlCommand
        Try
            mv_sSql = "UPDATE Sys_ROLES SET " & pv_sFieldName & "=N'" & pv_sValue & "' WHERE iRole=" & pv_iRoleID & " AND FP_sBranchID=N'" & gv_sBranchID & "'"
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Cập nhật đường dẫn ảnh cho mỗi phân hệ
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :20/03/2006
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function bUpdateField(ByVal pv_sTableName As String, ByVal pv_sSetValueForField As String, ByVal pv_sCondition As String) As Boolean
        Dim sv_oCmd As SqlCommand
        Try
            mv_sSql = "UPDATE " & pv_sTableName & "  SET " & pv_sSetValueForField & "  WHERE " & pv_sCondition & " AND FP_sBranchID=N'" & gv_sBranchID & "'"
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            'Trong trường hợp User đã tồn tại quyền này 
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
    Public Function bDeleteRoleOfUser(ByVal pv_sUID As String, ByVal pv_iRoleID As Integer) As Boolean
        Dim sv_oCmd As SqlCommand
        Try
            mv_sSql = "DELETE from Sys_ROLESFORUSERS WHERE sUID=N'" & pv_sUID & "' AND iRoleID=" & pv_iRoleID & " AND FP_sBranchID=N'" & gv_sBranchID & "'"
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            'sv_oCmd.Transaction = gv_objTrans
            sv_oCmd.ExecuteNonQuery()
            sv_oCmd = Nothing
            Return True
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
    Public Function bDeleteRoleOfGroupUser(ByVal pv_GroupID As Integer, ByVal pv_iRoleID As Integer) As Boolean
        Dim sv_oCmd As SqlCommand
        Try
            mv_sSql = "DELETE from Sys_GROUPROLES WHERE GROUPID=" & pv_GroupID & " AND RoleID=" & pv_iRoleID & " AND BranchID=N'" & gv_sBranchID & "'"
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            'sv_oCmd.Transaction = gv_objTrans
            sv_oCmd.ExecuteNonQuery()
            sv_oCmd = Nothing
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function bIsexited(ByVal pv_intRoleID As Integer) As Boolean
        Dim sv_ds As New DataSet
        Dim sv_DA As SqlDataAdapter
        Try
            mv_sSql = "SELECT * from Sys_ROLES WHERE iRole=" & pv_intRoleID & " AND FP_sBranchID=N'" & gv_sBranchID & "'"
            sv_DA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_DA.Fill(sv_ds, "Sys_ROLES")
            If sv_ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
