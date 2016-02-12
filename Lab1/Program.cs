using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApplication1
{
	internal static class Program
	{
		private static void Main()
		{
			var words = GetWords();

			IFrequencyCounter list = new TimeWatcher(new DictFreq());
			var res = list.GetCount(words);
			PrintResults(res);
			((IDisposable) list).Dispose();

			list = new TimeWatcher(new SortedDictFreq());
			res = list.GetCount(words);
			PrintResults(res);
			((IDisposable) list).Dispose();

			list = new TimeWatcher(new ListFreq());
			res = list.GetCount(words);
			PrintResults(res);
			((IDisposable) list).Dispose();

			list = new TimeWatcher(new SortedListFreq());
			res = list.GetCount(words);
			PrintResults(res);
			((IDisposable) list).Dispose();

			Console.ReadKey();
		}

		private static void PrintResults(FreqResult res)
		{
			Console.WriteLine($"{res.Count} : ");
			foreach (var keyValuePair in res.TopResults)
			{
				Console.WriteLine($"{keyValuePair.Key} : {keyValuePair.Value}");
			}
		}

		private static List<string> GetWords()
		{
			var words = new List<string>();
			using (var sr = new StreamReader("anna.txt"))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					line = new string(line.Where(sym => char.IsLetter(sym) || char.IsWhiteSpace(sym)).ToArray()).ToLower();
					var splitedLine = line.Split(new[] {' '},
						StringSplitOptions.RemoveEmptyEntries);
					words.AddRange(splitedLine);
				}
			}

			return words;
		}
	}
}