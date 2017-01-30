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
using System.Net;
using StatsdClient;
using Newtonsoft.Json;

namespace UriTester.Controllers
{
    [Route("api/[controller]")]
    public class ServersController : Controller
    {
        private IMemoryCache _cache;

        public ServersController (IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        // GET api/values
        [HttpGet]
        public JsonResult Get()
        {
            List<Server> servers = new List<Server>();
            if (_cache.TryGetValue(CacheKeys.Data, out servers))
            {
                List<Server> returnList = new List<Server>();
                foreach (Server serv in servers)
                {
                    Server addToList;
                    if (_cache.TryGetValue(serv.Name, out addToList))
                    {
                        returnList.Add(addToList);
                    }
                }
                return Json(returnList);
            }
            //this will be empty is cache miss and that is ok
            return Json(servers);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public JsonResult Get(String  id)
        {
            Server server = new Server();
            if (!_cache.TryGetValue(id, out server))
            {
                List<Server> list = new List<Server>();
                return Json(list);
            }
            return Json(server);
        }
    }
}
