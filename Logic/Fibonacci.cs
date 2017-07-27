using System;
using System.Collections.Generic;

namespace Logic
{
    public static class Fibonacci
    {
        /// <summary>
        /// Generates sequence of Fibonacci numbers.
        /// </summary>
        /// <param name="quantity">Quantity of numbers to generate.</param>
        /// <returns>Enumerator to iterate.</returns>
        public static IEnumerable<int> Generate(int quantity)
        {
            if (quantity <= 0) throw new ArgumentException($"{nameof(quantity)} is unsuitable.");

            int prevNumber = -1;
            int nextNumber = 1;
            int temp;
            for(int i = 0; i < quantity; i++)
            {
                temp = nextNumber;
                yield return nextNumber += prevNumber;
                prevNumber = temp;
            }
        }
    }
}
