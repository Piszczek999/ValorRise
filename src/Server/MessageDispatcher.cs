using Riptide;

namespace ValorRise.Server;

internal class MessageDispatcher
{
    private readonly Dictionary<ushort, IMessageHandler> _messageHandlers = new();

    public MessageDispatcher()
    {
        RegisterMessageHandlers();
    }

    public void Register(ushort messageId, IMessageHandler handler)
    {
        _messageHandlers[messageId] = handler;
    }

    public void Dispatch(ushort clientId, Message message, ushort messageId)
    {
        if (_messageHandlers.TryGetValue(messageId, out var handler))
        {
            handler.HandleMessage(clientId, message);
        }
        else
        {
            Logger.Warning($"Unhandled message type: {messageId}");
        }
    }

    private void RegisterMessageHandlers()
    {
        var handlerTypes = typeof(IMessageHandler).Assembly.GetTypes()
             .Where(t => typeof(IMessageHandler).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var handlerType in handlerTypes)
        {
            var attribute = (MessageAttribute)Attribute.GetCustomAttribute(handlerType, typeof(MessageAttribute));

            if (attribute != null)
            {
                var handler = (IMessageHandler)Activator.CreateInstance(handlerType);
                Register(attribute.MessageId, handler);
            }
            else
            {
                Logger.Warning($"Handler {handlerType.Name} does not have a MessageAttribute and will not be registered.");
            }
        }
    }
}