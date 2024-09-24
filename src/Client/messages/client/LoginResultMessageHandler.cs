namespace MMO_Library.Client;
using MongoDB.Bson;
using Riptide;

internal class LoginResultMessageHandler : IMessageHandler
{
    private readonly EventBus _eventBus;

    public LoginResultMessageHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        LoginResult result = (LoginResult)message.GetByte();

        var args = new LoginResultEvent(result);
        _eventBus.Publish(args);
    }
}