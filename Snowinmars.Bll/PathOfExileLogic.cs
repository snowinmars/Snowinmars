using System;
using System.Collections.Generic;
using System.Linq;

namespace Snowinmars.Bll
{
	public class PathOfExileLogic
	{
		public IEnumerable<IList<int>> PickQualityCombination(IList<int> qualities, int desiredValue)
		{
			for (int i = 1; i <= qualities.Count; i++)
			{
				printCombination(qualities, qualities.Count, i);
			}

			return combinations.Where(list => list.Sum() == desiredValue);
		}

		// The main function that prints all combinations of size r
		// in arr[] of size n. This function mainly uses combinationUtil()
		static void printCombination(IList<int> arr, int n, int r)
		{
			// A temporary array to store all combination one by one
			int[] data = new int[r];

			// Print all combination using temprary array 'data[]'
			combinationUtil(arr, data, 0, n - 1, 0, r);
		}

		static IList<IList<int>> combinations = new List<IList<int>>();

		/* arr[]  ---> Input Array
		   data[] ---> Temporary array to store current combination
		   start & end ---> Staring and Ending indexes in arr[]
		   index  ---> Current index in data[]
		   r ---> Size of a combination to be printed */
		static void combinationUtil(IList<int> arr, IList<int> data, int start, int end, int index, int r)
		{
			// Current combination is ready to be printed, print it
			if (index == r)
			{
				PathOfExileLogic.combinations.Add(data.Select(a => a).ToList()); // clone
				return;
			}

			// replace index with all possible elements. The condition
			// "end-i+1 >= r-index" makes sure that including one element
			// at index will make a combination with remaining elements
			// at remaining positions
			for (int i = start; ((i <= end) && (end - i + 1 >= r - index)); i++)
			{
				data[index] = arr[i];
				combinationUtil(arr, data, i + 1, end, index + 1, r);
			}
		}
	}
}