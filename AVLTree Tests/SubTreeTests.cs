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
			_subTree = new SubTree<int>(new Node<int>(5,0), null);
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
		public void AddItem_Test()
		{
			_subTree.Add(1);
			_subTree.Add(7);
			_subTree.Add(8);

			var foundOne = false;
			var foundSeven = false;
			var foundEight = false;
			foreach (var node in _subTree)
			{
				switch (node.Value)
				{
					case 1:
						foundOne = true;
						break;
					case 7:
						foundSeven = true;
						break;
					case 8:
						foundEight = true;
						break;
				}
			}

			Assert.AreEqual(true, foundOne);
			Assert.AreEqual(true, foundSeven);
			Assert.AreEqual(true, foundEight);
		}

		[TestMethod]
		public void ItemLevelAfterAdd_Test()
		{
			_subTree.Add(1);
			_subTree.Add(7);
			_subTree.Add(8);

			foreach (var node in _subTree)
			{
				switch (node.Value)
				{
					case 5:
						Assert.AreEqual(0, node.Level);
						break;
					case 1:
						Assert.AreEqual(1, node.Level);
						break;
					case 7:
						Assert.AreEqual(1, node.Level);
						break;
					case 8:
						Assert.AreEqual(2, node.Level);
						break;
				}
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "The same value already exists in tree.")]
		public void AddExistingItem_Test()
		{
			_subTree.Add(1);
			_subTree.Add(2);
			_subTree.Add(7);
			_subTree.Add(9);
			_subTree.Add(4);

			_subTree.Add(2); //Here exception required
		}

		[TestMethod]
		public void Contains_Test()
		{
			_subTree.Add(1);
			_subTree.Add(7);

			Assert.AreEqual(true, _subTree.Contains(5));
			Assert.AreEqual(true, _subTree.Contains(1));
			Assert.AreEqual(true, _subTree.Contains(7));

			Assert.AreEqual(false, _subTree.Contains(9));
		}

		[TestMethod]
		public void Height_Test()
		{
			Assert.AreEqual(0, _subTree.Height);

			_subTree.Add(7);
			Assert.AreEqual(1, _subTree.Height);

			_subTree.Add(8);
			Assert.AreEqual(2, _subTree.Height);

			_subTree.Add(3);
			Assert.AreEqual(2, _subTree.Height);

			_subTree.Add(1);
			Assert.AreEqual(2, _subTree.Height);

			_subTree.Add(4);
			Assert.AreEqual(2, _subTree.Height);

			_subTree.Add(-1);
			Assert.AreEqual(3, _subTree.Height);
		}

	}
}
