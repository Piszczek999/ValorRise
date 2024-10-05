using Riptide;

namespace ValorRise.Client.Messages;

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

public class NewCharacterResponseEvent : EventArgs
{
    public NewCharacterResult Result { get; }

    public NewCharacterResponseEvent(NewCharacterResult result)
    {
        Result = result;
    }
}