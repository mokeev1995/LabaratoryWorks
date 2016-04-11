using System;

namespace BinaryHeapLib
{
	internal class BinaryHeap<T> where T : IComparable
	{
		private T[] _arr;

		public BinaryHeap()
		{
		}

		private BinaryHeap(BinaryHeap<T> bh)
		{
			_arr = new T[bh._arr.Length - 2];
			for (var i = 0; i < _arr.Length; i++)
			{
				_arr[i] = bh._arr[i];
			}
			_hapify(0);
		}

		public T this[int i]
		{
			get { return _arr[i]; }
			set { _arr[i] = value; }
		}

		private void _shake()
		{
			for (var i = _arr.Length/2 - 1; i >= 0; i--)
			{
				_hapify(i);
			}
		}


		private void _hapify(int i)
		{
			if (_arr.Length <= 1)
			{
				return;
			}
			if (_arr.Length%2 == 0 && _arr.Length/2 == i + 1)
			{
				var single = this[2*i + 1];
				var up = this[i];
				if (up.CompareTo(single) >= 0)
				{
					return;
				}
				this[i] = single;
				this[2*i + 1] = up;
				return;
			}
			var left = this[2*i + 1];
			var right = this[2*i + 2];
			var big = this[i];
			if (big.CompareTo(right) > 0 && big.CompareTo(left) > 0)
			{
				return;
			}
			if (left.CompareTo(right) >= 0)
			{
				this[i] = left;
				this[2*i + 1] = big;
				if (2*i + 1 < _arr.Length/2)
				{
					_hapify(2*i + 1);
				}
			}
			else
			{
				this[i] = right;
				this[2*i + 2] = big;
				if (2*i + 2 < _arr.Length/2)
				{
					_hapify(2*i + 2);
				}
			}
		}

		private void _sort()
		{
			_shake();
			var n = _arr.Length;
			var newarr = new T[n];
			for (var i = 0; i <= n - 1; i++)
			{
				newarr[_arr.Length - 1] = _arr[0];
				;
				_arr[0] = _arr[_arr.Length - 1];
				var temparr = _arr;
				_arr = new T[temparr.Length - 1];
				for (var j = 0; j < _arr.Length; j++)
				{
					_arr[j] = temparr[j];
				}
				_hapify(0);
			}
			_arr = newarr;
		}

		public void Add(T item)
		{
			if (_arr == null)
			{
				_arr = new[] {item};
			}
			else
			{
				var temp = _arr;
				_arr = new T[temp.Length + 1];
				for (var i = 0; i < temp.Length; i++)
				{
					_arr[i] = temp[i];
				}
				_arr[temp.Length] = item;
				_sort();
			}
		}

		public T GetMax()
		{
			return _arr[_arr.Length - 1];
		}

		public void DelMax()
		{
			var temparr = _arr;
			_arr = new T[temparr.Length - 1];
			for (var i = 0; i < temparr.Length - 1; i++)
			{
				_arr[i] = temparr[i];
			}
		}

		public override string ToString()
		{
			var res = "";
			for (var i = 0; i < _arr.Length; i++)
			{
				res += _arr[i].ToString() + ' ';
			}
			return res;
		}
	}
}