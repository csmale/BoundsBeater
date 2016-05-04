Imports System.Xml

Public Class OSMRelation
    Inherits OSMObject

    Public Structure OSMRelationMember
        Dim o As OSMObject
        Dim Role As String
    End Structure

    Private _Nodes As New List(Of OSMRelationMember)
    Private _Ways As New List(Of OSMRelationMember)
    Private _Relations As New List(Of OSMRelationMember)

    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(XML As XmlElement, p As OSMPlanet)
        MyBase.New(XML, p)
        Dim st As String, role As String, oid As Long
        Dim mbr As OSMRelationMember
        For Each x As XmlElement In XML.SelectNodes("member")
            st = x.GetAttribute("type")
            role = x.GetAttribute("role")
            If Long.TryParse(x.GetAttribute("ref"), oid) Then
                mbr = New OSMRelationMember
                mbr.Role = role
                Select Case st
                    Case "node"
                        mbr.o = Planet.Node(oid)
                        _Nodes.Add(mbr)
                    Case "way"
                        mbr.o = Planet.Way(oid)
                        _Ways.Add(mbr)
                    Case "relation"
                        mbr.o = Planet.Relation(oid)
                        _Relations.Add(mbr)
                    Case Else
                        MsgBox("unknown relation member type '" & st & "' in relation " & ID)
                End Select
            Else
                MsgBox("unable to parse ID '" & x.GetAttribute("ref") & "' of " & st & " as " & role)
            End If
        Next
    End Sub
    Public ReadOnly Property Ways As List(Of OSMRelationMember)
        Get
            Return _Ways
        End Get
    End Property
    Public ReadOnly Property Nodes As List(Of OSMRelationMember)
        Get
            Return _Nodes
        End Get
    End Property
    Public ReadOnly Property Relations As List(Of OSMRelationMember)
        Get
            Return _Relations
        End Get
    End Property
End Class
