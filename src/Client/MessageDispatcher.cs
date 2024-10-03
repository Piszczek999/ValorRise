namespace MMOLibrary.Client;

using MMOLibrary.Client.Messages;
using Riptide;

internal class MessageDispatcher
{
    private readonly Dictionary<ushort, IMessageHandler> _messageHandlers;
    private readonly EventBus _eventBus;

    public MessageDispatcher(EventBus eventBus, ClientType type)
    {
        _eventBus = eventBus;
        _messageHandlers = type switch
        {
            ClientType.Gateway => new Dictionary<ushort, IMessageHandler>
            {
                {(ushort)MessageType.ToGateway.LoginAuthResponse, new LoginAuthResponse(_eventBus)},
                {(ushort)MessageType.ToGateway.RegisterAuthResponse, new RegisterAuthResponse(_eventBus)},
                {(ushort)MessageType.ToGateway.CharactersInfoAuthResponse, new CharactersInfoAuthResponse(_eventBus)},
                {(ushort)MessageType.ToGateway.NewCharacterAuthResponse, new NewCharacterAuthResponse(_eventBus)},
                {(ushort)MessageType.ToGateway.CharacterSelectAuthResponse, new CharacterSelectAuthResponse(_eventBus)},
            },
            ClientType.Client => new Dictionary<ushort, IMessageHandler>
            {
                {(ushort)MessageType.ToClient.LoginResponse, new LoginResponse(_eventBus)},
                {(ushort)MessageType.ToClient.RegisterResponse, new RegisterResponse(_eventBus)},
                {(ushort)MessageType.ToClient.CharactersInfoResponse, new CharactersInfoResponse(_eventBus)},
                {(ushort)MessageType.ToClient.NewCharacterResponse, new NewCharacterResponse(_eventBus)},
                {(ushort)MessageType.ToClient.CharacterSelectResponse, new CharacterSelectResponse(_eventBus)},
                {(ushort)MessageType.ToClient.VerifyTokenResponse, new VerifyTokenResponse(_eventBus)},
            },
            ClientType.GameServer => new Dictionary<ushort, IMessageHandler>
            {
                {(ushort)MessageType.ToGameServer.VerifyTokenDBResponse, new VerifyTokenDBResponse(_eventBus)},
            },
            ClientType.Authenticate => new Dictionary<ushort, IMessageHandler>
            {
                {(ushort)MessageType.ToAuthenticate.LoginDBResponse, new LoginDBResponse(_eventBus)},
                {(ushort)MessageType.ToAuthenticate.RegisterDBResponse, new RegisterDBResponse(_eventBus)},
                {(ushort)MessageType.ToAuthenticate.CharactersInfoDBResponse, new CharactersInfoDBResponse(_eventBus)},
                {(ushort)MessageType.ToAuthenticate.NewCharacterDBResponse, new NewCharacterDBResponse(_eventBus)},
                {(ushort)MessageType.ToAuthenticate.CharacterSelectDBResponse, new CharacterSelectDBResponse(_eventBus)},
            },
            _ => throw new NotImplementedException(),
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