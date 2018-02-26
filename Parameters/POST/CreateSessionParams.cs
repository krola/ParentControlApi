
using System;

public class CreateSessionParams {
        public Guid Id { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime? SessionEnd { get; set; }
        public int DeviceId { get; set;}
 }
