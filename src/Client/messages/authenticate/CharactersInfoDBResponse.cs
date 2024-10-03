namespace MMOLibrary.Client.Messages;
using Riptide;

internal class CharactersInfoDBResponse : IMessageHandler
{
    private readonly EventBus _eventBus;

    public CharactersInfoDBResponse(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        var gatewayId = message.GetUShort();
        var clientId = message.GetUShort();
        var characterInfos = message.GetCharacterInfos(3);

        var args = new CharactersInfoDBResponseEvent(gatewayId, clientId, characterInfos);
        _eventBus.Publish(args);
    }
}