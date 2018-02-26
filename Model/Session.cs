using System;
using System.ComponentModel.DataAnnotations.Schema;

public class Session
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime StarTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int DeviceId { get; set; }
        public virtual Device Device { get; set; }
    }