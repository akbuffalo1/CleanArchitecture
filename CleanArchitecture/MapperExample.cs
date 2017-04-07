using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture
{
	public class MapperExample
	{
		public static void Maini(string[] args)
		{
			var sourceList = new List<SourceClass> {
				new SourceClass{ Name="Name1", Age="10" },
				new SourceClass{ Name="Name2", Age="20" },
				new SourceClass{ Name="Name3", Age="30" },
				new SourceClass{ Name="Name4", Age="40" },
			};

			var mapper = new Mapper<SourceClass, ResultClass>(source => new ResultClass { 
				Description = "Name : " + source.Name + ", Age : "+ source.Age 
			});

			var resultList = sourceList.Select(source => mapper.Map(source)).ToList();

			resultList.ForEach(item => Console.WriteLine(item.Description));
		}
	}

	public class SourceClass { 
		public string Name { get; set;}
		public string Age { get; set; }
	}

	public class ResultClass
	{
		public string Description { get; set; }
	}

	public class Mapper<TSource, TResult> {
		private Func<TSource, TResult> _mapFunc;

		public Mapper(Func<TSource, TResult> mapFunc) 
		{
			_mapFunc = mapFunc;
		}
		public TResult Map(TSource source) {
			return _mapFunc(source);
		}
	}
}

