using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadBalancer.Infrastructure
{
    public class RoundRobin_Strategy : ILoadBalanceStrategy
    {

        private static readonly string[] Urls = new[]
{           // Add URL Paths here.
            "https://localhost:5001", "https://localhost:5002"
        };

        private static int index = 0;


        public string BalanceUrl(string option)
        {
            var url = Urls[index % 2];
            index++;

            if (index == 2)
            {
                index = 1;
            }

            if (option == "isPrime")
            {
                url = url + "/isPrime?";
            }
            else
            {
                url = url + "/getPrimes?";
            }
            return url;
        }
    }
}
