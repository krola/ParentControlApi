using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ParentControlApi.DTO;

public interface ITimesheerService
{
    IEnumerable<Timesheet> GetAll(int scheduleId);
    void Create(Timesheet timesheet);
    void Update(Timesheet newTimesheet);
    void Remove(int Id);
}

public class TimesheerService : ITimesheerService
{
     private readonly IRepository<Timesheet> timesheetRepositor;
    private readonly IScheduleService scheduleService;
    public TimesheerService(IScheduleService scheduleService, IRepository<Timesheet> timesheetRepositor){
        this.scheduleService = scheduleService;
        this.timesheetRepositor = timesheetRepositor;
    }
    public void Create(Timesheet timesheet)
    {
        timesheetRepositor.Add(timesheet);
    }

    public IEnumerable<Timesheet> GetAll(int scheduleId)
    {
        var schedule = scheduleService.Get(scheduleId);
        return timesheetRepositor.FindBy(t => t.Schedule == schedule).AsEnumerable();
    }

    public void Remove(int Id)
    {
        var timesheet = timesheetRepositor.FindBy(t => t.Id == Id).SingleOrDefault();
        timesheetRepositor.Delete(timesheet);
    }

    public void Update(Timesheet newTimesheet)
    {
        var timesheet = timesheetRepositor.FindBy(t => t.Id == newTimesheet.Id).SingleOrDefault();
        var updatedtimesheet = Mapper.Map<Timesheet, Timesheet>(newTimesheet, timesheet);
        timesheetRepositor.Edit(updatedtimesheet);
    }
}