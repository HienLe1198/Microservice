using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Product.Microservice.Controllers
{
    [Route("api/[controller]")]
    public class GatewayController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Clear();
            client.BaseAddress = new Uri("https://localhost:44335");

            // 1. without access_token will not access the service  
            //    and return 401 .  
            //var resWithoutToken = client.GetAsync("/gateway/product").Result;

            //print something here   

            //2. with access_token will access the service  
            //   and return result.  
            client.DefaultRequestHeaders.Clear();
            var jwt = GetJwt();

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");
            var resWithToken = client.GetAsync("/gateway/product").Result;
            return new string[] { resWithToken.ToString() };
        }
        private static string GetJwt()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44335");
            client.DefaultRequestHeaders.Clear();
            var res2 = client.GetAsync("/gateway/auth").Result;
            dynamic jwt = JsonConvert.DeserializeObject(res2.Content.ReadAsStringAsync().Result);
            return jwt.access_token;
        }
    }
}
