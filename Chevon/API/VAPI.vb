Imports SIMPLE3DES

Public Class VAPI

    Public Legacy As Boolean
    Private API_Token As APITKN



End Class

Public Class APITKN

    Private AccessToken As String
    Private TKNType As String
    Private Expiration As Integer
    Private User As String
    Private Issued As Date
    Private Expires As Date

    Sub New()

        If My.Settings.EncryptKey = "None" Then

            Dim Temp As New System.Text.StringBuilder
            Dim Random As New Random()
            For Count As Integer = 0 To 30

                Temp.Append(Chr(Random.Next(33, 126)))

            Next

            My.Settings.EncryptKey = Temp.ToString

        End If

        If My.Settings.AccessToken <> "None" Then

            Dim Crypt As New SIMPLE3DES.SIMPLE3DES(My.Settings.EncryptKey)

            AccessToken = Crypt.DecryptData(My.Settings.AccessToken)
            Expiration = CInt(Crypt.DecryptData(My.Settings.Expiration))
            User = Crypt.DecryptData(My.Settings.User)
            Issued = Crypt.DecryptData(My.Settings.Issued)
            Expires = Crypt.DecryptData(My.Settings.Expires)

            If Expires < Today Then

                'Access Token old, Replace

            End If

        End If

    End Sub

End Class
