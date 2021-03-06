﻿using System;
using System.IO;

namespace WorldChunkTool
{
    class Program
    {
        static void Main(string[] args)
        {
            int MagicChunk = 0x00504D43;
            int MagicPKG = 0x20474B50;
            int MagicInputFile;

            string FileInput = "";
            string StrPKGExtraction = "";
            bool FlagPKGExtraction = true;

            Console.WriteLine("WorldChunkTool v1.1 by MHVuze");
            Console.WriteLine("==============================");

            // Display commands
            if (args.Length == 0)
            {                
                Console.WriteLine("Usage: WorldChunkTool <chunkN_file|PKG_file> (PKGext)");
                Console.WriteLine("PKGext: use 'false' to turn off PKG file extraction, defaults to 'true'");
                Console.Read();
                return;
            }

            // Check file
            FileInput = args[0];
            if (!File.Exists(FileInput)) { Console.WriteLine("ERROR: Specified file doesn't exist."); Console.Read(); return; }

            // Turn PKG extraction output on or off
            if (args.Length > 1) StrPKGExtraction = args[1];
            if (StrPKGExtraction.Equals("false", StringComparison.InvariantCultureIgnoreCase)) { FlagPKGExtraction = false; Console.WriteLine("PKG extraction turned off."); }

            // Determine action based on file magic
            using (BinaryReader Reader = new BinaryReader(File.Open(FileInput, FileMode.Open))) MagicInputFile = Reader.ReadInt32();
            if (MagicInputFile == MagicChunk) { Console.WriteLine("Chunk file detected."); Chunk.DecompressChunks(FileInput, FlagPKGExtraction); Console.Read(); }
            else if (MagicInputFile == MagicPKG) { Console.WriteLine("PKG file detected."); PKG.ExtractPKG(FileInput, FlagPKGExtraction); Console.Read(); }
            else { Console.WriteLine($"Invalid magic {MagicInputFile.ToString("X8")}."); Console.Read(); return; }
        }
    }
}
