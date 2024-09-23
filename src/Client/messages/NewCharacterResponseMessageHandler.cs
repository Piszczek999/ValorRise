namespace MMO_Library.Client;
using Riptide;

internal class NewCharacterResponseMessageHandler : IMessageHandler
{
    private readonly EventBus _eventBus;

    public NewCharacterResponseMessageHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        ushort clientId = message.GetUShort();
        NewCharacterResult result = (NewCharacterResult)message.GetByte();

        var args = new NewCharacterResponseEvent(clientId, result);
        _eventBus.Publish(args);
    }
}