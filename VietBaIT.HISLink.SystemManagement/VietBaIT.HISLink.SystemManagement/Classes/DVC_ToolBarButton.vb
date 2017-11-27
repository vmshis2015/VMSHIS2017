Imports System.Reflection
Imports System.IO
Public Class DVC_ToolBarButton
    Public mv_DS As New DataSet
    Public Function GetToolBarButton(ByVal pv_sRoleName As String, ByVal pv_sText As String, ByVal pv_sToolTipText As String, ByVal pv_iRole As Integer, _
                                                    ByVal pv_sDLLName As String, ByVal pv_sFormName As String, _
                                                    ByVal pv_sAssemblyName As String, ByVal pv_sParameterList As String, ByVal pv_oImgList As ImageList, _
                                                    ByVal pv_sIconPath As String, ByVal pv_intIgmIndex As Integer, ByVal pv_intStyle As Integer, _
                                                     ByRef pv_tbr As ToolBar, Optional ByVal pv_bDisplayText As Boolean = False) As ToolBarBtn
        Dim fv_bCtxMenu As Boolean = False
        Dim fv_objCtxMenu As ContextMenu
        fv_objCtxMenu = objGetPopupMenu(pv_iRole)
        If fv_objCtxMenu Is Nothing Then
            fv_bCtxMenu = False
        Else
            fv_bCtxMenu = True
        End If
        Dim fv_oToolBarButton As New ToolBarBtn(pv_sText, pv_sToolTipText, pv_intIgmIndex, pv_bDisplayText, IIf(pv_intStyle = 0, False, True), fv_bCtxMenu, fv_objCtxMenu)
        With fv_oToolBarButton
            .mv_sRoleName = pv_sRoleName
            .mv_sParameterList = pv_sParameterList
            .mv_sText = pv_sText
            .mv_iID = pv_iRole
            .mv_sAssemblyName = pv_sAssemblyName
            .mv_sDLLName = pv_sDLLName
            .mv_sFormName = pv_sFormName
            .mv_intImgIndex = pv_intIgmIndex
        End With
        pv_tbr.Buttons.Add(fv_oToolBarButton)
        Return fv_oToolBarButton
    End Function
    Private Function objGetPopupMenu(ByVal pv_intRolePerformed As Integer) As ContextMenu
        Try
            If bRoleHasChildren(pv_intRolePerformed) Then
                Return CtxGetMenu(pv_intRolePerformed)
            Else
                Return Nothing
            End If
        Catch ex As Exception

        End Try
    End Function

    Private Function bRoleHasChildren(ByVal pv_intRolePerformed As Integer) As Boolean
        Dim sv_Ds As New DataSet
        Dim sv_DA As SqlClient.SqlDataAdapter
        Dim sv_sSql As String
        Try
            sv_sSql = "SELECT * from Sys_ROLES WHERE iParentRole=" & pv_intRolePerformed & " AND FP_sBranchID=N'" & gv_sBranchID & "'"
            sv_DA = New SqlClient.SqlDataAdapter(sv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_DA.Fill(sv_Ds, "Sys_ROLES")
            If sv_Ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    '----------------------------------------------------------------------------------------------------
    'Mục đích       :Xây dựng cây Menu của từng phân hệ
    'Đầu vào         :Mã phân hệ
    'Đầu ra           :Menu của phân hệ được xây dựng 
    'Người tạo      :CuongDV
    'Ngày tạo       :
    'Last Modified:
    '----------------------------------------------------------------------------------------------------
    Private Function CtxGetMenu(ByVal pv_intRolePerformed As Integer) As ContextMenu
        Dim fv_oCtxMenu As New ContextMenu
        Dim fv_oMnu As New DVC_MenuItemForToolBar
        Dim sv_oDR As DataRow()
        Try
            'get all ChildRoles of this Role
            sv_oDR = mv_DS.Tables(0).Select("iParentRole=" & pv_intRolePerformed)
            For i As Integer = 0 To sv_oDR.Length - 1
                Dim sv_oMenuItem As IconMenuItem
                'Lấy về ImageIndex của Menu
                sv_oMenuItem = fv_oMnu.GetMenuItem(IsNull_VN(sv_oDR(i)("sRoleName")), sv_oDR(i)("iRole"), _
                                                                        IsNull_VN(sv_oDR(i)("sDLLName")), IsNull_VN(sv_oDR(i)("sFormName")), _
                                                                        IsNull_VN(sv_oDR(i)("sAssemblyName")), IsNull_VN(sv_oDR(i)("sParameterList")), IsNull_VN(sv_oDR(i)("sIconPath")), ShortCutZero(sv_oDR(i)("intShortCutKey")))

                'Kiểm tra xem Menu có được kích hoạt hay không
                If Not IsDBNull(sv_oDR(i)("bEnable")) Then
                    If Not CBool(sv_oDR(i)("bEnable")) Then
                        sv_oMenuItem.Enabled = False
                    Else
                    End If
                End If
                AddMenuItem(sv_oMenuItem, sv_oDR(i)("iRole"))
                fv_oCtxMenu.MenuItems.Add(sv_oMenuItem)
            Next
            Return fv_oCtxMenu
        Catch ex As Exception

        End Try
    End Function
    'Thủ tục đệ quy tạo cây menu
    Private Sub AddMenuItem(ByVal pv_oParentMenuItem As MenuItem, ByVal pv_oRoleID As Integer)
        Dim sv_oDR As DataRow()
        Dim fv_oMnu As New DVC_MenuItemForToolBar
        Try
            'get all ChildRoles of this Role
            sv_oDR = mv_DS.Tables(0).Select("iParentRole=" & pv_oRoleID)
            For i As Integer = 0 To sv_oDR.Length - 1
                Dim sv_oMenuItem As IconMenuItem

                sv_oMenuItem = fv_oMnu.GetMenuItem(IsNull_VN(sv_oDR(i)("sRoleName")), sv_oDR(i)("iRole"), _
                                                                        IsNull_VN(sv_oDR(i)("sDLLName")), IsNull_VN(sv_oDR(i)("sFormName")), _
                                                                        IsNull_VN(sv_oDR(i)("sAssemblyName")), IsNull_VN(sv_oDR(i)("sParameterList")), IsNull_VN(sv_oDR(i)("sIconPath")), ShortCutZero(sv_oDR(i)("intShortCutKey")))
                'Kiểm tra xem Menu có được kích hoạt hay không
                If Not IsDBNull(sv_oDR(i)("bEnable")) Then
                    If Not CBool(sv_oDR(i)("bEnable")) Then
                        sv_oMenuItem.Enabled = False
                    Else
                    End If
                End If
                pv_oParentMenuItem.MenuItems.Add(sv_oMenuItem)
                'Gọi đệ quy đối với chức năng vừa được xét đến
                AddMenuItem(sv_oMenuItem, sv_oDR(i)("iRole"))
            Next
        Catch ex As Exception
        End Try
    End Sub
    Private Function IsNull_VN(ByVal pv_obj As Object) As String
        Return IIf(IsDBNull(pv_obj), " ", pv_obj.ToString)
    End Function
    Private Function IsNull_VNEN(ByVal pv_obj As Object, ByVal pv_obj1 As Object) As String
        If pv_obj1.ToString.Trim.Equals(String.Empty) Then
            Return pv_obj.ToString
        Else
            Return pv_obj1.ToString
        End If
    End Function
    Private Function ShortCutZero(ByVal pv_intValue As Integer) As Integer
        If pv_intValue = -1 Then
            Return 0
        Else
            Return pv_intValue
        End If
    End Function
End Class
