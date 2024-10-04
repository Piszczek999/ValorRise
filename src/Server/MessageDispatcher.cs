namespace MMOLibrary.Server;

using MMOLibrary.Server.Messages;
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
                {(ushort)MessageType.ToGateway.LoginRequest, new LoginRequest(_eventBus)},
                {(ushort)MessageType.ToGateway.RegisterRequest, new RegisterRequest(_eventBus)},
                {(ushort)MessageType.ToGateway.NewCharacterRequest, new NewCharacterRequest(_eventBus)},
                {(ushort)MessageType.ToGateway.CharacterSelectRequest, new CharacterSelectRequest(_eventBus)},
            },
            ServerType.Authenticate => new Dictionary<ushort, IMessageHandler>
            {
                {(ushort)MessageType.ToAuthenticate.LoginAuthRequest, new LoginAuthRequest(_eventBus)},
                {(ushort)MessageType.ToAuthenticate.RegisterAuthRequest, new RegisterAuthRequest(_eventBus)},
                {(ushort)MessageType.ToAuthenticate.NewCharacterAuthRequest, new NewCharacterAuthRequest(_eventBus)},
                {(ushort)MessageType.ToAuthenticate.CharacterSelectAuthRequest, new CharacterSelectAuthRequest(_eventBus)},
            },
            ServerType.GameServer => new Dictionary<ushort, IMessageHandler>
            {
                {(ushort)MessageType.ToGameServer.VerifyTokenRequest, new VerifyTokenRequest(_eventBus)},
            },
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