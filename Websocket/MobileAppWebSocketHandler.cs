using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Websocket;
using WebSocketManager;

public class MobileAppWebSocketHandler : WebSocketHandler
{
    private readonly IDeviceService deviceService;
    private readonly WebSocketConnectionManager _webSocketConnectionManager;

    public MobileAppWebSocketHandler(WebSocketConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
    {
        _webSocketConnectionManager = webSocketConnectionManager;
    }

    public override async Task OnConnected(Socket socket)
    {
        await base.OnConnected(socket);

        var socketId = WebSocketConnectionManager.GetId(socket);

        //await SendMessageToAllAsync($"{socketId} is now connected");
    }

    public async override Task ReceiveAsync(Socket socket, WebSocketReceiveResult result, byte[] buffer)
    {
        var socketId = WebSocketConnectionManager.GetId(socket);
        var json = Encoding.UTF8.GetString(buffer, 0, result.Count);

        if(socket.Type == SocketType.Client){
            var pocket = JsonConvert.DeserializeObject<ClientRequestPocket>(json);
            var serverSocket = _webSocketConnectionManager.GetSocket(socket.DeviceId, SocketType.Server);
            
            if(serverSocket != null){
                await SendMessageAsync(serverSocket, new ServerRequestPocket(){
                    Command = pocket.Command,
                    Origin = socket.SocketId.ToString()
                });
            }
            else{
                await SendMessageAsync(socket, new ClientResponsePocket(){
                    Command = pocket.Command,
                    Payload = JsonConvert.SerializeObject(new {Message = "No service connected"}),
                    Status = ResponseStatus.Error
                });
            }
        }
        else {
            var pocket = JsonConvert.DeserializeObject<ServerResposePocket>(json);
            var clientSocket = _webSocketConnectionManager.GetSocket(socket.DeviceId, SocketType.Client);
            
            if(clientSocket != null){
                await SendMessageAsync(clientSocket, new ClientResponsePocket(){
                    Command = pocket.Command,
                    Status = ResponseStatus.Done,
                    Payload = pocket.Payload
                });
            }
        }
    }
}