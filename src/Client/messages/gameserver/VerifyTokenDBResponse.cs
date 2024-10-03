namespace MMOLibrary.Client.Messages;
using Riptide;

internal class VerifyTokenDBResponse : IMessageHandler
{
    private readonly EventBus _eventBus;

    public VerifyTokenDBResponse(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        ushort clientId = message.GetUShort();
        var character = message.GetCharacter();

        var args = new VerifyTokenDBResponseEvent(clientId, character);
        _eventBus.Publish(args);
    }
}