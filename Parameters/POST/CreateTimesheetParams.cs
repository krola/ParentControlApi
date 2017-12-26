using System;

public class CreateTimesheetParams{
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime CreateTime { get; set; }

        public int ScheduleId { get; set; }
  }