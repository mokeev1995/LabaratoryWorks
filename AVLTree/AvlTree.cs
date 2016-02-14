using System;
using System.Collections;
using System.Collections.Generic;

namespace AVLTree
{
	public class AvlTree<TValue> : ICollection<TValue> where TValue : IComparable
	{
		private SubTree<TValue> _rootTree;

		public int Count { get; }

		public bool IsReadOnly => false;

		public void Add(TValue item)
		{
			if (_rootTree == null)
			{
				_rootTree = new SubTree<TValue>(item);
			}
			else
			{
				_rootTree.Add(item);
			}
		}

		public void Clear()
		{
			_rootTree = null;
		}

		public bool Contains(TValue item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(TValue[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public bool Remove(TValue item)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<TValue> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}