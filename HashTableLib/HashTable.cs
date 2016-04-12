using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HashTableLib
{
	/// <summary>
	///     Generic Hash Table
	/// </summary>
	/// <typeparam name="TKey">Comparable Key</typeparam>
	/// <typeparam name="TValue">Any Value to store</typeparam>
	public class HashTable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> where TKey : IComparable
	{
		private TableNode<TKey, TValue>[] _data;
		private int _capacity;


		/// <summary>
		///     Hash Table collection
		/// </summary>
		public HashTable(int capacity = 4)
		{
			_capacity = capacity;
			_data = new TableNode<TKey, TValue>[capacity];
		}

		/// <summary>
		///     Iterator for HashTable
		/// </summary>
		/// <param name="key"></param>
		/// <returns>Value for key</returns>
		/// <exception cref="ArgumentException" accessor="get">Key doesn't exists in hash table</exception>
		/// <exception cref="ArgumentException" accessor="set">Key doesn't exists in hash table</exception>
		public TValue this[TKey key]
		{
			get
			{
				var hash = key.GetHashCode();
				var item = FirstOrDefault(node => node.Hash == hash);
				if (item == null)
					throw new ArgumentException("Key doesn't exists in hash table");

				return item.GetValue(key);
			}
			set
			{
				var hash = key.GetHashCode();
				var item = FirstOrDefault(node => node.Hash == hash);
				if (item == null)
					throw new ArgumentException("Key doesn't exists in hash table");

				item.SetValue(key, value);
			}
		}

		/// <summary>
		///     Count of elements in hash table
		/// </summary>
		public int Count { get; private set; }

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return _data.SelectMany(item => item).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		///     Add item
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <exception cref="ArgumentNullException">hash is null.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		///     index is less than 0.Or index is equal to or greater than
		///     <see cref="P:System.Collections.Generic.List`1.Count" />.
		/// </exception>
		public void Add(TKey key, TValue value)
		{
			var node = new TableNode<TKey, TValue>(key, value);
			var hash = node.GetHashCode();
			var item = FirstOrDefault(nodeItem => nodeItem.Hash == hash);
			if (item != null)
			{
				item.Add(node);
				return;
			}

			AddToArray(node);
			Count++;
		}

		private TableNode<TKey, TValue> FirstOrDefault(Func<TableNode<TKey, TValue>, bool> predicate)
		{
			for (int i = 0; i < Count; i++)
			{
				if (predicate(_data[i]))
					return _data[i];
			}

			return null;
		}

		private void AddToArray(TableNode<TKey, TValue> node)
		{
			while (true)
			{
				if (Count < _capacity)
				{
					_data[Count] = node;
				}
				else
				{
					_capacity *= 2;
					var tmpData = new TableNode<TKey, TValue>[_capacity];
					Array.Copy(_data, tmpData, _data.Length);
					_data = tmpData;
					continue;
				}
				break;
			}
		}

		private void RemoveFromArray(TableNode<TKey, TValue> node)
		{
			var found = false;

			for (var i = 0; i < Count; i++)
			{
				if (!found)
				{
					if (node.Key.CompareTo(_data[i].Key) == 0)
					{
						found = true;
						_data[i] = i + 1 < _data.Length ? _data[i + 1] : null;
					}
				}
				else
				{
					_data[i] = i + 1 < _data.Length ? _data[i + 1] : null;
				}
			}
		}

		/// <summary>
		///     Removes item
		/// </summary>
		/// <param name="key"></param>
		/// <exception cref="ArgumentException">
		///     Key
		///     <param name="key"></param>
		///     doesn't exists in hash table.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		///     index is less than 0.Or index is equal to or greater than
		///     <see cref="P:System.Collections.Generic.List`1.Count" />.
		/// </exception>
		/// <exception cref="ArgumentNullException">match is null.</exception>
		public void Remove(TKey key)
		{
			var keyHash = key.GetHashCode();

			var item = FirstOrDefault(nodeItem => nodeItem.Hash == keyHash);
			if (item == null)
				throw new ArgumentException("Key doesn't exists in hash table.");

			if (item.Key.CompareTo(key) == 0)
			{

				if (item.NextNode != null)
				{
					RemoveFromArray(item);
					AddToArray(item.NextNode);
				}
				else
				{
					RemoveFromArray(item);
				}
			}
			else
			{
				item.Remove(key);
			}

			Count--;
		}

		public bool ContainsKey(TKey key)
		{
			var hash = key.GetHashCode();
			var item = FirstOrDefault(nodeItem => nodeItem.Hash == hash);

			return item != null && item.ContainsKey(key);
		}
	}

	internal class TableNode<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> where TKey : IComparable
	{
		public TableNode(TKey key, TValue value)
		{
			Key = key;
			Value = value;
			Hash = key.GetHashCode();
		}

		public int Hash { get; }

		public TKey Key { get; }
		private TValue Value { get; set; }

		public TableNode<TKey, TValue> NextNode { get; private set; }

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			var current = this;
			do
			{
				yield return new KeyValuePair<TKey, TValue>(current.Key, current.Value);
				current = current.NextNode;
			} while (current != null);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public override int GetHashCode()
		{
			return Key.GetHashCode();
		}

		public void Add(TableNode<TKey, TValue> node)
		{
			if (NextNode == null)
				NextNode = node;
			else
				NextNode.Add(node);
		}

		/// <summary>
		/// </summary>
		/// <param name="key"></param>
		/// <exception cref="ArgumentOutOfRangeException">if
		///     <param name="key">key</param>
		///     doestn't exists in node.
		/// </exception>
		/// <exception cref="ArgumentException"><paramref name="key" /> is not the same type as this instance. </exception>
		public void Remove(TKey key)
		{
			if (NextNode == null)
				throw new ArgumentOutOfRangeException(nameof(key));

			if (NextNode.Key.CompareTo(key) == 0)
			{
				NextNode = NextNode.NextNode;
			}
		}

		/// <summary>
		///     Returns value for key
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentOutOfRangeException">Wether key is not in list.</exception>
		/// <exception cref="ArgumentException"><paramref name="key" /> is not the same type as this instance. </exception>
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
	}
}