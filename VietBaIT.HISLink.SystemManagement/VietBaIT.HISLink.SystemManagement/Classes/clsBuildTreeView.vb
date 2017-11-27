'------------------------------------------------------------------------------------------------------
'Mục đích: Tạo lớp TreeView có nhiệm vụ xử lý tất cả các nghiệp vụ liên quan đến cây QTHT như: Tạo cây...
'Người tạo:CuongDV
'Ngày tạo:09/03/2005
'------------------------------------------------------------------------------------------------------
Public Class clsBuildTreeView
    '----------------------------------------------------------------------------------------------------
    'Mục đích       :
    'Đầu vào         :None
    'Đầu ra           :
    'Người tạo      :CuongDV
    'Ngày tạo       :
    'Last Modified:
    '----------------------------------------------------------------------------------------------------
    Sub New()
        Try
            Dim sv_oclsUser As New clsUser
            Dim cls_Menu As New clsFunction
            Dim cls_Role As New clsRole
            Dim cls_Param As New cls_Parameter
            Dim cls_TbrBtn As New clsTbrButton
            gv_dsTbrBtn = cls_TbrBtn.dsGetButton
            gv_dsFunction = cls_Menu.dsGetAllFunction
            gv_dsRole = cls_Menu.dsGetAllRole()
            gv_DSRoleForOutIn = cls_Menu.dsGetAllRoleForOutIn()
            gv_dsUser = sv_oclsUser.dsGetAllUser
            gv_dsGroupUser = sv_oclsUser.dsGetAllGroupUser
            gv_dsGroupMember = sv_oclsUser.dsGetAllGroupMember
            gv_dsDelegate = sv_oclsUser.dsGetDelegate
            gv_dsParam = cls_Param.dsGetAllParams
            gv_dsRolesForUsers = cls_Role.dsGetAllRoleOfUser
            gv_dsGroupRoles = cls_Role.dsGetAllRoleOfGroup
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    '----------------------------------------------------------------------------------------------------
    'Mục đích       :Xây dựng cây TreeView Quản trị hệ thống
    'Đầu vào         :Cây TreeView, Danh sách ảnh cho các Node của TreeView
    'Đầu ra           :Cây TreeView Quản trị Hệ thống được dựng
    'Người tạo      :CuongDV
    'Ngày tạo       :
    'Last Modified:
    '----------------------------------------------------------------------------------------------------
    Public Sub BuildTreeView(ByVal pv_otvw As TreeView, ByVal pv_oImgList As ImageList)
        Dim sv_oRootNode As New TreeNode("QTHT")
        Try
            'Tạo gốc cho cây
            pv_otvw.ImageList = pv_oImgList
            CreateRootNode(pv_otvw, "Quản trị hệ thống", globalModule.ImageIndex.RootNode, globalModule.ImageIndex.RootNode)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    '----------------------------------------------------------------------------------------------------
    'Mục đích       :Tạo gốc cho cây và tạo các Node mặc định bắt buộc có trên cây
    'Đầu vào         :Các thông tin cần thiết
    'Đầu ra           :
    'Người tạo      :CuongDV
    'Ngày tạo       :
    'Last Modified:
    '----------------------------------------------------------------------------------------------------
    Private Sub CreateRootNode(ByVal pv_otvw As TreeView, ByVal pv_sRoleName As String, ByVal pv_oImgIndex As ImageIndex, ByVal pv_oSelectedImageIndex As ImageIndex)
        Dim sv_oRootNode As New TreeNode
        With sv_oRootNode
            .Text = pv_sRoleName
            .Tag = "QTHT"
            .SelectedImageIndex = pv_oSelectedImageIndex
            .ImageIndex = pv_oImgIndex
            .ForeColor = System.Drawing.Color.DarkRed
            .NodeFont = New Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point)
        End With
        pv_otvw.Nodes.Add(sv_oRootNode)
        pv_otvw.SelectedNode = sv_oRootNode
        'Tạo các Node mặc định phải có
        AppLogger.NLogAction.Log.Trace("CreateDefaultNode...")
        CreateDefaultNode(pv_otvw, pv_otvw.SelectedNode)
    End Sub
    '----------------------------------------------------------------------------------------------------
    'Mục đích       :Tạo các Node mặc định của TreeView và gọi đệ quy để tạo các Con của từng ChildNode
    'đó. Trong trường hợp chiều sâu hoặc số con của cây nói chung và của các DefaultNode nói riêng quá lớn
    'sẽ dẫn đến trường hợp cây bị Load rất chậm. Vì vậy một giải pháp thứ 2 là chỉ tạo cây ở mức thứ2. Sau đó
    'cây sẽ được tăng chiều sâu mỗi lần click vào một ChildNode
    'Đầu vào         :None
    'Đầu ra           :
    'Người tạo      :CuongDV
    'Ngày tạo       :
    'Last Modified:
    '----------------------------------------------------------------------------------------------------
    Private Sub CreateDefaultNode(ByVal pv_otvw As TreeView, ByVal pv_oNode As TreeNode)
        Try
            Dim sv_oGroupUserNode As New TreeNode("Groups")
            Dim sv_oUserNode As New TreeNode("Users")
            Dim sv_oFunctionNode As New TreeNode("Functions")
            Dim sv_oRoleNode As New TreeNode("Roles")
            Dim sv_oParamNode As New TreeNode("Parameters")
            With sv_oGroupUserNode
                .Text = "Nhóm người dùng"
                .Tag = "ROOTGROUP"
                .SelectedImageIndex = ImageIndex.RootUser
                .ImageIndex = ImageIndex.RootUser
                .ForeColor = System.Drawing.Color.DarkBlue
                .NodeFont = New Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point)
            End With
            With sv_oUserNode
                .Text = "Người dùng"
                .Tag = "ROOTUSER"
                .SelectedImageIndex = ImageIndex.RootUser
                .ImageIndex = ImageIndex.RootUser
                .ForeColor = System.Drawing.Color.DarkBlue
                .NodeFont = New Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point)
            End With
            With sv_oFunctionNode
                .Text = "Chức năng"
                .Tag = "ROOTFUNCTION"
                .SelectedImageIndex = ImageIndex.RootFuntion
                .ImageIndex = ImageIndex.RootFuntion
                .ForeColor = System.Drawing.Color.DarkBlue
                .NodeFont = New Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point)
            End With
            With sv_oRoleNode
                .Text = "Menu"
                .Tag = "ROOTROLE#-2"
                .SelectedImageIndex = ImageIndex.RootRole
                .ImageIndex = ImageIndex.RootRole
                .ForeColor = System.Drawing.Color.DarkBlue
                .NodeFont = New Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point)
            End With
            With sv_oParamNode
                .Text = "Tham số hệ thống"
                .Tag = "ROOTPARAM"
                .SelectedImageIndex = ImageIndex.RootParam
                .ImageIndex = ImageIndex.RootParam
                .ForeColor = System.Drawing.Color.DarkBlue
                .NodeFont = New Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point)
            End With
            With pv_oNode
                .Nodes.Add(sv_oGroupUserNode)
                .Nodes.Add(sv_oUserNode)
                .Nodes.Add(sv_oFunctionNode)
                .Nodes.Add(sv_oRoleNode)
                .Nodes.Add(sv_oParamNode)
            End With
            'Tạo các ChildNode đến mức cuối cùng cho các DefaultNode. Nếu chiều sâu của cây lớn-->Load chậm
            'Tạo các GroupUserNode
            CreateGroupUserNode(pv_otvw, sv_oGroupUserNode, globalModule.ImageIndex.NodeUser, globalModule.ImageIndex.NodeUser)

            'Tạo các UserNode
            CreateUserNode(pv_otvw, sv_oUserNode, globalModule.ImageIndex.NodeUser, globalModule.ImageIndex.NodeUser)

            'Tạo các FunctionNode
            CreateFunctionNode(pv_otvw, sv_oFunctionNode, globalModule.ImageIndex.NodeFuntion, globalModule.ImageIndex.NodeFuntion)

            'Tạo các RoleNode
            CreateRoleNode(pv_otvw, sv_oRoleNode, globalModule.ImageIndex.LeafRole, globalModule.ImageIndex.LeafRole)

            'Tạo các ParameterNode
            CreateParameterNode(pv_otvw, sv_oParamNode, globalModule.ImageIndex.NodeParam, globalModule.ImageIndex.NodeParam)

            'Thu gọn tất cả các Node nằm dưới RootNode
            sv_oGroupUserNode.Collapse()
            sv_oFunctionNode.Collapse()
            sv_oRoleNode.Collapse()
            sv_oUserNode.Collapse()
            sv_oParamNode.Collapse()
        Catch ex As Exception
            AppLogger.NLogAction.Log.Trace("CreateDefaultNode.Exception..." + ex.Message)
        End Try
     
    End Sub
    '----------------------------------------------------------------------------------------------------
    'Mục đích       :Tạo các Node tham số hệ thống
    'Đầu vào         :None
    'Đầu ra           :
    'Người tạo      :CuongDV
    'Ngày tạo       :
    'Last Modified:
    '----------------------------------------------------------------------------------------------------
    Private Sub CreateParameterNode(ByVal pv_otvw As TreeView, ByVal pv_oNode As TreeNode, ByVal pv_oImgIndex As ImageIndex, ByVal pv_oSelectedImageIndex As ImageIndex)
        Dim i As Integer = 0
        Try
            If gv_dsParam.Tables(0).Rows.Count > 0 Then
                For i = 0 To gv_dsParam.Tables(0).Rows.Count - 1
                    Dim sv_oNode As New TreeNode
                    With sv_oNode
                        .Text = gv_dsParam.Tables(0).Rows(i)("sName").ToString
                        .Tag = "NODEPARAM"
                        If CInt(gv_dsParam.Tables(0).Rows(i)("iStatus")) = 0 Then
                            .ForeColor = gv_LockedParamColor
                        Else
                            .ForeColor = System.Drawing.Color.Navy
                        End If
                        .SelectedImageIndex = pv_oSelectedImageIndex
                        .ImageIndex = pv_oImgIndex
                        .NodeFont = New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
                    End With
                    pv_oNode.Nodes.Add(sv_oNode)
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("CreateParameterNode-->" + ex.Message)
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
    Private Sub CreateGroupUserNode(ByVal pv_otvw As TreeView, ByVal pv_oNode As TreeNode, ByVal pv_oImgIndex As ImageIndex, ByVal pv_oSelectedImageIndex As ImageIndex)
        Dim i As Integer = 0
        Try
            If gv_dsGroupUser.Tables(0).Rows.Count > 0 Then
                For i = 0 To gv_dsGroupUser.Tables(0).Rows.Count - 1
                    Dim sv_oNode As New TreeNode
                    With sv_oNode
                        .Text = gv_dsGroupUser.Tables(0).Rows(i)("GroupName").ToString
                        .Tag = "LEAFGROUP#" & gv_dsGroupUser.Tables(0).Rows(i)("GroupID").ToString
                        .ForeColor = System.Drawing.Color.Navy
                        .SelectedImageIndex = pv_oSelectedImageIndex
                        .ImageIndex = pv_oImgIndex
                        .NodeFont = New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
                    End With
                    pv_oNode.Nodes.Add(sv_oNode)
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("CreateGroupUserNode-->" + ex.Message)
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
    Private Sub CreateUserNode(ByVal pv_otvw As TreeView, ByVal pv_oNode As TreeNode, ByVal pv_oImgIndex As ImageIndex, ByVal pv_oSelectedImageIndex As ImageIndex)
        Dim i As Integer = 0
        Try
            If gv_dsUser.Tables(0).Rows.Count > 0 Then
                For i = 0 To gv_dsUser.Tables(0).Rows.Count - 1
                    Dim sv_oNode As New TreeNode
                    With sv_oNode
                        .Text = gv_dsUser.Tables(0).Rows(i)("PK_sUID").ToString
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
            MessageBox.Show("CreateUserNode-->" + ex.Message)
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
    Private Sub CreateFunctionNode(ByVal pv_otvw As TreeView, ByVal pv_oNode As TreeNode, ByVal pv_oImgIndex As ImageIndex, ByVal pv_oSelectedImageIndex As ImageIndex)
        Dim i As Integer = 0
        Try
            If gv_dsFunction.Tables(0).Rows.Count > 0 Then
                For i = 0 To gv_dsFunction.Tables(0).Rows.Count - 1
                    Dim sv_oNode As New TreeNode
                    With sv_oNode
                        .Text = gv_dsFunction.Tables(0).Rows(i)("sFunctionName").ToString
                        .Tag = "LEAFFUNCTIONS#" & gv_dsFunction.Tables(0).Rows(i)("PK_iID").ToString
                        If Not CBool(gv_dsFunction.Tables(0).Rows(i)("bEnable")) Then
                            .ForeColor = gv_LockedFunctionColor
                            .SelectedImageIndex = ImageIndex.LockFunction
                            .ImageIndex = ImageIndex.LockFunction
                        Else
                            .ForeColor = System.Drawing.Color.Navy
                            .SelectedImageIndex = pv_oSelectedImageIndex
                            .ImageIndex = pv_oImgIndex
                        End If
                        .NodeFont = New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
                    End With
                    pv_oNode.Nodes.Add(sv_oNode)
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("CreateFunctionNode-->" + ex.Message)
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
    Private Sub CreateRoleNode(ByVal pv_otvw As TreeView, ByVal pv_oNode As TreeNode, ByVal pv_oImgIndex As ImageIndex, ByVal pv_oSelectedImageIndex As ImageIndex)
        Dim i As Integer = 0
        Dim sv_oNode As TreeNode
        Try


            If gv_dsRole.Tables(0).Rows.Count > 0 Then
                For i = 0 To gv_dsRole.Tables(0).Rows.Count - 1
                    'Kiểm tra nếu mã ParentRole =-2 nghĩa là Node phân hệ 
                    If CInt(gv_dsRole.Tables(0).Rows(i)("iParentRole")) = -2 Then
                        If bIsValidPath(gv_dsRole.Tables(0).Rows(i)("sIconPath")) Then
                            pv_otvw.ImageList.Images.Add(Image.FromFile(gv_dsRole.Tables(0).Rows(i)("sIconPath")))
                            AddNewTreeNode(pv_otvw, pv_oNode, gv_dsRole.Tables(0).Rows(i)("iRole"), gv_dsRole.Tables(0).Rows(i)("sRoleName"), Int_IsNothingOrDBNull(gv_dsRole.Tables(0).Rows(i)("FK_iFunctionID"), -1), pv_otvw.ImageList.Images.Count - 1, pv_otvw.ImageList.Images.Count - 1, -2)
                        Else
                            AddNewTreeNode(pv_otvw, pv_oNode, gv_dsRole.Tables(0).Rows(i)("iRole"), gv_dsRole.Tables(0).Rows(i)("sRoleName"), Int_IsNothingOrDBNull(gv_dsRole.Tables(0).Rows(i)("FK_iFunctionID"), -1), globalModule.ImageIndex.NodeRole, globalModule.ImageIndex.NodeRole, -2)
                        End If
                        'Gọi thủ tục đệ quy để tạo các ChildNode cho Node vừa được thêm mới này
                        CreateChildNode(pv_otvw, pv_otvw.SelectedNode, CInt(pv_otvw.SelectedNode.Tag.ToString.Substring(pv_otvw.SelectedNode.Tag.ToString.IndexOf("#") + 1)))
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("CreateRoleNode-->" + ex.Message)
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
    Private Sub AddNewTreeNode(ByVal pv_oTvw As TreeView, ByVal pv_oParentNode As TreeNode, ByVal pv_iRole As Integer, ByVal pv_sRoleName As String, ByVal pv_iFunctionID As Integer, ByVal pv_oIndex As ImageIndex, ByVal pv_oSelectedIndex As ImageIndex, Optional ByVal pv_iParentRole As Integer = -1)
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
            arrDR = gv_dsRole.Tables(0).Select("iParentRole=" & pv_iRole, "iOrder")
            If UBound(arrDR) >= 0 Then
                For i As Integer = 0 To arrDR.Length - 1
                    sv_oNode = New TreeNode(arrDR(i).Item("sRoleName"))
                    sv_oNode.Tag = arrDR(i).Item("iRole")
                    sv_oNode.NodeFont = New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
                    AddNewTreeNode(pv_oTvw, pv_oNode, sv_oNode.Tag, sv_oNode.Text, Int_IsNothingOrDBNull(arrDR(i).Item("FK_iFunctionID"), -1), globalModule.ImageIndex.LeafRole, globalModule.ImageIndex.LeafRole)
                    CreateChildNode(pv_oTvw, pv_oTvw.SelectedNode, sv_oNode.Tag)
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class
