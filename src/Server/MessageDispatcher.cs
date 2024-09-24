namespace MMO_Library.Server;
using Riptide;

internal class MessageDispatcher
{
    private readonly Dictionary<ushort, IMessageHandler> _messageHandlers;
    private readonly EventBus _eventBus;
    private readonly Server _server;

    public MessageDispatcher(EventBus eventBus, Server server)
    {
        _eventBus = eventBus;
        _server = server;

        _messageHandlers = new()
        {
            {(ushort)MessageType.ToGateway.LoginRequest, new LoginRequestMessageHandler(_eventBus, _server)},
            {(ushort)MessageType.ToGateway.RegisterRequest, new RegisterRequestMessageHandler(_eventBus, _server)},
            {(ushort)MessageType.ToGateway.NewCharacterRequest, new NewCharacterRequestMessageHandler(_eventBus, _server)},
            {(ushort)MessageType.ToGateway.CharacterSelectRequest, new CharacterSelectRequestMessageHandler(_eventBus, _server)},

            {(ushort)MessageType.ToAuthenticate.CharacterSelectAuthRequest, new CharacterSelectAuthRequestMessageHandler(_eventBus, _server)},
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