using System.Collections.Generic;
using System.Linq;

namespace Lab1Lib
{
	public class ListFreq : IFrequencyCounter
	{
		private class WordCountPair<TKey, TValue>
		{
			public TKey Word { get; }
			public TValue Count { get; set; }

			public WordCountPair(TKey word, TValue count)
			{
				Word = word;
				Count = count;
			}
		}

		public FreqResult GetCount(IEnumerable<string> words)
		{
			var uniqueWords = new List<WordCountPair<string, int>>();
			foreach (var word in words)
			{
				var uniqueWord = uniqueWords.FirstOrDefault(wc => wc.Word == word);
				if (uniqueWord != null)
				{
					uniqueWord.Count++;
				}
				else
				{
					uniqueWords.Add(new WordCountPair<string, int>(word, 1));
				}
			}

			uniqueWords.Sort((first, second) => second.Count.CompareTo(first.Count));

			return new FreqResult(uniqueWords.Count,
				uniqueWords.Select(uw => new KeyValuePair<string, int>(uw.Word, uw.Count)).Take(10));
		}
	}
}