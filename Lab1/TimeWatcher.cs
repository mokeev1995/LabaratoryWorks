using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
	public class TimeWatcher : IFrequencyCounter, IDisposable
	{
		private IFrequencyCounter _counter;
		private DateTime _startTime;
		private DateTime _endTime;
		private bool _disposed = false;

		public TimeWatcher(IFrequencyCounter counter)
		{
			_counter = counter;
		}

		public FreqResult GetCount(IEnumerable<string> words)
		{
			_startTime = DateTime.Now;
			var value = _counter.GetCount(words);
			_endTime = DateTime.Now;
			return value;
		}

		~TimeWatcher()
		{
			Dispose();
		}

		public void Dispose()
		{
			if(_disposed)
				return;
			var time = _endTime - _startTime;
			Console.WriteLine($"{_counter.GetType()} : \t{time.Minutes}:{time.Seconds}:{time.Milliseconds}");
			_disposed = true;
		}
	}
}