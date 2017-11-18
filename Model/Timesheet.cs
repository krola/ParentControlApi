using System;
public class Timesheet
    {
        public int Id { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public TimeSpan Time { get; set; }

        public DateTime CreateTime { get; set; }

        public virtual Schedule Schedule { get; set; }
    }