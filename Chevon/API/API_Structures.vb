
''' <summary>
''' Helper Structure. Stores info sent by server on a API_SUBVERSE_INFO request.
''' </summary>
Structure SubVInfo

    Public Name As String
    Public Title As String
    Public Description As String
    Public Created As Date
    Public Subs As Integer
    Public Adult As Boolean
    Public Sidebar As String
    Public Type As String 'This seems pointless as of V1 at 7/13/2015 @ 3:39 AM Eastern UTC -4. All returns Link.

End Structure