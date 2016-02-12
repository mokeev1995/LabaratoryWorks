using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApplication1
{
	public interface IFrequencyCounter
	{
		FreqResult GetCount(IEnumerable<string> words);
	}

	public class TimeWatcher : IFrequencyCounter, IDisposable
	{
		private IFrequencyCounter _counter;
		private DateTime _startTime;
		private DateTime _endTime;
		private bool _disposed = false;

		public TimeWatcher(IFrequencyCounter counter)
		{
			_counter = counter;
		}

		public FreqResult GetCount(IEnumerable<string> words)
		{
			_startTime = DateTime.Now;
			var value = _counter.GetCount(words);
			_endTime = DateTime.Now;
			return value;
		}

		~TimeWatcher()
		{
			Dispose();
		}

		public void Dispose()
		{
			if(_disposed)
				return;
			var time = _endTime - _startTime;
			Console.WriteLine($"{_counter.GetType()} : \t{time.Minutes}:{time.Seconds}:{time.Milliseconds}");
			_disposed = true;
		}
	}

	public class SortedListFreq : IFrequencyCounter
	{
		public FreqResult GetCount(IEnumerable<string> words)
		{
			var uniqueWords = new SortedList<string, int>();
			foreach (var word in words)
			{
				if (uniqueWords.ContainsKey(word.ToLower()))
				{
					uniqueWords[word]++;
				}
				else
				{
					uniqueWords.Add(word.ToLower(), 1);
				}
			}

			var result = uniqueWords.OrderBy(pair => pair.Value);
			return new FreqResult(uniqueWords.Count, result.Take(10));
		}
	}

	public class ListFreq : IFrequencyCounter
	{
		public FreqResult GetCount(IEnumerable<string> words)
		{
			var uniqueWords = new List<KeyValuePair<string, int>>();
			foreach (var word in words)
			{
				if (uniqueWords.Any(wc => wc.Key == word))
				{
					var uniqueWord = uniqueWords.First(wc => wc.Key == word);
					uniqueWords.Remove(uniqueWord);
					uniqueWords.Add(new KeyValuePair<string, int>(uniqueWord.Key, uniqueWord.Value + 1));
				}
				else
				{
					uniqueWords.Add(new KeyValuePair<string, int>(word, 1));
				}
			}

			uniqueWords.Sort((first, second) => first.Value.CompareTo(second.Value));
			return new FreqResult(uniqueWords.Count, uniqueWords.Take(10));
		}
	}

	public class SortedDictFreq : IFrequencyCounter
	{
		public FreqResult GetCount(IEnumerable<string> words)
		{
			var uniqueWords = new SortedDictionary<string, int>();
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

			var result = uniqueWords.OrderByDescending(pair => pair.Value);

			return new FreqResult(uniqueWords.Count, result.Take(10));
		}
	}

	public class DictFreq : IFrequencyCounter
	{
		public FreqResult GetCount(IEnumerable<string> words)
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

			var result = uniqueWords.OrderByDescending(pair => pair.Value);

			return new FreqResult(uniqueWords.Count, result.Take(10));
		}
	}

	static class Program
	{
		static void Main()
		{
			var words = GetWords();

			IFrequencyCounter list;
			FreqResult res;

			list = new TimeWatcher(new DictFreq());
			res = list.GetCount(words);
			Console.WriteLine($"{res.Count} : ");
			foreach (var keyValuePair in res.TopResults)
			{
				Console.WriteLine($"{keyValuePair.Key} : {keyValuePair.Value}");
			}
			((IDisposable) list).Dispose();

			list = new TimeWatcher(new SortedDictFreq());
			res = list.GetCount(words);
			Console.WriteLine($"{res.Count} : ");
			foreach (var keyValuePair in res.TopResults)
			{
				Console.WriteLine($"{keyValuePair.Key} : {keyValuePair.Value}");
			}
			((IDisposable)list).Dispose();

			list = new TimeWatcher(new ListFreq());
			res = list.GetCount(words);
			Console.WriteLine($"{res.Count} : ");
			foreach (var keyValuePair in res.TopResults)
			{
				Console.WriteLine($"{keyValuePair.Key} : {keyValuePair.Value}");
			}
			((IDisposable)list).Dispose();

			list = new TimeWatcher(new SortedListFreq());
			res = list.GetCount(words);
			Console.WriteLine($"{res.Count} : ");
			foreach (var keyValuePair in res.TopResults)
			{
				Console.WriteLine($"{keyValuePair.Key} : {keyValuePair.Value}");
			}
			((IDisposable)list).Dispose();

			Console.ReadKey();
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
					var splitedLine = line.Split(new[] { ' ' },
						StringSplitOptions.RemoveEmptyEntries);
					words.AddRange(splitedLine);
				}
			}

			return words;
		}
	}

}
