Public Class SelectGroup
    Public CurrGroupList As String = ""
    Public bCancel As Boolean = True
    Public NewCurrGL As String = ""
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
        ds = sv_oclsUser.dsGetAllGroupUser
        If IsNothing(ds) OrElse ds.Tables.Count < 0 OrElse ds.Tables(0).Rows.Count < 0 Then Return
        lstGroup.Items.Clear()
        For Each dr As DataRow In ds.Tables(0).Rows
            If Not InStr("," & CurrGroupList.ToUpper & ",", "," & dr("GroupID").ToString & "#" & dr("GroupName").ToString & ",", CompareMethod.Text) > 0 Then
                lstGroup.Items.Add(dr("GroupID").ToString & "#" & dr("GroupName").ToString)
            End If
        Next
        cmdOK.Enabled = lstGroup.Items.Count > 0
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Dim bFound As Boolean = False
        NewCurrGL = ""
        For i As Integer = 0 To lstGroup.Items.Count - 1
            If lstGroup.GetItemChecked(i) Then
                bFound = True
                NewCurrGL &= lstGroup.Items(i).ToString & ","
            End If
        Next
        bCancel = Not bFound
        If bFound Then
            Me.Close()
        Else
            MessageBox.Show("Bạn chưa chọn Group nào. Mời bạn chọn lại hoặc nhấn Cancel để không chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub
End Class