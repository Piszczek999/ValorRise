namespace MMOLibrary.Client.Messages;
using Riptide;

internal class VerifyTokenResponse : IMessageHandler
{
    private readonly EventBus _eventBus;

    public VerifyTokenResponse(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        bool result = message.GetBool();

        var args = new VerifyTokenResponseEvent(result);
        _eventBus.Publish(args);
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