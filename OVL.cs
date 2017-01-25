// Project: OVL Extractor
// File: OVL.cs
//
//  Author: Eishiro Sugata
//  Based off of Joeywp's Cobra Overlay Format Blueprint
using System.Xml;
using System.IO;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Reflection.Emit;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO.Compression;
using System.Xml.Serialization;
using System.Threading;
using System.Text;

namespace OVL_Extractor
{
	[System.Xml.Serialization.XmlRoot]
	public struct BlockCount
	{
		public BlockCount (BinaryReader reader)
		{
			Block1 = reader.ReadUInt32 ();
			Block2 = reader.ReadUInt32 ();
			Block3 = reader.ReadUInt32 ();
			Block4a = reader.ReadUInt16 ();
			Block4b = reader.ReadUInt16 ();
			Block5a = reader.ReadUInt16 ();
			Block5b = reader.ReadUInt16 ();
			Block6a = reader.ReadUInt16 ();
			Block6b = reader.ReadUInt16 ();
			Block7a = reader.ReadUInt16 ();
			Block7b = reader.ReadUInt16 ();
			Block8a = reader.ReadUInt16 ();
			Block8b = reader.ReadUInt16 ();
			Block9a = reader.ReadUInt16 ();
			Block9b = reader.ReadUInt16 ();
			Block10a = reader.ReadUInt16 ();
			Block10b = reader.ReadUInt16 ();
			Unknown1 = reader.ReadUInt32 ();
			CompressedDataSize = reader.ReadUInt32 ();
			DecompressedDataSize = reader.ReadUInt32 ();
			Zero34 = reader.ReadUInt32 ();
			Unknown3 = reader.ReadUInt32 ();
			Header2Size = reader.ReadUInt32 ();
			Unknown5 = reader.ReadUInt32 ();
			Unknown6a = reader.ReadUInt16 ();
			Unknown6b = reader.ReadUInt16 ();
			Unknown7a = reader.ReadUInt16 ();
			Unknown7b = reader.ReadUInt16 ();
		}

		public void Compile (BinaryWriter writer)
		{
			writer.Write (Block1);
			writer.Write (Block2);
			writer.Write (Block3);
			writer.Write (Block4a);
			writer.Write (Block4b);
			writer.Write (Block5a);
			writer.Write (Block5b);
			writer.Write (Block6a);
			writer.Write (Block6b);
			writer.Write (Block7a);
			writer.Write (Block7b);
			writer.Write (Block8a);
			writer.Write (Block8b);
			writer.Write (Block9a);
			writer.Write (Block9b);
			writer.Write (Block10a);
			writer.Write (Block10b);
			writer.Write (Unknown1);
			writer.Write (CompressedDataSize);
			writer.Write (DecompressedDataSize);
			writer.Write (Zero34);
			writer.Write (Unknown3);
			writer.Write (Header2Size);
			writer.Write (Unknown5);
			writer.Write (Unknown6a);
			writer.Write (Unknown6b);
			writer.Write (Unknown7a);
			writer.Write (Unknown7b);

		}

		public uint Block1;
		public uint Block2;
		public uint Block3;
		public ushort Block4a;
		public ushort Block4b;
		public ushort Block5a;
		public ushort Block5b;
		public ushort Block6a;
		public ushort Block6b;
		public ushort Block7a;
		public ushort Block7b;
		public ushort Block8a;
		public ushort Block8b;
		public ushort Block9a;
		public ushort Block9b;
		public ushort Block10a;
		public ushort Block10b;
		public uint Unknown1;
		public uint CompressedDataSize;
		public uint DecompressedDataSize;
		public uint Zero34;
		public uint Unknown3;
		public uint Header2Size;
		public uint Unknown5;
		public ushort Unknown6a;
		public ushort Unknown6b;
		public ushort Unknown7a;
		public ushort Unknown7b;

	}

	[System.Xml.Serialization.XmlRoot]
	public struct ExtraData
	{

		public ExtraData (BinaryReader reader)
		{
			extraData = new char[3072];
			extraData = reader.ReadChars (3072);
		}

