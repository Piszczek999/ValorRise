namespace MMOLibrary.Client.Messages;
using Riptide;

internal class NewCharacterDBResponse : IMessageHandler
{
    private readonly EventBus _eventBus;

    public NewCharacterDBResponse(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        var gatewayId = message.GetUShort();
        var clientId = message.GetUShort();
        var result = (NewCharacterResult)message.GetByte();

        var args = new NewCharacterDBResponseEvent(gatewayId, clientId, result);
        _eventBus.Publish(args);
    }
}