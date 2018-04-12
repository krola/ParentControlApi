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
        private readonly IDeviceService _deviceService;
        private readonly IMapper _mapper;

        public DeviceController(IDeviceService deviceService, IMapper mapper){
            _deviceService = deviceService;
            _mapper = mapper;
        }  
        // GET api/device
        [HttpGet]
        public IEnumerable<DeviceDTO> Get()
        {
            return _deviceService.GetAll().Select(d => _mapper.Map<DeviceDTO>(d));
        }

        // GET api/device/5
        [HttpGet("{id}")]
        public DeviceDTO Get(int id)
        {
            return _mapper.Map<DeviceDTO>(_deviceService.Get(id));
        }

        // POST api/values
        [HttpPost]
        public DeviceDTO Post([FromBody]CreateDeviceParams device)
        {
            var newDevice = _deviceService.Create(_mapper.Map<Device>(device));
            return _mapper.Map<DeviceDTO>(newDevice);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]CreateDeviceParams device)
        {
            var domainDevice = _mapper.Map<Device>(device);
            domainDevice.Id = id;
            _deviceService.Update(domainDevice);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _deviceService.Remove(id);
        }
    }
}
