using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UriTester
{
    public class ReadData
    {
        public static List<Server> ReadDataAndConstructServers()
        {
            // load into XElement
            XElement doc = XElement.Load("data.xml");

            List<Server> returnValue = new List<Server>();

            // using our ToDynamicList (Extenion Method)
            var servers = doc.ToDynamicList();

            // loop through each person
            foreach (dynamic server in servers)
            {
                Server tempServer = new Server();
                if (((IDictionary<string, object>)server).ContainsKey("Name"))
                {
                    tempServer.Name = server.Name;
                }
                else
                {
                    break; 
                }
                if (((IDictionary<string, object>)server).ContainsKey("Site"))
                {
                    tempServer.Uri = server.Site;
                }
                else
                {
                    break;
                }
                
                if (((IDictionary<string, object>)server).ContainsKey("Lookfor"))
                {
                    tempServer.TextToSearchFor = server.Lookfor;
                }
                else
                {
                    tempServer.TextToSearchFor = null;
                }
                if (((IDictionary<string, object>)server).ContainsKey("Frequency"))
                {
                    tempServer.FrequencyToCheck = Int32.Parse(server.Frequency);
                }
                else
                {
                    tempServer.FrequencyToCheck = 5;
                }

                if (((IDictionary<string, object>)server).ContainsKey("Debounce"))
                {
                    tempServer.Debounce = Int32.Parse(server.Debounce);
                }else
                {
                    tempServer.Debounce = 1;
                }

                if (((IDictionary<string, object>)server).ContainsKey("MetricDotNet"))
                { 
                    tempServer.MetricsDotNetEndpoint = true;
                }
                else
                {
                    tempServer.MetricsDotNetEndpoint = false;
                }
                returnValue.Add(tempServer);
            }

            return returnValue;
        }

    }

}
