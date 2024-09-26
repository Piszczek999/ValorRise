namespace MMOLibrary.Client;
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
                {(ushort)MessageType.ToGateway.LoginResponse, new LoginResponseMessageHandler(_eventBus)},
                {(ushort)MessageType.ToGateway.RegisterResponse, new RegisterResponseMessageHandler(_eventBus)},
                {(ushort)MessageType.ToGateway.CharactersInfoResponse, new CharactersInfoResponseMessageHandler(_eventBus)},
                {(ushort)MessageType.ToGateway.NewCharacterResponse, new NewCharacterResponseMessageHandler(_eventBus)},
                {(ushort)MessageType.ToGateway.CharacterSelectResponse, new CharacterSelectResponseMessageHandler(_eventBus)},
            },
            ClientType.Client => new Dictionary<ushort, IMessageHandler>
            {
                {(ushort)MessageType.ToClient.LoginResult, new LoginResultMessageHandler(_eventBus)},
                {(ushort)MessageType.ToClient.RegisterResult, new RegisterResultMessageHandler(_eventBus)},
                {(ushort)MessageType.ToClient.CharactersInfoResult, new CharactersInfoResultMessageHandler(_eventBus)},
                {(ushort)MessageType.ToClient.NewCharacterResult, new NewCharacterResultMessageHandler(_eventBus)},
                {(ushort)MessageType.ToClient.TokenVerificationResult, new TokenVerificationResultMessageHandler(_eventBus)},
            },
            ClientType.GameServer => new Dictionary<ushort, IMessageHandler>(),
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