using System.Collections.Generic;

namespace ParentControlApi.DTO
{
    public class ScheduleDTO
    {
        public int Id { get; set; }
        public bool AllowWithNoTimesheet { get; set; }

        public bool Enabled { get; set; }

        public string Name { get; set; }
    }
}
