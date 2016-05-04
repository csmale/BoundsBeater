Public Class OSMUser
    Public UID As ULong
    Public Name As String

    Public Sub New()

    End Sub
    Public Sub New(UID As ULong, Name As String)
        Me.UID = UID
        Me.Name = Name
    End Sub
End Class
