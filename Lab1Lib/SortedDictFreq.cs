using System.Collections.Generic;
using System.Linq;

namespace Lab1Lib
{
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

			var result = uniqueWords.OrderBy(pair => pair.Value);

			return new FreqResult(uniqueWords.Count, result.Reverse().Take(10));
		}
	}
}