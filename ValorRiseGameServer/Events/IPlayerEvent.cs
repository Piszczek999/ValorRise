using ValorRiseGameServer.Entities;

namespace ValorRiseGameServer.Events;

public interface IPlayerEvent : IEntityEvent
{
    Player Player { get; }

    new Entity Entity => Player;
}