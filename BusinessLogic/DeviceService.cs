using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ParentControlApi.DTO;

public interface IDeviceService
{
    DeviceDTO Get(string deviceName);
    IEnumerable<DeviceDTO> GetAll();
    void Create(DeviceDTO device);
    void Update(string oldDeviceName, DeviceDTO newDevice);
    void Remove(string deviceName);

}
public class DeviceService : IDeviceService
{
    private readonly IRepository<Device> deviceRepositor;
    private readonly IUserProvider userProvider;
    private readonly IMapper mapper;

    public DeviceService(IRepository<Device> deviceRepositor,IUserProvider userProvider, IMapper mapper) {
        this.deviceRepositor = deviceRepositor;
        this.userProvider = userProvider;
        this.mapper = mapper;
    }

    public IEnumerable<DeviceDTO> GetAll()
    {
        return GetAllDevices().Select(d => mapper.Map<DeviceDTO>(d));
    }

    public DeviceDTO Get(string deviceName)
    {
        return mapper.Map<DeviceDTO>(GetEntity(deviceName));
    }

    public void Create(DeviceDTO device)
    {
        if(GetAllDevices().Any(d => d.Name == device.Name))
            throw new DeviceAlreadyExistsException();
            
        var user = userProvider.GetAuthorizedUser();
        var newDevice = mapper.Map<Device>(device);
        newDevice.UserId = user.Id;
        deviceRepositor.Add(newDevice);
    }

    private IEnumerable<Device> GetAllDevices()
    {
        var user = userProvider.GetAuthorizedUser();
        return deviceRepositor.FindBy(d => d.User == user).AsEnumerable();
    }

    private Device GetEntity(string deviceName)
    {
        var user = userProvider.GetAuthorizedUser();
        return deviceRepositor.FindBy(d => d.User == user && d.Name == deviceName).SingleOrDefault();
    }

    public void Update(string oldDeviceName, DeviceDTO newDevice)
    {
        var oldDevice = GetEntity(oldDeviceName);

        if(GetAllDevices().Any(d => d.Name == newDevice.Name))
            throw new DeviceAlreadyExistsException();

        var updatedDevice = Mapper.Map<DeviceDTO, Device>(newDevice, oldDevice);
        deviceRepositor.Edit(updatedDevice);
    }

    public void Remove(string deviceName)
    {
        var entity = GetEntity(deviceName);
        deviceRepositor.Delete(entity);
    }
}