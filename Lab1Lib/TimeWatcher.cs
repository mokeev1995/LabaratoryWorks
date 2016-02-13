using System;
using System.Collections.Generic;

namespace Lab1Lib
{
	public class TimeWatcher : IFrequencyCounter, IDisposable
	{
		private readonly IFrequencyCounter _counter;
		private bool _disposed;
		private DateTime _endTime;
		private DateTime _startTime;

		public TimeWatcher(IFrequencyCounter counter)
		{
			_counter = counter;
		}

		public void Dispose()
		{
			if (_disposed)
				return;
			var time = _endTime - _startTime;
			Console.WriteLine($"{_counter.GetType()} : \t{time.Minutes}:{time.Seconds}:{time.Milliseconds}");
			_disposed = true;
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
	}
}