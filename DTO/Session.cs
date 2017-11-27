using System;

namespace ParentControlApi.DTO
{
    public class SessionDTO
    {
        public Guid SessionId { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime? SessionEnd { get; set; }

        public string DeviceID { get; set; }
    }
}
