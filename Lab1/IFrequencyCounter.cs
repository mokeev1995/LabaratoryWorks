using System.Collections.Generic;

namespace ConsoleApplication1
{
	public interface IFrequencyCounter
	{
		FreqResult GetCount(IEnumerable<string> words);
	}
}