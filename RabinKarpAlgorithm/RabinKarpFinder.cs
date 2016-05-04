using System;
using System.Collections.Generic;
using System.Linq;
using SubstringCoreLib;

namespace RabinKarpAlgorithm
{
	public class RabinKarpFinder : SubstringFinder
	{
		private const int PrimeNumber = 191;

		private static int Hash(string stringForHashing)
		{
			return stringForHashing
				.Select((symbol, index) => (int) Math.Pow(PrimeNumber, stringForHashing.Length - 1 - index)*symbol)
				.Sum();
		}

		private int Hash(int oldHash, string what, int index)
		{
			return (oldHash - (int)Math.Pow(PrimeNumber, what.Length - 1) * SourceText[index]) * PrimeNumber +
									   SourceText[index + what.Length];
		}

		public override IEnumerable<int> Find(string what)
		{
			var positions = new List<int>();


			if (what.Length > SourceText.Length)
				return positions;

			var findingStringHash = Hash(what); 
			var sourceStringHash = Hash(SourceText.Substring(0, what.Length));

			for (var index = 0; index <= SourceText.Length - what.Length; index++)
			{
				if (findingStringHash == sourceStringHash)
				{
					var partOfSourceIsEqualToFindingString = true;
					var j = 0;
					while (partOfSourceIsEqualToFindingString && (j < what.Length))
					{
						if (what[j] != SourceText[index + j])
							partOfSourceIsEqualToFindingString = false;
						j++;
					}

					if (partOfSourceIsEqualToFindingString)
						positions.Add(index);
				}
				else if(index + what.Length < SourceText.Length)
				{
					sourceStringHash = Hash(sourceStringHash, what, index);
				}
				else
				{
					break;
				}
			}

			return positions;
		}

		public override string ToString()
		{
			return "Алгоритм Рабина — Карпа";
		}
	}
}