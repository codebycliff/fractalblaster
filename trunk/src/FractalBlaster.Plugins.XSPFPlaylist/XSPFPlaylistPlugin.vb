Imports FractalBlaster.Universe
Imports System.Collections.Generic
Imports System.Linq
Imports System.IO
Imports System.Xml
Imports System.Xml.Linq

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
<PluginAttribute(Name:="XSPF Playlist", Description:="Reads and writes XSPF playlists")>
Public Class XSPFPlaylistPlugin
    Implements IPlaylistPlugin

    Public ReadOnly Property SupportedFileExtensions As IEnumerable(Of String) Implements IPlaylistPlugin.SupportedFileExtensions
        Get
            Return New String() {".xspf"}
        End Get
    End Property

    Public Function Read(ByVal path As String) As Playlist Implements IPlaylistPlugin.Read
        Dim playlist As New Playlist

        Dim xmlreader As New XmlTextReader(path)
        While xmlreader.Read()
            If xmlreader.LocalName = "location" Then
                xmlreader.Read()
                playlist.AddItem(New MediaFile(xmlreader.Value))
                xmlreader.Read()
            End If
        End While

        Return playlist
    End Function

    Public Sub Write(ByVal playlist As Playlist, ByVal path As String) Implements IPlaylistPlugin.Write

        Dim xspf As XElement = _
            <playlist version="1" xmlns="http://sxpf.org/ns/0/">
                <tracklist>
                    <%=
                        From media In playlist
                        Select <track>
                                   <title>
                                       <%=
                                           From item In media.Metadata
                                           Where item.Name = "Title"
                                           Select item.Name
                                       %>
                                   </title>
                                   <location>
                                       <%=
                                           media.Info.FullName
                                       %>
                                   </location>
                               </track>
                    %>
                </tracklist>
            </playlist>

        Using stream As New StreamWriter(New FileStream(path, FileMode.Create))
            xspf.WriteTo(New XmlTextWriter(stream))
        End Using

    End Sub

    Public Sub Initialize(ByVal context As Universe.AppContext) Implements Universe.IPlugin.Initialize
        MyContext = context
    End Sub

    Private MyContext As AppContext

End Class

