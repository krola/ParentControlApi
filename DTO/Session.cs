using System;

namespace ParentControlApi.DTO
{
    public class SessionDTO
    {
        public Guid Id { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime? SessionEnd { get; set; }
        public DeviceDTO Device {get; set;}
    }
}
