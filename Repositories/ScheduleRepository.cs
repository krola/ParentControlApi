using System.Collections.Generic;
using System.Linq;

public class ScheduleRepository 
        : GenericRepository<ParentControlContext, Schedule>  {
    public List<Schedule> GetAllSchedules() => GetAll().ToList();
}