using System.Collections.Generic;
using System.Linq;

public class TimesheetRepository 
        : GenericRepository<ParentControlContext, Timesheet>, ITimesheetRepository  {
    public List<Timesheet> GetAllTimesheets() => GetAll().ToList();
}