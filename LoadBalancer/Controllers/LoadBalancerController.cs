using System;
using System.Net.Http;
using System.Threading.Tasks;
using LoadBalancer.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models;
using RestSharp;
using System.IO;


namespace LoadBalancer.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class LoadBalancerController : ControllerBase
    {

        public LoadBalancerController(ILoadBalanceStrategy strategy)
        {
            _Strategy = strategy;
        }

        private ILoadBalanceStrategy _Strategy;


        [HttpGet]
        [Route("isPrime")]
        public IsPrimeEntity IsPrime(int number)
        {
            return LoadBalanceAsyncIsPrime(number).Result;
        }

        [HttpGet]
        [Route("getPrimes")]
        public Entity GetPrimes(int startInt, int endInt)
        {
            return LoadBalanceAsyncPrimes(startInt, endInt).Result;
        }

        private async Task<Entity> LoadBalanceAsyncPrimes(int startInt, int endInt)
        {
            Console.WriteLine("Balance - Get Primes");
            Console.WriteLine(startInt);
            Console.WriteLine(endInt);

            //Set up the client 
            HttpClient _client = new HttpClient();

            var path = _Strategy.BalanceUrl("getPrimes");

            //Create the request
            path = path + "?startInt=" + startInt + "&endInt=" + endInt;
            //var request = new RestRequest(Method.GET);
            //request.AddParameter("startInt", startInt);
            //request.AddParameter("endInt", endInt);

            //Wait for the response
            //var response = await c.ExecuteAsync<Entity>(request);

            var response = _client.GetAsync(path);

            var result = (response.Result.Content.ReadAsStringAsync().Result);

            Console.WriteLine(result);

            return null;
        }
        private async Task<IsPrimeEntity> LoadBalanceAsyncIsPrime(int number)
        {
            Console.WriteLine("Balance - isPrime");
            Console.WriteLine(number);

            RestClient c = new RestClient();
            c.BaseUrl = new Uri(_Strategy.BalanceUrl("isPrime"));

            Console.WriteLine(c.BaseUrl);

            var request = new RestRequest(Method.GET);

            request.AddParameter("number", number);

            var response = await c.ExecuteAsync<IsPrimeEntity>(request);
            Console.WriteLine(response.Data);

            return response.Data;
        }

    }
}
