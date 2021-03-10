using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

namespace LoadBalancer.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class PrimeController : ControllerBase
    {
        int instanceId = 2;

        public PrimeController()
        {
 }

        [HttpGet]
        [Route("isPrime")]
        public IsPrimeEntity isPrime(int number)
        {
            Console.WriteLine("2");
            Console.WriteLine(number);

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
            IsPrimeEntity outputEntity = new IsPrimeEntity
            {
                InstanceId = instanceId,
                Time = DateTime.Now,
                IsPrime = isPrime
            };

            return outputEntity;
        }

        [HttpGet]
        [Route("getPrimes")]
        public Entity GetPrimes(int startInt, int endInt)
        {
            Console.WriteLine("2");
            Console.WriteLine(startInt);
            Console.WriteLine(endInt);


            //Creates the list to fill the prime numbers into
            List<int> primeNumbers = new List<int>();

            //Starts a for loop to check each number
            for (int i = startInt; i <= endInt; i++)
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
            Entity outputEntity = new Entity
            {
                InstanceId = instanceId,
                Time = DateTime.Now,
                Numbers = primeNumbers
            };

            return outputEntity;
        }
    }
}
