using System;
namespace CleanArchitecture
{
	public class StateMachineExample
	{
		static void Main_()
		{
			var galya = new Girl("galya", new OneYearKidState());

			Console.WriteLine(galya.Eat());
			Console.WriteLine(galya.Play());

			galya.SetState(new FiveYearKidState());

			Console.WriteLine(galya.Eat());
			Console.WriteLine(galya.Play());
		}
	}

	public interface IKidState 
	{
		string Play();
		string Eat();
	}

	public class OneYearKidState : IKidState
	{
		public string Eat()
		{
			return "eat as an 1 year's old kid";
		}

		public string Play()
		{
			return "play as an 1 year's old kid";
		}
	}

	public class FiveYearKidState : IKidState
	{
		public string Eat()
		{
			return "eat as an 5 year's old kid";
		}

		public string Play()
		{
			return "play as an 5 year's old kid";
		}
	}

	public abstract class AKid : IKidState
	{
		protected IKidState _state;
		public AKid(IKidState state) {
			_state = state;
		}

		public abstract string Eat();
		public abstract string Play();
		public abstract void SetState(IKidState newState);
	}

	public class Girl : AKid
	{
		string _name;

		public Girl(string name, IKidState state) : base(state)
		{
			_name = name;
		}

		public override string Eat()
		{
			return _name + " - " +_state.Eat();
		}

		public override string Play()
		{
			return _name + " - " + _state.Play();
		}

		public override void SetState(IKidState newState)
		{
			Console.WriteLine(_name + " - growed up");
			_state = newState;
		}
	}
}

