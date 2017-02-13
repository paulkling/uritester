
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
        
        public StatsdReporter()
        {
            
        }
        public void Execute()
        {
            foreach (Server serv in Data.servers)
            {
                //Console.WriteLine(serv.Name);
                if (serv.HealthCheck == Server.Status.Ok) Metrics.GaugeAbsoluteValue(PRE + serv.Name, -1);
                if (serv.HealthCheck == Server.Status.Degraded) Metrics.GaugeAbsoluteValue(PRE + serv.Name, 2);
                if (serv.HealthCheck == Server.Status.Error) Metrics.GaugeAbsoluteValue(PRE + serv.Name, 3);
            }
         
        }
    }
}
