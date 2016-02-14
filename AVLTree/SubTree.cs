using System;
using System.Collections;
using System.Collections.Generic;

namespace AVLTree
{
	public class SubTree<TValue> : IEnumerable<TValue> where TValue : IComparable
	{
		public SubTree(TValue value)
		{
			Value = value;
		}

		public TValue Value { get; }

		public int Height
		{
			get
			{
				if (RightSubtree != null && LeftSubtree != null)
				{
					var right = RightSubtree.Height;
					var left = LeftSubtree.Height;
					return right > left ? right : left;
				}

				if (RightSubtree != null)
				{
					return RightSubtree.Height;
				}

				if (LeftSubtree != null)
				{
					return LeftSubtree.Height;
				}
				
				//all of subtrees are null
				return 0;
			}
		}

		private SubTree<TValue> LeftSubtree { get; set; }
		private SubTree<TValue> RightSubtree { get; set; }

		public IEnumerator<TValue> GetEnumerator()
		{
			// first of all return current value
			yield return Value;

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

		public void Add(TValue value)
		{
			var compare = value.CompareTo(Value);
			if (compare == 0)
				throw new ArgumentException("The same value already exists in tree.",nameof(value));
			if (compare < 0)
			{
				if(LeftSubtree == null)
					LeftSubtree = new SubTree<TValue>(value);
				else
					LeftSubtree.Add(value);
			}
			else
			{
				
			}
		}

		public bool Contains(TValue item)
		{
			var current = this;
			while (current != null)
			{
				if (current.Value.CompareTo(item) == 0)
					return true;

				if (current.LeftSubtree == null && current.RightSubtree == null)
					return false;

				if (current.Value.CompareTo(item) > 0)
				{
					current = current.LeftSubtree;
				}
				else
				{
					if (current.Value.CompareTo(item) < 0)
						current = current.RightSubtree;
				}
			}

			return false;
		}
	}
}