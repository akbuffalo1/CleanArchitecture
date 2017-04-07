using System;
namespace CleanArchitecture
{
	public class NewExample
	{
		public static void Main_() {
			MyDelegate<Car> del = DelegateImpl<Car>;
			var car = del();
		}


		delegate T MyDelegate<T>() where T : new();
		
		public static T DelegateImpl<T>() where T : new() 
		{
			return new T();
		}
	}


	// One factory way
	public interface IFactory<T> {
		T CreateInstance(string model);
	}

	public class CarFactory : IFactory<Car>
	{
		public Car CreateInstance(string model)
		{
			return new Car() { Model = "Mercedes Benz" };
		}
	}
	// Second factory way
	public class Factory<T> where T : Car, new() 
	{
		public T CreateInstance(string model)
		{
			return new T() { Model = model };
		}
	}

	public class Car 
	{ 
		public string Model { get; set; }
	}
}

