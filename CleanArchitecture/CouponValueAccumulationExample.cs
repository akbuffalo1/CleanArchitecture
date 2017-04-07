using System;
namespace CleanArchitecture
{
	public class CouponValueAccumulationExample
	{
		public static void Maini(string[] args)
		{
			var couponManager = new CouponAmountManager(new FibonacciCouponAmountCaplculator(3), 1);

			Console.WriteLine(couponManager.GetAndIncrement());

			Console.WriteLine(couponManager.GetAndIncrement());

			Console.WriteLine(couponManager.GetAndIncrement());

			Console.WriteLine(couponManager.GetAndIncrement());

			Console.WriteLine(couponManager.GetAndIncrement());
		}
	}

	public class CouponAmountManager {
		private ICouponAmountCaplculator _calculator;
		public int Order { get; private set; }

		public CouponAmountManager(ICouponAmountCaplculator calculator, int offset = 0) 
		{
			_calculator = calculator;
			if (offset < 0) throw new ArgumentException("Coupon initial offset state can't be less than '0'");
			Order = offset; 
		}

		public int GetCollectedCouponAmount() {
			return _calculator.Calculate(Order);
		}

		public void IncrementOrder() {
			Order++;
		}

		public int GetAndIncrement() {
			var result = GetCollectedCouponAmount();
			IncrementOrder();
			return result;
		}
	}

	public interface ICouponAmountCaplculator {
		int Calculate(int calcOrder);
	}

	public class FibonacciCouponAmountCaplculator : ICouponAmountCaplculator {
		private Func<int, int> _calcFunction;

		public FibonacciCouponAmountCaplculator(int offset = 1) 
		{
			_calcFunction = (order) =>
			{	
				if (order == 0)
					return order;
				if (order == 1)
					return offset;
				else
					return _calcFunction(order - 1) + _calcFunction(1) + order;
			};
		}

		public int Calculate(int calcOrder) {
			return _calcFunction(calcOrder);
		}
	}
}

