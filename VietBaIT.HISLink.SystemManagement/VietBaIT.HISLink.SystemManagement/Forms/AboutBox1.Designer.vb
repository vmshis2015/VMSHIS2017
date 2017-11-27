<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AboutBox1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub


    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AboutBox1))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.labelCopyright = New System.Windows.Forms.Label
        Me.Button2 = New System.Windows.Forms.Button
        Me.labelCompanyName = New System.Windows.Forms.Label
        Me.labelVersion = New System.Windows.Forms.Label
        Me.labelProductName = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmdPrint = New System.Windows.Forms.Button
        Me.LogoPictureBox = New System.Windows.Forms.PictureBox
        Me.textBoxDescription = New System.Windows.Forms.TextBox
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Location = New System.Drawing.Point(-7, 349)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(674, 2)
        Me.GroupBox1.TabIndex = 30
        Me.GroupBox1.TabStop = False
        '
        'labelCopyright
        '
        Me.labelCopyright.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelCopyright.Location = New System.Drawing.Point(185, 274)
        Me.labelCopyright.Name = "labelCopyright"
        Me.labelCopyright.Size = New System.Drawing.Size(386, 70)
        Me.labelCopyright.TabIndex = 29
        Me.labelCopyright.Text = resources.GetString("labelCopyright.Text")
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(391, 365)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(86, 23)
        Me.Button2.TabIndex = 28
        Me.Button2.Text = "System info"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'labelCompanyName
        '
        Me.labelCompanyName.AutoSize = True
        Me.labelCompanyName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelCompanyName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.labelCompanyName.Location = New System.Drawing.Point(185, 94)
        Me.labelCompanyName.Name = "labelCompanyName"
        Me.labelCompanyName.Size = New System.Drawing.Size(110, 14)
        Me.labelCompanyName.TabIndex = 27
        Me.labelCompanyName.Text = "Tên viết tắt: DVC Cop"
        '
        'labelVersion
        '
        Me.labelVersion.AutoSize = True
        Me.labelVersion.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelVersion.Location = New System.Drawing.Point(185, 68)
        Me.labelVersion.Name = "labelVersion"
        Me.labelVersion.Size = New System.Drawing.Size(140, 14)
        Me.labelVersion.TabIndex = 26
        Me.labelVersion.Text = "Chủ sở hữu: Tập đoàn DVC"
        '
        'labelProductName
        '
        Me.labelProductName.AutoSize = True
        Me.labelProductName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelProductName.Location = New System.Drawing.Point(185, 44)
        Me.labelProductName.Name = "labelProductName"
        Me.labelProductName.Size = New System.Drawing.Size(93, 14)
        Me.labelProductName.TabIndex = 25
        Me.labelProductName.Text = "Phiên bản: 1.0.1.5"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Maroon
        Me.Label1.Location = New System.Drawing.Point(185, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(223, 16)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Tên phần mềm: Quản trị hệ thống"
        '
        'cmdPrint
        '
        Me.cmdPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdPrint.Location = New System.Drawing.Point(496, 365)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(75, 23)
        Me.cmdPrint.TabIndex = 23
        Me.cmdPrint.Text = "OK"
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'LogoPictureBox
        '
        Me.LogoPictureBox.Image = CType(resources.GetObject("LogoPictureBox.Image"), System.Drawing.Image)
        Me.LogoPictureBox.Location = New System.Drawing.Point(0, 11)
        Me.LogoPictureBox.Name = "LogoPictureBox"
        Me.LogoPictureBox.Size = New System.Drawing.Size(168, 166)
        Me.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.LogoPictureBox.TabIndex = 22
        Me.LogoPictureBox.TabStop = False
        '
        'textBoxDescription
        '
        Me.textBoxDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.textBoxDescription.Location = New System.Drawing.Point(188, 111)
        Me.textBoxDescription.Margin = New System.Windows.Forms.Padding(6, 3, 3, 3)
        Me.textBoxDescription.Multiline = True
        Me.textBoxDescription.Name = "textBoxDescription"
        Me.textBoxDescription.ReadOnly = True
        Me.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.textBoxDescription.Size = New System.Drawing.Size(383, 151)
        Me.textBoxDescription.TabIndex = 21
        Me.textBoxDescription.TabStop = False
        Me.textBoxDescription.Text = resources.GetString("textBoxDescription.Text")
        '
        'AboutBox1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(581, 404)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.labelCopyright)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.labelCompanyName)
        Me.Controls.Add(Me.labelVersion)
        Me.Controls.Add(Me.labelProductName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdPrint)
        Me.Controls.Add(Me.textBoxDescription)
        Me.Controls.Add(Me.LogoPictureBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AboutBox1"
        Me.Padding = New System.Windows.Forms.Padding(9)
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Thông tin về phần mềm"
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents labelCopyright As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents labelCompanyName As System.Windows.Forms.Label
    Friend WithEvents labelVersion As System.Windows.Forms.Label
    Friend WithEvents labelProductName As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdPrint As System.Windows.Forms.Button
    Friend WithEvents LogoPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents textBoxDescription As System.Windows.Forms.TextBox

End Class
