Imports System.Data.SqlClient
Public Class cls_Parameter
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
    'Mục đích        : Thêm mới, sửa, xóa một parm trong CSDL
    'Đầu vào          :Thông tin chức năng
    'Đầu ra            :Thành công=True. Không thành công=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function bAddNew(ByVal pv_ID As Integer, ByVal pv_sBranchID As String, ByVal pv_sName As String, ByVal pv_sValue As String, _
                                            ByVal pv_sDataType As String, ByVal pv_sDesc As String, _
                                            ByVal pv_iMonth As Integer, ByVal pv_iYear As Integer, _
                                            Optional ByVal pv_iStatus As Integer = 1, Optional ByVal pv_sOldName As String = "OPTIONAL") As Boolean
        Try
            Select Case pv_iStatus
                Case -1 'Delete
                    mv_sSql = "DELETE from Sys_SystemParameters  WHERE sName=N'" & pv_sName & "' AND FP_sBranchID=N'" & gv_sBranchID & "'"
                Case 0 'Update

                    mv_sSql = "UPDATE Sys_SystemParameters SET sName=N'" & pv_sName & "',"
                    mv_sSql &= "sValue=N'" & pv_sValue & "',sDataType=N'" & pv_sDataType & "',"
                    mv_sSql &= " sDesc=N'" & pv_sDesc & "' WHERE sName=N'" & pv_sOldName & "' AND FP_sBranchID=N'" & gv_sBranchID & "'"
                Case 1 'Insert
                    mv_sSql = "INSERT INTO Sys_SystemParameters VALUES(N'" & pv_sBranchID & "',N'"
                    mv_sSql &= pv_sName.ToUpper & "',N'" & pv_sDataType & "',N'"
                    mv_sSql &= pv_sValue & "'," & pv_iMonth & ","
                    mv_sSql &= pv_iYear & ",1,N'" & pv_sDesc & "')"

            End Select
            Dim sv_oCmd As SqlCommand
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            MessageBox.Show("Lỗi thao tác Tham số chức năng: " & ex.ToString, gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End Try
    End Function
    Public Function bUpdate(ByVal pv_sBranchID As String, ByVal pv_sName As String, ByVal pv_sValue As String, _
                                            ByVal pv_sDataType As String, ByVal pv_sDesc As String, _
                                            ByVal pv_iMonth As Integer, ByVal pv_iYear As Integer, _
                                            Optional ByVal pv_iStatus As Integer = 1) As Boolean
        Try
            mv_sSql = "UPDATE Sys_SystemParameters SET iStatus=" & pv_iStatus & ","
            mv_sSql &= "sValue=N'" & pv_sValue & "',sDataType=N'" & pv_sDataType & "',"
            mv_sSql &= " sDesc=N'" & pv_sDesc & "' WHERE sName=N'" & pv_sName & "' AND FP_sBranchID=N'" & pv_sBranchID & "'"
            Dim sv_oCmd As SqlCommand
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Lấy về danh sách tất cả các tham số từ CSDL
    'Đầu vào          :
    'Đầu ra            :Danh sách các Tham số
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function dsGetAllParams() As DataSet
        Dim ds As New DataSet
        Dim da As SqlDataAdapter
        Try
            mv_sSql = "SELECT 'F' AS Chon,ID,FP_sBranchID,sName,sDataType,sValue,iMonth,iYear,iStatus,sDesc from Sys_SystemParameters WHERE  FP_sBranchID=N'" & gv_sBranchID & "' Order By sName"
            da = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            da.Fill(ds, "Sys_Params")
            Return ds
        Catch ex As Exception
        End Try
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Lấy về thông tin của một tham số trong CSDL
    'Đầu vào          :
    'Đầu ra            :Thông tin tham số
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function dsGetParamInfor(ByVal pv_sName As String) As DataSet
        Dim ds As New DataSet
        Dim da As SqlDataAdapter
        Try
            mv_sSql = "SELECT * from Sys_SystemParameters WHERE sName=N'" & pv_sName & "' AND FP_sBranchID=N'" & gv_sBranchID & "' Order By sName"
            da = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            da.Fill(ds, "Sys_SystemParameters")
            Return ds
        Catch ex As Exception
        End Try
    End Function

    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Kiểm tra sự tồn tại của một tham số trong CSDL để đảm bảo không có 2 tham số cùng tên trong một ứng dụng
    'Đầu vào          :Tên tham số
    'Đầu ra            :Tồn tại=True. Không tồn tại=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function bIsexited(ByVal pv_sParamName As String, ByVal pv_sBranchID As String) As Boolean
        Dim sv_ds As New DataSet
        Dim sv_DA As SqlDataAdapter
        Try
            mv_sSql = "SELECT * from Sys_SystemParameters WHERE sName=N'" & pv_sParamName & "'" & " AND FP_sBranchID=N'" & pv_sBranchID & "'"
            sv_DA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_DA.Fill(sv_ds, "Sys_SystemParameters")
            If sv_ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function DeleteParam(ByVal pv_sParamName As String, ByVal pv_oNode As TreeNode) As Boolean
        Dim sv_oCmd As SqlCommand
        Dim Tran As SqlTransaction
        Tran = VNS.Libs.globalVariables.SqlConn.BeginTransaction
        mv_sSql = "DELETE FROM  Sys_SYSTEMPARAMETERS  WHERE sName=N'" & pv_sParamName & "' AND FP_sBranchID=N'" & gv_sBranchID & "'"
        Try
            sv_oCmd = VNS.Libs.globalVariables.SqlConn.CreateCommand
            sv_oCmd.Transaction = Tran
            sv_oCmd.CommandText = mv_sSql
            sv_oCmd.ExecuteNonQuery()
            pv_oNode.Remove()
            'Xóa trong DataSet
            For Each dr As DataRow In gv_dsParam.Tables(0).Rows
                If dr("sName").ToString.ToUpper = pv_sParamName.ToUpper Then
                    gv_dsParam.Tables(0).Rows.Remove(dr)
                    Exit For
                End If
            Next
            gv_dsParam.Tables(0).AcceptChanges()
            Tran.Commit()
            Return True
        Catch ex As Exception
            Tran.Rollback()
            Return False
        End Try
    End Function
    Public Function bUpdateParamStatus(ByVal pv_sName As String, ByVal pv_bActivate As Boolean) As Boolean
        Try
            mv_sSql = "UPDATE Sys_SystemParameters SET iStatus=" & IIf(pv_bActivate, 1, 0) & " WHERE sName=N'" & pv_sName & "' AND FP_sBranchID=N'" & gv_sBranchID & "'"
            Dim sv_oCmd As SqlCommand
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
