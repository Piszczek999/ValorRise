namespace MMOLibrary.Server.Messages;
using Riptide;

internal class LoginDBRequest : IMessageHandler
{
    private readonly EventBus _eventBus;

    public LoginDBRequest(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(ushort authenticateId, Message message)
    {
        ushort gatewayId = message.GetUShort();
        ushort clientId = message.GetUShort();
        string username = message.GetString();
        string password = message.GetString();

        if (!MMOServer.TryGetClient(authenticateId, out var authenticate)) throw new InvalidOperationException("Gateway not found for specified clientId");
        var args = new LoginDBRequestEvent(authenticate, gatewayId, clientId, username, password);
        _eventBus.Publish(args);
    }
}