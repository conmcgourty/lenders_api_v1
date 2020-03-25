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
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIApplication.Controllers
{
    
    [Route("api/advert/")]
    [ApiController]
    public class AdvertController : ControllerBase
    {   

        IServiceProvider _provider;

        public AdvertController(IServiceProvider provider)
        {
            _provider = provider;
        }

        // GET: api/<controller>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        [HttpPost("create")]
        [Authorize]
        public IActionResult Create([FromBody] Shared.Models.DomainModels.AdvertDTO value)
        {
            Console.WriteLine(value);
            var message = _provider.GetService<IMessage>();
            message.Command = Command.Create.ToString();
            message.Payload = JsonConvert.SerializeObject(value);

            var queue = _provider.GetService<IQueueRepo>();

            try
            {
                queue.AddMessage(message, "advert");

                return Ok(new
                {
                    Message = "New Advert Created"
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception thrown: {ex}");
                return StatusCode(500);
            }
            
        }

        // PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
