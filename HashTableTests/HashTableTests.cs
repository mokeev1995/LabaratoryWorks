using System;
using HashTableLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HashTableTests
{
	[TestClass]
	public class HashTableTests
	{
		private HashTable<int, string> _table;

		[TestInitialize]
		public void Init()
		{
			_table = new HashTable<int, string>();
			_table.Add(5, "test");
		}

		[TestMethod]
		public void AddingItemTest()
		{
			Assert.AreEqual("test", _table[5]);
		}

		[TestMethod]
		public void CountAfterFirstAdd()
		{
			Assert.AreEqual(1, _table.Count);
		}

		[TestMethod]
		public void CountAfterMultipleAdd()
		{
			_table.Add(7, "test123");
			_table.Add(4, "test123");
			_table.Add(1, "test123");
			_table.Add(-1, "test123");
			_table.Add(-4, "test123");
			Assert.AreEqual(6, _table.Count);

			Assert.AreEqual("test123", _table[7]);
			Assert.AreEqual("test123", _table[4]);
			Assert.AreEqual("test123", _table[1]);
			Assert.AreEqual("test123", _table[-1]);
			Assert.AreEqual("test123", _table[-4]);

		}

		[TestMethod]
		public void RemoveItem()
		{
			_table.Remove(5);

			Assert.AreEqual(0, _table.Count);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "Key doesn't exists in hash table")]
		public void AccessRemovedItem()
		{
			_table.Remove(5);

			var tmp = _table[5];
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "This key already exists")]
		public void AddExistingItem()
		{
			_table.Add(5, "test");
		}

		[TestMethod]
		public void ManyKeys()
		{
			var table = new HashTable<int, TestEq>();
			table.Add(1, new TestEq("1", 5));
			table.Add(2, new TestEq("2", 4));
			table.Add(4, new TestEq("4", 3));
			table.Add(3, new TestEq("3", 4));
			table.Add(5, new TestEq("5", 3));
			table.Add(6, new TestEq("6", 3));
			table.Add(7, new TestEq("7", 3));

			Assert.AreEqual("1", table[1].TestStr);
			Assert.AreEqual("2", table[2].TestStr);
			Assert.AreEqual("3", table[3].TestStr);
			Assert.AreEqual("4", table[4].TestStr);
			Assert.AreEqual("5", table[5].TestStr);
			Assert.AreEqual("6", table[6].TestStr);
			Assert.AreEqual("7", table[7].TestStr);
		}

		[TestMethod]
		public void SameHashes()
		{
			var table = new HashTable<TestEq, int>();

			var f1 = new TestEq("1", 5);
			var f2 = new TestEq("2", 4);
			var f3 = new TestEq("3", 3);
			var f4 = new TestEq("4", 4);
			var f5 = new TestEq("5", 3);
			var f6 = new TestEq("6", 3);
			var f7 = new TestEq("7", 3);

			table.Add(f1, 1);
			table.Add(f2, 2);
			table.Add(f3, 3);
			table.Add(f4, 4);
			table.Add(f5, 5);
			table.Add(f6, 6);
			table.Add(f7, 7);

			Assert.AreEqual("1", table[f1]);
			Assert.AreEqual("2 ", table[f2]);
			Assert.AreEqual("3  ", table[f3]);
			Assert.AreEqual("4   ", table[f4]);
			Assert.AreEqual("5    ", table[f5]);
			Assert.AreEqual("6     ", table[f6]);
			Assert.AreEqual("7      ", table[f7]);
		}
	}

	internal class TestEq : IComparable
	{
		public string TestStr { get; set; }
		private int Hash { get; }

		public TestEq(string testStr, int hash)
		{
			TestStr = testStr;
			Hash = hash;
		}

		public override int GetHashCode()
		{
			return Hash;
		}

		public int CompareTo(object obj)
		{
			var other = (TestEq) obj;
			var res = TestStr.Length + Hash - other.Hash + other.TestStr.Length;
			return res == 0
				? 0
				: res < 0
					? -1
					: 1;
		}

		public override string ToString()
		{
			return $"TestStr: {TestStr}, Hash: {Hash}";
		}
	}
}