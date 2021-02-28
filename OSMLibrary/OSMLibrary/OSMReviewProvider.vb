
Public Enum OSMReviewResult
        OK
        WrongType
        NoData
    End Enum

Public Interface IOSMReviewProvider
    ''' <summary>
    ''' Populates a dictionary of the tags and values that the OSMObject "should" have according to the source
    ''' </summary>
    ''' <param name="o">The OSM Object being reviewed</param>
    ''' <param name="Item">An object respresenting the source data</param>
    ''' <param name="d">A dictionary of tag-value pairs (output)</param>
    ''' <returns></returns>
    Function Process(o As OSMObject, Item As Object, d As Dictionary(Of String, String)) As OSMReviewResult
End Interface

Public Structure OSMRelationReviewItem
    Dim Seq As Integer
    Dim ID As Long
    Dim Type As OSMObject.ObjectType
    Dim Obj As OSMObject
    Dim OSMRole As String
    Dim SourceRole As String
    Dim NewRole As String
    Public Const OSMROLE_DELETE As String = "<delete>"
    Public Const OSMROLE_NA As String = "<na>"
End Structure



Public Interface IOSMRelationReviewProvider
    ''' <summary>
    ''' Populates a dictionary of the members that the OSMRelation "should" have according to the source
    ''' </summary>
    ''' <param name="r">The OSM Relation being reviewed</param>
    ''' <param name="Item">An object respresenting the source data</param>
    ''' <param name="d">A list of members (output)</param>
    ''' <returns></returns>
    Function Process(r As OSMRelation, Item As Object, d As List(Of OSMRelationReviewItem)) As OSMReviewResult
End Interface
