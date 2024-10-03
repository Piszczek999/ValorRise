namespace MMOLibrary.Server.Messages;
using Riptide;

internal class RegisterAuthRequest : IMessageHandler
{
    private readonly EventBus _eventBus;

    public RegisterAuthRequest(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(ushort gatewayId, Message message)
    {
        ushort clientId = message.GetUShort();
        string username = message.GetString();
        string password = message.GetString();

        if (!MMOServer.TryGetClient(gatewayId, out var gateway)) throw new InvalidOperationException("Gateway not found for specified clientId");
        var args = new RegisterAuthRequestEvent(gateway, clientId, username, password);
        _eventBus.Publish(args);
    }
}