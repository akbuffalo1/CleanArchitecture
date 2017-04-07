using System;
namespace CleanArchitecture.StatemachineATMExample
{
	public class ATMMachine : ATMState
	{

		ATMState hasCard;

		ATMState noCard;

		ATMState hasCorrectPin;

		ATMState atmOutOfMoney;

		ATMState atmState;

		public int CashInMachine { get; set; } = 2000;

		public bool CorrectPinEntered { get; set; } = false;

		public ATMMachine()
		{
			this.hasCard = new HasCard(this);
			this.noCard = new NoCard(this);
			this.hasCorrectPin = new HasPin(this);
			this.atmOutOfMoney = new NoCash(this);

			this.atmState = this.noCard;

			if ((this.CashInMachine < 0))
			{
				this.atmState = this.atmOutOfMoney;
			}

		}

		public void setATMState(ATMState newATMState)
		{
			this.atmState = newATMState;
		}

		public void setCashInMachine(int newCashInMachine)
		{
			this.CashInMachine = newCashInMachine;
		}

		public void insertCard()
		{
			this.atmState.insertCard();
		}

		public void ejectCard()
		{
			this.atmState.ejectCard();
		}

		public void requestCash(int cashToWithdraw)
		{
			this.atmState.requestCash(cashToWithdraw);
		}

		public void insertPin(int pinEntered)
		{
			this.atmState.insertPin(pinEntered);
		}

		public ATMState getYesCardState()
		{
			return this.hasCard;
		}

		public ATMState getNoCardState()
		{
			return this.noCard;
		}

		public ATMState getHasPin()
		{
			return this.hasCorrectPin;
		}

		public ATMState getNoCashState()
		{
			return this.atmOutOfMoney;
		}
	}
}

