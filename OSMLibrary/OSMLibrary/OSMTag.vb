Imports System.Xml

Public Class OSMTag
    Public Key As String
    Public Value As String
    Private Values As String()
    Public Sub LoadXML(XNode As XmlNode)
        Key = XNode.Attributes("k").InnerText
        Value = XNode.Attributes("v").InnerText
    End Sub
    Sub New(newKey As String, newValue As String)
        Key = newKey
        Value = newValue
    End Sub
    Sub New(xNode As XmlNode)
        LoadXML(xNode)
    End Sub
    Sub New()
        Key = ""
        Value = ""
    End Sub
End Class
