using System;
using System.Collections.Generic;
using System.Linq;
using SubstringCoreLib;

namespace RabinKarpAlgorithm
{
	public class RabinKarpFinder : SubstringFinder
	{
		private const int PrimeNumber = 31;

		/*private static long Hash(string stringForHashing)
		{
			return stringForHashing
				.Select((symbol, index) => (long)Math.Pow(PrimeNumber, stringForHashing.Length - 1 - index) * symbol)
				.Sum();
		}

		private long Hash(long oldHash, string what, int index)
		{
			return (oldHash - (long)Math.Pow(PrimeNumber, what.Length - 1) * SourceText[index]) * PrimeNumber +
									   SourceText[index + what.Length];
		}

		public override IEnumerable<long> Find(string what)
		{
			var positions = new List<long>();


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
		}*/

		private static ulong MaxOf(params ulong[] items)
		{
			return items.Max();
		}

		public override IEnumerable<ulong> Find(string what)
		{
			var positions = new List<ulong>();

			if (what.Length > SourceText.Length)
				return positions;

			var whatLength = Convert.ToUInt64(what.Length);
			var sourceLength = Convert.ToUInt64(SourceText.Length);

			var poweredHashes = new ulong[MaxOf(whatLength, sourceLength)];

			poweredHashes[0] = 1;
			for (var i = 1L; i < poweredHashes.LongLength; i++)
			{
				poweredHashes[i] = poweredHashes[i - 1]*PrimeNumber;
			}

			var h = new ulong[sourceLength];
			for (var i = 0UL; i < sourceLength; i++)
			{
				var value = Convert.ToUInt64(SourceText[(int) i]) - 'a' + 1;
				h[i] = Convert.ToUInt64(value)*poweredHashes[i];
				if (i != 0) h[i] += h[i - 1];
			}

			var whatStringHashItems = what.Select((symbol, index) => (Convert.ToUInt64(symbol) - 'a' + 1UL)*poweredHashes[index]);
			var whatStringHash = whatStringHashItems.Aggregate(0UL, (current, item) => current + item);

			for (var i = 0UL; i + whatLength - 1 < sourceLength; ++i)
			{
				var currentHash = h[i + whatLength - 1];
				if (i != 0) currentHash -= h[i - 1];
				if (currentHash == whatStringHash*poweredHashes[i])
					positions.Add(i);
			}

			return positions;
		}

		public override string ToString()
		{
			return "Алгоритм Рабина — Карпа";
		}
	}
}