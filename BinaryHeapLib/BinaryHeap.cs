using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BinaryHeapLib
{
	public abstract class BinaryHeap<T> : IBinaryHeap<T> where T : IComparable
	{
		protected BinaryHeap()
		{
		}

		protected BinaryHeap(IEnumerable<T> items)
		{
			foreach (var item in items)
			{
				Add(item);
			}
		}

		private T[] _items;

		public T this[int index]
		{
			get { return _items[index]; }
			set { _items[index] = value; }
		}

		public int Count => _items?.Length ?? 0;

		public void Add(T item)
		{
			if (_items == null)
			{
				_items = new[] {item};
			}
			else
			{
				var temp = _items;
				_items = new T[temp.Length + 1];
				for (var i = 0; i < temp.Length; i++)
				{
					_items[i] = temp[i];
				}
				_items[temp.Length] = item;
				UpOrDown(temp.Length);
			}
		}

		public T GetTopElement()
		{
			return _items[0];
		}

		public void DeleteTopElement()
		{
			var tempArr = _items;
			_items = new T[tempArr.Length - 1];
			for (var i = 0; i < tempArr.Length - 1; i++)
			{
				_items[i] = tempArr[i + 1];
			}

			Sort();
		}

		public IEnumerator<T> GetEnumerator()
		{
			return ((IEnumerable<T>) _items).GetEnumerator();
		}

		public override string ToString()
		{
			return _items.Aggregate("", (current, t) => current + (t.ToString() + ' '));
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _items.GetEnumerator();
		}

		public void RestoreHeapPropertiesFromItem(int itemIdx)
		{
			while (true)
			{
				if (_items.Length <= 1)
					return;

				if (_items.Length%2 == 0 && _items.Length/2 == itemIdx + 1)
				{
					var single = this[2*itemIdx + 1];
					var up = this[itemIdx];

					if (Compare(up, single) <= 0)
						return;

					this[itemIdx] = single;
					this[2*itemIdx + 1] = up;
					return;
				}

				var left = this[2*itemIdx + 1];
				var right = this[2*itemIdx + 2];
				var big = this[itemIdx];

				if (Compare(big, right) < 0 && Compare(big, left) < 0)
				{
					return;
				}

				if (Compare(left, right) <= 0)
				{
					this[itemIdx] = left;
					this[2*itemIdx + 1] = big;

					if (2*itemIdx + 1 < _items.Length/2)
					{
						itemIdx = 2*itemIdx + 1;
						continue;
					}
				}
				else
				{
					this[itemIdx] = right;
					this[2*itemIdx + 2] = big;

					if (2*itemIdx + 2 < _items.Length/2)
					{
						itemIdx = 2*itemIdx + 2;
						continue;
					}
				}

				break;
			}
		}

		private void Sort()
		{
			RestoreHeapProperties();

			var itemsCount = _items.Length;
			var newarr = new T[itemsCount];
			for (var i = 0; i <= itemsCount - 1; i++)
			{
				newarr[_items.Length - 1] = _items[0];
				_items[0] = _items[_items.Length - 1];
				var temparr = _items;
				_items = new T[temparr.Length - 1];
				for (var j = 0; j < _items.Length; j++)
				{
					_items[j] = temparr[j];
				}
				RestoreHeapPropertiesFromItem(0);
			}
			_items = newarr;
		}

		private void UpOrDown(int currentIndex)
		{
			while (true)
			{
				if (currentIndex == 0)
					return;

				var parentItem = this[(currentIndex - 1)/2];
				var current = this[currentIndex];

				if (Compare(parentItem, current) < 0)
				{
					this[(currentIndex - 1)/2] = current;
					this[currentIndex] = parentItem;
					currentIndex = (currentIndex - 1)/2;
					continue;
				}

				break;
			}
		}

		protected abstract int Compare(T first, T second);

		protected void RestoreHeapProperties()
		{
			for (var i = _items.Length/2 - 1; i >= 0; i--)
			{
				RestoreHeapPropertiesFromItem(i);
			}
		}
	}
}