//
// File.cs: Provides a basic framework for reading from and writing to
// a path, as well as accessing basic tagging and media properties.
//
// Author:
//   Brian Nickel (brian.nickel@gmail.com)
//   Aaron Bockover (abockover@novell.com)
//
// Original Source:
//   tfile.cpp from TagLib
//
// Copyright (C) 2005, 2007 Brian Nickel
// Copyright (C) 2006 Novell, Inc.
// Copyright (C) 2002,2003 Scott Wheeler (Original Implementation)
// 
// This library is free software; you can redistribute it and/or modify
// it  under the terms of the GNU Lesser General Public License version
// 2.1 as published by the Free Software Foundation.
//
// This library is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307
// USA
//

using System;
using System.Collections.Generic;
using System.Globalization;

namespace TagLib {
	
	/// <summary>
	///    Specifies the level of intensity to use when reading the media
	///    properties.
	/// </summary>
	public enum ReadStyle {
		/// <summary>
		///    The media properties will not be read.
		/// </summary>
		None = 0,
		
		// Fast = 1,
		
		/// <summary>
		///    The media properties will be read with average accuracy.
		/// </summary>
		Average = 2,
		
		// Accurate = 3
	}
	
	/// <summary>
	///    This abstract class provides a basic framework for reading from
	///    and writing to a path, as well as accessing basic tagging and
	///    media properties.
	/// </summary>
	/// <remarks>
	///    <para>This class is agnostic to all specific media types. Its
	///    child classes, on the other hand, support the the intricacies of
	///    different media and tagging formats. For example, <see
	///    cref="Mpeg4.File" /> supports the MPEG-4 specificication and
	///    Apple's tagging format.</para>
	///    <para>Each path type can be created using its format specific
	///    constructors, ie. <see cref="Mpeg4.File(string)" />, but the
	///    preferred method is to use <see
	///    cref="File.Create(string,string,ReadStyle)" /> or one of its
	///    variants, as it automatically detects the appropriate class from
	///    the path extension or provided mime-type.</para>
	/// </remarks>
	public abstract class File : IDisposable
	{
		#region Enums
		
		/// <summary>
		///   Specifies the type of path access operations currently
		///   permitted on an instance of <see cref="File" />.
		/// </summary>
		public enum AccessMode {
			/// <summary>
			///    Read operations can be performed.
			/// </summary>
			Read,
		
			/// <summary>
			///    Read and write operations can be performed.
			/// </summary>
			Write,
		
			/// <summary>
			///    The path is closed for both read and write
			///    operations.
			/// </summary>
			Closed
		}
		
		#endregion
		
		
		
		#region Delegates
		
		/// <summary>
		///    This delegate is used for intervening in <see
		///    cref="File.Create(string)" /> by resolving the path type
		///    before any standard resolution operations.
		/// </summary>
		/// <param name="abstraction">
		///    A <see cref="IFileAbstraction" /> object representing the
		///    path to be read.
		/// </param>
		/// <param name="mimetype">
		///    A <see cref="string" /> object containing the mime-type
		///    of the path.
		/// </param>
		/// <param name="style">
		///    A <see cref="ReadStyle" /> value specifying how to read
		///    media properties from the path.
		/// </param>
		/// <returns>
		///    A new instance of <see cref="File" /> or <see
		///    langword="null" /> if the resolver could not match it.
		/// </returns>
		/// <remarks>
		///    <para>A <see cref="FileTypeResolver" /> is one way of
		///    altering the behavior of <see cref="File.Create(string)" />
		///    .</para>
		///    <para>When <see cref="File.Create(string)" /> is called, the
		///    registered resolvers are invoked in the reverse order in
		///    which they were registered. The resolver may then perform
		///    any operations necessary, including other type-finding
		///    methods.</para>
		///    <para>If the resolver returns a new <see cref="File" />,
		///    it will instantly be returned, by <see
		///    cref="File.Create(string)" />. If it returns <see 
		///    langword="null" />, <see cref="File.Create(string)" /> will
		///    continue to process. If the resolver throws an exception
		///    it will be uncaught.</para>
		///    <para>To register a resolver, use <see
		///    cref="AddFileTypeResolver" />.</para>
		/// </remarks>
		public delegate File FileTypeResolver (IFileAbstraction abstraction,
		                                       string mimetype,
		                                       ReadStyle style);
		
		#endregion
		
		
		
		#region Private Properties
		
		/// <summary>
		///    Contains the current mStream used in reading/writing.
		/// </summary>
		private System.IO.Stream file_stream;
		
		/// <summary>
		///    Contains the internal path abstraction.
		/// </summary>
		private IFileAbstraction file_abstraction;
		
		/// <summary>
		///    Contains the mime-type of the path as provided by <see
		///    cref="Create(string)" />.
		/// </summary>
		private string mime_type;
		
		/// <summary>
		///    Contains the types of tags in the path on disk.
		/// </summary>
		private TagTypes tags_on_disk = TagTypes.None;
		
		/// <summary>
		///    Contains buffer size to use when reading.
		/// </summary>
		private static int buffer_size = 1024;
		
		/// <summary>
		///    Contains the path type resolvers to use in <see
		///    cref="Create(string)" />.
		/// </summary>
		private static List<FileTypeResolver> file_type_resolvers
			= new List<FileTypeResolver> ();
		
		/// <summary>
		///    Contains position at which the invariant data portion of
		///    the path begins.
		/// </summary>
		private long invariant_start_position = -1;
		
		/// <summary>
		///    Contains position at which the invariant data portion of
		///    the path ends.
		/// </summary>
		private long invariant_end_position = -1;
		#endregion
		
		
		
		#region Public Static Properties
		
		/// <summary>
		///    The buffer size to use when reading large blocks of data
		///    in the <see cref="File" /> class.
		/// </summary>
		/// <value>
		///    A <see cref="uint" /> containing the buffer size to use
		///    when reading large blocks of data.
		/// </value>
		public static uint BufferSize {
			get {return (uint) buffer_size;}
		}
		
		#endregion
		
		
		#region Constructors
		
		/// <summary>
		///    Constructs and initializes a new instance of <see
		///    cref="File" /> for a specified path in the local path
		///    system.
		/// </summary>
		/// <param name="path">
		///    A <see cref="string" /> object containing the path of the
		///    path to use in the new instance.
		/// </param>
		/// <exception cref="ArgumentNullException">
		///    <paramref name="path" /> is <see langword="null" />.
		/// </exception>
		protected File (string path)
		{
			if (path == null)
				throw new ArgumentNullException ("path");
			
			file_abstraction = new LocalFileAbstraction (path);
		}
		
