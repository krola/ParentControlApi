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
    [Authorize()]
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
        [HttpGet("{deviceId}")]
        public IEnumerable<SessionDTO> Get(int deviceId)
        {
            return _sessionService.Get(deviceId)
            .Select(s => _mapper.Map<SessionDTO>(s));
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<SessionDTO> Get([FromQuery] GetDateSessionsParams parameters)
        {
            return _sessionService.Get(parameters.DeviceId, parameters.From, parameters.To)
            .Select(s => _mapper.Map<SessionDTO>(s));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]CreateSessionParams parameters)
        {
            _sessionService.Create(_mapper.Map<Session>(parameters));
        }

        // PUT api/values
        [HttpPut]
        public void Put([FromBody]UpdateSessionParams parameters)
        {
            _sessionService.Update(_mapper.Map<Session>(parameters));
        }
    }
}
