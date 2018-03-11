Imports System.Xml

Public Class OSMTag
        Public Const MVDelimiter As String = ";"
        Public Key As String
        Private _value As String
        Public ReadOnly Property Values As String()
        ''' <summary>
        ''' Returns the whole value string, as stored in OSM
        ''' </summary>
        ''' <returns>The entire value of the tag</returns>
        Public Property Value As String
            Get
                Return _value
            End Get
            Set(value As String)
                _value = value
                _Values = Split(_value, MVDelimiter)
            End Set
        End Property
        ''' <summary>
        ''' Returns the primary value of the tag, being the first value in the case of a multi-valued tag,
        ''' or otherwise the whole value.
        ''' </summary>
        ''' <returns>The tag's primary value</returns>
        Public ReadOnly Property Value1 As String
            Get
                If _Values.Length > 0 Then
                    Return _Values(0)
                Else
                    Return ""
                End If
            End Get
        End Property
        ''' <summary>
        ''' Checks if any of the values is equal to a given value
        ''' </summary>
        ''' <param name="v"></param>
        ''' <returns>True if the value is found</returns>
        Public Function HasValue(v As String) As Boolean
            For i As Integer = 0 To (_Values.Length - 1)
                If _Values(i).Equals(v) Then Return True
            Next
            Return False
        End Function
        ''' <summary>
        ''' Populate this OSMTag object from OSM XML
        ''' </summary>
        ''' <param name="XNode">An XmlElement</param>
        Public Sub LoadXML(XNode As XmlElement)
            Key = XNode.Attributes("k").InnerText
            Value = XNode.Attributes("v").InnerText
        End Sub
        ''' <summary>
        ''' Initialise with the given key/value pair
        ''' </summary>
        ''' <param name="newKey">The key</param>
        ''' <param name="newValue">The value string</param>
        Sub New(newKey As String, newValue As String)
            Key = newKey
            Value = newValue
        End Sub
        ''' <summary>
        ''' Initialise from the given XML
        ''' </summary>
        ''' <param name="xNode">An XmlElement</param>
        Sub New(xNode As XmlElement)
            LoadXML(xNode)
        End Sub
        ''' <summary>
        ''' Initialise with empty key and value
        ''' </summary>
        Sub New()
            Key = ""
            Value = ""
        End Sub
    End Class
