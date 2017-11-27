Imports System.Threading
Public Class frmSplash
    Inherits System.Windows.Forms.Form
    Public thre As Thread
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
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblIntro As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents lblLoad As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmSplash))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.lblIntro = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lblLoad = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(0, 32)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(536, 272)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'lblIntro
        '
        Me.lblIntro.BackColor = System.Drawing.Color.CornflowerBlue
        Me.lblIntro.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIntro.ForeColor = System.Drawing.Color.Yellow
        Me.lblIntro.Location = New System.Drawing.Point(8, 372)
        Me.lblIntro.Name = "lblIntro"
        Me.lblIntro.Size = New System.Drawing.Size(512, 16)
        Me.lblIntro.TabIndex = 1
        Me.lblIntro.Text = "For Win2000 or higher - Copyright (c) by DVC Company"
        Me.lblIntro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLoad
        '
        Me.lblLoad.BackColor = System.Drawing.Color.CornflowerBlue
        Me.lblLoad.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoad.ForeColor = System.Drawing.Color.Yellow
        Me.lblLoad.Location = New System.Drawing.Point(8, 6)
        Me.lblLoad.Name = "lblLoad"
        Me.lblLoad.Size = New System.Drawing.Size(288, 16)
        Me.lblLoad.TabIndex = 2
        Me.lblLoad.Text = "Loading"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 24)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(536, 8)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(536, 8)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Location = New System.Drawing.Point(-32, 304)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(568, 5)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(456, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 16)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Version 2.0"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.CornflowerBlue
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(0, 312)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(528, 60)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Warning: This computer program is protected by copyright law and international tr" & _
        "eaties. Unauthorized duplication or distribution of this program, or any portion" & _
        " of it, may result in severe civil or criminal penalties and will be prosecuted " & _
        "to the maximum extent possible under the law."
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmSplash
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.CornflowerBlue
        Me.ClientSize = New System.Drawing.Size(528, 392)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblLoad)
        Me.Controls.Add(Me.lblIntro)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmSplash"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "dlgSplash"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Try
            thre.Suspend()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmSplash_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        thre = New Thread(AddressOf Flash)
        thre.Start()
    End Sub
    Private Sub Flash()
        For i As Integer = 0 To 50
            lblLoad.Text &= "."
            thre.Sleep(100)
        Next
        Close()
        'thre.Abort()
    End Sub
    Private Sub frmSplash_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.Close()
    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click
        Try
            thre.Suspend()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lblIntro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblIntro.Click
        Try
            thre.Resume()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lblLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblLoad.Click
        Try
            thre.Resume()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
    End Sub
End Class
