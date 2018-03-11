Imports System
Imports System.Collections


Public Class OSMCollection(Of T)
        Inherits DictionaryBase

        Private ObjType As OSMObject.ObjectType
        Private MbrType As Type

#If False Then
    Public Sub New(OType As OSMObject.ObjectType)
        MyBase.New()
        ObjType = OType
        Select Case OType
            Case OSMObject.ObjectType.Node
                MbrType = GetType(OSMNode)
            Case OSMObject.ObjectType.Way
                MbrType = GetType(OSMWay)
            Case OSMObject.ObjectType.Relation
                MbrType = GetType(OSMRelation)
            Case OSMObject.ObjectType.Changeset
                MbrType = GetType(OSMChangeset)
        End Select
    End Sub
#End If

        Public Sub New()
            MbrType = GetType(T)
        End Sub

        Default Public Property Item(key As Long) As T
            Get
                Return DirectCast(Dictionary(key), T)
            End Get
            Set(value As T)
                Dictionary(key) = value
            End Set
        End Property

        Public ReadOnly Property Keys() As ICollection
            Get
                Return Dictionary.Keys
            End Get
        End Property

        Public ReadOnly Property Values() As ICollection
            Get
                Return Dictionary.Values
            End Get
        End Property

        Public Sub Add(key As Long, value As T)
            Dictionary.Add(key, value)
        End Sub 'Add

        Public Function Contains(key As Long) As Boolean
            Return Dictionary.Contains(key)
        End Function 'Contains

        Public Sub Remove(key As Long)
            Dictionary.Remove(key)
        End Sub 'Remove

        Protected Overrides Sub OnInsert(key As Object, value As Object)
            If Not GetType(Long).IsAssignableFrom(key.GetType()) Then
                Throw New ArgumentException("key must be of type Integer.", NameOf(key))
            End If
#If False Then
        If Not MbrType.IsAssignableFrom(value.GetType()) Then
            Throw New ArgumentException("value must be of type OSMObject.", NameOf(value))
        End If
#End If
        End Sub 'OnInsert

        Protected Overrides Sub OnRemove(key As Object, value As Object)
            If Not GetType(Long).IsAssignableFrom(key.GetType()) Then
                Throw New ArgumentException("key must be of type Integer.", NameOf(key))
            End If
        End Sub 'OnRemove

        Protected Overrides Sub OnSet(key As Object, oldValue As Object, newValue As Object)
            If Not GetType(Long).IsAssignableFrom(key.GetType()) Then
                Throw New ArgumentException("key must be of type Integer.", NameOf(key))
            End If
#If False Then
        If Not MbrType.IsAssignableFrom(newValue.GetType()) Then
            Throw New ArgumentException("newValue must be of type OSMObject.", NameOf(newValue))
        End If
#End If
        End Sub 'OnSet

        Protected Overrides Sub OnValidate(key As Object, value As Object)
            If Not GetType(Long).IsAssignableFrom(key.GetType()) Then
                Throw New ArgumentException("key must be of type Integer.", NameOf(key))
            End If
#If False Then
        If Not MbrType.IsAssignableFrom(value.GetType()) Then
            Throw New ArgumentException("value must be of type OSMObject.", NameOf(value))
        End If
#End If
        End Sub 'OnValidate 
    End Class