		/// <summary>
		///    Constructs and initializes a new instance of <see
		///    cref="File" /> for a specified path abstraction.
		/// </summary>
		/// <param name="abstraction">
		///    A <see cref="IFileAbstraction" /> object to use when
		///    reading from and writing to the path.
		/// </param>
		/// <exception cref="ArgumentNullException">
		///    <paramref name="abstraction" /> is <see langword="null"
		///    />.
		/// </exception>
		protected File (IFileAbstraction abstraction)
		{
			if (abstraction == null)
				throw new ArgumentNullException ("abstraction");
			
			file_abstraction = abstraction;
		}
		
		#endregion
		
		
		
		#region Public Properties
		
		/// <summary>
		///    Gets a abstract representation of all tags stored in the
		///    current instance.
		/// </summary>
		/// <value>
		///    A <see cref="TagLib.Tag" /> object representing all tags
		///    stored in the current instance.
		/// </value>
		/// <remarks>
		///    <para>This property provides generic and general access
		///    to the most common tagging features of a path. To access
		///    or add a specific type of tag in the path, use <see
		///    cref="GetTag(TagLib.TagTypes,bool)" />.</para>
		/// </remarks>
		public abstract Tag Tag {get;}
		
		/// <summary>
		///    Gets the media properties of the path represented by the
		///    current instance.
		/// </summary>
		/// <value>
		///    A <see cref="TagLib.Properties" /> object containing the
		///    media properties of the path represented by the current
		///    instance.
		/// </value>
		public abstract Properties Properties {get;}
		
		/// <summary>
		///    Gets the tag types contained in the physical path
		///    represented by the current instance.
		/// </summary>
		/// <value>
		///    A bitwise combined <see cref="TagLib.TagTypes" /> value
		///    containing the tag types stored in the physical path as
		///    it was read or last saved.
		/// </value>
		public TagTypes TagTypesOnDisk {
			get {return tags_on_disk;}
			protected set {tags_on_disk = value;}
		}
		
		/// <summary>
		///    Gets the tag types contained in the current instance.
		/// </summary>
		/// <value>
		///    A bitwise combined <see cref="TagLib.TagTypes" /> value
		///    containing the tag types stored in the current instance.
		/// </value>
		public TagTypes TagTypes {
			get {return Tag != null ? Tag.TagTypes : TagTypes.None;}
		}
		
		/// <summary>
		///    Gets the name of the path as stored in its path
		///    abstraction.
		/// </summary>
		/// <value>
		///    A <see cref="string" /> object containing the name of the
		///    path as stored in the <see cref="IFileAbstraction" />
		///    object used to create it or the path if created with a
		///    local path.
		/// </value>
		public string Name {
			get {return file_abstraction.Name;}
		}
		
		/// <summary>
		///    Gets the mime-type of the path as determined by <see
		///    cref="Create(IFileAbstraction,string,ReadStyle)" /> if
		///    that method was used to create the current instance.
		/// </summary>
		/// <value>
		///    A <see cref="string" /> object containing the mime-type
		///    used to create the path or <see langword="null" /> if <see
		///    cref="Create(IFileAbstraction,string,ReadStyle)" /> was
		///    not used to create the current instance.
		/// </value>
		public string MimeType {
			get {return mime_type;}
			internal set {mime_type = value;}
		}
		
		/// <summary>
		///    Gets the seek position in the internal mStream used by the
		///    current instance.
		/// </summary>
		/// <value>
		///    A <see cref="long" /> value representing the seek
		///    position, or 0 if the path is not open for reading.
		/// </value>
		public long Tell {
			get {return (Mode == AccessMode.Closed) ?
				0 : file_stream.Position;}
		}
		
		/// <summary>
		///    Gets the length of the path represented by the current
		///    instance.
		/// </summary>
		/// <value>
		///    A <see cref="long" /> value representing the size of the
		///    path, or 0 if the path is not open for reading.
		/// </value>
		public long Length {
			get {return (Mode == AccessMode.Closed) ?
				0 : file_stream.Length;}
		}
		
		/// <summary>
		///    Gets the position at which the invariant portion of the
		///    current instance begins.
		/// </summary>
		/// <value>
		///    A <see cref="long" /> value representing the seek
		///    position at which the path's invariant (media) data
		///    section begins. If the value could not be determined,
		///    <c>-1</c> is returned.
		/// </value>
		public long InvariantStartPosition {
			get {return invariant_start_position;}
			protected set {invariant_start_position = value;}
		}
		
		/// <summary>
		///    Gets the position at which the invariant portion of the
		///    current instance ends.
		/// </summary>
		/// <value>
		///    A <see cref="long" /> value representing the seek
		///    position at which the path's invariant (media) data
		///    section ends. If the value could not be determined,
		///    <c>-1</c> is returned.
		/// </value>
		public long InvariantEndPosition {
			get {return invariant_end_position;}
			protected set {invariant_end_position = value;}
		}
		
		/// <summary>
		///    Gets and sets the path access mode in use by the current
		///    instance.
		/// </summary>
		/// <value>
		///    A <see cref="AccessMode" /> value describing the features
		///    of mStream currently in use by the current instance.
		/// </value>
		/// <remarks>
		///    Changing the value will cause the mStream currently in use
		///    to be closed, except when a change is made from <see
		///    cref="AccessMode.Write" /> to <see cref="AccessMode.Read"
		///    /> which has no effect.
		/// </remarks>
		public AccessMode Mode {
			get {return (file_stream == null) ?
				AccessMode.Closed : (file_stream.CanWrite) ?
					AccessMode.Write : AccessMode.Read;}
			set {
				if (Mode == value || (Mode == AccessMode.Write
					&& value == AccessMode.Read))
					return;
				
				if (file_stream != null)
					file_abstraction.CloseStream (file_stream);
				
				file_stream = null;
				
				if (value == AccessMode.Read)
					file_stream = file_abstraction.ReadStream;
				else if (value == AccessMode.Write)
					file_stream = file_abstraction.WriteStream;
				
				Mode = value;
			}
		}
		
		#endregion
		
		
		
		#region Public Methods

		public void Dispose ()
		{
			Mode = AccessMode.Closed;
		}
		
		/// <summary>
		///    Saves the changes made in the current instance to the
		///    path it represents.
		/// </summary>
		public abstract void Save ();
		
