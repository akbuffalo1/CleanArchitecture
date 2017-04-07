using System;
namespace CleanArchitecture
{
	public class DecoratorORFacadeExample
	{
		public void Main()
		{
			ISome some = new Somme();
			Console.WriteLine(some.GetName());

			ISomeDecorator decorated = new SomeDecorator(some);
			Console.WriteLine(decorated.GetName());
			decorated.PrintName();
		}
	}

	public interface ISome
	{
		string GetName();
	}

	class Somme : ISome
	{
		public string GetName()
		{
			return nameof(Some);
		}
	}

	public interface ISomeDecorator : ISome
	{
		void PrintName();
	}

	public class SomeDecorator : ISomeDecorator
	{
		private ISome _instance;
		public SomeDecorator(ISome instance) {
			_instance = instance;
		}

		public string GetName()
		{
			return _instance.GetName() + "Decorated";
		}

		public void PrintName()
		{
			Console.WriteLine(GetName());
		}
	}
}
