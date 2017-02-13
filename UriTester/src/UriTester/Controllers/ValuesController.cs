﻿using System.Collections.Generic;


using Microsoft.AspNetCore.Mvc;


namespace UriTester.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        
        public ValuesController ()
        {

        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
           
            return new string[] { "value1", "value1" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "ddfs";
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
    }
}
