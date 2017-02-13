using FluentScheduler;


using System.Collections.Generic;
using System.Collections.Concurrent;

namespace UriTester
{
    public class MyRegistry : Registry
    {
        

        public MyRegistry()
        {
            

            //ConcurrentDictionary<string, string> cities = new ConcurrentDictionary<string, string>();

            //may want to swith this to: using concurrent dictionary since the cache class is evicting objects
            // https://msdn.microsoft.com/en-us/library/dd997369(v=vs.110).aspx
            
            //change this to report to statsd job
            JobManager.AddJob(() => new StatsdReporter().Execute(), (s) => s.ToRunEvery(5).Seconds()); //this is the statsd job
            List<Server> servers = ReadData.ReadDataAndConstructServers();
            
            foreach (Server server in servers)
            {
                Data.servers.Add(server);
                JobManager.AddJob(() => new Pinger(server).Execute(), (s) => s.ToRunEvery(server.FrequencyToCheck).Seconds());
            }
        }
    }
}
