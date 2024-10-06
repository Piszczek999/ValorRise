using Riptide;

namespace ValorRise.Client.Messages;

[Message((ushort)MessageType.ToClient.NewCharacterResponse)]
internal class NewCharacterResponse : IMessageHandler
{
    private GlobalEventHandler _eventHandler = MMOClient.GlobalEventHandler;

    public void HandleMessage(Message message)
    {
        NewCharacterResult result = (NewCharacterResult)message.GetByte();

        var args = new NewCharacterResponseEvent(result);
        _eventHandler.InvokeEvent(args);
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