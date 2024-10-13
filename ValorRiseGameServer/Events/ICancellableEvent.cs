namespace ValorRiseGameServer.Events;

public interface ICancellableEvent : IEvent
{
    bool IsCancelled { get; set; }
}