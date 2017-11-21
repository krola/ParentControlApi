using System.Collections.Generic;
using System.Linq;

public class ScheduleRepository 
        : GenericRepository<ParentControlContext, Schedule>, IScheduleRepository  {
    public List<Schedule> GetAllSchedules() => GetAll().ToList();
}