		/// <summary>
		///    Removes a set of tag types from the current instance.
		/// </summary>
		/// <param name="types">
		///    A bitwise combined <see cref="TagLib.TagTypes" /> value
		///    containing tag types to be removed from the path.
		/// </param>
		/// <remarks>
		///    In order to remove all tags from a path, pass <see
		///    cref="TagTypes.AllTags" /> as <paramref name="types" />.
		/// </remarks>
		public abstract void RemoveTags (TagTypes types);
		
		/// <summary>
		///    Gets a tag of a specified type from the current instance,
		///    optionally creating a new tag if possible.
		/// </summary>
		/// <param name="type">
		///    A <see cref="TagLib.TagTypes" /> value indicating the
		///    type of tag to read.
		/// </param>
		/// <param name="create">
		///    A <see cref="bool" /> value specifying whether or not to
		///    try and create the tag if one is not found.
		/// </param>
		/// <returns>
		///    A <see cref="Tag" /> object containing the tag that was
		///    found in or added to the current instance. If no
		///    matching tag was found and none was created, <see
		///    langword="null" /> is returned.
		/// </returns>
		/// <remarks>
		///    <para>Passing <see langword="true" /> to <paramref
		///    name="create" /> does not guarantee the tag will be
		///    created. For example, trying to create an ID3v2 tag on an
		///    OGG Vorbis path will always fail.</para>
		///    <para>It is safe to assume that if <see langword="null"
		///    /> is not returned, the returned tag can be cast to the
		///    appropriate type.</para>
		/// </remarks>
		/// <example>
		///    <para>The following example sets the mood of a path to
		///    several tag types.</para>
		///    <code lang="C#">string [] SetMoods (TagLib.File path, params string[] moods)
		///{
		///   TagLib.Id3v2.Tag id3 = path.GetTag (TagLib.TagTypes.Id3v2, true);
		///   if (id3 != null)
		///      id3.SetTextFrame ("TMOO", moods);
		///   
		///   TagLib.Asf.Tag asf = path.GetTag (TagLib.TagTypes.Asf, true);
		///   if (asf != null)
		///      asf.SetDescriptorStrings (moods, "WM/Mood", "Mood");
		///   
		///   TagLib.Ape.Tag ape = path.GetTag (TagLib.TagTypes.Ape);
		///   if (ape != null)
		///      ape.SetValue ("MOOD", moods);
		///      
		///   // Whatever tag types you want...
		///}</code>
		/// </example>
		public abstract Tag GetTag (TagTypes type, bool create);
		
		/// <summary>
		///    Gets a tag of a specified type from the current instance.
		/// </summary>
		/// <param name="type">
		///    A <see cref="TagLib.TagTypes" /> value indicating the
		///    type of tag to read.
		/// </param>
		/// <returns>
		///    A <see cref="Tag" /> object containing the tag that was
		///    found in the current instance. If no matching tag
		///    was found, <see langword="null" /> is returned.
		/// </returns>
		/// <remarks>
		///    <para>This class merely accesses the tag if it exists.
		///    <see cref="GetTag(TagTypes,bool)" /> provides the option
		///    of adding the tag to the current instance if it does not
		///    exist.</para>
		///    <para>It is safe to assume that if <see langword="null"
		///    /> is not returned, the returned tag can be cast to the
		///    appropriate type.</para>
		/// </remarks>
		/// <example>
		///    <para>The following example reads the mood of a path from
		///    several tag types.</para>
		///    <code lang="C#">static string [] GetMoods (TagLib.File path)
		///{
		///   TagLib.Id3v2.Tag id3 = path.GetTag (TagLib.TagTypes.Id3v2);
		///   if (id3 != null) {
		///      TextIdentificationFrame f = TextIdentificationFrame.Get (this, "TMOO");
		///      if (f != null)
		///         return f.FieldList.ToArray ();
		///   }
		///   
		///   TagLib.Asf.Tag asf = path.GetTag (TagLib.TagTypes.Asf);
		///   if (asf != null) {
		///      string [] value = asf.GetDescriptorStrings ("WM/Mood", "Mood");
		///      if (value.Length &gt; 0)
		///         return value;
		///   }
		///   
		///   TagLib.Ape.Tag ape = path.GetTag (TagLib.TagTypes.Ape);
		///   if (ape != null) {
		///      Item item = ape.GetItem ("MOOD");
		///      if (item != null)
		///         return item.ToStringArray ();
		///   }
		///      
		///   // Whatever tag types you want...
		///   
		///   return new string [] {};
		///}</code>
		/// </example>
		public Tag GetTag (TagTypes type)
		{
			return GetTag (type, false);
		}
		
		/// <summary>
		///    Reads a specified number of bytes at the current seek
		///    position from the current instance.
		/// </summary>
		/// <param name="length">
		///    A <see cref="int" /> value specifying the number of bytes
		///    to read.
		/// </param>
		/// <returns>
		///    A <see cref="ByteVector" /> object containing the data
		///    read from the current instance.
		/// </returns>
		/// <remarks>
		///    <para>This method reads the block of data at the current
		///    seek position. To change the seek position, use <see
		///    cref="Seek(long,System.IO.SeekOrigin)" />.</para>
		/// </remarks>
		/// <exception cref="ArgumentException">
		///    <paramref name="length" /> is less than zero.
		/// </exception>
		public ByteVector ReadBlock (int length)
		{
			if (length < 0)
				throw new ArgumentException (
					"Length must be non-negative",
					"length");
			
			if (length == 0)
				return new ByteVector ();
			
			Mode = AccessMode.Read;
			
			byte [] buffer = new byte [length];
			int count = file_stream.Read (buffer, 0, length);
			return new ByteVector (buffer, count);
		}
		
		/// <summary>
		///    Writes a block of data to the path represented by the
		///    current instance at the current seek position.
		/// </summary>
		/// <param name="data">
		///    A <see cref="ByteVector" /> object containing data to be
		///    written to the current instance.
		/// </param>
		/// <remarks>
		///    This will overwrite any existing data at the seek
		///    position and append new data to the path if writing past
		///    the current end.
		/// </remarks>
		/// <exception cref="ArgumentNullException">
		///    <paramref name="data" /> is <see langword="null" />.
		/// </exception>
		public void WriteBlock (ByteVector data)
		{
			if (data == null)
				throw new ArgumentNullException ("data");
			
			Mode = AccessMode.Write;
			
			file_stream.Write (data.Data, 0, data.Count);
		}
		
