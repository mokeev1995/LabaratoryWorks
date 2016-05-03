using System;
using System.Collections;
using System.Collections.Generic;

namespace SkipListLib
{
	public class SkipList<TKey, TValue> : IDictionary<TKey, TValue>
	{
		private const int MaxLevel = 4;

		private const double Probability = 0.5;

		private readonly IComparer _comparer;

		private readonly Node _header = new Node(MaxLevel);

		private readonly Random _random = new Random();

		private int _listLevel;

		private long _version;


		public SkipList()
		{
			Initialize();
		}

		public SkipList(IComparer comparer)
		{
			_comparer = comparer;

			Initialize();
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			if (!Search(key))
			{
				value = default(TValue);
				return false;
			}

			value = this[key];

			return true;
		}

		public TValue this[TKey key]
		{
			get
			{
				var val = default(TValue);
				Node curr;

				if (Search(key, out curr))
				{
					val = curr.Entry.Value;
				}

				return val;
			}
			set
			{
				var update = new Node[MaxLevel];
				Node curr;

				if (Search(key, out curr, update))
				{
					curr.Entry.Value = value;
					_version++;
				}
				else
				{
					Insert(key, value, update);
				}
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SkipListEnumerator(this);
		}

		public bool ContainsKey(TKey key)
		{
			return Search(key);
		}

		public void Add(TKey key, TValue value)
		{
			var update = new Node[MaxLevel];

			if (!Search(key, update))
			{
				Insert(key, value, update);
			}
			else
			{
				throw new ArgumentException(
					"An attempt was made to add an element in which the key of the element already exists in the SkipList.");
			}
		}

		public bool Remove(TKey key)
		{
			var update = new Node[MaxLevel];
			Node curr;

			if (!Search(key, out curr, update)) return true;

			for (var i = 0;
				i < _listLevel &&
				update[i].Forward[i] == curr;
				i++)
			{
				update[i].Forward[i] = curr.Forward[i];
			}

			curr.Dispose();

			while (_listLevel > 1 && _header.Forward[_listLevel - 1] == _header)
			{
				_listLevel--;
			}

			Count--;
			_version++;

			return true;
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			Add(item.Key, item.Value);
		}

		public void Clear()
		{
			// Start at the beginning of the skip list.
			var curr = _header.Forward[0];

			// While we haven't reached the end of the skip list.
			while (curr != _header)
			{
				var prev = curr;
				curr = curr.Forward[0];
				prev.Dispose();
			}

			Initialize();
			_version++;
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return Search(item.Key);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(index),
					"An attempt was made to copy the value elements of a SkipList to an array that is too small.");
			}

			if (index >= array.Length)
			{
				throw new ArgumentException(
					"An attempt was made to copy the value elements of a SkipList to an array that is too small.");
			}

			if (array.Length - index < Count)
			{
				throw new ArgumentException("An attempt was made to pass an out of range index to the CopyTo method of a SkipList.");
			}

			var curr = _header.Forward[0];

			while (curr != _header)
			{
				array.SetValue(curr.Entry.Value, index);

				curr = curr.Forward[0];
				index++;
			}
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			return Remove(item.Key);
		}


		public ICollection<TValue> Values
		{
			get
			{
				var curr = _header.Forward[0];
				var collection = new List<TValue>();

				while (curr != _header)
				{
					collection.Add(curr.Entry.Value);
					curr = curr.Forward[0];
				}

				return collection;
			}
		}

		public ICollection<TKey> Keys
		{
			get
			{
				var curr = _header.Forward[0];
				var collection = new List<TKey>();

				while (curr != _header)
				{
					collection.Add(curr.Entry.Key);
					curr = curr.Forward[0];
				}

				return collection;
			}
		}

		public int Count { get; private set; }

