Public Enum OSMReviewResult
    OK
    WrongType
    NoData
End Enum

Public Interface IOSMReviewProvider
    Function Process(o As OSMObject, d As Dictionary(Of String, String)) As OSMReviewResult
End Interface
