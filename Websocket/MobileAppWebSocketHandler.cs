using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Websocket;
using WebSocketManager;

public class MobileAppWebSocketHandler : WebSocketHandler
{
    private readonly WebSocketConnectionManager _webSocketConnectionManager;
    private readonly string ErrorPayload = JsonConvert.SerializeObject(new {Message = "Server not connected"});

    public MobileAppWebSocketHandler(WebSocketConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
    {
        _webSocketConnectionManager = webSocketConnectionManager;
    }

    public override async Task OnConnected(Socket socket)
    {
        base.OnConnected(socket);

        var socketId = WebSocketConnectionManager.GetId(socket);

        //await SendMessageToAllAsync($"{socketId} is now connected");
    }

    public async override Task ReceiveAsync(Socket socket, WebSocketReceiveResult result, byte[] buffer)
    {
        var socketId = WebSocketConnectionManager.GetId(socket);
        var json = Encoding.UTF8.GetString(buffer, 0, result.Count);

        if(socket.Type == SocketType.Client){
            var pocket = JsonConvert.DeserializeObject<ClientRequestPocket>(json);
            var serverSocket = _webSocketConnectionManager.GetSockets(socket.DeviceId, SocketType.Server).FirstOrDefault();
            
            if(serverSocket != null){
                await SendMessageAsync(serverSocket, new ServerRequestPocket(){
                    Payload = pocket.Payload,
                    Command = pocket.Command,
                    Origin = socket.SocketId.ToString()
                });
            }
            else{
                await SendMessageAsync(socket, new ClientResponsePocket(){
                    Command = pocket.Command,
                    Status = ResponseStatus.Error,
                    Payload = ErrorPayload
                });
            }
        }
        else {
            var pocket = JsonConvert.DeserializeObject<ServerResposePocket>(json);
            var clientSockets = 
                        string.IsNullOrEmpty(pocket.Origin) ? _webSocketConnectionManager.GetSockets(socket.DeviceId, SocketType.Client)
                                                            : _webSocketConnectionManager.GetSockets(socket.DeviceId, pocket.Origin, SocketType.Client);
            
            if(clientSockets != null && clientSockets.Any()){
                foreach(var clientSocket in clientSockets){
                    await SendMessageAsync(clientSocket, new ClientResponsePocket(){
                    Command = pocket.Command,
                    Status = ResponseStatus.Done,
                    Payload = pocket.Payload
                });
                }
            }
        }
    }
}