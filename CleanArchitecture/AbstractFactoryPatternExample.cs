using System;
namespace CleanArchitecture
{
	public class AbstractFactoryPatternExample
	{
		static void Main_()
		{
			var phoneFactory = new IphoneFactory();
			var iphoneMobile = phoneFactory.CreateMobile();
			var iphoneTablet = phoneFactory.CreateTablet();

			iphoneMobile.Call();
			iphoneTablet.Research();
		}
	}

	abstract class PhoneFactory 
	{
		public abstract Mobile CreateMobile();
		public abstract Tablet CreateTablet();
	}

	interface Mobile
	{
		void Call();
	}

	interface Tablet
	{
		void Research();
	}

	class IphoneFactory : PhoneFactory
	{
		public override Mobile CreateMobile()
		{
			return new IphoneMobile();
		}

		public override Tablet CreateTablet()
		{
			return new IphoneTablet();
		}
	}

	class IphoneMobile : Mobile
	{
		public void Call()
		{
			Console.WriteLine("Call trough iphone");
		}
	}

	class IphoneTablet : Tablet
	{
		public void Research()
		{
			Console.WriteLine("Research trough iphone");
		}
	}
}

