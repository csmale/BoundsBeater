Public Class SubareaReviewProvider
    Implements IOSMReviewProvider

    ''' <summary>
    ''' Subarea Review Provider Class
    ''' </summary>
    ''' <param name="o"></param>
    ''' <param name="Item"></param>
    ''' <param name="d"></param>
    ''' <returns></returns>
    Public Function Process(o As OSMObject, Item As Object, d As Dictionary(Of String, String)) As OSMReviewResult Implements IOSMReviewProvider.Process
        Throw New NotImplementedException()
        If o Is Nothing Then Return OSMReviewResult.NoData
        If Item Is Nothing Then Return OSMReviewResult.NoData
        If o.Type <> OSMObject.ObjectType.Relation Then Return OSMReviewResult.WrongType

        Dim rel As OSMRelation = DirectCast(o, OSMRelation)
        For Each mbr In rel.Members
            If mbr.Role <> "subarea" Then Continue For
            If mbr.Member.Type <> OSMObject.ObjectType.Relation Then
                '' non-relation as subarea?
                Continue For
            End If

        Next
    End Function
End Class
