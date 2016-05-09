using System;
using GraphLib;

namespace Lab7
{
	internal static class Program
	{
		private static void Main()
		{
			var text = new[]
			{
				"7",
				"1,4",
				"0,4",
			};

			var graph = Graph.Build(text);

			Console.ReadKey();
		}
	}
}