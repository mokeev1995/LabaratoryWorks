using System;
using BinaryHeapLib;

namespace Lab5
{
	internal static class Program
	{
		private static void Main()
		{
		}

		private static void Tests()
		{
			IBinaryHeap<double> heap = new MaxBinaryHeap<double>();
			heap.Add(1);
			heap.Add(2);
			heap.Add(3);
			heap.Add(-1);
			heap.Add(0);
			heap.Add(5);
			heap.Add(-3);
			heap.Add(10);

			Console.WriteLine(heap);
			Console.WriteLine(heap.GetTopElement());
			heap.DeleteTopElement();
			Console.WriteLine(heap);
			Console.WriteLine();
			Console.ReadKey();

			heap = new MinBinaryHeap<double>();
			heap.Add(1);
			heap.Add(2);
			heap.Add(3);
			heap.Add(-1);
			heap.Add(0);
			heap.Add(5);
			heap.Add(-3);
			heap.Add(10);

			Console.WriteLine(heap);
			Console.WriteLine(heap.GetTopElement());
			heap.DeleteTopElement();
			Console.WriteLine(heap);
			Console.ReadKey();
		}
	}
}