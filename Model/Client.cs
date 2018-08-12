public class Client
    {
        public string ConnectionId {get; set;}

        public int DeviceId { get; set; }
        public virtual Device Device { get; set; }
    }