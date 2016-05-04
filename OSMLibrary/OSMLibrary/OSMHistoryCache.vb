Imports System.Data.SQLite
Imports System.IO
Imports System.Text
Imports System.Xml
' caches individual objects, possibly as full history
' uses two tables in SQLite
' (type,ID,headversion,fullhistory,lastcheck)
' (type,ID,version,body)
' needs two connections:
' 1) online connection - OSMAPI instance
' 2) persistent storage - SQLite connection

Public Class OSMHistoryCache
    Dim sqlConn As SQLiteConnection
    Public Class OSMHistoryObject
        Public Type As OSMObject.ObjectType
        Public ID As ULong
        Public Version As Integer
        Public LastChecked As Date
        Friend _XML As String
        Public ReadOnly Property XML
            Get
                Return _XML
            End Get
        End Property

    End Class

    Private Oldest As Date = Now
    Private Cache As New Dictionary(Of String, OSMHistoryObject)
    Private api As New OSMApi
    Private Function MakeKey(Type As OSMObject.ObjectType, ID As ULong) As String
        Dim s As String
        If Type = OSMObject.ObjectType.Node Then
            s = "n"
        ElseIf Type = OSMObject.ObjectType.Way Then
            s = "w"
        Else
            s = "r"
        End If
        Return s & ID.ToString
    End Function
    Sub New()
        sqlConn = New SQLiteConnection("Data Source='T:\BoundsBeater\hcache.sqlite'")
        sqlConn.Open()
    End Sub
    Public Function OSMObjectHistory(Type As OSMObject.ObjectType, ID As ULong, Optional ForceFetch As Boolean = False) As OSMObject
        Dim xDoc As OSMDoc
        Dim xCurrent As OSMObject
        Dim xObj As OSMObject
        Dim hObj As OSMHistoryObject
        Dim sql As IDbCommand
        Dim sKey As String = MakeKey(Type, ID)
        If Cache.ContainsKey(sKey) Then
            hObj = Cache(sKey)
            If ForceFetch OrElse hObj.LastChecked < Oldest Then
                ' get current version
                xDoc = api.GetOSMDoc(Type, ID, False)
                ' get full history
            End If
        Else
            ' get full history
            xDoc = api.GetOSMObjectHistory(Type, ID)
            Select Case Type
                Case OSMObject.ObjectType.Node
                    xObj = xDoc.Nodes(ID)
                Case OSMObject.ObjectType.Way
                    xObj = xDoc.Ways(ID)
                Case OSMObject.ObjectType.Relation
                    xObj = xDoc.Relations(ID)
            End Select
            ' insert new cache entry
            hObj = New OSMHistoryObject
            hObj.Type = Type
            hObj.ID = ID
            hObj.LastChecked = Now
            hObj.Version = xObj.Version
            Dim sw As New MemoryStream
            Dim xtw As New XmlTextWriter(sw, Encoding.UTF8)
            xObj.Serialize(xtw)
            xtw.Flush()
            Dim sr As New StreamReader(sw, Encoding.UTF8)
            sw.Seek(0, SeekOrigin.Begin)
            hObj._XML = sr.ReadToEnd
            Insert(hObj)
        End If

#If False Then
        If found Then
            If too_old Or ForceFetch Then
            get current version
                If current.ver > osmobj.ver Then
                    If current.ver = osmobj.ver + 1 Then
                        merge current into osmobj
                    Else
                        get full history(type, id)
                        replace osmobj.xml with new full history
                    End If
                    update osmobj.ver = current.ver
                update timestamp of last check = now
                End If
            End If
            Return osmobj.xml
        Else
        get full history(type, id)
            insert osmobj
xml:        history.xml()
            Return history.xml
        End If
#End If

    End Function

    Public Function OSMNodeHistory(ID As ULong, Optional ForceFetch As Boolean = False) As OSMNode
        Return OSMObjectHistory(OSMObject.ObjectType.Node, ID, ForceFetch)
    End Function
    Public Function OSMWayHistory(ID As ULong, Optional ForceFetch As Boolean = False) As OSMWay

    End Function

    Public Function OSMRelationHistory(ID As ULong, Optional ForceFetch As Boolean = False) As OSMRelation

    End Function
#If False Then
osmobj
    type (n,w,r)
    id
    ver (head version)
    timestamp of last check
    xml (full history)

to get an objects history:
    get from osmobj(type, id, forcefetch)
    if found
        if too_old OR forcefetch then
            get current version
            if current.ver > osmobj.ver then
                if current.ver = osmobj.ver+1 then
                    merge current into osmobj
                else
                    get full history(type, id)
                    replace osmobj.xml with new full history
                end if
                update osmobj.ver = current.ver
                update timestamp of last check = now
            end if
        endif
        return osmobj.xml
    else
        get full history(type, id)
        insert osmobj
            xml: history.xml
        return history.xml
    end if

to get the deep history of a way:
    get shallow history as above
    for each version of way
        for each node in way
            if node is not in used_nodes
                add node to used_nodes
            end if
        next
    next
    for each node in used_nodes
        get shallow history of node
    next

to get the deep history of a relation:
    get shallow history as above
    for each version of relation
        for each way in relation
            if way is not in used_ways
                add way to used_ways
            end if
        next
        for each node in relation
            if node is not in used_nodes
                add node to used_nodes
            end if
        next
        for each subrel in relation
            do nothing - subrelations not supported!
        next
    next
    for each way in used_ways
        get deep history of way
    next
    for each node in used_nodes
        get deep history of nodes
    next
#End If

    Private Sub Insert(c As OSMHistoryObject)
        Dim cmd As New SQLiteCommand("INSERT INTO Cache (Key, LastChecked, HeadVersion, XML) VALUES(:Key, :Time, :Ver, :Data)", sqlConn)
        cmd.Parameters.Add("Key", DbType.String).Value = MakeKey(c.Type, c.ID)
        cmd.Parameters.Add("Time", DbType.String).Value = c.LastChecked.ToString("o")
        cmd.Parameters.Add("Ver", DbType.Int32).Value = c.Version
        cmd.Parameters.Add("Data", DbType.Xml).Value = c.XML
        Try
            cmd.ExecuteNonQuery()
        Catch

        End Try
    End Sub
    Public Function OSMObjectX() As OSMObject
    End Function
End Class
