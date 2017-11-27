Public Class frm_ListOfRoleFunction
    Inherits System.Windows.Forms.Form
    Public DV As New DataView

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
    Friend WithEvents grdList As System.Windows.Forms.DataGrid
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Sys_ROLES As System.Windows.Forms.DataGridTableStyle
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frm_ListOfRoleFunction))
        Me.grdList = New System.Windows.Forms.DataGrid
        Me.Sys_ROLES = New System.Windows.Forms.DataGridTableStyle
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmdClose = New System.Windows.Forms.Button
        CType(Me.grdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdList
        '
        Me.grdList.BackgroundColor = System.Drawing.Color.White
        Me.grdList.CaptionVisible = False
        Me.grdList.DataMember = ""
        Me.grdList.Dock = System.Windows.Forms.DockStyle.Top
        Me.grdList.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdList.Location = New System.Drawing.Point(0, 0)
        Me.grdList.Name = "grdList"
        Me.grdList.RowHeaderWidth = 0
        Me.grdList.Size = New System.Drawing.Size(534, 318)
        Me.grdList.TabIndex = 0
        Me.grdList.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.Sys_ROLES})
        '
        'Sys_ROLES
        '
        Me.Sys_ROLES.DataGrid = Me.grdList
        Me.Sys_ROLES.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn4})
        Me.Sys_ROLES.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.Sys_ROLES.MappingName = ""
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.MappingName = ""
        Me.DataGridTextBoxColumn1.NullText = ""
        Me.DataGridTextBoxColumn1.Width = 0
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "Tên Role-Tiếng Việt"
        Me.DataGridTextBoxColumn2.MappingName = "sRoleName"
        Me.DataGridTextBoxColumn2.NullText = ""
        Me.DataGridTextBoxColumn2.Width = 150
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "Tên Role-Tiếng Anh"
        Me.DataGridTextBoxColumn3.MappingName = "sEngRoleName"
        Me.DataGridTextBoxColumn3.NullText = ""
        Me.DataGridTextBoxColumn3.Width = 150
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "Tên chức năng"
        Me.DataGridTextBoxColumn4.MappingName = "sFunctionName"
        Me.DataGridTextBoxColumn4.NullText = ""
        Me.DataGridTextBoxColumn4.Width = 150
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(3, 324)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(525, 3)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'cmdClose
        '
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.Location = New System.Drawing.Point(219, 333)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(99, 30)
        Me.cmdClose.TabIndex = 2
        Me.cmdClose.Text = "Thoát"
        '
        'frm_ListOfRoleFunction
        '
        Me.AcceptButton = Me.cmdClose
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(534, 368)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grdList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_ListOfRoleFunction"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Danh sách các Role đang gắn với chức năng này"
        CType(Me.grdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

   
    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub frm_ListOfRoleFunction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            With grdList
                .TableStyles(0).MappingName = DV.Table.TableName
                .DataSource = DV
            End With
            DV.AllowDelete = False
            DV.AllowNew = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdList_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdList.CurrentCellChanged
        Try
            grdList.CurrentCell = New DataGridCell(grdList.CurrentRowIndex, 0)
            grdList.Select(grdList.CurrentRowIndex)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub frm_ListOfRoleFunction_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
End Class
