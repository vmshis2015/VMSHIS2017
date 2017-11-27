Imports System.Reflection
Public Class InsertFunction
    Inherits System.Windows.Forms.Form
    Private mv_iStatus As Integer = 1
    Private mv_bSuccess As Boolean = False
    Private mv_iID As Integer = 0
    Private mv_sFunctionName As String
    Private mv_sOldFunctionName As String = ""
    Private mv_bEnable As Boolean = False

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    Public Sub New(ByVal pv_sTitle As String)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.Text = pv_sTitle
        'Add any initialization after the InitializeComponent() call

    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents txtDllName As System.Windows.Forms.TextBox
    Friend WithEvents chkEnable As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFunctionName As System.Windows.Forms.TextBox
    Friend WithEvents CboForms As System.Windows.Forms.ComboBox
    Friend WithEvents cmdGetFuntion As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(InsertFunction))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmdGetFuntion = New System.Windows.Forms.Button()
        Me.CboForms = New System.Windows.Forms.ComboBox()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.txtDllName = New System.Windows.Forms.TextBox()
        Me.txtFunctionName = New System.Windows.Forms.TextBox()
        Me.chkEnable = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdGetFuntion)
        Me.GroupBox1.Controls.Add(Me.CboForms)
        Me.GroupBox1.Controls.Add(Me.txtDesc)
        Me.GroupBox1.Controls.Add(Me.txtDllName)
        Me.GroupBox1.Controls.Add(Me.txtFunctionName)
        Me.GroupBox1.Controls.Add(Me.chkEnable)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(542, 322)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Thông tin biểu mẫu"
        '
        'cmdGetFuntion
        '
        Me.cmdGetFuntion.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdGetFuntion.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGetFuntion.Image = CType(resources.GetObject("cmdGetFuntion.Image"), System.Drawing.Image)
        Me.cmdGetFuntion.Location = New System.Drawing.Point(512, 57)
        Me.cmdGetFuntion.Name = "cmdGetFuntion"
        Me.cmdGetFuntion.Size = New System.Drawing.Size(24, 21)
        Me.cmdGetFuntion.TabIndex = 1
        Me.cmdGetFuntion.TabStop = False
        '
        'CboForms
        '
        Me.CboForms.Location = New System.Drawing.Point(108, 89)
        Me.CboForms.Name = "CboForms"
        Me.CboForms.Size = New System.Drawing.Size(398, 24)
        Me.CboForms.TabIndex = 2
        '
        'txtDesc
        '
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.Location = New System.Drawing.Point(108, 119)
        Me.txtDesc.MaxLength = 1000
        Me.txtDesc.Multiline = True
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDesc.Size = New System.Drawing.Size(398, 159)
        Me.txtDesc.TabIndex = 4
        '
        'txtDllName
        '
        Me.txtDllName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDllName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDllName.Location = New System.Drawing.Point(108, 56)
        Me.txtDllName.MaxLength = 100
        Me.txtDllName.Name = "txtDllName"
        Me.txtDllName.Size = New System.Drawing.Size(398, 22)
        Me.txtDllName.TabIndex = 1
        '
        'txtFunctionName
        '
        Me.txtFunctionName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFunctionName.Location = New System.Drawing.Point(108, 24)
        Me.txtFunctionName.MaxLength = 150
        Me.txtFunctionName.Name = "txtFunctionName"
        Me.txtFunctionName.Size = New System.Drawing.Size(428, 22)
        Me.txtFunctionName.TabIndex = 0
        '
        'chkEnable
        '
        Me.chkEnable.Checked = True
        Me.chkEnable.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkEnable.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEnable.Location = New System.Drawing.Point(108, 284)
        Me.chkEnable.Name = "chkEnable"
        Me.chkEnable.Size = New System.Drawing.Size(251, 32)
        Me.chkEnable.TabIndex = 5
        Me.chkEnable.Text = "Kích hoạt ngay sau khi thêm"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(5, 118)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(101, 16)
        Me.Label4.TabIndex = 31
        Me.Label4.Text = "Mô tả thêm"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(5, 94)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(101, 16)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "Tên Form"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(5, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 16)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Tên DLL"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(5, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 16)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Tên chức năng"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdSave
        '
        Me.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSave.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSave.Location = New System.Drawing.Point(376, 328)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(80, 25)
        Me.cmdSave.TabIndex = 6
        Me.cmdSave.Text = "&Ghi"
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdClose.Location = New System.Drawing.Point(462, 328)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(80, 25)
        Me.cmdClose.TabIndex = 17
        Me.cmdClose.Text = "Th&oát"
        '
        'InsertFunction
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(550, 358)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "InsertFunction"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "Thêm mới biểu mẫu"
        Me.Text = "Thêm chức năng"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Properties"
    Public Property ps_FunctionName() As String
        Get
            Return mv_sFunctionName
        End Get
        Set(ByVal Value As String)
            mv_sFunctionName = Value
        End Set
    End Property
    Public Property ps_iID() As Integer
        Get
            Return mv_iID
        End Get
        Set(ByVal Value As Integer)
            mv_iID = Value
        End Set
    End Property
    Public Property ps_iStatus() As Integer
        Get
            Return mv_iStatus
        End Get
        Set(ByVal Value As Integer)
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
    Public Property pb_Enable() As Boolean
        Get
            Return mv_bEnable
        End Get
        Set(ByVal Value As Boolean)
            mv_bEnable = Value
        End Set
    End Property
