namespace MMOLibrary.Server.Messages;
using Riptide;

internal class RegisterDBRequest : IMessageHandler
{
    private readonly EventBus _eventBus;

    public RegisterDBRequest(EventBus eventBus)
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
        var args = new RegisterDBRequestEvent(authenticate, gatewayId, clientId, username, password);
        _eventBus.Publish(args);
    }
}