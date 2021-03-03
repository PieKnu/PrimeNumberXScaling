using System;
using System.Collections.Generic;

namespace PrimeNumberXScaling.Data
{
    public class PrimeClass
    {

        public bool isPrime(int number) {
            int m;
            int n = number;
            int i;
            //assume true
            bool isPrime = true;
            
            m = n / 2;
            for (i = 2; i <= m; i++)
                {
                if (n % i == 0)
                {
                    isPrime = false;
                    break;
                }
            }
            return isPrime;
        }

        public static List<long> getPrimeNumbers(long startInt, long endInt)
        {
            //Creates the list to fill the prime numbers into
            List<long> primeNumbers = new List<long>();

            //Starts a for loop to check each number
            for (long i = startInt; i <= endInt; i++)
            {
                //Assume each number is a prime number
                bool isPrime = true;

                //For loops with the power of math to ascertain the actuality of prime status
                for (int j = 2; j <= Math.Sqrt(i); j++)
                {
                    //Wasn't actually a prime number.
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                //Adds the prime number to the list.
                if (isPrime)
                {
                    primeNumbers.Add(i);
                }
            }
            return primeNumbers;
        }

    }
}
