﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TickTackToe_API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class GameController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/game/move
        [HttpPost]
        public string Move([FromBody] string data)
        {
            return data;
        }

        // POST api/game/join
        [HttpPost]
        public string Join([FromBody] string data)
        {
            return data;
        }
 

        // GET api/game/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}
