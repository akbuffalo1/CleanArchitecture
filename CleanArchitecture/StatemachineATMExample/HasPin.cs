using System;
namespace CleanArchitecture.StatemachineATMExample
{
	public class HasPin : ATMState
	{

		ATMMachine atmMachine;

		public HasPin(ATMMachine newATMMachine)
		{
			this.atmMachine = newATMMachine;
		}

		public void insertCard()
		{
			Console.WriteLine("You already entered a card");
		}

		public void ejectCard()
		{
			Console.WriteLine("Your card is ejected");
			this.atmMachine.setATMState(this.atmMachine.getNoCardState());
		}

		public void requestCash(int cashToWithdraw)
		{
			if ((cashToWithdraw > this.atmMachine.CashInMachine))
			{
				Console.WriteLine("You don\'t have that much cash available");
				Console.WriteLine("Your card is ejected");
				this.atmMachine.setATMState(this.atmMachine.getNoCardState());
			}
			else {
				Console.WriteLine((cashToWithdraw + " is provided by the machine"));
				this.atmMachine.setCashInMachine((this.atmMachine.CashInMachine - cashToWithdraw));
				Console.WriteLine("Your card is ejected");
				this.atmMachine.setATMState(this.atmMachine.getNoCardState());
				if ((this.atmMachine.CashInMachine <= 0))
				{
					this.atmMachine.setATMState(this.atmMachine.getNoCashState());
				}

			}

		}

		public void insertPin(int pinEntered)
		{
			Console.WriteLine("You already entered a PIN");
		}
	}
}

