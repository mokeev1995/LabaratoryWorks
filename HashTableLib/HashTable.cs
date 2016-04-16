using System;
using System.Collections;
using System.Collections.Generic;

namespace HashTableLib
{
	public class HashTable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> where TKey : IComparable
	{
		private long _capacity;
		private TableNode<TKey, TValue>[] _data;

		public HashTable(int capacity = 2)
		{
			_capacity = capacity;
			_data = new TableNode<TKey, TValue>[_capacity];
		}


		public int Count { get; private set; }

		public TValue this[TKey key]
		{
			get
			{
				if (!ContainsKey(key))
					throw new KeyNotFoundException("Key doesn't exists in hash table");

				return _data[ComputeHash(key)].GetValue(key);
			}
			set
			{
				if(!ContainsKey(key))
					throw new KeyNotFoundException("Key doesn't exists in hash table");

				_data[ComputeHash(key)].SetValue(key, value);
			}
		}

		private uint ComputeHash(TKey key)
		{
			var hash = key.GetHashCode();

			var code = hash%_capacity;

			return hash >= 0 ? Convert.ToUInt32(code) : Convert.ToUInt32(-code);
		}

		private TableNode<TKey, TValue> FirstOrDefault(Func<TableNode<TKey, TValue>, bool> predicate)
		{
			for (var i = 0; i < Count; i++)
			{
				if (predicate(_data[i]))
					return _data[i];
			}

			return null;
		}

		public bool ContainsKey(TKey key)
		{
			var hash = ComputeHash(key);
			if (_data[hash] == null)
				return false;

			return _data[hash].ContainsKey(key);
		}

		public void Add(TKey key, TValue value)
		{
			if(ContainsKey(key))
				throw new ArgumentException("This key already exists");

			if (_capacity <= Count)
			{
				ExtendArray();
				RebuildHashTable();
			}

			var hash = ComputeHash(key);
			if (_data[hash] == null)
				_data[hash] = new TableNode<TKey, TValue>(key, value);
			else
				_data[hash].Add(key, value);

			Count++;
		}

		private void ExtendArray()
		{
			_capacity *= 2;

			var tmp = new TableNode<TKey, TValue>[_capacity];
			Array.Copy(_data, tmp, _data.Length);
			_data = tmp;
		}

		private void RebuildHashTable()
		{
			var tmpData = new TableNode<TKey, TValue>[_capacity];

			foreach (var node in _data)
			{
				if(node == null)
					continue;

				foreach (var item in node)
				{
					var hash = ComputeHash(item.Key);
					if (tmpData[hash] != null)
						tmpData[hash].Add(item.Key, item.Value);
					else
						tmpData[hash] = item;
				}
			}

			_data = tmpData;
		}

		public void Remove(TKey key)
		{
			if (!ContainsKey(key))
				throw new KeyNotFoundException("Key doesn't exists in hash table");


			var hash = ComputeHash(key);
			if (_data[hash].Key.CompareTo(key) == 0)
			{
				_data[hash] = _data[hash].NextNode;
			}
			else
			{
				if (_data[hash].NextNode == null)
				{
					throw new KeyNotFoundException("Key doesn't exists in hash table");
				}

				_data[hash].Remove(key);
			}

			Count--;
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			foreach (var node in _data)
			{
				if(node == null)
					continue;

				foreach (var item in node)
				{
					yield return new KeyValuePair<TKey, TValue>(item.Key, item.Value);
				}
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}