// Project:  OVL Extractor
// File: Program.cs
//
//  Author: Eishiro Sugata

using System;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;
using System.IO.Ports;
using System.Diagnostics;
using Ionic.Zlib;
using Ionic.Zip;

namespace Ovl_Compiler
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			// Extract
			if (args [0].Contains ((".ovl"))) {
				string path = Path.Combine (Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location), Path.GetFileNameWithoutExtension (args [0]));
				TextWriter writer = File.CreateText (path + ".xml");
				OVL o = new OVL (new BinaryReader (File.OpenRead ((args [0]))));
				try {
					

					XmlSerializer xml = new XmlSerializer (typeof(OVL));


					xml.Serialize (writer, o);
				
				} catch (Exception e) {
					Console.WriteLine (e);
				} finally {
					writer.Close ();
				}

				File.WriteAllBytes (path + ".out", ZlibStream.UncompressBuffer (o.GetCompressedData));
			} else if (args [0].Contains (".zlib") || args [0].Contains (".ovs")) {
				string path = Path.Combine (Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location), Path.GetFileNameWithoutExtension (args [0]));
				if (args [0].Contains (".zlib"))
					File.WriteAllBytes (path + ".out", ZlibStream.UncompressBuffer (File.ReadAllBytes (args [0])));
				else
					File.WriteAllBytes (path + ".ovs.out", ZlibStream.UncompressBuffer (File.ReadAllBytes (args [0])));
				try {

				} catch {

				} finally {

				}
			} else if (args [0].Contains ("-c") && args [1].Contains (".xml")) {
				// Compile
				string path = Path.GetFileNameWithoutExtension (args [1]);
				if (!File.Exists (path + ".out")) {
					Console.WriteLine ("No (.out) file found : Aborting");
					Console.WriteLine ("Press Any Key");
					Console.ReadKey ();
					return;
				}

				XmlSerializer xml = new XmlSerializer (typeof(OVL));
				OVL o = (OVL)xml.Deserialize (File.OpenRead (args [1]));

				byte[] data = File.ReadAllBytes (path + ".out");

				BinaryWriter writer = new BinaryWriter (File.Create (path + ".ovl"));
				o.Compile (writer);
				ZlibStream zlb = new ZlibStream (writer.BaseStream, CompressionMode.Compress);
				zlb.Write (data, 0, data.Length);
				zlb.Close ();
				writer.Close ();

			}
			Console.WriteLine ("Press Any Key");
			Console.ReadKey ();
		}
	}
}
