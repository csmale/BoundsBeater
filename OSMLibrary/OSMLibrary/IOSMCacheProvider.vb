Imports OSMLibrary

Public Class OSMCacheRecord
    Public ID As Long
    Public Type As OSMObject.ObjectType
    Public Version As Integer
    Public Timestamp As Date
    Public LastChecked As Date
    Public Status As OSMCacheRecordStatus
    Dim Compression As System.IO.Compression.CompressionLevel
    Public XML As String
End Class

Public Enum OSMCacheRecordStatus
    Deleted
    OK
End Enum

Public Interface IOSMCacheProvider
    Function Open(ConnString As String, Optional Create As Boolean = False) As Boolean
    Sub Close()
    Function GetByVersion() As OSMCacheRecord
    Function GetByDate() As OSMCacheRecord
    Function GetLatest(t As OSMObject.ObjectType, ID As ULong) As OSMCacheRecord
    Function Upsert(Data As OSMCacheRecord)
End Interface
