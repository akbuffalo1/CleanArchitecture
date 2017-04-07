using System;
namespace CleanArchitecture
{
	public class RefOutExample
	{
		static int propVal;

		static void Main_()
		{
			int varVal;
			Methods(out varVal);
			Console.WriteLine(varVal); // Output: 45

			var clazz = new RefOutExample();
			clazz.test();
			Console.WriteLine(propVal); // Output: 45
		}

		void test() {
			Method(ref propVal);
		}

		static void Methods(out int i)
		{
			i = 44; //We can not increment since 'i' isn't defined yet
		}

		void Method(ref int i)
		{
			i = i + 44;
		}
	}

	class CS0663_Example
	{
		// Compiler error CS0663: "Cannot define overloaded 
		// methods that differ only on ref and out".
	//	public void SampleMethod(out int i) { }
	//	public void SampleMethod(ref int i) { }
	}

	class RefOverloadExample
	{
		public void SampleMethod(int i) { }
		public void SampleMethod(ref int i) { }
	}
}

