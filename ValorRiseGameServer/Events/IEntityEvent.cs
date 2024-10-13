using ValorRiseGameServer.Entities;

namespace ValorRiseGameServer.Events;

public interface IEntityEvent : IEvent
{
    Entity Entity { get; }
}