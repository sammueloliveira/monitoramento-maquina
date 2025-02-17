using System.Net.WebSockets;
using System.Text;

public class WebSocketHandler
{
    private readonly List<WebSocket> _connections = new();

    public async Task HandleConnectionAsync(WebSocket webSocket)
    {
        _connections.Add(webSocket);
        var buffer = new byte[1024 * 4];

        while (webSocket.State == WebSocketState.Open)
        {
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            if (result.MessageType == WebSocketMessageType.Close)
            {
                _connections.Remove(webSocket);
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Conexão fechada", CancellationToken.None);
            }
        }
    }

    public async Task SendMessageToAllAsync(string message)
    {
        var messageBytes = Encoding.UTF8.GetBytes(message);
        var buffer = new ArraySegment<byte>(messageBytes);

        foreach (var webSocket in _connections)
        {
            if (webSocket.State == WebSocketState.Open)
            {
                await webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}
