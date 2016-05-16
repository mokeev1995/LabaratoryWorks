using System;
using System.Linq;

namespace BinaryHeapLib
{
	public class BinaryHeap<T> where T : IComparable
	{
		private T[] _data;

		public T this[int i]
		{
			get { return _data[i]; }
			set { _data[i] = value; }
		}

		public int Count => _data?.Length ?? 0;

		private void Shake()
		{
			for (var i = _data.Length/2 - 1; i >= 0; i--)
			{
				Heapify(i);
			}
		}

		private void Heapify(int itemIdx)
		{
			while (true)
			{
				if (_data.Length <= 1)
				{
					return;
				}

				if (_data.Length%2 == 0 && _data.Length/2 == itemIdx + 1)
				{
					var single = this[2*itemIdx + 1];
					var up = this[itemIdx];
					if (up.CompareTo(single) <= 0)
					{
						return;
					}
					this[itemIdx] = single;
					this[2*itemIdx + 1] = up;
					return;
				}
				var left = this[2*itemIdx + 1];
				var right = this[2*itemIdx + 2];
				var big = this[itemIdx];
				if (big.CompareTo(right) < 0 && big.CompareTo(left) < 0)
				{
					return;
				}
				if (left.CompareTo(right) <= 0)
				{
					this[itemIdx] = left;
					this[2*itemIdx + 1] = big;
					if (2*itemIdx + 1 < _data.Length/2)
					{
						itemIdx = 2*itemIdx + 1;
						continue;
					}
				}
				else
				{
					this[itemIdx] = right;
					this[2*itemIdx + 2] = big;
					if (2*itemIdx + 2 < _data.Length/2)
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
			Shake();
			var itemsCount = _data.Length;
			var newarr = new T[itemsCount];
			for (var i = 0; i <= itemsCount - 1; i++)
			{
				newarr[_data.Length - 1] = _data[0];
				_data[0] = _data[_data.Length - 1];
				var temparr = _data;
				_data = new T[temparr.Length - 1];
				for (var j = 0; j < _data.Length; j++)
				{
					_data[j] = temparr[j];
				}
				Heapify(0);
			}
			_data = newarr;
		}

		public void Add(T item)
		{
			if (_data == null)
			{
				_data = new[] {item};
			}
			else
			{
				var temp = _data;
				_data = new T[temp.Length + 1];
				for (var i = 0; i < temp.Length; i++)
				{
					_data[i] = temp[i];
				}
				_data[temp.Length] = item;
				Up(temp.Length);
			}
		}

		private void Up(int i)
		{
			while (true)
			{
				if (i == 0)
				{
					return;
				}
				var father = this[(i - 1)/2];
				var current = this[i];
				if (father.CompareTo(current) < 0)
				{
					this[(i - 1)/2] = current;
					this[i] = father;
					i = (i - 1)/2;
					continue;
				}
				break;
			}
		}

		public T GetMaximum()
		{
			return _data[_data.Length - 1];
		}

		public void DeleteMaximum()
		{
			var temparr = _data;
			_data = new T[temparr.Length - 1];
			for (var i = 0; i < temparr.Length - 1; i++)
			{
				_data[i] = temparr[i];
			}
		}

		public override string ToString()
		{
			return _data.Aggregate("", (current, t) => current + (t.ToString() + ' '));
		}
	}
}