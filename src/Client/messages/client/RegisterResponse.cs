using Riptide;

namespace ValorRise.Client.Messages;

[Message((ushort)MessageType.ToClient.RegisterResponse)]
internal class RegisterResponse : IMessageHandler
{
    private GlobalEventHandler _eventHandler = MMOClient.GlobalEventHandler;

    public void HandleMessage(Message message)
    {
        RegisterResult result = (RegisterResult)message.GetByte();

        var args = new RegisterResponseEvent(result);
        _eventHandler.InvokeEvent(args);
    }
}

public class RegisterResponseEvent : EventArgs
{
    public RegisterResult Result { get; }

    public RegisterResponseEvent(RegisterResult result)
    {
        Result = result;
    }
}