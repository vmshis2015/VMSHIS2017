

'-------------------------------------------------------------------------------------------------------
'Mục đích: Lớp User nhằm xử lý tất cả các nghiệp vụ liên quan đến người dùng
'Người tạo: CuongDV
'Ngày tạo :09/03/2005
'-------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient
Public Class clsTbrButton
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
    Public Function InsertButton(ByVal pv_sSubID As Integer, ByVal pv_sBranchID As String, ByVal pv_sText As String, ByVal pv_sIconPath As String, _
                                ByVal pv_sDesc As String, ByVal pv_sTTT As String, _
                                ByVal pv_intRolePerformed As Integer, ByVal pv_intOrder As Integer, _
                                ByVal pv_sName As String, ByVal pv_intStyle As Integer, _
                                ByVal pv_sRoleName As String, ByVal pv_intDisplayText As Integer) As Boolean
        Dim sv_oCmd As SqlCommand
        If Not bExistInsert(pv_sName) Then
            mv_sSql = "INSERT INTO Sys_TOOLBARBUTTON VALUES(" & pv_sSubID & "," & sUnicode(pv_sBranchID) & "," & sUnicode(pv_sText) & "," & sUnicode(pv_sIconPath) & "," & sUnicode(pv_sDesc) & "," & sUnicode(pv_sTTT) & "," & pv_intRolePerformed & "," & pv_intOrder & "," & sUnicode(pv_sName) & "," & pv_intStyle & "," & sUnicode(pv_sRoleName) & "," & pv_intDisplayText & ")"
        Else
            mv_sSql = "UPDATE Sys_TOOLBARBUTTON SET  FP_intRoleID=" & pv_sSubID & ",sText=" & sUnicode(pv_sText) & ",sIconPath=" & sUnicode(pv_sIconPath) & ",sDesc=" & sUnicode(pv_sDesc) & ",sTTT=" & sUnicode(pv_sTTT) & ",intRolePerformed=" & pv_intRolePerformed & ",intOrder=" & pv_intOrder & ",sName=" & sUnicode(pv_sName) & ",intStyle=" & pv_intStyle & ",sRoleName=" & sUnicode(pv_sRoleName) & ",intDisplayText" & pv_intDisplayText & " WHERE sName=" & sUnicode(pv_sName)
        End If
        Try
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function sUnicode(ByVal pv_sValue As String) As String
        Return "N'" & pv_sValue & "'"
    End Function
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :  Lấy thông tin người dùng
    'Đầu vào          :Mã người dùng
    'Đầu ra            :Thông tin người dùng
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function dsGetButton(Optional ByVal pv_bAllBtn As Boolean = True) As DataSet
        Dim sv_DS As New DataSet
        Dim sv_DA As SqlDataAdapter
        If pv_bAllBtn Then
            mv_sSql = "SELECT * FROM  Sys_TOOLBARBUTTON "
        Else
            mv_sSql = "SELECT * FROM  Sys_TOOLBARBUTTON WHERE FP_intRoleID= " & gv_intSubSysID
        End If
        Try
            sv_DA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_DA.Fill(sv_DS, "Sys_TOOLBARBUTTON")
            Return sv_DS
        Catch ex As Exception
            AppLogger.NLogAction.Log.Trace("dsGetButton.Exception-->" + ex.Message)
            Return Nothing
        End Try
    End Function
    Public Function UpdateButton(ByVal pv_sName As String, ByVal pv_sField As String, ByVal pv_sValue As String, Optional ByVal pv_bIntValue As Boolean = False, Optional ByVal pv_bUpdateSRoleName As Boolean = False, Optional ByVal pv_sRoleName As String = "") As Boolean
        Dim sv_oCmd As SqlCommand
        If pv_sField.ToUpper = "SNAME" Then
            If bExistUpdate(pv_sName, pv_sValue) Then
                MessageBox.Show("Đã tồn tại Nút có tên như vậy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If
        End If
        If pv_bUpdateSRoleName Then
            mv_sSql = "UPDATE Sys_TOOLBARBUTTON SET intRolePerformed=" & CInt(pv_sValue) & ", sRoleName =N'" & pv_sRoleName & "' WHERE sName=N'" & pv_sName & "' AND FP_intRoleID=" & gv_intSubSysID
        Else

            If pv_bIntValue Then
                mv_sSql = "UPDATE Sys_TOOLBARBUTTON SET " & pv_sField & "=" & CInt(pv_sValue) & " WHERE sName=N'" & pv_sName & "' AND FP_intRoleID=" & gv_intSubSysID
            Else
                mv_sSql = "UPDATE Sys_TOOLBARBUTTON SET " & pv_sField & "=N'" & pv_sValue & "' WHERE sName=N'" & pv_sName & "' AND FP_intRoleID=" & gv_intSubSysID
            End If
        End If
        Try
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function bExistInsert(ByVal pv_sName As String) As Boolean

        Dim sv_DS As New DataSet
        Dim sv_DA As SqlDataAdapter
        mv_sSql = "SELECT * from Sys_TOOLBARBUTTON WHERE sName=N'" & pv_sName & "' AND FP_intRoleID=" & gv_intSubSysID
        Try
            sv_DA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_DA.Fill(sv_DS, "Sys_TOOLBARBUTTON")
            If sv_DS.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function bExistUpdate(ByVal pv_sName As String, ByVal pv_sValue As String) As Boolean

        Dim sv_DS As New DataSet
        Dim sv_DA As SqlDataAdapter
        mv_sSql = "SELECT * from Sys_TOOLBARBUTTON WHERE sName=N'" & pv_sValue & "' AND FP_intRoleID=" & gv_intSubSysID & " AND sName<>" & sUnicode(pv_sName)
        Try
            sv_DA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_DA.Fill(sv_DS, "Sys_TOOLBARBUTTON")
            If sv_DS.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function bIsExisted(ByVal pv_sName As String, ByVal pv_intRoleID As Integer) As Boolean

        Dim sv_DS As New DataSet
        Dim sv_DA As SqlDataAdapter
        mv_sSql = "SELECT * from Sys_TOOLBARBUTTON WHERE sName=N'" & pv_sName & "' AND FP_intRoleID=" & pv_intRoleID
        Try
            sv_DA = New SqlDataAdapter(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_DA.Fill(sv_DS, "Sys_TOOLBARBUTTON")
            If sv_DS.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function bDelete(ByVal pv_sName As String) As Boolean
        Dim sv_oCmd As SqlCommand
        mv_sSql = "DELETE from Sys_TOOLBARBUTTON WHERE sName=N'" & pv_sName & "' AND FP_intRoleID=" & gv_intSubSysID
        Try
            sv_oCmd = New SqlCommand(mv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_oCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class

