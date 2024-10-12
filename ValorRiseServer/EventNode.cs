namespace ValorRiseServer;

public class EventNode<TEvent> : IEventNode<TEvent> where TEvent : IEvent
{
    private readonly Dictionary<Type, List<Action<TEvent>>> _listeners = new();
    private readonly List<EventNode<TEvent>> _children = new();

    public void AddChild<T>(EventNode<T> child) where T : TEvent
    {
        _children.Add(child as EventNode<TEvent>);
    }

    public void AddListener<T>(Action<T> listener) where T : TEvent
    {
        var type = typeof(T);
        if (!_listeners.ContainsKey(type))
        {
            _listeners[type] = new List<Action<TEvent>>();
        }
        _listeners[type].Add(listener as Action<TEvent>);
    }

    public void RemoveListener<T>(Action<T> listener) where T : TEvent
    {
        var type = typeof(T);
        if (_listeners.ContainsKey(type))
        {
            _listeners[type].Remove(listener as Action<TEvent>);
        }
    }

    public void Invoke(TEvent @event)
    {
        // Invoke listeners in this node
        var eventType = @event.GetType();
        if (_listeners.TryGetValue(eventType, out var eventListeners))
        {
            foreach (Delegate listener in eventListeners)
            {
                listener.DynamicInvoke(@event);

                if (@event is ICancellableEvent cancellable && cancellable.IsCancelled)
                {
                    return;
                }
            }
        }

        // Propagate to children nodes
        foreach (var child in _children)
        {
            if (eventType.IsAssignableFrom(child.GetType().GetGenericArguments()[0]))
            {
                child.Invoke(@event);
            }
        }
    }
}
