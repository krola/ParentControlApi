using System;

public class Session
    {
        public int Id { get; set; }
        public Guid SessionId { get; set; }

        public DateTime StarTime { get; set; }

        public DateTime? EndTime { get; set; }

        public virtual Device Device { get; set; }
    }