using System.Collections.Generic;
using System.Linq;

namespace Lab1Lib
{
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
}