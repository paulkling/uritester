using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UriTester
{
    public class Server
    {

        public enum Status
        {
            Ok,
            Degraded,
            Error
        };

        public string Name { get; set; }

        public string Uri { get; set; }

        public string TextToSearchFor { get; set; }

        public bool MetricsDotNetEndpoint { get; set; }

        public int FrequencyToCheck { get; set; }

        public int Debounce { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Server.Status HealthCheck { get; set; }

        public int Attempts { get; set; }

        public String LastResult { get; set; }

        public DateTime LastResultDate { get; set; }
    }
}
