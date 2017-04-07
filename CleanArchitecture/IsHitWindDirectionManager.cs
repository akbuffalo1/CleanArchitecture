using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanArchitecture
{
	public class IsHitWindDirectionManagerExample
	{
		public void Main(string[] args)
		{
			var windDirectionManager = new WindDirectionManager();

			windDirectionManager.Add(new WindDirection(deg => deg > 0 && deg <= 90, "Сереро-восток"));
			windDirectionManager.Add(new WindDirection(deg => deg > 90 && deg <= 180, "Юго-восток"));
			windDirectionManager.Add(new WindDirection(deg => deg > 180 && deg <= 270, "Юго-запад"));
			windDirectionManager.Add(new WindDirection(deg => deg > 270 && deg <= 360, "Северо-запад"));

			windDirectionManager.Add(new WindDirection(null, null));

			Console.OutputEncoding = Encoding.UTF8;
			Console.WriteLine(windDirectionManager.GetDirection(20).Name);
			Console.WriteLine(windDirectionManager.GetDirection(120).Name);
			Console.WriteLine(windDirectionManager.GetDirection(200).Name);
			Console.WriteLine(windDirectionManager.GetDirection(320).Name);
		}
	}

	public class WindDirectionManager : List<WindDirection>{
		public WindDirection GetDirection(int deg) {
			return this.FirstOrDefault(item => item.IsHit(deg));
		}
	}

	public class WindDirection {
		private Func<int, bool> _predicat;
		public string Name { get; protected set; }
		public WindDirection(Func<int, bool> predicat, string name) 
		{
			_predicat = predicat ?? (deg => false);
			Name = name ?? "Doesn't defined..";
		}

		public bool IsHit(int deg) {
			return _predicat(deg);
		}
	}
}

