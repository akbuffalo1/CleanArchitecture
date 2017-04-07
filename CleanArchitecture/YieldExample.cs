using System;
using System.Collections.Generic;

namespace CleanArchitecture
{
	public class YieldExample
	{
		static void Main_()
		{
			Console.WriteLine("Simple:");
			var list = Power(2, 8);
			foreach (int i in list)
			{
				Console.Write("{0} ", i);
			}
			Console.WriteLine("\n");

			Console.WriteLine("Yield:");
			foreach (int i in PowerYield(2, 8))
			{
				Console.Write("{0} ", i);
			}
			Console.WriteLine("\n");

			Console.WriteLine("Yield Second:");
			var enumerator = PowerYield(2, 8);
			foreach (int i in enumerator)
			{
				Console.Write("{0} ", i);
			}
			Console.WriteLine("\n");

			Console.WriteLine("Super Yield:");
			foreach (Galaxy theGalaxy in new Galaxies().NextGalaxy)
			{
				Console.WriteLine(theGalaxy.Name + " " + theGalaxy.MegaLightYears.ToString());
			}
		}

		public static IEnumerable<int> PowerYield(int number, int exponent)
		{
			int result = 1;

			for (int i = 0; i < exponent; i++)
			{
				result = result * number;
				if (i > 5) yield break;
				yield return result;
			}
		}

		public static List<int> Power(int number, int exponent)
		{
			int result = 1;
			var list = new List<int>();
			for (int i = 0; i < exponent; i++)
			{
				result = result * number;
				if (i > 5) return list;
				list.Add(result);
			}

			return list;
		}
	}

	public class Galaxies
	{
		public IEnumerable<Galaxy> NextGalaxy
		{
			get
			{
				yield return new Galaxy { Name = "Tadpole", MegaLightYears = 400 };
				yield return new Galaxy { Name = "Pinwheel", MegaLightYears = 25 };
				yield return new Galaxy { Name = "Milky Way", MegaLightYears = 0 };
				yield return new Galaxy { Name = "Andromeda", MegaLightYears = 3 };
			}
		}
	}

	public class Galaxy
	{
		public String Name { get; set; }
		public int MegaLightYears { get; set; }
	}
}

