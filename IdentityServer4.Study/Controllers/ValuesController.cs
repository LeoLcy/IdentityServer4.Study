using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IdentityServer4.Study.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public async Task<ActionResult<string>> GetAsync()
        {
            // 从元数据中发现端口
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");

            if (disco.IsError)
            {
                return disco.Error;
            }
            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            if (tokenResponse.IsError)
            {
                return tokenResponse.Error;
            }

            Console.WriteLine();

            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/identity");
            if (!response.IsSuccessStatusCode)
            {
                 return JsonConvert.SerializeObject(response.StatusCode);
            }
             var content = await response.Content.ReadAsStringAsync();
            var obj = new {
                tokenJson=tokenResponse.Json,
                content= content
            };
            return JsonConvert.SerializeObject(obj);
        }
    }
}