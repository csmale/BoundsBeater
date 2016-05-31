
      ' Generated from 
' Generated from: osmformat.proto
Namespace osmformat
<Global.System.Serializable, Global.ProtoBuf.ProtoContract(Name:="HeaderBlock")> _
Public Partial Class HeaderBlock
    implements Global.ProtoBuf.IExtensible
	
	Public Sub New
	End Sub
    
    Private _bbox As HeaderBBox =Nothing
        <Global.ProtoBuf.ProtoMember(1, IsRequired:=False, Name:="bbox", DataFormat:=Global.ProtoBuf.DataFormat.Default)>
        <Global.System.ComponentModel.DefaultValue(CType(Nothing, HeaderBBox))> _ 
    Public Property bbox As HeaderBBox
		Get 
			Return _bbox
		End Get
	
		Set(value As HeaderBBox)
		_bbox = value 
			
		End Set
	End Property
    
    Private ReadOnly _required_features as Global.System.Collections.Generic.List(Of String) = New Global.System.Collections.Generic.List(Of String)()
		
	<Global.ProtoBuf.ProtoMember(4, Name:="required_features", DataFormat:=Global.ProtoBuf.DataFormat.Default)> _
		Public ReadOnly Property required_features As Global.System.Collections.Generic.List(Of String)
		
		Get
			Return _required_features
		End Get
		
	End Property
  
    Private ReadOnly _optional_features as Global.System.Collections.Generic.List(Of String) = New Global.System.Collections.Generic.List(Of String)()
		
	<Global.ProtoBuf.ProtoMember(5, Name:="optional_features", DataFormat:=Global.ProtoBuf.DataFormat.Default)> _
		Public ReadOnly Property optional_features As Global.System.Collections.Generic.List(Of String)
		
		Get
			Return _optional_features
		End Get
		
	End Property
  
    Private _writingprogram As String =""
    <Global.ProtoBuf.ProtoMember(16, IsRequired:=False, Name:="writingprogram", DataFormat:=Global.ProtoBuf.DataFormat.Default)> _ 
    <Global.System.ComponentModel.DefaultValue(CType("", String))> _ 
    Public Property writingprogram As String
		Get 
			Return _writingprogram
		End Get
	
		Set(value As String)
		_writingprogram = value 
			
		End Set
	End Property
    
    Private _source As String =""
    <Global.ProtoBuf.ProtoMember(17, IsRequired:=False, Name:="source", DataFormat:=Global.ProtoBuf.DataFormat.Default)> _ 
    <Global.System.ComponentModel.DefaultValue(CType("", String))> _ 
    Public Property source As String
		Get 
			Return _source
		End Get
	
		Set(value As String)
		_source = value 
			
		End Set
	End Property
    
    Private extensionObject As Global.ProtoBuf.IExtension
		Function GetExtensionObject(createIfMissing As Boolean) As Global.ProtoBuf.IExtension Implements Global.ProtoBuf.IExtensible.GetExtensionObject
			Return Global.ProtoBuf.Extensible.GetExtensionObject(extensionObject, createIfMissing)
		End Function
End Class
  
<Global.System.Serializable, Global.ProtoBuf.ProtoContract(Name:="HeaderBBox")> _
Public Partial Class HeaderBBox
    implements Global.ProtoBuf.IExtensible
	
	Public Sub New
	End Sub
    
    Private _left As Long
    <Global.ProtoBuf.ProtoMember(1, IsRequired:=True, Name:="left", DataFormat:=Global.ProtoBuf.DataFormat.ZigZag)> _ 
    Public Property left As Long
		Get 
			Return _left
		End Get
	
		Set(value As Long)
		_left = value 
			
		End Set
	End Property
    
    Private _right As Long
    <Global.ProtoBuf.ProtoMember(2, IsRequired:=True, Name:="right", DataFormat:=Global.ProtoBuf.DataFormat.ZigZag)> _ 
    Public Property right As Long
		Get 
			Return _right
		End Get
	
		Set(value As Long)
		_right = value 
			
		End Set
	End Property
    
    Private _top As Long
    <Global.ProtoBuf.ProtoMember(3, IsRequired:=True, Name:="top", DataFormat:=Global.ProtoBuf.DataFormat.ZigZag)> _ 
    Public Property top As Long
		Get 
			Return _top
		End Get
	
		Set(value As Long)
		_top = value 
			
		End Set
	End Property
    
    Private _bottom As Long
    <Global.ProtoBuf.ProtoMember(4, IsRequired:=True, Name:="bottom", DataFormat:=Global.ProtoBuf.DataFormat.ZigZag)> _ 
    Public Property bottom As Long
		Get 
			Return _bottom
		End Get
	
		Set(value As Long)
		_bottom = value 
			
		End Set
	End Property
    
    Private extensionObject As Global.ProtoBuf.IExtension
		Function GetExtensionObject(createIfMissing As Boolean) As Global.ProtoBuf.IExtension Implements Global.ProtoBuf.IExtensible.GetExtensionObject
			Return Global.ProtoBuf.Extensible.GetExtensionObject(extensionObject, createIfMissing)
		End Function
