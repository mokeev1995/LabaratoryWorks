using System;
using System.Collections.Generic;
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
		public void CountAfterFirstRemove()
		{
			_table.Remove(5);
			Assert.AreEqual(0, _table.Count);
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
		[ExpectedException(typeof(KeyNotFoundException), "Key doesn't exists in hash table")]
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
			table.Add(8, new TestEq("8", 3));
			table.Add(9, new TestEq("9", 3));
			table.Add(10, new TestEq("10", 3));
			table.Add(11, new TestEq("11", 3));
			table.Add(12, new TestEq("12", 3));
			table.Add(13, new TestEq("13", 3));
			table.Add(14, new TestEq("14", 3));
			table.Add(15, new TestEq("15", 3));
			table.Add(16, new TestEq("16", 3));
			table.Add(17, new TestEq("17", 3));
			table.Add(18, new TestEq("18", 3));
			table.Add(19, new TestEq("19", 3));
			table.Add(20, new TestEq("20", 3));
			table.Add(-5, new TestEq("-5", 3));
			table.Add(21, new TestEq("2", 4));
			table.Add(22, new TestEq("4", 3));
			table.Add(23, new TestEq("3", 4));
			table.Add(24, new TestEq("5", 3));
			table.Add(25, new TestEq("6", 3));
			table.Add(26, new TestEq("7", 3));
			table.Add(27, new TestEq("8", 3));
			table.Add(28, new TestEq("9", 3));
			table.Add(29, new TestEq("10", 3));
			table.Add(30, new TestEq("11", 3));

			Assert.AreEqual("1", table[1].TestStr);
			Assert.AreEqual("2", table[2].TestStr);
			Assert.AreEqual("3", table[3].TestStr);
			Assert.AreEqual("4", table[4].TestStr);
			Assert.AreEqual("5", table[5].TestStr);
			Assert.AreEqual("6", table[6].TestStr);
			Assert.AreEqual("7", table[7].TestStr);
			Assert.AreEqual("8", table[8].TestStr);
			Assert.AreEqual("9", table[9].TestStr);
			Assert.AreEqual("10", table[10].TestStr);
			Assert.AreEqual("11", table[11].TestStr);
			Assert.AreEqual("12", table[12].TestStr);
			Assert.AreEqual("13", table[13].TestStr);
			Assert.AreEqual("14", table[14].TestStr);
			Assert.AreEqual("15", table[15].TestStr);
			Assert.AreEqual("16", table[16].TestStr);
			Assert.AreEqual("17", table[17].TestStr);
			Assert.AreEqual("18", table[18].TestStr);
			Assert.AreEqual("19", table[19].TestStr);
			Assert.AreEqual("20", table[20].TestStr);
		}

		[TestMethod]
		public void SameHashes()
		{
			var table = new HashTable<TestEq, int>();

			var f1 = new TestEq("1", 5);
			var f2 = new TestEq("2 ", 4);
			var f3 = new TestEq("3  ", 3);
			var f4 = new TestEq("4   ", 7);
			var f5 = new TestEq("5    ", 3);
			var f6 = new TestEq("6     ", 3);
			var f7 = new TestEq("7      ", 4);
			var f8 = new TestEq("8       ", 4);
			var f9 = new TestEq("9        ", 4);
			var f10 = new TestEq("10         ", 4);
			var f11 = new TestEq("11          ", 5);
			var f12 = new TestEq("12           ", 5);
			var f13 = new TestEq("13            ", 5);
			var f14 = new TestEq("14             ", 5);
			var f15 = new TestEq("15              ", 6);
			var f16 = new TestEq("16               ", 6);
			var f17 = new TestEq("17                ", 6);
			var f18 = new TestEq("18                 ", 6);


			table.Add(f1, 1);
			table.Add(f2, 2);
			table.Add(f3, 3);
			table.Add(f4, 4);
			table.Add(f5, 5);
			table.Add(f6, 6);
			table.Add(f7, 7);
			table.Add(f8, 8);
			table.Add(f9, 9);
			table.Add(f10, 10);
			table.Add(f11, 11);
			table.Add(f12, 12);
			table.Add(f13, 13);
			table.Add(f14, 14);
			table.Add(f15, 15);
			table.Add(f16, 16);
			table.Add(f17, 17);
			table.Add(f18, 18);

			Assert.AreEqual(1, table[f1]);
			Assert.AreEqual(2, table[f2]);
			Assert.AreEqual(3, table[f3]);
			Assert.AreEqual(4, table[f4]);
			Assert.AreEqual(5, table[f5]);
			Assert.AreEqual(6, table[f6]);
			Assert.AreEqual(7, table[f7]);
			Assert.AreEqual(8, table[f8]);
			Assert.AreEqual(9, table[f9]);
			Assert.AreEqual(10, table[f10]);
			Assert.AreEqual(11, table[f11]);
			Assert.AreEqual(12, table[f12]);
			Assert.AreEqual(13, table[f13]);
			Assert.AreEqual(14, table[f14]);
			Assert.AreEqual(15, table[f15]);
			Assert.AreEqual(16, table[f16]);
			Assert.AreEqual(17, table[f17]);
			Assert.AreEqual(18, table[f18]);
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
			return string.Compare(TestStr, other.TestStr, StringComparison.Ordinal);
		}

		public override string ToString()
		{
			return $"{Hash}";
		}
	}
}