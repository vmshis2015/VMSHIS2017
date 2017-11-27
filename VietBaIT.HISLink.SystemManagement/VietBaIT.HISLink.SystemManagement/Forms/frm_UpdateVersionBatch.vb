Imports System.Threading
Imports System.Data.SqlClient
Imports System.IO
Public Class frm_UpdateVersionBatch
    Inherits System.Windows.Forms.Form
    Dim t1, t2, t3, t4, t5 As Thread
    Dim t As SqlTransaction
    Dim i As Integer = 1
    Dim g1, g2, g3, g4, g5 As SqlConnection
    Dim g11, g22, g33, g44, g55 As SqlConnection

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
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtFile5 As System.Windows.Forms.TextBox
    Friend WithEvents txtFile4 As System.Windows.Forms.TextBox
    Friend WithEvents txtFile3 As System.Windows.Forms.TextBox
    Friend WithEvents txtFile2 As System.Windows.Forms.TextBox
    Friend WithEvents txtFile1 As System.Windows.Forms.TextBox
    Friend WithEvents chkPatch1 As System.Windows.Forms.CheckBox
    Friend WithEvents cmdFilePath5 As System.Windows.Forms.Button
    Friend WithEvents cmdFilePath4 As System.Windows.Forms.Button
    Friend WithEvents cmdFilePath3 As System.Windows.Forms.Button
    Friend WithEvents cmdFilePath2 As System.Windows.Forms.Button
    Friend WithEvents cmdFilePath1 As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtVersion5 As System.Windows.Forms.TextBox
    Friend WithEvents txtCapacity5 As System.Windows.Forms.TextBox
    Friend WithEvents chkPatch5 As System.Windows.Forms.CheckBox
    Friend WithEvents chkCompressFile5 As System.Windows.Forms.CheckBox
    Friend WithEvents txtVersion4 As System.Windows.Forms.TextBox
    Friend WithEvents txtCapacity4 As System.Windows.Forms.TextBox
    Friend WithEvents chkPatch4 As System.Windows.Forms.CheckBox
    Friend WithEvents chkCompressFile4 As System.Windows.Forms.CheckBox
    Friend WithEvents txtVersion3 As System.Windows.Forms.TextBox
    Friend WithEvents txtCapacity3 As System.Windows.Forms.TextBox
    Friend WithEvents chkPatch3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkCompressFile3 As System.Windows.Forms.CheckBox
    Friend WithEvents txtVersion2 As System.Windows.Forms.TextBox
    Friend WithEvents txtCapacity2 As System.Windows.Forms.TextBox
    Friend WithEvents chkPatch2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkCompressFile2 As System.Windows.Forms.CheckBox
    Friend WithEvents txtVersion1 As System.Windows.Forms.TextBox
    Friend WithEvents txtCapacity1 As System.Windows.Forms.TextBox
    Friend WithEvents chkCompressFile1 As System.Windows.Forms.CheckBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox5 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox6 As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_UpdateVersionBatch))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtVersion5 = New System.Windows.Forms.TextBox
        Me.txtCapacity5 = New System.Windows.Forms.TextBox
        Me.chkPatch5 = New System.Windows.Forms.CheckBox
        Me.chkCompressFile5 = New System.Windows.Forms.CheckBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtVersion4 = New System.Windows.Forms.TextBox
        Me.txtCapacity4 = New System.Windows.Forms.TextBox
        Me.chkPatch4 = New System.Windows.Forms.CheckBox
        Me.chkCompressFile4 = New System.Windows.Forms.CheckBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtVersion3 = New System.Windows.Forms.TextBox
        Me.txtCapacity3 = New System.Windows.Forms.TextBox
        Me.chkPatch3 = New System.Windows.Forms.CheckBox
        Me.chkCompressFile3 = New System.Windows.Forms.CheckBox
        Me.txtVersion2 = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtCapacity2 = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.chkPatch2 = New System.Windows.Forms.CheckBox
        Me.chkCompressFile2 = New System.Windows.Forms.CheckBox
        Me.txtVersion1 = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtCapacity1 = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.chkPatch1 = New System.Windows.Forms.CheckBox
        Me.chkCompressFile1 = New System.Windows.Forms.CheckBox
        Me.cmdFilePath5 = New System.Windows.Forms.Button
        Me.txtFile5 = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmdFilePath4 = New System.Windows.Forms.Button
        Me.txtFile4 = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmdFilePath3 = New System.Windows.Forms.Button
        Me.txtFile3 = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmdFilePath2 = New System.Windows.Forms.Button
        Me.txtFile2 = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmdFilePath1 = New System.Windows.Forms.Button
        Me.txtFile1 = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmdClose = New System.Windows.Forms.Button
        Me.cmdSave = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.PictureBox4 = New System.Windows.Forms.PictureBox
        Me.PictureBox5 = New System.Windows.Forms.PictureBox
        Me.PictureBox6 = New System.Windows.Forms.PictureBox
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.txtVersion5)
        Me.GroupBox1.Controls.Add(Me.txtCapacity5)
        Me.GroupBox1.Controls.Add(Me.chkPatch5)
        Me.GroupBox1.Controls.Add(Me.chkCompressFile5)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txtVersion4)
        Me.GroupBox1.Controls.Add(Me.txtCapacity4)
        Me.GroupBox1.Controls.Add(Me.chkPatch4)
        Me.GroupBox1.Controls.Add(Me.chkCompressFile4)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtVersion3)
        Me.GroupBox1.Controls.Add(Me.txtCapacity3)
        Me.GroupBox1.Controls.Add(Me.chkPatch3)
        Me.GroupBox1.Controls.Add(Me.chkCompressFile3)
        Me.GroupBox1.Controls.Add(Me.txtVersion2)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtCapacity2)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.chkPatch2)
        Me.GroupBox1.Controls.Add(Me.chkCompressFile2)
        Me.GroupBox1.Controls.Add(Me.txtVersion1)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtCapacity1)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.chkPatch1)
        Me.GroupBox1.Controls.Add(Me.chkCompressFile1)
        Me.GroupBox1.Controls.Add(Me.cmdFilePath5)
        Me.GroupBox1.Controls.Add(Me.txtFile5)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cmdFilePath4)
        Me.GroupBox1.Controls.Add(Me.txtFile4)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmdFilePath3)
        Me.GroupBox1.Controls.Add(Me.txtFile3)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cmdFilePath2)
        Me.GroupBox1.Controls.Add(Me.txtFile2)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmdFilePath1)
        Me.GroupBox1.Controls.Add(Me.txtFile1)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(174, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(456, 390)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Danh sách các File được cập nhật"
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(27, 363)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(93, 16)
        Me.Label14.TabIndex = 76
        Me.Label14.Text = "Số phiên bản"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(27, 339)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(93, 16)
        Me.Label15.TabIndex = 75
        Me.Label15.Text = "Dung lượng"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtVersion5
        '
        Me.txtVersion5.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtVersion5.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtVersion5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVersion5.Location = New System.Drawing.Point(126, 360)
        Me.txtVersion5.MaxLength = 100
        Me.txtVersion5.Name = "txtVersion5"
        Me.txtVersion5.ReadOnly = True
        Me.txtVersion5.Size = New System.Drawing.Size(174, 22)
        Me.txtVersion5.TabIndex = 74
        '
        'txtCapacity5
        '
        Me.txtCapacity5.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCapacity5.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCapacity5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCapacity5.Location = New System.Drawing.Point(126, 336)
        Me.txtCapacity5.MaxLength = 100
        Me.txtCapacity5.Name = "txtCapacity5"
        Me.txtCapacity5.ReadOnly = True
        Me.txtCapacity5.Size = New System.Drawing.Size(174, 22)
        Me.txtCapacity5.TabIndex = 73
        '
        'chkPatch5
        '
        Me.chkPatch5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPatch5.Location = New System.Drawing.Point(306, 339)
        Me.chkPatch5.Name = "chkPatch5"
        Me.chkPatch5.Size = New System.Drawing.Size(74, 19)
        Me.chkPatch5.TabIndex = 72
        Me.chkPatch5.Text = "Patch?"
        '
        'chkCompressFile5
        '
        Me.chkCompressFile5.Checked = True
        Me.chkCompressFile5.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCompressFile5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCompressFile5.Location = New System.Drawing.Point(306, 360)
        Me.chkCompressFile5.Name = "chkCompressFile5"
        Me.chkCompressFile5.Size = New System.Drawing.Size(75, 19)
        Me.chkCompressFile5.TabIndex = 71
        Me.chkCompressFile5.Text = "Nén file trước khi cập nhật"
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(27, 291)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(93, 16)
        Me.Label12.TabIndex = 70
        Me.Label12.Text = "Số phiên bản"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(27, 267)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(93, 16)
        Me.Label13.TabIndex = 69
        Me.Label13.Text = "Dung lượng"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtVersion4
        '
        Me.txtVersion4.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtVersion4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtVersion4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVersion4.Location = New System.Drawing.Point(126, 288)
        Me.txtVersion4.MaxLength = 100
        Me.txtVersion4.Name = "txtVersion4"
        Me.txtVersion4.ReadOnly = True
        Me.txtVersion4.Size = New System.Drawing.Size(174, 22)
        Me.txtVersion4.TabIndex = 68
        '
        'txtCapacity4
        '
        Me.txtCapacity4.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCapacity4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCapacity4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCapacity4.Location = New System.Drawing.Point(126, 264)
        Me.txtCapacity4.MaxLength = 100
        Me.txtCapacity4.Name = "txtCapacity4"
        Me.txtCapacity4.ReadOnly = True
        Me.txtCapacity4.Size = New System.Drawing.Size(174, 22)
        Me.txtCapacity4.TabIndex = 67
        '
        'chkPatch4
        '
        Me.chkPatch4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPatch4.Location = New System.Drawing.Point(306, 267)
        Me.chkPatch4.Name = "chkPatch4"
        Me.chkPatch4.Size = New System.Drawing.Size(74, 19)
        Me.chkPatch4.TabIndex = 66
        Me.chkPatch4.Text = "Patch?"
        '
        'chkCompressFile4
        '
        Me.chkCompressFile4.Checked = True
        Me.chkCompressFile4.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCompressFile4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCompressFile4.Location = New System.Drawing.Point(306, 288)
        Me.chkCompressFile4.Name = "chkCompressFile4"
        Me.chkCompressFile4.Size = New System.Drawing.Size(75, 19)
        Me.chkCompressFile4.TabIndex = 65
        Me.chkCompressFile4.Text = "Nén file trước khi cập nhật"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(27, 219)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(93, 16)
        Me.Label10.TabIndex = 64
        Me.Label10.Text = "Số phiên bản"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(27, 195)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(93, 16)
        Me.Label11.TabIndex = 63
        Me.Label11.Text = "Dung lượng"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtVersion3
        '
        Me.txtVersion3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtVersion3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtVersion3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVersion3.Location = New System.Drawing.Point(126, 216)
        Me.txtVersion3.MaxLength = 100
        Me.txtVersion3.Name = "txtVersion3"
        Me.txtVersion3.ReadOnly = True
        Me.txtVersion3.Size = New System.Drawing.Size(174, 22)
        Me.txtVersion3.TabIndex = 62
        '
        'txtCapacity3
        '
        Me.txtCapacity3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCapacity3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCapacity3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCapacity3.Location = New System.Drawing.Point(126, 192)
        Me.txtCapacity3.MaxLength = 100
        Me.txtCapacity3.Name = "txtCapacity3"
        Me.txtCapacity3.ReadOnly = True
        Me.txtCapacity3.Size = New System.Drawing.Size(174, 22)
        Me.txtCapacity3.TabIndex = 61
        '
        'chkPatch3
        '
        Me.chkPatch3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPatch3.Location = New System.Drawing.Point(306, 195)
        Me.chkPatch3.Name = "chkPatch3"
        Me.chkPatch3.Size = New System.Drawing.Size(74, 19)
        Me.chkPatch3.TabIndex = 60
        Me.chkPatch3.Text = "Patch?"
        '
        'chkCompressFile3
        '
        Me.chkCompressFile3.Checked = True
        Me.chkCompressFile3.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCompressFile3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCompressFile3.Location = New System.Drawing.Point(306, 216)
        Me.chkCompressFile3.Name = "chkCompressFile3"
        Me.chkCompressFile3.Size = New System.Drawing.Size(75, 19)
        Me.chkCompressFile3.TabIndex = 59
        Me.chkCompressFile3.Text = "Nén file trước khi cập nhật"
        '
        'txtVersion2
        '
        Me.txtVersion2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtVersion2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtVersion2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVersion2.Location = New System.Drawing.Point(126, 144)
        Me.txtVersion2.MaxLength = 100
        Me.txtVersion2.Name = "txtVersion2"
        Me.txtVersion2.ReadOnly = True
        Me.txtVersion2.Size = New System.Drawing.Size(174, 22)
        Me.txtVersion2.TabIndex = 57
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(24, 147)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(93, 16)
        Me.Label8.TabIndex = 58
        Me.Label8.Text = "Số phiên bản"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtCapacity2
        '
        Me.txtCapacity2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCapacity2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCapacity2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCapacity2.Location = New System.Drawing.Point(126, 120)
        Me.txtCapacity2.MaxLength = 100
        Me.txtCapacity2.Name = "txtCapacity2"
        Me.txtCapacity2.ReadOnly = True
        Me.txtCapacity2.Size = New System.Drawing.Size(174, 22)
        Me.txtCapacity2.TabIndex = 55
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(24, 123)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(93, 16)
        Me.Label9.TabIndex = 56
        Me.Label9.Text = "Dung lượng"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkPatch2
        '
        Me.chkPatch2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPatch2.Location = New System.Drawing.Point(306, 123)
        Me.chkPatch2.Name = "chkPatch2"
        Me.chkPatch2.Size = New System.Drawing.Size(74, 19)
        Me.chkPatch2.TabIndex = 54
        Me.chkPatch2.Text = "Patch?"
        '
        'chkCompressFile2
        '
        Me.chkCompressFile2.Checked = True
        Me.chkCompressFile2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCompressFile2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCompressFile2.Location = New System.Drawing.Point(306, 144)
        Me.chkCompressFile2.Name = "chkCompressFile2"
        Me.chkCompressFile2.Size = New System.Drawing.Size(75, 19)
        Me.chkCompressFile2.TabIndex = 53
        Me.chkCompressFile2.Text = "Nén file trước khi cập nhật"
        '
        'txtVersion1
        '
        Me.txtVersion1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtVersion1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtVersion1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVersion1.Location = New System.Drawing.Point(126, 72)
        Me.txtVersion1.MaxLength = 100
        Me.txtVersion1.Name = "txtVersion1"
        Me.txtVersion1.ReadOnly = True
        Me.txtVersion1.Size = New System.Drawing.Size(174, 22)
        Me.txtVersion1.TabIndex = 51
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(24, 75)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(93, 16)
        Me.Label7.TabIndex = 52
        Me.Label7.Text = "Số phiên bản"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtCapacity1
        '
        Me.txtCapacity1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCapacity1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCapacity1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCapacity1.Location = New System.Drawing.Point(126, 48)
        Me.txtCapacity1.MaxLength = 100
        Me.txtCapacity1.Name = "txtCapacity1"
        Me.txtCapacity1.ReadOnly = True
        Me.txtCapacity1.Size = New System.Drawing.Size(174, 22)
        Me.txtCapacity1.TabIndex = 49
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(24, 51)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(93, 16)
        Me.Label6.TabIndex = 50
        Me.Label6.Text = "Dung lượng"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkPatch1
        '
        Me.chkPatch1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPatch1.Location = New System.Drawing.Point(306, 51)
        Me.chkPatch1.Name = "chkPatch1"
        Me.chkPatch1.Size = New System.Drawing.Size(74, 19)
        Me.chkPatch1.TabIndex = 44
        Me.chkPatch1.Text = "Patch?"
        '
        'chkCompressFile1
        '
        Me.chkCompressFile1.Checked = True
        Me.chkCompressFile1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCompressFile1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCompressFile1.Location = New System.Drawing.Point(306, 72)
        Me.chkCompressFile1.Name = "chkCompressFile1"
        Me.chkCompressFile1.Size = New System.Drawing.Size(75, 19)
        Me.chkCompressFile1.TabIndex = 43
        Me.chkCompressFile1.Text = "Nén file trước khi cập nhật"
        '
        'cmdFilePath5
        '
        Me.cmdFilePath5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdFilePath5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFilePath5.Image = CType(resources.GetObject("cmdFilePath5.Image"), System.Drawing.Image)
        Me.cmdFilePath5.Location = New System.Drawing.Point(420, 312)
        Me.cmdFilePath5.Name = "cmdFilePath5"
        Me.cmdFilePath5.Size = New System.Drawing.Size(24, 21)
        Me.cmdFilePath5.TabIndex = 35
        Me.cmdFilePath5.TabStop = False
        '
        'txtFile5
        '
        Me.txtFile5.BackColor = System.Drawing.Color.White
        Me.txtFile5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFile5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtFile5.Location = New System.Drawing.Point(126, 312)
        Me.txtFile5.MaxLength = 150
        Me.txtFile5.Name = "txtFile5"
        Me.txtFile5.ReadOnly = True
        Me.txtFile5.Size = New System.Drawing.Size(288, 21)
        Me.txtFile5.TabIndex = 34
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 318)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(114, 16)
        Me.Label5.TabIndex = 36
        Me.Label5.Text = "Đường dẫn file 5"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdFilePath4
        '
        Me.cmdFilePath4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdFilePath4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFilePath4.Image = CType(resources.GetObject("cmdFilePath4.Image"), System.Drawing.Image)
        Me.cmdFilePath4.Location = New System.Drawing.Point(420, 240)
        Me.cmdFilePath4.Name = "cmdFilePath4"
        Me.cmdFilePath4.Size = New System.Drawing.Size(24, 21)
        Me.cmdFilePath4.TabIndex = 32
        Me.cmdFilePath4.TabStop = False
        '
        'txtFile4
        '
        Me.txtFile4.BackColor = System.Drawing.Color.White
        Me.txtFile4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFile4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtFile4.Location = New System.Drawing.Point(126, 240)
        Me.txtFile4.MaxLength = 150
        Me.txtFile4.Name = "txtFile4"
        Me.txtFile4.ReadOnly = True
        Me.txtFile4.Size = New System.Drawing.Size(288, 21)
        Me.txtFile4.TabIndex = 31
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 246)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(114, 16)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "Đường dẫn file 4"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdFilePath3
        '
        Me.cmdFilePath3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdFilePath3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFilePath3.Image = CType(resources.GetObject("cmdFilePath3.Image"), System.Drawing.Image)
        Me.cmdFilePath3.Location = New System.Drawing.Point(420, 168)
        Me.cmdFilePath3.Name = "cmdFilePath3"
        Me.cmdFilePath3.Size = New System.Drawing.Size(24, 21)
        Me.cmdFilePath3.TabIndex = 29
        Me.cmdFilePath3.TabStop = False
        '
        'txtFile3
        '
        Me.txtFile3.BackColor = System.Drawing.Color.White
        Me.txtFile3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFile3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtFile3.Location = New System.Drawing.Point(126, 168)
        Me.txtFile3.MaxLength = 150
        Me.txtFile3.Name = "txtFile3"
        Me.txtFile3.ReadOnly = True
        Me.txtFile3.Size = New System.Drawing.Size(288, 21)
        Me.txtFile3.TabIndex = 28
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 171)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(114, 16)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "Đường dẫn file 3"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdFilePath2
        '
        Me.cmdFilePath2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdFilePath2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFilePath2.Image = CType(resources.GetObject("cmdFilePath2.Image"), System.Drawing.Image)
        Me.cmdFilePath2.Location = New System.Drawing.Point(420, 96)
        Me.cmdFilePath2.Name = "cmdFilePath2"
        Me.cmdFilePath2.Size = New System.Drawing.Size(24, 21)
        Me.cmdFilePath2.TabIndex = 26
        Me.cmdFilePath2.TabStop = False
        '
        'txtFile2
        '
        Me.txtFile2.BackColor = System.Drawing.Color.White
        Me.txtFile2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFile2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtFile2.Location = New System.Drawing.Point(126, 96)
        Me.txtFile2.MaxLength = 150
        Me.txtFile2.Name = "txtFile2"
        Me.txtFile2.ReadOnly = True
        Me.txtFile2.Size = New System.Drawing.Size(288, 21)
        Me.txtFile2.TabIndex = 25
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 99)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 16)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Đường dẫn file 2"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdFilePath1
        '
        Me.cmdFilePath1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdFilePath1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFilePath1.Image = CType(resources.GetObject("cmdFilePath1.Image"), System.Drawing.Image)
        Me.cmdFilePath1.Location = New System.Drawing.Point(420, 24)
        Me.cmdFilePath1.Name = "cmdFilePath1"
        Me.cmdFilePath1.Size = New System.Drawing.Size(24, 21)
        Me.cmdFilePath1.TabIndex = 1
        Me.cmdFilePath1.TabStop = False
        '
        'txtFile1
        '
        Me.txtFile1.BackColor = System.Drawing.Color.White
        Me.txtFile1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFile1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtFile1.Location = New System.Drawing.Point(126, 24)
        Me.txtFile1.MaxLength = 150
        Me.txtFile1.Name = "txtFile1"
        Me.txtFile1.ReadOnly = True
        Me.txtFile1.Size = New System.Drawing.Size(288, 21)
        Me.txtFile1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(5, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(115, 16)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Đường dẫn file 1"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdClose
        '
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdClose.Location = New System.Drawing.Point(533, 402)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(88, 29)
        Me.cmdClose.TabIndex = 20
        Me.cmdClose.Text = "Th&oát"
        '
        'cmdSave
        '
        Me.cmdSave.Enabled = False
        Me.cmdSave.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSave.Location = New System.Drawing.Point(425, 402)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(91, 29)
        Me.cmdSave.TabIndex = 19
        Me.cmdSave.Text = "&Ghi"
        '
        'PictureBox1
        '
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(3, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(165, 178)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 22
        Me.PictureBox1.TabStop = False
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.MintCream
        Me.Label16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label16.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(3, 198)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(171, 195)
        Me.Label16.TabIndex = 51
        Me.Label16.Text = "Cập nhật phiên bản theo lô cho phép cập nhật đồng thời được nhiều dạng File khác " & _
            "nhau lên máy chủ Server . Do chạy theo cơ chế Thread nên các File bạn chọn không" & _
            " được giống nhau(không kể phần mở rộng)"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(315, 363)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(15, 15)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 52
        Me.PictureBox2.TabStop = False
        Me.PictureBox2.Visible = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(333, 363)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(15, 15)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 53
        Me.PictureBox3.TabStop = False
        Me.PictureBox3.Visible = False
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = CType(resources.GetObject("PictureBox4.Image"), System.Drawing.Image)
        Me.PictureBox4.Location = New System.Drawing.Point(351, 363)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(15, 15)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox4.TabIndex = 54
        Me.PictureBox4.TabStop = False
        Me.PictureBox4.Visible = False
        '
        'PictureBox5
        '
        Me.PictureBox5.Image = CType(resources.GetObject("PictureBox5.Image"), System.Drawing.Image)
        Me.PictureBox5.Location = New System.Drawing.Point(369, 363)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(15, 15)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox5.TabIndex = 55
        Me.PictureBox5.TabStop = False
        Me.PictureBox5.Visible = False
        '
        'PictureBox6
        '
        Me.PictureBox6.Image = CType(resources.GetObject("PictureBox6.Image"), System.Drawing.Image)
        Me.PictureBox6.Location = New System.Drawing.Point(387, 363)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(15, 15)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox6.TabIndex = 56
        Me.PictureBox6.TabStop = False
        Me.PictureBox6.Visible = False
        '
        'frm_UpdateVersionBatch
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(636, 443)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.PictureBox6)
        Me.Controls.Add(Me.PictureBox5)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.PictureBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_UpdateVersionBatch"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cập nhật phiên bản theo lô"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frm_UpdateVersionBatch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtFile1.BackColor = Color.WhiteSmoke
        txtFile2.BackColor = Color.WhiteSmoke
        txtFile3.BackColor = Color.WhiteSmoke
        txtFile4.BackColor = Color.WhiteSmoke
        txtFile5.BackColor = Color.WhiteSmoke
        PictureBox2.Location = PictureBox1.Location
        PictureBox2.Size = PictureBox1.Size
        PictureBox3.Location = PictureBox1.Location
        PictureBox3.Size = PictureBox1.Size
        PictureBox4.Location = PictureBox1.Location
        PictureBox4.Size = PictureBox1.Size
        PictureBox5.Location = PictureBox1.Location
        PictureBox5.Size = PictureBox1.Size
        PictureBox6.Location = PictureBox1.Location
        PictureBox6.Size = PictureBox1.Size
        PictureBox1.BringToFront()
    End Sub

    Private Sub cmdFilePath1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFilePath1.Click, cmdFilePath2.Click, cmdFilePath3.Click, cmdFilePath4.Click, cmdFilePath5.Click
        Select Case sender.name
            Case cmdFilePath1.Name
                OpenFileVersion(txtFile1, txtCapacity1, txtVersion1)
            Case cmdFilePath2.Name
                OpenFileVersion(txtFile2, txtCapacity2, txtVersion2)
            Case cmdFilePath3.Name
                OpenFileVersion(txtFile3, txtCapacity3, txtVersion3)
            Case cmdFilePath4.Name
                OpenFileVersion(txtFile4, txtCapacity4, txtVersion4)
            Case cmdFilePath5.Name
                OpenFileVersion(txtFile5, txtCapacity5, txtVersion5)
        End Select

    End Sub
    Public Function bIsLastestVersion1(ByVal pv_sFileName As String, ByVal pv_sVersion As String) As Boolean
        Dim sv_ds As New DataSet
        Dim sv_DA As SqlDataAdapter
        Dim sv_sSql As String
        sv_sSql = "SELECT * from Sys_VERSION WHERE  sFileName=N'" & pv_sFileName & "' AND sVersion=N'" & pv_sVersion & "'"
        If g11 Is Nothing Then
            g11 = New SqlConnection(mv_sConnString)
            g11.Open()
        ElseIf g11.State = ConnectionState.Closed Then
            g11.Open()
        End If
        Try
            sv_DA = New SqlDataAdapter(sv_sSql, g11)
            sv_DA.Fill(sv_ds, "Sys_VERSION")
            If sv_ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function bIsLastestVersion2(ByVal pv_sFileName As String, ByVal pv_sVersion As String) As Boolean
        Dim sv_ds As New DataSet
        Dim sv_DA As SqlDataAdapter
        Dim sv_sSql As String
        sv_sSql = "SELECT * from Sys_VERSION WHERE  sFileName=N'" & pv_sFileName & "' AND sVersion=N'" & pv_sVersion & "'"
        If g22 Is Nothing Then
            g22 = New SqlConnection(mv_sConnString)
            g22.Open()
        ElseIf g22.State = ConnectionState.Closed Then
            g22.Open()
        End If
        Try
            sv_DA = New SqlDataAdapter(sv_sSql, g22)
            sv_DA.Fill(sv_ds, "Sys_VERSION")
            If sv_ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function bIsLastestVersion3(ByVal pv_sFileName As String, ByVal pv_sVersion As String) As Boolean
        Dim sv_ds As New DataSet
        Dim sv_DA As SqlDataAdapter
        Dim sv_sSql As String
        sv_sSql = "SELECT * from Sys_VERSION WHERE  sFileName=N'" & pv_sFileName & "' AND sVersion=N'" & pv_sVersion & "'"
        sv_sSql = "SELECT * from Sys_VERSION WHERE  sFileName=N'" & pv_sFileName & "' AND sVersion=N'" & pv_sVersion & "'"
        If g33 Is Nothing Then
            g33 = New SqlConnection(mv_sConnString)
            g33.Open()
        ElseIf g33.State = ConnectionState.Closed Then
            g33.Open()
        End If
        Try
            sv_DA = New SqlDataAdapter(sv_sSql, g33)
            sv_DA.Fill(sv_ds, "Sys_VERSION")
            If sv_ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function bIsLastestVersion4(ByVal pv_sFileName As String, ByVal pv_sVersion As String) As Boolean
        Dim sv_ds As New DataSet
        Dim sv_DA As SqlDataAdapter
        Dim sv_sSql As String
        sv_sSql = "SELECT * from Sys_VERSION WHERE  sFileName=N'" & pv_sFileName & "' AND sVersion=N'" & pv_sVersion & "'"
        If g44 Is Nothing Then
            g44 = New SqlConnection(mv_sConnString)
            g44.Open()
        ElseIf g44.State = ConnectionState.Closed Then
            g44.Open()
        End If
        Try
            sv_DA = New SqlDataAdapter(sv_sSql, g44)
            sv_DA.Fill(sv_ds, "Sys_VERSION")
            If sv_ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function bIsLastestVersion5(ByVal pv_sFileName As String, ByVal pv_sVersion As String) As Boolean
        Dim sv_ds As New DataSet
        Dim sv_DA As SqlDataAdapter
        Dim sv_sSql As String
        sv_sSql = "SELECT * from Sys_VERSION WHERE  sFileName=N'" & pv_sFileName & "' AND sVersion=N'" & pv_sVersion & "'"
        If g55 Is Nothing Then
            g55 = New SqlConnection(mv_sConnString)
            g55.Open()
        ElseIf g55.State = ConnectionState.Closed Then
            g55.Open()
        End If
        Try
            sv_DA = New SqlDataAdapter(sv_sSql, g55)
            sv_DA.Fill(sv_ds, "Sys_VERSION")
            If sv_ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub OpenFileVersion(ByVal pv_objTxtFilePath As TextBox, ByVal pv_objTxtCapacity As TextBox, ByVal pv_objTxtVersion As TextBox)
        Dim sv_oDiag As New OpenFileDialog
        sv_oDiag.Title = "Chọn file XML chứa cấu hình"
        sv_oDiag.Filter = "All files|*.*|DLL file|*.dll"
        If sv_oDiag.ShowDialog = DialogResult.OK Then
            pv_objTxtFilePath.Text = sv_oDiag.FileName
            ToolTip1.SetToolTip(pv_objTxtFilePath, sv_oDiag.FileName)
            pv_objTxtFilePath.Tag = sv_oDiag.FileName
            Dim _FileVersionInfo As FileVersionInfo = FileVersionInfo.GetVersionInfo(pv_objTxtFilePath.Text)
            pv_objTxtVersion.Text = _FileVersionInfo.ProductVersion
            Dim _FileProperty As System.IO.FileInfo
            _FileProperty = New System.IO.FileInfo(pv_objTxtFilePath.Text)
            pv_objTxtCapacity.Text = _FileProperty.Length.ToString & " Bytes"
            cmdSave.Enabled = True
        End If
    End Sub
    Private Sub CheckRarExists()
        Try
            'Kiểm tra sự tồn tại của ứng dụng Winrar. Nếu ko có thì Copy về thư mục cài ứng dụng để chạy
            If Not File.Exists(Application.StartupPath & "\WINRAR\WINRAR.EXE") Then
                Dim sv_oDlg As New OpenFileDialog
                sv_oDlg.Title = "Chọn đến thư mục chứa ứng dụng Winrar"
                sv_oDlg.Filter = "Winrar|Winrar.exe"
                If sv_oDlg.ShowDialog = DialogResult.OK Then
                    If Not Directory.Exists(Application.StartupPath & "\WINRAR") Then
                        Directory.CreateDirectory(Application.StartupPath & "\WINRAR")
                    End If
                    File.Copy(sv_oDlg.FileName, Application.StartupPath & "\WINRAR\WINRAR.EXE", True)
                    MessageBox.Show("Hãy nhấn lại nút Ghi để thực hiện cập nhật phiên bản", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub UpdateFile1()
        Dim sFileName1 As String
        Try
            If txtFile1.Text.Trim = String.Empty Or InStr(txtFile1.Text.Trim, "\") = 0 Or InStr(txtFile1.Text.Trim, ".") = 0 Then Return
            sFileName1 = txtFile1.Text.Substring(txtFile1.Text.LastIndexOf("\") + 1)
            If bIsLastestVersion1(sFileName1, txtVersion1.Text.Trim) Then
                txtFile1.Text = "Đã tồn tại phiên bản này trên Server. Đề nghị kiểm tra lại"
                Return
            End If
            CompressFile(t1, txtFile1, chkCompressFile1, txtCapacity1, txtVersion1, chkPatch1)
            UpdateVersion1(txtFile1, chkCompressFile1, txtCapacity1, txtVersion1, chkPatch1)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub UpdateFile2()
        Dim sFileName2 As String
        Try
            If txtFile2.Text.Trim = String.Empty Or InStr(txtFile2.Text.Trim, "\") = 0 Or InStr(txtFile2.Text.Trim, ".") = 0 Then Return
            sFileName2 = txtFile2.Text.Substring(txtFile2.Text.LastIndexOf("\") + 1)
            If bIsLastestVersion2(sFileName2, txtVersion2.Text.Trim) Then
                txtFile2.Text = "Đã tồn tại phiên bản này trên Server. Đề nghị kiểm tra lại"
                Return
            End If
            CompressFile(t2, txtFile2, chkCompressFile2, txtCapacity2, txtVersion2, chkPatch2)
            UpdateVersion2(txtFile2, chkCompressFile2, txtCapacity2, txtVersion2, chkPatch2)
        Catch ex As Exception

        End Try

    End Sub
    Private Sub UpdateFile3()
        Dim sFileName3 As String
        Try
            If txtFile3.Text.Trim = String.Empty Or InStr(txtFile3.Text.Trim, "\") = 0 Or InStr(txtFile3.Text.Trim, ".") = 0 Then Return
            sFileName3 = txtFile3.Text.Substring(txtFile3.Text.LastIndexOf("\") + 1)
            If bIsLastestVersion3(sFileName3, txtVersion3.Text.Trim) Then
                txtFile3.Text = "Đã tồn tại phiên bản này trên Server. Đề nghị kiểm tra lại"
                Return
            End If
            CompressFile(t3, txtFile3, chkCompressFile3, txtCapacity3, txtVersion3, chkPatch3)
            UpdateVersion3(txtFile3, chkCompressFile3, txtCapacity3, txtVersion3, chkPatch3)
        Catch ex As Exception

        End Try

    End Sub
    Private Sub UpdateFile4()
        Try
            Dim sFileName4 As String
            If txtFile4.Text.Trim = String.Empty Or InStr(txtFile4.Text.Trim, "\") = 0 Or InStr(txtFile4.Text.Trim, ".") = 0 Then Return
            sFileName4 = txtFile4.Text.Substring(txtFile4.Text.LastIndexOf("\") + 1)
            If bIsLastestVersion4(sFileName4, txtVersion4.Text.Trim) Then
                txtFile4.Text = "Đã tồn tại phiên bản này trên Server. Đề nghị kiểm tra lại"
                Return
            End If
            CompressFile(t4, txtFile4, chkCompressFile4, txtCapacity4, txtVersion4, chkPatch4)
            UpdateVersion4(txtFile4, chkCompressFile4, txtCapacity4, txtVersion4, chkPatch4)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub UpdateFile5()
        Dim sFileName5 As String
        Try
            If txtFile5.Text.Trim = String.Empty Or InStr(txtFile5.Text.Trim, "\") = 0 Or InStr(txtFile5.Text.Trim, ".") = 0 Then Return
            sFileName5 = txtFile5.Text.Substring(txtFile5.Text.LastIndexOf("\") + 1)
            If bIsLastestVersion5(sFileName5, txtVersion5.Text.Trim) Then
                txtFile5.Text = "Đã tồn tại phiên bản này trên Server. Đề nghị kiểm tra lại"
                Return
            End If
            CompressFile(t5, txtFile5, chkCompressFile5, txtCapacity5, txtVersion5, chkPatch5)
            UpdateVersion5(txtFile5, chkCompressFile5, txtCapacity5, txtVersion5, chkPatch5)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub CompressFile(ByVal t As Thread, ByVal pv_TxtFile As TextBox, ByVal pv_ChkCompress As CheckBox, ByVal pv_txtCapacity As TextBox, ByVal pv_txtVer As TextBox, ByVal pv_chkPatch As CheckBox)
        Try
            If Not bCheckData(pv_TxtFile) Then
                Try
                    If t.ThreadState = ThreadState.Running Then
                        t.Abort()
                    End If
                Catch ex As Exception

                End Try

                Return
            End If
            If Not File.Exists(pv_TxtFile.Text) Then Return
            If pv_ChkCompress.Checked Then
                'Thực hiện nén File được chọn
                If CompressFile(pv_TxtFile, pv_TxtFile.Text) Then
                    pv_TxtFile.Text = "Đang thực hiện lưu File vào CSDL..."
                    pv_TxtFile.Refresh()
                    Me.Refresh()
                End If
            End If
        Catch ex As Exception
        End Try

    End Sub
    Private Sub UpdateVersion1(ByVal pv_TxtFile As TextBox, ByVal pv_ChkCompress As CheckBox, ByVal pv_txtCapacity As TextBox, ByVal pv_txtVer As TextBox, ByVal pv_chkPatch As CheckBox)
        Dim arrFilename() As String = Split(pv_TxtFile.Tag, "\")
        Dim sRarFile As String
        If pv_TxtFile.Text.Trim = String.Empty Or pv_TxtFile.Tag = String.Empty Then Return
        sRarFile = pv_TxtFile.Tag.Replace(pv_TxtFile.Tag.Substring(pv_TxtFile.Tag.LastIndexOf(".") + 1), "RAR")
        arrFilename.Reverse(arrFilename)
        Dim fs As FileStream
        Dim intRar As Integer
        Dim rd As BinaryReader
        Dim arrData() As Byte
        Try
            If File.Exists(sRarFile) Then
                fs = New FileStream(sRarFile, FileMode.Open)
                intRar = 1
            Else
                fs = New FileStream(pv_TxtFile.Tag, FileMode.Open)
                intRar = 0
            End If

            rd = New BinaryReader(fs)
            arrData = rd.ReadBytes(CInt(fs.Length))
            fs.Flush()
            fs.Close()
        Catch ex As Exception
            pv_TxtFile.Text = ex.Message
            Return
        End Try
        If SaveFile1(pv_TxtFile, arrFilename(0), arrFilename(0).Replace(arrFilename(0).Substring(arrFilename(0).LastIndexOf(".") + 1), "rar"), arrData, pv_txtVer.Text.Trim, intRar, IIf(pv_chkPatch.Checked, 1, 0), CDec(pv_txtCapacity.Text.Trim.ToUpper.Replace("BYTES", "")), "") Then
            pv_TxtFile.Text = "Đã cập nhật lên Server"
            pv_TxtFile.Refresh()
            If File.Exists(sRarFile) Then
                File.Delete(sRarFile)
            End If
        End If
    End Sub
    Private Sub UpdateVersion2(ByVal pv_TxtFile As TextBox, ByVal pv_ChkCompress As CheckBox, ByVal pv_txtCapacity As TextBox, ByVal pv_txtVer As TextBox, ByVal pv_chkPatch As CheckBox)
        Dim arrFilename() As String = Split(pv_TxtFile.Tag, "\")
        Dim sRarFile As String
        If pv_TxtFile.Text.Trim = String.Empty Or pv_TxtFile.Tag = String.Empty Then Return
        sRarFile = pv_TxtFile.Tag.Replace(pv_TxtFile.Tag.Substring(pv_TxtFile.Tag.LastIndexOf(".") + 1), "RAR")
        arrFilename.Reverse(arrFilename)
        Dim fs As FileStream
        Dim intRar As Integer
        Dim rd As BinaryReader
        Dim arrData() As Byte
        Try
            If File.Exists(sRarFile) Then
                fs = New FileStream(sRarFile, FileMode.Open)
                intRar = 1
            Else
                fs = New FileStream(pv_TxtFile.Tag, FileMode.Open)
                intRar = 0
            End If

            rd = New BinaryReader(fs)
            arrData = rd.ReadBytes(CInt(fs.Length))
            fs.Flush()
            fs.Close()
        Catch ex As Exception
            pv_TxtFile.Text = ex.Message
            Return
        End Try
        If SaveFile2(pv_TxtFile, arrFilename(0), arrFilename(0).Replace(arrFilename(0).Substring(arrFilename(0).LastIndexOf(".") + 1), "rar"), arrData, pv_txtVer.Text.Trim, intRar, IIf(pv_chkPatch.Checked, 1, 0), CDec(pv_txtCapacity.Text.Trim.ToUpper.Replace("BYTES", "")), "") Then
            pv_TxtFile.Text = "Đã cập nhật lên Server"
            pv_TxtFile.Refresh()
            If File.Exists(sRarFile) Then
                File.Delete(sRarFile)
            End If
        End If
    End Sub
    Private Sub UpdateVersion3(ByVal pv_TxtFile As TextBox, ByVal pv_ChkCompress As CheckBox, ByVal pv_txtCapacity As TextBox, ByVal pv_txtVer As TextBox, ByVal pv_chkPatch As CheckBox)
        Dim arrFilename() As String = Split(pv_TxtFile.Tag, "\")
        Dim sRarFile As String
        If pv_TxtFile.Text.Trim = String.Empty Or pv_TxtFile.Tag = String.Empty Then Return
        sRarFile = pv_TxtFile.Tag.Replace(pv_TxtFile.Tag.Substring(pv_TxtFile.Tag.LastIndexOf(".") + 1), "RAR")
        arrFilename.Reverse(arrFilename)
        Dim fs As FileStream
        Dim intRar As Integer
        Dim rd As BinaryReader
        Dim arrData() As Byte
        Try
            If File.Exists(sRarFile) Then
                fs = New FileStream(sRarFile, FileMode.Open)
                intRar = 1
            Else
                fs = New FileStream(pv_TxtFile.Tag, FileMode.Open)
                intRar = 0
            End If

            rd = New BinaryReader(fs)
            arrData = rd.ReadBytes(CInt(fs.Length))
            fs.Flush()
            fs.Close()
        Catch ex As Exception
            pv_TxtFile.Text = ex.Message
            Return
        End Try
        If SaveFile3(pv_TxtFile, arrFilename(0), arrFilename(0).Replace(arrFilename(0).Substring(arrFilename(0).LastIndexOf(".") + 1), "rar"), arrData, pv_txtVer.Text.Trim, intRar, IIf(pv_chkPatch.Checked, 1, 0), CDec(pv_txtCapacity.Text.Trim.ToUpper.Replace("BYTES", "")), "") Then
            pv_TxtFile.Text = "Đã cập nhật lên Server"
            pv_TxtFile.Refresh()
            If File.Exists(sRarFile) Then
                File.Delete(sRarFile)
            End If
        End If
    End Sub
    Private Sub UpdateVersion4(ByVal pv_TxtFile As TextBox, ByVal pv_ChkCompress As CheckBox, ByVal pv_txtCapacity As TextBox, ByVal pv_txtVer As TextBox, ByVal pv_chkPatch As CheckBox)
        Dim arrFilename() As String = Split(pv_TxtFile.Tag, "\")
        Dim sRarFile As String
        If pv_TxtFile.Text.Trim = String.Empty Or pv_TxtFile.Tag = String.Empty Then Return
        sRarFile = pv_TxtFile.Tag.Replace(pv_TxtFile.Tag.Substring(pv_TxtFile.Tag.LastIndexOf(".") + 1), "RAR")
        arrFilename.Reverse(arrFilename)
        Dim fs As FileStream
        Dim intRar As Integer
        Dim rd As BinaryReader
        Dim arrData() As Byte
        Try
            If File.Exists(sRarFile) Then
                fs = New FileStream(sRarFile, FileMode.Open)
                intRar = 1
            Else
                fs = New FileStream(pv_TxtFile.Tag, FileMode.Open)
                intRar = 0
            End If

            rd = New BinaryReader(fs)
            arrData = rd.ReadBytes(CInt(fs.Length))
            fs.Flush()
            fs.Close()
        Catch ex As Exception
            pv_TxtFile.Text = ex.Message
            Return
        End Try
        If SaveFile4(pv_TxtFile, arrFilename(0), arrFilename(0).Replace(arrFilename(0).Substring(arrFilename(0).LastIndexOf(".") + 1), "rar"), arrData, pv_txtVer.Text.Trim, intRar, IIf(pv_chkPatch.Checked, 1, 0), CDec(pv_txtCapacity.Text.Trim.ToUpper.Replace("BYTES", "")), "") Then
            pv_TxtFile.Text = "Đã cập nhật lên Server"
            pv_TxtFile.Refresh()
            If File.Exists(sRarFile) Then
                File.Delete(sRarFile)
            End If
        End If
    End Sub
    Private Sub UpdateVersion5(ByVal pv_TxtFile As TextBox, ByVal pv_ChkCompress As CheckBox, ByVal pv_txtCapacity As TextBox, ByVal pv_txtVer As TextBox, ByVal pv_chkPatch As CheckBox)
        Dim arrFilename() As String = Split(pv_TxtFile.Tag, "\")
        Dim sRarFile As String
        If pv_TxtFile.Text.Trim = String.Empty Or pv_TxtFile.Tag = String.Empty Then Return
        sRarFile = pv_TxtFile.Tag.Replace(pv_TxtFile.Tag.Substring(pv_TxtFile.Tag.LastIndexOf(".") + 1), "RAR")
        arrFilename.Reverse(arrFilename)
        Dim fs As FileStream
        Dim intRar As Integer
        Dim rd As BinaryReader
        Dim arrData() As Byte
        Try
            If File.Exists(sRarFile) Then
                fs = New FileStream(sRarFile, FileMode.Open)
                intRar = 1
            Else
                fs = New FileStream(pv_TxtFile.Tag, FileMode.Open)
                intRar = 0
            End If
            rd = New BinaryReader(fs)
            arrData = rd.ReadBytes(CInt(fs.Length))
            fs.Flush()
            fs.Close()
        Catch ex As Exception
            pv_TxtFile.Text = ex.Message
            Return
        End Try
        If SaveFile5(pv_TxtFile, arrFilename(0), arrFilename(0).Replace(arrFilename(0).Substring(arrFilename(0).LastIndexOf(".") + 1), "rar"), arrData, pv_txtVer.Text.Trim, intRar, IIf(pv_chkPatch.Checked, 1, 0), CDec(pv_txtCapacity.Text.Trim.ToUpper.Replace("BYTES", "")), "") Then
            pv_TxtFile.Text = "Đã cập nhật lên Server"
            pv_TxtFile.Refresh()
            If File.Exists(sRarFile) Then
                File.Delete(sRarFile)
            End If
        End If
    End Sub
    Private Function bCheckData(ByVal pv_objTxt As TextBox) As Boolean
        Try
            If pv_objTxt.Text.Trim.Equals(String.Empty) Then
                Return False
            End If
            Return True
        Catch ex As Exception

        End Try
    End Function
    Private Function CompressFile(ByVal pv_objTxt As TextBox, ByVal pv_sFilePath As String) As Boolean
        Try
            Dim pStartupPath As String = Application.StartupPath
            Dim info As New ProcessStartInfo
            Dim pv_sRarFile As String
            pv_sRarFile = pv_sFilePath.Replace(pv_sFilePath.Substring(pv_sFilePath.LastIndexOf(".") + 1), "RAR")
            info.FileName = pStartupPath & "\WinRAR\WinRAR.exe"
            info.Arguments = "a -pSYSMAN -ep -o+ " & Chr(34) & pv_sRarFile & Chr(34) & " " & Chr(34) & pv_sFilePath & Chr(34)
            info.WindowStyle = ProcessWindowStyle.Hidden
            pv_objTxt.Text = "Đang thực hiện Nén File..."
            pv_objTxt.Refresh()
            Me.Refresh()
            Dim pro As Process = System.Diagnostics.Process.Start(info)
            pro.WaitForExit()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function SaveFile1(ByVal pv_objTxt As TextBox, ByVal pv_sFileName As String, ByVal pv_sRarFileName As String, ByVal pv_arrData() As Byte, ByVal pv_sVersion As String, ByVal pv_intRar As Int16, _
                        ByVal pv_intPatch As Int16, ByVal pv_decCapacity As Decimal, ByVal pv_sDesc As String) As Boolean

        Try

            Dim strSQL As String = "INSERT INTO Sys_VERSION(sFileName,sRarFileName,objData,sVersion,intRar,intPatch,dblCapacity,sDesc) VALUES(@_sFileName,@_sRarFileName,@_objData,@_sVersion,@_intRar,@_intPatch,@_dblCapacity,@_sDesc)"
            If g1 Is Nothing Then
                g1 = New SqlConnection(mv_sConnString)
                g1.Open()
            ElseIf g1.State = ConnectionState.Closed Then
                g1.Open()
            End If
            Dim cmd As SqlCommand = New SqlCommand(strSQL, g1)
            With cmd
                .Parameters.Add(New SqlParameter("@_sFileName", SqlDbType.NVarChar)).Value = pv_sFileName
                .Parameters.Add(New SqlParameter("@_sRarFileName", SqlDbType.NVarChar)).Value = pv_sRarFileName
                .Parameters.Add(New SqlParameter("@_objData", SqlDbType.Image)).Value = pv_arrData
                .Parameters.Add(New SqlParameter("@_sVersion", SqlDbType.NVarChar)).Value = pv_sVersion
                .Parameters.Add(New SqlParameter("@_intRar", SqlDbType.SmallInt)).Value = pv_intRar
                .Parameters.Add(New SqlParameter("@_intPatch", SqlDbType.SmallInt)).Value = pv_intPatch
                .Parameters.Add(New SqlParameter("@_dblCapacity", SqlDbType.Float)).Value = pv_decCapacity
                .Parameters.Add(New SqlParameter("@_sDesc", SqlDbType.NVarChar)).Value = pv_sDesc
                .ExecuteNonQuery()
            End With
            If g1.State = ConnectionState.Open Then
                g1.Close()
            End If
            If t1.ThreadState = ThreadState.Running Or t2.ThreadState = ThreadState.Running Or t3.ThreadState = ThreadState.Running Or t4.ThreadState = ThreadState.Running Or t5.ThreadState = ThreadState.Running Then
            Else
                Me.Cursor = Cursors.Default
            End If

            Return True
        Catch ex As Exception
            pv_objTxt.Text = ex.Message
            If t1.ThreadState = ThreadState.Running Or t2.ThreadState = ThreadState.Running Or t3.ThreadState = ThreadState.Running Or t4.ThreadState = ThreadState.Running Or t5.ThreadState = ThreadState.Running Then
            Else
                Me.Cursor = Cursors.Default
            End If
            Return False
        End Try

    End Function
    Private Function SaveFile2(ByVal pv_objTxt As TextBox, ByVal pv_sFileName As String, ByVal pv_sRarFileName As String, ByVal pv_arrData() As Byte, ByVal pv_sVersion As String, ByVal pv_intRar As Int16, _
                      ByVal pv_intPatch As Int16, ByVal pv_decCapacity As Decimal, ByVal pv_sDesc As String) As Boolean

        Try

            Dim strSQL As String = "INSERT INTO Sys_VERSION(sFileName,sRarFileName,objData,sVersion,intRar,intPatch,dblCapacity,sDesc) VALUES(@_sFileName,@_sRarFileName,@_objData,@_sVersion,@_intRar,@_intPatch,@_dblCapacity,@_sDesc)"
            If g2 Is Nothing Then
                g2 = New SqlConnection(mv_sConnString)
                g2.Open()
            ElseIf g2.State = ConnectionState.Closed Then
                g2.Open()
            End If
            Dim cmd As SqlCommand = New SqlCommand(strSQL, g2)
            With cmd
                .Parameters.Add(New SqlParameter("@_sFileName", SqlDbType.NVarChar)).Value = pv_sFileName
                .Parameters.Add(New SqlParameter("@_sRarFileName", SqlDbType.NVarChar)).Value = pv_sRarFileName
                .Parameters.Add(New SqlParameter("@_objData", SqlDbType.Image)).Value = pv_arrData
                .Parameters.Add(New SqlParameter("@_sVersion", SqlDbType.NVarChar)).Value = pv_sVersion
                .Parameters.Add(New SqlParameter("@_intRar", SqlDbType.SmallInt)).Value = pv_intRar
                .Parameters.Add(New SqlParameter("@_intPatch", SqlDbType.SmallInt)).Value = pv_intPatch
                .Parameters.Add(New SqlParameter("@_dblCapacity", SqlDbType.Float)).Value = pv_decCapacity
                .Parameters.Add(New SqlParameter("@_sDesc", SqlDbType.NVarChar)).Value = pv_sDesc
                .ExecuteNonQuery()
            End With
            If g2.State = ConnectionState.Open Then
                g2.Close()
            End If
            If t1.ThreadState = ThreadState.Running Or t2.ThreadState = ThreadState.Running Or t3.ThreadState = ThreadState.Running Or t4.ThreadState = ThreadState.Running Or t5.ThreadState = ThreadState.Running Then
            Else
                Me.Cursor = Cursors.Default
            End If
            Return True
        Catch ex As Exception
            pv_objTxt.Text = ex.Message
            If t1.ThreadState = ThreadState.Running Or t2.ThreadState = ThreadState.Running Or t3.ThreadState = ThreadState.Running Or t4.ThreadState = ThreadState.Running Or t5.ThreadState = ThreadState.Running Then
            Else
                Me.Cursor = Cursors.Default
            End If
            Return False
        End Try

    End Function
    Private Function SaveFile3(ByVal pv_objTxt As TextBox, ByVal pv_sFileName As String, ByVal pv_sRarFileName As String, ByVal pv_arrData() As Byte, ByVal pv_sVersion As String, ByVal pv_intRar As Int16, _
                      ByVal pv_intPatch As Int16, ByVal pv_decCapacity As Decimal, ByVal pv_sDesc As String) As Boolean

        Try

            Dim strSQL As String = "INSERT INTO Sys_VERSION(sFileName,sRarFileName,objData,sVersion,intRar,intPatch,dblCapacity,sDesc) VALUES(@_sFileName,@_sRarFileName,@_objData,@_sVersion,@_intRar,@_intPatch,@_dblCapacity,@_sDesc)"
            If g3 Is Nothing Then
                g3 = New SqlConnection(mv_sConnString)
                g3.Open()
            ElseIf g3.State = ConnectionState.Closed Then
                g3.Open()
            End If
            Dim cmd As SqlCommand = New SqlCommand(strSQL, g3)
            With cmd
                .Parameters.Add(New SqlParameter("@_sFileName", SqlDbType.NVarChar)).Value = pv_sFileName
                .Parameters.Add(New SqlParameter("@_sRarFileName", SqlDbType.NVarChar)).Value = pv_sRarFileName
                .Parameters.Add(New SqlParameter("@_objData", SqlDbType.Image)).Value = pv_arrData
                .Parameters.Add(New SqlParameter("@_sVersion", SqlDbType.NVarChar)).Value = pv_sVersion
                .Parameters.Add(New SqlParameter("@_intRar", SqlDbType.SmallInt)).Value = pv_intRar
                .Parameters.Add(New SqlParameter("@_intPatch", SqlDbType.SmallInt)).Value = pv_intPatch
                .Parameters.Add(New SqlParameter("@_dblCapacity", SqlDbType.Float)).Value = pv_decCapacity
                .Parameters.Add(New SqlParameter("@_sDesc", SqlDbType.NVarChar)).Value = pv_sDesc
                .ExecuteNonQuery()
            End With
            If g3.State = ConnectionState.Open Then
                g3.Close()
            End If
            If t1.ThreadState = ThreadState.Running Or t2.ThreadState = ThreadState.Running Or t3.ThreadState = ThreadState.Running Or t4.ThreadState = ThreadState.Running Or t5.ThreadState = ThreadState.Running Then
            Else
                Me.Cursor = Cursors.Default
            End If
            Return True
        Catch ex As Exception
            pv_objTxt.Text = ex.Message
            If t1.ThreadState = ThreadState.Running Or t2.ThreadState = ThreadState.Running Or t3.ThreadState = ThreadState.Running Or t4.ThreadState = ThreadState.Running Or t5.ThreadState = ThreadState.Running Then
            Else
                Me.Cursor = Cursors.Default
            End If
            Return False
        End Try

    End Function
    Private Function SaveFile4(ByVal pv_objTxt As TextBox, ByVal pv_sFileName As String, ByVal pv_sRarFileName As String, ByVal pv_arrData() As Byte, ByVal pv_sVersion As String, ByVal pv_intRar As Int16, _
                      ByVal pv_intPatch As Int16, ByVal pv_decCapacity As Decimal, ByVal pv_sDesc As String) As Boolean

        Try

            Dim strSQL As String = "INSERT INTO Sys_VERSION(sFileName,sRarFileName,objData,sVersion,intRar,intPatch,dblCapacity,sDesc) VALUES(@_sFileName,@_sRarFileName,@_objData,@_sVersion,@_intRar,@_intPatch,@_dblCapacity,@_sDesc)"
            If g4 Is Nothing Then
                g4 = New SqlConnection(mv_sConnString)
                g4.Open()
            ElseIf g5.State = ConnectionState.Closed Then
                g4.Open()
            End If
            Dim cmd As SqlCommand = New SqlCommand(strSQL, g4)
            With cmd
                .Parameters.Add(New SqlParameter("@_sFileName", SqlDbType.NVarChar)).Value = pv_sFileName
                .Parameters.Add(New SqlParameter("@_sRarFileName", SqlDbType.NVarChar)).Value = pv_sRarFileName
                .Parameters.Add(New SqlParameter("@_objData", SqlDbType.Image)).Value = pv_arrData
                .Parameters.Add(New SqlParameter("@_sVersion", SqlDbType.NVarChar)).Value = pv_sVersion
                .Parameters.Add(New SqlParameter("@_intRar", SqlDbType.SmallInt)).Value = pv_intRar
                .Parameters.Add(New SqlParameter("@_intPatch", SqlDbType.SmallInt)).Value = pv_intPatch
                .Parameters.Add(New SqlParameter("@_dblCapacity", SqlDbType.Float)).Value = pv_decCapacity
                .Parameters.Add(New SqlParameter("@_sDesc", SqlDbType.NVarChar)).Value = pv_sDesc
                .ExecuteNonQuery()
            End With
            If g4.State = ConnectionState.Open Then
                g4.Close()
            End If
            If t1.ThreadState = ThreadState.Running Or t2.ThreadState = ThreadState.Running Or t3.ThreadState = ThreadState.Running Or t4.ThreadState = ThreadState.Running Or t5.ThreadState = ThreadState.Running Then
            Else
                Me.Cursor = Cursors.Default
            End If
            Return True
        Catch ex As Exception
            pv_objTxt.Text = ex.Message
            If t1.ThreadState = ThreadState.Running Or t2.ThreadState = ThreadState.Running Or t3.ThreadState = ThreadState.Running Or t4.ThreadState = ThreadState.Running Or t5.ThreadState = ThreadState.Running Then
            Else
                Me.Cursor = Cursors.Default
            End If
            Return False
        End Try

    End Function
    Private Function SaveFile5(ByVal pv_objTxt As TextBox, ByVal pv_sFileName As String, ByVal pv_sRarFileName As String, ByVal pv_arrData() As Byte, ByVal pv_sVersion As String, ByVal pv_intRar As Int16, _
                      ByVal pv_intPatch As Int16, ByVal pv_decCapacity As Decimal, ByVal pv_sDesc As String) As Boolean

        Try

            Dim strSQL As String = "INSERT INTO Sys_VERSION(sFileName,sRarFileName,objData,sVersion,intRar,intPatch,dblCapacity,sDesc) VALUES(@_sFileName,@_sRarFileName,@_objData,@_sVersion,@_intRar,@_intPatch,@_dblCapacity,@_sDesc)"
            If g5 Is Nothing Then
                g5 = New SqlConnection(mv_sConnString)
                g5.Open()
            ElseIf g5.State = ConnectionState.Closed Then
                g5.Open()
            End If
            Dim cmd As SqlCommand = New SqlCommand(strSQL, g5)
            With cmd
                .Parameters.Add(New SqlParameter("@_sFileName", SqlDbType.NVarChar)).Value = pv_sFileName
                .Parameters.Add(New SqlParameter("@_sRarFileName", SqlDbType.NVarChar)).Value = pv_sRarFileName
                .Parameters.Add(New SqlParameter("@_objData", SqlDbType.Image)).Value = pv_arrData
                .Parameters.Add(New SqlParameter("@_sVersion", SqlDbType.NVarChar)).Value = pv_sVersion
                .Parameters.Add(New SqlParameter("@_intRar", SqlDbType.SmallInt)).Value = pv_intRar
                .Parameters.Add(New SqlParameter("@_intPatch", SqlDbType.SmallInt)).Value = pv_intPatch
                .Parameters.Add(New SqlParameter("@_dblCapacity", SqlDbType.Float)).Value = pv_decCapacity
                .Parameters.Add(New SqlParameter("@_sDesc", SqlDbType.NVarChar)).Value = pv_sDesc
                .ExecuteNonQuery()
            End With
            If g5.State = ConnectionState.Open Then
                g5.Close()
            End If
            If t1.ThreadState = ThreadState.Running Or t2.ThreadState = ThreadState.Running Or t3.ThreadState = ThreadState.Running Or t4.ThreadState = ThreadState.Running Or t5.ThreadState = ThreadState.Running Then
            Else
                Me.Cursor = Cursors.Default
            End If
            Return True
        Catch ex As Exception
            pv_objTxt.Text = ex.Message
            If t1.ThreadState = ThreadState.Running Or t2.ThreadState = ThreadState.Running Or t3.ThreadState = ThreadState.Running Or t4.ThreadState = ThreadState.Running Or t5.ThreadState = ThreadState.Running Then
            Else
                Me.Cursor = Cursors.Default
            End If
            Return False
        End Try

    End Function

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim sv_sFile As String
        Try
            CheckRarExists()
            If Not File.Exists(Application.StartupPath & "\WINRAR\WINRAR.EXE") Then
                Return
            Else
                t1 = New Thread(AddressOf UpdateFile1)
                t1.Start()
                t2 = New Thread(AddressOf UpdateFile2)
                t2.Start()
                t3 = New Thread(AddressOf UpdateFile3)
                t3.Start()
                t4 = New Thread(AddressOf UpdateFile4)
                t4.Start()
                t5 = New Thread(AddressOf UpdateFile5)
                t5.Start()

            End If
            cmdSave.Enabled = False
        Catch ex As Exception
        End Try

    End Sub


    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click, PictureBox2.Click, PictureBox3.Click, PictureBox4.Click, PictureBox5.Click, PictureBox6.Click
        Try
            Select Case sender.name
                Case PictureBox1.Name
                    PictureBox1.Visible = False
                    PictureBox2.Visible = True
                    PictureBox2.BringToFront()
                Case PictureBox2.Name
                    PictureBox2.Visible = False
                    PictureBox3.Visible = True
                    PictureBox3.BringToFront()
                Case PictureBox3.Name
                    PictureBox3.Visible = False
                    PictureBox4.Visible = True
                    PictureBox4.BringToFront()
                Case PictureBox4.Name
                    PictureBox4.Visible = False
                    PictureBox5.Visible = True
                    PictureBox5.BringToFront()
                Case PictureBox5.Name
                    PictureBox5.Visible = False
                    PictureBox6.Visible = True
                    PictureBox6.BringToFront()
                Case PictureBox6.Name
                    PictureBox6.Visible = False
                    PictureBox1.Visible = True
                    PictureBox1.BringToFront()
            End Select
        Catch ex As Exception

        End Try

    End Sub
End Class
