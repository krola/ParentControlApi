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
        private IRepository<Device> _deviceRepository;
        private readonly IDeviceService _deviceService;
        private readonly IMapper _mapper;

        public DeviceController(IRepository<Device> deviceRepository, IDeviceService deviceService, IMapper mapper){
            _deviceRepository = deviceRepository;
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
        public DeviceDTO Get(int id)
        {
            var device = _deviceRepository.Get(id);
            return _mapper.Map<DeviceDTO>(device);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]DeviceDTO device)
        {
            _deviceRepository.Add(_mapper.Map<Device>(device));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]DeviceDTO device)
        {
            var existingDevice = _deviceRepository.Get(id);
            Mapper.Map<DeviceDTO, Device>(device, existingDevice);
            _deviceRepository.Edit(existingDevice);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var device = _deviceRepository.Get(id);
            _deviceRepository.Delete(device);
        }
    }
}
