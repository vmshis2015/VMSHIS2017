Public Class frm_GetXMLFile
    Inherits System.Windows.Forms.Form
    Public mv_bCancel As Boolean = True
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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdPath_file As System.Windows.Forms.Button
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
    Friend WithEvents cmdNext As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frm_GetXMLFile))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmdPath_file = New System.Windows.Forms.Button
        Me.txtFilePath = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmdNext = New System.Windows.Forms.Button
        Me.cmdClose = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdPath_file)
        Me.GroupBox1.Controls.Add(Me.txtFilePath)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 59)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(345, 67)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'cmdPath_file
        '
        Me.cmdPath_file.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdPath_file.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPath_file.Image = CType(resources.GetObject("cmdPath_file.Image"), System.Drawing.Image)
        Me.cmdPath_file.Location = New System.Drawing.Point(309, 27)
        Me.cmdPath_file.Name = "cmdPath_file"
        Me.cmdPath_file.Size = New System.Drawing.Size(24, 22)
        Me.cmdPath_file.TabIndex = 31
        Me.cmdPath_file.TabStop = False
        Me.ToolTip1.SetToolTip(Me.cmdPath_file, "Click vào đây để hiện thư mục chứa file cấu hình XML cần nạp")
        '
        'txtFilePath
        '
        Me.txtFilePath.Enabled = False
        Me.txtFilePath.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilePath.Location = New System.Drawing.Point(81, 27)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.Size = New System.Drawing.Size(228, 22)
        Me.txtFilePath.TabIndex = 1
        Me.txtFilePath.Text = ""
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 18)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Đường dẫn"
        '
        'cmdNext
        '
        Me.cmdNext.Enabled = False
        Me.cmdNext.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdNext.Location = New System.Drawing.Point(99, 140)
        Me.cmdNext.Name = "cmdNext"
        Me.cmdNext.Size = New System.Drawing.Size(81, 23)
        Me.cmdNext.TabIndex = 1
        Me.cmdNext.Text = "Tiếp tục"
        '
        'cmdClose
        '
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.Location = New System.Drawing.Point(195, 140)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(81, 23)
        Me.cmdClose.TabIndex = 2
        Me.cmdClose.Text = "Thoát"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(3, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(343, 61)
        Me.Panel1.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.Label3.Location = New System.Drawing.Point(51, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(208, 19)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Nhấn tiếp tục để Load cấu hình"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(275, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(48, 47)
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.Label2.Location = New System.Drawing.Point(18, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(241, 23)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Mời bạn chọn file lưu cấu hình XML"
        '
        'frm_GetXMLFile
        '
        Me.AcceptButton = Me.cmdNext
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(349, 172)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdNext)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_GetXMLFile"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Nhập cấu hình hệ thống từ File XML"
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub cmdPath_file_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPath_file.Click
        Dim sv_oDiag As New OpenFileDialog
        sv_oDiag.Title = "Chọn file XML chứa cấu hình"
        sv_oDiag.Filter = "XML files|*.xml"
        If sv_oDiag.ShowDialog = DialogResult.OK Then
            txtFilePath.Text = sv_oDiag.FileName
            If TestXMLFile(sv_oDiag.FileName) Then
                cmdNext.Enabled = True
                gv_sXMLFilePath = sv_oDiag.FileName
                ToolTip1.SetToolTip(txtFilePath, sv_oDiag.FileName)
            Else
                MessageBox.Show("Bạn chưa chọn đúng file lưu cấu hình. Mời bạn chọn lại!", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmdNext.Enabled = False
                cmdPath_file.Focus()
                gv_sXMLFilePath = ""
            End If
        End If
    End Sub
    Private Function TestXMLFile(ByVal pv_sFilePath As String) As Boolean
        Dim DS As New DataSet
        Try
            DS.ReadXml(pv_sFilePath)
            If DS.Tables.Count = 6 Then
                If DS.Tables.Contains("Sys_ToolBarButton") And DS.Tables.Contains("Sys_ROLES") And DS.Tables.Contains("Sys_Params") And DS.Tables.Contains("Sys_USERS") And DS.Tables.Contains("Sys_RFU") And DS.Tables.Contains("Sys_Functions") Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub frm_GetXMLFile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        gv_sXMLFilePath = ""
        txtFilePath.BackColor = Color.WhiteSmoke
    End Sub

    Private Sub cmdNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        Dim sv_oForm As New frm_ConfigurationOutput
        sv_oForm.mv_bOutIn = False
        sv_oForm.Text = "Nhập cấu hình từ file XML"
        sv_oForm.GroupBox2.Text = "Tùy chọn nhập dữ liệu cho User"
        sv_oForm.optAllRoles.Text = "Nhập tất cả các Role của User"
        sv_oForm.optIncludingRolesOfUser.Text = "Nhập cả Role của User"
        sv_oForm.optOnlyUser.Text = "Chỉ nhập dữ liệu người dùng"
        sv_oForm.optOnlySelectedRoles.Text = "Chỉ nhập Role được chọn"
        sv_oForm.cmdXMLOutput.Text = "Full Import"
        sv_oForm.cmdSave.Text = "Nhập theo tùy chọn"
        sv_oForm.mv_oRoleNode = gv_oMainForm.tvwAdminSystem.Nodes(0).Nodes(3).Clone
        Me.Close()
        sv_oForm.ShowDialog()
        mv_bCancel = sv_oForm.mv_bCancel
    End Sub

    Private Sub frm_GetXMLFile_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        mv_bCancel = True
        Me.Close()
    End Sub
End Class
