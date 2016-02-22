using System;
using System.Collections;
using System.Collections.Generic;

namespace AVLTree
{
	public class SubTree<TValue> : IEnumerable<Node<TValue>> where TValue : IComparable
	{
		public SubTree(Node<TValue> node, SubTree<TValue> parentSubTree)
		{
			Data = node;
			_parentSubTree = parentSubTree;
		}

		public Node<TValue> Data { get; }

		private readonly SubTree<TValue> _parentSubTree;

		public int Height
		{
			get
			{
				if (RightSubtree != null && LeftSubtree != null)
				{
					var right = RightSubtree.Height;
					var left = LeftSubtree.Height;
					return right > left ? right + 1 : left + 1;
				}

				if (RightSubtree != null)
				{
					return RightSubtree.Height + 1;
				}

				if (LeftSubtree != null)
				{
					return LeftSubtree.Height + 1;
				}

				//all of subtrees are null
				return 0;
			}
		}

		private int LeftHeight
		{
			get
			{
				if (LeftSubtree == null)
					return 0;
				return LeftSubtree.Height + 1;
			}
		}

		private int RightHeight
		{
			get
			{
				if (RightSubtree == null)
					return 0;
				return RightSubtree.Height + 1;
			}
		}

		private SubTree<TValue> LeftSubtree { get; set; }
		private SubTree<TValue> RightSubtree { get; set; }

		public void Add(TValue value)
		{
			var current = this;
			var level = 1;
			while (true)
			{
				var compare = value.CompareTo(current.Data.Value);

				if (compare == 0)
					throw new ArgumentException("The same value already exists in tree.", nameof(value));

				if (compare < 0)
				{
					if (current.LeftSubtree == null)
					{
						current.LeftSubtree = new SubTree<TValue>(new Node<TValue>(value, level), current);
						BalanceAfterAdd(current);
						return;
					}

					current = current.LeftSubtree;
				}
				else
				{
					if (current.RightSubtree == null)
					{
						current.RightSubtree = new SubTree<TValue>(new Node<TValue>(value, level), current);
						BalanceAfterAdd(current);
						return;
					}

					current = current.RightSubtree;
				}

				level++;
			}
		}

		private static void BalanceAfterAdd(SubTree<TValue> tree)
		{
			var br = "--------------------------------------------------------------------------------------------------------------------------------";
			Console.WriteLine(br);
			var current = tree;
			while (current != null)
			{
				Console.WriteLine($"|\tValue: {current.Data.Value}\t" +
				                  $"|\tLevel: {current.Data.Level}\t" +
				                  $"|\tCurrent Height: {current.Height}\t" +
				                  $"|\tLeft Height: {current.LeftHeight}\t" +
				                  $"|\tRight Height: {current.RightHeight}");
				if (Math.Abs(current.LeftHeight - current.RightHeight) > 1)
				{
					Console.WriteLine(br);
					Console.WriteLine("Need balance!");
				}
				current = current._parentSubTree;
			}
		}

		public bool Contains(TValue item)
		{
			var current = this;
			while (current != null)
			{
				if (current.Data.Value.CompareTo(item) == 0)
					return true;

				if (current.LeftSubtree == null && current.RightSubtree == null)
					return false;

				if (current.Data.Value.CompareTo(item) > 0)
				{
					current = current.LeftSubtree;
				}
				else
				{
					if (current.Data.Value.CompareTo(item) < 0)
						current = current.RightSubtree;
				}
			}

			return false;
		}

		public IEnumerator<Node<TValue>> GetEnumerator()
		{
			// first of all return current value
			yield return Data;

			// save all nodes we checking for subtrees in near future
			var subtrees = new Queue<SubTree<TValue>>();

			if (LeftSubtree == null && RightSubtree == null)
				yield break;

			if (LeftSubtree != null)
			{
				foreach (var value in LeftSubtree)
				{
					yield return value;
				}
			}

			if (RightSubtree != null)
			{
				foreach (var value in RightSubtree)
				{
					yield return value;
				}
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public override string ToString()
		{
			return $"Value: {Data}, Current Height: {Height}, Left Height: {LeftHeight}, Right Height: {RightHeight}";
		}
	}
}