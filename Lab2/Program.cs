using System;
using System.Linq;
using AVLTree;

namespace Lab2
{
	internal static class Program
	{
		private static void Main()
		{
			var tree = new AvlTree<int>();
			tree.Add(11);
			tree.Add(10);
			tree.Add(1);
			tree.Add(15);
			tree.Add(13);
			tree.Add(12);

			var vals = tree.ToArray();

			Console.ReadKey();
		}
	}
}