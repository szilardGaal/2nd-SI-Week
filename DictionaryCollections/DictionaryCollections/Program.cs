using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DictionaryCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            Hashtable hTable = new Hashtable();
            string[] numStr = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            for (int i = 0; i < 10; i++)
            {
                hTable.Add(i.ToString(), numStr[i]);
            }

            string str = "2304535356794";

            foreach (char c in str) {
                string num = c.ToString();
                if (hTable.ContainsKey(num))
                {
                    Console.WriteLine(hTable[num]);
                }
            }

            Console.ReadLine();
        }
    }
}
