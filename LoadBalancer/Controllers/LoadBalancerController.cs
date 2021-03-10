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
            //Set up the client 
            RestClient c = new RestClient();

            c.BaseUrl = new Uri(_Strategy.BalanceUrl() + "/getPrimes");

            //Create the request
            var request = new RestRequest(Method.GET);
            request.AddParameter("startInt", startInt);
            request.AddParameter("endInt", endInt);

            //Wait for the response
            var response = await c.ExecuteAsync<Entity>(request);

            return response.Data;
        }
        private async Task<IsPrimeEntity> LoadBalanceAsyncIsPrime(int number)
        {
            //Set up the client 
            RestClient c = new RestClient();
            c.BaseUrl = new Uri(_Strategy.BalanceUrl() + "/isPrime");

            //Create the request
            var request = new RestRequest(Method.GET);
            request.AddParameter("number", number);

            //Wait for the response
            var response = await c.ExecuteAsync<IsPrimeEntity>(request);

            return response.Data;
        }

    }
}
