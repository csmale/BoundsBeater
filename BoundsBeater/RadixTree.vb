Imports System.Windows.Forms.VisualStyles

Public Class RadixTree
    ''' <summary>
    ''' Implements a RadixTree structure
    ''' The implementation uses the priniciples of a Radix Tree
    ''' See https://en.wikipedia.org/wiki/Radix_tree
    ''' </summary>
    Public ReadOnly Property Key As String = ""
    Public Tag As Object
    Public ReadOnly Property Children As List(Of RadixTree) = New List(Of RadixTree)
    Public ReadOnly Property Parent As RadixTree = Nothing

    ''' <summary>
    ''' Adds a new key to the RadixTree, splitting nodes as required
    ''' </summary>
    ''' <param name="sKey">The new key to be added</param>
    ''' <param name="oTag">The tag to be associated with the leaf node</param>
    ''' <returns>The leaf node</returns>
    Public Function Insert(sKey As String, oTag As Object) As RadixTree
        If Len(sKey) = 0 Then Throw New ArgumentOutOfRangeException(NameOf(sKey), "Cannot be empty")
        Dim xNew As RadixTree

        Debug.Print($"Insert: {sKey}")

        ' see how much is already there
        Dim base As RadixTree = Search(sKey)

        ' case 1: no match at all
        If base Is Nothing Then ' no partial match so just add new child
            xNew = New RadixTree()
            Children.Add(xNew)
            xNew._Parent = Me
            xNew._Key = sKey
            xNew.Tag = oTag
            Return xNew
        End If

        '  get the initial substring covered by the search result
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
        Dim nt As RadixTree
        ' need to split the base node?
        If Len(sBasePath) > Len(sKey) Then
            nt = base.Split(sKey)
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
                nt = New RadixTree
                nt.Children.Add(c)
                nt._Parent = Me
                nt.Tag = oTag
                nt._Key = sKey
                Return nt
            End If
            If Left(sKey, Len(c.Key)) = c.Key Then
                Return c.Insert(Mid(sKey, Len(c.Key) + 1), oTag)
            End If
        Next
        Dim x As New RadixTree
        x._Parent = Me
        Children.Add(x)
        _Key = sKey
        Tag = oTag
        Return x
    End Function

    ''' <summary>
    ''' Split the current node, making it represent the given substring, pushing the lower levels down
    ''' to the new node
    ''' </summary>
    ''' <param name="Substring">Must contain an initial substring of the key of the current node</param>
    ''' <returns>The newly created node, containing the remainder of the key</returns>
    Private Function Split(Substring As String) As RadixTree
        Debug.Print($"Split {_Key} for {Substring}")
        If Len(Substring) >= Len(_Key) Or Left(_Key, Len(Substring)) <> Substring Then
            Throw New ArgumentException("Bad substring for split", NameOf(Substring))
        End If
        Dim nt As New RadixTree
        nt._Parent = Me
        nt._Key = Mid(_Key, Len(Substring))
        nt.Tag = Me.Tag
        nt._Children.AddRange(Children)
        Me._Key = Substring
        Me.Tag = Nothing
        Me._Children.Clear()
        Me._Children.Add(nt)
        Debug.Print($"Split returning {nt._Key}")
        Return nt
    End Function
    ''' <summary>
    ''' Searches for a key by recursing down the tree
    ''' </summary>
    ''' <param name="sKey">The key to be searched for</param>
    ''' <returns>The node where the key leads</returns>
    Public Function Search(sKey As String) As RadixTree
        Debug.Print($"Search for {sKey} from {_Key}")
        If Len(sKey) = 0 Then Throw New ArgumentOutOfRangeException(NameOf(sKey), "Cannot be empty")
        For Each c In Children
            If Left(sKey, Len(c.Key)) = c.Key Then
                Return c.Search(Mid(sKey, Len(c.Key) + 1))
            End If
        Next
        If Left(sKey, Len(Key)) = Key Then
            Debug.Print($"Search for {sKey} returning {_Key}")
            Return Me
        End If
        Debug.Print($"Search for {sKey} failed")
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

    ''' <summary>
    ''' '
    ''' </summary>
    ''' <param name="s1"></param>
    ''' <param name="s2"></param>
    ''' <returns></returns>
    Private Shared Function FindCommonPrefix(s1 As String, s2 As String) As Integer
        Dim iLen As Integer
        Dim iMatchLen As Integer = Math.Min(Len(s1), Len(s2))
        If iMatchLen = 0 Then Return 0
        For iLen = 1 To iMatchLen
            If Mid(s1, iLen, 1) <> Mid(s2, iLen, 1) Then Exit For
        Next
        Return iLen - 1
    End Function
End Class
