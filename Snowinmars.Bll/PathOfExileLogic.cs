using System;
using System.Collections.Generic;
using System.Linq;

namespace Snowinmars.Bll
{
	public class PathOfExileLogic
	{
		IList<IList<int>> qualityCombinations = new List<IList<int>>();
		
		/// <summary>
		/// I didn't write it, sorry.
		/// I just paste it from one math lovers forum
		/// Surely, I will rewrite this code, but not now.
		/// It works combine all input numbers in recursion cycle. Don't try to call this method on large arrays
		/// </summary>
		/// <param name="qualities"></param>
		/// <param name="s"></param>
		/// <param name="i"></param>
		/// <param name="p"></param>
		/// <param name="qualitiesLenght"></param>
		/// <param name="desiredValue"></param>
		/// <param name="corsize"></param>
		/// <returns></returns>
		private IList<IList<int>> PickQualityCombinationImplementation(IList<int> qualities,
			IList<int> s,
			int i,
			int p,
			int qualitiesLenght,
			int desiredValue,
			int corsize)
		{
			for (; i < qualitiesLenght; i++)
			{
				if (corsize + qualities[i] == desiredValue)
				{
					qualityCombinations.Add(new List<int>());
					var collection = qualityCombinations[qualityCombinations.Count - 1];

					for (int j = 0; j <= p; j++)
					{
						collection.Add(qualities[s[j]]);
						Console.Write("{0} + ", qualities[s[j]]);
					}

					collection.Add(qualities[i]);
					Console.WriteLine("{0} = {1};", qualities[i], desiredValue);
				}
				else
				{
					if (corsize + qualities[i] < desiredValue)
					{
						s[p + 1] = i;

						this.PickQualityCombinationImplementation(qualities,
							s,
							i + 1,
							p + 1,
							qualitiesLenght,
							desiredValue,
							corsize + qualities[i]);
					}
				}
			}

			return qualityCombinations;
		}

		/// <summary>
		/// At this version it works combine all input numbers in recursion cycle. Don't try to call this method on large arrays
		/// </summary>
		/// <param name="qualities">Less then 10 numbers, otherwise InvalidOperationException will be thrown for the server processor's safe</param>
		/// <param name="desiredValue"></param>
		/// <returns></returns>
		public IList<IList<int>> PickQualityCombination(IList<int> qualities, int desiredValue)
		{
			if (qualities.Count > 10)
			{
				throw new InvalidOperationException("This method can't proceed more then 20 numbers. Due to this code is from 2010 math forum. I'm sorry");
			}

			int[] s = new int[qualities.Count];

			return this.PickQualityCombinationImplementation(qualities.ToArray(), s, 0, -1, qualities.Count, desiredValue, 0);
		}
	}
}