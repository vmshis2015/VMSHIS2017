Public Class frm_addImgAndIcon
    Inherits System.Windows.Forms.Form
    Public mv_htb As New Hashtable
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
    Friend WithEvents cmdRemove As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents cmdOpen As System.Windows.Forms.Button
    Public WithEvents lstImgAndIcon As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_addImgAndIcon))
        Me.cmdRemove = New System.Windows.Forms.Button
        Me.lstImgAndIcon = New System.Windows.Forms.ListBox
        Me.cmdOpen = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmdClose = New System.Windows.Forms.Button
        Me.cmdOK = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdRemove
        '
        Me.cmdRemove.Enabled = False
        Me.cmdRemove.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRemove.Image = CType(resources.GetObject("cmdRemove.Image"), System.Drawing.Image)
        Me.cmdRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdRemove.Location = New System.Drawing.Point(87, 243)
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.Size = New System.Drawing.Size(69, 23)
        Me.cmdRemove.TabIndex = 5
        Me.cmdRemove.Text = "&Delete"
        Me.cmdRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lstImgAndIcon
        '
        Me.lstImgAndIcon.Location = New System.Drawing.Point(3, 3)
        Me.lstImgAndIcon.Name = "lstImgAndIcon"
        Me.lstImgAndIcon.Size = New System.Drawing.Size(189, 225)
        Me.lstImgAndIcon.TabIndex = 3
        '
        'cmdOpen
        '
        Me.cmdOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOpen.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOpen.Image = CType(resources.GetObject("cmdOpen.Image"), System.Drawing.Image)
        Me.cmdOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdOpen.Location = New System.Drawing.Point(195, 3)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(27, 24)
        Me.cmdOpen.TabIndex = 8
        Me.cmdOpen.TabStop = False
        Me.cmdOpen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(195, 33)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(168, 195)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Khung hiển thị"
        '
        'PictureBox1
        '
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(18, 45)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(138, 117)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(24, 90)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(123, 21)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Not Available"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmdClose
        '
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdClose.Location = New System.Drawing.Point(273, 243)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(79, 24)
        Me.cmdClose.TabIndex = 11
        Me.cmdClose.Text = "&Thoát"
        '
        'cmdOK
        '
        Me.cmdOK.Enabled = False
        Me.cmdOK.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOK.Image = CType(resources.GetObject("cmdOK.Image"), System.Drawing.Image)
        Me.cmdOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdOK.Location = New System.Drawing.Point(6, 243)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(72, 23)
        Me.cmdOK.TabIndex = 10
        Me.cmdOK.Text = "&Save"
        Me.cmdOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(3, 231)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(357, 3)
        Me.GroupBox2.TabIndex = 12
        Me.GroupBox2.TabStop = False
        '
        'frm_addImgAndIcon
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(364, 278)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdOpen)
        Me.Controls.Add(Me.cmdRemove)
        Me.Controls.Add(Me.lstImgAndIcon)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frm_addImgAndIcon"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Thêm ảnh nền và Icon"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub cmdOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen.Click
        Dim fileDiag As New OpenFileDialog
        Try
            fileDiag.Multiselect = True
            fileDiag.Title = "Chọn Icon cho các nút trên ToolBar"
            fileDiag.Filter = "All files|*.ico;*.bmp;*.gif;*.jpg|Icon|*.ico|Gif|*.gif|Jpg|*.Jpg|Bmp|*.Bmp|PNG|*.PNG"
            If fileDiag.ShowDialog = DialogResult.OK Then
                Dim _fileNames() As String
                Dim _fileName As String
                _fileNames = fileDiag.FileNames()
                For i As Integer = 0 To _fileNames.GetLength(0) - 1
                    If mv_htb.Contains(_fileNames(i).ToUpper) Then
                    Else
                        mv_htb.Add(_fileNames(i), _fileNames(i))
                        lstImgAndIcon.Items.Add(_fileNames(i))
                    End If
                Next
                cmdRemove.Enabled = True
                cmdOK.Enabled = True
                lstImgAndIcon.SelectedIndex = 0
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        mv_bCancel = False
        PictureBox1.Image = Nothing
        GC.Collect()
        Me.Close()
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        mv_bCancel = True
        Me.Close()
    End Sub

    Private Sub cmdRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemove.Click
        Dim _index As Integer
        Dim k As Integer = 0
        Try
            _index = lstImgAndIcon.SelectedIndex
            mv_htb.Remove(lstImgAndIcon.Items(_index))
            lstImgAndIcon.Items.RemoveAt(lstImgAndIcon.SelectedIndex)
            If lstImgAndIcon.Items.Count > 0 Then
                If _index <= lstImgAndIcon.Items.Count - 1 Then
                    lstImgAndIcon.SelectedIndex = _index
                Else
                    lstImgAndIcon.SelectedIndex = _index - 1
                End If
            End If
            If lstImgAndIcon.Items.Count = 0 Then
                cmdRemove.Enabled = False
                cmdOK.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lstImgAndIcon_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstImgAndIcon.SelectedIndexChanged
        Try
            If lstImgAndIcon.Items.Count > 0 Then
                Label1.SendToBack()
                If InStr(lstImgAndIcon.Items(lstImgAndIcon.SelectedIndex).ToString.ToUpper, ".ICO", CompareMethod.Text) > 0 Then
                    PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage
                Else
                    PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
                End If
                PictureBox1.Image = Image.FromFile(lstImgAndIcon.Items(lstImgAndIcon.SelectedIndex))
            End If
        Catch ex As Exception
            PictureBox1.Image = Nothing
            Label1.BringToFront()
        End Try
    End Sub

    Private Sub lstImgAndIcon_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstImgAndIcon.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    cmdRemove.PerformClick()
                Case Keys.Escape
                    cmdClose.PerformClick()
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frm_addImgAndIcon_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Escape
                    cmdClose.PerformClick()
            End Select
        Catch ex As Exception

        End Try
    End Sub
End Class
