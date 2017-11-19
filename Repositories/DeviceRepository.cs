using System.Collections.Generic;
using System.Linq;

public class DeviceRepository 
        : GenericRepository<ParentControlContext, Device>  {
    public List<Device> GetAllDevices() => GetAll().ToList();
}