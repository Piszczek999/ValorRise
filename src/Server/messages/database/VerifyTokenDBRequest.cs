namespace MMOLibrary.Server.Messages;
using Riptide;

internal class VerifyTokenDBRequest : IMessageHandler
{
    private readonly EventBus _eventBus;

    public VerifyTokenDBRequest(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(ushort gameserverId, Message message)
    {
        ushort clientId = message.GetUShort();
        string token = message.GetString();

        if (!MMOServer.TryGetClient(gameserverId, out var gameserver)) throw new InvalidOperationException("Gateway not found for specified clientId");
        var args = new VerifyTokenDBRequestEvent(gameserver, clientId, token);
        _eventBus.Publish(args);
    }
}