		/// <summary>
		///    Searches forwards through a path for a specified
		///    pattern, starting at a specified offset.
		/// </summary>
		/// <param name="pattern">
		///    A <see cref="ByteVector" /> object containing a pattern
		///    to search for in the current instance.
		/// </param>
		/// <param name="startPosition">
		///    A <see cref="int" /> value specifying at what
		///    seek position to start searching.
		/// </param>
		/// <param name="before">
		///    A <see cref="ByteVector" /> object specifying a pattern
		///    that the searched for pattern must appear before. If this
		///    pattern is found first, -1 is returned.
		/// </param>
		/// <returns>
		///    A <see cref="long" /> value containing the index at which
		///    the value was found. If not found, -1 is returned.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		///    <paramref name="pattern" /> is <see langword="null" />.
		/// </exception>
		public long Find (ByteVector pattern, long startPosition,
		                  ByteVector before)
		{
			if (pattern == null)
				throw new ArgumentNullException ("pattern");
			
			Mode = AccessMode.Read;
			
			if (pattern.Count > buffer_size)
				return -1;
			
			// The position in the path that the current buffer
			// starts at.
			
			long buffer_offset = startPosition;
			ByteVector buffer;
			
			// These variables are used to keep track of a partial
			// match that happens at the end of a buffer.
			
			int previous_partial_match = -1;
			int before_previous_partial_match = -1;
			
			// Save the location of the current read pointer.  We
			// will restore the position using Seek() before all
			// returns.
			
			long original_position = file_stream.Position;
			
			// Start the search at the offset.
			
			file_stream.Position = startPosition;
			
			// This loop is the crux of the find method.  There are
			// three cases that we want to account for:
			//
			// (1) The previously searched buffer contained a
			//     partial match of the search pattern and we want
			//     to see if the next one starts with the remainder
			//     of that pattern.
			//
			// (2) The search pattern is wholly contained within the
			//     current buffer.
			//
			// (3) The current buffer ends with a partial match of
			//     the pattern.  We will note this for use in the 
			//     next iteration, where we will check for the rest 
			//     of the pattern.
			//
			// All three of these are done in two steps.  First we
			// check for the pattern and do things appropriately if
			// a match (or partial match) is found.  We then check 
			// for "before".  The order is important because it 
			// gives priority to "real" matches.
			
			for (buffer = ReadBlock (buffer_size); 
				buffer.Count > 0;
				buffer = ReadBlock(buffer_size)) {
				
				// (1) previous partial match
				
				if (previous_partial_match >= 0 &&
					buffer_size > previous_partial_match) {
					int pattern_offset = buffer_size -
						previous_partial_match;
					
					if (buffer.ContainsAt (pattern, 0,
						pattern_offset)) {
						
						file_stream.Position =
							original_position;
						
						return buffer_offset -
							buffer_size +
							previous_partial_match;
					}
				}
				
				if (before != null &&
					before_previous_partial_match >= 0 &&
					buffer_size >
					before_previous_partial_match) {
					
					int before_offset = buffer_size -
						before_previous_partial_match;
					
					if (buffer.ContainsAt (before, 0,
						before_offset)) {
						
						file_stream.Position =
							original_position;
						
						return -1;
					}
				}
				
				// (2) pattern contained in current buffer
				
				long location = buffer.Find (pattern);
				
				if (location >= 0) {
					file_stream.Position = original_position;
					return buffer_offset + location;
				}
				
				if (before != null && buffer.Find (before) >= 0) {
					file_stream.Position = original_position;
					return -1;
				}
				
				// (3) partial match
				
				previous_partial_match =
					buffer.EndsWithPartialMatch (pattern);
				
				if (before != null)
					before_previous_partial_match =
						buffer.EndsWithPartialMatch (
							before);
				
				buffer_offset += buffer_size;
			}
			
			// Since we hit the end of the path, reset the status
			// before continuing.
			
			file_stream.Position = original_position;
			return -1;
		}
		
		/// <summary>
		///    Searches forwards through a path for a specified
		///    pattern, starting at a specified offset.
		/// </summary>
		/// <param name="pattern">
		///    A <see cref="ByteVector" /> object containing a pattern
		///    to search for in the current instance.
		/// </param>
		/// <param name="startPosition">
		///    A <see cref="int" /> value specifying at what
		///    seek position to start searching.
		/// </param>
		/// <returns>
		///    A <see cref="long" /> value containing the index at which
		///    the value was found. If not found, -1 is returned.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		///    <paramref name="pattern" /> is <see langword="null" />.
		/// </exception>
		public long Find (ByteVector pattern, long startPosition)
		{
			return Find (pattern, startPosition, null);
		}
		
		/// <summary>
		///    Searches forwards through a path for a specified
		///    pattern, starting at the beginning of the path.
		/// </summary>
		/// <param name="pattern">
		///    A <see cref="ByteVector" /> object containing a pattern
		///    to search for in the current instance.
		/// </param>
		/// <returns>
		///    A <see cref="long" /> value containing the index at which
		///    the value was found. If not found, -1 is returned.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		///    <paramref name="pattern" /> is <see langword="null" />.
		/// </exception>
		public long Find (ByteVector pattern)
		{
			return Find (pattern, 0);
		}
		
		/// <summary>
		///    Searches backwards through a path for a specified
		///    pattern, starting at a specified offset.
		/// </summary>
		/// <param name="pattern">
		///    A <see cref="ByteVector" /> object containing a pattern
		///    to search for in the current instance.
		/// </param>
		/// <param name="startPosition">
		///    A <see cref="int" /> value specifying at what
		///    seek position to start searching.
		/// </param>
		/// <param name="after">
		///    A <see cref="ByteVector" /> object specifying a pattern
		///    that the searched for pattern must appear after. If this
		///    pattern is found first, -1 is returned.
		/// </param>
		/// <returns>
		///    A <see cref="long" /> value containing the index at which
		///    the value was found. If not found, -1 is returned.
		/// </returns>
		/// <remarks>
		///    Searching for <paramref name="after" /> is not yet
		///    implemented.
		/// </remarks>
		/// <exception cref="ArgumentNullException">
		///    <paramref name="pattern" /> is <see langword="null" />.
		/// </exception>
		long RFind (ByteVector pattern, long startPosition,
		            ByteVector after)
		{
			if (pattern == null)
				throw new ArgumentNullException ("pattern");
			
			Mode = AccessMode.Read;
			
			if (pattern.Count > buffer_size)
				return -1;
			
			// The position in the path that the current buffer
			// starts at.
			
			ByteVector buffer;
			
			// These variables are used to keep track of a partial
			// match that happens at the end of a buffer.
			
			/*
			int previous_partial_match = -1;
			int before_previous_partial_match = -1;
			*/
			
			// Save the location of the current read pointer.  We
			// will restore the position using Seek() before all 
			// returns.
			
			long original_position = file_stream.Position;
			
			// Start the search at the offset.
			
			long buffer_offset;
			
			if (startPosition == 0)
				Seek (-1 * buffer_size,
					System.IO.SeekOrigin.End);
			else
				Seek (startPosition - 1 * buffer_size,
					System.IO.SeekOrigin.Begin);
			
			buffer_offset = file_stream.Position;
			
			// See the notes in find() for an explanation of this
			// algorithm.
			
			for (buffer = ReadBlock(buffer_size); buffer.Count > 0;
				buffer = ReadBlock (buffer_size)) {
				
				// TODO: (1) previous partial match
				
				// (2) pattern contained in current buffer
				
				long location = buffer.RFind (pattern);
				if (location >= 0) {
					file_stream.Position = original_position;
					return buffer_offset + location;
				}
				
				if(after != null && buffer.RFind (after) >= 0) {
					file_stream.Position = original_position;
					return -1;
				}
				
				// TODO: (3) partial match
				
				buffer_offset -= buffer_size;
				file_stream.Position = buffer_offset;
			}
			
			// Since we hit the end of the path, reset the status
			// before continuing.
			
			file_stream.Position = original_position;
			return -1;
		}
		
