namespace MMOLibrary.Client.Messages;
using Riptide;

internal class CharacterSelectAuthResponse : IMessageHandler
{
    private readonly EventBus _eventBus;

    public CharacterSelectAuthResponse(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        ushort clientId = message.GetUShort();
        string token = message.GetString();
        string ipAddress = message.GetString();

        var args = new CharacterSelectAuthResponseEvent(clientId, token, ipAddress);
        _eventBus.Publish(args);
    }
}