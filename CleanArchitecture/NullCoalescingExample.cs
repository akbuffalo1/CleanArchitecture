using System;
namespace CleanArchitecture
{
	public class NullCoalescingExample
	{
		public static void Maini(string[] args)
		{
			var humane = new Entity("Peter");

			var name = humane?.GetName();
		}
	}

	public class Entity {
		object _name;

		public Entity(object name) {
			_name = name;
		}

		public string GetName() {
			return _name as String ?? "Default";
		}
	}
}

