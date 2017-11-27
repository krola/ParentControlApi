using System;

namespace ParentControlApi.DTO
{
    public class TimesheetDTO
    {
        public int ScheduleId { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
