using System;
namespace stage0
{
    partial class  Program
    {
        static void Main(string[] args)
        {
            welcome6572();
            welcome9575();
            Console.ReadKey();
        }

        static partial void welcome9575();
        private static void welcome6572()
        {
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}
