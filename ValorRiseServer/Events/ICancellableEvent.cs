namespace ValorRiseServer;

public interface ICancellableEvent : IEvent
{
    bool IsCancelled { get; }
    void SetCancelled(bool value);
}