#End Region

    Private Sub InsertFunction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Select Case mv_iStatus
                Case 0 'Cập nhật
                    GetDataForUpdate()
                Case 1
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdBuild_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub _KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Try
            If Asc(e.KeyChar) = Keys.Enter Then e.Handled = True
        Catch ex As Exception
        End Try
    End Sub
    Private Sub GetDataForUpdate()
        Dim clsFunction As New clsFunction
        Dim sv_DS As New DataSet
        Try
            mv_sOldFunctionName = mv_sFunctionName
            sv_DS = clsFunction.dsGetFunctionInfor(mv_iID)
            If sv_DS.Tables(0).Rows.Count > 0 Then
                txtFunctionName.Text = sv_DS.Tables(0).Rows(0)("sFunctionName")
                txtDllName.Text = sv_DS.Tables(0).Rows(0)("sDLLName")
                CboForms.Text = sv_DS.Tables(0).Rows(0)("sFormName")

                txtDesc.Text = sv_DS.Tables(0).Rows(0)("sDesc")
                chkEnable.Checked = IIf(sv_DS.Tables(0).Rows(0)("bEnable") = 0, False, True)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim sv_oMenu As New clsFunction
        Dim sv_sFunctionName As String = ""
        Dim sv_sDLLName As String = ""
        Dim sv_sFormName As String = ""
        Dim sv_sDesc As String = ""
        Dim sv_bEnable, sv_bChangeName As Boolean
        Try
            sv_sFunctionName = txtFunctionName.Text.Trim.Replace("'", "''")
            sv_sDLLName = txtDllName.Text.Trim.Replace("'", "''")
            sv_sFormName = CboForms.Text.Trim.Replace("'", "''")

            sv_sDesc = txtDesc.Text.Trim.Replace("'", "''")
            sv_bEnable = chkEnable.Checked
            mv_sFunctionName = sv_sFunctionName
            If Not mf_bCheckData() Then
                Return
            End If
            If mv_sOldFunctionName.Trim.Equals(String.Empty) Then
            Else
                If mv_sOldFunctionName.Trim.ToUpper.Equals(mv_sFunctionName.Trim.ToUpper) Then 'Không thay đổi về tên
                    sv_bChangeName = False
                Else 'Có thay đổi về tên-->yêu cầu tên mới được thay đổi không đuợc trùng trong hệ thống
                    sv_bChangeName = True
                End If
            End If
            If sv_oMenu.bAddNew(gv_sBranchID, mv_iID, sv_sFunctionName, sv_sDLLName, sv_sFormName, sv_sDLLName & "." & sv_sFormName, sv_bEnable, sv_sDesc, sv_bChangeName, mv_iStatus) Then
                mv_bSuccess = True
                mv_bEnable = sv_bEnable
                Select Case mv_iStatus
                    Case 0
                        Dim sv_oDR As DataRow
                        'Tiến hành cập nhật trong danh sách các chức năng để tạo thay đổi trên giao diện người dùng
                        For Each sv_oDR In gv_dsFunction.Tables(0).Select("PK_iID =" & mv_iID)
                            With sv_oDR
                                .Item("sFunctionName") = sv_sFunctionName
                                .Item("sDLLName") = sv_sDLLName
                                .Item("sFormName") = sv_sFormName

                                .Item("sAssemblyName") = sv_sDLLName & "." & sv_sFormName
                                .Item("sDesc") = sv_sDesc
                                .Item("bEnable") = sv_bEnable
                            End With
                        Next
                        'Tiến hành cập nhật trong danh sách các Role để tạo thay đổi trên giao diện người dùng
                        For Each sv_oDR In gv_dsRole.Tables(0).Select("FK_iFunctionID=" & mv_iID)
                            With sv_oDR
                                .Item("sFunctionName") = sv_sFunctionName
                                .Item("sDLLName") = sv_sDLLName
                                .Item("sFormName") = sv_sFormName
                                .Item("sDesc") = sv_sDesc
                                .Item("bEnable") = sv_bEnable
                            End With
                        Next
                        gv_dsFunction.Tables(0).AcceptChanges()
                        gv_dsRole.Tables(0).AcceptChanges()
                        If gv_bAnnounceAfterUpdatingSuccessfully Then
                            MessageBox.Show(IIf(mv_iStatus = 1, "Thêm mới chức năng thành công!", "Cập nhật chức năng thành công").ToString, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                        Me.Close()
                    Case 1
                        mv_iID = sv_oMenu.GetBiggestID
                        Dim sv_oDR As DataRow
                        sv_oDR = gv_dsFunction.Tables(0).NewRow
                        With sv_oDR
                            .Item("PK_iID") = mv_iID
                            .Item("sFunctionName") = sv_sFunctionName
                            .Item("sDLLName") = sv_sDLLName
                            .Item("sFormName") = sv_sFormName
                            .Item("sAssemblyName") = sv_sDLLName & "." & sv_sFormName
                            .Item("sDesc") = sv_sDesc
                            .Item("bEnable") = sv_bEnable
                        End With
                        gv_dsFunction.Tables(0).Rows.Add(sv_oDR)
                        gv_dsFunction.Tables(0).AcceptChanges()
                        If gv_bAnnounceAfterInsertingSuccessfully Then
                            MessageBox.Show(IIf(mv_iStatus = 1, "Thêm mới chức năng thành công!", "Cập nhật chức năng thành công").ToString, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                        If gv_bCloseFormAfterDML Then
                            Me.Close()
                        End If
                        'Thêm nút vào cây
                        If mv_bSuccess Then
                            Dim sv_oNode As New TreeNode(mv_sFunctionName)
                            With sv_oNode
                                .Tag = "LEAFFUNCTIONS#" & mv_iID
                                .SelectedImageIndex = ImageIndex.NodeFuntion
                                .ImageIndex = ImageIndex.NodeFuntion
                                .NodeFont = New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
                            End With
                            'Thêm vào DataSet chức năng này
                            gv_oMainForm.tvwAdminSystem.SelectedNode.Nodes.Add(sv_oNode)
                            gv_oMainForm.tvwAdminSystem.SelectedNode.ExpandAll()
                        End If
                        txtFunctionName.Text = ""
                        CboForms.Text = ""
                        txtDesc.Text = ""
                        txtFunctionName.Focus()
                End Select
            Else
                MessageBox.Show("Đã tồn tại chức năng có tên:" & sv_sFunctionName, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtFunctionName.Focus()
                mv_bSuccess = False
            End If
        Catch ex As Exception
            mv_bSuccess = False
        End Try
    End Sub
    Private Function mf_bCheckData() As Boolean
        Select Case mv_iStatus
            Case 0 'Update
                Return True
            Case 1 'Insert
                If txtFunctionName.Text.Trim = "" Then
                    MessageBox.Show("Bạn phải nhập tên chức năng", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtFunctionName.Focus()
                    Return False
                End If
                If txtDllName.Text.Trim = "" Then
                    MessageBox.Show("Bạn phải nhập tên phân hệ", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtDllName.Focus()
                    Return False
                End If
                If CboForms.Text.Trim = "" Then
                    MessageBox.Show("Bạn phải nhập tên hàm", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    CboForms.Focus()
                    Return False
                End If
                Return True
        End Select
    End Function
    Private Sub InsertFunction_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub GetAllFrmOfAssembly(ByVal ASS As System.Reflection.Assembly)
        Dim t As Type
        CboForms.Items.Clear()
        Try
            For Each t In ASS.GetTypes
                If Not IsDBNull(t.BaseType) And Not t.BaseType Is Nothing And (t.BaseType.FullName.ToUpper = "VietBaIT.CommonLibrary.BaseForm".ToUpper OrElse t.GetType().FullName.Equals(New Form().GetType().FullName) OrElse t.BaseType.FullName.ToUpper = "SYSTEM.WINDOWS.FORMS.FORM" OrElse t.GetType().BaseType.BaseType.FullName.Equals(New Form().GetType().FullName)) Then
                    CboForms.Items.Add(t.Name)
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmdGetFuntion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetFuntion.Click
        Dim openDlg As New OpenFileDialog
        Try
            openDlg.Filter = "DLL|*.DLL"
            If openDlg.ShowDialog = DialogResult.OK Then
                Dim s As String
                s = openDlg.FileName.Substring(openDlg.FileName.LastIndexOf("\") + 1)
                txtDllName.Text = s.ToUpper.Replace(".DLL", "").Trim
                Dim Ass As [Assembly]
                Ass = [Assembly].LoadFrom(openDlg.FileName)
                GetAllFrmOfAssembly(Ass)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CboForms_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CboForms.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                cmdSave.Focus()
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
