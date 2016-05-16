using System;
using BinaryHeapLib;

namespace Lab5
{
	internal static class Program
	{
		private static void Main()
		{
			var heap = new BinaryHeap<double>();
			Console.WriteLine(heap.Count);
			heap.Add(1);
			heap.Add(2);
			heap.Add(3);
			heap.Add(-1);
			heap.Add(0);
			heap.Add(5);
			heap.Add(-3);
			heap.Add(10);
			Console.WriteLine(heap.Count);
		}
	}
}