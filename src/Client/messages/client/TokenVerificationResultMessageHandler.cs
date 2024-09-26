namespace MMOLibrary.Client;
using Riptide;

internal class TokenVerificationResultMessageHandler : IMessageHandler
{
    private readonly EventBus _eventBus;

    public TokenVerificationResultMessageHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        bool result = message.GetBool();

        var args = new TokenVerificationResultEvent(result);
        _eventBus.Publish(args);
    }
}