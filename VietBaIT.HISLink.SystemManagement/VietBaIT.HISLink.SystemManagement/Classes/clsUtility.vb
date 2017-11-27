Public Class clsUtility
    Dim mv_sSql As String

    Public Sub New()
        Try
            If Not gv_ConnectSuccess Then
                gv_ConnectSuccess = InitializeConnection()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function CheckTable(ByVal pv_sTableName As String, Optional ByVal pv_sCondition As String = "") As Boolean
        Dim sv_ssql As String
        If pv_sCondition = "" Then
            sv_ssql = "SELECT * FROM " & pv_sTableName
        Else
            sv_ssql = "SELECT * FROM " & pv_sTableName & " WHERE " & pv_sCondition
        End If
        Try
            Dim sv_ocmd As New SqlClient.SqlCommand
            Dim DA As New SqlClient.SqlDataAdapter(sv_ssql, VNS.Libs.globalVariables.SqlConn)
            Dim DS As New DataSet
            DA.Fill(DS, "Table0")
            If DS.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show("Lỗi: " & ex.Message, gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Function
End Class
