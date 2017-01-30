﻿using FluentScheduler;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UriTester
{
    public class MyRegistry : Registry
    {
        private IMemoryCache _cache;




        public MyRegistry(IMemoryCache cache)
        {
            _cache = cache;
            
            //change this to report to statsd job
            JobManager.AddJob(() => Console.WriteLine("Late job!"), (s) => s.ToRunEvery(5).Seconds());

            List<Server> servers = ReadData.ReadDataAndConstructServers();
            _cache.Set(CacheKeys.Data, servers);  //used to generate json for webservice; may be able to do without this

            foreach (Server server in servers)
            {
                JobManager.AddJob(() => new Pinger(_cache,server).Execute(), (s) => s.ToRunEvery(server.FrequencyToCheck).Seconds());
            }
        }
    }
}