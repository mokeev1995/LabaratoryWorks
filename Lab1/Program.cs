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

			var types = new IFrequencyCounter[]
			{
				new DictFreq(),
				new SortedDictFreq(),
				new SortedListFreq(),
				new ListFreq()
			};

			foreach (var type in types)
			{
				using (var timer = new TimeWatcher(type))
				{
					var res = timer.GetCount(words);
					PrintResults(res);
				}
			}

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