using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ParentControlApi.DTO;

public interface IDeviceService
{
    Device Get(int Id);
    IEnumerable<Device> GetAll();
    Device Create(Device device);
    void Update(Device newDevice);
    void Remove(int Id);

}
public class DeviceService : IDeviceService
{
    private readonly IRepository<Device> deviceRepositor;
    private readonly IUserProvider userProvider;

    public DeviceService(IRepository<Device> deviceRepositor,IUserProvider userProvider) {
        this.deviceRepositor = deviceRepositor;
        this.userProvider = userProvider;
    }

    public IEnumerable<Device> GetAll()
    {
        return GetAllDevices();
    }

    public Device Get(int Id)
    {
        return deviceRepositor.Get(Id);
    }

    public Device Create(Device device)
    {
        if(GetAllDevices().Any(d => d.Name == device.Name))
            throw new DeviceAlreadyExistsException();
            
        var userId = userProvider.GetAuthorizedUserId();
        device.UserId = userId;
        deviceRepositor.Add(device);
        return device;
    }

    private IEnumerable<Device> GetAllDevices()
    {
        var userId = userProvider.GetAuthorizedUserId();
        return deviceRepositor.FindBy(d => d.UserId == userId).AsEnumerable();
    }

    private Device GetEntity(int Id)
    {
        var userId = userProvider.GetAuthorizedUserId();
        return deviceRepositor.FindBy(d => d.UserId == userId && d.Id == Id).SingleOrDefault();
    }

    public void Update(Device newDevice)
    {
        var oldDevice = GetEntity(newDevice.Id);

        if(GetAllDevices().Any(d => d.Name == newDevice.Name))
            throw new DeviceAlreadyExistsException();

        oldDevice.Name = newDevice.Name;
        deviceRepositor.Edit(oldDevice);
    }

    public void Remove(int Id)
    {
        var entity = GetEntity(Id);
        if(entity == null){
            throw new DeviceNotExistException();
        }
        deviceRepositor.Delete(entity);
    }
}