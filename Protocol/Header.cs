public enum Type{
    Service,
    Mobile
}

public class Header {
    public int? DeviceId { get; set; }

    public string Origin {get; set;}

    public Type Type {get; set;}
}