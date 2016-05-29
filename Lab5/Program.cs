using System;
using System.Collections.Generic;
using System.Linq;
using BinaryHeapLib;

namespace Lab5
{
	internal static class Program
	{
		private static void Main()
		{
			IReaderWriter readerWriter = new ConsoleReaderWriter();

			var weights = WeightsReader.Read(readerWriter).ToArray();

			var storage = new MaxBinaryHeap<double> {1};

			foreach (var weight in weights.Select(w => w.Value))
			{
				var idx = 0;
				var item = weight;
				while (idx < storage.Count)
				{
					if (storage[idx] - item >= -0.000001)
					{
						storage[idx] -= item;
						if (idx < storage.Count / 2)
							storage.RestoreHeapPropertiesFromItem(idx);
						break;
					}

					idx++;
				}
				if (storage.Count == idx)
					storage.Add(1 - item);
			}

			readerWriter.WriteLine("Boxes count: {0}", storage.Count);

			Console.ReadKey();
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

	internal class Weight : IComparable, IEquatable<Weight>
	{
		public double Value { get; }

		private Weight(double value)
		{
			Value = value;
		}

		public static Weight Parse(string stringWeight)
		{
			double value;
			var parseSucceed = double.TryParse(stringWeight, out value);
			if(!parseSucceed)
				throw new ArgumentException("Wrong data was sended!", nameof(stringWeight));

			if(value < 0)
				value = 0;
			if (value > 1)
				value = 1;

			return new Weight(value);
		}

		public int CompareTo(object obj)
		{
			var weight = obj as Weight;
			if (weight != null)
				return Value.CompareTo(weight.Value);

			throw new ApplicationException("Wrong type accepted! Type should be " + GetType());
		}

		public override string ToString()
		{
			return $"Value: {Value}";
		}

		public bool Equals(Weight other)
		{
			if (ReferenceEquals(null, other)) return false;
			return ReferenceEquals(this, other) || Value.Equals(other.Value);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == this.GetType() && Equals((Weight) obj);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public static double operator -(Weight first, Weight second)
		{
			return first.Value - second.Value;
		}
	}

	internal static class WeightsReader
	{
		public static IEnumerable<Weight> Read(IReaderWriter readerWriter)
		{
			readerWriter.Write("Enter items count: ");
			var itemsCountString = readerWriter.ReadLine();
			var itemsCount = int.Parse(itemsCountString);
			readerWriter.WriteLine();

			var items = new Weight[itemsCount];
			for (var i = 0; i < itemsCount; i++)
			{
				readerWriter.Write("Item #{0}: ", i);
				items[i] = Weight.Parse(readerWriter.ReadLine());
				readerWriter.WriteLine();
			}
			readerWriter.WriteLine("Weights input complete!");
			return items;
		}
	}

	public interface IReaderWriter
	{
		string ReadLine();
		void Write(string inputString, params object[] @params);
		void WriteLine();
		void WriteLine(string inputString, params object[] @params);
	}

	internal class ConsoleReaderWriter : IReaderWriter
	{
		public string ReadLine()
		{
			return Console.ReadLine();
		}

		public void Write(string inputString, params object[] @params)
		{
			Console.Write(inputString, @params);
		}

		public void WriteLine()
		{
			Write("\n");
		}

		public void WriteLine(string inputString, params object[] @params)
		{
			Write(inputString + "\n", @params);
		}
	}
}