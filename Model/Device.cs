public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ConnectionId {get; set;}

        public int UserId { get; set; }
        public User User { get; set; }
    }