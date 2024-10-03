namespace MMOLibrary.Client.Messages;
using Riptide;

internal class LoginDBResponse : IMessageHandler
{
    private readonly EventBus _eventBus;

    public LoginDBResponse(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        var gatewayId = message.GetUShort();
        var clientId = message.GetUShort();
        var result = (LoginResult)message.GetByte();
        var userId = message.GetObjectId();

        var args = new LoginDBResponseEvent(gatewayId, clientId, result, userId);
        _eventBus.Publish(args);
    }
}