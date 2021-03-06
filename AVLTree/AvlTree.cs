﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace AVLTree
{
	public class AvlTree<TValue> : ICollection<Node<TValue>> where TValue : IComparable
	{
		private SubTree<TValue> _rootTree;

		public AvlTree()
		{
			Count = 0;
		}

		public Node<TValue> this[int index]
		{
			get
			{
				if (index > Count)
					throw new ArgumentOutOfRangeException(nameof(index));

				var idx = 0;
				foreach (var node in _rootTree)
				{
					if (idx != index)
					{
						idx++;
						continue;
					}

					return node;
				}

				throw new ApplicationException("Value not found.");
			}
		}

		public int Count { get; private set; }

		public bool IsReadOnly => false;

		public void Add(Node<TValue> item)
		{
			Add(item.Value);
		}

		public bool Remove(Node<TValue> item)
		{
			return Remove(item.Value);
		}

		public void Clear()
		{
			_rootTree = null;
		}

		public bool Contains(Node<TValue> item)
		{
			return _rootTree.Contains(item.Value);
		}

		public void CopyTo(Node<TValue>[] array, int arrayIndex)
		{
			if (array.Length < Count)
				throw new ArgumentOutOfRangeException(nameof(array), "Wrong array dimension");

			var i = 0;
			foreach (var val in this)
			{
				array[i] = val;
				i++;
			}
		}

		public IEnumerator<Node<TValue>> GetEnumerator()
		{
			return _rootTree.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable) _rootTree).GetEnumerator();
		}

		public void Add(TValue item)
		{
			if (_rootTree == null)
			{
				_rootTree = new SubTree<TValue>(new Node<TValue>(item, 0), null);
			}
			else
			{
				var itm = _rootTree.Add(item);
				_rootTree = BalanceAfterAdd(itm);
			}
			Count++;
		}

		private static SubTree<TValue> BalanceAfterAdd(SubTree<TValue> tree)
		{
			var current = tree;
			while (true)
			{
				var balanceFactor = SubTree<TValue>.CalculateBalanceFactor(current);
				if (Math.Abs(balanceFactor) > 1)
				{
					current = SubTree<TValue>.Balance(current);
				}

				if (current.ParentSubTree != null)
					current = current.ParentSubTree;
				else
					return current;
			}
		}

		public bool Remove(TValue item)
		{
			throw new NotImplementedException();
		}

		public bool Contains(TValue item)
		{
			return _rootTree.Contains(item);
		}

		public void CopyTo(TValue[] array, int arrayIndex)
		{
			if (array.Length < Count)
				throw new ArgumentOutOfRangeException(nameof(array), "Wrong array dimension");

			var i = 0;
			foreach (var val in this)
			{
				array[i] = val.Value;
				i++;
			}
		}
	}
}