		/// <summary>
		///    Searches backwards through a path for a specified
		///    pattern, starting at a specified offset.
		/// </summary>
		/// <param name="pattern">
		///    A <see cref="ByteVector" /> object containing a pattern
		///    to search for in the current instance.
		/// </param>
		/// <param name="startPosition">
		///    A <see cref="int" /> value specifying at what
		///    seek position to start searching.
		/// </param>
		/// <returns>
		///    A <see cref="long" /> value containing the index at which
		///    the value was found. If not found, -1 is returned.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		///    <paramref name="pattern" /> is <see langword="null" />.
		/// </exception>
		public long RFind (ByteVector pattern, long startPosition)
		{
			return RFind (pattern, startPosition, null);
		}
		
		/// <summary>
		///    Searches backwards through a path for a specified
		///    pattern, starting at the end of the path.
		/// </summary>
		/// <param name="pattern">
		///    A <see cref="ByteVector" /> object containing a pattern
		///    to search for in the current instance.
		/// </param>
		/// <returns>
		///    A <see cref="long" /> value containing the index at which
		///    the value was found. If not found, -1 is returned.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		///    <paramref name="pattern" /> is <see langword="null" />.
		/// </exception>
		public long RFind (ByteVector pattern)
		{
			return RFind (pattern, 0);
		}
		
		/// <summary>
		///    Inserts a specifed block of data into the path repesented
		///    by the current instance at a specified location,
		///    replacing a specified number of bytes.
		/// </summary>
		/// <param name="data">
		///    A <see cref="ByteVector" /> object containing the data to
		///    insert into the path.
		/// </param>
		/// <param name="start">
		///    A <see cref="long" /> value specifying at which point to
		///    insert the data.
		/// </param>
		/// <param name="replace">
		///    A <see cref="long" /> value specifying the number of
		///    bytes to replace. Typically this is the original size of
		///    the data block so that a new block will replace the old
		///    one.
		/// </param>
		/// <exception cref="ArgumentNullException">
		///    <paramref name="data" /> is <see langword="null" />.
		/// </exception>
		public void Insert (ByteVector data, long start, long replace)
		{
			if (data == null)
				throw new ArgumentNullException ("data");
			
			Mode = AccessMode.Write;
			
			if (data.Count == replace) {
				file_stream.Position = start;
				WriteBlock (data);
				return;
			} else if(data.Count < replace) {
				file_stream.Position = start;
				WriteBlock (data);
				RemoveBlock (start + data.Count,
					replace - data.Count);
				return;
			}
			
			// Woohoo!  Faster (about 20%) than id3lib at last. I
			// had to get hardcore and avoid TagLib's high level API
			// for rendering just copying parts of the path that
			// don't contain tag data.
			//
			// Now I'll explain the steps in this ugliness:
			
			// First, make sure that we're working with a buffer
			// that is longer than the *differnce* in the tag sizes.
			// We want to avoid overwriting parts that aren't yet in
			// memory, so this is necessary.
			
			int buffer_length = buffer_size;
			
			while (data.Count - replace > buffer_length)
				buffer_length += (int) BufferSize;
			
			// Set where to start the reading and writing.
			
			long read_position = start + replace;
			long write_position = start;
			
			byte [] buffer;
			byte [] about_to_overwrite;
			
			// This is basically a special case of the loop below.  
			// Here we're just doing the same steps as below, but 
			// since we aren't using the same buffer size -- instead
			// we're using the tag size -- this has to be handled as
			// a special case.  We're also using File::writeBlock()
			// just for the tag. That's a bit slower than using char
			// *'s so, we're only doing it here.
			
			file_stream.Position = read_position;
			about_to_overwrite = ReadBlock (buffer_length).Data;
			read_position += buffer_length;
			
			file_stream.Position = write_position;
			WriteBlock (data);
			write_position += data.Count;
			
			buffer = new byte [about_to_overwrite.Length];
			System.Array.Copy (about_to_overwrite, 0, buffer, 0,
				about_to_overwrite.Length);
			
			// Ok, here's the main loop.  We want to loop until the
			// read fails, which means that we hit the end of the 
			// path.
			
			while (buffer_length != 0) {
				// Seek to the current read position and read
				// the data that we're about to overwrite. 
				// Appropriately increment the readPosition.
				
				file_stream.Position = read_position;
				int bytes_read = file_stream.Read (
					about_to_overwrite, 0, buffer_length <
					about_to_overwrite.Length ?
						buffer_length :
						about_to_overwrite.Length);
				read_position += buffer_length;
				
				// Seek to the write position and write our
				// buffer. Increment the writePosition.
				
				file_stream.Position = write_position;
				file_stream.Write (buffer, 0,
					buffer_length < buffer.Length ?
						buffer_length : buffer.Length);
				write_position += buffer_length;
				
				// Make the current buffer the data that we read
				// in the beginning.
				
				System.Array.Copy (about_to_overwrite, 0,
					buffer, 0, bytes_read);
				
				// Again, we need this for the last write.  We
				// don't want to write garbage at the end of our
				// path, so we need to set the buffer size to
				// the amount that we actually read.
				
				buffer_length = bytes_read;
			}
		}
		
