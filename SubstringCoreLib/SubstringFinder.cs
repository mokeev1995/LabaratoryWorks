using System;
using System.Collections.Generic;

namespace SubstringCoreLib
{
	public abstract class SubstringFinder : ISubstringFinder
	{
		protected string SourceText = string.Empty;

		public void SetSource(string source)
		{
			SourceText = source;
		}

		public abstract IEnumerable<ulong> Find(string what);

		public abstract override string ToString();
	}
}