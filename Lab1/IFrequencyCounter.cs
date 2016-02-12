using System.Collections.Generic;

namespace Lab1
{
	public interface IFrequencyCounter
	{
		FreqResult GetCount(IEnumerable<string> words);
	}
}