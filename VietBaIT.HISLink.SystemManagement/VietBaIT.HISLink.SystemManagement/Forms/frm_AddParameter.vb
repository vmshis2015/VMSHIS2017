Public Class frm_AddParameter
    Inherits System.Windows.Forms.Form
    Public mv_iStatus As Status
    Public mv_bSuccess As Boolean = False
    Public mv_sParamName As String = "UnKnown"
    Public mv_intID As Integer

#Region " Windows Form Designer generated code "

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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents txtParamValue As System.Windows.Forms.TextBox
    Friend WithEvents txtParamName As System.Windows.Forms.TextBox
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cboParamType As System.Windows.Forms.ComboBox
    Friend WithEvents txt_ID As System.Windows.Forms.TextBox
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_AddParameter))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cboParamType = New System.Windows.Forms.ComboBox
        Me.txtDesc = New System.Windows.Forms.TextBox
        Me.txtParamValue = New System.Windows.Forms.TextBox
        Me.txtParamName = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmdSave = New System.Windows.Forms.Button
        Me.txt_ID = New System.Windows.Forms.TextBox
        Me.cmdClose = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.cboParamType)
        Me.GroupBox1.Controls.Add(Me.txtDesc)
        Me.GroupBox1.Controls.Add(Me.txtParamValue)
        Me.GroupBox1.Controls.Add(Me.txtParamName)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(384, 214)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Thông tin tham số"
        '
        'cboParamType
        '
        Me.cboParamType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboParamType.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboParamType.Items.AddRange(New Object() {"bigint", "boolean", "char", "datetime", "decimal", "float", "int", "money", "numeric", "nvarchar", "real", "smallint"})
        Me.cboParamType.Location = New System.Drawing.Point(117, 115)
        Me.cboParamType.Name = "cboParamType"
        Me.cboParamType.Size = New System.Drawing.Size(261, 24)
        Me.cboParamType.TabIndex = 2
        '
        'txtDesc
        '
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.Location = New System.Drawing.Point(117, 145)
        Me.txtDesc.Multiline = True
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDesc.Size = New System.Drawing.Size(261, 60)
        Me.txtDesc.TabIndex = 3
        '
        'txtParamValue
        '
        Me.txtParamValue.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtParamValue.Location = New System.Drawing.Point(117, 51)
        Me.txtParamValue.MaxLength = 300
        Me.txtParamValue.Multiline = True
        Me.txtParamValue.Name = "txtParamValue"
        Me.txtParamValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtParamValue.Size = New System.Drawing.Size(261, 58)
        Me.txtParamValue.TabIndex = 1
        '
        'txtParamName
        '
        Me.txtParamName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtParamName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtParamName.Location = New System.Drawing.Point(117, 24)
        Me.txtParamName.Name = "txtParamName"
        Me.txtParamName.Size = New System.Drawing.Size(261, 22)
        Me.txtParamName.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 145)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 23)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Diễn giải"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 118)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 23)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Kiểu dữ liệu"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 23)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Giá trị tham số"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Tên tham số"
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSave.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSave.Location = New System.Drawing.Point(201, 226)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(87, 25)
        Me.cmdSave.TabIndex = 4
        Me.cmdSave.Text = "Ghi"
        '
        'txt_ID
        '
        Me.txt_ID.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_ID.Location = New System.Drawing.Point(24, 207)
        Me.txt_ID.Name = "txt_ID"
        Me.txt_ID.Size = New System.Drawing.Size(3, 20)
        Me.txt_ID.TabIndex = 6
        Me.txt_ID.Visible = False
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdClose.Location = New System.Drawing.Point(294, 226)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(87, 25)
        Me.cmdClose.TabIndex = 16
        Me.cmdClose.Text = "Th&oát"
        '
        'frm_AddParameter
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(390, 258)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.txt_ID)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frm_AddParameter"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tham số hệ thống"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub frm_AddParameter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            cboParamType.SelectedIndex = 0
            Select Case mv_iStatus
                Case globalModule.Status.Update 'Cập nhật
                    GetDataForUpdate()
                Case Else
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
        Dim sv_oclsParam As New cls_Parameter
        Dim sv_DS As New DataSet
        Try
            sv_DS = sv_oclsParam.dsGetParamInfor(mv_sParamName)
            If sv_DS.Tables(0).Rows.Count > 0 Then
                txtParamName.Text = sv_DS.Tables(0).Rows(0)("sName")
                txtParamValue.Text = sv_DS.Tables(0).Rows(0)("sValue")
                cboParamType.Text = sv_DS.Tables(0).Rows(0)("sDataType")
                txtDesc.Text = sv_DS.Tables(0).Rows(0)("sDesc")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function mf_bCheckValidData() As Boolean
        Try
            If txtParamName.Text.Trim.Equals(String.Empty) Then
                MessageBox.Show("Bạn phải nhập tên tham số", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtParamName.Focus()
                Return False
            End If
            If txtParamValue.Text.Trim.Equals(String.Empty) Then
                MessageBox.Show("Bạn phải nhập giá trị tham số", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtParamValue.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show("Lỗi cần gặp người lập trình" & ex.Message, gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End Try
    End Function
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim sv_oclsParam As New cls_Parameter
        Dim sv_sName As String = ""
        Dim sv_sValue As String = ""
        Dim sv_sDataType As String = ""
        Dim sv_sDesc As String = ""
        Dim intID As Integer
        Try
            If mf_bCheckValidData() Then
                sv_sName = ValidData(txtParamName.Text)
                sv_sValue = ValidData(txtParamValue.Text)
                sv_sDataType = ValidData(cboParamType.Text)
                sv_sDesc = ValidData(txtDesc.Text)
                Select Case mv_iStatus
                    Case globalModule.Status.Update
                        If Not mv_sParamName.Trim.ToUpper.Equals(sv_sName.Trim.ToUpper) Then
                            If sv_oclsParam.bIsexited(sv_sName, gv_sBranchID) Then
                                MessageBox.Show("Đã tồn tại tham số có tên: " & sv_sName, gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                txtParamName.Focus()
                                mv_bSuccess = False
                                Return
                            End If
                        End If
                        If sv_oclsParam.bAddNew(-1, gv_sBranchID, sv_sName, sv_sValue, sv_sDataType, sv_sDesc, gv_intCurrMonth, gv_intCurrYear, mv_iStatus, mv_sParamName.ToUpper) Then
                            mv_bSuccess = True
                            Dim sv_oDR As DataRow
                            For Each sv_oDR In gv_dsParam.Tables(0).Rows
                                If sv_oDR.Item("sName").ToString.Trim.ToUpper.Equals(mv_sParamName.ToUpper) Then
                                    With sv_oDR
                                        .Item("sName") = sv_sName
                                        .Item("sValue") = sv_sValue
                                        .Item("sDataType") = sv_sDataType
                                        .Item("sDesc") = sv_sDesc
                                    End With
                                    Exit For
                                End If
                            Next
                            gv_dsParam.Tables(0).AcceptChanges()
                            mv_sParamName = sv_sName
                            Me.Close()
                        Else
                            mv_bSuccess = False
                            Return
                        End If
                    Case globalModule.Status.Insert
                        If sv_oclsParam.bIsexited(sv_sName, gv_sBranchID) Then
                            MessageBox.Show("Đã tồn tại tham số có tên: " & sv_sName, gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            txtParamName.Focus()
                            mv_bSuccess = False
                            Return
                        End If
                        If sv_oclsParam.bAddNew(-1, gv_sBranchID, sv_sName, sv_sValue, sv_sDataType, sv_sDesc, gv_intCurrMonth, gv_intCurrYear, mv_iStatus) Then
                            mv_bSuccess = True
                            Dim sv_oDR As DataRow
                            sv_odr = gv_dsParam.Tables(0).NewRow
                            With sv_oDR
                                .Item("sName") = sv_sName
                                .Item("sValue") = sv_sValue
                                .Item("sDataType") = sv_sDataType
                                .Item("iMonth") = gv_intCurrMonth
                                .Item("iYear") = gv_intCurrYear
                                .Item("sDesc") = sv_sDesc
                            End With
                            gv_dsParam.Tables(0).Rows.Add(sv_odr)
                            gv_dsParam.Tables(0).AcceptChanges()
                            mv_sParamName = sv_sName
                            '***************************************************
                            If mv_bSuccess Then
                                Dim sv_oNode As New TreeNode(mv_sParamName)
                                With sv_oNode
                                    .Tag = "NODEPARAM"
                                    .SelectedImageIndex = ImageIndex.NodeParam
                                    .ImageIndex = ImageIndex.NodeParam
                                    .NodeFont = New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
                                End With
                                'Thêm vào DataSet chức năng này
                                gv_oMainForm.tvwAdminSystem.SelectedNode.Nodes.Add(sv_oNode)
                                gv_oMainForm.tvwAdminSystem.SelectedNode.ExpandAll()
                            End If
                            If gv_bCloseFormAfterDML Then
                                Me.Close()
                            Else
                                txtParamName.Text = ""
                                txtParamValue.Text = ""
                                txtDesc.Text = ""
                                txtParamName.Focus()
                            End If
                            '***************************************************
                        Else
                            MessageBox.Show("Đã tồn tại tham số có tên: " & sv_sName, gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            txtParamName.Focus()
                            mv_bSuccess = False
                            Return
                        End If
                End Select
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            mv_bSuccess = False
        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frm_AddParameter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                processtabkey(True)
            Case Keys.Escape
                Me.Close()
        End Select
    End Sub
End Class
