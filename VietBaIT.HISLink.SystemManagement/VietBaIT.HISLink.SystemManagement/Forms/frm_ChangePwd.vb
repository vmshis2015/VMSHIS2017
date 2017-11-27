Public Class frm_ChangePwd
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        txtOldPwd.Focus()
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
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmdLogin As System.Windows.Forms.Button
    Friend WithEvents txtConfirm As System.Windows.Forms.TextBox
    Friend WithEvents txtNewPwd As System.Windows.Forms.TextBox
    Friend WithEvents txtOldPwd As System.Windows.Forms.TextBox
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frm_ChangePwd))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtOldPwd = New System.Windows.Forms.TextBox
        Me.txtConfirm = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtNewPwd = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmdLogin = New System.Windows.Forms.Button
        Me.cmdClose = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtOldPwd)
        Me.GroupBox1.Controls.Add(Me.txtConfirm)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtNewPwd)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 57)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(339, 114)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'txtOldPwd
        '
        Me.txtOldPwd.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOldPwd.Location = New System.Drawing.Point(120, 28)
        Me.txtOldPwd.MaxLength = 25
        Me.txtOldPwd.Name = "txtOldPwd"
        Me.txtOldPwd.PasswordChar = Microsoft.VisualBasic.ChrW(42)
        Me.txtOldPwd.Size = New System.Drawing.Size(204, 22)
        Me.txtOldPwd.TabIndex = 0
        Me.txtOldPwd.Text = ""
        '
        'txtConfirm
        '
        Me.txtConfirm.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConfirm.Location = New System.Drawing.Point(120, 81)
        Me.txtConfirm.MaxLength = 25
        Me.txtConfirm.Name = "txtConfirm"
        Me.txtConfirm.PasswordChar = Microsoft.VisualBasic.ChrW(42)
        Me.txtConfirm.Size = New System.Drawing.Size(204, 22)
        Me.txtConfirm.TabIndex = 2
        Me.txtConfirm.Text = ""
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(9, 78)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 20)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Xác nhận lại"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNewPwd
        '
        Me.txtNewPwd.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewPwd.Location = New System.Drawing.Point(120, 54)
        Me.txtNewPwd.MaxLength = 25
        Me.txtNewPwd.Name = "txtNewPwd"
        Me.txtNewPwd.PasswordChar = Microsoft.VisualBasic.ChrW(42)
        Me.txtNewPwd.Size = New System.Drawing.Size(204, 22)
        Me.txtNewPwd.TabIndex = 1
        Me.txtNewPwd.Text = ""
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 20)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Mật khẩu mới"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 20)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Mật khẩu cũ"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Navy
        Me.Label6.Location = New System.Drawing.Point(39, 30)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(243, 16)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Thay đổi mật khẩu quản trị"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.Label5.Location = New System.Drawing.Point(18, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(184, 16)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Thay đổi mật khẩu"
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(291, -9)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(48, 66)
        Me.PictureBox1.TabIndex = 13
        Me.PictureBox1.TabStop = False
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Window
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(342, 57)
        Me.Label1.TabIndex = 14
        '
        'cmdLogin
        '
        Me.cmdLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdLogin.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLogin.Image = CType(resources.GetObject("cmdLogin.Image"), System.Drawing.Image)
        Me.cmdLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdLogin.Location = New System.Drawing.Point(78, 186)
        Me.cmdLogin.Name = "cmdLogin"
        Me.cmdLogin.Size = New System.Drawing.Size(92, 25)
        Me.cmdLogin.TabIndex = 4
        Me.cmdLogin.Text = "&Thay đổi"
        Me.cmdLogin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdClose.Location = New System.Drawing.Point(186, 186)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(87, 25)
        Me.cmdClose.TabIndex = 17
        Me.cmdClose.Text = "Th&oát"
        '
        'frm_ChangePwd
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(343, 221)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdLogin)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_ChangePwd"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Thay đổi mật khẩu Quản trị"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub cmdLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLogin.Click
        Dim sv_sPWD As String = String.Empty
        Dim sv_oEncrypt As New VietBaIT.Encrypt(gv_sSymmetricAlgorithmName)
        Dim sv_oUser As New clsUser
        Try

            sv_sPWD = sv_oEncrypt.Mahoa(txtOldPwd.Text.Trim)
            If Not txtNewPwd.Text.Trim.Equals(txtConfirm.Text.Trim) Then
                MessageBox.Show("Mật khẩu xác nhận phải giống mật khẩu mới!", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtConfirm.Focus()
                Return
            End If
            'Kiểm tra xem đã nhập mật khẩu cũ đúng hay chưa?
            If Not sv_oUser.bLoginSuccessAdmin(gv_sUID, sv_sPWD) Then
                MessageBox.Show("Sai mật khẩu đăng nhập", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtOldPwd.Focus()
                Return
            End If
            'Kiểm tra xem mật khẩu cũ và mật khẩu mới có giống nhau không
            If txtNewPwd.Text.Trim.Equals(txtOldPwd.Text.Trim) Then
                MessageBox.Show("Đã thay đổi mật khẩu thành công!", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
                Return
            End If
            If sv_oUser.bChangePasswordForAdmin(gv_sUID, sv_oEncrypt.Mahoa(txtNewPwd.Text.Trim)) Then
                MessageBox.Show("Đã thay đổi mật khẩu thành công!", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
                Return
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frm_ChangePwd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtOldPwd.Focus()
    End Sub
    Private Sub frm_ChangePwd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Enter
                    ProcessTabKey(True)
                Case Keys.Escape
                    Me.Close()
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
End Class
