using System;
using System.Collections.Generic;
using ParentControlApi.DTO;

public interface ITimesheerService
{
    IEnumerable<TimesheetDTO> GetAll();
    void Create(TimesheetDTO timesheet);
    void Update(string scheduleName, DateTime CreateTime, TimesheetDTO timesheet);
    void Remove(string scheduleName, DateTime CreateTime);
}

public class TimesheerService : ITimesheerService
{
    public void Create(TimesheetDTO timesheet)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TimesheetDTO> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Remove(string scheduleName, DateTime CreateTime)
    {
        throw new NotImplementedException();
    }

    public void Update(string scheduleName, DateTime CreateTime, TimesheetDTO timesheet)
    {
        throw new NotImplementedException();
    }
}