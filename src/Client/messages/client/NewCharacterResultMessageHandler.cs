namespace MMOLibrary.Client;
using Riptide;

internal class NewCharacterResultMessageHandler : IMessageHandler
{
    private readonly EventBus _eventBus;

    public NewCharacterResultMessageHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        NewCharacterResult result = (NewCharacterResult)message.GetByte();

        var args = new NewCharacterResultEvent(result);
        _eventBus.Publish(args);
    }
}