namespace MMOLibrary;

public class EventBus
{
    private readonly Dictionary<Type, List<Action<EventArgs>>> _handlers = new();

    public void Subscribe<TEventArgs>(Action<TEventArgs> handler) where TEventArgs : EventArgs
    {
        if (!_handlers.ContainsKey(typeof(TEventArgs)))
        {
            _handlers[typeof(TEventArgs)] = new List<Action<EventArgs>>();
        }

        _handlers[typeof(TEventArgs)].Add((args) => handler((TEventArgs)args));
    }

    public void Publish<T>(T args) where T : EventArgs
    {
        if (_handlers.TryGetValue(args.GetType(), out var handlers))
        {
            foreach (var handler in handlers)
            {
                handler(args);
            }
        }
        else
        {
            Logger.Warning($"Unhandled event type: {args.GetType()}");
        }
    }
}