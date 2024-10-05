using Riptide;

namespace ValorRise.Server.Messages;

internal class VerifyTokenRequest : IMessageHandler
{
    private readonly EventBus _eventBus;

    public VerifyTokenRequest(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(ushort clientId, Message message)
    {
        string token = message.GetString();

        if (!MMOServer.TryGetClient(clientId, out var client)) throw new InvalidOperationException("Client not found for specified clientId");
        var args = new VerifyTokenRequestEvent(client, token);
        _eventBus.Publish(args);
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