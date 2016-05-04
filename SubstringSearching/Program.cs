using System;
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

			var sourceText = LoadText();

			Console.Write("Что ищем? ");
			var textToFind = Console.ReadLine();

			foreach (var substringFinder in algs)
			{
				substringFinder.SetSource(sourceText);

				var positions = (substringFinder.Find(textToFind) ?? new int[0]).ToArray();

				Console.WriteLine($"Найдено: {positions.Length}. Способ: {substringFinder.ToString()}");

				if (positions.Length <= 0) continue;

				Console.WriteLine("Вывести? (Y/y/N/n)");
				var decision = (Console.ReadLine() ?? "").ToLower();
				if (decision == "y" || decision == "д")
				{
					Console.WriteLine("\t" +
					                  positions.Select(item => Convert.ToString(item))
						                  .Aggregate((first, second) => first + ", \n\t" + second));
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