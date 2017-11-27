Imports System.Reflection
Imports System.IO
Public Class DVC_MenuItemForToolBar
    Public Function GetMenuItem(ByVal pv_sRoleName As String, ByVal pv_iRole As Integer, _
                                                    ByVal pv_sDLLName As String, ByVal pv_sFormName As String, _
                                                    ByVal pv_sAssemblyName As String, ByVal pv_sParameterList As String, _
                                                    ByVal pv_sIconPath As String, ByVal pv_intShorCutKey As Integer) As IconMenuItem
        Dim fv_oMenuItem As New IconMenuItem(pv_sRoleName, pv_sIconPath, pv_intShorCutKey)
        With fv_oMenuItem
            .mv_sParameterList = pv_sParameterList
            .mv_sRoleName = pv_sRoleName
            .mv_iID = pv_iRole
            .mv_sAssemblyName = pv_sAssemblyName
            .mv_sDLLName = pv_sDLLName
            .mv_sFormName = pv_sFormName
        End With
        AddHandler fv_oMenuItem.Click, AddressOf _Click
        Return fv_oMenuItem
    End Function
    '----------------------------------------------------------------------------------------------------
    'Mục đích       :Thực hiện khi chọn một chức năng trên Menu
    'Đầu vào         :
    'Đầu ra           :
    'Người tạo      :CuongDV
    'Ngày tạo       :
    'Last Modified:
    '----------------------------------------------------------------------------------------------------
    Private Sub _Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            ms_InvokeForm(CType(sender, IconMenuItem).mv_sAssemblyName, CType(sender, IconMenuItem).mv_sDLLName, CType(sender, IconMenuItem).mv_sFormName, CType(sender, IconMenuItem).mv_sParameterList, CType(sender, IconMenuItem).mv_sRoleName)
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
                MessageBox.Show("Chưa gán chức năng cho nút này", "Chức năng", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            s &= "Các thông số cụ thể như sau: " & vbCrLf
            s &= "1.Tên DLL: " & pv_sDLLName & vbCrLf
            s &= "2.Tên Form: " & pv_sFormName & vbCrLf
            s &= "3.Danh sách tham số đầu vào của Form: " & IIf(pv_sParameterList.Trim.Equals(String.Empty), "Không có", pv_sParameterList)
            MessageBox.Show(s, "Chức năng", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(s, "Chức năng", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

End Class
