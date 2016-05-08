using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabinKarpAlgorithm;

namespace RabinKarpFinderTests
{
	[TestClass]
	public class RabinKarpFinderTests
	{
		private RabinKarpFinder _rk;

		[TestInitialize]
		public void Init()
		{
			_rk = new RabinKarpFinder();
		}

		[TestMethod]
		public void TextSmallerThanSearchingString()
		{
			const string what = "1232123213";
			const string where = "123";

			_rk.SetSource(where);
			var found = _rk.Find(what);
			Assert.AreEqual(0, found.Count());
		}

		[TestMethod]
		public void ShouldFindTwoPositions()
		{
			const string what = "123";
			const string where = "1232123213";

			_rk.SetSource(where);
			var found = _rk.Find(what).ToArray();

			Assert.AreEqual(found.Length, 2);
			Assert.AreEqual(0UL, found[0]);
			Assert.AreEqual(4UL, found[1]);
		}

		[TestMethod]
		public void ShouldFindOnePosition()
		{
			const string what = "213";
			const string where = "1232123213";

			_rk.SetSource(where);
			var found = _rk.Find(what).ToArray();

			Assert.AreEqual(found.Length, 1);
			Assert.AreEqual(7UL, found[0]);
		}

		[TestMethod]
		public void ShouldNotFind()
		{
			const string what = "123456";
			const string where = "1232123213";

			_rk.SetSource(where);
			var found = _rk.Find(what).ToArray();

			Assert.AreEqual(found.Length, 0);
		}

		[TestMethod]
		public void ShouldNotFindAnything()
		{
			const string what = "tmp.Sum(d => d)";
			string where = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "anna.txt"));

			_rk.SetSource(where);
			var found = _rk.Find(what).ToArray();

			Assert.AreEqual(found.Length, 0);
		}
	}
}