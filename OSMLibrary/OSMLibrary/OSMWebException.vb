
Public Class OSMWebException
        Inherits Exception
        Implements System.Runtime.Serialization.ISerializable

        Public Sub New()
            MyBase.New()
            ' Add implementation.
        End Sub

        Public Sub New(ByVal message As String)
            MyBase.New()
            ' Add implementation.
        End Sub

        Public Sub New(ByVal message As String, ByVal inner As Exception)
            MyBase.New()
            ' Add implementation.
        End Sub
    End Class

