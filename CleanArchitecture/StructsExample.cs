using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture
{
    public class StructsExample
    {
        public void Main()
        {
            MyStruct myStruct = "Lol";
            Console.WriteLine(myStruct.s);
            Console.WriteLine(myStruct.length);
        }

        

    }
    public struct MyStruct
    {
        public string s;
        public int length;

        public static implicit operator MyStruct(string value)
        {
            return new MyStruct() { s = value, length = value.Length };
        }

    }

}