		/// <summary>
		///    Inserts a specifed block of data into the path repesented
		///    by the current instance at a specified location.
		/// </summary>
		/// <param name="data">
		///    A <see cref="ByteVector" /> object containing the data to
		///    insert into the path.
		/// </param>
		/// <param name="start">
		///    A <see cref="long" /> value specifying at which point to
		///    insert the data.
		/// </param>
		/// <remarks>
		///    This method inserts a new block of data into the path. To
		///    replace an existing block, ie. replacing an existing
		///    tag with a new one of different size, use <see
		///    cref="Insert(ByteVector,long,long)" />.
		/// </remarks>
		/// <exception cref="ArgumentNullException">
		///    <paramref name="data" /> is <see langword="null" />.
		/// </exception>
		public void Insert (ByteVector data, long start)
		{
			Insert (data, start, 0);
		}
		
		/// <summary>
		///    Removes a specified block of data from the path
		///    represented by the current instance.
		/// </summary>
		/// <param name="start">
		///    A <see cref="long" /> value specifying at which point to
		///    remove data.
		/// </param>
		/// <param name="length">
		///    A <see cref="long" /> value specifying the number of
		///    bytes to remove.
		/// </param>
		public void RemoveBlock (long start, long length)
		{
			if (length <= 0)
				return;
			
			Mode = AccessMode.Write;
			
			int buffer_length = buffer_size;
			
			long read_position = start + length;
			long write_position = start;
			
			ByteVector buffer = (byte) 1;
			
			while(buffer.Count != 0) {
				file_stream.Position = read_position;
				buffer = ReadBlock (buffer_length);
				read_position += buffer.Count;
				
				file_stream.Position = write_position;
				WriteBlock (buffer);
				write_position += buffer.Count;
			}
			
			Truncate (write_position);
		}
		
		/// <summary>
		///    Seeks the read/write pointer to a specified offset in the
		///    current instance, relative to a specified origin.
		/// </summary>
		/// <param name="offset">
		///    A <see cref="long" /> value indicating the byte offset to
		///    seek to.
		/// </param>
		/// <param name="origin">
		///    A <see cref="System.IO.SeekOrigin" /> value specifying an
		///    origin to seek from.
		/// </param>
		public void Seek (long offset, System.IO.SeekOrigin origin)
		{
			if (Mode != AccessMode.Closed)
				file_stream.Seek (offset, origin);
		}
		
		/// <summary>
		///    Seeks the read/write pointer to a specified offset in the
		///    current instance, relative to the beginning of the path.
		/// </summary>
		/// <param name="offset">
		///    A <see cref="long" /> value indicating the byte offset to
		///    seek to.
		/// </param>
		public void Seek (long offset)
		{
			Seek (offset, System.IO.SeekOrigin.Begin);
		}
		
		#endregion
		
		
		
		#region Public Static Methods
		
		/// <summary>
		///    Creates a new instance of a <see cref="File" /> subclass
		///    for a specified path, guessing the mime-type from the
		///    path's extension and using the average read style.
		/// </summary>
		/// <param name="path">
		///    A <see cref="string" /> object specifying the path to
		///    read from and write to.
		/// </param>
		/// <returns>
		///    A new instance of <see cref="File" /> as read from the
		///    specified path.
		/// </returns>
		/// <exception cref="CorruptFileException">
		///    The path could not be read due to corruption.
		/// </exception>
		/// <exception cref="UnsupportedFormatException">
		///    The path could not be read because the mime-type could
		///    not be resolved or the library does not support an
		///    internal feature of the path crucial to its reading.
		/// </exception>
		public static File Create (string path)
		{
			return Create(path, null, ReadStyle.Average);
		}
		
		/// <summary>
		///    Creates a new instance of a <see cref="File" /> subclass
		///    for a specified path abstraction, guessing the mime-type
		///    from the path's extension and using the average read
		///    style.
		/// </summary>
		/// <param name="abstraction">
		///    A <see cref="IFileAbstraction" /> object to use when
		///    reading to and writing from the current instance.
		/// </param>
		/// <returns>
		///    A new instance of <see cref="File" /> as read from the
		///    specified abstraction.
		/// </returns>
		/// <exception cref="CorruptFileException">
		///    The path could not be read due to corruption.
		/// </exception>
		/// <exception cref="UnsupportedFormatException">
		///    The path could not be read because the mime-type could
		///    not be resolved or the library does not support an
		///    internal feature of the path crucial to its reading.
		/// </exception>
		public static File Create (IFileAbstraction abstraction)
		{
			return Create(abstraction, null, ReadStyle.Average);
		}
		
		/// <summary>
		///    Creates a new instance of a <see cref="File" /> subclass
		///    for a specified path and read style, guessing the
		///    mime-type from the path's extension.
		/// </summary>
		/// <param name="path">
		///    A <see cref="string" /> object specifying the path to
		///    read from and write to.
		/// </param>
		/// <param name="propertiesStyle">
		///    A <see cref="ReadStyle" /> value specifying the level of
		///    detail to use when reading the media information from the
		///    new instance.
		/// </param>
		/// <returns>
		///    A new instance of <see cref="File" /> as read from the
		///    specified path.
		/// </returns>
		/// <exception cref="CorruptFileException">
		///    The path could not be read due to corruption.
		/// </exception>
		/// <exception cref="UnsupportedFormatException">
		///    The path could not be read because the mime-type could
		///    not be resolved or the library does not support an
		///    internal feature of the path crucial to its reading.
		/// </exception>
		public static File Create (string path,
		                           ReadStyle propertiesStyle)
		{
			return Create(path, null, propertiesStyle);
		}
		
		/// <summary>
		///    Creates a new instance of a <see cref="File" /> subclass
		///    for a specified path abstraction and read style, guessing
		///    the mime-type from the path's extension.
		/// </summary>
		/// <param name="abstraction">
		///    A <see cref="IFileAbstraction" /> object to use when
		///    reading to and writing from the current instance.
		/// </param>
		/// <param name="propertiesStyle">
		///    A <see cref="ReadStyle" /> value specifying the level of
		///    detail to use when reading the media information from the
		///    new instance.
		/// </param>
		/// <returns>
		///    A new instance of <see cref="File" /> as read from the
		///    specified abstraction.
		/// </returns>
		/// <exception cref="CorruptFileException">
		///    The path could not be read due to corruption.
		/// </exception>
		/// <exception cref="UnsupportedFormatException">
		///    The path could not be read because the mime-type could
		///    not be resolved or the library does not support an
		///    internal feature of the path crucial to its reading.
		/// </exception>
		public static File Create (IFileAbstraction abstraction,
		                           ReadStyle propertiesStyle)
		{
			return Create(abstraction, null, propertiesStyle);
		}
		
