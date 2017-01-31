using System;
using System.Threading.Tasks;
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
            // Do work!
            Server server;
            if (!_cache.TryGetValue(key.Name, out server))
            {
                //Console.WriteLine("Missing server= " + key.Name);
                _cache.Set(key.Name, key);
            }
            Task<UriCheckerResponse> response = UriChecker.CheckSite(server);
            Task.WaitAll(response);

            server.LastResultDate = DateTime.Now;

            if (response.Result.Status == Server.Status.Ok)
            {
                server.HealthCheck = response.Result.Status;
                server.Attempts = 1;
                server.LastResult = "";
               
                _cache.Set(key.Name, server);
            }
            //if not Ok must fill in message
            else
            {
                int attempts = server.Attempts;
                if (server.Debounce > 1)
                {
                    if (attempts < server.Debounce)
                    {
                        server.Attempts = attempts + 1;
                    }
                    else
                    {
                        server.HealthCheck = response.Result.Status;
                        server.LastResult = response.Result.Message;
                    }
                }
                else
                {
                    server.HealthCheck = response.Result.Status;
                    server.Attempts = 1;
                    server.LastResult = response.Result.Message;
                    _cache.Set(key.Name, server);
                }
            }
            
        }
    }
}
