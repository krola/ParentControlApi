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
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;
        private readonly IMapper _mapper;
        public ScheduleController(IScheduleService scheduleService, IMapper mapper){
            _scheduleService = scheduleService;
            _mapper = mapper;
        } 
       
        // GET api/values
        [HttpGet]
        public IEnumerable<ScheduleDTO> Get([FromQuery]GetSchedulesParams getScheduleParams)
        {
            return _scheduleService.GetAll(getScheduleParams.DeviceId)
            .Select(s => _mapper.Map<ScheduleDTO>(s));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ScheduleDTO Get(int id)
        {
            return _mapper.Map<ScheduleDTO>(_scheduleService.Get(id));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]CreateScheduleParams parameters)
        {
            var newSchedule = _scheduleService.Create(_mapper.Map<Schedule>(parameters));
            return Ok(_mapper.Map<ScheduleDTO>(newSchedule));
        }

        [HttpPost("SetActive")]
        public IActionResult SetActive([FromBody]SetActiveScheduleParams parameters)
        {
            var newSchedule = _scheduleService.Create(_mapper.Map<Schedule>(parameters));
            return Ok(_mapper.Map<ScheduleDTO>(newSchedule));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateScheduleParams parameters)
        {
            var schedule = _mapper.Map<Schedule>(parameters);
            schedule.Id = id;
            _scheduleService.Update(schedule);
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _scheduleService.Remove(id);
            return NoContent();
        }
    }
}
