using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.Microservice.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Catcher Wong", "James Li" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"Catcher Wong - {id}";
        }
    }
}
