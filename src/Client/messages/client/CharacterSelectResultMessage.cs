namespace MMOLibrary.Client;
using MongoDB.Bson;
using Riptide;

internal class CharacterSelectResultMessageHandler : IMessageHandler
{
    private readonly EventBus _eventBus;

    public CharacterSelectResultMessageHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void HandleMessage(Message message)
    {
        var result = (CharacterSelectResult)message.GetByte();

        var args = new CharacterSelectResultEvent(result);
        _eventBus.Publish(args);
    }
}