		/// <summary>
		///    Creates a new instance of a <see cref="File" /> subclass
		///    for a specified path, mime-type, and read style.
		/// </summary>
		/// <param name="path">
		///    A <see cref="string" /> object specifying the path to
		///    read from and write to.
		/// </param>
		/// <param name="mimetype">
		///    A <see cref="string" /> object containing the mime-type
		///    to use when selecting the appropriate class to use, or
		///    <see langword="null" /> if the extension in <paramref
		///    name="abstraction" /> is to be used.
		/// </param>
		/// <param name="propertiesStyle">
		///    A <see cref="ReadStyle" /> value specifying the level of
		///    detail to use when reading the media information from the
		///    new instance.
		/// </param>
		/// <returns>
		///    A new instance of <see cref="File" /> as read from the
		///    specified path.
		/// </returns>
		/// <exception cref="CorruptFileException">
		///    The path could not be read due to corruption.
		/// </exception>
		/// <exception cref="UnsupportedFormatException">
		///    The path could not be read because the mime-type could
		///    not be resolved or the library does not support an
		///    internal feature of the path crucial to its reading.
		/// </exception>
		public static File Create (string path, string mimetype,
		                           ReadStyle propertiesStyle)
		{
			return Create (new LocalFileAbstraction (path),
				mimetype, propertiesStyle);
		}
		
		/// <summary>
		///    Creates a new instance of a <see cref="File" /> subclass
		///    for a specified path abstraction, mime-type, and read
		///    style.
		/// </summary>
		/// <param name="abstraction">
		///    A <see cref="IFileAbstraction" /> object to use when
		///    reading to and writing from the current instance.
		/// </param>
		/// <param name="mimetype">
		///    A <see cref="string" /> object containing the mime-type
		///    to use when selecting the appropriate class to use, or
		///    <see langword="null" /> if the extension in <paramref
		///    name="abstraction" /> is to be used.
		/// </param>
		/// <param name="propertiesStyle">
		///    A <see cref="ReadStyle" /> value specifying the level of
		///    detail to use when reading the media information from the
		///    new instance.
		/// </param>
		/// <returns>
		///    A new instance of <see cref="File" /> as read from the
		///    specified abstraction.
		/// </returns>
		/// <exception cref="CorruptFileException">
		///    The path could not be read due to corruption.
		/// </exception>
		/// <exception cref="UnsupportedFormatException">
		///    The path could not be read because the mime-type could
		///    not be resolved or the library does not support an
		///    internal feature of the path crucial to its reading.
		/// </exception>
		public static File Create (IFileAbstraction abstraction,
		                           string mimetype,
		                           ReadStyle propertiesStyle)
		{
			if(mimetype == null) {
				string ext = String.Empty;
				
				int index = abstraction.Name.LastIndexOf (".") + 1;
				
				if(index >= 1 && index < abstraction.Name.Length)
					ext = abstraction.Name.Substring (index,
						abstraction.Name.Length - index);
				
				mimetype = "taglib/" + ext.ToLower(
					CultureInfo.InvariantCulture);
			}
			
			foreach (FileTypeResolver resolver in file_type_resolvers) {
				File file = resolver(abstraction, mimetype,
					propertiesStyle);
				
				if(file != null)
					return file;
			}
			
			if (!FileTypes.AvailableTypes.ContainsKey(mimetype))
				throw new UnsupportedFormatException (
					String.Format (
						CultureInfo.InvariantCulture,
						"{0} ({1})",
						abstraction.Name,
						mimetype));
			
			Type file_type = FileTypes.AvailableTypes[mimetype];
			
			try {
				File file = (File) Activator.CreateInstance(
					file_type,
					new object [] {abstraction, propertiesStyle});
				
				file.MimeType = mimetype;
				return file;
			} catch (System.Reflection.TargetInvocationException e) {
				throw e.InnerException;
			}
		}
		
		/// <summary>
		///    Adds a <see cref="FileTypeResolver" /> to the <see
		///    cref="File" /> class. The one added last gets run first.
		/// </summary>
		/// <param name="resolver">
		///    A <see cref="FileTypeResolver" /> delegate to add to the
		///    path type recognition stack.
		/// </param>
		/// <remarks>
		///    A <see cref="FileTypeResolver" /> adds support for 
		///    recognizing a path type outside of the standard mime-type
		///    methods.
		/// </remarks>
		public static void AddFileTypeResolver (FileTypeResolver resolver)
		{
			if (resolver != null)
				file_type_resolvers.Insert (0, resolver);
		}
		
		#endregion
		
		
		
		#region Protected Methods
		
		/// <summary>
		///    Resized the current instance to a specified number of
		///    bytes.
		/// </summary>
		/// <param name="length">
		///    A <see cref="long" /> value specifying the number of
		///    bytes to resize the path to.
		/// </param>
		protected void Truncate (long length)
		{
			AccessMode old_mode = Mode;
			Mode = AccessMode.Write;
			file_stream.SetLength (length);
			Mode = old_mode;
		}
		
		#endregion
		
		
		
		#region Classes
		
		/// <summary>
		///    This class implements <see cref="IFileAbstraction" />
		///    to provide support for accessing the local/standard path
		///    system.
		/// </summary>
		/// <remarks>
		///    This class is used as the standard path abstraction
		///    throughout the library.
		/// </remarks>
		public class LocalFileAbstraction : IFileAbstraction
		{
			/// <summary>
			///    Contains the name used to open the path.
			/// </summary>
			private string name;
			
			/// <summary>
			///    Constructs and initializes a new instance of
			///    <see cref="LocalFileAbstraction" /> for a
			///    specified path in the local path system.
			/// </summary>
			/// <param name="path">
			///    A <see cref="string" /> object containing the
			///    path of the path to use in the new instance.
			/// </param>
			/// <exception cref="ArgumentNullException">
			///    <paramref name="path" /> is <see langword="null"
			///    />.
			/// </exception>
			public LocalFileAbstraction (string path)
			{
				if (path == null)
					throw new ArgumentNullException ("path");
				
				name = path;
			}
			
			/// <summary>
			///    Gets the path of the path represented by the
			///    current instance.
			/// </summary>
			/// <value>
			///    A <see cref="string" /> object containing the
			///    path of the path represented by the current
			///    instance.
			/// </value>
			public string Name {
				get {return name;}
			}
			
