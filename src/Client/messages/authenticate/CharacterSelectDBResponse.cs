namespace MMOLibrary.Client.Messages;
using Riptide;

internal class CharacterSelectDBResponse : IMessageHandler
{
    private readonly EventBus _eventBus;

    public CharacterSelectDBResponse(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        var gatewayId = message.GetUShort();
        var clientId = message.GetUShort();
        string token = message.GetString();
        string ipAddress = message.GetString();

        var args = new CharacterSelectDBResponseEvent(gatewayId, clientId, token, ipAddress);
        _eventBus.Publish(args);
    }
}