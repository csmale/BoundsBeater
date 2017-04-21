Public Class OSMTagList
    Public Tags As New Dictionary(Of String, OSMTag)
    Public Sub Add(Key As String, Value As String)
        Add(New OSMTag(Key, Value))
    End Sub
    Public Sub Add(xTag As OSMTag)
        Tags.Add(xTag.Key, xTag)
    End Sub
    Default ReadOnly Property Item(Key As String) As OSMTag
        Get
            Return Tags(Key)
        End Get
    End Property
    Public Function Clone() As OSMTagList
        Dim x As New OSMTagList
        For Each y In Tags.Keys
            x.Tags.Add(y, Tags(y))
        Next
        Return x
    End Function
End Class
