using System.Collections.Generic;
using System.Linq;
using ParentControlApi.DTO;

public class DeviceService : IDeviceService
{
    private readonly IRepository<Device> _deviceRepository;

    public DeviceService(IDeviceRepository deviceRepository) => _deviceRepository = (IRepository<Device>)deviceRepository;
    public IEnumerable<Device> GetUserDevices()
    {
        return _deviceRepository.GetAll();
    }
}