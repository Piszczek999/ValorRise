namespace MMOLibrary.Server;
using Riptide;

internal class MessageDispatcher
{
    private readonly Dictionary<ushort, IMessageHandler> _messageHandlers;
    private readonly EventBus _eventBus;

    public MessageDispatcher(EventBus eventBus, ServerType type)
    {
        _eventBus = eventBus;

        _messageHandlers = type switch
        {
            ServerType.Gateway => new Dictionary<ushort, IMessageHandler>
            {
                {(ushort)MessageType.ToGateway.LoginRequest, new LoginRequestMessageHandler(_eventBus)},
                {(ushort)MessageType.ToGateway.RegisterRequest, new RegisterRequestMessageHandler(_eventBus)},
                {(ushort)MessageType.ToGateway.NewCharacterRequest, new NewCharacterRequestMessageHandler(_eventBus)},
                {(ushort)MessageType.ToGateway.CharacterSelectRequest, new CharacterSelectRequestMessageHandler(_eventBus)},
            },
            ServerType.Authenticate => new Dictionary<ushort, IMessageHandler>
            {
                {(ushort)MessageType.ToAuthenticate.LoginAuthRequest, new LoginAuthRequestMessageHandler(_eventBus)},
                {(ushort)MessageType.ToAuthenticate.RegisterAuthRequest, new RegisterAuthRequestMessageHandler(_eventBus)},
                {(ushort)MessageType.ToAuthenticate.NewCharacterAuthRequest, new NewCharacterAuthRequestMessageHandler(_eventBus)},
                {(ushort)MessageType.ToAuthenticate.CharacterSelectAuthRequest, new CharacterSelectAuthRequestMessageHandler(_eventBus)},
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