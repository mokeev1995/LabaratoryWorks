using System;
using AVLTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AVLTree_Tests
{
	[TestClass]
	public class SubTreeTests
	{
		private SubTree<int> _subTree;

		[TestInitialize]
		public void Init()
		{
			_subTree = new SubTree<int>(new Node<int>(5,0));
		}

		[TestMethod]
		public void ValueCheck_Test()
		{
			Assert.AreEqual(5, _subTree.Data.Value);
		}

		[TestMethod]
		public void LevelCheck_Test()
		{
			Assert.AreEqual(0, _subTree.Data.Level);
		}

		[TestMethod]
		public void Add_Test()
		{
			_subTree.Add(1);
			_subTree.Add(7);

			var foundOne = false;
			var foundSeven = false;
			foreach (var node in _subTree)
			{
				if (node == 1)
				{
					foundOne = true;
				}
				else if (node == 7)
				{
					foundSeven = true;
				}
			}

			Assert.AreEqual(true, foundOne);
			Assert.AreEqual(true, foundSeven);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "The same value already exists in tree.")]
		public void AddExistingItem_Test()
		{
			_subTree.Add(5);
		}

		[TestMethod]
		public void Contains_Test()
		{
			_subTree.Add(1);
			_subTree.Add(7);

			Assert.AreEqual(true, _subTree.Contains(5));
			Assert.AreEqual(true, _subTree.Contains(1));
			Assert.AreEqual(true, _subTree.Contains(7));
		}
	}
}
