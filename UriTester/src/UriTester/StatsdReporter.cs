using Microsoft.Extensions.Caching.Memory;
using StatsdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UriTester
{
    public class StatsdReporter
    {
        private static String PRE = ".";
        private IMemoryCache _cache;
        public StatsdReporter(IMemoryCache cache)
        {
            _cache = cache;
        }
        public void Execute()
        {
            List<Server> servers = new List<Server>();
            if (_cache.TryGetValue(CacheKeys.Data, out servers))
            {
                foreach (Server serv in servers)
                {
                    //Console.WriteLine(serv.Name);
                    if (serv.HealthCheck == Server.Status.Ok) Metrics.GaugeAbsoluteValue(PRE + serv.Name, 1);
                    if (serv.HealthCheck == Server.Status.Degraded) Metrics.GaugeAbsoluteValue(PRE + serv.Name, 2);
                    if (serv.HealthCheck == Server.Status.Error) Metrics.GaugeAbsoluteValue(PRE + serv.Name, 3);
                }
            }
        }
    }
}
