namespace MMO_Library.Client;
using Riptide;

public class MessageDispatcher
{
    private readonly Dictionary<ushort, IMessageHandler> _messageHandlers;
    private readonly EventBus _eventBus;

    public MessageDispatcher(EventBus eventBus)
    {
        _eventBus = eventBus;
        _messageHandlers = new()
        {
            {(ushort)MessageType.ToGateway.LoginResponse, new LoginResponseMessageHandler(_eventBus)},
            {(ushort)MessageType.ToGateway.RegisterResponse, new RegisterResponseMessageHandler(_eventBus)},
        };
    }

    public void Dispatch(Message message, ushort messageId)
    {
        if (_messageHandlers.TryGetValue(messageId, out var handler))
        {
            handler.HandleMessage(message);
        }
        else
        {
            Logger.Warning($"Unhandled message type: {messageId}");
        }
    }
}