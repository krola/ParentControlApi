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
    private readonly BasePocketHandler _pocketsHandlers;

    public MobileAppWebSocketHandler(WebSocketConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
    {
        _pocketsHandlers = InitHandlers();
    }

    public override async Task OnConnected(WebSocket socket)
    {
        await base.OnConnected(socket);

        var socketId = WebSocketConnectionManager.GetId(socket);

        await SendMessageToAllAsync($"{socketId} is now connected");
    }

    public async override Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
    {
        var socketId = WebSocketConnectionManager.GetId(socket);
        var json = Encoding.UTF8.GetString(buffer, 0, result.Count);

        var pocket = JsonConvert.DeserializeObject<BasePocket>(json);

        // if(!IsValidPocket(pocket)){
        //     return;
        // }

        await _pocketsHandlers.Handle(socket,pocket.Type, json);
    }

    public BasePocketHandler InitHandlers(){
        return new HelloPocketHandler(WebSocketConnectionManager, this);
    }

    private bool IsValidPocket(RequestPocket pocket)
    {
        if(string.IsNullOrEmpty(pocket.Type) ||
        pocket.Header?.DeviceId == null){
            return false;
        }

        return true;
    }
}