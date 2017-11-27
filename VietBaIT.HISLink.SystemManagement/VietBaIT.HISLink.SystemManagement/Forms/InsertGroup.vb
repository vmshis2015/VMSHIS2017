Public Class InsertGroup
    Private mv_iStatus As Status
    Private mv_bSuccess As Boolean = False
    Private mv_GroupID As Integer = 0
    Private mv_GroupName As String = ""
    Public mv_bAdmin As Boolean = False
    Public CurrUserList As String = ""

#Region "Properties"
    Public Property ps_GroupID() As Integer
        Get
            Return mv_GroupID
        End Get
        Set(ByVal Value As Integer)
            mv_GroupID = Value
        End Set
    End Property
    Public Property ps_iStatus() As Status
        Get
            Return mv_iStatus
        End Get
        Set(ByVal Value As Status)
            mv_iStatus = Value
        End Set
    End Property
    Public Property pb_Success() As Boolean
        Get
            Return mv_bSuccess
        End Get
        Set(ByVal Value As Boolean)
            mv_bSuccess = Value
        End Set
    End Property
#End Region

    Private Sub InsertFunction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Select Case mv_iStatus
                Case globalModule.Status.Update 'Cập nhật
                    GetDataForUpdate()
                    cmdCreate.Text = "Apply"
                Case Else
                    cmdCreate.Text = "Create"
                    txtGroupName.Enabled = True
                    txtDesc.Enabled = True
                    cmdAddMember.Enabled = True
                    cmdRemoveMember.Enabled = False
                    cmdCreate.Enabled = True
                    txtGroupName.Focus()
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Try
            If Asc(e.KeyChar) = Keys.Enter Then e.Handled = True
        Catch ex As Exception
        End Try
    End Sub
    Private Sub GetDataForUpdate()
        Dim sv_oclsUser As New clsUser
        Dim sv_DS As New DataSet
        Try
            CurrUserList = ""
            sv_DS = sv_oclsUser.dsGetGroupUserInfor(mv_GroupID)
            If sv_DS.Tables(0).Rows.Count > 0 Then
                txtGroupName.Enabled = True
                txtDesc.Enabled = True
                chkFullAuthority.Checked = IIf(CInt(sv_DS.Tables(0).Rows(0)("IsAdmin")) = 1, True, False)
                txtGroupName.Text = sDBnull(sv_DS.Tables(0).Rows(0)("GroupName"), "")
                txtDesc.Text = sDBnull(sv_DS.Tables(0).Rows(0)("DESC"), "")
                Dim sv_DSMemberOfGroup As DataRow() = gv_dsGroupMember.Tables(0).Select("GroupID=" & mv_GroupID)
                If Not IsNothing(sv_DSMemberOfGroup) AndAlso sv_DSMemberOfGroup.GetLength(0) > 0 Then
                    lstMemberList.Items.Clear()
                    For Each dr As DataRow In sv_DSMemberOfGroup
                        CurrUserList &= dr("UserID") & ","
                        lstMemberList.Items.Add(dr("UserID"))
                    Next
                    cmdRemoveMember.Enabled = lstMemberList.Items.Count > 0
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function mf_bCheckValidData() As Boolean
        Try
            If txtGroupName.Text.Trim.Equals(String.Empty) Then
                MessageBox.Show("Bạn phải nhập tên nhóm người dùng.", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtGroupName.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show("Lỗi cần liên hệ với VietBaIT để giải quyết:" & ex.Message, gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End Try
    End Function


    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub InsertUser_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Enter
                    processtabkey(True)
                Case Keys.Escape
                    Me.Close()
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdRemoveMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveMember.Click
        Try
            If lstMemberList.Items.Count > 0 AndAlso Not IsNothing(lstMemberList.SelectedItem) Then
                Dim UID As String = lstMemberList.SelectedItem.ToString()
                Dim objUser As New clsUser()
                'If objUser.DeleteMemberOfGroup(mv_GroupID, UID) Then
                lstMemberList.Items.RemoveAt(lstMemberList.SelectedIndex)
                'Xóa khỏi Dataset
                'For Each dr As DataRow In gv_dsGroupMember.Tables(0).Rows
                '    If CInt(dr("GroupID")) = mv_GroupID AndAlso dr("UserID").ToString.ToUpper = UID.ToUpper Then
                '        gv_dsGroupMember.Tables(0).Rows.Remove(dr)
                '        gv_dsGroupMember.Tables(0).AcceptChanges()
                '        Exit For
                '    End If
                'Next
                'End If
                cmdRemoveMember.Enabled = lstMemberList.Items.Count > 0
                If lstMemberList.Items.Count > 0 Then
                    lstMemberList.SelectedIndex = 0
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreate.Click
        Dim sv_oclsUser As New clsUser
        Dim sv_sGroupName As String = ""

        Dim sv_sDesc As String = ""
        Try
            If mf_bCheckValidData() Then
                sv_sGroupName = ValidData(txtGroupName.Text)
                sv_sDesc = ValidData(txtDesc.Text)
                mv_GroupName = sv_sGroupName
                Select Case mv_iStatus
                    Case globalModule.Status.Update
                        If sv_oclsUser.UpdateGroup(gv_sBranchID, mv_GroupID, sv_sGroupName, sv_sDesc, IIf(chkFullAuthority.Checked, 1, 0)) Then
                            mv_bSuccess = True
                            Dim sv_oDR As DataRow
                            For Each sv_oDR In gv_dsGroupUser.Tables(0).Rows
                                If CInt(sv_oDR.Item("GroupID")) = mv_GroupID Then
                                    With sv_oDR
                                        .Item("GroupName") = sv_sGroupName
                                        .Item("IsAdmin") = IIf(chkFullAuthority.Checked, 1, 0)
                                        .Item("Desc") = sv_sDesc
                                    End With
                                    Exit For
                                End If
                            Next
                            'Chỉnh lại tên trên Chiview
                            gv_oMainForm.tvwAdminSystem.SelectedNode.Text = sv_sGroupName
                            gv_dsGroupUser.Tables(0).AcceptChanges()
                            'Cập nhật Members of Groups
                            'Đầu tiên xóa tất cả các Member
                            sv_oclsUser.DeleteAllMemberOfGroup(mv_GroupID)
                            'Xóa trong Dataset
                            Dim intCount As Integer = gv_dsGroupMember.Tables(0).Select("GroupID=" & mv_GroupID).GetLength(0)
                            Do While intCount > 0
                                For Each dr As DataRow In gv_dsGroupMember.Tables(0).Rows
                                    If CInt(dr("GroupID")) = mv_GroupID Then
                                        intCount -= 1
                                        gv_dsGroupMember.Tables(0).Rows.Remove(dr)
                                        gv_dsGroupMember.Tables(0).AcceptChanges()
                                        Exit For
                                    End If
                                Next
                            Loop
                            'Thêm lại Member
                            For i As Integer = 0 To lstMemberList.Items.Count - 1
                                If sv_oclsUser.InsertMemberOfGroup(gv_sBranchID, mv_GroupID, lstMemberList.Items(i).ToString()) Then
                                    'Thêm vào Dataset
                                    Dim dr As DataRow = gv_dsGroupMember.Tables(0).NewRow
                                    dr("BranchID") = gv_sBranchID
                                    dr("GroupName") = sv_sGroupName
                                    dr("GroupID") = mv_GroupID
                                    dr("UserID") = lstMemberList.Items(i).ToString()
                                    gv_dsGroupMember.Tables(0).Rows.Add(dr)
                                    gv_dsGroupMember.Tables(0).AcceptChanges()
                                End If
                            Next
                            Me.Close()
                        Else
                            MessageBox.Show("Đã tồn tại nhóm có tên:" & sv_sGroupName, gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            mv_bSuccess = False
                            Return
                        End If
                    Case globalModule.Status.Insert
                        mv_GroupID = sv_oclsUser.InsertGroup(gv_sBranchID, sv_sGroupName, sv_sDesc, chkFullAuthority.Checked)
                        If mv_GroupID <> -1 Then
                            mv_bSuccess = True
                            Dim sv_oDR As DataRow
                            sv_oDR = gv_dsGroupUser.Tables(0).NewRow
                            With sv_oDR
                                .Item("BranchID") = gv_sBranchID
                                .Item("Isadmin") = IIf(chkFullAuthority.Checked, 1, 0)
                                .Item("GroupID") = mv_GroupID
                                .Item("GroupName") = sv_sGroupName
                                .Item("Desc") = sv_sDesc
                            End With
                            gv_dsGroupUser.Tables(0).Rows.Add(sv_oDR)
                            gv_dsGroupUser.Tables(0).AcceptChanges()
                            'Cập nhật Members of Groups
                            'Đầu tiên xóa tất cả các Member
                            sv_oclsUser.DeleteAllMemberOfGroup(mv_GroupID)
                            'Xóa trong Dataset
                            Dim intCount As Integer = gv_dsGroupMember.Tables(0).Select("GroupID=" & mv_GroupID).GetLength(0)
                            Do While intCount > 0
                                For Each dr As DataRow In gv_dsGroupMember.Tables(0).Rows
                                    If CInt(dr("GroupID")) = mv_GroupID Then
                                        intCount -= 1
                                        gv_dsGroupMember.Tables(0).Rows.Remove(dr)
                                        gv_dsGroupMember.Tables(0).AcceptChanges()
                                        Exit For
                                    End If
                                Next
                            Loop
                            'Thêm lại Member
                            For i As Integer = 0 To lstMemberList.Items.Count - 1
                                If sv_oclsUser.InsertMemberOfGroup(gv_sBranchID, mv_GroupID, lstMemberList.Items(i).ToString()) Then
                                    'Thêm vào Dataset
                                    Dim dr As DataRow = gv_dsGroupMember.Tables(0).NewRow
                                    dr("BranchID") = gv_sBranchID
                                    dr("GroupName") = sv_sGroupName
                                    dr("GroupID") = mv_GroupID
                                    dr("UserID") = lstMemberList.Items(i).ToString()
                                    gv_dsGroupMember.Tables(0).Rows.Add(dr)
                                    gv_dsGroupMember.Tables(0).AcceptChanges()
                                End If
                            Next


                            If mv_bSuccess Then
                                'Thêm nhánh trên cây
                                Dim sv_oNode As New TreeNode(sv_sGroupName)
                                With sv_oNode
                                    .Tag = "LEAFGROUP#" & mv_GroupID
                                    .SelectedImageIndex = ImageIndex.NodeUser
                                    .ImageIndex = ImageIndex.NodeUser
                                    .ForeColor = Color.Navy
                                    .NodeFont = New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
                                End With

                                'Thêm vào DataSet chức năng này
                                gv_oMainForm.tvwAdminSystem.SelectedNode.Nodes.Add(sv_oNode)
                                gv_oMainForm.tvwAdminSystem.SelectedNode.ExpandAll()
                            End If
                            If gv_bCloseFormAfterDML Then
                                Me.Close()
                            Else
                                txtGroupName.Text = ""
                                txtDesc.Text = ""
                                lstMemberList.Items.Clear()
                                cmdRemoveMember.Enabled = False
                                txtGroupName.Focus()
                            End If
                        Else
                            MessageBox.Show("Đã tồn tại nhóm có tên:" & sv_sGroupName, gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            txtGroupName.Focus()
                            mv_bSuccess = False
                        End If
                End Select
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            mv_bSuccess = False
            Return
        End Try
    End Sub

    Private Sub cmdAddMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddMember.Click
        Dim SU As New SelectUser()
        SU.CurrUserList = CurrUserList
        SU.ShowDialog()
        If Not SU.bCancel Then
            CurrUserList &= SU.NewCurrUL
            Dim arrUL As String() = SU.NewCurrUL.Split(",")
            For i As Integer = 0 To arrUL.Length - 1
                If arrUL(i).Trim <> "" Then
                    lstMemberList.Items.Add(arrUL(i))
                    If mv_iStatus = Status.Update Then
                        'Thêm luôn vào CSDL nếu người dùng bỏ qua
                    End If
                End If
            Next
        End If
    End Sub

End Class