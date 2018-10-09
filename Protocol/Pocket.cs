public enum ResponseStatus{
    Done,
    Error
}

public class ClientRequestPocket {
    public string Command {get; set;}
    public string Payload {get; set;}
}

public class ServerRequestPocket {
    public string Command {get; set;}
    public string Payload {get; set;}
    public string Origin {get; set;}
}

public class ClientResponsePocket {
    public string Command {get; set;}
    public ResponseStatus Status{ get; set;}
    public string Payload {get; set;}
}

public class ServerResposePocket {
    public string Command {get; set;}
    public string Payload {get; set;}
    public string Origin {get; set;}
}


