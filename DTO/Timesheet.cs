using System;

namespace ParentControlApi.DTO
{
    public class TimesheetDTO
    {
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime CreateTime { get; set; }

        public string ScheduleName { get; set; }
    }
}
