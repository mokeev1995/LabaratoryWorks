using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AVLTree
{
	public class AvlTree<TValue> : ICollection<Node<TValue>> where TValue : IComparable
	{
		private SubTree<TValue> _rootTree;

		public AvlTree()
		{
			Count = 0;
		}

		public int Count { get; private set; }

		public bool IsReadOnly => false;

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

		public void Add(Node<TValue> item)
		{
			Add(item.Value);
		}

		public void Add(TValue item)
		{
			if (_rootTree == null)
			{
				_rootTree = new SubTree<TValue>(new Node<TValue>(item, 0));
			}
			else
			{
				_rootTree.Add(item);
			}
			Count++;
		}

		public bool Remove(Node<TValue> item)
		{
			return Remove(item.Value);
		}

		public bool Remove(TValue item)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			_rootTree = null;
		}

		public bool Contains(Node<TValue> item)
		{
			return _rootTree.Contains(item.Value);
		}

		public bool Contains(TValue item)
		{
			return _rootTree.Contains(item);
		}

		public void CopyTo(Node<TValue>[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(TValue[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<Node<TValue>> GetEnumerator()
		{
			return _rootTree.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable) _rootTree).GetEnumerator();
		}
	}
}