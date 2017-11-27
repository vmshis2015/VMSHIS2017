Imports System.IO
Public Class frm_BackupAndRestore
    Inherits System.Windows.Forms.Form
    Dim mv_SQLServer As SQLDMO.SQLServerClass
    Dim strConnect As String

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

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
    Friend WithEvents cmdPath_file As System.Windows.Forms.Button
    Friend WithEvents txtFile As System.Windows.Forms.TextBox
    Friend WithEvents label4 As System.Windows.Forms.Label
    Friend WithEvents txtServer_name As System.Windows.Forms.TextBox
    Friend WithEvents chkWindows As System.Windows.Forms.CheckBox
    Friend WithEvents cmdDisconnect As System.Windows.Forms.Button
    Friend WithEvents cmdConnect As System.Windows.Forms.Button
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents cmdBackup As System.Windows.Forms.Button
    Friend WithEvents optRestore As System.Windows.Forms.RadioButton
    Friend WithEvents optBackup As System.Windows.Forms.RadioButton
    Friend WithEvents lstDBList As System.Windows.Forms.ListBox
    Friend WithEvents chkGetAccFromConfigFile As System.Windows.Forms.CheckBox
    Friend WithEvents grbBackUp As System.Windows.Forms.GroupBox
    Friend WithEvents grbConnect As System.Windows.Forms.GroupBox
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frm_BackupAndRestore))
        Me.grbBackUp = New System.Windows.Forms.GroupBox
        Me.lstDBList = New System.Windows.Forms.ListBox
        Me.cmdClose = New System.Windows.Forms.Button
        Me.cmdPath_file = New System.Windows.Forms.Button
        Me.cmdBackup = New System.Windows.Forms.Button
        Me.optRestore = New System.Windows.Forms.RadioButton
        Me.optBackup = New System.Windows.Forms.RadioButton
        Me.txtFile = New System.Windows.Forms.TextBox
        Me.label4 = New System.Windows.Forms.Label
        Me.grbConnect = New System.Windows.Forms.GroupBox
        Me.chkGetAccFromConfigFile = New System.Windows.Forms.CheckBox
        Me.txtServer_name = New System.Windows.Forms.TextBox
        Me.chkWindows = New System.Windows.Forms.CheckBox
        Me.cmdDisconnect = New System.Windows.Forms.Button
        Me.cmdConnect = New System.Windows.Forms.Button
        Me.label3 = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.txtUser = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.grbBackUp.SuspendLayout()
        Me.grbConnect.SuspendLayout()
        Me.SuspendLayout()
        '
        'grbBackUp
        '
        Me.grbBackUp.Controls.Add(Me.lstDBList)
        Me.grbBackUp.Controls.Add(Me.cmdClose)
        Me.grbBackUp.Controls.Add(Me.cmdPath_file)
        Me.grbBackUp.Controls.Add(Me.cmdBackup)
        Me.grbBackUp.Controls.Add(Me.optRestore)
        Me.grbBackUp.Controls.Add(Me.optBackup)
        Me.grbBackUp.Controls.Add(Me.txtFile)
        Me.grbBackUp.Controls.Add(Me.label4)
        Me.grbBackUp.Location = New System.Drawing.Point(237, 63)
        Me.grbBackUp.Name = "grbBackUp"
        Me.grbBackUp.Size = New System.Drawing.Size(249, 264)
        Me.grbBackUp.TabIndex = 45
        Me.grbBackUp.TabStop = False
        Me.grbBackUp.Text = "Sao lưu - Phục hồi Cơ sở dữ liệu"
        '
        'lstDBList
        '
        Me.lstDBList.Location = New System.Drawing.Point(6, 39)
        Me.lstDBList.Name = "lstDBList"
        Me.lstDBList.Size = New System.Drawing.Size(237, 95)
        Me.lstDBList.TabIndex = 32
        '
        'cmdClose
        '
        Me.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClose.ForeColor = System.Drawing.Color.Black
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdClose.Location = New System.Drawing.Point(141, 228)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(80, 24)
        Me.cmdClose.TabIndex = 31
        Me.cmdClose.Text = "Thoát"
        '
        'cmdPath_file
        '
        Me.cmdPath_file.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdPath_file.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPath_file.Image = CType(resources.GetObject("cmdPath_file.Image"), System.Drawing.Image)
        Me.cmdPath_file.Location = New System.Drawing.Point(201, 186)
        Me.cmdPath_file.Name = "cmdPath_file"
        Me.cmdPath_file.Size = New System.Drawing.Size(24, 20)
        Me.cmdPath_file.TabIndex = 30
        Me.cmdPath_file.TabStop = False
        '
        'cmdBackup
        '
        Me.cmdBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdBackup.Image = CType(resources.GetObject("cmdBackup.Image"), System.Drawing.Image)
        Me.cmdBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdBackup.Location = New System.Drawing.Point(32, 228)
        Me.cmdBackup.Name = "cmdBackup"
        Me.cmdBackup.Size = New System.Drawing.Size(88, 24)
        Me.cmdBackup.TabIndex = 29
        Me.cmdBackup.Text = "Sao lưu"
        '
        'optRestore
        '
        Me.optRestore.Location = New System.Drawing.Point(9, 165)
        Me.optRestore.Name = "optRestore"
        Me.optRestore.Size = New System.Drawing.Size(233, 16)
        Me.optRestore.TabIndex = 28
        Me.optRestore.Text = "Khôi phục dữ liệu từ File"
        '
        'optBackup
        '
        Me.optBackup.Checked = True
        Me.optBackup.Location = New System.Drawing.Point(9, 141)
        Me.optBackup.Name = "optBackup"
        Me.optBackup.Size = New System.Drawing.Size(233, 16)
        Me.optBackup.TabIndex = 27
        Me.optBackup.TabStop = True
        Me.optBackup.Text = "Sao lưu dữ liệu đến File"
        '
        'txtFile
        '
        Me.txtFile.BackColor = System.Drawing.SystemColors.Window
        Me.txtFile.Location = New System.Drawing.Point(9, 186)
        Me.txtFile.Name = "txtFile"
        Me.txtFile.Size = New System.Drawing.Size(192, 20)
        Me.txtFile.TabIndex = 26
        Me.txtFile.Text = "c:\NWIND.bak"
        '
        'label4
        '
        Me.label4.Location = New System.Drawing.Point(9, 21)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(237, 16)
        Me.label4.TabIndex = 25
        Me.label4.Text = "Chọn Cơ sở dữ liệu cần sao lưu hoặc phục hồi"
        '
        'grbConnect
        '
        Me.grbConnect.Controls.Add(Me.chkGetAccFromConfigFile)
        Me.grbConnect.Controls.Add(Me.txtServer_name)
        Me.grbConnect.Controls.Add(Me.chkWindows)
        Me.grbConnect.Controls.Add(Me.cmdDisconnect)
        Me.grbConnect.Controls.Add(Me.cmdConnect)
        Me.grbConnect.Controls.Add(Me.label3)
        Me.grbConnect.Controls.Add(Me.label2)
        Me.grbConnect.Controls.Add(Me.label1)
        Me.grbConnect.Controls.Add(Me.txtPassword)
        Me.grbConnect.Controls.Add(Me.txtUser)
        Me.grbConnect.Location = New System.Drawing.Point(3, 63)
        Me.grbConnect.Name = "grbConnect"
        Me.grbConnect.Size = New System.Drawing.Size(237, 264)
        Me.grbConnect.TabIndex = 46
        Me.grbConnect.TabStop = False
        Me.grbConnect.Text = "Đăng nhập Cơ sở dữ liệu"
        '
        'chkGetAccFromConfigFile
        '
        Me.chkGetAccFromConfigFile.Location = New System.Drawing.Point(9, 183)
        Me.chkGetAccFromConfigFile.Name = "chkGetAccFromConfigFile"
        Me.chkGetAccFromConfigFile.Size = New System.Drawing.Size(225, 27)
        Me.chkGetAccFromConfigFile.TabIndex = 60
        Me.chkGetAccFromConfigFile.Text = "Lấy tài khoản đăng nhập từ file cấu hình"
        '
        'txtServer_name
        '
        Me.txtServer_name.Location = New System.Drawing.Point(12, 45)
        Me.txtServer_name.Name = "txtServer_name"
        Me.txtServer_name.Size = New System.Drawing.Size(198, 20)
        Me.txtServer_name.TabIndex = 59
        Me.txtServer_name.Text = "(local)"
        '
        'chkWindows
        '
        Me.chkWindows.Location = New System.Drawing.Point(12, 69)
        Me.chkWindows.Name = "chkWindows"
        Me.chkWindows.Size = New System.Drawing.Size(152, 24)
        Me.chkWindows.TabIndex = 58
        Me.chkWindows.Text = "Windows Authentication"
        '
        'cmdDisconnect
        '
        Me.cmdDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDisconnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdDisconnect.Location = New System.Drawing.Point(126, 228)
        Me.cmdDisconnect.Name = "cmdDisconnect"
        Me.cmdDisconnect.Size = New System.Drawing.Size(80, 24)
        Me.cmdDisconnect.TabIndex = 57
        Me.cmdDisconnect.Text = "&Ngắt kết nối"
        '
        'cmdConnect
        '
        Me.cmdConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdConnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdConnect.Location = New System.Drawing.Point(21, 228)
        Me.cmdConnect.Name = "cmdConnect"
        Me.cmdConnect.Size = New System.Drawing.Size(80, 24)
        Me.cmdConnect.TabIndex = 56
        Me.cmdConnect.Text = "&Kết nối"
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(9, 141)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(198, 16)
        Me.label3.TabIndex = 55
        Me.label3.Text = "Mật khẩu đăng nhập(Password)"
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(9, 96)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(153, 16)
        Me.label2.TabIndex = 54
        Me.label2.Text = "Tên đăng nhập(UserName)"
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(12, 27)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(198, 16)
        Me.label1.TabIndex = 53
        Me.label1.Text = "Nhập tên máy chủ CSDL"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(9, 162)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(198, 20)
        Me.txtPassword.TabIndex = 52
        Me.txtPassword.Text = ""
        '
        'txtUser
        '
        Me.txtUser.Location = New System.Drawing.Point(9, 117)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Size = New System.Drawing.Size(198, 20)
        Me.txtUser.TabIndex = 51
        Me.txtUser.Text = "sa"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Window
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(486, 60)
        Me.Label5.TabIndex = 47
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 6)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(297, 16)
        Me.Label6.TabIndex = 48
        Me.Label6.Text = "Sao lưu và phục hồi dữ liệu SQL Server"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(30, 27)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(405, 21)
        Me.Label7.TabIndex = 49
        Me.Label7.Text = "Đăng nhập CSDL SQL Server. Chọn sao lưu hoặc phục hồi dữ liệu"
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(431, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(54, 51)
        Me.PictureBox1.TabIndex = 50
        Me.PictureBox1.TabStop = False
        '
        'frm_BackupAndRestore
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(489, 332)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.grbConnect)
        Me.Controls.Add(Me.grbBackUp)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_BackupAndRestore"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sao lưu và phục hồi dữ liệu"
        Me.grbBackUp.ResumeLayout(False)
        Me.grbConnect.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frm_BackupAndRestore_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            txtServer_name.Text = "(local)"
            txtUser.Text = "sa"
            txtPassword.Text = "sa"
            optBackup.Checked = True
            cmdBackup.Enabled = False
            cmdDisconnect.Enabled = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConnect.Click
        If txtServer_name.Text = "" Then
            MessageBox.Show("Hãy nhập tên hoặc địa chỉ máy chủ cài CSDL SQL Server!", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Try
            Me.Cursor = Cursors.WaitCursor
            mv_SQLServer = New SQLDMO.SQLServerClass
            lstDBList.Items.Clear()
            strConnect = "server=" & txtServer_name.Text

            If chkWindows.Checked Then
                strConnect &= ";Integrated Security=SSPI"
                mv_SQLServer.Connect(txtServer_name.Text, "sa", "")
            Else
                strConnect += ";User Id=" & txtUser.Text & ";Password=" & txtPassword.Text
                mv_SQLServer.Connect(txtServer_name.Text, txtUser.Text, txtPassword.Text)
            End If
            'Duyệt qua từng CSDL tìm thấy để đưa vào ListBox
            For Each db As SQLDMO.Database In mv_SQLServer.Databases
                If Not db Is Nothing Then
                    lstDBList.Items.Add(db.Name)
                End If
            Next
            If lstDBList.Items.Count > 0 Then
                lstDBList.Sorted = True
            Else
                lstDBList.Text = "<No databases found>"
            End If
            Me.Cursor = Cursors.Default
            'Disable các điều khiển phía kết nối
            For Each ctr As Control In grbConnect.Controls
                If ctr.Name.Equals(cmdDisconnect.Name) Then
                    ctr.Enabled = True
                Else
                    ctr.Enabled = False
                End If
            Next
            'Enable các điều khiển phía sao lưu phục hồi dữ liệu
            For Each ctr As Control In grbBackUp.Controls
                ctr.Enabled = True
            Next
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            cmdBackup.Enabled = False
            MessageBox.Show("Không đăng nhập được vào CSDL SQL Server. Mời bạn nhập lại thông tin đăng nhập cho đúng", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            cmdDisconnect.Enabled = False
            cmdConnect.Enabled = True
        End Try
    End Sub

    Private Sub optBackup_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optBackup.CheckedChanged, optRestore.CheckedChanged
        Try
            If optBackup.Checked Then
                cmdBackup.Text = "&Sao lưu"
            Else
                cmdBackup.Text = "&Phục hồi"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdPath_file_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPath_file.Click

        If optBackup.Checked Then
            Dim sv_oSaveDialog As New SaveFileDialog
            If sv_oSaveDialog.ShowDialog = DialogResult.OK Then
                txtFile.Text = sv_oSaveDialog.FileName
            Else
                txtFile.Text = "Chọn lại File sao lưu"
            End If
        Else
            Dim sv_oOpenDialog As New OpenFileDialog
            If sv_oOpenDialog.ShowDialog = DialogResult.OK Then
                txtFile.Text = sv_oOpenDialog.FileName
            Else
                txtFile.Text = "Chọn lại File phục hồi"
            End If
        End If
    End Sub
    Private Sub BackUpDB()
        If lstDBList.SelectedItem Is Nothing Then
            MessageBox.Show("Bạn cần chọn một CSDL để sao lưu", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Try
            Dim sv_SQLServer As New SQLDMO.SQLServerClass
            Dim strConnect As String
            Me.Cursor = Cursors.WaitCursor
            Dim BackUp As New SQLDMO.BackupClass
            BackUp.Devices = BackUp.Files
            If File.Exists(txtFile.Text.Trim) Then
                File.Delete(txtFile.Text.Trim)
            End If
            BackUp.Files = txtFile.Text
            BackUp.Database = lstDBList.SelectedItem.ToString()
            BackUp.SQLBackup(mv_SQLServer)
            MessageBox.Show("Đã sao lưu xong CSDL có tên " & lstDBList.SelectedItem.ToString(), gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show("Bạn hãy đóng CSDL trước khi tiến hành BackUp." & vbCrLf & ex.Message, gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
    Private Sub RestoreDB()
        Try
            If lstDBList.SelectedItem Is Nothing Then
                MessageBox.Show("Bạn cần chọn một CSDL để phục hồi", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            If txtFile.Text.Trim.Equals(String.Empty) Then
                MessageBox.Show("Bạn hãy chọn file lưu dữ liệu trước khi nhấn nút Phục hồi", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            If mv_SQLServer Is Nothing Then mv_SQLServer = New SQLDMO.SQLServerClass
            If lstDBList.SelectedItem.ToString.Equals(gv_sDBName) Then
                VNS.Libs.globalVariables.SqlConn.Dispose()
                GC.Collect()
            End If
            Me.Cursor = Cursors.WaitCursor
            Dim Restore As New SQLDMO.RestoreClass
            Restore.Devices = Restore.Files
            Restore.Files = txtFile.Text
            Restore.ReplaceDatabase = True
            Restore.Database = lstDBList.SelectedItem.ToString()
            Restore.SQLRestore(mv_SQLServer)
            If VNS.Libs.globalVariables.SqlConn.State = ConnectionState.Closed Then VNS.Libs.globalVariables.SqlConn.Open()
            MessageBox.Show("Đã khôi phục thành công CSDL SQL Server", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            If VNS.Libs.globalVariables.SqlConn.State = ConnectionState.Closed Then VNS.Libs.globalVariables.SqlConn.Open()
            Me.Cursor = Cursors.Default
            MessageBox.Show("Bạn chọn sai CSDL để phục hồi hoặc bạn phải đóng tất cả các thao tác mở CSDL SQL Server trước khi thực hiện phục hồi", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub cmdDisconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDisconnect.Click
        Try
            mv_SQLServer.DisConnect()
            mv_SQLServer = Nothing
            lstDBList.Items.Clear()
            'Disable các điều khiển phía sao lưu
            For Each ctr As Control In grbBackUp.Controls
                If ctr.Name.Equals(cmdClose.Name) Then
                    ctr.Enabled = True
                Else
                    ctr.Enabled = False
                End If
            Next
            'Enable các điều khiển phía kết nối
            For Each ctr As Control In grbConnect.Controls
                If ctr.Name.Equals(cmdDisconnect.Name) Then
                    ctr.Enabled = False
                Else
                    ctr.Enabled = True
                End If
            Next
        Catch ex As Exception
            MessageBox.Show("Lỗi khi thực hiện ngắt kết nối CSDL" & vbCrLf & ex.Message, gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Try
            If Not mv_SQLServer Is Nothing Then
                mv_SQLServer.DisConnect()
                mv_SQLServer = Nothing
            End If
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdBackup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBackup.Click
        If optBackup.Checked Then
            BackUpDB()
        Else
            RestoreDB()
        End If
    End Sub

    Private Sub chkGetAccFromConfigFile_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGetAccFromConfigFile.CheckedChanged
        Dim sv_sUID, sv_sPwd As String
        txtUser.Text = gv_sSecretUID
        txtPassword.Text = gv_sSecretPWD
        cmdConnect.Focus()
    End Sub

End Class
