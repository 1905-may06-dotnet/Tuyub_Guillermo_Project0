using System;

namespace BasicsOfCSharp
{
    class Program
    {
       static string name;

        static void Main(string[] args)
        {
            Console.Write("Please Enter your name")
            name = Console.ReadLine();
            Console.WriteLine("Hello " + name);
        }
    }
}
