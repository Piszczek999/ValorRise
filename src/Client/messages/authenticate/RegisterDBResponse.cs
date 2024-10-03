namespace MMOLibrary.Client.Messages;
using Riptide;

internal class RegisterDBResponse : IMessageHandler
{
    private readonly EventBus _eventBus;

    public RegisterDBResponse(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        var gatewayId = message.GetUShort();
        var clientId = message.GetUShort();
        var result = (RegisterResult)message.GetByte();

        var args = new RegisterDBResponseEvent(gatewayId, clientId, result);
        _eventBus.Publish(args);
    }
}