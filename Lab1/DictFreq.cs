using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
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
}