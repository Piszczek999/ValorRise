using Riptide;

namespace ValorRise.Server.Messages;

[Message((ushort)MessageType.ToGameServer.VerifyTokenRequest)]
internal class VerifyTokenRequest : IMessageHandler
{
    public void HandleMessage(ushort clientId, Message message)
    {
        string token = message.GetString();

        if (!MMOServer.TryGetClient(clientId, out var client)) throw new InvalidOperationException("Client not found for specified clientId");
        var args = new VerifyTokenRequestEvent(client, token);
        MMOServer.EventBus.Publish(args);
    }
}

public class VerifyTokenRequestEvent : EventArgs
{
    public Connection Client { get; }
    public string Token { get; }

    public VerifyTokenRequestEvent(Connection client, string token)
    {
        Client = client;
        Token = token;
    }
}