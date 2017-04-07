using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CleanArchitecture
{
	public class StateRepoExample
	{
		const string MESSAGE = nameof(MESSAGE);

		readonly static StateRepository<State> StateRepo = new StateRepository<State>();
		void Main()
		{
			var example = new StateRepoExample();

			example.Subscribe();
			example.Send();
		}

		private void Subscribe()
		{
			MessagingCenter.Subscribe<StateRepoExample, string>(this, MESSAGE, (arg1, arg2) =>
			{
				var data = StateRepo.Remove(arg2).Data;

				Console.WriteLine($"Name:{data.Name}, Age:{data.Age}, Address:{data.Address}");
			});
		}

		private void Send()
		{
			var data = new CustomClass { Name = "CustomClass", Age = 3, Address = "CustomClassAddress" };
			var key = StateRepo.Add(new State { Data = data });
			MessagingCenter.Send<StateRepoExample, string>(this, MESSAGE, key);
		}
	}

	internal class State
	{
		public CustomClass Data;
	}

	class CustomClass
	{ 
		public string Name { get; set; }
		public int Age { get; set; }
		public string Address { get; set; }
	}


	public class StateRepository<T>
	{
		readonly Random rand = new Random();
		readonly Dictionary<string, T> states = new Dictionary<string, T>();

		public string Add(T state)
		{
			var key = rand.Next().ToString();
			while (states.ContainsKey(key))
			{
				key = rand.Next().ToString();
			}
			states[key] = state;
			return key;
		}

		public T Remove(string key)
		{
			if (states.ContainsKey(key))
			{
				var s = states[key];
				states.Remove(key);
				return s;
			}
			else {
				return default(T);
			}
		}
	}
}
