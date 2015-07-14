''' <summary>
''' Chevon is the class containing the code and functions that allow interactions with the Voat.co API
''' </summary>
Public Class Chevon

    ''' <summary>
    ''' Submit a request for info on a specific subverse
    ''' </summary>
    ''' <param name="Subverse">Which Subverse do you want info on?</param>
    ''' <param name="Key">Public API Key</param>
    ''' <returns>SubVInfo structure containing data on the subverse you asked about.</returns>
    ''' <remarks>If an error is encounted, SubVInfo will contain 'Error' as the name and the received message will go to description.</remarks>
    Function API_SUBVERSE_INFO(ByVal Subverse As String, ByVal Key As String) As SubVInfo

        Dim Request As Net.HttpWebRequest = Net.HttpWebRequest.Create("https://fakevout.azurewebsites.net/api/v1/v/" & Subverse & "/info")
        Request.Method = "GET"
        Request.ContentType = "application/json"
        Request.Headers.Add("Voat-ApiKey:" & Key)

        Dim Response As Net.WebResponse = Request.GetResponse
        Dim Reader As New IO.StreamReader(Response.GetResponseStream)
        Dim JR As New Newtonsoft.Json.JsonTextReader(Reader)
        Dim Storage As New SubVInfo

        While JR.Read

            If JR.Value IsNot Nothing And JR.TokenType = 4 Then

                If JR.Value = "name" Then

                    JR.Read()
                    Storage.Name = JR.Value

                ElseIf JR.Value = "success" Then

                    JR.Read()
                    If JR.Value = "false" Then

                        Storage.Name = "Error"

                    End If

                ElseIf JR.Value = "title" Then

                    JR.Read()
                    Storage.Title = JR.Value

                ElseIf JR.Value = "description" Then

                    JR.Read()
                    Storage.Description = JR.Value

                ElseIf JR.Value = "creationDate" Then

                    JR.Read()
                    Storage.Created = JR.Value

                ElseIf JR.Value = "subscriberCount" Then

                    JR.Read()
                    Storage.Subs = JR.Value

                ElseIf JR.Value = "ratedAdult" Then

                    JR.Read()
                    Storage.Adult = JR.Value

                ElseIf JR.Value = "sidebar" Then

                    JR.Read()
                    Storage.Sidebar = JR.Value

                ElseIf JR.Value = "type" Then

                    JR.Read()
                    Storage.Type = JR.Value

                ElseIf JR.Value = "message" Then

                    JR.Read()
                    Storage.Description = JR.Value

                End If

            End If

        End While

        Return Storage

    End Function


    ''' <summary>
    ''' Function call the API To get submissions from a subverse
    ''' </summary>
    ''' <param name="Subverse">Subverse to get posts from</param>
    ''' <param name="Key">Public API Key</param>
    ''' <returns>A list of posts from that subverse</returns>
    ''' <remarks>If an error is encounted, The first post will have an id of -1 and content will contain the error message.</remarks>
    Function API_SUBMISSIONS_GET(ByVal Subverse As String, ByVal Key As String) As List(Of Post)

        Dim Posts As New List(Of Post)
        Dim Request As Net.HttpWebRequest = Net.HttpWebRequest.Create("https://fakevout.azurewebsites.net/api/v1/v/" & Subverse)
        Request.Method = "GET"
        Request.ContentType = "application/json"
        Request.Headers.Add("Voat-ApiKey:" & Key)

        Dim Response As Net.WebResponse = Request.GetResponse
        Dim Reader As New IO.StreamReader(Response.GetResponseStream)
        Dim JR As New Newtonsoft.Json.JsonTextReader(Reader)

        Dim temp As New Post
        Dim temp2 As New PostType

        While JR.Read

            If JR.Value IsNot Nothing And JR.TokenType = 4 Then

                If JR.Value = "success" Then

                    JR.Read()
                    If JR.Value = "false" Then

                        temp.ID = -1

                    End If

                ElseIf JR.Value = "id" Then

                    JR.Read()
                    temp.ID = JR.Value
                    JR.Read()
                    JR.Read()
                    temp.Comments = JR.Value
                    JR.Read()
                    JR.Read()
                    temp.Created = JR.Value
                    JR.Read()
                    JR.Read()
                    temp.Ups = JR.Value
                    JR.Read()
                    JR.Read()
                    temp.Downs = JR.Value
                    JR.Read()
                    JR.Read()
                    temp.LastEdit = JR.Value
                    JR.Read()
                    JR.Read()
                    temp.Views = JR.Value
                    JR.Read()
                    JR.Read()
                    temp.User = JR.Value
                    JR.Read()
                    JR.Read()
                    temp.Subverse = JR.Value
                    JR.Read()
                    JR.Read()
                    temp.Thumbnail = JR.Value
                    JR.Read()
                    JR.Read()
                    temp.Title = JR.Value
                    JR.Read()
                    JR.Read()
                    If JR.Value = 1 Then

                        temp2.Type = 1
                        temp2.Name = "Self Post"
                        temp.Type = temp2

                    Else

                        temp2.Type = 2
                        temp2.Name = "Link Post"
                        temp.Type = temp2

                    End If
                    JR.Read()
                    JR.Read()
                    temp.URL = JR.Value
                    JR.Read()
                    JR.Read()
                    temp.Content = JR.Value
                    JR.Read()
                    JR.Read()
                    temp.Formatted = JR.Value
                    Posts.Add(temp)

                ElseIf JR.Value = "message" Then

                    JR.Read()
                    temp.Content = JR.Value
                    Posts.Add(temp)

                End If

            End If

        End While

        Return Posts

    End Function

    ''' <summary>
    ''' This is the function responsible for taking a user's username and password and requesting an access token based on it from the API.
    ''' </summary>
    ''' <param name="UserName">Your voat.co Username</param>
    ''' <param name="Password">Your voat.co Password</param>
    ''' <param name="Key">Public API Key</param>
    ''' <returns>Returns APITKN structure containing the Access Token, Issued and Expiration Date. If it fails, access token is set to 0.</returns>
    Function NewAccessToken(ByVal UserName As String, ByVal Password As String, ByVal Key As String) As APITKN

        Dim Request As Net.HttpWebRequest = Net.HttpWebRequest.Create("https://fakevout.azurewebsites.net/api/token")
        Request.Method = "POST"
        Request.ContentType = "application/x-www-form-urlencoded"
        Request.Headers.Add("Voat-ApiKey:" & Key)

        'Header built, write data
        Dim Writer As IO.Stream = Request.GetRequestStream
        Dim Content As Byte() = System.Text.Encoding.UTF8.GetBytes("grant_type=password&username=" & UserName & "&password=" & Password)
        Writer.Write(Content, 0, Content.Length)
        Writer.Close()

        'Header Built, Data put on the Body, Ask for Key
        Dim Response As Net.WebResponse
        Dim Data As New APITKN

        Try
            Response = Request.GetResponse()
        Catch
            Data.Access_Token = "0"
            Return Data
        End Try

        Dim Reader As New IO.StreamReader(Response.GetResponseStream)

        Dim JR As New Newtonsoft.Json.JsonTextReader(Reader)
        While JR.Read()

            If JR.Value IsNot Nothing And JR.TokenType = 4 Then

                If JR.Value = "access_token" Then

                    JR.Read()
                    Data.Access_Token = JR.Value

                ElseIf JR.Value = ".expires" Or JR.Value = "expires" Then

                    JR.Read()
                    Data.Expires = JR.Value

                ElseIf JR.Value = ".issued" Or JR.Value = "issued" Then

                    JR.Read()
                    Data.Issued = JR.Value

                End If

            End If

        End While

        Return Data

    End Function

End Class