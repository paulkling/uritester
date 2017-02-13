using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace UriTester
{
    public class Data
    {
        public static ConcurrentBag<Server> servers { get; set; }
        public static ConcurrentDictionary<String, Server> server { get; set; }

        
        static Data()
        {
            servers = new ConcurrentBag<Server>();
            server = new ConcurrentDictionary<string, Server>();
        }
    }
}
