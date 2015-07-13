Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Test As New APITKN

        MsgBox(Test.NewAccessToken("cccubed", "AmagamiSS3"))

    End Sub
End Class
