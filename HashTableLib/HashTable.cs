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


		public TValue this[TKey key]
		{
			get
			{
				var hash = ComputeHash(key);
				var item = _data[hash];
				if (item == null)
					throw new ArgumentException("Key doesn't exists in hash table");

				return item.GetValue(key);
			}
			set
			{
				var hash = ComputeHash(key);
				var item = _data[hash];
				if (item == null)
					throw new ArgumentException("Key doesn't exists in hash table");

				item.SetValue(key, value);
			}
		}

		public int Count { get; private set; }

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			foreach (var item in _data)
			{
				if (item == null)
					continue;

				foreach (var key in item)
				{
					yield return new KeyValuePair<TKey, TValue>(key.Key, key.Value);
				}
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		private uint ComputeHash(TKey key)
		{
			var hash = key.GetHashCode();

			if (hash >= 0)
				return Convert.ToUInt32(hash%_capacity);
			return Convert.ToUInt32(-hash%_capacity);
		}

		public void Add(TKey key, TValue value)
		{
			var node = new TableNode<TKey, TValue>(key, value);

			var hash = ComputeHash(key);
			var item = _data[hash];

			if(item!= null && item.ContainsKey(key))
				throw new ArgumentException("This key already exists");

			if (Count >= _capacity)
			{
				ExtendArray();
				RebuildAllHashes();
			}

			item = _data[hash];

			if (item != null)
			{
				item.AddValue(node);
			}
			else
			{
				AddToArray(node);
			}
			Count++;
		}

		private void RebuildAllHashes()
		{
			var tmp = new TableNode<TKey, TValue>[_capacity];
			foreach (var node in _data)
			{
				if (node == null) continue;
				foreach (var item in node)
				{
					var hash = ComputeHash(item.Key);
					if (tmp[hash] == null)
						tmp[hash] = item;
					else
						tmp[hash].AddValue(item);
				}
			}

			_data = tmp;
		}

		private void ExtendArray()
		{
			_capacity *= 2;

			var tmp = new TableNode<TKey, TValue>[_capacity];
			Array.Copy(_data, tmp, _data.Length);
			_data = tmp;
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

		private void AddToArray(TableNode<TKey, TValue> node)
		{
			if (Count < _data.Length)
			{
				var hash = ComputeHash(node.Key);
				if (_data[hash] == null)
					_data[hash] = node;
				else
					_data[hash].AddValue(node);
			}
			else
			{
				throw new OutOfMemoryException("No more memory to store data");
			}
		}

		private void RemoveFromArray(uint hash, TKey key)
		{
			if (_data[hash].Key.CompareTo(key) == 0)
			{
				_data[hash] = _data[hash].NextNode;
			}
			else
			{
				_data[hash].RemoveItem(key);
			}
		}

		public void Remove(TKey key)
		{
			var hash = ComputeHash(key);

			var item = _data[hash];
			var exists = _data[hash].ContainsKey(item.Key);
			if (item == null || !exists)
				throw new ArgumentException("Key doesn't exists in hash table.");

			if (item.Key.CompareTo(key) == 0)
			{
				if (item.NextNode != null)
				{
					RemoveFromArray(hash, item.Key);
					AddToArray(item.NextNode);
				}
				else
				{
					RemoveFromArray(hash, item.Key);
				}
			}
			else
			{
				item.RemoveItem(key);
			}

			Count--;
		}

		public bool ContainsKey(TKey key)
		{
			var hash = ComputeHash(key);
			var item = _data[hash];

			return item != null && item.ContainsKey(key);
		}
	}

	internal class TableNode<TKey, TValue> : IEnumerable<TableNode<TKey, TValue>> where TKey : IComparable
	{
		public TableNode(TKey key, TValue value)
		{
			Key = key;
			Value = value;
		}

		public TKey Key { get; }
		public TValue Value { get; set; }

		public TableNode<TKey, TValue> NextNode { get; private set; }

		public IEnumerator<TableNode<TKey, TValue>> GetEnumerator()
		{
			var current = this;
			do
			{
				yield return new TableNode<TKey, TValue>(current.Key, current.Value);
				current = current.NextNode;
			} while (current != null);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void AddValue(TableNode<TKey, TValue> node)
		{
			var current = this;
			while (current.NextNode != null)
			{
				current = current.NextNode;
			}

			NextNode = node;
		}

		public void RemoveItem(TKey key)
		{
			var current = this;

			do
			{
				if (current.NextNode.Key.CompareTo(key) == 0)
				{
					NextNode = NextNode.NextNode;
					return;
				}
			} while (current.NextNode != null);

			throw new ArgumentOutOfRangeException(nameof(key));
		}

		public TValue GetValue(TKey key)
		{
			if (Key.CompareTo(key) == 0)
				return Value;

			if (NextNode == null)
				throw new ArgumentOutOfRangeException(nameof(key));

			return NextNode.GetValue(key);
		}

		public void SetValue(TKey key, TValue value)
		{
			if (Key.CompareTo(key) == 0)
			{
				Value = value;
				return;
			}

			if (NextNode == null)
				throw new ArgumentOutOfRangeException(nameof(key));

			NextNode.SetValue(key, value);
		}

		public bool ContainsKey(TKey key)
		{
			var current = this;
			do
			{
				if (current.Key.CompareTo(key) == 0)
					return true;
				current = current.NextNode;
			} while (current != null);

			return false;
		}

		public override string ToString()
		{
			return $"{Key}: {Value}; " + NextNode;
		}
	}
}