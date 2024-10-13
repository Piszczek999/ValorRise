using ValorRiseGameServer.Events;

namespace ValorRiseGameServer;

/// <summary>
/// Interface for an event node that handles events of type <typeparamref name="TEvent"/>.
/// </summary>
/// <typeparam name="TEvent">The type of events handled by this node, which must implement <see cref="IEvent"/>.</typeparam>
public interface IEventNode<TEvent> where TEvent : IEvent
{
    /// <summary>
    /// Adds a child event node.
    /// </summary>
    /// <typeparam name="T">The type of events that the child node will handle, which must be derived from <typeparamref name="TEvent"/>.</typeparam>
    /// <param name="child">The child event node to be added.</param>
    void AddChild<T>(EventNode<T> child) where T : TEvent;

    /// <summary>
    /// Adds a listener for a specific event type.
    /// </summary>
    /// <typeparam name="T">The type of event to listen for, which must be derived from <typeparamref name="TEvent"/>.</typeparam>
    /// <param name="listener">The action to execute when the event is raised.</param>
    void AddListener<T>(Action<T> listener) where T : TEvent;

    /// <summary>
    /// Removes a listener for a specific event type.
    /// </summary>
    /// <typeparam name="T">The type of event for which to remove the listener, which must be derived from <typeparamref name="TEvent"/>.</typeparam>
    /// <param name="listener">The action to remove from the list of listeners.</param>
    void RemoveListener<T>(Action<T> listener) where T : TEvent;

    /// <summary>
    /// Dispatches an event to this node and its children.
    /// </summary>
    /// <param name="event">The event to be dispatched.</param>
    void Invoke(TEvent @event);
}
