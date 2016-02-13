using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lab1Lib;

namespace Lab1
{
	internal static class Program
	{
		private static void Main()
		{
			Console.WriteLine("Enter file name *.txt");
			var name = Console.ReadLine();

			var words = GetWords(name);

			IFrequencyCounter list = new TimeWatcher(new DictFreq());
			var res = list.GetCount(words);
			PrintResults(res);
			((IDisposable) list).Dispose();

			list = new TimeWatcher(new SortedDictFreq());
			res = list.GetCount(words);
			PrintResults(res);
			((IDisposable) list).Dispose();

			list = new TimeWatcher(new SortedListFreq());
			res = list.GetCount(words);
			PrintResults(res);
			((IDisposable)list).Dispose();

			list = new TimeWatcher(new ListFreq());
			res = list.GetCount(words);
			PrintResults(res);
			((IDisposable) list).Dispose();

			Console.ReadKey();
		}

		private static void PrintResults(FreqResult res)
		{
			Console.WriteLine();
			Console.WriteLine($"Всего найдено {res.Count} уникальных слов, из которых самые частые: ");
			foreach (var keyValuePair in res.TopResults)
			{
				Console.WriteLine($"\t{keyValuePair.Key}\t: {keyValuePair.Value} повторов");
			}
		}

		private static List<string> GetWords(string filename)
		{
			filename = filename + ".txt";

			if (!File.Exists(filename))
				throw new FileNotFoundException(filename);

			var words = new List<string>();
			using (var sr = new StreamReader(filename))
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