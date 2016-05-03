using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SkipListLib;

namespace SkipListApp
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var skipList = new SkipList<int, int>();
			var sortedList = new SortedList<int, int>();

			Mesaure(skipList);
			Mesaure(sortedList);

			Console.ReadKey();
		}

		private static void Mesaure(ICollection<KeyValuePair<int, int>> list)
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();

			foreach (var item in Enumerable.Range(0, 10000))
			{
				list.Add(new KeyValuePair<int, int>(item, item));
			}

			foreach (var item in Enumerable.Range(5000, 7000))
			{
				list.Remove(new KeyValuePair<int, int>(item, item));
			}

			foreach (var pair in list)
			{
			}

			stopwatch.Stop();

			Console.WriteLine(list + ":\t\t" + stopwatch.Elapsed);
		}
	}
}