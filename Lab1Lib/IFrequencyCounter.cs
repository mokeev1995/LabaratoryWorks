using System.Collections.Generic;

namespace Lab1Lib
{
	public interface IFrequencyCounter
	{
		FreqResult GetCount(IEnumerable<string> words);
	}
}