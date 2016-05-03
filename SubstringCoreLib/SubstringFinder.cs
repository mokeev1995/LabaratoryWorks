using System;
using System.Collections.Generic;

namespace SubstringCoreLib
{
	public abstract class SubstringFinder : ISubstringFinder
	{
		private string _source = string.Empty;

		public void SetSource(string source)
		{
			_source = source;
		}

		public abstract IEnumerable<int> Find(string what);
	}
}