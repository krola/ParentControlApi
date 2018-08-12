using System.Net.WebSockets;

public enum SocketType{
    Client,
    Server
}

public class Socket {
    public string DeviceId {get;set;}
    public SocketType Type {get;set;}
    public WebSocket WebSocket {get; set;}
}