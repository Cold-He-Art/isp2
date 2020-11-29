using System.IO;
using System.IO.Compression;

namespace CSService
{
    class Acts
    {
        public static string EncryptFile(string path)
        {
            using (FileStream fstream = File.OpenRead(path))
            {
                byte[] fileBytes = new byte[fstream.Length];
                fstream.Read(fileBytes, 0, fileBytes.Length);
                for (int i = 0; i < fileBytes.Length; i++)
                {
                    fileBytes[i] ^= 1;
                }
                string newPath = path.Remove(path.LastIndexOf(".txt")) + ".crypt";
                using (FileStream wrStream = new FileStream(newPath, FileMode.OpenOrCreate))
                {
                    wrStream.Write(fileBytes, 0, fileBytes.Length);
                }
                return newPath;
            }
        }
        public static void DecryptFile(string path)
        {
            using (FileStream fstream = File.OpenRead(path))
            {
                byte[] fileBytes = new byte[fstream.Length];
                fstream.Read(fileBytes, 0, fileBytes.Length);
                for (int i = 0; i < fileBytes.Length; i++)
                {
                    fileBytes[i] ^= 1;
                }
                string newPath = path.Remove(path.LastIndexOf(".crypt")) + ".txt";
                using (FileStream wrStream = new FileStream(newPath, FileMode.OpenOrCreate))
                {
                    wrStream.Write(fileBytes, 0, fileBytes.Length);
                }
            }
        }
        public static string Compress(string FileSource)
        {
            using (FileStream sourceStream = new FileStream(FileSource, FileMode.OpenOrCreate))
            {
                using (FileStream targetStream = File.Create(FileSource.Remove(FileSource.LastIndexOf(".crypt")) + ".gz"))
                {
                    
                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        sourceStream.CopyTo(compressionStream); 
                    }
                }
                return FileSource.Remove(FileSource.LastIndexOf(".crypt")) + ".gz";
            }
        }
        public static void Decompress(string ComprFilePath)
        {
            FileInfo fileInfo = new FileInfo(ComprFilePath);
            using (FileStream sourceStream = new FileStream(ComprFilePath, FileMode.Open))
            {
                string DecomprFilePath = ComprFilePath.Remove(ComprFilePath.LastIndexOf(".gz")) + ".crypt";
                using (FileStream targetStream = File.Create(DecomprFilePath))
                {
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                    }
                }
            }
        }
    }
}