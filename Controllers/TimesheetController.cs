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
    public class TimesheetController : Controller
    {
        private readonly ITimesheerService _timesheetService;
        private readonly IMapper _mapper;

        public TimesheetController(ITimesheerService timesheetService, IMapper mapper){
            _timesheetService = timesheetService;
            _mapper = mapper;
        } 
        // GET api/values
        [HttpGet]
        public IEnumerable<TimesheetDTO> Get([FromQuery] GetTimesheetParams parameters)
        {
            return _timesheetService
            .GetAll(parameters.ScheduleId)
            .Select(t => _mapper.Map<TimesheetDTO>(t));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]CreateTimesheetParams parameters)
        {
            var newTimesheet = _timesheetService.Create(_mapper.Map<Timesheet>(parameters));
            return Ok(_mapper.Map<TimesheetDTO>(newTimesheet));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateTimesheetParams parameters)
        {
            var timesheet = _mapper.Map<Timesheet>(parameters);
            timesheet.Id = id;
            _timesheetService.Update(timesheet);
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _timesheetService.Remove(id);
            return NoContent();
        }
    }
}
