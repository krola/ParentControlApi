using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ParentControlApi.DTO;

public interface IScheduleService
{
    ScheduleDTO Get(string scheduleId);
    IEnumerable<DeviceDTO> GetAll(string deviceId);
    void Create(DeviceDTO device);
    void Update(string oldDeviceId, DeviceDTO newDevice);
    void Remove(string deviceId);
}
public class ScheduleService : BaseService<Schedule, ScheduleDTO>, IScheduleService
{    public ScheduleService(IRepository<Schedule> scheduleRepositor,IUserProvider userProvider, IMapper mapper) : base(userProvider, scheduleRepositor, mapper) {
    }

    public override void Create(ScheduleDTO entity)
    {

    }

    public override ScheduleDTO Get(string Id, string deviceId)
    {
        return mapper.Map<ScheduleDTO>(GetEntity(Id));
    }

    public override IEnumerable<ScheduleDTO> GetAll()
    {
        return entityRepository.GetAll().Select(s => mapper.Map<ScheduleDTO>(s));
    }

    protected override Schedule GetEntity(string Id)
    {
        return entityRepository.FindBy(s => s.Name == Id).SingleOrDefault();
    }
}