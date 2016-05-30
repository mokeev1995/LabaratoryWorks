using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GraphLib;

namespace Lab7
{
	internal static class Program
	{
		private static void Main()
		{
			var text = new[]
			{
				"5",
				"1,3;4,1;",
				"0,3;2,5;4,4;",
				"1,5;3,2;4,6;",
				"2,2;4,7;",
				"0,1;1,4;2,6;3,7;",
			};

			/*var file = Environment.CurrentDirectory + "TestData.txt";
			if (File.Exists(file))
				text = File.ReadAllLines(file);*/

			var graph = Graph.Build(text);

			var mst = graph.GetMst();

			Console.WriteLine($"Weight: {mst.Weight}. Included points: {PrintRoute(mst.PointsRoute)}");

			Console.ReadKey();
		}

		private static string PrintRoute(IEnumerable<Point> points)
		{
			var text = new StringBuilder();
			foreach (var point in points)
			{
				text.Append(point.Number + ", ");
			}

			text.Remove(text.Length - 2, 2);

			return text.ToString();
		}
	}
}