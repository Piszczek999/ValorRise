using Riptide;

namespace ValorRise.Client.Messages;

[Message((ushort)MessageType.ToClient.VerifyTokenResponse)]
internal class VerifyTokenResponse : IMessageHandler
{
    private GlobalEventHandler _eventHandler = MMOClient.GlobalEventHandler;

    public void HandleMessage(Message message)
    {
        bool result = message.GetBool();

        var args = new VerifyTokenResponseEvent(result);
        _eventHandler.InvokeEvent(args);
    }
}

public class VerifyTokenResponseEvent : EventArgs
{
    public bool Result { get; set; }

    public VerifyTokenResponseEvent(bool result)
    {
        Result = result;
    }
}