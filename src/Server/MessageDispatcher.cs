namespace MMO_Library.Server;
using Riptide;

public class MessageDispatcher
{
    private readonly Dictionary<ushort, IMessageHandler> _messageHandlers;
    private readonly EventBus _eventBus;
    private readonly ConnectionManager _connectionManager;

    public MessageDispatcher(EventBus eventBus, ConnectionManager connectionManager)
    {
        _eventBus = eventBus;
        _connectionManager = connectionManager;

        _messageHandlers = new()
        {
            {(ushort)MessageType.ToGateway.LoginRequest, new LoginRequestMessageHandler(_eventBus, _connectionManager)},
            {(ushort)MessageType.ToGateway.RegisterRequest, new RegisterRequestMessageHandler(_eventBus, _connectionManager)},
        };
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
}