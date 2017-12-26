using System.Collections.Generic;

namespace ParentControlApi.DTO
{
    public class ScheduleDTO
    {
        public int Id { get; set; }
        public bool AllowWitoutTimesheet { get; set; }

        public string Name { get; set; }
        public DeviceDTO Device { get; set; }
    }
}
