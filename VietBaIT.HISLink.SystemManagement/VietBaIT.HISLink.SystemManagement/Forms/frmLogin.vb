Imports System.IO
Imports clsRegistry.clsRegistry
Public Class frmLogin
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        gv_bIncreateOrDecrete = True
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmdLogin As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtPWD As System.Windows.Forms.TextBox
    Friend WithEvents txtUID As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmLogin))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.cmdLogin = New System.Windows.Forms.Button
        Me.cmdClose = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtPWD = New System.Windows.Forms.TextBox
        Me.txtUID = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(364, 64)
        Me.Label1.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.White
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(216, 16)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Đăng nhập quản trị hệ thống"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.White
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(16, 28)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(272, 16)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Chứng thực quyền sử dụng của người dùng"
        '
        'cmdLogin
        '
        Me.cmdLogin.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLogin.Location = New System.Drawing.Point(76, 176)
        Me.cmdLogin.Name = "cmdLogin"
        Me.cmdLogin.Size = New System.Drawing.Size(92, 28)
        Me.cmdLogin.TabIndex = 2
        Me.cmdLogin.Text = "&Chấp nhận"
        '
        'cmdClose
        '
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.Location = New System.Drawing.Point(176, 176)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(92, 28)
        Me.cmdClose.TabIndex = 3
        Me.cmdClose.Text = "&Kết thúc"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtPWD)
        Me.GroupBox1.Controls.Add(Me.txtUID)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(0, 68)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(336, 96)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Thông tin đăng nhập"
        '
        'txtPWD
        '
        Me.txtPWD.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPWD.Location = New System.Drawing.Point(120, 56)
        Me.txtPWD.MaxLength = 25
        Me.txtPWD.Name = "txtPWD"
        Me.txtPWD.PasswordChar = Microsoft.VisualBasic.ChrW(42)
        Me.txtPWD.Size = New System.Drawing.Size(184, 22)
        Me.txtPWD.TabIndex = 1
        Me.txtPWD.Text = ""
        '
        'txtUID
        '
        Me.txtUID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtUID.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUID.Location = New System.Drawing.Point(120, 24)
        Me.txtUID.MaxLength = 25
        Me.txtUID.Name = "txtUID"
        Me.txtUID.Size = New System.Drawing.Size(184, 22)
        Me.txtUID.TabIndex = 0
        Me.txtUID.Text = ""
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 20)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Mật khẩu"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 20)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Tên đăng nhập"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.BackColor = System.Drawing.Color.AliceBlue
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(284, -4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(48, 68)
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'frmLogin
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(338, 219)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdLogin)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmLogin"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Đăng nhập hệ thống"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Function mf_bCheckData() As Boolean
        Try
            If txtUID.Text.Trim.Equals(String.Empty) Then
                MessageBox.Show("Bạn phải nhập tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub frmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sv_sUID, sv_sPwd As String
        Dim _clsRegistry As New clsRegistry.clsRegistry
        Try
            sv_sUID = VBITJSC.RegConfiguration.GetSettings("DVC_COMPANY", "Sys_MAN_DVC", "Sys_MAN_UID")
            sv_sPwd = VBITJSC.RegConfiguration.GetSettings("DVC_COMPANY", "Sys_MAN_DVC", "Sys_MAN_PWD")
            txtUID.Text = sv_sUID.Trim
            'txtPWD.Text = sv_sPwd.Trim
            txtUID.Focus()
        Catch ex As Exception

        End Try

    End Sub
    '------------------------------------------------------------------------------------------------------------
    'Mục đích        :Kiểm tra sự tồn tại của một số DLL cần thiết
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       : CuongDV
    'Ngày tạo         :05/03/2006
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Private Function CheckFileNotFound() As Boolean
        Dim s As String = ""
        Try
            If Not File.Exists(Application.StartupPath & "\Encrypt.Dll") Then
                MessageBox.Show("Bạn cần phải có file Encrypt.Dll để thực hiện giải mã mật khẩu quản trị", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If
            If Not File.Exists(Application.StartupPath & "\data.dat") Then
                MessageBox.Show("Bạn cần phải có file data.dat để hỗ trợ việc mã hóa và giải mã dữ liệu", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If
            Return True
        Catch ex As Exception
            AppLogger.NLogAction.Log.Trace("CheckFileNotFound.Excaption..." + ex.Message)
        End Try
    End Function
    Private Sub _KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Try
            If Asc(e.KeyChar) = Keys.Enter Then e.Handled = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmdLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLogin.Click
        Dim sv_sUID As String = String.Empty
        Dim sv_sPWD As String = String.Empty
        Dim sv_oEncrypt As New VietBaIT.Encrypt(gv_sSymmetricAlgorithmName)
        Dim _clsRegistry As New clsRegistry.clsRegistry
        Try
            If mf_bCheckData() Then
                Me.Cursor = Cursors.WaitCursor
                gv_sUID = txtUID.Text.Trim
                AppLogger.NLogAction.Log.Trace("Mahoa...")
                gv_sPWD = sv_oEncrypt.Mahoa(txtPWD.Text.Trim)
                If bLoginSuccess() Then
                    VBITJSC.RegConfiguration.SaveSettings("DVC_COMPANY", "Sys_MAN_DVC", "Sys_MAN_UID", txtUID.Text.Trim)
                    VBITJSC.RegConfiguration.SaveSettings("DVC_COMPANY", "Sys_MAN_DVC", "Sys_MAN_PWD", txtPWD.Text.Trim)
                    AppLogger.NLogAction.Log.Trace("SaveSettings OK...")
                    gv_bLoginSuccess = True
                    Me.Close()
                Else
                    gv_bLoginSuccess = False
                End If

            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            AppLogger.NLogAction.Log.Trace("cmdLogin_Click.Exception-->" + ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Function bLoginSuccess() As Boolean
        Dim sv_oUser As New clsUser
        If Not gv_ConnectSuccess Then
            Return False
        End If
        AppLogger.NLogAction.Log.Trace("Check bIsExistedAmin")
        If Not sv_oUser.bIsExistedAmin(gv_sUID) Then
            MessageBox.Show("Không tồn tại người quản trị có tên đăng nhập là " & gv_sUID & ". Đề nghị nhập lại", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtUID.Focus()
            Return False
        End If
        AppLogger.NLogAction.Log.Trace("Check bLoginSuccessAdmin")
        If Not sv_oUser.bLoginSuccessAdmin(gv_sUID, gv_sPWD) Then
            MessageBox.Show("Sai mật khẩu đăng nhập", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtPWD.Focus()
            Return False
        End If
        AppLogger.NLogAction.Log.Trace("bLoginSuccess=true-->")
        Return True
    End Function
    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Try
            'gv_bLoginSuccess = False
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub frmLogin_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then ProcessTabKey(True)
    End Sub
End Class
