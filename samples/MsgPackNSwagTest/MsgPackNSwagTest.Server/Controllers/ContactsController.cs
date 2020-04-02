using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenFu;
using MsgPackNSwagTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MsgPackNSwagTest.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/x-msgpack", "application/json")]
    [Consumes("application/x-msgpack", "application/json")] // json is still required for monkey dance with NSwag 
    public class ContactController : ControllerBase
    {
        static List<Contact> _data;

        static ContactController()
        {
            A.Configure<Contact>().Fill(p => p.StandardAddress).WithRandom(A.ListOf<PostAddress>());
            _data = A.ListOf<Contact>(300);
        }

        [HttpGet("{id}")]
        public Task<Contact> GetContact(int id)
        {
            var contact = new Contact()
            {
                Id = id,
                FirstName = "Herbert",
                LastName = "Colestich",
                EmailAdress = "herby.master@universe.com",
                PhoneNumber = "001-1233-123-2",
                Gender = Genders.Mixed,
                StandardAddress = new PostAddress() { Zip = "2134", City = "Oklahoma" }
            };
            return Task.FromResult(contact);
        }

        [HttpGet("All")]
        public Task<List<Contact>> GetAll()
        {
            return Task.FromResult(_data);
        }

        [HttpPost]
        public Task UpdateContact(Contact contact)
        {
            return Task.CompletedTask;
        }

        [HttpGet("Download/{id}")]
        [Produces("application/octet-stream")]
        public async Task<IActionResult> Download(string id)
        {
            var stream = new System.IO.MemoryStream();
            var buf = new byte[1024];

            for (var i = 0; i < buf.Length; i++)
                buf[i] = (byte)i;

            for (var i = 0; i < 100; i++)
                await stream.WriteAsync(buf);

            stream.Position = 0;

            if (stream == null)
                return NotFound(); // returns a NotFoundResult with Status404NotFound response.

            return File(stream, "application/octet-stream"); // returns a FileStreamResult
        }
    }
}