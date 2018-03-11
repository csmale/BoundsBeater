Imports System.Data.SQLite
Imports System.IO
Imports System.Text
Imports System.Xml
#If False Then
Schema:
table OSMOBJVER
    type char(1) n,w,r
    ID numeric
    version numeric
    status char(1) Good, Deleted, ?Unknown
    timestamp datetime
    compression numeric (0 is uncompressed)
    XML blob - string or compressed string
    primary key(type,ID,version)
    
table OSMOBJ
    type char(1) as above
    ID numeric
    maxver numeric
    primary key(type,ID)

#End If
Public Class OSMCache
        Dim sqlConn As SQLiteConnection
        Dim myCompressionLevel As System.IO.Compression.CompressionLevel = Compression.CompressionLevel.Optimal

        Public Sub New(CacheFile As String, Optional Create As Boolean = False)
            Open(CacheFile, Create)
        End Sub
        Public Function Open(CacheFile As String, Optional Create As Boolean = False) As Boolean
            Dim xcs As New SQLiteConnectionStringBuilder

            If Not System.IO.File.Exists(CacheFile) Then
                If Create Then
                    SQLiteConnection.CreateFile(CacheFile)
                Else
                    Return False
                End If
            End If
            ' we now know the file exists, lets open it
            xcs.Add("Data Source", CacheFile)
            Dim newconn As SQLiteConnection
            Try
                newconn = New SQLiteConnection(xcs.ConnectionString)
                newconn.Open()
            Catch ex As Exception
                Return False
            End Try
            ' now we are connected to a database. can we use it?
            If isValidDB(newconn) Then
                sqlConn = newconn
                Return True
            End If
            If CreateSchema(newconn) Then
                sqlConn = newconn
                Return True
            End If
            Return False
        End Function
        Public Function isValidDB(Conn As SQLiteConnection) As Boolean
            ' check the tables exist
            If IsNothing(Conn) Then Return False
            ' If Conn.State <> ConnectionState.Open Then Return False
            Dim cmd As SQLiteCommand = Conn.CreateCommand
            Try
                cmd.CommandText = "select count(*) from osmobj"
                cmd.ExecuteScalar()
                cmd.CommandText = "select count(*) from osmobjver"
                cmd.ExecuteScalar()
            Catch ex As Exception
                MsgBox(ex.Message)
                Return False
            End Try
            Return True
        End Function
        Public Function CreateSchema(Conn As SQLiteConnection) As Boolean
            ' create required tables in an empty database
            Dim cmd As SQLiteCommand = Conn.CreateCommand()
            Dim cb As New SQLiteCommandBuilder
            Dim i As Integer
            Try
                cmd.CommandText = <![CDATA[
                CREATE TABLE osmobjver (
                    Type CHAR,
                    ID INTEGER,
                    Version INTEGER,
                    Status INTEGER,
                    Timestamp DATE,
                    Compression INTEGER,
                    Value CHAR,
                    PRIMARY KEY(Type,ID,Version)
                )   
                ]]>.Value
                i = cmd.ExecuteNonQuery()
                cmd.CommandText = <![CDATA[
                CREATE TABLE osmobj (
                    Type CHAR,
                    ID INTEGER,
                    Version INTEGER,
                    Lastcheck DATE,
                    PRIMARY KEY(Type, ID)
                )
                ]]>.Value()
                i = cmd.ExecuteNonQuery()

            Catch e As SQLiteException
                MsgBox(e.ToString)
                Return False
            End Try
            Return True
        End Function

        Public Function AddObject(Type As OSMObject.ObjectType, ID As Long, Version As Integer, Timestamp As Date, sValue As String) As Boolean
            Dim sqlTxn As SQLiteTransaction
            Dim sqlCmd As SQLiteCommand = sqlConn.CreateCommand()
            Dim iRows As Integer
            Try
                sqlTxn = sqlConn.BeginTransaction()
                sqlCmd.CommandText = "INSERT INTO osmobjver (Type, ID, Version, Status, TimeStamp, Compression, Value) VALUES (@type,@id,@version,@status,@timestamp,@compression,@value)"
                sqlCmd.Parameters.AddWithValue("@type", OSMObject.ObjectTypeChar(Type))
                sqlCmd.Parameters.AddWithValue("@id", ID)
                sqlCmd.Parameters.AddWithValue("@version", Version)
                sqlCmd.Parameters.AddWithValue("@status", "G")
                sqlCmd.Parameters.AddWithValue("@timestamp", Timestamp)
                sqlCmd.Parameters.AddWithValue("@compression", 0)
                sqlCmd.Parameters.AddWithValue("@value", sValue)
                sqlCmd.Parameters.AddWithValue("@lastcheck", Now())
                iRows = sqlCmd.ExecuteNonQuery()
                If iRows = 1 Then
                    sqlCmd.CommandText = "UPDATE osmobj SET Version = @version, LastCheck=@lastcheck WHERE Type=@type AND ID=@id AND Version<@version"
                    iRows = sqlCmd.ExecuteNonQuery()
                    If iRows = 0 Then
                        sqlCmd.CommandText = "INSERT INTO osmobj (Type, ID, Version, Lastcheck) VALUES (@type,@id,@version,@lastcheck)"
                        iRows = sqlCmd.ExecuteNonQuery()
                    End If
                End If
                sqlTxn.Commit()
            Catch e As SQLiteException
                sqlTxn?.Rollback()
                Return False
            End Try
            Return True
        End Function

        Public Function GetObject(Type As OSMObject.ObjectType, ID As Long, Version As Integer) As Object
            Dim oTmp As Object
            Dim sqlCmd As SQLiteCommand = sqlConn.CreateCommand
            sqlCmd.Parameters.AddWithValue("@id", ID)
            sqlCmd.Parameters.AddWithValue("@type", OSMObject.ObjectTypeChar(Type))
            sqlCmd.Parameters.AddWithValue("@version", Version)
            If Version = 0 Then ' latest version
                sqlCmd.CommandText = "SELECT V.Value FROM osmobjver V, osmobj O WHERE v.Type=o.Type AND v.ID=o.ID AND v.Version = o.Version AND o.Type=@type AND o.ID=@id"
            Else ' specific version
                sqlCmd.CommandText = "SELECT V.Value FROM osmobjver V WHERE v.Type=@type AND v.ID=@id AND v.Version = @version"
            End If
            oTmp = sqlCmd.ExecuteScalar()
            Return oTmp
        End Function

        Public Function xxxgetObjectVersion(type, ID, version) As String
            ' Select status, userID, username, timestamp, xml from objver v  where v.type And v.ID = ID And v.version = version
        End Function

        Public Function xxxgetObject(type, ID) As String
            ' Select status, userID, username, timestamp, xml from objver v where v.type = type And v.ID = ID And v.version = (Select maxver from obj o where o.type = type And o.ID = ID)


        End Function
        Private Function Compress(s As String) As Byte()
            'Compress
            Dim mem As New IO.MemoryStream
            Dim gz As New System.IO.Compression.GZipStream(mem, myCompressionLevel)
            Dim sw As New IO.StreamWriter(gz)
            sw.Write(s)
            mem.Seek(0, IO.SeekOrigin.Begin)
            Dim sr As New IO.BinaryReader(mem)
            Compress = sr.ReadBytes(CInt(mem.Length))
            sw.Close()
        End Function
        Private Function Decompress(b() As Byte) As String
            'Compress
            Dim mem As New IO.MemoryStream
            Dim gz As New System.IO.Compression.GZipStream(mem, IO.Compression.CompressionMode.Decompress)
            Dim sw As New IO.StreamWriter(gz)
            sw.Write(b)
            mem.Seek(0, IO.SeekOrigin.Begin)
            Dim sr As New IO.BinaryReader(mem)
            Decompress = sr.ReadString
            sr.Close()
        End Function
        Public Function CachePeek() As OSMCacheRecord
            ' select from osmobjver
        End Function
        Public Function CacheForget(Record As OSMCacheRecord)
            ' delete from osmobjver
            ' version=0 means whole object, otherwise specified version only
            If Record.Version = 0 Then
                ' delete from osmobjver where id=record.id
                ' delete from osmobj from id=record.id
            Else
                ' delete from osmobjver where id=record.id and version=record.version

            End If
        End Function
        Public Function CacheInsert(Record As OSMCacheRecord)
            ' insert into osmobjver
        End Function


    End Class
