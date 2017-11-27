'Imports VietBaIT.MultiLanguage
Public NotInheritable Class AboutBox1

    Private Sub AboutBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub AboutBox1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'SetLanguage(gv_sLanguageDisplay, Me, "GOLFMAN", VNS.Libs.globalVariables.SqlConn)
        textBoxDescription.Text = "Văn phòng Hà Nội" & vbCrLf & "Số nhà 3/197, Phố Nam Dư, Phường Lĩnh Nam, Quận Hoàng Mai, Hà Nội" & vbCrLf & "ĐT: 84 4 6434705 / 21 Fax: 84 4 6442376 " & vbCrLf & "E-mail: yourfriend20030@gmail.com " & vbCrLf & "Văn phòng TP. Hồ Chí Minh " & vbCrLf & "Số nhà 29/127 Ngõ Thông Phong" & vbCrLf & "ĐT/Fax: 0904 648006 " & vbCrLf & "E-mail: yourfriend20030@yahoo.com"
        textBoxDescription.Text &= vbCrLf & "Author:" & vbCrLf & "   Đào Văn Cường(090 4648006)."
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Dim p As New System.Diagnostics.Process
            p.StartInfo = New System.Diagnostics.ProcessStartInfo("MSInfo32.exe")
            p.Start()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        Me.Close()
    End Sub
End Class
