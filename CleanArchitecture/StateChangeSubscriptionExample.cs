using System;
using System.Reactive.Subjects;

namespace CleanArchitecture
{
	public class StateChangeSubscriptionExample
	{
		public static void Main_(string[] args)
		{
			var order = new SalesOrder();
			order.Status = "completed";

			order.StatChange.Subscribe(
				x => Console.WriteLine(x.OrderStatus),
				ex => Console.WriteLine(ex),
				() => Console.WriteLine("Complited")
			);

			order.Status = "Finished";
			order.Status = "Reopened";

			order.StatChange.OnCompleted();

		}
	}

	public class SalesOrder
	{
		string _status;
		public ISubject<StatusChange> StatChange { get; private set; }

		public int Id { get; set; }

		public string Status
		{
			get { return _status; }
			set
			{
				_status = value;
				StatChange.OnNext(new StatusChange() { OrderId = this.Id, OrderStatus = this.Status });
			}
		}

		public SalesOrder()
		{
			StatChange = new Subject<StatusChange>();
		}
	}

	public class StatusChange
	{
		public int OrderId { get; set; }
		public string OrderStatus { get; set; }
	}
}

