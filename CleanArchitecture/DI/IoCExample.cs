using System;
namespace CleanArchitecture.DI
{
	public class IoCExample
	{
		public static void Maini(string[] args)
		{

			SomeInterface @someClass = null;
			SomeInterface someClass2 = null;
			SomeInterface someClass3 = null;

			var resolver = new IocResolver(new IocContainer());

			//resolver.Register<SomeInterface>(new SomeClass());
			//Lazy initialization
			resolver.Register<SomeInterface>(() => new SomeAnotherClass());

			Console.WriteLine("resolver.Register<SomeInterface>(() => new SomeAnotherClass());");

			@someClass = resolver.Resolve<SomeInterface>();
			someClass2 = resolver.Resolve<SomeInterface>();
			someClass3 = resolver.Resolve<SomeInterface>();
		
			Console.WriteLine("asas");
			Console.WriteLine(@someClass?.Description);
			Console.WriteLine(someClass2?.Description);
			Console.WriteLine(someClass3?.Description);
		}
	}

	public interface SomeInterface {
		string Description { get; set; } 
	}

	public class SomeClass : SomeInterface {
		public SomeClass() {
			Console.WriteLine("SomeClass has been created");
		}
		public string Description { get; set; } = "SomeClass Description";

	}

	public class SomeAnotherClass : SomeInterface {
		public SomeAnotherClass()
		{
			Console.WriteLine("SomeAnotherClass has been created");
		}
		public string Description { get; set; } = "SomeAnotherClass Description";
	}
}