		public void Compile (BinaryWriter writer)
		{
			writer.Write (extraData);
		}

		// 3072
		public char[] extraData;
	}

	[System.Xml.Serialization.XmlRoot]
	public struct LoaderSymbol
	{


		public LoaderSymbol (BinaryReader reader)
		{
			StringPointer = reader.ReadUInt32 ();
			Name = string.Empty;
			//Hash = reader.ReadUInt32 ();
			Unknown2a = reader.ReadByte ();
			Unknown2b = reader.ReadByte ();
			Unknown2c = reader.ReadByte ();
			Unknown2d = reader.ReadByte ();
			Type = reader.ReadByte ();

			Unknown3b = reader.ReadByte ();
			Unknown3c = reader.ReadByte ();
			Unknown3d = reader.ReadByte ();

		}

		public void Compile (BinaryWriter writer)
		{
			writer.Write (StringPointer);
			//writer.Write (Hash);
			writer.Write (Unknown2a);
			writer.Write (Unknown2b);
			writer.Write (Unknown2c);
			writer.Write (Unknown2d);
			writer.Write (Type);
			writer.Write (Unknown3b);
			writer.Write (Unknown3c);
			writer.Write (Unknown3d);

		}

		public uint StringPointer;
		public string Name;

		//public uint Hash;

		public byte Unknown2a;
		public byte Unknown2b;
		public byte Unknown2c;
		public byte Unknown2d;

		public byte Type;
		// Type?

		public byte Unknown3b;

		public byte Unknown3c;

		public byte Unknown3d;

	}

	[System.Xml.Serialization.XmlRoot]
	public struct Loader
	{
		public Loader (BinaryReader reader)
		{
			NameStringPointer = reader.ReadUInt32 ();
			Name = string.Empty;
			Unknown1 = reader.ReadUInt32 ();
			//Hash = reader.ReadUInt32 ();
			Unknown2a = reader.ReadByte ();
			Unknown2b = reader.ReadByte ();
			Unknown2c = reader.ReadByte ();
			Unknown2d = reader.ReadByte ();
			LoaderType = reader.ReadUInt32 ();
			StringOffset = reader.ReadUInt32 ();
			SymbolsToResolve = reader.ReadByte ();
			ExtraDataCount = reader.ReadByte ();
			Unknown3 = reader.ReadByte ();
			Unknown4 = reader.ReadByte ();
		}

		public void Compile (BinaryWriter writer)
		{
			writer.Write (NameStringPointer);
			writer.Write (Unknown1);
			//writer.Write (Hash);
			writer.Write (Unknown2a);
			writer.Write (Unknown2b);
			writer.Write (Unknown2c);
			writer.Write (Unknown2d);
			writer.Write (LoaderType);
			writer.Write (StringOffset);
			writer.Write (SymbolsToResolve);
			writer.Write (ExtraDataCount);
			writer.Write (Unknown3);
			writer.Write (Unknown4);

		}

		public uint NameStringPointer;
		public string Name;
		public uint Unknown1;
		//public uint Hash;
		public byte Unknown2a;
		public byte Unknown2b;
		public byte Unknown2c;
		public byte Unknown2d;

		public uint LoaderType;
		public uint StringOffset;
		public byte SymbolsToResolve;
		public byte ExtraDataCount;
		public byte Unknown3;
		public byte Unknown4;
	}

	[XmlRoot ("Header"),XmlType ("OverlayHeader"),Serializable]
	public struct Header
	{


