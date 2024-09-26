namespace MMOLibrary.Server;
using Riptide;

internal class MessageDispatcher
{
    private readonly Dictionary<ushort, IMessageHandler> _messageHandlers;
    private readonly EventBus _eventBus;
    private readonly MMOServer _server;

    public MessageDispatcher(EventBus eventBus, MMOServer server, ServerType type)
    {
        _eventBus = eventBus;
        _server = server;

        _messageHandlers = type switch
        {
            ServerType.Gateway => new Dictionary<ushort, IMessageHandler>
            {
                {(ushort)MessageType.ToGateway.LoginRequest, new LoginRequestMessageHandler(_eventBus, _server)},
                {(ushort)MessageType.ToGateway.RegisterRequest, new RegisterRequestMessageHandler(_eventBus, _server)},
                {(ushort)MessageType.ToGateway.NewCharacterRequest, new NewCharacterRequestMessageHandler(_eventBus, _server)},
                {(ushort)MessageType.ToGateway.CharacterSelectRequest, new CharacterSelectRequestMessageHandler(_eventBus, _server)},
            },
            ServerType.Authenticate => new Dictionary<ushort, IMessageHandler>
            {
                {(ushort)MessageType.ToAuthenticate.LoginAuthRequest, new LoginAuthRequestMessageHandler(_eventBus, _server)},
                {(ushort)MessageType.ToAuthenticate.RegisterAuthRequest, new RegisterAuthRequestMessageHandler(_eventBus, _server)},
                {(ushort)MessageType.ToAuthenticate.NewCharacterAuthRequest, new NewCharacterAuthRequestMessageHandler(_eventBus, _server)},
                {(ushort)MessageType.ToAuthenticate.CharacterSelectAuthRequest, new CharacterSelectAuthRequestMessageHandler(_eventBus, _server)},
            },
            ServerType.GameServer => new Dictionary<ushort, IMessageHandler>(),
            _ => throw new NotImplementedException(),
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