Public Class frm_MultiLanguage
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
    Friend WithEvents cmdPath_file As System.Windows.Forms.Button
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TreeView2 As System.Windows.Forms.TreeView
    Friend WithEvents cboFormList As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtVn As System.Windows.Forms.TextBox
    Friend WithEvents txtEn As System.Windows.Forms.TextBox
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frm_MultiLanguage))
        Me.cmdPath_file = New System.Windows.Forms.Button
        Me.txtFilePath = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TreeView2 = New System.Windows.Forms.TreeView
        Me.cboFormList = New System.Windows.Forms.ComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtVn = New System.Windows.Forms.TextBox
        Me.txtEn = New System.Windows.Forms.TextBox
        Me.cmdClose = New System.Windows.Forms.Button
        Me.cmdSave = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.SuspendLayout()
        '
        'cmdPath_file
        '
        Me.cmdPath_file.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdPath_file.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPath_file.Image = CType(resources.GetObject("cmdPath_file.Image"), System.Drawing.Image)
        Me.cmdPath_file.Location = New System.Drawing.Point(438, 9)
        Me.cmdPath_file.Name = "cmdPath_file"
        Me.cmdPath_file.Size = New System.Drawing.Size(24, 22)
        Me.cmdPath_file.TabIndex = 34
        Me.cmdPath_file.TabStop = False
        '
        'txtFilePath
        '
        Me.txtFilePath.Enabled = False
        Me.txtFilePath.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilePath.Location = New System.Drawing.Point(132, 9)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.Size = New System.Drawing.Size(303, 22)
        Me.txtFilePath.TabIndex = 33
        Me.txtFilePath.Text = ""
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(126, 18)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Đường dẫn file DLL"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(126, 18)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "Danh sách các Form"
        '
        'TreeView2
        '
        Me.TreeView2.ImageIndex = -1
        Me.TreeView2.Location = New System.Drawing.Point(3, 75)
        Me.TreeView2.Name = "TreeView2"
        Me.TreeView2.SelectedImageIndex = -1
        Me.TreeView2.Size = New System.Drawing.Size(252, 348)
        Me.TreeView2.TabIndex = 36
        '
        'cboFormList
        '
        Me.cboFormList.Location = New System.Drawing.Point(132, 36)
        Me.cboFormList.Name = "cboFormList"
        Me.cboFormList.Size = New System.Drawing.Size(330, 21)
        Me.cboFormList.TabIndex = 37
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(3, 63)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(477, 3)
        Me.GroupBox1.TabIndex = 38
        Me.GroupBox1.TabStop = False
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(261, 87)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 18)
        Me.Label3.TabIndex = 39
        Me.Label3.Text = "Tiếng Việt"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(261, 180)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 18)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "Tiếng Anh"
        '
        'txtVn
        '
        Me.txtVn.Location = New System.Drawing.Point(261, 108)
        Me.txtVn.Multiline = True
        Me.txtVn.Name = "txtVn"
        Me.txtVn.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtVn.Size = New System.Drawing.Size(219, 63)
        Me.txtVn.TabIndex = 41
        Me.txtVn.Text = ""
        '
        'txtEn
        '
        Me.txtEn.Location = New System.Drawing.Point(261, 201)
        Me.txtEn.Multiline = True
        Me.txtEn.Name = "txtEn"
        Me.txtEn.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtEn.Size = New System.Drawing.Size(219, 63)
        Me.txtEn.TabIndex = 42
        Me.txtEn.Text = ""
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdClose.Location = New System.Drawing.Point(393, 390)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(87, 25)
        Me.cmdClose.TabIndex = 44
        Me.cmdClose.Text = "Th&oát"
        '
        'cmdSave
        '
        Me.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSave.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSave.Location = New System.Drawing.Point(300, 390)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(87, 25)
        Me.cmdSave.TabIndex = 43
        Me.cmdSave.Text = "Ghi"
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(258, 375)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(225, 3)
        Me.GroupBox2.TabIndex = 45
        Me.GroupBox2.TabStop = False
        '
        'frm_MultiLanguage
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(484, 426)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.txtEn)
        Me.Controls.Add(Me.txtVn)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cboFormList)
        Me.Controls.Add(Me.TreeView2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmdPath_file)
        Me.Controls.Add(Me.txtFilePath)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_MultiLanguage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Khai báo ngôn ngữ cho các Control hiển thị trên Form"
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class
