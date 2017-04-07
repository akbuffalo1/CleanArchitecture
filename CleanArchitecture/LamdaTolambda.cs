using System;
namespace CleanArchitecture
{
	public class LamdaToLambda
	{
		public static void Maini(string[] args)
		{
			var name = "Hello";
			var executor = new Executor<string>(_ =>
			{
				Console.WriteLine(_ + " " + name);
			});
			executor.Execute(__ =>
			{
				__("Hello");
			});
		}
	}

	public class Executor<T> {
		private Action<T> _toDo;

		public Executor(Action<T> toDo) {
			_toDo = toDo;
		}

		public void Execute(Action<Action<T>> executeToDo) {
			executeToDo(_toDo);
		}
 	}
}

