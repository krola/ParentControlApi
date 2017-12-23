using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

public interface IDeviceService {
    IEnumerable<Device> GetAll();
    Device GetById(string Id);
}

public class DeviceService : IDeviceService
{
    private readonly IRepository<Device> deviceRepository;
    private readonly IHttpContextAccessor httpContextAccessor;

    public DeviceService(IRepository<Device> deviceRepository, IHttpContextAccessor httpContextAccessor){
        this.deviceRepository = deviceRepository;
        this.httpContextAccessor = httpContextAccessor;
    }

    public IEnumerable<Device> GetAll()
    {
        var test = httpContextAccessor.HttpContext.User;
        throw new System.NotImplementedException();
    }

    public Device GetById(string Id)
    {
        var test = httpContextAccessor.HttpContext.User;
        throw new System.NotImplementedException();
    }
}