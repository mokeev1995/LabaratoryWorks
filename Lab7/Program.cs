using System;
using System.Collections.Generic;
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

			var graph = Graph.Build(text);

			Console.ReadKey();
		}
	}
}