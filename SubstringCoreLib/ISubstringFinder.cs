using System;
using System.Collections.Generic;

namespace SubstringCoreLib
{
	public interface ISubstringFinder
	{
		void SetSource(string source);

		IEnumerable<int> Find(string what);

		string ToString();
	}
}