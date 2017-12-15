using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ParentControlApi.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class SessionController : Controller
    {
         private IRepository<Session> _sessionRepository;

        public SessionController(IRepository<Session> sessionRepository) => _sessionRepository = sessionRepository;
       
        // GET api/values
        [HttpGet]
        public IEnumerable<Session> Get()
        {
            return _sessionRepository.GetAll();
        }

        // GET api/values/5
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
