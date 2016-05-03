using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using HashTableLib;

namespace HashTableProgram
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			var words = GetWords("test");

			var sw = new Stopwatch();

			sw.Reset();
			sw.Start();
			DictTest(words);
			sw.Stop();
			Console.WriteLine($"Dictionary result: {sw.Elapsed}");

			sw.Reset();
			sw.Start();
			HashTableTest(words);
			sw.Stop();
			Console.WriteLine($"HashTable result: {sw.Elapsed}");

			Console.ReadKey();
		}

		private static string[] GetWords(string filename)
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

			return words.ToArray();
		}

		private static void HashTableTest(IEnumerable<string> words)
		{
			var table = new HashTable<string, int>();
			foreach (var word in words)
			{
				if (table.ContainsKey(word))
				{
					table[word]++;
				}
				else
				{
					table.Add(word, 1);
				}
			}

			var wordsWithSevenLetters = table.Where(word => word.Key.Length == 7).Select(item => item.Key).ToArray();

			foreach (var sevenLetterWord in wordsWithSevenLetters)
			{
				table.Remove(sevenLetterWord);
			}
		}

		private static void DictTest(IEnumerable<string> words)
		{
			var uniqueWords = new Dictionary<string, int>();
			foreach (var word in words)
			{
				if (uniqueWords.ContainsKey(word))
				{
					uniqueWords[word]++;
				}
				else
				{
					uniqueWords.Add(word, 1);
				}
			}
			var wordsWithSevenLetters = uniqueWords.Where(word => word.Key.Length == 7).Select(item => item.Key).ToArray();

			foreach (var sevenLetterWord in wordsWithSevenLetters)
			{
				uniqueWords.Remove(sevenLetterWord);
			}
		}
	}
}