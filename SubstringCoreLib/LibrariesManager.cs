using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SubstringCoreLib
{
	public static class LibrariesManager
	{
		public static string LibrariesDir => Path.Combine(Environment.CurrentDirectory, "Algorithms");

		public static IEnumerable<ISubstringFinder> LoadAlgorithms()
		{
			var algs = new List<ISubstringFinder>();

			if (!Directory.Exists(LibrariesDir))
				return algs;

			var dllFiles = Directory.GetFiles(LibrariesDir).Where(file => Path.GetExtension(file) == ".dll");

			var interfaceType = typeof(ISubstringFinder);
			foreach (var file in dllFiles)
			{
				var asm = Assembly.LoadFrom(file);
				var instances =
					asm.GetExportedTypes()
						.Where(type => interfaceType.IsAssignableFrom(type))
						.Select(Activator.CreateInstance)
						.Cast<ISubstringFinder>()
						.ToArray();
				if (instances.Length > 0)
					algs.AddRange(instances);
			}


			return algs;
		}
	}
}