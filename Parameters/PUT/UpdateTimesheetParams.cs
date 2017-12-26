using System;

public class UpdateTimesheetParams {
        public int Id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime CreateTime { get; set; }
  }