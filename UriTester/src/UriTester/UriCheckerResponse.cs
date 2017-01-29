using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UriTester
{
    public class UriCheckerResponse
    {
        public Server.Status Status { get; set;  }
        public string Message { get; set;  }

        public UriCheckerResponse ()
        {
            Status = Server.Status.Error;
            Message = "";
        }
    }
}
