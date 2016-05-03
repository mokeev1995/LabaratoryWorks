using System.Collections.Generic;

namespace SubstringCoreLib
{
	public interface ISubstringFinder
	{
		string Source { get; }

		void SetSource(string sourse);

		IEnumerable<int> Find();
	}
}