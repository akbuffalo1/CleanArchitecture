using System;
using CleanArchitecture.StatemachineATMExample;

namespace CleanArchitecture
{
	public class StateATMExample
	{
		public static void Main_()
		{ 
			ATMMachine atmMachine = new ATMMachine();

			atmMachine.insertCard();

			atmMachine.ejectCard();

			atmMachine.insertCard();

			atmMachine.insertPin(1234);

			atmMachine.requestCash(2000);

			atmMachine.insertCard();

			atmMachine.insertPin(1234);
		}
	}
}

