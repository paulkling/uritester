using System;
using System.Threading.Tasks;


namespace UriTester
{
    public class Pinger 
    {
  
  
        private Server server;


        public Pinger(Server key)
        {
 
            this.server = key;   
        }
        

        public void Execute()
        {
            // Do work!
            //Server server;
            ///if (!Data.server.TryGetValue(key.Name, out server))
            //{
                //Console.WriteLine("Missing server= " + key.Name);
            //    Data.server[key.Name] = key;
            //}

            if (server != null)
            {
                Task<UriCheckerResponse> response = UriChecker.CheckSite(server);
                Task.WaitAll(response);

                server.LastResultDate = DateTime.Now;

                if (response.Result.Status == Server.Status.Ok)
                {
                    server.HealthCheck = response.Result.Status;
                    server.Attempts = 1;
                    server.LastResult = "";

                    Data.server[server.Name] = server;
                    
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

                        Data.server[server.Name] = server;
                    }
                }
            }
        }
    }
}
