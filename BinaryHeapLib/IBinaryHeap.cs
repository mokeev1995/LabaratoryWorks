using System;
using System.Collections.Generic;

namespace BinaryHeapLib
{
	public interface IBinaryHeap<T> : IEnumerable<T> where T : IComparable
	{
		T this[int index] { get; set; }
		int Count { get; }
		void Add(T item);
		T GetTopElement();
		void DeleteTopElement();
		string ToString();
	}
}