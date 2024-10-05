using Riptide;

namespace ValorRise.Client.Messages;

internal class RegisterResponse : IMessageHandler
{
    private readonly EventBus _eventBus;

    public RegisterResponse(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        RegisterResult result = (RegisterResult)message.GetByte();

        var args = new RegisterResponseEvent(result);
        _eventBus.Publish(args);
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