using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ParentControlApi.DTO;

public interface IDeviceService
{
    DeviceDTO Get(string deviceId);
    IEnumerable<DeviceDTO> GetAll();
    void Create(DeviceDTO device);
    void Update(string oldDeviceId, DeviceDTO newDevice);
    void Remove(string deviceId);

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

    public DeviceDTO Get(string Id)
    {
        return mapper.Map<DeviceDTO>(GetEntity(Id));
    }

    public void Create(DeviceDTO device)
    {
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

    private Device GetEntity(string Id)
    {
        var user = userProvider.GetAuthorizedUser();
        return deviceRepositor.FindBy(d => d.User == user && d.DeviceId == Id).SingleOrDefault();
    }

    public void Update(string oldDeviceId, DeviceDTO newDevice)
    {
        var oldDevice = GetEntity(oldDeviceId);
        var updatedDevice = Mapper.Map<DeviceDTO, Device>(newDevice, oldDevice);
        deviceRepositor.Edit(updatedDevice);
    }

    public void Remove(string id)
    {
        var entity = GetEntity(id);
        deviceRepositor.Delete(entity);
    }
}