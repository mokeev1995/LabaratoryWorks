using System.Collections.Generic;
using SubstringCoreLib;

namespace KnuthMorrisPrattAlgorithm
{
	public class KnuthMorrisPrattFinder : SubstringFinder
	{
		public override IEnumerable<int> Find(string what)
		{
			var results = new List<int>();

			if (what.Length > _sourceText.Length)
				return results;

			var prefixes = GetPrefixesForString(what);
			var i = 0;
			while (i < _sourceText.Length)
			{
				int j;
				for (j = 0; (i < _sourceText.Length) && (j < what.Length); i++, j++)
				{
					while ((j >= 0) && (what[j] != _sourceText[i]))
					{
						j = prefixes[j];
					}
				}

				if (j == what.Length)
					results.Add(i - j);
			}

			return results;
		}

		private static int[] GetPrefixesForString(string incomeString)
		{
			var prefixes = new int[incomeString.Length];

			prefixes[0] = -1;

			var i = 0;
			var j = -1;

			while (i < incomeString.Length - 1)
			{
				while ((j >= 0) && (incomeString[j] != incomeString[i]))
				{
					j = prefixes[j];
				}

				i++;
				j++;

				if (incomeString[i] == incomeString[j])
					prefixes[i] = prefixes[j];
				else
					prefixes[i] = j;
			}

			return prefixes;
		}

		public override string ToString()
		{
			return "Алгоритм Кнута — Морриса — Пратта";
		}
	}
}