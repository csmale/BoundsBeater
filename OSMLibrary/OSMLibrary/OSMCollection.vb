Imports System
Imports System.Collections

Public Class OSMCollection
    Inherits DictionaryBase

    Private ObjType As OSMObject.ObjectType
    Private MbrType As Type

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

    Default Public Property Item(key As ULong) As OSMObject
        Get
            Return CType(Dictionary(key), OSMObject)
        End Get
        Set(value As OSMObject)
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

    Public Sub Add(key As ULong, value As OSMObject)
        Dictionary.Add(key, value)
    End Sub 'Add

    Public Function Contains(key As ULong) As Boolean
        Return Dictionary.Contains(key)
    End Function 'Contains

    Public Sub Remove(key As ULong)
        Dictionary.Remove(key)
    End Sub 'Remove

    Protected Overrides Sub OnInsert(key As Object, value As Object)
        If Not GetType(System.UInt64).IsAssignableFrom(key.GetType()) Then
            Throw New ArgumentException("key must be of type Integer.", "key")
        End If
        If Not MbrType.IsAssignableFrom(value.GetType()) Then
            Throw New ArgumentException("value must be of type OSMObject.", "value")
        End If
    End Sub 'OnInsert

    Protected Overrides Sub OnRemove(key As Object, value As Object)
        If Not GetType(System.UInt64).IsAssignableFrom(key.GetType()) Then
            Throw New ArgumentException("key must be of type Integer.", "key")
        End If
    End Sub 'OnRemove

    Protected Overrides Sub OnSet(key As Object, oldValue As Object, newValue As Object)
        If Not GetType(System.UInt64).IsAssignableFrom(key.GetType()) Then
            Throw New ArgumentException("key must be of type Integer.", "key")
        End If
        If Not MbrType.IsAssignableFrom(newValue.GetType()) Then
            Throw New ArgumentException("newValue must be of type OSMObject.", "newValue")
        End If
    End Sub 'OnSet

    Protected Overrides Sub OnValidate(key As Object, value As Object)
        If Not GetType(System.UInt64).IsAssignableFrom(key.GetType()) Then
            Throw New ArgumentException("key must be of type Integer.", "key")
        End If
        If Not MbrType.IsAssignableFrom(value.GetType()) Then
            Throw New ArgumentException("value must be of type OSMObject.", "value")
        End If
    End Sub 'OnValidate 
End Class
