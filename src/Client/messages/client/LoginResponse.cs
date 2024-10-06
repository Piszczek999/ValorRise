using Riptide;

namespace ValorRise.Client.Messages;

[Message((ushort)MessageType.ToClient.LoginResponse)]
internal class LoginResponse : IMessageHandler
{
    private GlobalEventHandler _eventHandler = MMOClient.GlobalEventHandler;

    public void HandleMessage(Message message)
    {
        LoginResult result = (LoginResult)message.GetByte();

        var args = new LoginResponseEvent(result);
        _eventHandler.InvokeEvent(args);
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