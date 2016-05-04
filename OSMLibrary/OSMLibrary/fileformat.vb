
      ' Generated from 
' Generated from: fileformat.proto
Namespace OSMPBF
<Global.System.Serializable, Global.ProtoBuf.ProtoContract(Name:="Blob")> _
Public Partial Class Blob
    implements Global.ProtoBuf.IExtensible
	
	Public Sub New
	End Sub
    
    Private _raw As Byte() =Nothing
    <Global.ProtoBuf.ProtoMember(1, IsRequired:=False, Name:="raw", DataFormat:=Global.ProtoBuf.DataFormat.Default)> _ 
    <Global.System.ComponentModel.DefaultValue(CType(Nothing, Byte()))> _ 
    Public Property raw As Byte()
		Get 
			Return _raw
		End Get
	
		Set(value As Byte())
		_raw = value 
			
		End Set
	End Property
    
    Private _raw_size As Integer =0
    <Global.ProtoBuf.ProtoMember(2, IsRequired:=False, Name:="raw_size", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _ 
    <Global.System.ComponentModel.DefaultValue(CType(0, Integer))> _ 
    Public Property raw_size As Integer
		Get 
			Return _raw_size
		End Get
	
		Set(value As Integer)
		_raw_size = value 
			
		End Set
	End Property
    
    Private _zlib_data As Byte() =Nothing
    <Global.ProtoBuf.ProtoMember(3, IsRequired:=False, Name:="zlib_data", DataFormat:=Global.ProtoBuf.DataFormat.Default)> _ 
    <Global.System.ComponentModel.DefaultValue(CType(Nothing, Byte()))> _ 
    Public Property zlib_data As Byte()
		Get 
			Return _zlib_data
		End Get
	
		Set(value As Byte())
		_zlib_data = value 
			
		End Set
	End Property
    
    Private _lzma_data As Byte() =Nothing
    <Global.ProtoBuf.ProtoMember(4, IsRequired:=False, Name:="lzma_data", DataFormat:=Global.ProtoBuf.DataFormat.Default)> _ 
    <Global.System.ComponentModel.DefaultValue(CType(Nothing, Byte()))> _ 
    Public Property lzma_data As Byte()
		Get 
			Return _lzma_data
		End Get
	
		Set(value As Byte())
		_lzma_data = value 
			
		End Set
	End Property
    
    Private _OBSOLETE_bzip2_data As Byte() =Nothing
    <Global.ProtoBuf.ProtoMember(5, IsRequired:=False, Name:="OBSOLETE_bzip2_data", DataFormat:=Global.ProtoBuf.DataFormat.Default)> _ 
    <Global.System.ComponentModel.DefaultValue(CType(Nothing, Byte()))> _ 
    Public Property OBSOLETE_bzip2_data As Byte()
		Get 
			Return _OBSOLETE_bzip2_data
		End Get
	
		Set(value As Byte())
		_OBSOLETE_bzip2_data = value 
			
		End Set
	End Property
    
    Private extensionObject As Global.ProtoBuf.IExtension
		Function GetExtensionObject(createIfMissing As Boolean) As Global.ProtoBuf.IExtension Implements Global.ProtoBuf.IExtensible.GetExtensionObject
			Return Global.ProtoBuf.Extensible.GetExtensionObject(extensionObject, createIfMissing)
		End Function
End Class
  
<Global.System.Serializable, Global.ProtoBuf.ProtoContract(Name:="BlobHeader")> _
Public Partial Class BlobHeader
    implements Global.ProtoBuf.IExtensible
	
	Public Sub New
	End Sub
    
    Private _type As String
    <Global.ProtoBuf.ProtoMember(1, IsRequired:=True, Name:="type", DataFormat:=Global.ProtoBuf.DataFormat.Default)> _ 
    Public Property type As String
		Get 
			Return _type
		End Get
	
		Set(value As String)
		_type = value 
			
		End Set
	End Property
    
    Private _indexdata As Byte() =Nothing
    <Global.ProtoBuf.ProtoMember(2, IsRequired:=False, Name:="indexdata", DataFormat:=Global.ProtoBuf.DataFormat.Default)> _ 
    <Global.System.ComponentModel.DefaultValue(CType(Nothing, Byte()))> _ 
    Public Property indexdata As Byte()
		Get 
			Return _indexdata
		End Get
	
		Set(value As Byte())
		_indexdata = value 
			
		End Set
	End Property
    
    Private _datasize As Integer
    <Global.ProtoBuf.ProtoMember(3, IsRequired:=True, Name:="datasize", DataFormat:=Global.ProtoBuf.DataFormat.TwosComplement)> _ 
    Public Property datasize As Integer
		Get 
			Return _datasize
		End Get
	
		Set(value As Integer)
		_datasize = value 
			
		End Set
	End Property
    
    Private extensionObject As Global.ProtoBuf.IExtension
		Function GetExtensionObject(createIfMissing As Boolean) As Global.ProtoBuf.IExtension Implements Global.ProtoBuf.IExtensible.GetExtensionObject
			Return Global.ProtoBuf.Extensible.GetExtensionObject(extensionObject, createIfMissing)
		End Function
End Class
  
End Namespace