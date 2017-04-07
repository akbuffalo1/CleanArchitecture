using System;
using System.Collections.Generic;

namespace test
{
	public class MessengingCenterExample
	{
		public static void Mainі(string[] args)
		{
			MessengingCenter<string>.Subscribe("hello", (data) =>
			{
				Console.WriteLine(data);
			});

			MessengingCenter<string>.Send("hello", "How are you");

			MessengingCenter<string>.UnSubscribe("hello");

			MessengingCenter<string>.Send("hello", "How are you");

			MessengingCenter<string>.Subscribe("hello", (data) =>
			{
				Console.WriteLine(data);
			});

			MessengingCenter<string>.Send("hello", "How are you");

			MessengingCenter<string>.UnSubscribe("hello");

			MessengingCenter<string>.Send("hello", "How are you");
		}
	}

	public class MessengingCenter<T> {
		private static readonly Dictionary<string, Dictionary<Type, Action<T>>> subcribers = new Dictionary<string, Dictionary<Type, Action<T>>>();

		public static void Subscribe(string message, Action<T> action) 
		{
			if (subcribers.ContainsKey(message))
			{
				subcribers[message].Add(typeof(T), action);
			}
			else 
			{
				var dict = new Dictionary<Type, Action<T>>();
				dict.Add(typeof(T), action);

				subcribers.Add(message, dict);
			}
		}

		public static void UnSubscribe(string message)
		{
			if (subcribers.ContainsKey(message) && subcribers[message].ContainsKey(typeof(T)))
			{
				subcribers[message].Remove(typeof(T));
			}
		}

		public static void Send(string message, T data) 
		{ 
			if (subcribers.ContainsKey(message) && subcribers[message].ContainsKey(typeof(T)))
			{
				subcribers[message][typeof(T)]?.Invoke(data);
			}
		}
	}
}

