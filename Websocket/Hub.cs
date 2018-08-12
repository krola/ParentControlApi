using System.Collections.Concurrent;
using System.Net.WebSockets;
public class Hub {
    public Hub(){
        Clients = new ConcurrentDictionary<string, WebSocket>();
    }
    public int Id { get; set; }

    public WebSocket ServerSocket { get; set; }

    public ConcurrentDictionary<string, WebSocket> Clients { get; set; }
}