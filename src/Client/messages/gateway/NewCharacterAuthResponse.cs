using Riptide;

namespace ValorRise.Client.Messages;

[Message((ushort)MessageType.ToGateway.NewCharacterAuthResponse)]
internal class NewCharacterAuthResponse : IMessageHandler
{
    public void HandleMessage(Message message)
    {
        ushort clientId = message.GetUShort();
        NewCharacterResult result = (NewCharacterResult)message.GetByte();

        var args = new NewCharacterAuthResponseEvent(clientId, result);
        MMOClient.EventBus.Publish(args);
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