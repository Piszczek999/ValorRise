namespace ValorRiseServer;

public interface ICancellableEvent : IEvent
{
    bool IsCancelled { get; set; }
}