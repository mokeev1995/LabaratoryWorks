using System;
using System.Collections.Generic;
using System.Linq;
using SubstringCoreLib;

namespace KnuthMorrisPrattAlgorithm
{
	public class KnuthMorrisPrattFinder : SubstringFinder
	{
		public override IEnumerable<ulong> Find(string what)
		{
			var positions = new List<ulong>();

			if (what.Length > SourceText.Length)
				return positions;

			var prefixes = GetPrefixesForString(what);
			var i = 0;
			while (i < SourceText.Length)
			{
				int j;
				for (j = 0; (i < SourceText.Length) && (j < what.Length); i++, j++)
				{
					while ((j >= 0) && (what[j] != SourceText[i]))
					{
						j = prefixes[j];
					}
				}

				if (j == what.Length)
					positions.Add(Convert.ToUInt64(i - j));
			}

			return positions;
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