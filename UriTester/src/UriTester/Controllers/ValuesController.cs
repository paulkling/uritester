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

            String value; 
            if (!_cache.TryGetValue(CacheKeys.Data, out value))
            {

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "powershell";
                psi.UseShellExecute = false;
                psi.RedirectStandardOutput = true;

                psi.Arguments = "Get-Host";
                Process p = Process.Start(psi);
                string strOutput = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                Console.WriteLine(strOutput);

                _cache.Set(CacheKeys.Data, strOutput);
            }

            return new string[] { "value1", "value1" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            String value;
            if (!_cache.TryGetValue(CacheKeys.Data, out value))
            {
            }

            var check1 = "https://node1-qe00us-auth.pmdomhq.protomold.com";
            var check2 = "http://node1-produs-taxapi.webservices.protolabs.com";
            var check3 = "google.com";


            var check = UriChecker.CheckSite(check1, true);
            //var check = UriChecker.CheckSite(check2, true);
            //var check = UriChecker.CheckSite(check3);
            //var check = UriChecker.CheckSite(check3,"paul");
            Task.WaitAll(check);
            var respose = check.Result;








            
         
            // load into XElement
            XElement doc = XElement.Load("data.xml");

            // using our ToDynamicList (Extenion Method)
            var servers = doc.ToDynamicList();

            // loop through each person
            String serversText = "\r\n";
            foreach (dynamic server in servers)
            {
                serversText += server.Name +":";
                serversText += server.Site + ":";

                if (((IDictionary<string, object>)server).ContainsKey("Lookfor"))
                {
                    serversText += server.Lookfor + ":";
                }


                if (((IDictionary<string, object>)server).ContainsKey("Frequency"))
                {
                    serversText += server.Frequency + ":";
                }

                if (((IDictionary<string, object>)server).ContainsKey("Debounce"))
                {
                    serversText += server.Debounce;
                }

                serversText += "\r\n";

                Console.WriteLine("Name:\t" + server.Name);
                Console.WriteLine("----------------------------------");
            }







                return "value="+ value + respose.Message + "\r\n"+ respose.Status + serversText;
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


         private string xml = @"<Servers>
                 <Server Name=""100"" Site=""google.com"" />
                 <Server Name=""sdasdasdas"">
                     <Mark>80</Mark>
                 </Server>
             </Servers>";
    }
}
