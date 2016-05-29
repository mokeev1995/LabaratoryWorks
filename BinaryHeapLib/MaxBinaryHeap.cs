using System;
using System.Collections.Generic;

namespace BinaryHeapLib
{
	public class MaxBinaryHeap<T> : BinaryHeap<T> where T : IComparable
	{
		public MaxBinaryHeap()
		{
		}

		public MaxBinaryHeap(IEnumerable<T> items) : base(items)
		{
		}

		protected override int Compare(T first, T second)
		{
			return first.CompareTo(second);
		}
	}
}