using Riptide;
using ValorRise.Client.Entities;

namespace ValorRise.Client.Messages;

[Message((ushort)MessageType.ToClient.InitMainPlayer)]
internal class InitMainPlayer : IMessageHandler
{
    private GlobalEventHandler _eventHandler = MMOClient.GlobalEventHandler;

    public void HandleMessage(Message message)
    {
        var playerEntity = (PlayerEntity)Entity.Deserialize(message);

        var args = new InitMainPlayerEvent(playerEntity);
        _eventHandler.InvokeEvent(args);
    }
}

public class InitMainPlayerEvent : EventArgs
{
    public PlayerEntity PlayerEntity { get; }

    public InitMainPlayerEvent(PlayerEntity playerEntity)
    {
        PlayerEntity = playerEntity;
    }
}