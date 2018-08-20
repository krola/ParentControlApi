public enum ResponseStatus{
    Done,
    Error
}

public class ClientRequestPocket {
    public string Command {get; set;}
}

public class ServerRequestPocket : ClientRequestPocket{
    public string Origin {get; set;}
}

public class ClientResponsePocket : ClientRequestPocket{
    public ResponseStatus Status{ get; set;}

    public string Payload {get; set;}
}

public class ServerResposePocket : ServerRequestPocket {
    public string Payload {get; set;}
}


