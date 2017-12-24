using System.Collections.Generic;

namespace ParentControlApi.DTO
{
    public class ScheduleDTO
    {
        public bool AllowWitoutTimesheet { get; set; }

        public string Name { get; set; }
        public string DeviceName { get; set; }
    }
}
