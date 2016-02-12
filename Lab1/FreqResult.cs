using System.Collections.Generic;

namespace Lab1
{
	public class FreqResult
	{
		public FreqResult(int count, IEnumerable<KeyValuePair<string, int>> topResults)
		{
			Count = count;
			TopResults = topResults;
		}

		public int Count { get; }
		public IEnumerable<KeyValuePair<string, int>> TopResults { get; }
	}
}