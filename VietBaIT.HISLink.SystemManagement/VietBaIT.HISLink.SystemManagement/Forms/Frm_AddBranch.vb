Imports System.IO
Imports System.Data.SqlClient

Public Class Frm_AddBranch
    Inherits System.Windows.Forms.Form
    Public mv_bInsert As Boolean = True

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
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtBankCode As System.Windows.Forms.TextBox
    Friend WithEvents txtPosition As System.Windows.Forms.TextBox
    Friend WithEvents txtProxyNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtRepresentative As System.Windows.Forms.TextBox
    Friend WithEvents txtAccountCode As System.Windows.Forms.TextBox
    Friend WithEvents txtTaxCode As System.Windows.Forms.TextBox
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtFax As System.Windows.Forms.TextBox
    Friend WithEvents txtPhone As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtUnitName As System.Windows.Forms.TextBox
    Friend WithEvents txtUnitCode As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtHotPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtSupportPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtwebsite As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents picIcon As System.Windows.Forms.PictureBox
    Friend WithEvents cmdDel As System.Windows.Forms.Button
    Friend WithEvents txtParentBranchName As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_AddBranch))
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtwebsite = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtParentBranchName = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtSupportPhone = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtHotPhone = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtBankCode = New System.Windows.Forms.TextBox()
        Me.txtPosition = New System.Windows.Forms.TextBox()
        Me.txtProxyNumber = New System.Windows.Forms.TextBox()
        Me.txtRepresentative = New System.Windows.Forms.TextBox()
        Me.txtAccountCode = New System.Windows.Forms.TextBox()
        Me.txtTaxCode = New System.Windows.Forms.TextBox()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtFax = New System.Windows.Forms.TextBox()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.txtUnitName = New System.Windows.Forms.TextBox()
        Me.txtUnitCode = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.picIcon = New System.Windows.Forms.PictureBox()
        Me.cmdDel = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSave.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSave.Location = New System.Drawing.Point(415, 357)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(81, 24)
        Me.cmdSave.TabIndex = 16
        Me.cmdSave.Text = "Ghi"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdDel)
        Me.GroupBox1.Controls.Add(Me.picIcon)
        Me.GroupBox1.Controls.Add(Me.txtwebsite)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.txtParentBranchName)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txtSupportPhone)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.txtHotPhone)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.txtBankCode)
        Me.GroupBox1.Controls.Add(Me.txtPosition)
        Me.GroupBox1.Controls.Add(Me.txtProxyNumber)
        Me.GroupBox1.Controls.Add(Me.txtRepresentative)
        Me.GroupBox1.Controls.Add(Me.txtAccountCode)
        Me.GroupBox1.Controls.Add(Me.txtTaxCode)
        Me.GroupBox1.Controls.Add(Me.txtEmail)
        Me.GroupBox1.Controls.Add(Me.txtFax)
        Me.GroupBox1.Controls.Add(Me.txtPhone)
        Me.GroupBox1.Controls.Add(Me.txtAddress)
        Me.GroupBox1.Controls.Add(Me.txtUnitName)
        Me.GroupBox1.Controls.Add(Me.txtUnitCode)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(583, 325)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Thông tin đơn vị"
        '
        'txtwebsite
        '
        Me.txtwebsite.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtwebsite.Location = New System.Drawing.Point(126, 192)
        Me.txtwebsite.MaxLength = 30
        Me.txtwebsite.Name = "txtwebsite"
        Me.txtwebsite.Size = New System.Drawing.Size(450, 21)
        Me.txtwebsite.TabIndex = 9
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(12, 195)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(111, 18)
        Me.Label16.TabIndex = 102
        Me.Label16.Text = "Web site"
        '
        'txtParentBranchName
        '
        Me.txtParentBranchName.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtParentBranchName.Location = New System.Drawing.Point(126, 69)
        Me.txtParentBranchName.MaxLength = 100
        Me.txtParentBranchName.Name = "txtParentBranchName"
        Me.txtParentBranchName.Size = New System.Drawing.Size(350, 21)
        Me.txtParentBranchName.TabIndex = 2
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Maroon
        Me.Label13.Location = New System.Drawing.Point(12, 72)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(111, 18)
        Me.Label13.TabIndex = 100
        Me.Label13.Text = "Tên CN cấp trên"
        '
        'txtSupportPhone
        '
        Me.txtSupportPhone.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupportPhone.Location = New System.Drawing.Point(371, 117)
        Me.txtSupportPhone.MaxLength = 20
        Me.txtSupportPhone.Name = "txtSupportPhone"
        Me.txtSupportPhone.Size = New System.Drawing.Size(205, 21)
        Me.txtSupportPhone.TabIndex = 5
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(266, 120)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(99, 18)
        Me.Label15.TabIndex = 98
        Me.Label15.Text = "Điện thoại hỗ trợ"
        '
        'txtHotPhone
        '
        Me.txtHotPhone.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHotPhone.Location = New System.Drawing.Point(126, 141)
        Me.txtHotPhone.MaxLength = 20
        Me.txtHotPhone.Name = "txtHotPhone"
        Me.txtHotPhone.Size = New System.Drawing.Size(134, 21)
        Me.txtHotPhone.TabIndex = 6
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(12, 144)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(108, 18)
        Me.Label14.TabIndex = 96
        Me.Label14.Text = "Điện thoại nóng"
        '
        'txtBankCode
        '
        Me.txtBankCode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankCode.Location = New System.Drawing.Point(371, 292)
        Me.txtBankCode.MaxLength = 7
        Me.txtBankCode.Name = "txtBankCode"
        Me.txtBankCode.Size = New System.Drawing.Size(205, 21)
        Me.txtBankCode.TabIndex = 15
        '
        'txtPosition
        '
        Me.txtPosition.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPosition.Location = New System.Drawing.Point(126, 268)
        Me.txtPosition.MaxLength = 50
        Me.txtPosition.Name = "txtPosition"
        Me.txtPosition.Size = New System.Drawing.Size(450, 21)
        Me.txtPosition.TabIndex = 13
        '
        'txtProxyNumber
        '
        Me.txtProxyNumber.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProxyNumber.Location = New System.Drawing.Point(126, 292)
        Me.txtProxyNumber.MaxLength = 20
        Me.txtProxyNumber.Name = "txtProxyNumber"
        Me.txtProxyNumber.Size = New System.Drawing.Size(134, 21)
        Me.txtProxyNumber.TabIndex = 14
        '
        'txtRepresentative
        '
        Me.txtRepresentative.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRepresentative.Location = New System.Drawing.Point(126, 244)
        Me.txtRepresentative.MaxLength = 50
        Me.txtRepresentative.Name = "txtRepresentative"
        Me.txtRepresentative.Size = New System.Drawing.Size(450, 21)
        Me.txtRepresentative.TabIndex = 12
        '
        'txtAccountCode
        '
        Me.txtAccountCode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccountCode.Location = New System.Drawing.Point(371, 220)
        Me.txtAccountCode.MaxLength = 15
        Me.txtAccountCode.Name = "txtAccountCode"
        Me.txtAccountCode.Size = New System.Drawing.Size(205, 21)
        Me.txtAccountCode.TabIndex = 11
        '
        'txtTaxCode
        '
        Me.txtTaxCode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTaxCode.Location = New System.Drawing.Point(126, 220)
        Me.txtTaxCode.MaxLength = 17
        Me.txtTaxCode.Name = "txtTaxCode"
        Me.txtTaxCode.Size = New System.Drawing.Size(134, 21)
        Me.txtTaxCode.TabIndex = 10
        '
        'txtEmail
        '
        Me.txtEmail.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.Location = New System.Drawing.Point(126, 165)
        Me.txtEmail.MaxLength = 30
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(450, 21)
        Me.txtEmail.TabIndex = 8
        '
        'txtFax
        '
        Me.txtFax.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFax.Location = New System.Drawing.Point(371, 141)
        Me.txtFax.MaxLength = 13
        Me.txtFax.Name = "txtFax"
        Me.txtFax.Size = New System.Drawing.Size(205, 21)
        Me.txtFax.TabIndex = 7
        '
        'txtPhone
        '
        Me.txtPhone.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPhone.Location = New System.Drawing.Point(126, 117)
        Me.txtPhone.MaxLength = 20
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(134, 21)
        Me.txtPhone.TabIndex = 4
        '
        'txtAddress
        '
        Me.txtAddress.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress.Location = New System.Drawing.Point(126, 93)
        Me.txtAddress.MaxLength = 100
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(451, 21)
        Me.txtAddress.TabIndex = 3
        '
        'txtUnitName
        '
        Me.txtUnitName.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUnitName.Location = New System.Drawing.Point(126, 45)
        Me.txtUnitName.MaxLength = 100
        Me.txtUnitName.Name = "txtUnitName"
        Me.txtUnitName.Size = New System.Drawing.Size(350, 21)
        Me.txtUnitName.TabIndex = 1
        '
        'txtUnitCode
        '
        Me.txtUnitCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtUnitCode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUnitCode.Location = New System.Drawing.Point(126, 20)
        Me.txtUnitCode.MaxLength = 10
        Me.txtUnitCode.Name = "txtUnitCode"
        Me.txtUnitCode.Size = New System.Drawing.Size(134, 21)
        Me.txtUnitCode.TabIndex = 0
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(266, 295)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(102, 18)
        Me.Label12.TabIndex = 92
        Me.Label12.Text = "Mã số ngân hàng"
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(12, 271)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(111, 18)
        Me.Label11.TabIndex = 91
        Me.Label11.Text = "Chức danh"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(12, 295)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(111, 18)
        Me.Label10.TabIndex = 90
        Me.Label10.Text = "Số giấy ủy quyền"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(12, 247)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(111, 18)
        Me.Label9.TabIndex = 89
        Me.Label9.Text = "Tên người đại diện"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(266, 223)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(99, 18)
        Me.Label8.TabIndex = 88
        Me.Label8.Text = "Mã số tài khoản"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(12, 223)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(111, 18)
        Me.Label7.TabIndex = 87
        Me.Label7.Text = "Mã số thuế"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 168)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(111, 18)
        Me.Label6.TabIndex = 86
        Me.Label6.Text = "Email"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(266, 144)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 18)
        Me.Label5.TabIndex = 85
        Me.Label5.Text = "Số Fax"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 120)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(111, 18)
        Me.Label4.TabIndex = 84
        Me.Label4.Text = "Điện thoại đơn vị"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 96)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(111, 18)
        Me.Label3.TabIndex = 83
        Me.Label3.Text = "Địa chỉ"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Maroon
        Me.Label2.Location = New System.Drawing.Point(12, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 18)
        Me.Label2.TabIndex = 82
        Me.Label2.Text = "Tên chi nhánh"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Maroon
        Me.Label1.Location = New System.Drawing.Point(12, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 18)
        Me.Label1.TabIndex = 81
        Me.Label1.Text = "Mã chi nhánh"
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdClose.Location = New System.Drawing.Point(505, 357)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(81, 25)
        Me.cmdClose.TabIndex = 17
        Me.cmdClose.Text = "Th&oát"
        '
        'picIcon
        '
        Me.picIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picIcon.Location = New System.Drawing.Point(492, 16)
        Me.picIcon.Name = "picIcon"
        Me.picIcon.Size = New System.Drawing.Size(84, 71)
        Me.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picIcon.TabIndex = 18
        Me.picIcon.TabStop = False
        '
        'cmdDel
        '
        Me.cmdDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdDel.Location = New System.Drawing.Point(463, 17)
        Me.cmdDel.Name = "cmdDel"
        Me.cmdDel.Size = New System.Drawing.Size(28, 25)
        Me.cmdDel.TabIndex = 103
        Me.cmdDel.Text = "X"
        '
        'Frm_AddBranch
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(607, 394)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "Frm_AddBranch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Đơn vị làm việc"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
       
        If txtUnitCode.Text.Trim.Equals(String.Empty) Then
            MessageBox.Show("Bạn phải nhập mã chi nhánh", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtUnitCode.Focus()
            Return
        End If
        If txtUnitName.Text.Trim.Equals(String.Empty) Then
            MessageBox.Show("Bạn phải nhập tên chi nhánh", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtUnitName.Focus()
            Return
        End If
        If txtParentBranchName.Text.Trim.Equals(String.Empty) Then
            MessageBox.Show("Bạn phải nhập tên chi nhánh cấp trên", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtParentBranchName.Focus()
            Return
        End If
        Dim sv_ssql As String
        Dim arrLogo(0) As Byte
        'VNS.Libs.Utility.ConvertImageToByteArray(picIcon.Image, System.Drawing.Imaging.ImageFormat.Jpeg)
        Image2Byte(picIcon.Image, arrLogo)
        Dim sv_oCmd As New SqlClient.SqlCommand
        If mv_bInsert Then
            sv_ssql = "INSERT INTO Sys_MANAGEMENTUNIT(PK_sBranchID,sName,sParentBranchName,sAddress,sPhone,sHotPhone,sDutyPhone,sFAX,sEMAIL,sTaxCode,sAccountID,sRepresentative,sPosition,sProxyNumber,sBankCode,website,logo) "
            sv_ssql &= " VALUES(N'" & ValidData(txtUnitCode.Text.Trim) & "',N'" & ValidData(txtUnitName.Text.Trim) & "',N'" & ValidData(txtParentBranchName.Text.Trim) & "',"
            sv_ssql &= "N'" & ValidData(txtAddress.Text.Trim) & "',N'" & ValidData(txtPhone.Text.Trim) & "',N'" & ValidData(txtHotPhone.Text.Trim) & "',N'" & ValidData(txtSupportPhone.Text.Trim) & "',"
            sv_ssql &= "N'" & ValidData(txtFax.Text.Trim) & "',N'" & ValidData(txtEmail.Text.Trim) & "',"
            sv_ssql &= "N'" & ValidData(txtTaxCode.Text.Trim) & "',N'" & ValidData(txtAccountCode.Text.Trim) & "',"
            sv_ssql &= "N'" & ValidData(txtRepresentative.Text.Trim) & "',N'" & ValidData(txtPosition.Text.Trim) & "',"
            sv_ssql &= "N'" & ValidData(txtProxyNumber.Text.Trim) & "',N'" & ValidData(txtBankCode.Text.Trim) & "',N'" & ValidData(txtwebsite.Text.Trim) & "')"
        Else
            sv_ssql = "UPDATE Sys_MANAGEMENTUNIT SET sName=N'" & ValidData(txtUnitName.Text.Trim) & "',sAddress=N'" & ValidData(txtAddress.Text.Trim)
            sv_ssql &= "',sPhone=N'" & ValidData(txtPhone.Text.Trim) & "',sFAX=N'" & ValidData(txtFax.Text.Trim) & "',sParentBranchName=N'" & ValidData(txtParentBranchName.Text.Trim)
            sv_ssql &= "',sEMAIL=N'" & ValidData(txtEmail.Text.Trim) & "',sTaxCode=N'" & ValidData(txtTaxCode.Text.Trim)
            sv_ssql &= "',sAccountID=N'" & ValidData(txtAccountCode.Text.Trim) & "',sRepresentative=N'" & ValidData(txtRepresentative.Text.Trim)
            sv_ssql &= "',sPosition=N'" & ValidData(txtPosition.Text.Trim) & "',sProxyNumber=N'" & ValidData(txtProxyNumber.Text.Trim)
            sv_ssql &= "',sHotPhone=N'" & ValidData(txtHotPhone.Text.Trim) & "',sDutyPhone=N'" & ValidData(txtSupportPhone.Text.Trim)
            sv_ssql &= "',sBankCode=N'" & ValidData(txtBankCode.Text.Trim) & "',website=N'" & ValidData(txtwebsite.Text.Trim) & "' WHERE PK_sBranchID=N'" & gv_sBranchID & "'"
        End If

        Try
            With sv_oCmd
                .Connection = VNS.Libs.globalVariables.SqlConn
                .CommandType = CommandType.Text
                .CommandText = sv_ssql
                .ExecuteNonQuery()

            End With
            If IsNothingOrDBNull(arrLogo) Then
                sv_ssql = "UPDATE Sys_MANAGEMENTUNIT set logo=null WHERE PK_sBranchID=N'" & gv_sBranchID & "'"
            Else
                sv_ssql = "UPDATE Sys_MANAGEMENTUNIT set logo=@_objData WHERE PK_sBranchID=N'" & gv_sBranchID & "'"
            End If

            Dim cmd As New SqlCommand(sv_ssql, VNS.Libs.globalVariables.SqlConn)
            If IsNothingOrDBNull(arrLogo) = False Then
                cmd.Parameters.Add(New SqlParameter("@_objData", SqlDbType.Image)).Value = arrLogo
            End If

            cmd.ExecuteNonQuery()
            gv_sBranchID = txtUnitCode.Text.Trim
            gv_sBranchName = txtUnitName.Text.Trim
            If mv_bInsert Then
                MessageBox.Show("Đã thêm đơn vị làm việc thành công. Bạn có thể phải Config lại file cấu hình cho lần chạy kế tiếp!", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            Else
                MessageBox.Show("Cập nhật thông tin đơn vị làm việc thành công!", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub Frm_AddBranch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If mv_bInsert Then
        Else
            txtUnitCode.Enabled = False
            txtUnitCode.BackColor = Color.WhiteSmoke
            LoadBranchInfo()
        End If
    End Sub
    Private Sub LoadBranchInfo()
        Dim da As New SqlClient.SqlDataAdapter("SELECT * from Sys_MANAGEMENTUNIT WHERE PK_sBranchID=N'" & gv_sBranchID & "'", VNS.Libs.globalVariables.SqlConn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                With dt.Rows(0)
                    txtAccountCode.Text = s_IsNothingOrDBNull(dt.Rows(0)("sAccountID"))
                    txtAddress.Text = s_IsNothingOrDBNull(dt.Rows(0)("sAddress"))
                    txtParentBranchName.Text = s_IsNothingOrDBNull(dt.Rows(0)("sParentBranchName"))
                    txtBankCode.Text = s_IsNothingOrDBNull(dt.Rows(0)("sBankCode"))
                    txtEmail.Text = s_IsNothingOrDBNull(dt.Rows(0)("sEMAIL"))
                    txtFax.Text = s_IsNothingOrDBNull(dt.Rows(0)("sFAX"))
                    txtHotPhone.Text = s_IsNothingOrDBNull(dt.Rows(0)("sHotPhone"))
                    txtPhone.Text = s_IsNothingOrDBNull(dt.Rows(0)("sPhone"))
                    txtPosition.Text = s_IsNothingOrDBNull(dt.Rows(0)("sPosition"))
                    txtProxyNumber.Text = s_IsNothingOrDBNull(dt.Rows(0)("sProxyNumber"))
                    txtRepresentative.Text = s_IsNothingOrDBNull(dt.Rows(0)("sRepresentative"))
                    txtSupportPhone.Text = s_IsNothingOrDBNull(dt.Rows(0)("sDutyPhone"))
                    txtTaxCode.Text = s_IsNothingOrDBNull(dt.Rows(0)("sTaxCode"))
                    txtUnitCode.Text = s_IsNothingOrDBNull(dt.Rows(0)("PK_sBranchID"))
                    txtUnitName.Text = s_IsNothingOrDBNull(dt.Rows(0)("sName"))
                    txtwebsite.Text = s_IsNothingOrDBNull(dt.Rows(0)("website"))
                    If Not dt.Rows(0)("logo") Is Nothing Then
                        Dim imageData() As Byte = CType(dt.Rows(0)("logo"), Byte())
                        'Initialize image variable
                        Dim newImage As Image
                        Byte2Image(newImage, imageData)
                        picIcon.Image = newImage
                        'Read image data into a memory stream
                        'Using ms As New System.IO.MemoryStream(imageData, 0, imageData.Length)
                        '    ms.Write(imageData, 0, imageData.Length)
                        '    picIcon.Image = Image.FromStream(ms, False)
                        'End Using
                    Else
                        picIcon.Image = Nothing
                    End If

                End With
            End If
            txtUnitName.Focus()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub Byte2Image(ByRef NewImage As Image, ByVal ByteArr() As Byte)
        '
        Dim ImageStream As MemoryStream

        Try
            If ByteArr.GetUpperBound(0) > 0 Then
                ImageStream = New MemoryStream(ByteArr)
                NewImage = Image.FromStream(ImageStream)
            Else
                NewImage = Nothing
            End If
        Catch ex As Exception
            NewImage = Nothing
        End Try

    End Sub

    ''' <summary>
    ''' Convert an image to array of bytes
    ''' </summary>
    ''' <param name="NewImage">Image to be converted</param>
    ''' <param name="ByteArr">Returns bytes</param>
    ''' <remarks></remarks>
    Public Sub Image2Byte(ByRef NewImage As Image, ByRef ByteArr() As Byte)
        '
        Dim ImageStream As MemoryStream

        Try
            ReDim ByteArr(0)
            If NewImage IsNot Nothing Then
                ImageStream = New MemoryStream
                NewImage.Save(ImageStream, Imaging.ImageFormat.Jpeg)
                ReDim ByteArr(CInt(ImageStream.Length - 1))
                ImageStream.Position = 0
                ImageStream.Read(ByteArr, 0, CInt(ImageStream.Length))
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Frm_AddBranch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then Me.ProcessTabKey(True)
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub picIcon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picIcon.Click

        Try
            Dim openDiag As New OpenFileDialog
            openDiag.Title = "Chọn biểu tượng cho Menu"
            openDiag.Filter = "Icon|*.ico|Gif|*.gif|Jpg|*.Jpg|Bmp|*.Bmp|PNG|*.PNG"
            If openDiag.ShowDialog = DialogResult.OK Then
                picIcon.Image = Image.FromFile(openDiag.FileName)
            End If
        Catch ex As Exception
            MessageBox.Show("Icon bạn vừa chọn không sử dụng được. Mời bạn chọn lại!", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
        picIcon.Image = Nothing
    End Sub
End Class
