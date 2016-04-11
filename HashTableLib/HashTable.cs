using System;
using System.Collections.Generic;

namespace HashTableLib
{
	/// <summary>
	/// Generic Hash Table
	/// </summary>
	/// <typeparam name="TKey">Comparable Key</typeparam>
	/// <typeparam name="TValue">Any Value to store</typeparam>
	public class HashTable<TKey, TValue> where TKey : IComparable
	{
		private readonly List<TableNode<TKey, TValue>> _data;

		private readonly List<int> _hashes;

		/// <summary>
		/// Hash Table collection
		/// </summary>
		public HashTable()
		{
			_hashes = new List<int>();
			_data = new List<TableNode<TKey, TValue>>();
		}

		/// <summary>
		/// Iterator for HashTable
		/// </summary>
		/// <param name="key"></param>
		/// <returns>Value for key</returns>
		/// <exception cref="ArgumentException" accessor="get">Key doesn't exists in hash table</exception>
		public TValue this[TKey key]
		{
			get
			{
				var hash = key.GetHashCode();
				if(!_hashes.Contains(hash))
					throw new ArgumentException("Key doesn't exists in hash table");

				var position = _hashes.FindIndex(hashItem => hash.CompareTo(hashItem) == 0);
				return _data[position].GetValue(key);
			}
		}
		
		/// <summary>
		/// Count of elements in hash table
		/// </summary>
		public int Count { get; private set; }

		/// <summary>
		/// Add item
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <exception cref="ArgumentNullException">hash is null.</exception>
		/// <exception cref="ArgumentOutOfRangeException">index is less than 0.Or index is equal to or greater than <see cref="P:System.Collections.Generic.List`1.Count" />. </exception>
		public void Add(TKey key, TValue value)
		{
			var node = new TableNode<TKey, TValue>(key, value);
			var hash = node.GetHashCode();

			if (_hashes.Contains(hash))
			{
				var position = _hashes.FindIndex(hashItem => hashItem == hash);
				_data[position].Add(node);

				return;
			}

			_hashes.Add(hash);
			_data.Add(node);
			Count++;
		}

		/// <summary>
		/// Removes item
		/// </summary>
		/// <param name="key"></param>
		/// <exception cref="ArgumentException">Key <param name="key"></param> doesn't exists in hash table.</exception>
		/// <exception cref="ArgumentOutOfRangeException">index is less than 0.Or index is equal to or greater than <see cref="P:System.Collections.Generic.List`1.Count" />. </exception>
		/// <exception cref="ArgumentNullException">match is null.</exception>
		public void Remove(TKey key)
		{
			var keyHash = key.GetHashCode();

			if (!_hashes.Contains(keyHash))
				throw new ArgumentException("Key doesn't exists in hash table.");

			var position = _hashes.FindIndex(hash => keyHash == hash);

			if (_data[position].Key.CompareTo(key) == 0)
			{
				if (_data[position].NextNode != null)
				{
					_data[position] = _data[position].NextNode;
					if (_data[position] == null)
					{
						_hashes.Remove(keyHash);
					}
				}
				else
				{
					
				}

			}
			else
			{
				_data[position].Remove(key);
			}

			Count--;
		}
	}

	internal class TableNode<TKey, TValue> where TKey : IComparable
	{
		public TableNode(TKey key, TValue value)
		{
			Key = key;
			Value = value;
		}

		public TKey Key { get; }
		private TValue Value { get; }

		public TableNode<TKey, TValue> NextNode { get; private set; }

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
		/// <exception cref="ArgumentOutOfRangeException">if <param name="key">key</param> doestn't exists in node.</exception>
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
		/// Returns value for key
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
	}
}