		public bool IsReadOnly { get; } = false;

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return new SkipListEnumerator(this);
		}

		~SkipList()
		{
			Clear();
		}

		private void Initialize()
		{
			_listLevel = 1;
			Count = 0;


			for (var i = 0; i < MaxLevel; i++)
			{
				_header.Forward[i] = _header;
			}
		}

		private int GetNewLevel()
		{
			var level = 1;

			while (_random.NextDouble() < Probability && level < MaxLevel &&
			       level <= _listLevel)
			{
				level++;
			}

			return level;
		}

		private bool Search(TKey key)
		{
			Node curr;
			var dummy = new Node[MaxLevel];

			return Search(key, out curr, dummy);
		}

		private bool Search(TKey key, out Node curr)
		{
			var dummy = new Node[MaxLevel];

			return Search(key, out curr, dummy);
		}

		private bool Search(TKey key, Node[] update)
		{
			Node curr;

			return Search(key, out curr, update);
		}

		private bool Search(TKey key, out Node curr, Node[] update)
		{
			if (key == null)
			{
				throw new ArgumentNullException(nameof(key), "An attempt was made to pass a null key to a SkipList.");
			}

			var result = _comparer != null
				? SearchWithComparer(key, out curr, update)
				: SearchWithComparable(key, out curr, update);

			return result;
		}

		private bool SearchWithComparer(TKey key, out Node curr, Node[] update)
		{
			var found = false;

			curr = _header;

			for (var i = _listLevel - 1; i >= 0; i--)
			{
				while (curr.Forward[i] != _header &&
				       _comparer.Compare(curr.Forward[i].Entry.Key, key) < 0)
				{
					curr = curr.Forward[i];
				}

				update[i] = curr;
			}

			curr = curr.Forward[0];

			if (curr != _header && _comparer.Compare(key, curr.Entry.Key) == 0)
			{
				found = true;
			}

			return found;
		}

		private bool SearchWithComparable(TKey key, out Node curr, Node[] update)
		{
			if (!(key is IComparable))
			{
				throw new ArgumentException(
					"The SkipList was set to use the IComparable interface and an attempt was made to add a key that does not support this interface.");
			}

			var found = false;
			IComparable comp;

			curr = _header;

			for (var i = _listLevel - 1; i >= 0; i--)
			{
				comp = (IComparable) curr.Forward[i].Entry.Key;

				while (curr.Forward[i] != _header && comp.CompareTo(key) < 0)
				{
					curr = curr.Forward[i];
					comp = (IComparable) curr.Forward[i].Entry.Key;
				}

				update[i] = curr;
			}

			curr = curr.Forward[0];

			comp = (IComparable) curr.Entry.Key;

			if (curr != _header && comp.CompareTo(key) == 0)
			{
				found = true;
			}

			return found;
		}

		private void Insert(TKey key, TValue val, Node[] update)
		{
			var newLevel = GetNewLevel();

			if (newLevel > _listLevel)
			{
				for (var i = _listLevel; i < newLevel; i++)
				{
					update[i] = _header;
				}

				_listLevel = newLevel;
			}

			var newNode = new Node(newLevel, key, val);

			for (var i = 0; i < newLevel; i++)
			{
				newNode.Forward[i] = update[i].Forward[i];

				update[i].Forward[i] = newNode;
			}

			Count++;
			_version++;
		}

		private class Node : IDisposable
		{
			public readonly Node[] Forward;

			public DictionaryEntry<TKey, TValue> Entry;

			public Node(int level)
			{
				Forward = new Node[level];
			}

			public Node(int level, TKey key, TValue val)
			{
				Forward = new Node[level];
				Entry.Key = key;
				Entry.Value = val;
			}

			public void Dispose()
			{
				for (var i = 0; i < Forward.Length; i++)
				{
					Forward[i] = null;
				}
			}
		}

		private class SkipListEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>
		{
			private readonly SkipList<TKey, TValue> _list;

			private readonly long _version;

			private Node _current;

			private bool _moveResult = true;

			public SkipListEnumerator(SkipList<TKey, TValue> list)
			{
				_list = list;
				_version = list._version;
				_current = list._header;
			}

			public bool MoveNext()
			{
				if (_version == _list._version)
				{
					if (!_moveResult) return _moveResult;

					_current = _current.Forward[0];

					if (_current == _list._header)
					{
						_moveResult = false;
					}
				}
				else
				{
					throw new InvalidOperationException(
						"SkipListEnumerator is no longer valid. The SkipList has been modified since the creation of this enumerator.");
				}

				return _moveResult;
			}

			public void Reset()
			{
				if (_version == _list._version)
				{
					_current = _list._header;
					_moveResult = true;
				}
				else
				{
					throw new InvalidOperationException(
						"SkipListEnumerator is no longer valid. The SkipList has been modified since the creation of this enumerator.");
				}
			}

			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					if (_version != _list._version)
					{
						throw new InvalidOperationException(
							"SkipListEnumerator is no longer valid. The SkipList has been modified since the creation of this enumerator.");
					}
					if (_current == _list._header)
					{
						throw new InvalidOperationException(
							"An attempt was made to access a SkipListEnumerator that is positioned before the first element of a SkipList or after the last element. ");
					}
					var entry = new KeyValuePair<TKey, TValue>(_current.Entry.Key, _current.Entry.Value);

					return entry;
				}
			}

			object IEnumerator.Current => Current;

			public void Dispose()
			{
				Reset();
			}
		}
	}

	public struct DictionaryEntry<TTKey, TTValue>
	{
		public TTKey Key { get; set; }
		public TTValue Value { get; set; }
	}
}