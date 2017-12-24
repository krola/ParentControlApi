using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ParentControlApi.DTO;

public interface IScheduleService
{
    ScheduleDTO Get(string deviceName, string scheduleName);
    IEnumerable<ScheduleDTO> GetAll(string deviceName);
    void Create(ScheduleDTO schedule);
    void Update(string deviceName, string oldScheduleName, ScheduleDTO newSchedule);
    void Remove(string deviceName, string scheduleName);
}

public class ScheduleService : IScheduleService
{
    private readonly IRepository<Schedule> scheduleRepositor;
    private readonly IRepository<Device> deviceRepositor;
    private readonly IDeviceService deviceService;
    private readonly IMapper mapper;

    public ScheduleService(IRepository<Schedule> scheduleRepositor,IRepository<Device> deviceRepositor, IDeviceService deviceService, IMapper mapper){
        this.scheduleRepositor = scheduleRepositor;
        this.deviceRepositor = deviceRepositor;
        this.deviceService = deviceService;
        this.mapper = mapper;
    }

    public void Create(ScheduleDTO schedule)
    {
        var device = GetDevice(schedule.DeviceName);
        if(GetAll(schedule.DeviceName).Any(s => s.Name == schedule.Name))
            throw new ScheduleAlreadyExistsException();

        var newSchedule = mapper.Map<Schedule>(schedule);
        newSchedule.DeviceId = device.Id;
        scheduleRepositor.Add(newSchedule);
    }

    public ScheduleDTO Get(string deviceName, string scheduleName)
    {
        var schedule = GetSchedule(deviceName, scheduleName);
        if(schedule != null){
            return mapper.Map<ScheduleDTO>(schedule);
        }
        return null;
 }

    public IEnumerable<ScheduleDTO> GetAll(string deviceName)
    {
        var device = GetDevice(deviceName);
        return scheduleRepositor.FindBy(s => s.Device == device)
        .Select(s => mapper.Map<ScheduleDTO>(s))
        .AsEnumerable();
    }

    public void Remove(string deviceName, string scheduleName)
    {
        var schedule = GetSchedule(deviceName, scheduleName);
        scheduleRepositor.Delete(schedule);
    }

    public void Update(string deviceName, string oldScheduleName, ScheduleDTO newSchedule)
    {
        var oldSchedule = GetSchedule(deviceName, oldScheduleName);
        var exisitngSchedule = GetSchedule(deviceName, newSchedule.Name);
        if(exisitngSchedule != null)
            throw new ScheduleAlreadyExistsException();
        
        var updatedSchedule = Mapper.Map<ScheduleDTO, Schedule>(newSchedule, oldSchedule);
        scheduleRepositor.Edit(updatedSchedule);
    }

    private Device GetDevice(string deviceName){
        var device =  deviceRepositor.FindBy(d => d.Name == deviceName).SingleOrDefault();  
        if(device == null)
           throw new DeviceNotExistException();
        return device;
    }

    private Schedule GetSchedule(string deviceName, string scheduleName){
        var device = GetDevice(deviceName);
        return scheduleRepositor.FindBy(s => s.Device == device && s.Name == scheduleName).SingleOrDefault();
    }
}