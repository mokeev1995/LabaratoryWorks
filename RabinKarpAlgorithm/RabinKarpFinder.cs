using System;
using System.Collections.Generic;
using System.Linq;
using SubstringCoreLib;

namespace RabinKarpAlgorithm
{
	public class RabinKarpFinder : SubstringFinder
	{
		private const int PrimeNumber = 31;

		public override IEnumerable<ulong> Find(string what)
		{
			var positions = new List<ulong>();

			if (what.Length > SourceText.Length)
				return positions;

			var whatLength = Convert.ToUInt64(what.Length);
			var sourceLength = Convert.ToUInt64(SourceText.Length);

			// считаем все степени
			var poweredHashes = new ulong[sourceLength];

			poweredHashes[0] = 1;
			for (var i = 1L; i < poweredHashes.LongLength; i++)
			{
				poweredHashes[i] = poweredHashes[i - 1]*PrimeNumber;
			}

			// считаем хэши от всех префиксов строки SourceText
			var h = new ulong[sourceLength];
			for (var i = 0UL; i < sourceLength; i++)
			{
				var value = Convert.ToUInt64(SourceText[(int) i]) - 'a' + 1;
				h[i] = Convert.ToUInt64(value)*poweredHashes[i];
				if (i != 0) h[i] += h[i - 1];
			}

			// считаем хэш от строки what
			var whatStringHashItems = what.Select((symbol, index) => (Convert.ToUInt64(symbol) - 'a' + 1UL)*poweredHashes[index]);
			var whatStringHash = whatStringHashItems.Aggregate(0UL, (current, item) => current + item);

			// перебираем все подстроки T длины |S| и сравниваем их
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