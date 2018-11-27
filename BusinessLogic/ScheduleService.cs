using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ParentControlApi.DTO;

public interface IScheduleService
{
    Schedule Get(int Id);
    IEnumerable<Schedule> GetAll(int deviceId);
    Schedule Create(Schedule schedule);
    void SetActive(int scheduleId);
    void Update(Schedule newSchedule);
    void Remove(int Id);
}

public class ScheduleService : IScheduleService
{
    private readonly IRepository<Schedule> scheduleRepositor;
    private readonly IDeviceService deviceService;

    public ScheduleService(IRepository<Schedule> scheduleRepositor,IDeviceService deviceService){
        this.scheduleRepositor = scheduleRepositor;
        this.deviceService = deviceService;
    }

    public Schedule Create(Schedule schedule)
    {
        var device = deviceService.Get(schedule.DeviceId);
        if(GetAll(schedule.DeviceId).Any(s => s.Name == schedule.Name))
            throw new ScheduleAlreadyExistsException();

        schedule.DeviceId = device.Id;
        scheduleRepositor.Add(schedule);
        schedule = scheduleRepositor.FindBy(s => s.DeviceId == device.Id && s.Name == schedule.Name).First();
        if(schedule.Enabled){
            DisableSchedulesExcept(schedule);
        }
        return schedule;
    }

    public Schedule Get(int Id)
    {
        return GetSchedule(Id);
    }

    public IEnumerable<Schedule> GetAll(int deviceId)
    {
        var device = deviceService.Get(deviceId);
        return scheduleRepositor.FindBy(s => s.Device == device)
        .AsEnumerable();
    }

    public void Remove(int Id)
    {
        var schedule = GetSchedule(Id);
        if(schedule == null){
            throw new ScheduleNotExistsException();
        }
        scheduleRepositor.Delete(schedule);
    }

    public void Update(Schedule newSchedule)
    {
        var oldSchedule = GetSchedule(newSchedule.Id);
        if(oldSchedule == null)
            throw new ScheduleNotExistsException();
        
        oldSchedule.AllowWithNoTimesheet = newSchedule.AllowWithNoTimesheet;
        oldSchedule.Name = newSchedule.Name ?? oldSchedule.Name;
        oldSchedule.DeviceId = newSchedule.DeviceId;
        oldSchedule.Enabled = newSchedule.Enabled;
        if(newSchedule.Enabled){
            DisableSchedulesExcept(oldSchedule);
        }
        scheduleRepositor.Edit(oldSchedule);
    }

    public void SetActive(int scheduleId){
        var schedule = GetSchedule(scheduleId);
        schedule.Enabled = true;
        scheduleRepositor.Edit(schedule);
        DisableSchedulesExcept(schedule);
    }

    private Schedule GetSchedule(int Id){
         var devices = deviceService.GetAll();
        var schedule = scheduleRepositor.Get(Id);
        if(devices.Any(d => d.Id == schedule.DeviceId)){
            return schedule;
        }
        return null;
    }

    private void DisableSchedulesExcept(Schedule schedule){
        foreach(var s in scheduleRepositor.FindBy(s => s.Enabled && s.Id != schedule.Id && s.DeviceId == schedule.DeviceId)){
            s.Enabled = false;
            scheduleRepositor.Edit(s);
        }
    }
}