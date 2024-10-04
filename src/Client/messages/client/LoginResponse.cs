namespace MMOLibrary.Client.Messages;
using Riptide;

internal class LoginResponse : IMessageHandler
{
    private readonly EventBus _eventBus;

    public LoginResponse(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        LoginResult result = (LoginResult)message.GetByte();

        var args = new LoginResponseEvent(result);
        _eventBus.Publish(args);
    }
}

public class LoginResponseEvent : EventArgs
{
    public LoginResult Result { get; }

    public LoginResponseEvent(LoginResult result)
    {
        Result = result;
    }
}