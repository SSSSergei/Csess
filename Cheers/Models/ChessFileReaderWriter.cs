using ChessLogic;
using System;
using System.IO;

namespace Chees.Models
{
    public static class ChessFileReaderWriter
    {
        public static Chess ReadChessFromFile(string pathToFile)
        {
            BinaryReader binaryReader = new BinaryReader(File.Open(pathToFile, FileMode.Open));
            var str = new Chess(binaryReader.ReadString());
            binaryReader.Close();
            return str;
        }

        public static bool WriteChessToFile(string pathToFile, Chess chess)
        {
            File.Delete(pathToFile);
            BinaryWriter binaryWriter = new BinaryWriter(File.Open(pathToFile, FileMode.OpenOrCreate));
            binaryWriter.Write(chess.Data);
            binaryWriter.Close();
            return true;
        }
    }
}
