using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using LinqKit;
using ParentControlApi.DTO;

public interface ITimesheerService
{
    IEnumerable<Timesheet> Get(int scheduleId, DateTime? from = null, DateTime? to = null);
    Timesheet Create(Timesheet timesheet);
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
    public Timesheet Create(Timesheet timesheet)
    {
        timesheetRepositor.Add(timesheet);
        return timesheet;
    }

    public IEnumerable<Timesheet> Get(int scheduleId, DateTime? from = null, DateTime? to = null)
    {
        var schedule = scheduleService.Get(scheduleId);
        var predicate = PredicateBuilder.New<Timesheet>()
                        .And(t => t.Schedule == schedule);
        
        if(from.HasValue){
            predicate = predicate.And(t => t.DateFrom.Date >= from.Value.Date);
        }
        if(to.HasValue){
            predicate = predicate.And(t => t.DateTo.HasValue && t.DateTo.Value.Date <= to.Value.Date);
        }

         return timesheetRepositor.FindBy(predicate).AsEnumerable();
    }

    public void Remove(int Id)
    {
        var timesheet = timesheetRepositor.FindBy(t => t.Id == Id).SingleOrDefault();
        if(timesheet == null){
            throw new TimesheetNonExistsException();
        }
        timesheetRepositor.Delete(timesheet);
    }

    public void Update(Timesheet newTimesheet)
    {
        var timesheet = timesheetRepositor.Get(newTimesheet.Id);

        if(timesheet == null){
            throw new TimesheetNonAlreadyExistsException();
        }
        timesheet.CreateTime = newTimesheet.CreateTime;
        timesheet.DateFrom = newTimesheet.DateFrom;
        timesheet.DateTo = newTimesheet.DateTo;
        timesheet.Time = newTimesheet.Time;
        timesheetRepositor.Edit(timesheet );
    }
}