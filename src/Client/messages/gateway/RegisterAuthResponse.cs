using Riptide;

namespace ValorRise.Client.Messages;

[Message((ushort)MessageType.ToGateway.RegisterAuthResponse)]
internal class RegisterAuthResponse : IMessageHandler
{
    public void HandleMessage(Message message)
    {
        ushort clientId = message.GetUShort();
        RegisterResult result = (RegisterResult)message.GetByte();

        var args = new RegisterAuthResponseEvent(clientId, result);
        MMOClient.EventBus.Publish(args);
    }
}

public class RegisterAuthResponseEvent : EventArgs
{
    public ushort ClientId { get; }
    public RegisterResult Result { get; }

    public RegisterAuthResponseEvent(ushort clientId, RegisterResult result)
    {
        ClientId = clientId;
        Result = result;
    }
}