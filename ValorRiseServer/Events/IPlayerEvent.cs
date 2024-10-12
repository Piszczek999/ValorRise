using ValorRiseServer.Entities;

namespace ValorRiseServer;

public interface IPlayerEvent : IEntityEvent
{
    Player Player { get; }

    new Entity Entity => Player;
}