		public Header (BinaryReader reader)
		{
			magicNumber = reader.ReadUInt32 ();
			if (magicNumber != 1397051974) {
				Debug.Assert ((magicNumber != 1397051974), "Error Encountered!", "Magic Number Found is {0} Magic Number Needed is 1397051974", magicNumber);

			}
			VersionOrGame = reader.ReadByte ();
			Unknown1a = reader.ReadByte ();
			BigEndian = reader.ReadByte ();
			Unknown1c = reader.ReadByte ();
			Unknown2a = reader.ReadByte ();
			Unknown2b = reader.ReadByte ();
			Unknown2c = reader.ReadByte ();
			Unknown2d = reader.ReadByte ();
			Unknown3 = reader.ReadUInt32 ();
			StringTableSize = reader.ReadUInt32 ();
			Unknown4 = reader.ReadUInt32 ();
			OtherCount = reader.ReadUInt32 ();
			DirCount = reader.ReadUInt16 ();
			LoaderCount = reader.ReadUInt16 ();
			FileCount1 = reader.ReadUInt32 ();
			FileCount2 = reader.ReadUInt32 ();
			PartCount = reader.ReadUInt32 ();
			ArchiveCount = reader.ReadUInt32 ();
			Unknown11 = reader.ReadUInt32 ();
			Unknown12 = reader.ReadUInt32 ();
			Unknown13 = reader.ReadUInt32 ();
			Unknown14 = reader.ReadUInt32 ();
			UnknownCount = reader.ReadUInt32 ();
			Unknown16a = reader.ReadUInt16 ();
			Unknown16b = reader.ReadUInt16 ();
			Unknown17a = reader.ReadUInt16 ();
			Unknown17b = reader.ReadUInt16 ();
			Unknown18 = reader.ReadUInt32 ();
			StaticTextLength = reader.ReadUInt32 ();
			FileCount3 = reader.ReadUInt32 ();
			TypeNamesLength = reader.ReadUInt32 ();
			Unknown22 = reader.ReadUInt32 ();
			Unknown23 = reader.ReadUInt32 ();
			Unknown24 = reader.ReadUInt32 ();
			Unknown25 = reader.ReadUInt32 ();
			Unknown26 = reader.ReadUInt32 ();
			Unknown27 = reader.ReadUInt32 ();
			Unknown28 = reader.ReadUInt32 ();
			Unknown29 = reader.ReadUInt32 ();
			Unknown30 = reader.ReadUInt32 ();
			Unknown31 = reader.ReadUInt32 ();
			Unknown32 = reader.ReadUInt32 ();
			Unknown33 = reader.ReadUInt32 ();
			Unknown34 = reader.ReadUInt32 ();
		}

		public void Compile (BinaryWriter writer)
		{
			writer.Write (magicNumber);
			writer.Write (VersionOrGame);
			writer.Write (Unknown1a);
			writer.Write (BigEndian);
			writer.Write (Unknown1c);
			writer.Write (Unknown2a);
			writer.Write (Unknown2b);
			writer.Write (Unknown2c);
			writer.Write (Unknown2d);
			writer.Write (Unknown3);
			writer.Write (StringTableSize);
			writer.Write (Unknown4);
			writer.Write (OtherCount);
			writer.Write (DirCount);
			writer.Write (LoaderCount);
			writer.Write (FileCount1);
			writer.Write (FileCount2);
			writer.Write (PartCount);
			writer.Write (ArchiveCount);
			writer.Write (Unknown11);
			writer.Write (Unknown12);
			writer.Write (Unknown13);
			writer.Write (Unknown14);
			writer.Write (UnknownCount);
			writer.Write (Unknown16a);
			writer.Write (Unknown16b);
			writer.Write (Unknown17a);
			writer.Write (Unknown17b);
			writer.Write (Unknown18);
			writer.Write (StaticTextLength);
			writer.Write (FileCount3);
			writer.Write (TypeNamesLength);
			writer.Write (Unknown22);
			writer.Write (Unknown23);
			writer.Write (Unknown24);
			writer.Write (Unknown25);
			writer.Write (Unknown26);
			writer.Write (Unknown27);
			writer.Write (Unknown28);
			writer.Write (Unknown29);
			writer.Write (Unknown30);
			writer.Write (Unknown31);
			writer.Write (Unknown32);
			writer.Write (Unknown33);
			writer.Write (Unknown34);
		}


