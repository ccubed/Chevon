
''' <summary>
''' Helper Structure. Stores info sent by server on a API_SUBVERSE_INFO request.
''' </summary>
Public Structure SubVInfo

    ''' <summary>
    ''' Name of the Subverse
    ''' </summary>
    Public Name As String
    ''' <summary>
    ''' The Title of the subverse that would be put in the URL
    ''' </summary>
    Public Title As String
    ''' <summary>
    ''' Description of the Subverse
    ''' </summary>
    Public Description As String
    ''' <summary>
    ''' Date of Creation
    ''' </summary>
    Public Created As Date
    ''' <summary>
    ''' Number of Subscribers
    ''' </summary>
    Public Subs As Integer
    ''' <summary>
    ''' Subverse marked Adult?
    ''' </summary>
    Public Adult As Boolean
    ''' <summary>
    ''' Formatted contents of the sidebar
    ''' </summary>
    Public Sidebar As String
    ''' <summary>
    ''' Type? Not sure what this is. API documentation doesn't say and no one has answered me. All return link for now.
    ''' </summary>
    Public Type As String

End Structure


''' <summary>
''' Helper Structure. Stores access token response information.
''' </summary>
Public Structure APITKN

    ''' <summary>
    ''' The returned access token
    ''' </summary>
    Public Access_Token As String
    ''' <summary>
    ''' When it was issued. You technically don't need this, but it doesn't hurt to have.
    ''' </summary>
    Public Issued As Date
    ''' <summary>
    ''' Date the access token expires.
    ''' </summary>
    Public Expires As Date

End Structure


''' <summary>
''' Helper Structure. Stores the make up of a submission as given by the API. Type is stored as a PostType structure.
''' </summary>
Public Structure Post


    ''' <summary>
    ''' The Submissions ID. You need this later to call other API functions on it. This is the ID you give those functions.
    ''' </summary>
    Public ID As Integer
    ''' <summary>
    ''' How many comments are on this submission?
    ''' </summary>
    Public Comments As String
    ''' <summary>
    ''' When was this submission posted?
    ''' </summary>
    Public Created As Date
    ''' <summary>
    ''' How many upvotes has it received?
    ''' </summary>
    Public Ups As Integer
    ''' <summary>
    ''' Conversely, how many downvotes?
    ''' </summary>
    Public Downs As Integer
    ''' <summary>
    ''' When was it last edited, if at all? This will be nothing if there was never an edit.
    ''' </summary>
    Public LastEdit As Date
    ''' <summary>
    ''' How many views has this submission had?
    ''' </summary>
    Public Views As Integer
    ''' <summary>
    ''' Which user posted it.
    ''' </summary>
    Public User As String
    ''' <summary>
    ''' What subverse was the submission submitted to at the time of posting. (This is not necessarily the one it appears on)
    ''' </summary>
    Public Subverse As String
    ''' <summary>
    ''' Is there a thumbnail? If so, this is the URL.
    ''' </summary>
    Public Thumbnail As String
    ''' <summary>
    ''' What is the title of this submission.
    ''' </summary>
    Public Title As String
    ''' <summary>
    ''' Type is 1 or 2. However I don't think that's useful. My code goes a step further. This is a PostType structure that gives you all information relating to what type means in relation to a post.
    ''' </summary>
    Public Type As PostType
    ''' <summary>
    ''' If it's a link post, URL that was posted. Nothing otherwise.
    ''' </summary>
    Public URL As String
    ''' <summary>
    ''' If it's a self post, content of the post. Nothing otherwise. 
    ''' </summary>
    Public Content As String
    ''' <summary>
    ''' If it's a self post, this will contain the formatted content of the post with HTML formatting. Useful for web view.
    ''' </summary>
    Public Formatted As String

End Structure


''' <summary>
''' Helper structure. PostType stores the type of a submission. It is extrapolated from what was given and stores the number value and the name (Self Post or Link Post)
''' </summary>
Public Structure PostType

    ''' <summary>
    ''' 1 for a self post, ie: text content that may include embedded links and images. 2 for a link post, ie: a post that is only a link to something else.
    ''' </summary>
    Public Type As Integer
    ''' <summary>
    ''' Rather than a number, it might be more useful for someone to have the ability to get the name of the type. This is either Link Post or Self Post depending.
    ''' </summary>
    Public Name As String

End Structure