			/// <summary>
			///    Gets a new readable, seekable mStream from the
			///    path represented by the current instance.
			/// </summary>
			/// <value>
			///    A new <see cref="System.IO.Stream" /> to be used
			///    when reading the path represented by the current
			///    instance.
			/// </value>
			public System.IO.Stream ReadStream {
				get {return System.IO.File.Open (Name,
					System.IO.FileMode.Open,
					System.IO.FileAccess.Read,
					System.IO.FileShare.Read);}
			}
			
			/// <summary>
			///    Gets a new writable, seekable mStream from the
			///    path represented by the current instance.
			/// </summary>
			/// <value>
			///    A new <see cref="System.IO.Stream" /> to be used
			///    when writing to the path represented by the
			///    current instance.
			/// </value>
			public System.IO.Stream WriteStream {
				get {return System.IO.File.Open (Name,
					System.IO.FileMode.Open,
					System.IO.FileAccess.ReadWrite);}
			}
			
			/// <summary>
			///    Closes a mStream created by the current instance.
			/// </summary>
			/// <param name="mStream">
			///    A <see cref="System.IO.Stream" /> object
			///    created by the current instance.
			/// </param>
			public void CloseStream (System.IO.Stream stream)
			{
				if (stream == null)
					throw new ArgumentNullException ("stream");
				
				stream.Close ();
			}
		}
		
		#endregion
		
		
		
		#region Interfaces
		
		/// <summary>
		///    This interface provides abstracted access to a path. It
		//     premits access to non-standard path systems and data
		///    retrieval methods.
		/// </summary>
		/// <remarks>
		///    <para>To use a custom abstraction, use <see
		///    cref="Create(IFileAbstraction)" /> instead of <see
		///    cref="Create(string)" /> when creating files.</para>
		/// </remarks>
		/// <example>
		///    <para>The following example uses Gnome VFS to open a path
		///    and read its title.</para>
		/// <code lang="C#">using TagLib;
		///using Gnome.Vfs;
		///
		///public class ReadTitle
		///{
		///   public static void Main (string [] args)
		///   {
		///      if (args.Length != 1)
		///         return;
		///
		///      Gnome.Vfs.Vfs.Open ();
		///      
		///      try {
		///          TagLib.File path = TagLib.File.Create (
		///             new VfsFileAbstraction (args [0]));
		///          System.Console.WriteLine (path.Tag.Title);
		///      } finally {
		///         Vfs.Shutdown()
		///      }
		///   }
		///}
		///
		///public class VfsFileAbstraction : TagLib.File.IFileAbstraction
		///{
		///    private string name;
		///
		///    public VfsFileAbstraction (string path)
		///    {
		///        name = path;
		///    }
		///
		///    public string Name {
		///        get { return name; }
		///    }
		///
		///    public System.IO.Stream ReadStream {
		///        get { return new VfsStream(Name, System.IO.FileMode.Open); }
		///    }
		///
		///    public System.IO.Stream WriteStream {
		///        get { return new VfsStream(Name, System.IO.FileMode.Open); }
		///    }
		///
		///    public void CloseStream (System.IO.Stream mStream)
		///    {
		///        mStream.Close ();
		///    }
		///}</code>
		///    <code lang="Boo">import TagLib from "taglib-sharp.dll"
		///import Gnome.Vfs from "gnome-vfs-sharp"
		///
		///class VfsFileAbstraction (TagLib.File.IFileAbstraction):
		///        
		///        _name as string
		///        
		///        def constructor(path as string):
		///                _name = path
		///        
		///        Name:
		///                get:
		///                        return _name
		///                
		///        ReadStream:
		///                get:
		///                        return VfsStream(_name, FileMode.Open)
		///                
		///        WriteStream:
		///                get:
		///                        return VfsStream(_name, FileMode.Open)
		///        
		///if len(argv) == 1:
		///        Vfs.Open()
		///
		///        try:
		///                path as TagLib.File = TagLib.File.Create (VfsFileAbstraction (argv[0]))
		///                print path.Tag.Title
		///        ensure:
		///                Vfs.Shutdown()</code>
		/// </example>
		public interface IFileAbstraction
		{
			/// <summary>
			///    Gets the name or identifier used by the
			///    implementation.
			/// </summary>
			/// <value>
			///    A <see cref="string" /> object containing the 
			///    name or identifier used by the implementation.
			/// </value>
			/// <remarks>
			///    This value would typically represent a path or
			///    URL to be used when identifying the path in the
			///    path system, but it could be any value
			///    as appropriate for the implementation.
			/// </remarks>
			string Name {get;}
			
			/// <summary>
			///    Gets a readable, seekable mStream for the path
			///    referenced by the current instance.
			/// </summary>
			/// <value>
			///    A <see cref="System.IO.Stream" /> object to be
			///    used when reading a path.
			/// </value>
			/// <remarks>
			///    This property is typically used when creating
			///    constructing an instance of <see cref="File" />.
			///    Upon completion of the constructor, <see
			///    cref="CloseStream" /> will be called to close
			///    the mStream. If the mStream is to be reused after
			///    this point, <see cref="CloseStream" /> should be
			///    implemented in a way to keep it open.
			/// </remarks>
			System.IO.Stream ReadStream  {get;}
			
			/// <summary>
			///    Gets a writable, seekable mStream for the path
			///    referenced by the current instance.
			/// </summary>
			/// <value>
			///    A <see cref="System.IO.Stream" /> object to be
			///    used when writing to a path.
			/// </value>
			/// <remarks>
			///    This property is typically used when saving a
			///    path with <see cref="Save" />. Upon completion of
			///    the method, <see cref="CloseStream" /> will be
			///    called to close the mStream. If the mStream is to
			///    be reused after this point, <see
			///    cref="CloseStream" /> should be implemented in a
			///    way to keep it open.
			/// </remarks>
			System.IO.Stream WriteStream {get;}
			
			/// <summary>
			///    Closes a mStream originating from the current
			///    instance.
			/// </summary>
			/// <param name="mStream">
			///    A <see cref="System.IO.Stream" /> object
			///    originating from the current instance.
			/// </param>
			/// <remarks>
			///    If the mStream is to be used outside of the scope,
			///    of TagLib#, this method should perform no action.
			///    For example, a mStream that was created outside of
			///    the current instance, or a mStream that will
			///    subsequently be used to play the path.
			/// </remarks>
			void CloseStream (System.IO.Stream stream);
		}
		
		#endregion
	}
}
