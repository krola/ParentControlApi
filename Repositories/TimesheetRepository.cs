using System.Collections.Generic;
using System.Linq;

public class TimesheetRepository 
        : GenericRepository<ParentControlContext, Timesheet>  {
    public List<Timesheet> GetAllTimesheets() => GetAll().ToList();
}