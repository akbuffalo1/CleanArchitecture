using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web.Routing;
using Microsoft.CSharp;
using Newtonsoft.Json.Linq;

namespace CleanArchitecture
{
	public class DynamicExample
	{
	    public void Main(params string[] args)
	    {
	        object o = new
	        {
	            name = "theName",
	            props = new
	            {
	                p1 = "prop1",
	                p2 = "prop2"
	            }
	        };

	        dynamic d = new
	        {
	            name = "theDynamicName",
	            props = new
	            {
	                p1 = "theDynamicprop1",
	                p2 = "theDynamicprop2"
	            }
	        };

			var something = new { Age = 12, Name = "name111111" };
			dynamic otherthing = new { Age = 12, Name = "name111111" };

			var name = something.Name;
			var city = otherthing.Age;


	        new Dictionary<int, string>
	        {

	            {1, "asas"},
	            {2, "asas"},
	            {3, "asas"}
	        };


	        var rvd = new RouteValueDictionary(o);

//Does not work:
	        Console.WriteLine(d.name);
	        Console.WriteLine(d.props.p1);

//DOES work!
	        Console.WriteLine(rvd["name"]);

//Does not work
	        //Console.WriteLine(rvd["props"].p1);
	        //Console.WriteLine(rvd["props"]["p1"]);
	    }


	    abstract class AEmpty<T> where T : AEmpty<T>, new()
		{
			public static T Empty =>  new T { IsEmpty = true };
			public bool IsEmpty { get; set; } = false;
		}

		class SomeClass : AEmpty<SomeClass>
		{
			public override string ToString() => "HELLO SOMECLASS";
		}

		static void PrintData(dynamic product)
		{
			var type = product.GetType();
			var property = type.GetProperty("Title");
			var name = property?.GetValue(product, null);
			var age = product.Age ?? -1;
			var some = product.Some;
			var other = (product.Other?.HasValue ?? false) ? product.Other.Value : SomeClass.Empty;

			Console.WriteLine(other.ToString());
		}
	}

}

