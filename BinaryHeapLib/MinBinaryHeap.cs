using System;

namespace BinaryHeapLib
{
	public class MinBinaryHeap<T> : BinaryHeap<T> where T : IComparable
	{
		protected override int Compare(T first, T second)
		{
			return second.CompareTo(first);
		}
	}
}