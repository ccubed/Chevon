Imports SIMPLE3DES

Public Class VAPI

    Private API_Token As APITKN


    ''' <summary>
    ''' Submit a request for info on a specific subverse
    ''' </summary>
    ''' <param name="Subverse">Which Subverse do you want info on?</param>
    Sub API_SUBVERSE_INFO(ByVal Subverse As String)

        Dim Request As Net.HttpWebRequest = Net.HttpWebRequest.Create("https://fakevout.azurewebsites.net/api/v1/v/" & Subverse & "/info")
        Request.Method = "GET"
        Request.ContentType = "application/json"
        Request.Headers.Add("Voat-ApiKey:" & My.Settings.PublicKey)
        Request.Headers.Add("Authorization: Bearer " & My.Settings.AccessToken)

        Dim Response As Net.WebResponse = Request.GetResponse
        Dim Reader As New IO.StreamReader(Response.GetResponseStream)
        Dim JR As New Newtonsoft.Json.JsonTextReader(Reader)
        Dim Storage As New SubVInfo




        '  "data": {
        ' "name" "DerpyGuyIsRadCakes", - name
        ' "title": "Derps is Rad-Cakes", - link 
        ' "description": "Subverse Description", - desc
        ' "creationDate": "2013-07-20T09:49:41.9910147Z ", - created
        ' "subscriberCount": -1, - subs
        ' "ratedAdult": true, - rated adult
        ' "sidebar": "Sidebar info", - sidebar
        ' "type": "Link" - type
        '}



    End Sub

End Class

Public Class APITKN

    ''' <summary>
    ''' APITKN class constructor. All it does is build an encrypt key if one isn't present. All encrypt keys are per program.
    ''' </summary>
    Sub New()

        If My.Settings.EncryptKey = "None" Then

            Dim Temp As New System.Text.StringBuilder
            Dim Random As New Random()
            For Count As Integer = 0 To 30

                Temp.Append(Chr(Random.Next(33, 126)))

            Next

            My.Settings.EncryptKey = Temp.ToString

        End If

    End Sub

    ''' <summary>
    ''' Name: Valid_Access_Token
    ''' Purpose: Returns an integer value to indicate whether or not an access token is currently stored and if it is still valid.
    ''' Returns: 0 - None Stored, 1 - All Clear, 2 - Stored but old, 3 - Stored but Corrupt 
    ''' </summary>
    ''' <returns>Integer</returns>
    Function Valid_Access_Token() As Integer

        If My.Settings.AccessToken = "None" Then

            Return 0

        Else

            Dim CryptE As New SIMPLE3DES.SIMPLE3DES(My.Settings.EncryptKey)
            Dim Expires As Date = CryptE.DecryptData(My.Settings.Expires)
            If IsDate(Expires) Then

                If Expires < Today Then

                    Return 2

                Else

                    Return 1

                End If

            Else

                Return 3

            End If

        End If

    End Function

    ''' <summary>
    ''' This is the function responsible for taking a user's username and password and requesting an access token based on it from the API.
    ''' </summary>
    ''' <param name="UserName">Your voat.co Username</param>
    ''' <param name="Password">Your voat.co Password</param>
    ''' <returns>Error Message if any or Success</returns>
    Function NewAccessToken(ByVal UserName As String, ByVal Password As String) As String

        Dim Request As Net.HttpWebRequest = Net.HttpWebRequest.Create("https://fakevout.azurewebsites.net/api/token")
        Request.Method = "POST"
        Request.ContentType = "application/x-www-form-urlencoded"
        Request.Headers.Add("Voat-ApiKey:" & My.Settings.PublicKey)

        'Header built, write data
        Dim Writer As IO.Stream = Request.GetRequestStream
        Dim Content As Byte() = System.Text.Encoding.UTF8.GetBytes("grant_type=password&username=" & UserName & "&password=" & Password)
        Writer.Write(Content, 0, Content.Length)
        Writer.Close()

        'Header Built, Data put on the Body, Ask for Key
        Dim Response As Net.WebResponse

        Try
            Response = Request.GetResponse()
        Catch
            Return "Invalid Credentials. Bad Username or Password."
        End Try

        Dim Reader As New IO.StreamReader(Response.GetResponseStream)

        Dim S3 As New SIMPLE3DES.SIMPLE3DES(My.Settings.EncryptKey)

        Dim JR As New Newtonsoft.Json.JsonTextReader(Reader)
        While JR.Read()

            If JR.Value IsNot Nothing And JR.TokenType = 4 Then

                If JR.Value = "access_token" Then

                    JR.Read()
                    My.Settings.AccessToken = S3.EncryptData(JR.Value)

                ElseIf JR.Value = ".expires" Or JR.Value = "expires" Then

                    JR.Read()
                    My.Settings.Expires = S3.EncryptData(JR.Value)

                ElseIf JR.Value = ".issued" Or JR.Value = "issued" Then

                    JR.Read()
                    My.Settings.Issued = S3.EncryptData(JR.Value)

                End If

            End If

        End While

        My.Settings.User = S3.EncryptData(UserName)

        Return "Success"

    End Function

End Class
