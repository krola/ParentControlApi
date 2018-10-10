using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Websocket
{
    public class WebSocketConnectionManager
    {
        private List<Socket> _sockets = new List<Socket>();
        public Socket GetSocketById(string id)
        {
            return _sockets.FirstOrDefault(p => p.SocketId  == id);
        }

        public IEnumerable<Socket> GetSockets(string deviceId, SocketType type)
        {
            return _sockets.Where(p => p.DeviceId == deviceId && p.Type == type);
        }

        public IEnumerable<Socket> GetSockets(string deviceId, string origin, SocketType type)
        {
            return _sockets.Where(p => p.DeviceId == deviceId && p.Type == type && p.SocketId == origin);
        }

        public List<Socket> GetAll()
        {
            return _sockets;
        }

        public string GetId(Socket socket)
        {
            return _sockets.FirstOrDefault(p => p == socket).SocketId;
        }
        public void AddSocket(Socket socket)
        {
            socket.SocketId = CreateConnectionId();
            _sockets.Add(socket);
        }

        public async Task RemoveSocket(string id)
        {
            Socket socket = _sockets.First(s => s.SocketId == id);
            _sockets.Remove(socket);

            await socket.WebSocket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure, 
                                    statusDescription: "Closed by the WebSocketManager", 
                                    cancellationToken: CancellationToken.None);
        }

        private string CreateConnectionId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}