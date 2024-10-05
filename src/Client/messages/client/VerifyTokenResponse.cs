using Riptide;

namespace ValorRise.Client.Messages;

[Message((ushort)MessageType.ToClient.VerifyTokenResponse)]
internal class VerifyTokenResponse : IMessageHandler
{
    public void HandleMessage(Message message)
    {
        bool result = message.GetBool();

        var args = new VerifyTokenResponseEvent(result);
        MMOClient.EventBus.Publish(args);
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