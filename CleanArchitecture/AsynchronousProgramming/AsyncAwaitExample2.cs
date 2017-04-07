using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.AsynchronousProgramming
{
	public class AsyncAwaitExample2
	{
		static void Main_()
		{
			BeginWork().Wait();
		}

		public async static Task BeginWork() {
			var i = 0;
			Console.WriteLine("Start on Thread {0}", Thread.CurrentThread.ManagedThreadId);
			while (true)
			{ 
				var result = await Task.Run(() => 
				{
					Console.WriteLine("Execute on Thread {0}", Thread.CurrentThread.ManagedThreadId);
					Task.Delay(1000);
					return ++i;
				});
				Console.WriteLine("Result on Thread {0} => {1}", Thread.CurrentThread.ManagedThreadId, result);
			}
		}
	}
}

