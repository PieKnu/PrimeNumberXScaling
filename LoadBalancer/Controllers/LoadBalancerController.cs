using System;
using System.Net.Http;
using System.Threading.Tasks;
using LoadBalancer.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models;
using RestSharp;
using System.IO;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace LoadBalancer.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class LoadBalancerController : ControllerBase
    {

        private ILoadBalanceStrategy _Strategy;
        private readonly ILogger<LoadBalancerController> _logger;

        public LoadBalancerController(ILogger<LoadBalancerController> logger, ILoadBalanceStrategy strategy)
        {
            _logger = logger;
            _Strategy = strategy;
        }

        

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
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var result = LoadBalanceAsyncPrimes(startInt, endInt).Result;
            sw.Stop();

            TimeSpan ts = sw.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

            // Start of logging.
            string path = "log.txt";
            if (!System.IO.File.Exists(path))
            {
                // Create a file to write to.
                var log = System.IO.File.CreateText(path);
                log.Close();
            }

            using (StreamWriter w = System.IO.File.AppendText("log.txt"))
            {
                Log(result, w, startInt, endInt, elapsedTime );
            }

            return result;
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
        public static void Log(Entity entity, TextWriter w, int startInt, int endInt, String elapsedTime)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine($"InstanceId: {entity.InstanceId}");
            w.WriteLine($"Prime Variance Values: {startInt} - {endInt}");
            w.WriteLine($"Time taken: {elapsedTime}");
            w.WriteLine($"Date: {entity.Time}");

            var primeArray = entity.Numbers.ToArray();
            w.WriteLine($"Prime Numbers: ");

            for (int i = 0; i < primeArray.Length; i++)
            {
                w.WriteLine($"  : {primeArray[i]}");
            }
        }
    }
}
