using System;
namespace CleanArchitecture.StatemachineATMExample
{
	public class NoCard : ATMState
	{

		ATMMachine atmMachine;

		public NoCard(ATMMachine newATMMachine)
		{
			this.atmMachine = newATMMachine;
		}

		public void insertCard()
		{
			Console.WriteLine("Please enter your pin");
			this.atmMachine.setATMState(this.atmMachine.getYesCardState());
		}

		public void ejectCard()
		{
			Console.WriteLine("You didn\'t enter a card");
		}

		public void requestCash(int cashToWithdraw)
		{
			Console.WriteLine("You have not entered your card");
		}

		public void insertPin(int pinEntered)
		{
			Console.WriteLine("You have not entered your card");
		}

	}
}

