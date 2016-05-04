Imports System.Xml

Public Class OSMObject
    Public ID As Long
    Public Version As Integer
    Public Changeset As Long
    Public User As String
    Public UserID As Long
    Public Timestamp As Date
    Public Planet As OSMPlanet
    Public Tags As New System.Collections.Hashtable(13)

    ' <node id="1774154673" version="1" changeset="11781363" lat="51.4082707" lon="0.1598613" user="The Maarssen Mapper" uid="30525" visible="true" timestamp="2012-06-02T23:12:42Z"/>
    ' <way id="165897977" visible="true" timestamp="2012-08-08T19:28:13Z" version="5" changeset="12660832" user="The Maarssen Mapper" uid="30525">
    ' <relation id="2210488" visible="true" timestamp="2012-08-11T23:00:00Z" version="24" changeset="12696728" user="The Maarssen Mapper" uid="30525">

    Public Sub New()

    End Sub
    Public Sub New(XML As XmlElement, p As OSMPlanet)
        Dim x As XmlElement
        Dim k As String
        Dim v As String
        Dim b As Boolean
        Planet = p
        b = Long.TryParse(XML.GetAttribute("id"), ID)
        b = Long.TryParse(XML.GetAttribute("version"), Version)
        b = Long.TryParse(XML.GetAttribute("changeset"), Changeset)
        User = XML.GetAttribute("user")
        b = Long.TryParse(XML.GetAttribute("uid"), UserID)
        b = DateTime.TryParse(XML.GetAttribute("timestamp"), Timestamp)
        For Each x In XML.SelectNodes("tag")
            k = x.GetAttribute("k")
            If Len(k) > 0 Then
                v = x.GetAttribute("v")
                Tags.Add(k, v)
            End If
        Next
    End Sub
    Public Function SubTags(Prefix As String) As System.Collections.Hashtable
        Dim ht As New System.Collections.Hashtable
        Dim sPref As String = Prefix
        If Right(sPref, 1) <> ":" Then sPref = sPref & ":"
        Dim prelen As Integer = Len(sPref)
        For Each k In Tags.Keys
            If Left(k, prelen) = sPref Then
                ht.Add(Mid(k, prelen + 1), Tags(k))
            End If
        Next
        Return ht
    End Function
End Class