End Class
  
<Global.System.Serializable, Global.ProtoBuf.ProtoContract(Name:="PrimitiveBlock")> _
Public Partial Class PrimitiveBlock
    implements Global.ProtoBuf.IExtensible
	
	Public Sub New
	End Sub
    
    Private _stringtable As StringTable
    <Global.ProtoBuf.ProtoMember(1, IsRequired:=True, Name:="stringtable", DataFormat:=Global.ProtoBuf.DataFormat.Default)> _ 
    Public Property stringtable As StringTable
		Get 
			Return _stringtable
		End Get
	
		Set(value As StringTable)
		_stringtable = value 
			
		End Set
	End Property
    
    Private ReadOnly _primitivegroup as Global.System.Collections.Generic.List(Of PrimitiveGroup) = New Global.System.Collections.Generic.List(Of PrimitiveGroup)()
		
	<Global.ProtoBuf.ProtoMember(2, Name:="primitivegroup", DataFormat:=Global.ProtoBuf.DataFormat.Default)> _
		Public ReadOnly Property primitivegroup As Global.System.Collections.Generic.List(Of PrimitiveGroup)
		
		Get
			Return _primitivegroup
		End Get
		
	End Property
  
    Private _granularity As Integer =CType(100, Integer)
    <Global.ProtoBuf.ProtoMember(17, IsRequired:=False, Name:="granularity", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _ 
    <Global.System.ComponentModel.DefaultValue(CType(CType(100, Integer), Integer))> _ 
    Public Property granularity As Integer
		Get 
			Return _granularity
		End Get
	
		Set(value As Integer)
		_granularity = value 
			
		End Set
	End Property
    
    Private _lat_offset As Long =CType(0, Long)
    <Global.ProtoBuf.ProtoMember(19, IsRequired:=False, Name:="lat_offset", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _ 
    <Global.System.ComponentModel.DefaultValue(CType(CType(0, Long), Long))> _ 
    Public Property lat_offset As Long
		Get 
			Return _lat_offset
		End Get
	
		Set(value As Long)
		_lat_offset = value 
			
		End Set
	End Property
    
    Private _lon_offset As Long =CType(0, Long)
    <Global.ProtoBuf.ProtoMember(20, IsRequired:=False, Name:="lon_offset", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _ 
    <Global.System.ComponentModel.DefaultValue(CType(CType(0, Long), Long))> _ 
    Public Property lon_offset As Long
		Get 
			Return _lon_offset
		End Get
	
		Set(value As Long)
		_lon_offset = value 
			
		End Set
	End Property
    
    Private _date_granularity As Integer =CType(1000, Integer)
    <Global.ProtoBuf.ProtoMember(18, IsRequired:=False, Name:="date_granularity", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _ 
    <Global.System.ComponentModel.DefaultValue(CType(CType(1000, Integer), Integer))> _ 
    Public Property date_granularity As Integer
		Get 
			Return _date_granularity
		End Get
	
		Set(value As Integer)
		_date_granularity = value 
			
		End Set
	End Property
    
    Private extensionObject As Global.ProtoBuf.IExtension
		Function GetExtensionObject(createIfMissing As Boolean) As Global.ProtoBuf.IExtension Implements Global.ProtoBuf.IExtensible.GetExtensionObject
			Return Global.ProtoBuf.Extensible.GetExtensionObject(extensionObject, createIfMissing)
		End Function
End Class
  
<Global.System.Serializable, Global.ProtoBuf.ProtoContract(Name:="PrimitiveGroup")> _
Public Partial Class PrimitiveGroup
    implements Global.ProtoBuf.IExtensible
	
	Public Sub New
	End Sub
    
    Private ReadOnly _nodes as Global.System.Collections.Generic.List(Of Node) = New Global.System.Collections.Generic.List(Of Node)()
		
	<Global.ProtoBuf.ProtoMember(1, Name:="nodes", DataFormat:=Global.ProtoBuf.DataFormat.Default)> _
		Public ReadOnly Property nodes As Global.System.Collections.Generic.List(Of Node)
		
		Get
			Return _nodes
		End Get
		
	End Property
  
    Private _dense As DenseNodes =Nothing
    <Global.ProtoBuf.ProtoMember(2, IsRequired:=False, Name:="dense", DataFormat:=Global.ProtoBuf.DataFormat.Default)> _ 
    <Global.System.ComponentModel.DefaultValue(CType(Nothing, DenseNodes))> _ 
    Public Property dense As DenseNodes
		Get 
			Return _dense
		End Get
	
		Set(value As DenseNodes)
		_dense = value 
			
		End Set
	End Property
    
    Private ReadOnly _ways as Global.System.Collections.Generic.List(Of Way) = New Global.System.Collections.Generic.List(Of Way)()
		
	<Global.ProtoBuf.ProtoMember(3, Name:="ways", DataFormat:=Global.ProtoBuf.DataFormat.Default)> _
		Public ReadOnly Property ways As Global.System.Collections.Generic.List(Of Way)
		
		Get
			Return _ways
		End Get
		
	End Property
  
    Private ReadOnly _relations as Global.System.Collections.Generic.List(Of Relation) = New Global.System.Collections.Generic.List(Of Relation)()
		
	<Global.ProtoBuf.ProtoMember(4, Name:="relations", DataFormat:=Global.ProtoBuf.DataFormat.Default)> _
		Public ReadOnly Property relations As Global.System.Collections.Generic.List(Of Relation)
		
		Get
			Return _relations
		End Get
		
	End Property
  
    Private ReadOnly _changesets as Global.System.Collections.Generic.List(Of ChangeSet) = New Global.System.Collections.Generic.List(Of ChangeSet)()
		
	<Global.ProtoBuf.ProtoMember(5, Name:="changesets", DataFormat:=Global.ProtoBuf.DataFormat.Default)> _
		Public ReadOnly Property changesets As Global.System.Collections.Generic.List(Of ChangeSet)
		
		Get
			Return _changesets
		End Get
		
	End Property
  
    Private extensionObject As Global.ProtoBuf.IExtension
		Function GetExtensionObject(createIfMissing As Boolean) As Global.ProtoBuf.IExtension Implements Global.ProtoBuf.IExtensible.GetExtensionObject
			Return Global.ProtoBuf.Extensible.GetExtensionObject(extensionObject, createIfMissing)
		End Function
End Class
  
<Global.System.Serializable, Global.ProtoBuf.ProtoContract(Name:="StringTable")> _
Public Partial Class StringTable
    implements Global.ProtoBuf.IExtensible
	
	Public Sub New
	End Sub
    
    Private ReadOnly _s as Global.System.Collections.Generic.List(Of Byte()) = New Global.System.Collections.Generic.List(Of Byte())()
		
	<Global.ProtoBuf.ProtoMember(1, Name:="s", DataFormat:=Global.ProtoBuf.DataFormat.Default)> _
		Public ReadOnly Property s As Global.System.Collections.Generic.List(Of Byte())
		
		Get
			Return _s
		End Get
		
	End Property
  
    Private extensionObject As Global.ProtoBuf.IExtension
		Function GetExtensionObject(createIfMissing As Boolean) As Global.ProtoBuf.IExtension Implements Global.ProtoBuf.IExtensible.GetExtensionObject
			Return Global.ProtoBuf.Extensible.GetExtensionObject(extensionObject, createIfMissing)
		End Function
End Class
  
<Global.System.Serializable, Global.ProtoBuf.ProtoContract(Name:="Info")> _
Public Partial Class Info
    implements Global.ProtoBuf.IExtensible
	
	Public Sub New
	End Sub
    
    Private _version As Integer =CType(-1, Integer)
    <Global.ProtoBuf.ProtoMember(1, IsRequired:=False, Name:="version", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _ 
    <Global.System.ComponentModel.DefaultValue(CType(CType(-1, Integer), Integer))> _ 
    Public Property version As Integer
		Get 
			Return _version
		End Get
	
		Set(value As Integer)
		_version = value 
			
		End Set
	End Property
    
    Private _timestamp As Integer =0
    <Global.ProtoBuf.ProtoMember(2, IsRequired:=False, Name:="timestamp", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _ 
    <Global.System.ComponentModel.DefaultValue(CType(0, Integer))> _ 
    Public Property timestamp As Integer
		Get 
			Return _timestamp
		End Get
	
		Set(value As Integer)
		_timestamp = value 
			
		End Set
	End Property
    
    Private _changeset As Long =0L
    <Global.ProtoBuf.ProtoMember(3, IsRequired:=False, Name:="changeset", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _ 
    <Global.System.ComponentModel.DefaultValue(CType(0L, Long))> _ 
    Public Property changeset_Property As Long
		Get 
			Return _changeset
		End Get
	
		Set(value As Long)
		_changeset = value 
			
		End Set
	End Property
    
    Private _uid As Integer =0
    <Global.ProtoBuf.ProtoMember(4, IsRequired:=False, Name:="uid", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _ 
    <Global.System.ComponentModel.DefaultValue(CType(0, Integer))> _ 
    Public Property uid As Integer
		Get 
			Return _uid
		End Get
	
		Set(value As Integer)
		_uid = value 
			
		End Set
	End Property
    
    Private _user_sid As Integer =0
    <Global.ProtoBuf.ProtoMember(5, IsRequired:=False, Name:="user_sid", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _ 
    <Global.System.ComponentModel.DefaultValue(CType(0, Integer))> _ 
    Public Property user_sid As Integer
		Get 
			Return _user_sid
		End Get
	
		Set(value As Integer)
		_user_sid = value 
			
		End Set
	End Property
    
    Private extensionObject As Global.ProtoBuf.IExtension
		Function GetExtensionObject(createIfMissing As Boolean) As Global.ProtoBuf.IExtension Implements Global.ProtoBuf.IExtensible.GetExtensionObject
			Return Global.ProtoBuf.Extensible.GetExtensionObject(extensionObject, createIfMissing)
		End Function
End Class
  
<Global.System.Serializable, Global.ProtoBuf.ProtoContract(Name:="DenseInfo")> _
Public Partial Class DenseInfo
    implements Global.ProtoBuf.IExtensible
	
	Public Sub New
	End Sub
    
    Private ReadOnly _version as Global.System.Collections.Generic.List(Of Integer) = New Global.System.Collections.Generic.List(Of Integer)()
		
	<Global.ProtoBuf.ProtoMember(1, Name:="version", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _
		Public ReadOnly Property version As Global.System.Collections.Generic.List(Of Integer)
		
		Get
			Return _version
		End Get
		
	End Property
  
    Private ReadOnly _timestamp as Global.System.Collections.Generic.List(Of Long) = New Global.System.Collections.Generic.List(Of Long)()
		
	<Global.ProtoBuf.ProtoMember(2, Name:="timestamp", DataFormat:=Global.ProtoBuf.DataFormat.ZigZag)> _
		Public ReadOnly Property timestamp As Global.System.Collections.Generic.List(Of Long)
		
		Get
			Return _timestamp
		End Get
		
	End Property
  
    Private ReadOnly _changeset as Global.System.Collections.Generic.List(Of Long) = New Global.System.Collections.Generic.List(Of Long)()
		
	<Global.ProtoBuf.ProtoMember(3, Name:="changeset", DataFormat:=Global.ProtoBuf.DataFormat.ZigZag)> _
		Public ReadOnly Property changeset As Global.System.Collections.Generic.List(Of Long)
		
		Get
			Return _changeset
		End Get
		
	End Property
  
    Private ReadOnly _uid as Global.System.Collections.Generic.List(Of Integer) = New Global.System.Collections.Generic.List(Of Integer)()
		
	<Global.ProtoBuf.ProtoMember(4, Name:="uid", DataFormat:=Global.ProtoBuf.DataFormat.ZigZag)> _
		Public ReadOnly Property uid As Global.System.Collections.Generic.List(Of Integer)
		
		Get
			Return _uid
		End Get
		
	End Property
  
    Private ReadOnly _user_sid as Global.System.Collections.Generic.List(Of Integer) = New Global.System.Collections.Generic.List(Of Integer)()
		
	<Global.ProtoBuf.ProtoMember(5, Name:="user_sid", DataFormat:=Global.ProtoBuf.DataFormat.ZigZag)> _
		Public ReadOnly Property user_sid As Global.System.Collections.Generic.List(Of Integer)
		
		Get
			Return _user_sid
		End Get
		
	End Property
  
    Private extensionObject As Global.ProtoBuf.IExtension
		Function GetExtensionObject(createIfMissing As Boolean) As Global.ProtoBuf.IExtension Implements Global.ProtoBuf.IExtensible.GetExtensionObject
			Return Global.ProtoBuf.Extensible.GetExtensionObject(extensionObject, createIfMissing)
		End Function
End Class
  
<Global.System.Serializable, Global.ProtoBuf.ProtoContract(Name:="ChangeSet")> _
Public Partial Class ChangeSet
    implements Global.ProtoBuf.IExtensible
	
	Public Sub New
	End Sub
    
    Private _id As Long
    <Global.ProtoBuf.ProtoMember(1, IsRequired:=True, Name:="id", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _ 
    Public Property id As Long
		Get 
			Return _id
		End Get
	
		Set(value As Long)
		_id = value 
			
		End Set
	End Property
    
    Private ReadOnly _keys as Global.System.Collections.Generic.List(Of UInteger) = New Global.System.Collections.Generic.List(Of UInteger)()
		
	<Global.ProtoBuf.ProtoMember(2, Name:="keys", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _
		Public ReadOnly Property keys As Global.System.Collections.Generic.List(Of UInteger)
		
		Get
			Return _keys
		End Get
		
	End Property
  
    Private ReadOnly _vals as Global.System.Collections.Generic.List(Of UInteger) = New Global.System.Collections.Generic.List(Of UInteger)()
		
	<Global.ProtoBuf.ProtoMember(3, Name:="vals", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _
		Public ReadOnly Property vals As Global.System.Collections.Generic.List(Of UInteger)
		
		Get
			Return _vals
		End Get
		
	End Property
  
    Private _info As Info =Nothing
    <Global.ProtoBuf.ProtoMember(4, IsRequired:=False, Name:="info", DataFormat:=Global.ProtoBuf.DataFormat.Default)> _ 
    <Global.System.ComponentModel.DefaultValue(CType(Nothing, Info))> _ 
    Public Property info_Property As Info
		Get 
			Return _info
		End Get
	
		Set(value As Info)
		_info = value 
			
		End Set
	End Property
    
    Private _created_at As Long
    <Global.ProtoBuf.ProtoMember(8, IsRequired:=True, Name:="created_at", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _ 
    Public Property created_at As Long
		Get 
			Return _created_at
		End Get
	
		Set(value As Long)
		_created_at = value 
			
		End Set
	End Property
    
    Private _closetime_delta As Long =0L
    <Global.ProtoBuf.ProtoMember(9, IsRequired:=False, Name:="closetime_delta", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _ 
    <Global.System.ComponentModel.DefaultValue(CType(0L, Long))> _ 
    Public Property closetime_delta As Long
		Get 
			Return _closetime_delta
		End Get
	
		Set(value As Long)
		_closetime_delta = value 
			
		End Set
	End Property
    
    Private _open As Boolean
    <Global.ProtoBuf.ProtoMember(10, IsRequired:=True, Name:="open", DataFormat:=Global.ProtoBuf.DataFormat.Default)> _ 
    Public Property open As Boolean
		Get 
			Return _open
		End Get
	
		Set(value As Boolean)
		_open = value 
			
		End Set
	End Property
    
    Private _bbox As HeaderBBox =Nothing
    <Global.ProtoBuf.ProtoMember(11, IsRequired:=False, Name:="bbox", DataFormat:=Global.ProtoBuf.DataFormat.Default)> _ 
    <Global.System.ComponentModel.DefaultValue(CType(Nothing, HeaderBBox))> _ 
    Public Property bbox As HeaderBBox
		Get 
			Return _bbox
		End Get
	
		Set(value As HeaderBBox)
		_bbox = value 
			
		End Set
	End Property
    
    Private extensionObject As Global.ProtoBuf.IExtension
		Function GetExtensionObject(createIfMissing As Boolean) As Global.ProtoBuf.IExtension Implements Global.ProtoBuf.IExtensible.GetExtensionObject
			Return Global.ProtoBuf.Extensible.GetExtensionObject(extensionObject, createIfMissing)
		End Function
End Class
  
<Global.System.Serializable, Global.ProtoBuf.ProtoContract(Name:="Node")> _
Public Partial Class Node
    implements Global.ProtoBuf.IExtensible
	
	Public Sub New
	End Sub
    
    Private _id As Long
    <Global.ProtoBuf.ProtoMember(1, IsRequired:=True, Name:="id", DataFormat:=Global.ProtoBuf.DataFormat.ZigZag)> _ 
    Public Property id As Long
		Get 
			Return _id
		End Get
	
		Set(value As Long)
		_id = value 
			
		End Set
	End Property
    
    Private ReadOnly _keys as Global.System.Collections.Generic.List(Of UInteger) = New Global.System.Collections.Generic.List(Of UInteger)()
		
	<Global.ProtoBuf.ProtoMember(2, Name:="keys", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _
		Public ReadOnly Property keys As Global.System.Collections.Generic.List(Of UInteger)
		
		Get
			Return _keys
		End Get
		
	End Property
  
    Private ReadOnly _vals as Global.System.Collections.Generic.List(Of UInteger) = New Global.System.Collections.Generic.List(Of UInteger)()
		
	<Global.ProtoBuf.ProtoMember(3, Name:="vals", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _
		Public ReadOnly Property vals As Global.System.Collections.Generic.List(Of UInteger)
		
		Get
			Return _vals
		End Get
		
	End Property
  
    Private _info As Info =Nothing
    <Global.ProtoBuf.ProtoMember(4, IsRequired:=False, Name:="info", DataFormat:=Global.ProtoBuf.DataFormat.Default)> _ 
    <Global.System.ComponentModel.DefaultValue(CType(Nothing, Info))> _ 
    Public Property info_Property As Info
		Get 
			Return _info
		End Get
	
		Set(value As Info)
		_info = value 
			
		End Set
	End Property
    
    Private _lat As Long
    <Global.ProtoBuf.ProtoMember(8, IsRequired:=True, Name:="lat", DataFormat:=Global.ProtoBuf.DataFormat.ZigZag)> _ 
    Public Property lat As Long
		Get 
			Return _lat
		End Get
	
		Set(value As Long)
		_lat = value 
			
		End Set
	End Property
    
    Private _lon As Long
    <Global.ProtoBuf.ProtoMember(9, IsRequired:=True, Name:="lon", DataFormat:=Global.ProtoBuf.DataFormat.ZigZag)> _ 
    Public Property lon As Long
		Get 
			Return _lon
		End Get
	
		Set(value As Long)
		_lon = value 
			
		End Set
	End Property
    
    Private extensionObject As Global.ProtoBuf.IExtension
		Function GetExtensionObject(createIfMissing As Boolean) As Global.ProtoBuf.IExtension Implements Global.ProtoBuf.IExtensible.GetExtensionObject
			Return Global.ProtoBuf.Extensible.GetExtensionObject(extensionObject, createIfMissing)
		End Function
End Class
  
<Global.System.Serializable, Global.ProtoBuf.ProtoContract(Name:="DenseNodes")> _
Public Partial Class DenseNodes
    implements Global.ProtoBuf.IExtensible
	
	Public Sub New
	End Sub
    
    Private ReadOnly _id as Global.System.Collections.Generic.List(Of Long) = New Global.System.Collections.Generic.List(Of Long)()
		
	<Global.ProtoBuf.ProtoMember(1, Name:="id", DataFormat:=Global.ProtoBuf.DataFormat.ZigZag)> _
		Public ReadOnly Property id As Global.System.Collections.Generic.List(Of Long)
		
		Get
			Return _id
		End Get
		
	End Property
  
    Private _denseinfo As DenseInfo =Nothing
    <Global.ProtoBuf.ProtoMember(5, IsRequired:=False, Name:="denseinfo", DataFormat:=Global.ProtoBuf.DataFormat.Default)> _ 
    <Global.System.ComponentModel.DefaultValue(CType(Nothing, DenseInfo))> _ 
    Public Property denseinfo_Property As DenseInfo
		Get 
			Return _denseinfo
		End Get
	
		Set(value As DenseInfo)
		_denseinfo = value 
			
		End Set
	End Property
    
    Private ReadOnly _lat as Global.System.Collections.Generic.List(Of Long) = New Global.System.Collections.Generic.List(Of Long)()
		
	<Global.ProtoBuf.ProtoMember(8, Name:="lat", DataFormat:=Global.ProtoBuf.DataFormat.ZigZag)> _
		Public ReadOnly Property lat As Global.System.Collections.Generic.List(Of Long)
		
		Get
			Return _lat
		End Get
		
	End Property
  
    Private ReadOnly _lon as Global.System.Collections.Generic.List(Of Long) = New Global.System.Collections.Generic.List(Of Long)()
		
	<Global.ProtoBuf.ProtoMember(9, Name:="lon", DataFormat:=Global.ProtoBuf.DataFormat.ZigZag)> _
		Public ReadOnly Property lon As Global.System.Collections.Generic.List(Of Long)
		
		Get
			Return _lon
		End Get
		
	End Property
  
    Private ReadOnly _keys_vals as Global.System.Collections.Generic.List(Of Integer) = New Global.System.Collections.Generic.List(Of Integer)()
		
	<Global.ProtoBuf.ProtoMember(10, Name:="keys_vals", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _
		Public ReadOnly Property keys_vals As Global.System.Collections.Generic.List(Of Integer)
		
		Get
			Return _keys_vals
		End Get
		
	End Property
  
    Private extensionObject As Global.ProtoBuf.IExtension
		Function GetExtensionObject(createIfMissing As Boolean) As Global.ProtoBuf.IExtension Implements Global.ProtoBuf.IExtensible.GetExtensionObject
			Return Global.ProtoBuf.Extensible.GetExtensionObject(extensionObject, createIfMissing)
		End Function
End Class
  
<Global.System.Serializable, Global.ProtoBuf.ProtoContract(Name:="Way")> _
Public Partial Class Way
    implements Global.ProtoBuf.IExtensible
	
	Public Sub New
	End Sub
    
    Private _id As Long
    <Global.ProtoBuf.ProtoMember(1, IsRequired:=True, Name:="id", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _ 
    Public Property id As Long
		Get 
			Return _id
		End Get
	
		Set(value As Long)
		_id = value 
			
		End Set
	End Property
    
    Private ReadOnly _keys as Global.System.Collections.Generic.List(Of UInteger) = New Global.System.Collections.Generic.List(Of UInteger)()
		
	<Global.ProtoBuf.ProtoMember(2, Name:="keys", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _
		Public ReadOnly Property keys As Global.System.Collections.Generic.List(Of UInteger)
		
		Get
			Return _keys
		End Get
		
	End Property
  
    Private ReadOnly _vals as Global.System.Collections.Generic.List(Of UInteger) = New Global.System.Collections.Generic.List(Of UInteger)()
		
	<Global.ProtoBuf.ProtoMember(3, Name:="vals", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _
		Public ReadOnly Property vals As Global.System.Collections.Generic.List(Of UInteger)
		
		Get
			Return _vals
		End Get
		
	End Property
  
    Private _info As Info =Nothing
    <Global.ProtoBuf.ProtoMember(4, IsRequired:=False, Name:="info", DataFormat:=Global.ProtoBuf.DataFormat.Default)> _ 
    <Global.System.ComponentModel.DefaultValue(CType(Nothing, Info))> _ 
    Public Property info_Property As Info
		Get 
			Return _info
		End Get
	
		Set(value As Info)
		_info = value 
			
		End Set
	End Property
    
    Private ReadOnly _refs as Global.System.Collections.Generic.List(Of Long) = New Global.System.Collections.Generic.List(Of Long)()
		
	<Global.ProtoBuf.ProtoMember(8, Name:="refs", DataFormat:=Global.ProtoBuf.DataFormat.ZigZag)> _
		Public ReadOnly Property refs As Global.System.Collections.Generic.List(Of Long)
		
		Get
			Return _refs
		End Get
		
	End Property
  
    Private extensionObject As Global.ProtoBuf.IExtension
		Function GetExtensionObject(createIfMissing As Boolean) As Global.ProtoBuf.IExtension Implements Global.ProtoBuf.IExtensible.GetExtensionObject
			Return Global.ProtoBuf.Extensible.GetExtensionObject(extensionObject, createIfMissing)
		End Function
End Class
  
<Global.System.Serializable, Global.ProtoBuf.ProtoContract(Name:="Relation")> _
Public Partial Class Relation
    implements Global.ProtoBuf.IExtensible
	
	Public Sub New
	End Sub
    
    Private _id As Long
    <Global.ProtoBuf.ProtoMember(1, IsRequired:=True, Name:="id", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _ 
    Public Property id As Long
		Get 
			Return _id
		End Get
	
		Set(value As Long)
		_id = value 
			
		End Set
	End Property
    
    Private ReadOnly _keys as Global.System.Collections.Generic.List(Of UInteger) = New Global.System.Collections.Generic.List(Of UInteger)()
		
	<Global.ProtoBuf.ProtoMember(2, Name:="keys", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _
		Public ReadOnly Property keys As Global.System.Collections.Generic.List(Of UInteger)
		
		Get
			Return _keys
		End Get
		
	End Property
  
    Private ReadOnly _vals as Global.System.Collections.Generic.List(Of UInteger) = New Global.System.Collections.Generic.List(Of UInteger)()
		
	<Global.ProtoBuf.ProtoMember(3, Name:="vals", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _
		Public ReadOnly Property vals As Global.System.Collections.Generic.List(Of UInteger)
		
		Get
			Return _vals
		End Get
		
	End Property
  
    Private _info As Info =Nothing
    <Global.ProtoBuf.ProtoMember(4, IsRequired:=False, Name:="info", DataFormat:=Global.ProtoBuf.DataFormat.Default)> _ 
    <Global.System.ComponentModel.DefaultValue(CType(Nothing, Info))> _ 
    Public Property info_Property As Info
		Get 
			Return _info
		End Get
	
		Set(value As Info)
		_info = value 
			
		End Set
	End Property
    
    Private ReadOnly _roles_sid as Global.System.Collections.Generic.List(Of Integer) = New Global.System.Collections.Generic.List(Of Integer)()
		
	<Global.ProtoBuf.ProtoMember(8, Name:="roles_sid", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _
		Public ReadOnly Property roles_sid As Global.System.Collections.Generic.List(Of Integer)
		
		Get
			Return _roles_sid
		End Get
		
	End Property
  
    Private ReadOnly _memids as Global.System.Collections.Generic.List(Of Long) = New Global.System.Collections.Generic.List(Of Long)()
		
	<Global.ProtoBuf.ProtoMember(9, Name:="memids", DataFormat:=Global.ProtoBuf.DataFormat.ZigZag)> _
		Public ReadOnly Property memids As Global.System.Collections.Generic.List(Of Long)
		
		Get
			Return _memids
		End Get
		
	End Property
  
    Private ReadOnly _types as Global.System.Collections.Generic.List(Of Relation.MemberType) = New Global.System.Collections.Generic.List(Of Relation.MemberType)()
		
	<Global.ProtoBuf.ProtoMember(10, Name:="types", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _
		Public ReadOnly Property types As Global.System.Collections.Generic.List(Of Relation.MemberType)
		
		Get
			Return _types
		End Get
		
	End Property
  
    Public Enum MemberType 
		NODE = 0 
		WAY = 1 
		RELATION = 2
    End Enum
  
    Private extensionObject As Global.ProtoBuf.IExtension
		Function GetExtensionObject(createIfMissing As Boolean) As Global.ProtoBuf.IExtension Implements Global.ProtoBuf.IExtensible.GetExtensionObject
			Return Global.ProtoBuf.Extensible.GetExtensionObject(extensionObject, createIfMissing)
		End Function
End Class
  
End Namespace