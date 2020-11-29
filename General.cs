using System;
using System.IO;

namespace CSService
{
    class General
    {
        public string Source { get; set; }
        public string Target { get; set; }
        public string FileName { get; set; }
        public FileInfo Information { get; set; }

        public General(string source, string target, string fileName)
        {
            Source = source;
            Target = target;
            FileName = fileName;
            Information = new FileInfo(source + fileName);
        }
        public string Creation()
        {
            string year = Information.CreationTime.Year.ToString();
            string month = Information.CreationTime.Month.ToString();
            string day = Information.CreationTime.Day.ToString();
            string path = $"{year}\\{month}\\{day}\\";
            Directory.CreateDirectory(Target + path);
            Directory.CreateDirectory(Target + "tarchive\\" + path);
            return path;
        }
        public void Changes()
        {
          
                string encryptPath = Acts.EncryptFile(Source + FileName);
                string pressPath = Acts.Compress(encryptPath);
                File.Delete(encryptPath);
                string newFileName = $"Sales_{Information.CreationTime.Year}_{Information.CreationTime.Month}_{Information.CreationTime.Day}" +
                    $"_{Information.CreationTime.Hour}_{Information.CreationTime.Minute}_{Information.CreationTime.Second}";
                string genPath = Creation();
                File.Copy(pressPath, Target + genPath + newFileName + ".gz");
                string achivePath = Target + "tarchive\\" + genPath + newFileName;  
                File.Move(pressPath, achivePath + ".gz");
                Acts.Decompress(achivePath + ".gz");
                Acts.DecryptFile(achivePath + ".crypt");
                File.Delete(achivePath + ".gz");
                File.Delete(achivePath + ".crypt");
      
  
        }
     
    }
}

