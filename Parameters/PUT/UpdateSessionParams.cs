using System;

public class UpdateSessionParams {
        public Guid Id { get; set; }
        public int DeviceId { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime? SessionEnd { get; set; }
 }
