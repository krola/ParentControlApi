using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ParentControlApi.DTO;

public class DeviceService : BaseService<Device, DeviceDTO>
{    public DeviceService(IRepository<Device> deviceRepositor,IUserProvider userProvider) : base(userProvider, deviceRepositor) {
    }

    public override IEnumerable<Device> GetAll()
    {
        var user = userProvider.GetAuthorizedUser();
        return entityRepository.FindBy(d => d.User == user).AsEnumerable();
    }

    public override Device Get(string Id)
    {
        var user = userProvider.GetAuthorizedUser();
        return entityRepository.FindBy(d => d.User == user && d.DeviceId == Id).SingleOrDefault();
    }

    public override void Create(Device device)
    {
        var user = userProvider.GetAuthorizedUser();
        device.UserId = user.Id;
        entityRepository.Add(device);
    }
}