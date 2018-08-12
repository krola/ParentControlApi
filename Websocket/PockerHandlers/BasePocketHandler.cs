using System.Net.WebSockets;
using System.Threading.Tasks;
using Websocket;
using WebSocketManager;

public abstract class BasePocketHandler{

    public BasePocketHandler(WebSocketConnectionManager webSocketConnectionManager, WebSocketHandler socketHandler){
        WebSocketConnectionManager = webSocketConnectionManager;
        SocketHandler = socketHandler;
    }

    protected BasePocketHandler(WebSocketConnectionManager webSocketConnectionManager)
    {
        WebSocketConnectionManager = webSocketConnectionManager;
    }

    public BasePocketHandler Next { get; set; }
    protected WebSocketConnectionManager WebSocketConnectionManager { get; }
    public WebSocketHandler SocketHandler { get; }

    public async Task Handle (WebSocket socket, string pocketType, string pocketJson) {
        if(ValidHandler(pocketType)){
            await Execute(socket, pocketJson);
        }

        if(Next == null){
            return;
        }

        await Next.Handle(socket, pocketType, pocketJson);
    }
    public abstract bool ValidHandler(string pocketType);

    public abstract Task Execute(WebSocket socket, string pocketJson);
}