using AVLTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AVLTree_Tests
{
	[TestClass]
	public class AvlTreeTests
	{
		private AvlTree<int> _tree;

		[TestInitialize]
		public void Init()
		{
			_tree = new AvlTree<int>();
		}

		[TestMethod]
		public void Count_Test()
		{
			Assert.AreEqual(0, _tree.Count);

			_tree.Add(5);
			Assert.AreEqual(1, _tree.Count);

			_tree.Add(1);
			Assert.AreEqual(2, _tree.Count);

			_tree.Add(7);
			Assert.AreEqual(3, _tree.Count);
		}

		[TestMethod]
		public void Foreach_Tests()
		{
			_tree.Add(5);
			_tree.Add(1);
			_tree.Add(7);
			_tree.Add(8);

			var idx = 0;
			foreach (var val in _tree)
			{
				switch (idx)
				{
					case 0:
						Assert.AreEqual(5, val.Value);
						break;
					case 1:
						Assert.AreEqual(1, val.Value);
						break;
					case 2:
						Assert.AreEqual(7, val.Value);
						break;
					case 3:
						Assert.AreEqual(8, val.Value);
						break;
				}

				idx++;
			}
		}

		[TestMethod]
		public void For_Test()
		{
			_tree.Add(5);
			Assert.AreEqual(5, _tree[0].Value);

			_tree.Add(1);
			Assert.AreEqual(1, _tree[1].Value);

			_tree.Add(7);
			Assert.AreEqual(7, _tree[2].Value);
		}
	}
}