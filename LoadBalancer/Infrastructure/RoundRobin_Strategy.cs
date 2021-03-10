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
        private static int arrayLength = Urls.Length;

        public string BalanceUrl()
        {
            index++;

            if (index == arrayLength)
            {
                index = 0;
            }

            var url = Urls[index];

            return url;
        }
    }
}
