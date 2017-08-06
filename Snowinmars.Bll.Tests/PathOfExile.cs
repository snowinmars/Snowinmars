using NUnit.Framework;
using System.Linq;

namespace Snowinmars.Bll.Tests
{
	public class PathOfExile
	{
		private PathOfExileLogic pathOfExileLogic;

		[Theory]
		[TestCase(new[] { 15, 15, 10, 5, 5 }, 40, 2)]
		[TestCase(new[] { 1, 1 }, 2, 1)]
		[TestCase(new[] { 1, 1, 1 }, 4, null)]
		[TestCase(new[] { 19, 13, 15, 9, 17, 7 }, 40, null)]
		public void A(int[] values, int desiredValue, int? combinationsCount)
		{
			bool isPossible = combinationsCount.HasValue;

			var combinations = this.pathOfExileLogic.PickQualityCombination(values, desiredValue).ToList();

			Assert.False(!isPossible && combinations.Any(), "It's not possible to make a valid combinations, but logic returns something");
			Assert.False(isPossible && !combinations.Any(), "It's possible to make a valid combinations, but logic returns nothing");

			if (isPossible)
			{
				Assert.True(combinations.Count == combinationsCount, "Actual combinations count is not valid");

				foreach (var combination in combinations)
				{
					Assert.True(combination.Sum() == desiredValue, $"Actual sum of combination {string.Join(",", combination.Select(a => a.ToString()))} is not valid");
				}
			}
		}

		[SetUp]
		public void Setup()
		{
			this.pathOfExileLogic = new PathOfExileLogic();
		}
	}
}