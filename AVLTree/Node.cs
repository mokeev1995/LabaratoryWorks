using System;

namespace AVLTree
{
	public class Node<TValue>  where TValue : IComparable
	{
		public Node(TValue value, int level)
		{
			Value = value;
			Level = level;
		}

		public TValue Value { get; }
		public int Level { get; }

		public override string ToString()
		{
			return $"Value: {Value}, Level: {Level}";
		}
	}
}