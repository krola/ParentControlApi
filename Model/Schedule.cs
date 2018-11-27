 public class Schedule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool AllowWithNoTimesheet { get; set; }
        public bool Enabled { get; set; }
        public int DeviceId { get; set; }
        public virtual Device Device { get; set; }
    }
    