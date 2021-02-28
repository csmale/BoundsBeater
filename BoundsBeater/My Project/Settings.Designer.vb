﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My
    
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0"),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Partial Friend NotInheritable Class MySettings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings()),MySettings)
        
#Region "My.Settings Auto-Save Functionality"
#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(sender As Global.System.Object, e As Global.System.EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If
#End Region
        
        Public Shared ReadOnly Property [Default]() As MySettings
            Get
                
#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("T:\BoundsBeater\OSMCache.osm")>  _
        Public Property OSMCache() As String
            Get
                Return CType(Me("OSMCache"),String)
            End Get
            Set
                Me("OSMCache") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("%USERPROFILE%\Google Drive\BoundsBeater\UKBoundaries.xml")>  _
        Public Property BoundaryXML() As String
            Get
                Return CType(Me("BoundaryXML"),String)
            End Get
            Set
                Me("BoundaryXML") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property ListColumnWidth() As String
            Get
                Return CType(Me("ListColumnWidth"),String)
            End Get
            Set
                Me("ListColumnWidth") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property ListColumnOrder() As String
            Get
                Return CType(Me("ListColumnOrder"),String)
            End Get
            Set
                Me("ListColumnOrder") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property ListColumnSorting() As String
            Get
                Return CType(Me("ListColumnSorting"),String)
            End Get
            Set
                Me("ListColumnSorting") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("http://overpass-api.de/api/xapi")>  _
        Public Property xapiAPI() As String
            Get
                Return CType(Me("xapiAPI"),String)
            End Get
            Set
                Me("xapiAPI") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("300")>  _
        Public Property MaxCacheAge() As Integer
            Get
                Return CType(Me("MaxCacheAge"),Integer)
            End Get
            Set
                Me("MaxCacheAge") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("http://nominatim.openstreetmap.org/search/gb/{0}?format=xml&accept-language=en&fe"& _ 
            "aturetype=city&viewbox={1},{2},{3},{4}&bounded=1")>  _
        Public Property NominatimURL() As String
            Get
                Return CType(Me("NominatimURL"),String)
            End Get
            Set
                Me("NominatimURL") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property frmAnalyze_Splitter1() As Integer
            Get
                Return CType(Me("frmAnalyze_Splitter1"),Integer)
            End Get
            Set
                Me("frmAnalyze_Splitter1") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property frmAnalyze_Splitter2() As Integer
            Get
                Return CType(Me("frmAnalyze_Splitter2"),Integer)
            End Get
            Set
                Me("frmAnalyze_Splitter2") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("C:\Users\Colin\Downloads\Parishes_December_2016_Full_Extent_Boundaries_in_England"& _ 
            "_and_Wales.csv")>  _
        Public Property LatLongFile() As String
            Get
                Return CType(Me("LatLongFile"),String)
            End Get
            Set
                Me("LatLongFile") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("http://ra.osmsurround.org/analyzeRelation?relationId={id}&noCache=true&_noCache=o"& _ 
            "n")>  _
        Public Property AnalyzeUrl() As String
            Get
                Return CType(Me("AnalyzeUrl"),String)
            End Get
            Set
                Me("AnalyzeUrl") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("14")>  _
        Public Property BrowseZoom() As Integer
            Get
                Return CType(Me("BrowseZoom"),Integer)
            End Get
            Set
                Me("BrowseZoom") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property frmAnalyze_MinMax() As Integer
            Get
                Return CType(Me("frmAnalyze_MinMax"),Integer)
            End Get
            Set
                Me("frmAnalyze_MinMax") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("20")>  _
        Public Property InstantSearchCount() As Integer
            Get
                Return CType(Me("InstantSearchCount"),Integer)
            End Get
            Set
                Me("InstantSearchCount") = value
            End Set
        End Property
        
        <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.SpecialSettingAttribute(Global.System.Configuration.SpecialSetting.ConnectionString),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=""C:\VMShare\OS OpenData\Code_Histor"& _ 
            "y_Database_(August_2018)_UK\CodeHistoryDatabase_AUG2018.accdb""")>  _
        Public ReadOnly Property CHDConnectionString() As String
            Get
                Return CType(Me("CHDConnectionString"),String)
            End Get
        End Property
    End Class
End Namespace

Namespace My
    
    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Module MySettingsProperty
        
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")>  _
        Friend ReadOnly Property Settings() As Global.BoundsBeater.My.MySettings
            Get
                Return Global.BoundsBeater.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace
