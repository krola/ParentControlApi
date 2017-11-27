using System.Collections.Generic;
using ParentControlApi.DTO;

public interface IDeviceService {
    IEnumerable<Device> GetUserDevices();
}