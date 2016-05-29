using System;
using System.Collections.Generic;

namespace BinaryHeapLib
{
	public class MinBinaryHeap<T> : BinaryHeap<T> where T : IComparable
	{
		public MinBinaryHeap()
		{
		}

		public MinBinaryHeap(IEnumerable<T> items) : base(items)
		{
		}

		protected override int Compare(T first, T second)
		{
			return second.CompareTo(first);
		}
	}
}