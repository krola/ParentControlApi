using System.Net.WebSockets;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Websocket;
using WebSocketManager;

public class HelloPocketHandler : BasePocketHandler
{
    private const string Name = "HelloPocket";

    public HelloPocketHandler(WebSocketConnectionManager webSocketConnectionManager, WebSocketHandler socketHandler) : base(webSocketConnectionManager, socketHandler)
    {
    }

    public async override Task Execute(WebSocket socket, string pocketJson)
    {
       var pocket = JsonConvert.DeserializeObject<RequestPocket>(pocketJson);

       if(pocket.Header.Type == Type.Mobile){
           var id = WebSocketConnectionManager.AddClient(socket, pocket.Header.DeviceId.Value);
           var response = new HelloResponse();
           response.Type = Name;
           response.Data = new HelloPocketData(){
               Id = id
           };
            await SocketHandler.SendMessageAsync(socket, JsonConvert.SerializeObject(response));
       }
       else{
           WebSocketConnectionManager.AddService(socket, pocket.Header.DeviceId.Value);
       }
    }

    public override bool ValidHandler(string pocketType)
    {
        if(pocketType == Name)
        {
            return true;
        }

        return false;
    }
}