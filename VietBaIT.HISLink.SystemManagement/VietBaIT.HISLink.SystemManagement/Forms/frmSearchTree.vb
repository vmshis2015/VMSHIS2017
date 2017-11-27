Public Class frmSearchTree
    Inherits System.Windows.Forms.Form
    Public mv_sSearch As String
    Public mv_sValue As String
    Public mv_bSearchLike As Boolean = True
    Public mv_bHasSearch As Boolean = False
    Public mv_sOldStatus As String

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
    Friend WithEvents cmdSearch As System.Windows.Forms.Button
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents chkSearchLike As System.Windows.Forms.CheckBox
    Friend WithEvents chkSearchBranch As System.Windows.Forms.CheckBox
    Friend WithEvents cboValue As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmSearchTree))
        Me.cmdSearch = New System.Windows.Forms.Button
        Me.chkSearchLike = New System.Windows.Forms.CheckBox
        Me.lblStatus = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.chkSearchBranch = New System.Windows.Forms.CheckBox
        Me.cboValue = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'cmdSearch
        '
        Me.cmdSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.cmdSearch.Location = New System.Drawing.Point(105, 105)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(90, 24)
        Me.cmdSearch.TabIndex = 3
        Me.cmdSearch.Text = "&Tìm kiếm(F3)"
        '
        'chkSearchLike
        '
        Me.chkSearchLike.Location = New System.Drawing.Point(6, 72)
        Me.chkSearchLike.Name = "chkSearchLike"
        Me.chkSearchLike.Size = New System.Drawing.Size(129, 24)
        Me.chkSearchLike.TabIndex = 2
        Me.chkSearchLike.Text = "Tìm đúng theo giá trị"
        '
        'lblStatus
        '
        Me.lblStatus.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(48, 3)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(228, 36)
        Me.lblStatus.TabIndex = 5
        Me.lblStatus.Text = "Giá trị tìm kiếm"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(6, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(36, 36)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 8
        Me.PictureBox1.TabStop = False
        '
        'chkSearchBranch
        '
        Me.chkSearchBranch.Checked = True
        Me.chkSearchBranch.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearchBranch.Location = New System.Drawing.Point(141, 72)
        Me.chkSearchBranch.Name = "chkSearchBranch"
        Me.chkSearchBranch.Size = New System.Drawing.Size(165, 24)
        Me.chkSearchBranch.TabIndex = 1
        Me.chkSearchBranch.Text = "Tìm theo nhánh được chọn"
        '
        'cboValue
        '
        Me.cboValue.Location = New System.Drawing.Point(9, 45)
        Me.cboValue.Name = "cboValue"
        Me.cboValue.Size = New System.Drawing.Size(300, 21)
        Me.cboValue.TabIndex = 0
        '
        'frmSearchTree
        '
        Me.AcceptButton = Me.cmdSearch
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(309, 137)
        Me.Controls.Add(Me.cboValue)
        Me.Controls.Add(Me.chkSearchBranch)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.chkSearchLike)
        Me.Controls.Add(Me.cmdSearch)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmSearchTree"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tìm kiếm trên TreeView"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmSearchTree_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mv_bHasSearch = False
        LoadKeyhasSearched()
    End Sub
    Private Sub LoadKeyhasSearched()
        Try
            For i As Integer = gv_arrKeyhasSearched.Count - 1 To 0 Step -1
                cboValue.Items.Add(gv_arrKeyhasSearched.Item(i))
            Next
            If cboValue.Items.Count > 0 Then cboValue.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub
    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If cboValue.Text.Trim = String.Empty Then
            'MessageBox.Show("Bạn phải nhập giá trị tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            mv_bHasSearch = False
            cboValue.Focus()
        Else
            If Not gv_arrKeyhasSearched.Contains(cboValue.Text.Trim) Then
                gv_arrKeyhasSearched.Add(cboValue.Text.Trim)
            End If
            mv_sValue = cboValue.Text.Trim
            mv_bHasSearch = True
            mv_bSearchLike = Not chkSearchLike.Checked
            Me.Close()
        End If
    End Sub

    Private Sub frmSearchTree_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                cmdSearch.PerformClick()
            Case Keys.F3
                cmdSearch.PerformClick()
            Case Keys.Escape
                mv_bHasSearch = False
                Me.Close()
        End Select
    End Sub

    Private Sub chkSearchBranch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSearchBranch.CheckedChanged
        Try
            If chkSearchBranch.Checked Then
                lblStatus.Text = mv_sOldStatus
            Else
                lblStatus.Text = "Tìm kiếm trên toàn cây"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Try
            If Asc(e.KeyChar) = Keys.Enter Then e.Handled = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cboValue_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboValue.SelectedIndexChanged

    End Sub

    Private Sub cboValue_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboValue.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                cmdSearch.PerformClick()
            Case Keys.Escape
                Me.Close()
        End Select
    End Sub
End Class
