using System;
using System.Collections.Generic;

namespace HashTableLib
{
	public class HashTable<TKey, TValue>
	{
		public int Count { get; private set; }

		private List<int> _hashes;
		private List<TableNode<TKey, TValue>> _data;

		public HashTable()
		{
			_hashes = new List<int>();
			_data = new List<TableNode<TKey, TValue>>();
		}

		public void Add(TKey key, TValue value)
		{
			var node = new TableNode<TKey, TValue>(key, value);
			var hash = node.GetHashCode();
		}

		public void Remove(TKey key)
		{
			throw new NotImplementedException();
		}
	}

	internal class TableNode<TKey, TValue>
	{
		public TKey Key { get; }
		public TValue Value { get; }

		public TableNode(TKey key, TValue value)
		{
			Key = key;
			Value = value;
		}

		public override int GetHashCode()
		{
			return Key.GetHashCode();
		}
	}
}