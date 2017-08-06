using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Snowinmars.Bll.Tests
{
    public class PathOfExile
    {
	    private PathOfExileLogic pathOfExileLogic;

	    [SetUp]
	    public void Setup()
	    {
			this.pathOfExileLogic = new PathOfExileLogic();
		}

	    [Theory]
		[TestCase(new[] { 15, 15, 10, 5, 5 }, 40, 2)]
	    [TestCase(new[] { 1, 1 }, 2, 1)]
		[TestCase(new[] { 1, 1, 1 }, 4, null)]
		[TestCase(new[] { 19,13,15,9,17,7 }, 40, null)]
		public void A(int[] values, int desiredValue, int? combinationsCount)
	    {
		    bool isPossible = combinationsCount.HasValue;

		    var combinations = this.pathOfExileLogic.PickQualityCombination(values, desiredValue).ToList();

			Assert.False(!isPossible && combinations.Any(), "1");
		    Assert.False(isPossible && !combinations.Any(), "2");

		    if (isPossible)
		    {
			    Assert.True(combinations.Count == combinationsCount);

			    foreach (var combination in combinations)
			    {
				    Assert.True(combination.Sum() == desiredValue);
			    }
		    }
		}
	}
}
