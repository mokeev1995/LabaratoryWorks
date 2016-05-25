using System;

namespace BinaryHeapLib
{
	public interface IBinaryHeap<T> where T : IComparable
	{
		T this[int i] { get; set; }
		int Count { get; }
		void Add(T item);
		T GetTopElement();
		void DeleteTopElement();
		string ToString();
	}
}