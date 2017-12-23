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
    public class DeviceController : Controller
    {
        private readonly BaseService<Device, DeviceDTO> _deviceService;
        private readonly IMapper _mapper;

        public DeviceController(BaseService<Device, DeviceDTO> deviceService, IMapper mapper){
            _deviceService = deviceService;
            _mapper = mapper;
        }  
        // GET api/device
        [HttpGet]
        public IEnumerable<DeviceDTO> Get()
        {
            var devices = _deviceService.GetAll();
            return devices.Select(d => _mapper.Map<DeviceDTO>(d)).ToArray();
        }

        // GET api/device/5
        [HttpGet("{id}")]
        public DeviceDTO Get(string id)
        {
            var device = _deviceService.Get(id);
            return _mapper.Map<DeviceDTO>(device);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]DeviceDTO device)
        {
            _deviceService.Create(_mapper.Map<Device>(device));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]DeviceDTO device)
        {
            _deviceService.Update(id, device);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _deviceService.Remove(id);
        }
    }
}
