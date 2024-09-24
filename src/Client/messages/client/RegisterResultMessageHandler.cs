namespace MMO_Library.Client;
using Riptide;

internal class RegisterResultMessageHandler : IMessageHandler
{
    private readonly EventBus _eventBus;

    public RegisterResultMessageHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        RegisterResult result = (RegisterResult)message.GetByte();

        var args = new RegisterResultEvent(result);
        _eventBus.Publish(args);
    }
}