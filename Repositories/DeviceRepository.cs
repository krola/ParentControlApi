using System.Collections.Generic;
using System.Linq;

public class DeviceRepository 
        : GenericRepository<ParentControlContext, Device>, IDeviceRepository  {
    public List<Device> GetAllDevices() => GetAll().ToList();
}