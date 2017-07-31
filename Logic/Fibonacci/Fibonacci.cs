using System;
using System.Collections.Generic;
using System.Numerics;

namespace Logic
{
    public static class Fibonacci
    {
        /// <summary>
        /// Generates sequence of Fibonacci numbers.
        /// </summary>
        /// <param name="quantity">Quantity of numbers to generate.</param>
        /// <returns>Fibonacci sequence.</returns>
        public static IEnumerable<BigInteger> Generate(int quantity)
        {
            if (quantity <= 0) throw new ArgumentException($"{nameof(quantity)} is unsuitable.");

            BigInteger prevNumber = -1;
            BigInteger nextNumber = 1;
            BigInteger temp;
            for(int i = 0; i < quantity; i++)
            {
                temp = nextNumber;
                yield return nextNumber += prevNumber;
                prevNumber = temp;
            }
        }
    }
}
