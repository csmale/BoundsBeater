Imports System.Drawing

Public Class OSMRelationMember
    Inherits OSMObject
    Public Role As String
    Public Member As OSMObject
    Public Overrides ReadOnly Property Type As ObjectType
        Get
            If IsNothing(Member) Then
                Return Nothing
            Else
                Return Member.Type
            End If
        End Get
    End Property
    Public ReadOnly Property TypeString As String
        Get
            Return OSMObject.ObjectTypeString(Member.Type)
        End Get
    End Property
    Public Overrides ReadOnly Property Bbox As BBox
        Get
            Return Member.Bbox
        End Get
    End Property
    Public Overrides ReadOnly Property JSON As String
        Get
            Return Member.JSON
        End Get
    End Property
    Public Overrides ReadOnly Property GeoJSON As String
        Get
            Return Member.GeoJSON
        End Get
    End Property
    Public Sub New(Way As OSMWay, Role As String)
        Member = Way
        Me.Role = Role
    End Sub
    Public Sub New(Rel As OSMRelation, Role As String)
        Member = Rel
        Me.Role = Role
    End Sub
    Public Sub New(Node As OSMNode, Role As String)
        Member = Node
        Me.Role = Role
    End Sub
    Public Sub New(Obj As OSMObject, Role As String)
        Member = Obj
        Me.Role = Role
    End Sub
    Public Overrides ReadOnly Property Centroid As DPoint
        Get
            Return Member.Centroid
        End Get
    End Property
    Public Overrides Sub SerializeMe(x As Xml.XmlWriter)

    End Sub
End Class
