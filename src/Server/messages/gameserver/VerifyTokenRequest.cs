using Riptide;

namespace ValorRise.Server.Messages;

[Message((ushort)MessageType.ToGameServer.VerifyTokenRequest)]
internal class VerifyTokenRequest : IMessageHandler
{
    private GlobalEventHandler _eventHandler = MMOServer.GlobalEventHandler;

    public void HandleMessage(ushort clientId, Message message)
    {
        string token = message.GetString();

        var args = new VerifyTokenRequestEvent(clientId, token);
        _eventHandler.InvokeEvent(args);
    }
}

public class VerifyTokenRequestEvent : EventArgs
{
    public ushort ClientId { get; }
    public Connection Client { get => MMOServer.TryGetClient(ClientId, out var client) ? client : null; }
    public string Token { get; }

    public VerifyTokenRequestEvent(ushort clientId, string token)
    {
        ClientId = clientId;
        Token = token;
    }
}