using System;
using System.Collections;
using System.Collections.Generic;

namespace HashTableLib
{
	internal class TableNode<TKey, TValue> : IEnumerable<TableNode<TKey, TValue>> where TKey : IComparable
	{
		public TableNode(TKey key, TValue value)
		{
			Key = key;
			Value = value;
		}

		public TKey Key { get; }
		public TValue Value { get; private set; }

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

		public bool ContainsKey(TKey key)
		{
			foreach (var item in this)
			{
				if (item.Key.CompareTo(key) == 0)
					return true;
			}

			return false;
		}

		public void SetValue(TKey key, TValue value)
		{
			if (!ContainsKey(key))
				throw new KeyNotFoundException("Key doesn't exists in hash table");

			var current = this;
			do
			{
				if (current.Key.CompareTo(key) == 0)
				{
					current.Value = value;
					break;
				}

				current = current.NextNode;
			} while (current.NextNode != null);
		}

		public TValue GetValue(TKey key)
		{
			if (!ContainsKey(key))
				throw new KeyNotFoundException("Key doesn't exists in hash table");

			var current = this;
			do
			{
				if (current.Key.CompareTo(key) == 0)
				{
					return current.Value;
				}

				current = current.NextNode;
			} while (current != null);

			throw new KeyNotFoundException("Key doesn't exists in hash table");
		}

		public void Add(TKey key, TValue value)
		{
			var current = this;
			while (current.NextNode != null)
			{
				current = current.NextNode;
			}

			current.NextNode = new TableNode<TKey, TValue>(key, value);
		}

		public void Remove(TKey key)
		{
			var current = this;

			do
			{
				if (current.NextNode.Key.CompareTo(key) == 0)
				{
					var next = current.NextNode.NextNode;
					current.NextNode = next;
					return;
				}
				current = current.NextNode;
			} while (current.NextNode != null);


			throw new KeyNotFoundException("Key was not deleted!");
		}
	}
}