Imports System.Windows.Forms.VisualStyles

Public Class Trie
    ''' <summary>
    ''' Implements a Trie structure based on https://en.wikipedia.org/wiki/Trie
    ''' The implementation uses the priniciples of a Radix Tree - see https://en.wikipedia.org/wiki/Radix_tree
    ''' </summary>
    Public ReadOnly Property Key As String = ""
    Public Tag As Object
    Public ReadOnly Property Children As List(Of Trie) = New List(Of Trie)
    Public ReadOnly Property Parent As Trie = Nothing

    ''' <summary>
    ''' Adds a new key to the Trie, splitting nodes as required
    ''' </summary>
    ''' <param name="sKey">The new key to be added</param>
    ''' <param name="oTag">The tag to be associated with the leaf node</param>
    ''' <returns>The leaf node</returns>
    Public Function Insert(sKey As String, oTag As Object) As Trie
        If Len(sKey) = 0 Then Throw New ArgumentOutOfRangeException(NameOf(sKey), "Cannot be empty")
        Dim xNew As Trie

        ' see how much is already there
        Dim base As Trie = Search(sKey)

        ' case 1: no match
        If base Is Nothing Then ' no partial match so just add new child
            xNew = New Trie()
            Children.Add(xNew)
            xNew._Parent = Me
            xNew._Key = sKey
            xNew.Tag = oTag
            Return xNew
        End If

        Dim sBasePath As String = base.Path("")
        ' case 2: new string is longer
        If Len(sKey) > Len(sBasePath) Then
            ' case 2a: simple substring already present
            If Left(sBasePath, Len(sKey)) = sKey Then
                Return base.Insert(Mid(sBasePath, Len(sKey) + 1), oTag)
            End If
            ' case 2b: common root but different leaf nodes required

        End If

        ' see how much we have already in the tree and extract what we still need to add

        Dim sBaseKey As String
        ' need to split the base node?
        If Len(sBasePath) > Len(sKey) Then
            sBaseKey = Mid(sBasePath, Len(sKey) + 1)
            base._Key = sBaseKey
            base.Insert(sRest, oTag)
        End If


        Dim sRest As String = Mid(sKey, Len(sBasePath) + 1)
        ' if we are there already, we have a duplicate
        If Len(sRest) = 0 Then
            base.Tag = oTag
            Return base
        End If
        ' search for a common prefix

        For Each c In Children
            ' duplicate value - ok if we are just adding a tag for the first time
            If Len(sKey) = Len(c.Key) Then
                If (c.Tag Is Nothing) Then
                    c.Tag = oTag
                Else
                    Throw New ArgumentException("Duplicate value", NameOf(sKey))
                End If
            End If
            If Len(sKey) < Len(c.Key) Then
                Dim x As New Trie
                x.Children.Add(c)
                x._Parent = Me
                x.Tag = oTag
                x._Key = sKey
                Return x
            End If
            If Left(sKey, Len(c.Key)) = c.Key Then
                Return c.Insert(Mid(sKey, Len(c.Key) + 1), oTag)
            End If
        Next
        Dim x As New Trie
        x._Parent = Me
        Children.Add(x)
        _Key = sKey
        Tag = oTag
        Return x
    End Function

    ''' <summary>
    ''' Searches for a key by recursing down the tree
    ''' </summary>
    ''' <param name="sKey">The key to be searched for</param>
    ''' <returns>The node where the key leads</returns>
    Public Function Search(sKey As String) As Trie
        If Len(sKey) = 0 Then Throw New ArgumentOutOfRangeException(NameOf(sKey), "Cannot be empty")
        For Each c In Children
            If Left(sKey, Len(c.Key)) = c.Key Then
                Return c.Search(Mid(sKey, Len(c.Key) + 1))
            End If
        Next
        If Left(sKey, Len(Key)) = Key Then Return Me
        Return Nothing
    End Function
    ''' <summary>
    ''' Joins the node with its parent back up to the root
    ''' </summary>
    ''' <param name="sDelim">A string to insert between the key parts</param>
    ''' <returns>The path with its components seperated by the delimiter</returns>
    Public Function Path(sDelim As String) As String
        If Parent Is Nothing Then Return ""
        Return Parent.Path(sDelim) & sDelim & Key
    End Function
End Class
