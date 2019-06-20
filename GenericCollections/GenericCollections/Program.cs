using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            var dict = new Dictionary<int, string>();

            dict[36] = "Hungary";
            dict[44] = "United Kingdom";
            dict[33] = "France";
            dict[55] = "Brazil";
            
            foreach (KeyValuePair<int, string> item in dict)
            {
                int code = item.Key;
                string country = item.Value;
                Console.WriteLine("Code {0} = {1}", code, country);
            }
            Console.ReadLine();
        }
    }
}