		public uint magicNumber;
		[System.Xml.Serialization.XmlElement (ElementName = "VersionOrGame")]
		public byte VersionOrGame;
		// Seen 0x0 (0) at Elite Dangerous, 0x2 (2) at Disneyland Kinect, 0x8 (8) at Planet Coaster
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown1a")]
		public byte Unknown1a;
		// Seen 0x12 (18)
		[System.Xml.Serialization.XmlElement (ElementName = "BigEndian")]
		public byte BigEndian;
		// Seen 1 if file uses big-endian, else it uses little-endian
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown1c")]
		public byte Unknown1c;
		// Seen 1
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown2a")]
		public byte Unknown2a;
		// Seen 148
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown2b")]
		public byte Unknown2b;
		// 32
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown2c")]
		public byte Unknown2c;
		// Seen 0
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown2d")]
		public byte Unknown2d;
		// Seen 0
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown3")]
		public uint Unknown3;
		// Seen 0
		[System.Xml.Serialization.XmlElement (ElementName = "StringTableSize")]
		public uint StringTableSize;

		[System.Xml.Serialization.XmlElement (ElementName = "Unknown4")]
		public uint Unknown4;
		// Seen 0
		[System.Xml.Serialization.XmlElement (ElementName = "OtherCount")]
		public uint OtherCount;
		// Seen 0, 1, 9, bitflag? Also seen 3 at ED
		[System.Xml.Serialization.XmlElement (ElementName = "DirCount")]
		public ushort DirCount;
		// Seen 0. Seen more at ED
		[System.Xml.Serialization.XmlElement (ElementName = "LoaderCount")]
		public ushort LoaderCount;
		// Seen 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 11, 29. Also seen 10 at ED
		[System.Xml.Serialization.XmlElement (ElementName = "FileCount1")]
		public uint FileCount1;
		// Seen values between 0 and 47968, mostly less then 1000
		[System.Xml.Serialization.XmlElement (ElementName = "FileCount2")]
		public uint FileCount2;
		// Same as Unknown7
		[System.Xml.Serialization.XmlElement (ElementName = "PartCount")]
		public uint PartCount;
		// Seen values between 0 and 57331, rarely more then 1000
		[System.Xml.Serialization.XmlElement (ElementName = "ArchiveCount")]
		public uint ArchiveCount;
		// Seen the values 1 to 3, also 4 and 5 at ED
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown11")]
		public uint Unknown11;
		// Seen the values 0 to 6, also 7 and 8 at ED
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown12")]
		public uint Unknown12;
		// Seen numbers between 0 and 2885, mostly less then or equal to 151
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown13")]
		public uint Unknown13;
		// Seen numbers between 0 and 2362, mostly less then or equal to 231
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown14")]
		public uint Unknown14;
		// Seen numbers between 0 and 2739
		[System.Xml.Serialization.XmlElement (ElementName = "UnknownCount")]
		public uint UnknownCount;
		// Seen values between 0 and 38
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown16a")]
		public ushort Unknown16a;
		// Seen 0
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown16b")]
		public ushort Unknown16b;
		// Seen 0
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown17a")]
		public ushort Unknown17a;
		// Seen 0
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown17b")]
		public ushort Unknown17b;
		// Seen 0
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown18")]
		public uint Unknown18;
		// Seen 0
		[System.Xml.Serialization.XmlElement (ElementName = "StaticTextLength")]
		public uint StaticTextLength;
		[System.Xml.Serialization.XmlElement (ElementName = "FileCount3")]
		public uint FileCount3;
		// Seen numbers between 0 and 47968
		[System.Xml.Serialization.XmlElement (ElementName = "TypeNamesLength")]
		public uint TypeNamesLength;
		// Seen numbers between 0 and 1103, mostly lower then 300
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown22")]
		public uint Unknown22;
		// Seen 0
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown23")]
		public uint Unknown23;
		// Seen 0
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown24")]
		public uint Unknown24;
		// Seen 0
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown25")]
		public uint Unknown25;
		// Seen 0
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown26")]
		public uint Unknown26;
		// Seen 0
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown27")]
		public uint Unknown27;
		// Seen 0
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown28")]
		public uint Unknown28;
		// Seen 0
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown29")]
		public uint Unknown29;
		// Seen 0
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown30")]
		public uint Unknown30;
		// Seen 0
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown31")]
		public uint Unknown31;
		// Seen 0
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown32")]
		public uint Unknown32;
		// Seen 0
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown33")]
		public uint Unknown33;
		// Seen 0
		[System.Xml.Serialization.XmlElement (ElementName = "Unknown34")]
		public uint Unknown34;
		// Seen 0
	}

