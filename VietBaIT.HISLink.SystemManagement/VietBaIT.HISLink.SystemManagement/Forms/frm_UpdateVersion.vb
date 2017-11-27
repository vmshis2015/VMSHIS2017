Imports System.IO
Imports System.Data.SqlClient
Public Class frm_UpdateVersion
    Inherits System.Windows.Forms.Form
    Public mv_intStatus As Integer
    Public mv_sFileName As String
    Public mv_sFolderName As String
    Public mv_sRarFileName As String
    Public mv_dblCapacity As Double
    Public mv_sFileVersion As String
    Public mv_intRar As Integer
    Public mv_intPatch As Integer
    Public mv_sDesc As String
    Public mv_intID As Integer
    Public mv_ds As DataSet
    Friend WithEvents txtFolder As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Public mv_bFileHasChanged As Boolean = False

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
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmdVerPath As System.Windows.Forms.Button
    Friend WithEvents CboVerType As System.Windows.Forms.ComboBox
    Friend WithEvents txtVersion As System.Windows.Forms.TextBox
    Friend WithEvents txtCapacity As System.Windows.Forms.TextBox
    Friend WithEvents chkCompressFile As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dtmDateUpdate As System.Windows.Forms.DateTimePicker
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_UpdateVersion))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dtmDateUpdate = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmdVerPath = New System.Windows.Forms.Button()
        Me.CboVerType = New System.Windows.Forms.ComboBox()
        Me.txtVersion = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.txtCapacity = New System.Windows.Forms.TextBox()
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.chkCompressFile = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.txtFolder = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtFolder)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.dtmDateUpdate)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.cmdVerPath)
        Me.GroupBox1.Controls.Add(Me.CboVerType)
        Me.GroupBox1.Controls.Add(Me.txtVersion)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtDesc)
        Me.GroupBox1.Controls.Add(Me.txtCapacity)
        Me.GroupBox1.Controls.Add(Me.txtFilePath)
        Me.GroupBox1.Controls.Add(Me.chkCompressFile)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(411, 277)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Thông tin File"
        '
        'dtmDateUpdate
        '
        Me.dtmDateUpdate.CustomFormat = "dd/MM/yyyy"
        Me.dtmDateUpdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtmDateUpdate.Location = New System.Drawing.Point(108, 162)
        Me.dtmDateUpdate.Name = "dtmDateUpdate"
        Me.dtmDateUpdate.Size = New System.Drawing.Size(204, 22)
        Me.dtmDateUpdate.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 165)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(96, 16)
        Me.Label7.TabIndex = 35
        Me.Label7.Text = "Ngày cập nhật"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdVerPath
        '
        Me.cmdVerPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdVerPath.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdVerPath.Image = CType(resources.GetObject("cmdVerPath.Image"), System.Drawing.Image)
        Me.cmdVerPath.Location = New System.Drawing.Point(372, 24)
        Me.cmdVerPath.Name = "cmdVerPath"
        Me.cmdVerPath.Size = New System.Drawing.Size(24, 21)
        Me.cmdVerPath.TabIndex = 1
        Me.cmdVerPath.TabStop = False
        '
        'CboVerType
        '
        Me.CboVerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboVerType.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboVerType.ItemHeight = 16
        Me.CboVerType.Items.AddRange(New Object() {"Phiên bản mới", "Bản vá lỗi(Patch)"})
        Me.CboVerType.Location = New System.Drawing.Point(108, 108)
        Me.CboVerType.Name = "CboVerType"
        Me.CboVerType.Size = New System.Drawing.Size(204, 24)
        Me.CboVerType.TabIndex = 3
        '
        'txtVersion
        '
        Me.txtVersion.BackColor = System.Drawing.Color.White
        Me.txtVersion.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVersion.Location = New System.Drawing.Point(108, 135)
        Me.txtVersion.MaxLength = 100
        Me.txtVersion.Name = "txtVersion"
        Me.txtVersion.ReadOnly = True
        Me.txtVersion.Size = New System.Drawing.Size(204, 22)
        Me.txtVersion.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(4, 138)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(101, 16)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Phiên bản"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtDesc
        '
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.Location = New System.Drawing.Point(108, 189)
        Me.txtDesc.MaxLength = 255
        Me.txtDesc.Multiline = True
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDesc.Size = New System.Drawing.Size(282, 39)
        Me.txtDesc.TabIndex = 6
        '
        'txtCapacity
        '
        Me.txtCapacity.BackColor = System.Drawing.Color.White
        Me.txtCapacity.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCapacity.Location = New System.Drawing.Point(108, 81)
        Me.txtCapacity.MaxLength = 100
        Me.txtCapacity.Name = "txtCapacity"
        Me.txtCapacity.ReadOnly = True
        Me.txtCapacity.Size = New System.Drawing.Size(204, 22)
        Me.txtCapacity.TabIndex = 2
        '
        'txtFilePath
        '
        Me.txtFilePath.BackColor = System.Drawing.Color.White
        Me.txtFilePath.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilePath.Location = New System.Drawing.Point(108, 24)
        Me.txtFilePath.MaxLength = 150
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.ReadOnly = True
        Me.txtFilePath.Size = New System.Drawing.Size(264, 22)
        Me.txtFilePath.TabIndex = 0
        '
        'chkCompressFile
        '
        Me.chkCompressFile.Checked = True
        Me.chkCompressFile.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCompressFile.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCompressFile.Location = New System.Drawing.Point(107, 231)
        Me.chkCompressFile.Name = "chkCompressFile"
        Me.chkCompressFile.Size = New System.Drawing.Size(229, 19)
        Me.chkCompressFile.TabIndex = 7
        Me.chkCompressFile.Text = "Nén file trước khi cập nhật"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(5, 189)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(101, 16)
        Me.Label4.TabIndex = 31
        Me.Label4.Text = "Mô tả thêm"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(5, 111)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(101, 16)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "Kiểu phiên  bản"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(5, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 16)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Dung lượng"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(5, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 16)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Đường dẫn"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdClose
        '
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdClose.Location = New System.Drawing.Point(325, 291)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(80, 25)
        Me.cmdClose.TabIndex = 9
        Me.cmdClose.Text = "Th&oát"
        '
        'cmdSave
        '
        Me.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSave.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSave.Location = New System.Drawing.Point(235, 291)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(80, 25)
        Me.cmdSave.TabIndex = 8
        Me.cmdSave.Text = "&Ghi"
        '
        'lblStatus
        '
        Me.lblStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(7, 283)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(219, 33)
        Me.lblStatus.TabIndex = 10
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtFolder
        '
        Me.txtFolder.BackColor = System.Drawing.Color.White
        Me.txtFolder.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFolder.Location = New System.Drawing.Point(107, 52)
        Me.txtFolder.MaxLength = 150
        Me.txtFolder.Name = "txtFolder"
        Me.txtFolder.Size = New System.Drawing.Size(264, 22)
        Me.txtFolder.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(4, 56)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(101, 16)
        Me.Label8.TabIndex = 37
        Me.Label8.Text = "Thư mục:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frm_UpdateVersion
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(426, 328)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.lblStatus)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_UpdateVersion"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cập nhật phiên bản"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frm_UpdateVersion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtCapacity.BackColor = Color.WhiteSmoke
        txtFilePath.BackColor = Color.WhiteSmoke
        txtVersion.BackColor = Color.WhiteSmoke
        cmdSave.Enabled = False
        Select Case mv_intStatus
            Case 1
                CboVerType.SelectedIndex = 0
            Case 0
                LoadDataForUpdate()
        End Select

    End Sub
    Private Sub LoadDataForUpdate()
        Try
            txtFilePath.Text = mv_sFileName
            txtCapacity.Text = mv_dblCapacity
            txtVersion.Text = mv_sFileVersion
            txtDesc.Text = mv_sDesc
            txtFolder.Text = mv_sFolderName
            chkCompressFile.Checked = IIf(mv_intRar = 1, True, False)
            CboVerType.SelectedIndex = IIf(mv_intPatch = 1, 0, 1)
            cmdSave.Enabled = True
            chkCompressFile.Enabled = False
        Catch ex As Exception

        End Try
    End Sub
    Dim lstfiles() As String
    Private Sub cmdVerPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdVerPath.Click
        Dim sv_oDiag As New OpenFileDialog
        sv_oDiag.Title = "Chọn file XML chứa cấu hình"
        sv_oDiag.Multiselect = True
        sv_oDiag.Filter = "All files|*.*|DLL file|*.dll"
        If sv_oDiag.ShowDialog = DialogResult.OK Then
            txtFilePath.Text = sv_oDiag.FileName
            lstfiles = sv_oDiag.FileNames
            ToolTip1.SetToolTip(txtFilePath, sv_oDiag.FileName)
            Dim _FileVersionInfo As FileVersionInfo = FileVersionInfo.GetVersionInfo(txtFilePath.Text)
            txtVersion.Text = _FileVersionInfo.ProductVersion
            Dim _FileProperty As System.IO.FileInfo
            _FileProperty = New System.IO.FileInfo(txtFilePath.Text)
            txtCapacity.Text = _FileProperty.Length & " bytes"
            If _FileProperty.Extension.ToUpper.Equals(".RAR") Then
                chkCompressFile.Enabled = False
                chkCompressFile.Checked = False
            Else
            End If
            mv_bFileHasChanged = True
            cmdSave.Enabled = True
        End If
    End Sub
    Public Function bIsLastestVersion(ByVal pv_sFileName As String, ByVal pv_sVersion As String) As Boolean
        Dim sv_ds As New DataSet
        Dim sv_DA As SqlDataAdapter
        Dim sv_sSql As String
        sv_sSql = "SELECT * from Sys_VERSION WHERE  sFileName=N'" & pv_sFileName & "' AND sVersion=N'" & pv_sVersion & "'"
        Try
            sv_DA = New SqlDataAdapter(sv_sSql, VNS.Libs.globalVariables.SqlConn)
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
    Public Function iGetNewestRole() As Integer
        Dim sv_ds As New DataSet
        Dim sv_DA As SqlDataAdapter
        Dim sv_sSql As String
        sv_sSql = "SELECT max(PK_intID) from Sys_VERSION WHERE  FP_sBranchID=N'" & gv_sBranchID & "'"
        Try
            sv_DA = New SqlDataAdapter(sv_sSql, VNS.Libs.globalVariables.SqlConn)
            sv_DA.Fill(sv_ds, "Sys_VERSION")
            Return CInt(sv_ds.Tables(0).Rows(0)(0))
        Catch ex As Exception
            Return -1
        End Try
    End Function
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            Dim sFileName As String
            If mv_intStatus = 1 Then
                If txtFilePath.Text.Trim = String.Empty Or InStr(txtFilePath.Text.Trim, "\") = 0 Then Return
                sFileName = txtFilePath.Text.Substring(txtFilePath.Text.LastIndexOf("\") + 1)
                'If bIsLastestVersion(sFileName, txtVersion.Text.Trim) Then
                '    MessageBox.Show("Đã tồn tại phiên bản này trên Server. Đề nghị kiểm tra lại", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    Return
                'End If
            End If
            If mv_intStatus = 1 Then
                Dim totalfile As Integer
                totalfile = lstfiles.Length
                Dim idx = 0
                If Not bCheckData() Then
                    Return
                End If
                Me.Cursor = Cursors.WaitCursor
                For Each sfile As String In lstfiles
                    txtFilePath.Text = sfile
                    Dim _FileVersionInfo As FileVersionInfo = FileVersionInfo.GetVersionInfo(sfile)

                    txtVersion.Text = _FileVersionInfo.ProductVersion
                    Dim _FileProperty As System.IO.FileInfo
                    _FileProperty = New System.IO.FileInfo(sfile)
                    Dim tick As Int64 = _FileProperty.LastWriteTime.Ticks
                    txtCapacity.Text = _FileProperty.Length & " bytes"
                    If _FileProperty.Extension.ToUpper.Equals(".RAR") Then
                        chkCompressFile.Enabled = False
                        chkCompressFile.Checked = False
                    Else
                    End If
                    idx = idx + 1
                    lblStatus.Text = "Đang cập nhật: " + idx.ToString + "/" + totalfile.ToString
                    lblStatus.Refresh()
                    If Not File.Exists(sfile) Then Return
                    If chkCompressFile.Checked Then
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
                                Return
                            End If
                        End If

                    Else 'Nếu không Nén File

                    End If
                    If chkCompressFile.Checked Then
                        'Thực hiện nén File được chọn
                        If CompressFile(sfile) Then
                        End If
                    End If
                    Dim arrFilename() As String = Split(sfile, "\")
                    Dim sRarFile As String
                    sRarFile = txtFilePath.Text.Replace(txtFilePath.Text.Substring(txtFilePath.Text.LastIndexOf(".") + 1), "RAR")
                    arrFilename.Reverse(arrFilename)
                    Dim fs As FileStream
                    Dim intRar As Integer
                    If File.Exists(sRarFile) Then
                        fs = New FileStream(sRarFile, FileMode.Open)
                        intRar = 1
                    Else
                        fs = New FileStream(txtFilePath.Text, FileMode.Open)
                        intRar = 0
                    End If
                    Dim rd As BinaryReader = New BinaryReader(fs)
                    Dim arrData() As Byte = rd.ReadBytes(CInt(fs.Length))
                    fs.Flush()
                    fs.Close()
                    If SaveFile(arrFilename(0), txtFolder.Text.Trim, IIf(arrFilename(0).LastIndexOf(".") <> -1, arrFilename(0).Replace(arrFilename(0).Substring(arrFilename(0).LastIndexOf(".") + 1), "rar"), arrFilename(0) & ".rar"), arrData, txtVersion.Text.Trim, intRar, IIf(CboVerType.SelectedIndex = 0, 1, 0), CDec(txtCapacity.Text.Trim.ToUpper.Replace("BYTES", "")), txtDesc.Text.Trim, tick) Then

                        If File.Exists(sRarFile) Then
                            File.Delete(sRarFile)
                        End If

                        '-------------------------------------------
                        Dim dr As DataRow
                        dr = frm_ListUpdateVersion.mv_DSVersion.Tables(0).NewRow
                        With dr
                            .Item("PK_intID") = iGetNewestRole()
                            .Item("sFileName") = arrFilename(0).Substring(0, arrFilename(0).LastIndexOf("."))
                            .Item("sRarFileName") = arrFilename(0).Replace(arrFilename(0).Substring(arrFilename(0).LastIndexOf(".") + 1), "rar")
                            .Item("sVersion") = txtVersion.Text
                            .Item("intRar") = intRar
                            .Item("sFolder") = txtFolder.Text.Trim
                            .Item("intPatch") = IIf(CboVerType.SelectedIndex = 0, 1, 0)
                            .Item("tUpdatedDate") = dtmDateUpdate.Value
                            .Item("dblCapacity") = CDbl(txtCapacity.Text.Trim.ToUpper.Replace("BYTES", ""))
                            .Item("sDesc") = txtDesc.Text.Trim
                        End With
                        frm_ListUpdateVersion.mv_DSVersion.Tables(0).Rows.Add(dr)
                        frm_ListUpdateVersion.mv_DSVersion.Tables(0).AcceptChanges()
                        '-------------------------------------------

                        txtFilePath.Text = ""
                        txtCapacity.Text = ""
                        txtDesc.Text = ""
                        txtVersion.Text = ""
                        cmdSave.Enabled = False
                        cmdVerPath.Focus()
                    End If
                Next
                MessageBox.Show("Đã cập nhật các file cập nhật lên Server thành công!", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)

            Else 'CẬP NHẬT FILE VERSION
                If mv_bFileHasChanged Then
                    If Not bCheckData() Then
                        Return
                    End If
                    Me.Cursor = Cursors.WaitCursor
                    If Not File.Exists(txtFilePath.Text) Then Return
                    Dim _FileProperty As System.IO.FileInfo
                    _FileProperty = New System.IO.FileInfo(txtFilePath.Text)
                    Dim tick As Int64 = _FileProperty.LastWriteTime.Ticks
                    If chkCompressFile.Checked Then
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
                                Return
                            End If
                        End If

                    Else 'Nếu không Nén File

                    End If
                    If chkCompressFile.Checked Then
                        'Thực hiện nén File được chọn
                        If CompressFile(txtFilePath.Text) Then
                        End If
                    End If
                    Dim arrFilename() As String = Split(txtFilePath.Text, "\")
                    Dim sRarFile As String
                    sRarFile = txtFilePath.Text.Replace(txtFilePath.Text.Substring(txtFilePath.Text.LastIndexOf(".") + 1), "RAR")
                    arrFilename.Reverse(arrFilename)
                    Dim fs As FileStream
                    Dim intRar As Integer
                    If File.Exists(sRarFile) Then
                        fs = New FileStream(sRarFile, FileMode.Open)
                        intRar = 1
                    Else
                        fs = New FileStream(txtFilePath.Text, FileMode.Open)
                        intRar = 0
                    End If
                    Dim rd As BinaryReader = New BinaryReader(fs)
                    Dim arrData() As Byte = rd.ReadBytes(CInt(fs.Length))
                    fs.Flush()
                    fs.Close()
                    If UpdateVersion(mv_intID, arrFilename(0), txtFolder.Text.Trim, IIf(arrFilename(0).LastIndexOf(".") <> -1, arrFilename(0).Replace(arrFilename(0).Substring(arrFilename(0).LastIndexOf(".") + 1), "rar"), arrFilename(0) & ".rar"), arrData, txtVersion.Text.Trim, intRar, IIf(CboVerType.SelectedIndex = 0, 1, 0), CDec(txtCapacity.Text.Trim.ToUpper.Replace("BYTES", "")), txtDesc.Text.Trim, tick, mv_bFileHasChanged) Then
                        lblStatus.Text = ""
                        lblStatus.Refresh()
                        If File.Exists(sRarFile) Then
                            File.Delete(sRarFile)
                        End If
                        '-------------------------------------------
                        For Each dr As DataRow In frm_ListUpdateVersion.mv_DSVersion.Tables(0).Rows
                            If dr("PK_intID") = mv_intID Then
                                With dr
                                    .Item("sFileName") = arrFilename(0).Substring(0, arrFilename(0).LastIndexOf("."))
                                    .Item("sRarFileName") = arrFilename(0).Replace(arrFilename(0).Substring(arrFilename(0).LastIndexOf(".") + 1), "rar")
                                    .Item("sVersion") = txtVersion.Text
                                    .Item("intRar") = mv_intRar
                                    .Item("sFolder") = txtFolder.Text.Trim
                                    .Item("intPatch") = IIf(CboVerType.SelectedIndex = 0, 1, 0)
                                    .Item("tUpdatedDate") = dtmDateUpdate.Value
                                    .Item("dblCapacity") = CDbl(txtCapacity.Text.Trim.ToUpper.Replace("BYTES", ""))
                                    .Item("sDesc") = txtDesc.Text.Trim
                                End With

                                Exit For
                            End If
                        Next
                        frm_ListUpdateVersion.mv_DSVersion.Tables(0).AcceptChanges()
                        MessageBox.Show("Đã cập nhật lại phiên bản của file " & mv_sFileName & " thành công!", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '-------------------------------------------
                        Me.Close()
                    End If
                Else 'Không thay đổi dữ liệu phiên bản
                    Dim arrData() As Byte
                    If UpdateVersion(mv_intID, "OK", txtFolder.Text.Trim, "OK", arrData, "OK", 1, IIf(CboVerType.SelectedIndex = 0, 1, 0), 1000, txtDesc.Text.Trim, mv_bFileHasChanged) Then
                        lblStatus.Text = ""
                        lblStatus.Refresh()
                        '-------------------------------------------
                        For Each dr As DataRow In frm_ListUpdateVersion.mv_DSVersion.Tables(0).Rows
                            If dr("PK_intID") = mv_intID Then
                                With dr
                                    .Item("intPatch") = IIf(CboVerType.SelectedIndex = 0, 1, 0)
                                    .Item("tUpdatedDate") = dtmDateUpdate.Value
                                    .Item("sDesc") = txtDesc.Text.Trim
                                End With
                                Exit For
                            End If
                        Next
                        frm_ListUpdateVersion.mv_DSVersion.Tables(0).AcceptChanges()
                        MessageBox.Show("Đã cập nhật lại phiên bản của file " & mv_sFileName & " thành công!", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '-------------------------------------------
                        Me.Close()
                    End If
                End If
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Private Function bCheckData() As Boolean
        Try
            If txtFilePath.Text.Trim.Equals(String.Empty) Then
                MessageBox.Show("Bạn chưa chọn File để cập nhật!", gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmdVerPath.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception

        End Try
    End Function
    Private Function CompressFile(ByVal pv_sFilePath As String) As Boolean
        Try
            Dim pStartupPath As String = Application.StartupPath
            Dim info As New ProcessStartInfo
            Dim pv_sRarFile As String
            pv_sRarFile = pv_sFilePath.Replace(pv_sFilePath.Substring(pv_sFilePath.LastIndexOf(".") + 1), "RAR")
            info.FileName = pStartupPath & "\WinRAR\WinRAR.exe"
            info.Arguments = "a -pSYSMAN -ep -o+ " & Chr(34) & pv_sRarFile & Chr(34) & " " & Chr(34) & pv_sFilePath & Chr(34)
            info.WindowStyle = ProcessWindowStyle.Hidden
            lblStatus.Text = "Đang thực hiện Nén File..."
            lblStatus.Refresh()
            Me.Refresh()
            'Shell(pStartupPath & "\WinRAR\WinRAR.exe a -pSYSMAN -ep -o+ " & pv_sRarFile & " " & pv_sFilePath, AppWinStyle.Hide, True)
            Dim pro As Process = System.Diagnostics.Process.Start(info)
            pro.WaitForExit()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function SaveFile(ByVal pv_sFileName As String, ByVal pv_sFolderName As String, ByVal pv_sRarFileName As String, ByVal pv_arrData() As Byte, ByVal pv_sVersion As String, ByVal pv_intRar As Int16, _
                        ByVal pv_intPatch As Int16, ByVal pv_decCapacity As Decimal, ByVal pv_sDesc As String,ByVal tick As Int64) As Boolean

        Try

            lblStatus.Text = "Đang thực hiện lưu File vào CSDL..."
            lblStatus.Refresh()
            Me.Refresh()
            Dim strSQL As String = "INSERT INTO Sys_VERSION(sFileName,sFolder,sRarFileName,objData,sVersion,intRar,intPatch,dblCapacity,sDesc,tUpdatedDate,tick) VALUES(@_sFileName,@_sFolder,@_sRarFileName,@_objData,@_sVersion,@_intRar,@_intPatch,@_dblCapacity,@_sDesc,@tUpdatedDate,@tick)"
            Dim cmd As New SqlCommand(strSQL, VNS.Libs.globalVariables.SqlConn)
            With cmd
                .Parameters.Add(New SqlParameter("@_sFileName", SqlDbType.NVarChar)).Value = pv_sFileName
                .Parameters.Add(New SqlParameter("@_sFolder", SqlDbType.NVarChar)).Value = pv_sFolderName
                .Parameters.Add(New SqlParameter("@_sRarFileName", SqlDbType.NVarChar)).Value = pv_sRarFileName
                .Parameters.Add(New SqlParameter("@_objData", SqlDbType.Image)).Value = pv_arrData
                .Parameters.Add(New SqlParameter("@_sVersion", SqlDbType.NVarChar)).Value = pv_sVersion
                .Parameters.Add(New SqlParameter("@_intRar", SqlDbType.SmallInt)).Value = pv_intRar
                .Parameters.Add(New SqlParameter("@_intPatch", SqlDbType.SmallInt)).Value = pv_intPatch
                .Parameters.Add(New SqlParameter("@_dblCapacity", SqlDbType.Float)).Value = pv_decCapacity
                .Parameters.Add(New SqlParameter("@tick", SqlDbType.BigInt)).Value = tick
                .Parameters.Add(New SqlParameter("@_sDesc", SqlDbType.NVarChar)).Value = pv_sDesc
                .Parameters.Add(New SqlParameter("@tUpdatedDate", SqlDbType.DateTime)).Value = "#" & dtmDateUpdate.Text & "#"
                .ExecuteNonQuery()
            End With
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try

    End Function
    Private Function UpdateVersion(ByVal pv_intID As Integer, ByVal pv_sFileName As String, ByVal pv_sFolderName As String, ByVal pv_sRarFileName As String, ByVal pv_arrData() As Byte, ByVal pv_sVersion As String, ByVal pv_intRar As Int16, _
                        ByVal pv_intPatch As Int16, ByVal pv_decCapacity As Decimal, ByVal pv_sDesc As String, ByVal tick As Long, Optional ByVal pv_bFilehasChanged As Boolean = False) As Boolean

        Try

            lblStatus.Text = "Đang thực hiện lưu File vào CSDL..."
            lblStatus.Refresh()
            Me.Refresh()
            If pv_bFilehasChanged Then 'Cập nhật có thay đổi dữ liệu File
                Dim strSQL As String = "UPDATE Sys_VERSION SET sFileName=@_sFileName,sFolder=@_sFolder,sRarFileName=@_sRarFileName,objData=@_objData,sVersion=@_sVersion,intRar=@_intRar,intPatch=@_intpatch,dblCapacity=@_dblCapacity,sDesc=@_sDesc,tUpdatedDate=@_tUpdatedDate,tick=@tick WHERE PK_intID=" & pv_intID
                Dim cmd As New SqlCommand(strSQL, VNS.Libs.globalVariables.SqlConn)
                With cmd
                    .Parameters.Add(New SqlParameter("@_sFileName", SqlDbType.NVarChar)).Value = pv_sFileName
                    .Parameters.Add(New SqlParameter("@_sFolder", SqlDbType.NVarChar)).Value = pv_sFolderName
                    .Parameters.Add(New SqlParameter("@_sRarFileName", SqlDbType.NVarChar)).Value = pv_sRarFileName
                    .Parameters.Add(New SqlParameter("@_objData", SqlDbType.Image)).Value = pv_arrData
                    .Parameters.Add(New SqlParameter("@_sVersion", SqlDbType.NVarChar)).Value = pv_sVersion
                    .Parameters.Add(New SqlParameter("@_intRar", SqlDbType.SmallInt)).Value = pv_intRar
                    .Parameters.Add(New SqlParameter("@_intPatch", SqlDbType.SmallInt)).Value = pv_intPatch
                    .Parameters.Add(New SqlParameter("@_dblCapacity", SqlDbType.Float)).Value = pv_decCapacity
                    .Parameters.Add(New SqlParameter("@_sDesc", SqlDbType.NVarChar)).Value = pv_sDesc
                    .Parameters.Add(New SqlParameter("@tick", SqlDbType.BigInt)).Value = tick
                    .Parameters.Add(New SqlParameter("@_tUpdatedDate", SqlDbType.SmallDateTime)).Value = "#" & dtmDateUpdate.Text & "#"
                    .ExecuteNonQuery()
                End With

            Else 'Cập nhật các thông tin khác ngoài dữ liệu File
                Dim strSQL As String = "UPDATE Sys_VERSION SET intPatch=@_intpatch,sFolder=@_sFolder,sDesc=@_sDesc,tUpdatedDate=@tUpdatedDate,tick=@tick WHERE PK_intID=" & pv_intID
                Dim cmd As New SqlCommand(strSQL, VNS.Libs.globalVariables.SqlConn)
                With cmd
                    .Parameters.Add(New SqlParameter("@_intPatch", SqlDbType.SmallInt)).Value = pv_intPatch
                    .Parameters.Add(New SqlParameter("@_sFolder", SqlDbType.NVarChar)).Value = pv_sFolderName
                    .Parameters.Add(New SqlParameter("@_sDesc", SqlDbType.NVarChar)).Value = pv_sDesc
                    .Parameters.Add(New SqlParameter("@tick", SqlDbType.BigInt)).Value = tick
                    .Parameters.Add(New SqlParameter("@tUpdatedDate", SqlDbType.DateTime)).Value = "#" & dtmDateUpdate.Text & "#"
                    .ExecuteNonQuery()
                End With
            End If
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try

    End Function
End Class
