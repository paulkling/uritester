using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using System.IO;
using System.Xml.Linq;
using System.Dynamic;
using System.Reflection;
using System.Text;
using System.Net;
using StatsdClient;

namespace UriTester.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private IMemoryCache _cache;

        public ValuesController (IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
           
            return new string[] { "value1", "value1" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            /*
            List<Server> servers = new List<Server>();
            if (!_cache.TryGetValue(CacheKeys.Data, out servers))
            {
                servers = ReadData.ReadDataAndConstructServers();
            }

            Task<UriCheckerResponse> check2 = null;
            foreach (Server server in servers)
            {
                check2 = UriChecker.CheckSite(server);
                Task.WaitAll(check2);
                Console.WriteLine("Name:\t" + server.Name + "message: " + check2.Result.Message + " status: " + check2.Result.Status);
                Console.WriteLine("----------------------------------");
            }
            Metrics.Set("something-special", "25");
            Metrics.GaugeAbsoluteValue("something-special", 50);
            */
            Metrics.Counter("something-special",10,1);

            //return "value=" + check2.Result.Message + "\r\n" + check2.Result.Status;
            return "ddfs";
        }

     

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
