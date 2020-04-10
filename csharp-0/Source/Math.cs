using System;
using System.Collections.Generic;

namespace Codenation.Challenge
{
    public class Math
    {
        public List<int> Fibonacci()
        {
            var listFibonacci = new List<int> { 0, 1 };

            for (int n = 2; n <= 13; n++)
            {
                listFibonacci.Add(listFibonacci[n - 1] + listFibonacci[n - 2]);
            }
            return listFibonacci;
        }

        public bool IsFibonacci(int numberToTest)
        {
            return Fibonacci().Contains(numberToTest);
        }
    }
}
