using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParentControlApi.DTO;

namespace ParentControlApi.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class DeviceController : Controller
    {
        private IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService) => _deviceService = deviceService;
        // GET api/values
        [HttpGet]
        public IEnumerable<DeviceDTO> Get()
        {
            var devices = _deviceService.GetUserDevices();
            return devices.Select(d => new DeviceDTO(){
                DeviceId = d.DeviceId,
                Name = d.Name
            }).ToArray();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "test";
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
