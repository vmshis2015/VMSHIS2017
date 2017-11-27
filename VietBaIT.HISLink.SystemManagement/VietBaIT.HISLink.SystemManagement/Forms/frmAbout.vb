Public Class frmAbout
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblAbout As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmAbout))
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.lblAbout = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'LinkLabel1
        '
        Me.LinkLabel1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LinkLabel1.BackColor = System.Drawing.SystemColors.Control
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(64, Byte), CType(0, Byte), CType(0, Byte))
        Me.LinkLabel1.LinkColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(64, Byte), CType(0, Byte))
        Me.LinkLabel1.Location = New System.Drawing.Point(247, 244)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(207, 23)
        Me.LinkLabel1.TabIndex = 0
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Email: Mabujin2003@yahoo.com"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.SystemColors.Control
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(3, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(231, 339)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'lblAbout
        '
        Me.lblAbout.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblAbout.BackColor = System.Drawing.SystemColors.Control
        Me.lblAbout.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.lblAbout.ForeColor = System.Drawing.Color.FromArgb(CType(64, Byte), CType(0, Byte), CType(0, Byte))
        Me.lblAbout.Location = New System.Drawing.Point(241, 1)
        Me.lblAbout.Name = "lblAbout"
        Me.lblAbout.Size = New System.Drawing.Size(224, 27)
        Me.lblAbout.TabIndex = 2
        Me.lblAbout.Text = "Tác giả : Đào Văn Cường          "
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.Label1.Location = New System.Drawing.Point(241, 82)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(225, 240)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Phần mềm đã được bảo hộ quyền tác giả. Việc sao chép dưới bất cứ hình thức hoặc b" & _
        "ằng bất kỳ phương tiện nào cũng là bất hợp pháp và phải chịu mức truy tố cao nhấ" & _
        "t trước pháp luật. "
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.Label2.Location = New System.Drawing.Point(241, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(225, 48)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Company: EVNIT 16 Le Dai Hanh - Hai Ba Trung - Ha Noi"
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(64, Byte), CType(0, Byte), CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(247, 196)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(210, 18)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Mobile: 0904648006"
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(64, Byte), CType(0, Byte), CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(247, 217)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(210, 24)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Phone Number: (08)046442376"
        '
        'frmAbout
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(471, 321)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblAbout)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmAbout"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub

    Private Sub Label1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label1.Click
        Me.Close()
    End Sub

    Private Sub Label2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label2.Click
        Me.Close()
    End Sub

    Private Sub Label3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label3.Click
        Me.Close()
    End Sub

    Private Sub lblAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblAbout.Click
        Me.Close()
    End Sub

    Private Sub frmAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Click
        Me.Close()
    End Sub

    Private Sub frmAbout_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click
        Me.Close()
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked

    End Sub
End Class
