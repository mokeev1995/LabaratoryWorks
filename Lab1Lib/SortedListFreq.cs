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
				if (uniqueWords.ContainsKey(word))
				{
					uniqueWords[word]++;
				}
				else
				{
					uniqueWords.Add(word, 1);
				}
			}

			var result = uniqueWords.OrderByDescending(pair => pair.Value).ToArray();
			return new FreqResult(uniqueWords.Count, result.Take(10));
		}
	}
}