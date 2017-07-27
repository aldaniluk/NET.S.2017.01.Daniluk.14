using System;
using Logic;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Fibonacci
            IEnumerable<int> numerator = Fibonacci.Generate(8);
            foreach(int i in numerator)
            {
                Console.WriteLine(i);
            }
            #endregion


        }
    }
}