	[System.Xml.Serialization.XmlRoot ("Overlay"),XmlType ("Overlay")]
	public class OVL
	{
		public Header header;
		//[System.Xml.Serialization.XmlElement]
		public Loader[] loaders;
		//[System.Xml.Serialization.XmlElement]
		public List<ExtraData> data = new List<ExtraData> ();
		//[System.Xml.Serialization.XmlElement]
		public List<LoaderSymbol> loaderSymbol = new List<LoaderSymbol> ();
		[System.Xml.Serialization.XmlElement ("static")]
		public char[] Static;
		//[System.Xml.Serialization.XmlElement]
		public BlockCount blockCount;
		//[System.Xml.Serialization.XmlElement]
		public string stringtable;



		//[System.Xml.Serialization.XmlElement]
		byte[] unknown;

		public byte[] Unknown{ get { return unknown; } set { unknown = value; } }

		byte[] compressedData;
		//byte[] uncompressedData;

		public byte[] GetCompressedData{ get { return compressedData; } }

		public OVL ()
		{
		}

		public OVL (BinaryReader reader)
		{
			try {
				header = new Header (reader);
				if (header.BigEndian == 1) {
					return;
				}
				LoadStringTable (reader);
				loaders = new Loader[header.LoaderCount];
				for (int i = 0; i < header.LoaderCount; i++) {
					loaders [i] = new Loader (reader);
				}
				for (int i = 0; i < header.LoaderCount; i++) {
					for (int s = 0; s < loaders [i].ExtraDataCount; s++) {
						if (loaders [i].ExtraDataCount > 0)
							data.Add (new ExtraData (reader));
					}
					for (int j = 0; j < loaders [i].SymbolsToResolve; j++) {
						loaderSymbol.Add (new LoaderSymbol (reader));
					}
				}

				Static = new char[header.StaticTextLength];

				Static = reader.ReadChars ((int)header.StaticTextLength);
				long pos = 0;
				blockCount = new BlockCount (reader);
				/*if (reader.PeekChar () == '\0') {
			while (reader.PeekChar () == '\0') {
				byte b = reader.ReadByte ();
			}
		}
*/
		
				//compressedData = new byte[blockCount.CompressedDataSize];
				//compressedData = reader.ReadBytes ((int)blockCount.CompressedDataSize);
				Console.WriteLine (reader.BaseStream.Position);
				//reader.BaseStream.Position = pos;//reader.BaseStream.Length - blockCount.CompressedDataSize;
				Console.WriteLine (reader.BaseStream.Position);
				//uncompressedData = new byte[(int)blockCount.DecompressedDataSize];
				//Ionic.Zlib.DeflateStream ds = new Ionic.Zlib.DeflateStream (reader.BaseStream, Ionic.Zlib.CompressionMode.Decompress);
				/*DeflateStream ds = new DeflateStream (reader.BaseStream, CompressionMode.Decompress);
		//GZipStream gz = new GZipStream (reader.BaseStream, CompressionMode.Decompress);

		//uncompressedData = Ionic.Zlib.ZlibStream.UncompressBuffer ((compressedData));
		bool success = false;
		bool quit = false;
		while (!success && !quit) {
			try {
				ds.Read (uncompressedData, 0, (int)blockCount.DecompressedDataSize);
				success = true;
			} catch {
				Thread.Sleep (10);
				pos++;
				reader.BaseStream.Position = pos;
				Console.WriteLine (reader.BaseStream.Position);
				if (reader.BaseStream.Position + blockCount.CompressedDataSize + 1 > reader.BaseStream.Length) {
					Console.WriteLine ("Unable to Continue");
					quit = true;
				}

			}
		}*/


				long position = reader.BaseStream.Position;
				reader.BaseStream.Position = reader.BaseStream.Length - blockCount.CompressedDataSize;

				compressedData = new byte[(long)blockCount.CompressedDataSize];

				reader.BaseStream.Read (compressedData, 0, (int)blockCount.CompressedDataSize);

				unknown = new byte[(reader.BaseStream.Length - blockCount.CompressedDataSize) - position];

				reader.BaseStream.Position = position;
				reader.Read (unknown, 0, (int)((reader.BaseStream.Length - blockCount.CompressedDataSize) - position));

				reader.Close ();
				/*char b = (char)uncompressedData [0];
		int x = 0;
		while (b != '\0' && uncompressedData.Length > x) {
			Console.Write (b);
			b = (char)uncompressedData [x];
			x++;
		}*/
			
				for (int i = 0; i < loaders.Length; i++) {
					loaders [i].Name = LoaderComment (loaders [i]);
				}

				for (int i = 0; i < loaderSymbol.Count; i++) {
					var l = loaderSymbol [i];
					l.Name = SymbolComment (loaderSymbol [i]);
					loaderSymbol [i] = l;
				}
				stringtable = stringtable.Replace ('\0', ' ');
			} catch (Exception e) {
				Console.Write (e);
			}

			Console.WriteLine ("File Count = {0}", header.FileCount1);
			Console.WriteLine ("Loader Symbol Count = {0}", loaderSymbol.Count);
			Console.WriteLine ("Archive Count = {0}", header.ArchiveCount);
		}

