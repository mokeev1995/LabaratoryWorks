using System.Collections.Generic;
using System.Linq;

namespace Lab1Lib
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

		public static bool operator ==(FreqResult first, FreqResult second)
		{
			if (ReferenceEquals(first, null) && ReferenceEquals(second, null))
				return true;
			if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
				return false;

			return first.Count == second.Count && ResultsEquals(first.TopResults, second.TopResults);
		}

		private static bool ResultsEquals(IEnumerable<KeyValuePair<string, int>> firstResults, IEnumerable<KeyValuePair<string, int>> secondResults)
		{
			var first = firstResults as IList<KeyValuePair<string, int>> ?? firstResults.ToList();
			var second = secondResults as IList<KeyValuePair<string, int>> ?? secondResults.ToList();

			var equalsFirstInSecond = first.Select(firstResult => second.Any(secondResult => secondResult.Key == firstResult.Key && secondResult.Value == firstResult.Value)).All(found => found);
			var equalsSecondInFirst = second.Select(secondResult=> first.Any(firstResult => secondResult.Key == firstResult.Key && secondResult.Value == firstResult.Value)).All(found => found);

			return equalsSecondInFirst && equalsFirstInSecond;
		}

		public static bool operator !=(FreqResult first, FreqResult second)
		{
			return !(first == second);
		}
	}
}