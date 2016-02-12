using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
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
}