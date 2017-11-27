Imports System.Data.SqlClient
Public Class clsFunction
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
    'Mục đích        : Thêm mới, sửa, xóa một chức năng trong CSDL
    'Đầu vào          :Thông tin chức năng
    'Đầu ra            :Thành công=True. Không thành công=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function bAddNew(byval pv_sBranchID as String ,ByVal pv_ID As Integer, ByVal pv_sFunctionName As String, _
                                            ByVal pv_sDLLName As String, ByVal pv_sFormName As String, _
                                            ByVal pv_sAssemblyName As String, ByVal pv_bEnable As Boolean, _
                                            ByVal pv_sDesc As String, ByVal pv_bChangeName As Boolean, Optional ByVal pv_iStatus As Integer = 1) As Boolean
        Try
            Select Case pv_iStatus
                Case -1 'Delete
                    mv_sSql = "DELETE from Sys_FUNCTON  WHERE PK_iID=" & pv_ID & " AND FP_sBranchID=N'" & pv_sBranchID & "'"
                Case 0 'Update
                    If pv_bChangeName Then
                        If bIsexited(pv_sFunctionName, pv_sBranchID) Then
                            Return False
                        End If
                    End If
                    mv_sSql = "UPDATE Sys_FUNCTIONS SET sDLLName=N'" & pv_sDLLName & "',"
                    mv_sSql &= "sFormName=N'" & pv_sFormName & "',sFunctionName=N'" & pv_sFunctionName & "',"
                    mv_sSql &= "sAssemblyName=N'" & pv_sAssemblyName & "',bEnable=" & IIf(pv_bEnable, 1, 0) & ","
                    mv_sSql &= " sDesc=N'" & pv_sDesc & "' WHERE PK_iID=" & pv_ID & " AND FP_sBranchID=N'" & pv_sBranchID & "'"

                Case 1 'Insert
                    If bIsexited(pv_sFunctionName, pv_sBranchID) Then
                        Return False
                    End If
                    mv_sSql = "INSERT INTO Sys_FUNCTIONS VALUES(N'" & pv_sBranchID & "',N'"
                    mv_sSql &= pv_sFunctionName & "',N'" & pv_sDLLName & "',N'"
                    mv_sSql &= pv_sFormName & "',N'" & pv_sAssemblyName & "',"
                    mv_sSql &= IIf(pv_bEnable, 1, 0) & ",N'" & pv_sDesc & "')"

            End Select
            Dim sv_oCmd As SqlCommand
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function bUpdate(ByVal pv_sBranchID As String, ByVal pv_sFunctionName As String, _
                                           ByVal pv_sDLLName As String, ByVal pv_sFormName As String, _
                                           ByVal pv_sAssemblyName As String, ByVal pv_bEnable As Boolean, _
                                           ByVal pv_sParamList As String, _
                                           ByVal pv_sDesc As String) As Boolean
        Try

            mv_sSql = "UPDATE Sys_FUNCTIONS SET sDLLName=N'" & pv_sDLLName & "',"
            mv_sSql &= "sFormName=N'" & pv_sFormName & "',sFunctionName=N'" & pv_sFunctionName & "',"
            mv_sSql &= "sAssemblyName=N'" & pv_sAssemblyName & "',bEnable=" & IIf(pv_bEnable, 1, 0) & ","
            mv_sSql &= " sDesc=N'" & pv_sDesc & "' WHERE sFunctionName=N'" & pv_sFunctionName & "' AND FP_sBranchID=N'" & pv_sBranchID & "'"
            Dim sv_oCmd As SqlCommand
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Lấy về danh sách tất cả các chức năng từ CSDL
    'Đầu vào          :
    'Đầu ra            :Danh sách các Functions
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function dsGetAllFunction() As DataSet
        Dim ds As New DataSet
        Dim da As SqlDataAdapter
        Try
            mv_sSql = "SELECT 'F' AS CHON,PK_iID,FP_sBranchID,sFunctionName,sDLLname,sFormName,sAssemblyName,bEnable,sDesc from Sys_Functions WHERE  FP_sBranchID=N'" & gv_sBranchID & "' Order By PK_iID ASC"
            da = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            da.Fill(ds, "Sys_Functions")
            Return ds
        Catch ex As Exception
            AppLogger.NLogAction.Log.Trace("dsGetAllFunction.Exception-->" + ex.Message)
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Lấy về thông tin của một chức năng trong CSDL
    'Đầu vào          :
    'Đầu ra            :Thông tin Function
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function dsGetFunctionInfor(ByVal pv_iID As Integer) As DataSet
        Dim ds As New DataSet
        Dim da As SqlDataAdapter
        Try
            mv_sSql = "SELECT * from Sys_Functions WHERE PK_iID=" & pv_iID & " AND FP_sBranchID=N'" & gv_sBranchID & "' Order By PK_iID ASC"
            da = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            da.Fill(ds, "Sys_Functions")
            Return ds
        Catch ex As Exception
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Lấy về danh sách tất cả các Role trong CSDL
    'Đầu vào          :
    'Đầu ra            :Danh sách các Roles
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function dsGetAllRole() As DataSet
        Dim ds As New DataSet
        Dim da As SqlDataAdapter
        Try
            'Xắp xếp theo thứ tự tăng dần để đảm bảo rằng các Role phân hệ được nạp đầu tiên
            mv_sSql = "SELECT 'F' AS CHON,R.iRole,R.FP_sBranchID,R.iParentRole,R.sRoleName,R.sEngRoleName,R.iOrder,R.FK_iFunctionID,R.tDateCreated,R.sImgPath,R.sDesc,R.sIconPath,R.intShortCutKey, sDLLName,sFormName,sFunctionName,sAssemblyName,R.sParameterList,R.bEnabled,R.isMultiview,R.isTabView " & _
                         "from Sys_ROLES R left join  Sys_FUNCTIONS F  on R.FK_iFunctionID =F.PK_iID  ORDER BY iOrder,iParentRole,iRole"
            da = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            da.Fill(ds, "Sys_ROLES")
            Return ds
        Catch ex As Exception
            AppLogger.NLogAction.Log.Trace("dsGetAllRole.Exception-->" + ex.Message)
        End Try
    End Function
    Public Function dsGetAllRoleForOutIn() As DataSet
        Dim ds As New DataSet
        Dim da As SqlDataAdapter
        Try
            'Xắp xếp theo thứ tự tăng dần để đảm bảo rằng các Role phân hệ được nạp đầu tiên
            mv_sSql = "SELECT 'F' AS CHON,iRole,FP_sBranchID,iParentRole,sRoleName,sEngRoleName,iOrder,FK_iFunctionID,tDateCreated,sImgPath,sDesc,sIconPath,intShortCutKey from Sys_ROLES WHERE FP_sBranchID=N'" & gv_sBranchID & "'"
            da = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            da.Fill(ds, "Sys_ROLES")
            Return ds
        Catch ex As Exception
            AppLogger.NLogAction.Log.Trace("dsGetAllRoleForOutIn.Exception-->" + ex.Message)
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Kiểm tra sự tồn tại của một chức năng trong CSDL để đảm bảo không có 2 chức năng cùng tên trong một ứng dụng
    'Đầu vào          :Tên chức năng
    'Đầu ra            :Tồn tại=True. Không tồn tại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function bIsexited(ByVal pv_sFunctionName As String, ByVal pv_sBranchID As String) As Boolean
        Return False
        Dim sv_ds As New DataSet
        Dim sv_DA As SqlDataAdapter
        Try
            mv_sSql = "SELECT * from Sys_FUNCTIONS WHERE sFunctionName=N'" & pv_sFunctionName & "'" & " AND FP_sBranchID=N'" & pv_sBranchID & "'"
            sv_DA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_DA.Fill(sv_ds, "Sys_FUNCTIONS")
            If sv_ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Lấy về mã chức năng lớn nhất hiện thời
    'Đầu vào          :
    'Đầu ra            :Max(Mã chức năng)
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function GetBiggestID() As Integer
        Dim sv_ds As New DataSet
        Dim sv_DA As SqlDataAdapter
        Try
            mv_sSql = "SELECT max(PK_iID) from Sys_FUNCTIONS WHERE  FP_sBranchID=N'" & gv_sBranchID & "'"
            sv_DA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_DA.Fill(sv_ds, "Sys_FUNCTIONS")
            If sv_ds.Tables(0).Rows.Count > 0 Then
                Return sv_ds.Tables(0).Rows(0)(0)
            Else
                Return 0
            End If
        Catch ex As Exception
            Return -1
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Khóa hoặc kích hoạt một chức năng
    'Đầu vào          :Mã chức năng, trạng thái kích hoạt(Khóa=True; Kích hoạt=False)
    'Đầu ra            :Khóa(Kích hoath) thành công=True. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function bLockFunction(ByVal pv_iFunctionID As Integer, Optional ByVal pv_bLockOrUnlock As Boolean = True) As Boolean
        Dim sv_oCmd As SqlCommand
        mv_sSql = "UPDATE Sys_FUNCTIONS SET bEnable=" & IIf(pv_bLockOrUnlock, 0, 1) & " WHERE PK_iID=" & pv_iFunctionID & " AND FP_sBranchID=N'" & gv_sBranchID & "'"
        Try
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Xóa một Function trong CSDL
    'Đầu vào          :FunctionID
    'Đầu ra            :Xóa thành công=True. Ngược lại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :26/04/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function DeleteFunction(ByVal pv_iFunctionID As Integer, ByVal pv_oNode As TreeNode) As Boolean
        Dim sv_oCmd As SqlCommand
        Dim Tran As SqlTransaction
        Tran = VNS.Libs.globalVariables.SqlConn.BeginTransaction
        mv_sSql = "DELETE FROM  Sys_FUNCTIONS  WHERE PK_iID=" & pv_iFunctionID & " AND FP_sBranchID=N'" & gv_sBranchID & "'"
        Try
            sv_oCmd = VNS.Libs.globalVariables.SqlConn.CreateCommand
            sv_oCmd.Transaction = Tran
            sv_oCmd.CommandText = mv_sSql
            sv_oCmd.ExecuteNonQuery()
            mv_sSql = "UPDATE Sys_ROLES SET FK_iFunctionID=-1 WHERE FK_iFunctionID=" & pv_iFunctionID & "AND FP_sBranchID=N'" & gv_sBranchID & "'"
            sv_oCmd.CommandText = mv_sSql
            sv_oCmd.ExecuteNonQuery()
            pv_oNode.Remove()
            'Xóa trong DataSet
            For Each dr As DataRow In gv_dsFunction.Tables(0).Rows
                If CInt(dr("PK_iID")) = pv_iFunctionID Then
                    gv_dsFunction.Tables(0).Rows.Remove(dr)
                    Exit For
                End If
            Next
            'Cập nhật trong DataSet Roles
            For Each dr As DataRow In gv_dsRole.Tables(0).Rows
                If CInt(dr("FK_iFunctionID")) = pv_iFunctionID Then
                    dr("FK_iFunctionID") = -1
                    dr.Item("sFunctionName") = "Chưa gán"
                    dr.Item("sDLLName") = "Chưa gán"
                    dr.Item("sFormName") = "Chưa gán"
                End If
            Next
            gv_dsFunction.Tables(0).AcceptChanges()
            gv_dsRole.Tables(0).AcceptChanges()
            Tran.Commit()
            Return True
        Catch ex As Exception
            Tran.Rollback()
            Return False
        End Try
    End Function
End Class
