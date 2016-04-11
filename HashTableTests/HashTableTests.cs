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
			Assert.AreEqual(4, _table.Count);
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
	}
}