using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenFu;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MsgPackBlazor.Shared;

namespace MsgPackBlazor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        static List<Contact> _data;
        private static int _counter = 0;

        static ContactsController()
        {
            A.Configure<Contact>().Fill(p => p.StandardAddress).WithRandom(A.ListOf<PostAddress>()).Fill(p => p.Id, () => ++_counter);
            _data = A.ListOf<Contact>(300);
        }

        [HttpGet("{id}")]
        public Task<Contact> GetContact(int id)
        {
            return Task.FromResult(_data.Single(p => p.Id == id));
        }

        [HttpGet("All")]
        public Task<List<Contact>> GetAll()
        {
            _data[0].LastName += "I";
             return Task.FromResult(_data);
        }

        [HttpPost]
        public Task UpdateContact(Contact contact)
        {
            var i = _data.IndexOf(_data.Single(p => p.Id == contact.Id));
            _data[i] = contact;
            return Task.CompletedTask;
        }
    }
}