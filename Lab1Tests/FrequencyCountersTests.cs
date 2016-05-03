using System.Collections.Generic;
using System.Linq;
using Lab1Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab1Tests
{
	[TestClass]
	public class FrequencyCountersTests
	{
		private string[] _words;

		[TestInitialize]
		public void Init()
		{
			_words = new[]
			{
				"test",
				"test1",
				"test",
				"asd",
				"das",
				"aaaa",
				"dsada",
				"qwe",
				"rty",
				"hgf",
				"ghjkghjk",
				"mvbnmvbmn",
				"qwerty",
				"test1",
				"test"
			};
		}

		[TestMethod]
		public void DictFreq_Test()
		{
			var dict = new DictFreq();
			var data = dict.GetCount(_words);

			var expected = new FreqResult(12,
				new[]
				{
					new KeyValuePair<string, int>("test", 3),
					new KeyValuePair<string, int>("test1", 2),
					new KeyValuePair<string, int>("asd", 1),
					new KeyValuePair<string, int>("das", 1),
					new KeyValuePair<string, int>("aaaa", 1),
					new KeyValuePair<string, int>("dsada", 1),
					new KeyValuePair<string, int>("qwe", 1),
					new KeyValuePair<string, int>("rty", 1),
					new KeyValuePair<string, int>("hgf", 1),
					new KeyValuePair<string, int>("ghjkghjk", 1)
				});

			Assert.AreEqual(true, FirstEqual(expected.TopResults, data.TopResults, 2));
			Assert.AreEqual(expected.Count, data.Count);
		}

		[TestMethod]
		public void SortedDictFreq_Test()
		{
			var dict = new SortedDictFreq();
			var data = dict.GetCount(_words);

			var expected = new FreqResult(12,
				new[]
				{
					new KeyValuePair<string, int>("test", 3),
					new KeyValuePair<string, int>("test1", 2),
					new KeyValuePair<string, int>("rty", 1),
					new KeyValuePair<string, int>("qwerty", 1),
					new KeyValuePair<string, int>("qwe", 1),
					new KeyValuePair<string, int>("mvbnmvbmn", 1),
					new KeyValuePair<string, int>("hgf", 1),
					new KeyValuePair<string, int>("ghjkghjk", 1),
					new KeyValuePair<string, int>("dsada", 1),
					new KeyValuePair<string, int>("das", 1)
				});

			Assert.AreEqual(true, FirstEqual(expected.TopResults, data.TopResults, 2));
			Assert.AreEqual(expected.Count, data.Count);
		}

		[TestMethod]
		public void ListFreq_Test()
		{
			var dict = new ListFreq();
			var data = dict.GetCount(_words);

			var expected = new FreqResult(12,
				new[]
				{
					new KeyValuePair<string, int>("test", 3),
					new KeyValuePair<string, int>("test1", 2),
					new KeyValuePair<string, int>("hgf", 1),
					new KeyValuePair<string, int>("rty", 1),
					new KeyValuePair<string, int>("ghjkghjk", 1),
					new KeyValuePair<string, int>("qwerty", 1),
					new KeyValuePair<string, int>("mvbnmvbmn", 1),
					new KeyValuePair<string, int>("das", 1),
					new KeyValuePair<string, int>("asd", 1),
					new KeyValuePair<string, int>("aaaa", 1)
				});

			Assert.AreEqual(true, FirstEqual(expected.TopResults, data.TopResults, 2));
			Assert.AreEqual(expected.Count, data.Count);
		}

		[TestMethod]
		public void SortedListFreq_Test()
		{
			var dict = new SortedListFreq();
			var data = dict.GetCount(_words);

			var expected = new FreqResult(12,
				new[]
				{
					new KeyValuePair<string, int>("test", 3),
					new KeyValuePair<string, int>("test1", 2),
					new KeyValuePair<string, int>("rty", 1),
					new KeyValuePair<string, int>("qwerty", 1),
					new KeyValuePair<string, int>("qwe", 1),
					new KeyValuePair<string, int>("mvbnmvbmn", 1),
					new KeyValuePair<string, int>("hgf", 1),
					new KeyValuePair<string, int>("ghjkghjk", 1),
					new KeyValuePair<string, int>("dsada", 1),
					new KeyValuePair<string, int>("das", 1)
				});

			Assert.AreEqual(true, FirstEqual(expected.TopResults, data.TopResults, 2));
			Assert.AreEqual(expected.Count, data.Count);
		}

		private bool FirstEqual(IEnumerable<KeyValuePair<string, int>> firstItems,
			IEnumerable<KeyValuePair<string, int>> secondItems, int count)
		{
			var firstData = firstItems.ToArray();
			var secondData = secondItems.ToArray();

			for (var i = 0; i < count; i++)
			{
				if (firstData[i].Key != secondData[i].Key)
					return false;
				if (firstData[i].Value != secondData[i].Value)
					return false;
			}

			return true;
		}
	}
}