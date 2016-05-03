using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkipListLib;

namespace SkipListApp
{
    class Program
    {
        static void Main(string[] args)
        {
	        var skipList = new SkipList<string, int>();

			skipList.Add("test", 123);
			skipList.Add("test1", 321);

	        foreach (var i in skipList)
	        {
		        Console.WriteLine(i);
	        }

	        Console.ReadKey();
        }
    }
}
