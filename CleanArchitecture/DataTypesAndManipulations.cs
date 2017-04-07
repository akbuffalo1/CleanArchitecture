using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture
{
	public class DataTypesAndManipulations
	{
		void Main() 
		{
			double num1 = 1;
			object obj = num1;

			var result1 = (int)num1;
			var result2 = (int)(double)obj;
            
			string str = "asdasd";
			str += " qwqwqw";
			str += " zxczxvzxv";
			str = null;

			Console.WriteLine(str.Get(5));

            var some1 = new Some("Some1", new SomeOther());
            var some2 = new Some("Some2", new SomeOther());
            var some3 = new Some("Some3", new SomeOther());

            IEnumerable<Some> numbers = new List<Some>() { some1, some2 , some3 };

			Console.WriteLine(numbers.GetMax());
			var observable = Observable.Create<Some>(obs =>
			{
				foreach (var item in numbers)
				{
					obs.OnNext(item);
				}
				return Disposable.Empty;
			}).Subscribe(Some => Console.WriteLine(Some));
			observable.Dispose();
		}
	}
	public class SomeOther 
	{ 
		
	}

    public class Some : IComparable<Some>
    {
		public SomeOther Other { get; set; }

		private string _name;
        public Some(string name, SomeOther other) {
            _name = name;
			Other = other;
        }

        public int CompareTo(Some other)
        {
            return GetNum() > other.GetNum() ? 1 : 0;
        }

        public virtual int GetNum()
        {
            return _name[_name.Length - 1];   
        }

        public override string ToString() {
            return "Name:" + _name;
        }
    }

	public class Nullable<T> where T : struct
	{
		private object _value;
		public Nullable(T value)
		{
			_value = value;
		}

		public bool HasValue
		{
			get { return _value != null; }
		}

		public T GetValueOrDefault()
		{
			return (T)(_value ?? default(T));
		}
	}

	public static class ExtExample 
	{
		public static string Get(this string str, int count)
		{
			return str + count;
		}

		public static int GetMax<TSource>(this IEnumerable<TSource> source) where TSource : IComparable<TSource> 
		{
			return 3;
		}
	}
}
