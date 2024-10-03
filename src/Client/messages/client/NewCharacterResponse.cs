namespace MMOLibrary.Client.Messages;
using Riptide;

internal class NewCharacterResponse : IMessageHandler
{
    private readonly EventBus _eventBus;

    public NewCharacterResponse(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        NewCharacterResult result = (NewCharacterResult)message.GetByte();

        var args = new NewCharacterResponseEvent(result);
        _eventBus.Publish(args);
    }
}