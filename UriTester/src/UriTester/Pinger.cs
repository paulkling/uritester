using FluentScheduler;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Caching.Memory;

namespace UriTester
{
    public class Pinger 
    {
  
        private IMemoryCache _cache;   
        private Server key;


        public Pinger(IMemoryCache cache, Server key)
        {
            _cache = cache;
            this.key = key;   
        }
        

        public void Execute()
        {
            // Do work, son!
            Server server;
            if (_cache.TryGetValue(key.Name, out server))
            {
                Console.WriteLine("Found server= " + key.Name);
            }
            else
            {
                Console.WriteLine("Missing server= " + key.Name);
                _cache.Set(key.Name, key);
            }
        }
    }
}
