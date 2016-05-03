using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SubstringCoreLib;

namespace SubstringSearching
{
	internal static class Program
	{
		private static void Main()
		{
			var algs = LibrariesManager.LoadAlgorithms();

			var text = LoadText();

			Console.Write("Что ищем? ");
			var toFind = Console.ReadLine();

			foreach (var substringFinder in algs)
			{
				substringFinder.SetSource(text);
				var positions = (substringFinder.Find(toFind) ?? new int[0]).ToArray();

				Console.WriteLine($"Найдено {positions.Length} встреч.");
				Console.WriteLine("Вывести? (Y/y/Д/д)");
				var decision = (Console.ReadLine() ?? "").ToLower();
				if (decision == "y" || decision == "д")
				{
					Console.WriteLine(positions.Select(item => Convert.ToString(item)).Aggregate((first, second) => first + ", " + second));
				}
			}

			Console.ReadKey();
		}

		private static string LoadText()
		{
			var file = Path.Combine(Environment.CurrentDirectory, "anna.txt");
			return File.ReadAllText(file);
		}
	}
}