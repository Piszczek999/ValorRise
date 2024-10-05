using Riptide;
using ValorRise.Client.Messages;

namespace ValorRise.Client;

internal class MessageDispatcher
{
    private readonly Dictionary<ushort, IMessageHandler> _messageHandlers;
    private readonly EventBus _eventBus;

    public MessageDispatcher(EventBus eventBus)
    {
        _eventBus = eventBus;
        _messageHandlers = new Dictionary<ushort, IMessageHandler>
        {
            {(ushort)MessageType.ToGateway.LoginAuthResponse, new LoginAuthResponse(_eventBus)},
            {(ushort)MessageType.ToGateway.RegisterAuthResponse, new RegisterAuthResponse(_eventBus)},
            {(ushort)MessageType.ToGateway.CharactersInfoAuthResponse, new CharactersInfoAuthResponse(_eventBus)},
            {(ushort)MessageType.ToGateway.NewCharacterAuthResponse, new NewCharacterAuthResponse(_eventBus)},
            {(ushort)MessageType.ToGateway.CharacterSelectAuthResponse, new CharacterSelectAuthResponse(_eventBus)},

            {(ushort)MessageType.ToClient.LoginResponse, new LoginResponse(_eventBus)},
            {(ushort)MessageType.ToClient.RegisterResponse, new RegisterResponse(_eventBus)},
            {(ushort)MessageType.ToClient.CharactersInfoResponse, new CharactersInfoResponse(_eventBus)},
            {(ushort)MessageType.ToClient.NewCharacterResponse, new NewCharacterResponse(_eventBus)},
            {(ushort)MessageType.ToClient.CharacterSelectResponse, new CharacterSelectResponse(_eventBus)},

            {(ushort)MessageType.ToClient.VerifyTokenResponse, new VerifyTokenResponse(_eventBus)},
            {(ushort)MessageType.ToClient.InitLevel, new InitLevel(_eventBus)},
            {(ushort)MessageType.ToClient.InitMainPlayer, new InitMainPlayer(_eventBus)},
            {(ushort)MessageType.ToClient.SpawnEntity, new SpawnEntity(_eventBus)},

            {(ushort)MessageType.ToGameServer.PlayerToken, new PlayerToken(_eventBus)},
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