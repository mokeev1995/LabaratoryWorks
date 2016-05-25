using System;

namespace BinaryHeapLib
{
	public class MaxBinaryHeap<T> : BinaryHeap<T> where T : IComparable
	{
		protected override int Compare(T first, T second)
		{
			return first.CompareTo(second);
		}
	}
}