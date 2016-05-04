Imports System.Xml

Public Class OSMNode
    Inherits OSMObject
    Public Lat As Double
    Public Lon As Double

    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(XML As XmlElement, p As OSMPlanet)
        MyBase.New(XML, p)
        Dim b As Boolean
        b = Double.TryParse(XML.GetAttribute("lat"), Lat)
        b = Double.TryParse(XML.GetAttribute("lon"), Lon)
    End Sub
End Class
