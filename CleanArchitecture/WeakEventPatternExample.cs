using System;
using System.Diagnostics.Tracing;
using System.Windows;

namespace CleanArchitecture
{
	public class WeakEventPatternExample
	{
		public event CustomEventHandler<EventArgs> CustomEvent = delegate { };

		public event CustomEventHandler<EventArgs> CustomEventForWeak = delegate { };

		public delegate void CustomEventHandler<ArgType>(object sender, ArgType e) where ArgType : EventArgs;

		void Main()
		{
			// One example:
			var example = new WeakEventPatternExample();
			example.AddEvents();
			example.DoSomethingAndFireEvent();

			//Another example:
			Console.WriteLine("\n\n========== Naive listener (bad) ==========");

			EventSource source = new EventSource();

			NaiveEventListener listener = new NaiveEventListener(source);

			source.Raise();

			Console.WriteLine("Setting listener to null.");
			listener = null;

			TriggerGC();

			source.Raise();

			Console.WriteLine("Setting source to null.");
			source = null;

			TriggerGC();
		}

		static void TriggerGC()
		{
			Console.WriteLine("Starting GC.");

			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();

			Console.WriteLine("GC finished.");
		}

		public void AddEvents() { 
			CustomEvent += new CustomEventHandler<EventArgs>((o, a) =>
			{
				Console.WriteLine("new CustomEventHandler((o, a) => {})");
			});

			CustomEvent += (o, a) =>
			{
				Console.WriteLine("Anonimous (o, a) => {}");
			};

			CustomEvent += EventHandlerDefault;

			CustomEvent += EventHandlerDefaultStatic; // Use static when add evenhandler in static context

			// Weak Event Pattern Implementation
			WeakEventManager<WeakEventPatternExample, EventArgs>.AddHandler(this, nameof(CustomEventForWeak), (o, a) =>
			{
				Console.WriteLine("WeakEventManager<WeakEventPatternExample, EventArgs>.AddHandler(example, nameof(CustomEventForWeak), (o, a) =>{}");
			});
		}

		public void DoSomethingAndFireEvent() {
			Console.WriteLine("Did something important..");
			CustomEvent(this, EventArgs.Empty);
			CustomEventForWeak(this, null);
		}


		private void EventHandlerDefault(object sender, EventArgs args) { 
			Console.WriteLine("EventHandlerDefault");
		}

		private static void EventHandlerDefaultStatic(object sender, EventArgs args)
		{
			Console.WriteLine("EventHandlerDefault__STATIC_CONTEXT");
		}
	}

	public class EventSource {
		public event EventHandler Event;

		public void Raise() {
			Event(this, EventArgs.Empty);
		}
	}

	public class NaiveEventListener
	{
		private EventSource _source;
		private void OnEvent(object source, EventArgs args)
		{
			Console.WriteLine("!!!!!!!!! --- EventListener received event --- !!!!!!!!!");
		}

		public NaiveEventListener(EventSource source)
		{
			//source.Event += OnEvent; // Will cause memory leak
			WeakEventManager<EventSource, EventArgs>.AddHandler(_source = source, nameof(EventSource.Event), OnEvent); // Will not cause memory leak
		}

		~NaiveEventListener()
		{
			Console.WriteLine("NaiveEventListener finalized.");
			WeakEventManager<EventSource, EventArgs>.RemoveHandler(_source, nameof(EventSource.Event), OnEvent); // Will not cause memory leak
		}
	}


}
	
