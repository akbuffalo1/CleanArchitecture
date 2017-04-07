using System;
namespace CleanArchitecture.StatemachineATMExample
{
	public class HasCard : ATMState
	{

		ATMMachine atmMachine;

		public HasCard(ATMMachine newATMMachine)
		{
			this.atmMachine = newATMMachine;
		}

		public void insertCard()
		{
			Console.WriteLine("You can only insert one card at a time");
		}

		public void ejectCard()
		{
			Console.WriteLine("Your card is ejected");
			this.atmMachine.setATMState(this.atmMachine.getNoCardState());
		}

		public void requestCash(int cashToWithdraw)
		{
			Console.WriteLine("You have not entered your PIN");
		}

		public void insertPin(int pinEntered)
		{
			if ((pinEntered == 1234))
			{
				Console.WriteLine("You entered the correct PIN");
				this.atmMachine.CorrectPinEntered = true;
				this.atmMachine.setATMState(this.atmMachine.getHasPin());
			}
			else {
				Console.WriteLine("You entered the wrong PIN");
				this.atmMachine.CorrectPinEntered = false;
				Console.WriteLine("Your card is ejected");
				this.atmMachine.setATMState(this.atmMachine.getNoCardState());
			}

		}
	}
}

