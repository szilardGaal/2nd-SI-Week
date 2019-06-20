using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.IsolatedStorage;
using System.IO;

namespace IsolatedStorageDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IsolatedStorageFile userStore = IsolatedStorageFile.GetUserStoreForAssembly();
            IsolatedStorageFileStream userStream = new IsolatedStorageFileStream("UserSettings.set", System.IO.FileMode.Create, userStore);
            StreamWriter userWriter = new StreamWriter(userStream);
            userWriter.WriteLine("User prefs");
            userWriter.Close();

            string[] Files = userStore.GetFileNames("UserSettings.set");
            if (Files.Length == 0)
            {
                Console.WriteLine("file not found");
                Console.ReadLine();
                return;
            }

            userStream = new IsolatedStorageFileStream("UserSettings.set", FileMode.Open, userStore);
            StreamReader userReader = new StreamReader(userStream);
            string contents = userReader.ReadToEnd();

            Console.WriteLine(contents);
            Console.ReadLine();
        }
    }
}
