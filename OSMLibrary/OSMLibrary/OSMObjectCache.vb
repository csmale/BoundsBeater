
Public Class OSMObjectCache
        Dim cp As IOSMCacheProvider
        Public Sub New()

        End Sub
        Public Sub New(p As IOSMCacheProvider)
            cp = p
        End Sub
        Public Property MaxAge As ULong
        Public Function AddObject(xObject As OSMObject) As Boolean

        End Function

        Public Function GetObject(t As OSMObject.ObjectType, ID As ULong) As OSMObject
            Dim cr As OSMCacheRecord
            cr = cp.GetLatest(t, ID)
        End Function
        Public Sub ForgetObject(t As OSMObject.ObjectType, ID As ULong)

        End Sub
        Public Function AddAllObjects(xDoc As OSMDoc) As Boolean
            Dim bRes As Boolean = True
            For Each r In xDoc.Relations
                bRes = bRes And AddObject(r)
            Next
            For Each w In xDoc.Ways
                bRes = bRes And AddObject(w)
            Next
            For Each n In xDoc.Nodes
                bRes = bRes And AddObject(n)
            Next
            Return bRes
        End Function
    End Class

