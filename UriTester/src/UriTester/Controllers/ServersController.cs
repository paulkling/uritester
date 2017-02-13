using System;
using System.Collections.Generic;


using Microsoft.AspNetCore.Mvc;


namespace UriTester.Controllers
{
    [Route("api/[controller]")]
    public class ServersController : Controller
    {
       
        public ServersController ()
        {
            
        }

        // GET api/values
        [HttpGet]
        public JsonResult Get()
        {
            List<Server> returnList = new List<Server>();
            foreach (Server serv in Data.servers)
            {
                Server s = Data.server[serv.Name];
                if (s != null)
                {
                    returnList.Add(s);
                }
            }
            //this will be empty is cache miss and that is ok
            return Json(returnList);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public JsonResult Get(String  id)
        {
            Server server = new Server();
            foreach (Server serv in Data.servers)
            {
                if (serv.Name == id)
                {
                    return Json(serv);
                }
            }
            return Json(server);
        }
    }
}
