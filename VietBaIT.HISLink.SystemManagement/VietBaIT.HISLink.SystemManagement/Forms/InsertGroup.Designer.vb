<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InsertGroup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(InsertGroup))
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtGroupName = New System.Windows.Forms.TextBox
        Me.lstMemberList = New System.Windows.Forms.ListBox
        Me.cmdAddMember = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtDesc = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmdRemoveMember = New System.Windows.Forms.Button
        Me.cmdCreate = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chkFullAuthority = New System.Windows.Forms.CheckBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmdClose = New System.Windows.Forms.Button
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Group Name"
        '
        'txtGroupName
        '
        Me.txtGroupName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtGroupName.Location = New System.Drawing.Point(84, 59)
        Me.txtGroupName.Name = "txtGroupName"
        Me.txtGroupName.Size = New System.Drawing.Size(278, 20)
        Me.txtGroupName.TabIndex = 1
        '
        'lstMemberList
        '
        Me.lstMemberList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstMemberList.FormattingEnabled = True
        Me.lstMemberList.Location = New System.Drawing.Point(84, 105)
        Me.lstMemberList.Name = "lstMemberList"
        Me.lstMemberList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstMemberList.Size = New System.Drawing.Size(377, 173)
        Me.lstMemberList.Sorted = True
        Me.lstMemberList.TabIndex = 2
        '
        'cmdAddMember
        '
        Me.cmdAddMember.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdAddMember.Image = CType(resources.GetObject("cmdAddMember.Image"), System.Drawing.Image)
        Me.cmdAddMember.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdAddMember.Location = New System.Drawing.Point(83, 284)
        Me.cmdAddMember.Name = "cmdAddMember"
        Me.cmdAddMember.Size = New System.Drawing.Size(80, 27)
        Me.cmdAddMember.TabIndex = 5
        Me.cmdAddMember.Text = "Add"
        Me.cmdAddMember.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 14)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Desc"
        '
        'txtDesc
        '
        Me.txtDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDesc.Location = New System.Drawing.Point(84, 81)
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(278, 20)
        Me.txtDesc.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(11, 106)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 14)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Members"
        '
        'cmdRemoveMember
        '
        Me.cmdRemoveMember.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveMember.Image = CType(resources.GetObject("cmdRemoveMember.Image"), System.Drawing.Image)
        Me.cmdRemoveMember.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdRemoveMember.Location = New System.Drawing.Point(169, 284)
        Me.cmdRemoveMember.Name = "cmdRemoveMember"
        Me.cmdRemoveMember.Size = New System.Drawing.Size(80, 27)
        Me.cmdRemoveMember.TabIndex = 7
        Me.cmdRemoveMember.Text = "Remove"
        Me.cmdRemoveMember.UseVisualStyleBackColor = True
        '
        'cmdCreate
        '
        Me.cmdCreate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCreate.Image = CType(resources.GetObject("cmdCreate.Image"), System.Drawing.Image)
        Me.cmdCreate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdCreate.Location = New System.Drawing.Point(288, 341)
        Me.cmdCreate.Name = "cmdCreate"
        Me.cmdCreate.Size = New System.Drawing.Size(82, 27)
        Me.cmdCreate.TabIndex = 4
        Me.cmdCreate.Text = "Create"
        Me.cmdCreate.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 322)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(462, 2)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        '
        'chkFullAuthority
        '
        Me.chkFullAuthority.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkFullAuthority.AutoSize = True
        Me.chkFullAuthority.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFullAuthority.Location = New System.Drawing.Point(366, 61)
        Me.chkFullAuthority.Name = "chkFullAuthority"
        Me.chkFullAuthority.Size = New System.Drawing.Size(95, 18)
        Me.chkFullAuthority.TabIndex = 2
        Me.chkFullAuthority.Text = "Full Authority?"
        Me.chkFullAuthority.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Location = New System.Drawing.Point(5, 54)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(462, 2)
        Me.GroupBox4.TabIndex = 12
        Me.GroupBox4.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.SystemColors.Control
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(6, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(57, 50)
        Me.PictureBox1.TabIndex = 13
        Me.PictureBox1.TabStop = False
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(67, 13)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(128, 23)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "NHÓM NGƯỜI DÙNG"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdClose.Location = New System.Drawing.Point(378, 341)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(82, 27)
        Me.cmdClose.TabIndex = 9
        Me.cmdClose.Text = "Cancel"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'InsertGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(473, 383)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.chkFullAuthority)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdCreate)
        Me.Controls.Add(Me.cmdRemoveMember)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmdAddMember)
        Me.Controls.Add(Me.lstMemberList)
        Me.Controls.Add(Me.txtGroupName)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "InsertGroup"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "New Group"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtGroupName As System.Windows.Forms.TextBox
    Friend WithEvents lstMemberList As System.Windows.Forms.ListBox
    Friend WithEvents cmdAddMember As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmdRemoveMember As System.Windows.Forms.Button
    Friend WithEvents cmdCreate As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkFullAuthority As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Public Sub New(ByVal txt As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.Text = txt
        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
