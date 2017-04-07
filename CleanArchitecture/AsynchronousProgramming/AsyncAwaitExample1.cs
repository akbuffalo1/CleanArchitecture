using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture
{
	public class AsyncAwaitExample1
	{
		static void Main_()
		{
			Console.WriteLine("Start working");
			var worker = new Worker();
			var cancellationSource = worker.CancelationSource;
			var toCalculate = new string[]
				{
					"https://www.google.com.ua/?gfe_rd=cr&ei=jCqzV-LjHKOt8weZj5_YCA#q=0",
					"https://www.google.com.ua/?gfe_rd=cr&ei=jCqzV-LjHKOt8weZj5_YCA#q=1",
					"https://www.google.com.ua/?gfe_rd=cr&ei=jCqzV-LjHKOt8weZj5_YCA#q=2",
					"https://www.google.com.ua/?gfe_rd=cr&ei=jCqzV-LjHKOt8weZj5_YCA#q=3",
					"https://www.google.com.ua/?gfe_rd=cr&ei=jCqzV-LjHKOt8weZj5_YCA#q=4",
					"https://www.google.com.ua/?gfe_rd=cr&ei=jCqzV-LjHKOt8weZj5_YCA#q=5",
			};
			worker.DoWork(toCalculate);
			var counter = 0;
			while (!worker.IsComplited)
			{
				if (counter > 20)
					cancellationSource.Cancel();
				Thread.Sleep(50);
				Console.Write(".");
				counter++;
			}
			Console.WriteLine("");
			Console.WriteLine("All work done");
			var i = 0;
			foreach (var res in worker.Result)
			{
				Console.WriteLine(res + " of " + toCalculate[i++]);
			}
		}
	}

	public class Worker 
	{
		private bool _isComplited;
		public bool IsComplited 
		{ 
			get  { return _isComplited; }
		}

		public int[] Result { get; private set; }

		public CancellationTokenSource CancelationSource { get; private set; }

		public Worker()
		{ 
			CancelationSource = new CancellationTokenSource();
		}

		public async void DoWork(string[] urls)
		{
			Result = new int[urls.Length];
			_isComplited = false;
			var i = 0;
			foreach (var url in urls)
			{
				if (CancelationSource.Token.IsCancellationRequested)
				{ 
					_isComplited = true;
					Console.WriteLine("\n\n CANCELLED \n\n");
					break;
				}
				// First Example
				/*
				try
				{
					var task = await DoLongOperationAsyncThrows(i);
					Result[i] = task;
				}
				catch(Exception ex)
				{
					Console.WriteLine("\nExeption occured: " + ex.Message);
					Result[i] = -1;
				}
				*/
				// Second Example

				try
				{
					var result = await Task.Factory.StartNew(async () =>
					{
						return await RunAsync(() => DoLongOperationAction(i));
					});
					Result[i] = result.Result;
				}
				catch (Exception ex)
				{
					Console.WriteLine("\nExeption occured: " + ex.InnerException.Message);
					Result[i] = -5;
				}

				i++;
			}
			_isComplited = true;
		}

		public static async Task<T> RunAsync<T>(Func<T> function)
		{
			if (function == null) throw new ArgumentNullException("function");
			var tcs = new TaskCompletionSource<T>();
			ThreadPool.QueueUserWorkItem(_ =>
			{
				try
				{
					T result = function();
					tcs.SetResult(result);
				}
				catch (Exception exc) 
				{ 
					tcs.SetException(exc); 
				}
			});
			return await tcs.Task;
		}

		public int DoLongOperationAction(int counter)
		{ 
			var toFetch = "https://www.google.com.ua/?gfe_rd=cr&ei=jCqzV-LjHKOt8weZj5_YCA#q=" + counter;
			if (counter > 1 && counter % 3 == 0)
			{
				throw new Exception("Service unavailable - " + toFetch);
				//return -5;
			}
			else
			{
				var result = new WebClient().DownloadStringTaskAsync(toFetch);
				return result.Result.Length;
			}
		}

		public Task<int> DoLongOperationAsyncCompletion(int counter)
		{
			var tasCompletionSource = new TaskCompletionSource<int>();
			var toFetch = "https://www.google.com.ua/?gfe_rd=cr&ei=jCqzV-LjHKOt8weZj5_YCA#q=" + counter;
			try
			{
				if (counter > 1 && counter % 3 == 0)
				{
					throw new Exception("Service unavailable - " + toFetch);
				}
				else
				{
					var result = new WebClient().DownloadStringTaskAsync(toFetch);
					tasCompletionSource.SetResult(result.Result.Length);
				}
			}
			catch (Exception ex)
			{
				tasCompletionSource.SetException(ex);
			}

			return tasCompletionSource.Task;
		}

		public async Task<int> DoLongOperationAsyncThrows(int counter)
		{
			var result = "";
			var toFetch = "https://www.google.com.ua/?gfe_rd=cr&ei=jCqzV-LjHKOt8weZj5_YCA#q=" + counter;
			if (counter > 1 && counter % 3 == 0)
			{
				throw new Exception("Service unavailable - " + toFetch);
			}
			else
			{
				result = await new WebClient().DownloadStringTaskAsync(toFetch);
			}

			return result.Length;
		}

		public async Task<int> RetrieveValueAsync(int id)
		{
			return await Task.Run(() =>
			{
				Thread.Sleep(500);
				return id + 20;
			});
		}
	}
}

