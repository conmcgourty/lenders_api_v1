using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Interfaces;
using Shared.Interfaces.Messages;
using Microsoft.Extensions.DependencyInjection;
using Shared.Interfaces.Infrastructure;
using Shared.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIApplication.Controllers
{
    
    [Route("api/[controller]")]
    public class AdvertController : Controller
    {

        IServiceProvider _provider;

        public AdvertController(IServiceProvider provider)
        {
            _provider = provider;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody] Shared.Models.DomainModels.Advert value)
        {
            var message = _provider.GetService<IMessage>();
            message.Command = Command.Create.ToString();
            message.Payload = JsonConvert.SerializeObject(value);

            var queue = _provider.GetService<IQueueRepo>();

            queue.AddMessage(message, "advert");

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
