using System.Linq;
using KnuthMorrisPrattAlgorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KnuthMorrisPrattFinderTests
{
	[TestClass]
	public class KnuthMorrisPrattFinderTests
	{

		private KnuthMorrisPrattFinder _kmp;

		[TestInitialize]
		public void Init()
		{
			_kmp = new KnuthMorrisPrattFinder();
		}

		[TestMethod]
		public void TextSmallerThanSearchingString()
		{
			const string what = "1232123213";
			const string where = "123";

			_kmp.SetSource(where);
			var found = _kmp.Find(what);
			Assert.AreEqual(found.Count(), 0);
		}

		[TestMethod]
		public void ShouldFindTwoPositions()
		{
			const string what = "123";
			const string where = "1232123213";

			_kmp.SetSource(where);
			var found = _kmp.Find(what).ToArray();

			Assert.AreEqual(found.Length, 2);
			Assert.AreEqual(0UL, found[0]);
			Assert.AreEqual(4UL, found[1]);
		}

		[TestMethod]
		public void ShouldFindOnePosition()
		{
			const string what = "213";
			const string where = "1232123213";

			_kmp.SetSource(where);
			var found = _kmp.Find(what).ToArray();

			Assert.AreEqual(found.Length, 1);
			Assert.AreEqual(7UL, found[0]);
		}

		[TestMethod]
		public void ShouldNotFind()
		{
			const string what = "123456";
			const string where = "1232123213";

			_kmp.SetSource(where);
			var found = _kmp.Find(what).ToArray();

			Assert.AreEqual(found.Length, 0);
		}
	}
}