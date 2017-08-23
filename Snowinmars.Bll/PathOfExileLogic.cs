using System;
using System.Collections.Generic;
using System.Linq;

namespace Snowinmars.Bll
{
	public class PathOfExileLogic
	{
		private readonly IList<IList<int>> combinations;

		public PathOfExileLogic()
		{
			this.combinations = new List<IList<int>>();
		}

		public IEnumerable<IList<int>> PickQualityCombination(IList<int> qualities, int desiredValue)
		{
			if (qualities.Count > 20)
			{
				throw new InvalidOperationException($"Can't handle qualities count: maximum allowed is 20 elements, but input was {qualities.Count} length");
			}

			int completeQualitiesCount = 0;
			IList<int> incompleteQualities = new List<int>();

			foreach (var quality in qualities)
			{
				if (quality == 20) // if you sell single item with 20% quality, you'll get orb as exchange
				{
					completeQualitiesCount++;
				}
				else
				{
					incompleteQualities.Add(quality);
				}
			}

			for (int i = 0; i < completeQualitiesCount; i++)
			{
				this.combinations.Add(new List<int> { 20 });
			}

			for (int i = 1; i <= incompleteQualities.Count; i++)
			{
				this.FillCombinations(incompleteQualities, i);
			}

			IEqualityComparer<IList<int>> c = new ListComparer();

			return this.combinations // for all combinations
				.Where(list => list.Sum() == desiredValue) // find some which have correct sum
				.Select(list => list.OrderBy(_ => _).ToList()) // order it by asc
				.Distinct(c) // and remove repeated sequence
				.Concat(this.combinations.Where(list => list.Count == 1 && list[0] == 20))
				;
		}

		/// <summary>
		/// Write all combinations for array to private list
		/// </summary>
		/// <param name="arr"></param>
		/// <param name="k">Size of a combination to be printed</param>
		private void FillCombinations(IList<int> arr, int k)
		{
			
			// A temporary array to store all combination one by one
			int[] data = new int[k];

			// Print all combination using temprary array 'data[]'
			this.FillCombinationsRecursive(arr: arr,
											data: data,
											start: 0,
											end: arr.Count - 1,
											index: 0,
											k: k);
		}

		/// <param name="arr">Input Array</param>
		/// <param name="data">Temporary array to store current combination</param>
		/// <param name="start">Staring indexes in arr[]</param>
		/// <param name="end">Ending indexes in arr[]</param>
		/// <param name="index">Current index in data[]</param>
		/// <param name="k">Size of a combination to be printed</param>
		private void FillCombinationsRecursive(IList<int> arr, IList<int> data, int start, int end, int index, int k)
		{
			// Current combination is ready to be saved
			if (index == k)
			{
				this.combinations.Add(data.Select(a => a).ToList()); // clone
				return;
			}

			// replace index with all possible elements. The condition
			// "end - i + 1 >= k - index" makes sure that including one element
			// at index will make a combination with remaining elements
			// at remaining positions
			for (int i = start; ((i <= end) && (end - i + 1 >= k - index)); i++)
			{
				data[index] = arr[i];
				this.FillCombinationsRecursive(arr: arr,
												data: data,
												start: i + 1,
												end: end,
												index: index + 1,
												k: k);
			}
		}

		private class ListComparer : IEqualityComparer<IList<int>>
		{
			public bool Equals(IList<int> x, IList<int> y)
			{
				return x.SequenceEqual(y);
			}

			public int GetHashCode(IList<int> obj)
			{
				unchecked
				{
					int hash = 19;

					foreach (var item in obj)
					{
						hash = hash * 31 + item.GetHashCode();
					}

					return hash;
				}
			}
		}
	}
}