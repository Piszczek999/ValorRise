using ValorRiseServer.Entities;

namespace ValorRiseServer;

public interface IEntityEvent : IEvent
{
    Entity Entity { get; }
}