using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;

namespace LookupCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            var dict = new ListDictionary(new CaseInsensitiveComparer(CultureInfo.InvariantCulture))
            {
                ["Estados Unidos"] = "United States of America",
                ["Canadá"] = "Canada",
                ["España"] = "Spain"
            };

            Console.WriteLine(dict["CANADÁ"]);
            Console.WriteLine(dict["españa"]);
            Console.ReadLine();
        }
    }
}
