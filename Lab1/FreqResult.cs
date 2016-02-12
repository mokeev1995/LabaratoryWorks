using System.Collections.Generic;

namespace ConsoleApplication1
{
	public class FreqResult
	{
		public int Count { get; }
		public IEnumerable<KeyValuePair<string, int>> TopResults { get; }

		public FreqResult(int count, IEnumerable<KeyValuePair<string, int>> topResults)
		{
			Count = count;
			TopResults = topResults;
		}
	}
}