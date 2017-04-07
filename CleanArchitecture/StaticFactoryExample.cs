using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace test
{
	public class StaticFactoryExample
	{
		public static void Maini(string[] args) {
			InputRetriever.RegisterInstance(name => name.StartsWith("http"), new FileInputRetriever());
			InputRetriever.RegisterInstance(name => name.StartsWith("file"), new CacheInputRetriever());
			InputRetriever.RegisterInstance(name => true, new DefaultInputRetriever());

			var retriever1 = InputRetriever.FromName("http://something.com/index.json");
			var retriever2 = InputRetriever.FromName("file://something/folder/filename");
		}
	}

	public interface IInputretriever
	{
		string ObtainData();
	}

	public abstract class InputRetriever : IInputretriever
	{
		private static Dictionary<Func<string, bool>, IInputretriever> implementationsList = new Dictionary<Func<string, bool>, IInputretriever>();

		public static void RegisterInstance(Func<string, bool> evaluator, IInputretriever instance)
		{
			implementationsList.Add(evaluator, instance);
		}

		public static IInputretriever FromName(string fileName)
		{
			return implementationsList.FirstOrDefault((keyVal) => keyVal.Key(fileName)).Value;
		}

		public abstract string ObtainData();
	}

	public class FileInputRetriever : InputRetriever
	{
		public override string ObtainData()
		{
			throw new NotImplementedException();
		}
	}

	public class CacheInputRetriever : InputRetriever
	{
		public override string ObtainData()
		{
			throw new NotImplementedException();
		}
	}

	public class DefaultInputRetriever : InputRetriever
	{
		public override string ObtainData()
		{
			throw new NotImplementedException();
		}
	}
}

