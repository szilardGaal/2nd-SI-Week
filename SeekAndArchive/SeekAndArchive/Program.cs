using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.IO.Compression;

namespace SeekAndArchive
{
    class Program
    {
        static List<FileInfo> FoundFiles;
        static List<FileSystemWatcher> Watchers;
        static List<DirectoryInfo> ArchiveDirs;

        static void Main(string[] args)
        {
            string fileName = WildCardToRegular(args[0]);
            string directoryName = Path.GetFullPath(args[1]);

            var rootDir = new DirectoryInfo(directoryName);
            if (!Directory.Exists(directoryName))
            {
                Console.WriteLine("There are no directory named {0}!", directoryName);
                Console.ReadLine();
                return;
            }

            Watchers = new List<FileSystemWatcher>();
            FoundFiles = new List<FileInfo>();

            RecursiveSearch(FoundFiles, fileName, rootDir);

            Console.WriteLine("{0} files found", FoundFiles.Count);

            foreach (FileInfo fi in FoundFiles)
            {
                Console.WriteLine("{0}", fi.FullName);
                FileSystemWatcher newWatcher = new FileSystemWatcher(fi.DirectoryName, fi.Name);
                newWatcher.Changed += new FileSystemEventHandler(WatcherChanged);
                newWatcher.EnableRaisingEvents = true;
                Watchers.Add(newWatcher);
            }

            ArchiveDirs = new List<DirectoryInfo>();
            for (int i = 0; i < FoundFiles.Count; i ++)
            {
                ArchiveDirs.Add(Directory.CreateDirectory("archive" + i.ToString()));
            }

            Console.ReadLine();
        }

        static void RecursiveSearch(List<FileInfo> foundFiles, string fileName, DirectoryInfo currentDirectory)
        {
            foreach (FileInfo fi in currentDirectory.GetFiles())
            {
                if (Regex.IsMatch(fi.Name, fileName))
                {
                    foundFiles.Add(fi);
                }
            }

            foreach (DirectoryInfo dir in currentDirectory.GetDirectories())
            {
                RecursiveSearch(foundFiles, fileName, dir);
            }
        }

        static void WatcherChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                Console.WriteLine("{0} has been changed!", e.FullPath);

                FileSystemWatcher senderWatcher = (FileSystemWatcher)sender;
                int index = Watchers.IndexOf(senderWatcher, 0);
                ArchiveFile(ArchiveDirs[index], FoundFiles[index]);
            }
        }

        static void ArchiveFile(DirectoryInfo archiveDir, FileInfo fileToArcive)
        {
            FileStream input = fileToArcive.OpenRead();
            FileStream output = File.Create(archiveDir.FullName + @"\" + fileToArcive.Name + ".gz");
            GZipStream compressor = new GZipStream(output, CompressionMode.Compress);
            int b = input.ReadByte();

            while (b != -1)
            {
                compressor.WriteByte((byte)b);
                b = input.ReadByte();
            }

            compressor.Close();
            input.Close();
            output.Close();

            Console.WriteLine("{0} has been archived!", fileToArcive.Name);
        }

        private static String WildCardToRegular(String value)
        {
            return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
        }
    }
}
