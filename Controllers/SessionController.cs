using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParentControlApi.DTO;

namespace ParentControlApi.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class SessionController : Controller
    {
         private ISessionService _sessionService;
        private readonly IMapper _mapper;
        public SessionController(ISessionService sessionService, IMapper mapper) {
            _sessionService = sessionService;
            _mapper = mapper;
        }
       
        // GET api/values
        [HttpGet]
        public IEnumerable<SessionDTO> Get([FromQuery] GetAllSessionsParams parameters)
        {
            return _sessionService.GetAll(parameters.DeviceId)
            .Select(s => _mapper.Map<SessionDTO>(s));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IEnumerable<SessionDTO> Get([FromQuery] GetDateSessionsParams parameters)
        {
            return _sessionService.GetForDay(parameters.DeviceId, parameters.Date)
            .Select(s => _mapper.Map<SessionDTO>(s));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]CreateSessionParams parameters)
        {
            _sessionService.Create(_mapper.Map<Session>(parameters));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put([FromBody]UpdateSessionParams parameters)
        {
            _sessionService.Update(_mapper.Map<Session>(parameters));
        }
    }
}
