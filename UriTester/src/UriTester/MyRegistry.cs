using FluentScheduler;
using Microsoft.Extensions.Caching.Memory;

using System.Collections.Generic;
using System.Collections.Concurrent;

namespace UriTester
{
    public class MyRegistry : Registry
    {
        private IMemoryCache _cache;

        public MyRegistry(IMemoryCache cache)
        {
            _cache = cache; 

            //ConcurrentDictionary<string, string> cities = new ConcurrentDictionary<string, string>();

            //may want to swith this to: using concurrent dictionary since the cache class is evicting objects
            // https://msdn.microsoft.com/en-us/library/dd997369(v=vs.110).aspx
            
            //change this to report to statsd job
            JobManager.AddJob(() => new StatsdReporter(_cache).Execute(), (s) => s.ToRunEvery(5).Seconds());

            List<Server> servers = ReadData.ReadDataAndConstructServers();
            _cache.Set(CacheKeys.Data, servers);  //used to generate json for webservice; may be able to do without this

            foreach (Server server in servers)
            {
                JobManager.AddJob(() => new Pinger(_cache,server).Execute(), (s) => s.ToRunEvery(server.FrequencyToCheck).Seconds());
            }
        }
    }
}
