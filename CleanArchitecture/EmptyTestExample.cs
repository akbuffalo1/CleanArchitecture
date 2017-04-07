using System;
using System.Collections.Generic;

namespace CleanArchitecture
{
	public static class EmptyTestExample
	{
		public static void Main_()
		{
			var toExecuteTasks = new List<CustomTask> {
				new CustomTask{
					WhatToDo = "Draw something"
				},
				new CustomTask{
					WhatToDo = "Bring something"
				},
				new CustomTask{
					WhatToDo = "Alert about something"
				}
			};

			toExecuteTasks.ExecuteForEach(task => Console.WriteLine(task.WhatToDo));
		}

		public static void ExecuteForEach(this List<CustomTask> selfList, Action<CustomTask> whatToDo) {
			selfList.ForEach(task => whatToDo(task));
		}
	}

	public class CustomTask
	{
		public string WhatToDo { get; set; }
	}
}