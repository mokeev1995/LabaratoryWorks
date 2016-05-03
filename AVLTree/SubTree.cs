using System;
using System.Collections;
using System.Collections.Generic;

namespace AVLTree
{
	public class SubTree<TValue> : IEnumerable<Node<TValue>>
		where TValue : IComparable
	{
		public SubTree(Node<TValue> node, SubTree<TValue> parentSubTree)
		{
			Data = node;
			ParentSubTree = parentSubTree;
		}

		public Node<TValue> Data { get; }

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

		public SubTree<TValue> ParentSubTree { get; private set; }

		public IEnumerator<Node<TValue>> GetEnumerator()
		{
			yield return Data;

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

		public SubTree<TValue> Add(TValue value)
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
						return current.LeftSubtree;
					}

					current = current.LeftSubtree;
				}
				else
				{
					if (current.RightSubtree == null)
					{
						current.RightSubtree = new SubTree<TValue>(new Node<TValue>(value, level), current);
						return current.RightSubtree;
					}

					current = current.RightSubtree;
				}

				level++;
			}
		}

		public static SubTree<T> Balance<T>(SubTree<T> tree)
			where T : IComparable
		{
			var treeBalanceFactor = CalculateBalanceFactor(tree);

			switch (treeBalanceFactor)
			{
				case -2:
					return CalculateBalanceFactor(tree.RightSubtree) > 0
						? BigLeftRotate(tree, tree.ParentSubTree)
						: SmallLeftRotate(tree, tree.ParentSubTree);
				case 2:
					return CalculateBalanceFactor(tree.LeftSubtree) < 0
						? BigRightRotate(tree.LeftSubtree, tree.ParentSubTree)
						: SmallRightRotate(tree, tree.ParentSubTree);
			}

			return tree;
		}

		internal static int CalculateBalanceFactor<T>(SubTree<T> tree)
			where T : IComparable
		{
			return tree.LeftHeight - tree.RightHeight;
		}

		private static SubTree<T> SmallLeftRotate<T>(SubTree<T> subTree, SubTree<T> parent)
			where T : IComparable
		{
			var pnt = subTree.ParentSubTree;

			var st = subTree.RightSubtree;
			var lvl = subTree.Data.Level;

			foreach (var node in st)
			{
				node.Level--;
			}

			subTree.RightSubtree = null;
			foreach (var node in subTree)
			{
				node.Level++;
			}

			subTree.RightSubtree = st.LeftSubtree;
			subTree.ParentSubTree = st;

			st.LeftSubtree = subTree;
			st.ParentSubTree = parent;
			st.LeftSubtree = pnt;
			st.Data.Level = lvl;

			return st;
		}

		private static SubTree<T> SmallRightRotate<T>(SubTree<T> subTree, SubTree<T> parent)
			where T : IComparable
		{
			var pnt = subTree.ParentSubTree;

			var st = subTree.LeftSubtree;
			var lvl = subTree.Data.Level;

			foreach (var node in st)
			{
				node.Level--;
			}

			subTree.LeftSubtree = null;
			foreach (var node in subTree)
			{
				node.Level++;
			}

			subTree.LeftSubtree = st.RightSubtree;
			subTree.ParentSubTree = st;

			st.ParentSubTree = parent;
			st.RightSubtree = subTree;
			st.LeftSubtree = pnt;
			st.Data.Level = lvl;

			return st;
		}

		private static SubTree<T> BigRightRotate<T>(SubTree<T> subTree, SubTree<T> parent)
			where T : IComparable
		{
			subTree.LeftSubtree = SmallLeftRotate(subTree.LeftSubtree, subTree);
			return SmallRightRotate(subTree, parent);
		}

		private static SubTree<T> BigLeftRotate<T>(SubTree<T> subTree, SubTree<T> parent)
			where T : IComparable
		{
			subTree.RightSubtree = SmallRightRotate(subTree.RightSubtree, subTree);
			return SmallLeftRotate(subTree, parent);
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

		public override string ToString()
		{
			return $"Value: {Data}, Current Height: {Height}, Left Height: {LeftHeight}, Right Height: {RightHeight}";
		}
	}
}