		public void Compile (BinaryWriter writer)
		{
			header.Compile (writer);

			stringtable = stringtable.Replace (' ', '\0');

			writer.Write (Encoding.ASCII.GetBytes (stringtable));

			for (int i = 0; i < loaders.Length; i++) {
				loaders [i].Compile (writer);
			}

			for (int i = 0; i < loaderSymbol.Count; i++) {
				loaderSymbol [i].Compile (writer);
			}

			writer.Write (Static);

			blockCount.Compile (writer);

			writer.Write (unknown);
		}

		private void LoadStringTable (BinaryReader reader)
		{
			stringtable = string.Empty;//[header.StringTableSize];
			uint i;
			for (i = 0; i < header.StringTableSize; i++) {
				char c = (char)reader.ReadByte ();
				stringtable += c;
			}
		}

		public void Setup ()
		{
			if (header.BigEndian == 1) {
				return;
			}


		}

		public void LittleEndian ()
		{
		}

		private void Serialize (TextWriter writer)
		{
			XmlSerializer serializer = new XmlSerializer (typeof(Header));
			serializer.Serialize (writer, header);
			serializer = new XmlSerializer (typeof(byte[]));
			serializer.Serialize (writer, stringtable);
			serializer = new XmlSerializer (typeof(Loader[]));
			serializer.Serialize (writer, loaders);
			if (data.Count > 0) {
				serializer = new XmlSerializer (typeof(ExtraData[]));
				serializer.Serialize (writer, data.ToArray ());
			}
			if (loaderSymbol.Count > 0) {
				serializer = new XmlSerializer (typeof(LoaderSymbol[]));
				serializer.Serialize (writer, loaderSymbol.ToArray ());
			}
			serializer = new XmlSerializer (typeof(char[]));
			serializer.Serialize (writer, Static);
			serializer = new XmlSerializer (typeof(BlockCount));
			serializer.Serialize (writer, blockCount);

		}

		public string StringPointer (uint pointer)
		{
			/*string[] list = stringtable.Split ('\0');
		if (pointer > list.Length) {
			Console.WriteLine ("Unable to find string: {0} out of {1}", pointer, list.Length);
			return string.Empty;
		}
		string value = list [pointer];

		return value;*/

			if (pointer > stringtable.Length) {
				Console.WriteLine ("Unable to find string: {0} out of {1}", pointer, stringtable.Length);
				return string.Empty;
			}

			int i = (int)pointer;

			string s = string.Empty;

			while (stringtable [i] != '\0') {
				s += stringtable [i];
				i++;
			}

			return s;
		
		}

		public string SymbolComment (LoaderSymbol symbol)
		{
			return StringPointer (symbol.StringPointer);
		}

		public string LoaderComment (Loader symbol)
		{
			return StringPointer (symbol.NameStringPointer);
		}
	}
}

