Public Class frmSearchFunctions
    Inherits System.Windows.Forms.Form
    Public mv_sSearch As String
    Public mv_sDLL, mv_sFunction As String
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
    Friend WithEvents cboDLL As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboFunction As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmSearchFunctions))
        Me.cmdSearch = New System.Windows.Forms.Button
        Me.lblStatus = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.cboDLL = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.cboFunction = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'cmdSearch
        '
        Me.cmdSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.cmdSearch.Location = New System.Drawing.Point(93, 114)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(90, 27)
        Me.cmdSearch.TabIndex = 2
        Me.cmdSearch.Text = "&Tìm kiếm(F3)"
        '
        'lblStatus
        '
        Me.lblStatus.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.ForeColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(64, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(75, 3)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(201, 36)
        Me.lblStatus.TabIndex = 5
        Me.lblStatus.Text = "Nhập các giá trị tìm kiếm"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(6, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(42, 36)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 8
        Me.PictureBox1.TabStop = False
        '
        'cboDLL
        '
        Me.cboDLL.Location = New System.Drawing.Point(75, 51)
        Me.cboDLL.Name = "cboDLL"
        Me.cboDLL.Size = New System.Drawing.Size(201, 21)
        Me.cboDLL.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 21)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Tên DLL"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 81)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 21)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Tên hàm"
        '
        'cboFunction
        '
        Me.cboFunction.Location = New System.Drawing.Point(75, 81)
        Me.cboFunction.Name = "cboFunction"
        Me.cboFunction.Size = New System.Drawing.Size(201, 21)
        Me.cboFunction.TabIndex = 1
        '
        'frmSearchFunctions
        '
        Me.AcceptButton = Me.cmdSearch
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(279, 152)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboFunction)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboDLL)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.cmdSearch)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmSearchFunctions"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tìm kiếm chức năng"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmSearchFunctions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mv_bHasSearch = False
        LoadKeyhasSearched()
    End Sub
    Private Sub LoadKeyhasSearched()
        Try
            For i As Integer = gv_arrDLLhasSearched.Count - 1 To 0 Step -1
                cboDLL.Items.Add(gv_arrDLLhasSearched.Item(i))
            Next
            For i As Integer = gv_arrFunctionhasSearched.Count - 1 To 0 Step -1
                cboFunction.Items.Add(gv_arrFunctionhasSearched.Item(i))
            Next
            If cboDLL.Items.Count > 0 Then cboDLL.SelectedIndex = 0
            If cboFunction.Items.Count > 0 Then cboFunction.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub
    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        If cboDLL.Text.Trim = String.Empty And cboFunction.Text.Trim = String.Empty Then
            mv_bHasSearch = False
            cboDLL.Focus()
        Else
            If Not gv_arrDLLhasSearched.Contains(cboDLL.Text.Trim) Then
                gv_arrDLLhasSearched.Add(cboDLL.Text.Trim)
            End If
            If Not gv_arrFunctionhasSearched.Contains(cboFunction.Text.Trim) Then
                gv_arrFunctionhasSearched.Add(cboFunction.Text.Trim)
            End If
            mv_sDLL = cboDLL.Text.Trim
            mv_sFunction = cboFunction.Text.Trim
            mv_bHasSearch = True
            Me.Close()
        End If
    End Sub
   
    Private Sub frmSearchFunctions_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub _KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Try
            If Asc(e.KeyChar) = Keys.Enter Then e.Handled = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cboValue_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDLL.SelectedIndexChanged

    End Sub

    Private Sub cboValue_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboDLL.KeyDown, cboFunction.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter, Keys.F3
                cmdSearch.PerformClick()
            Case Keys.Escape
                Me.Close()
        End Select
    End Sub
End Class
