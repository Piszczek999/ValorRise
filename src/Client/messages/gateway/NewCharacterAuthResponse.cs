namespace ValorRiseClient.Messages;
using Riptide;
using ValorRise;

internal class NewCharacterAuthResponse : IMessageHandler
{
    private readonly EventBus _eventBus;

    public NewCharacterAuthResponse(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        ushort clientId = message.GetUShort();
        NewCharacterResult result = (NewCharacterResult)message.GetByte();

        var args = new NewCharacterAuthResponseEvent(clientId, result);
        _eventBus.Publish(args);
    }
}

public class NewCharacterAuthResponseEvent : EventArgs
{
    public ushort ClientId { get; }
    public NewCharacterResult Result { get; }

    public NewCharacterAuthResponseEvent(ushort clientId, NewCharacterResult result)
    {
        ClientId = clientId;
        Result = result;
    }
}