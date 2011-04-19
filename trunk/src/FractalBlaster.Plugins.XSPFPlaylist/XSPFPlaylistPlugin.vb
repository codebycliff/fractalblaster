Imports FractalBlaster.Universe
Imports System.Collections.Generic
Imports System.Linq
Imports System.IO
Imports System.Xml
Imports System.Xml.Linq

''' <remarks>
''' Class providing a playlist plugin implementation for the XSPF playlist format.
''' </remarks> 
<PluginAttribute(
    Name:="XSPF Playlist",
    Description:="Reads and writes XSPF playlists",
    Author:="Cliff Braton @ FractalBlasters",
    Version:="0.01beta"
)>
Public Class XSPFPlaylistPlugin
    Implements IPlaylistPlugin

    ''' <summary>
    ''' Gets the supported file extensions.
    ''' </summary>
    Public ReadOnly Property SupportedFileExtensions As IEnumerable(Of String) Implements IPlaylistPlugin.SupportedFileExtensions
        Get
            Return New String() {".xspf"}
        End Get
    End Property

    ''' <summary>
    ''' Reads the file at the specified path and returns a <see cref="Playlist"/>
    ''' instance representing that files contents.
    ''' </summary>
    ''' <param name="path">The path.</param><returns></returns>
    Public Function Read(ByVal path As String) As Playlist Implements IPlaylistPlugin.Read
        Dim playlist As New Playlist

        Dim xmlreader As New XmlTextReader(path)

        Try
            While xmlreader.Read()
                If xmlreader.LocalName = "location" Then
                    xmlreader.Read()
                    Dim value As String
                    value = xmlreader.Value
                    Try
                        xmlreader.Read()
                        playlist.AddItem(New MediaFile(value))
                    Catch ex As Exception
                        Console.WriteLine("Error: Could not read file: {0}", value)
                    End Try
                End If
            End While
        Catch ex As Exception
            Console.WriteLine("Error: Invalid XSPF Playlist: {0}", path)
        End Try
        Return playlist
    End Function

    ''' <summary>
    ''' Writes the specified playlist in XSPF format.
    ''' </summary>
    ''' <param name="playlist">The playlist.</param>
    ''' <param name="path">The path.</param>
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

    ''' <summary>
    ''' Initializes the plugin with the specified application context.
    ''' </summary>
    ''' <param name="context">The context.</param>
    Public Sub Initialize(ByVal context As Universe.AppContext) Implements Universe.IPlugin.Initialize
        MyContext = context
    End Sub

    ''' <summary>
    ''' Private instance variable containing the application context this plugin
    ''' was initialized with.
    ''' </summary>
    Private MyContext As AppContext

End Class

