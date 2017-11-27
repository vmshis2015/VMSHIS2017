Public Class SelectUser
    Public CurrUserList As String = ""
    Public bCancel As Boolean = True
    Public NewCurrUL As String = ""
    Private Sub SelectUser_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then ProcessTabKey(True)
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
    Private Sub SelectUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadData()
    End Sub
    Private Sub loadData()
        Dim sv_oclsUser As New clsUser
        Dim ds As New DataSet
        ds = sv_oclsUser.dsGetAllUser
        If IsNothing(ds) OrElse ds.Tables.Count < 0 OrElse ds.Tables(0).Rows.Count < 0 Then Return
        lstUser.Items.Clear()
        For Each dr As DataRow In ds.Tables(0).Rows
            If Not InStr("," & CurrUserList.ToUpper & ",", "," & dr("PK_sUID").ToString.ToUpper & ",", CompareMethod.Text) > 0 Then
                lstUser.Items.Add(dr("PK_sUID").ToString & "::" & dr("sFullName").ToString)
            End If
        Next
        cmdOK.Enabled = lstUser.Items.Count > 0
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Dim bFound As Boolean = False
        NewCurrUL = ""
        For i As Integer = 0 To lstUser.Items.Count - 1
            If lstUser.GetItemChecked(i) Then
                bFound = True
                NewCurrUL &= lstUser.Items(i).ToString.Split("::")(0) & ","
            End If
        Next
        bCancel = Not bFound
        If bFound Then
            Me.Close()
        Else
            MessageBox.Show("Bạn chưa chọn User nào. Mời bạn chọn lại hoặc nhấn Cancel để không chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal txt As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.Text = txt
        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class