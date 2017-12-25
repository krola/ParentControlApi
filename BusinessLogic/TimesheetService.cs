using System;
using System.Collections.Generic;
using ParentControlApi.DTO;

public interface ITimesheerService
{
    IEnumerable<Timesheet> GetAll();
    void Create(Timesheet timesheet, string scheduleName, string deviceName);
    void Update(string deviceName, string scheduleName, DateTime CreateTime, TimesheetDTO timesheet);
    void Remove(string deviceName, string scheduleName, DateTime CreateTime);
}

public class TimesheerService : ITimesheerService
{
    
}