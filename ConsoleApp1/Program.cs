using System;

namespace ConsoleApp1
{
    internal class Program
    {

        [Flags]
        public enum BusinessType
        {
            Undefined,
            Sale,
            Rent
        }


        static void Main(string[] args)
        {

            BusinessType type = BusinessType.Sale | BusinessType.Rent; 


            Console.WriteLine(type);

            Console.WriteLine("Hello World!");
        }
    }
}
