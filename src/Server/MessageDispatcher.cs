using ValorRise.Server.Messages;
using Riptide;

namespace ValorRise.Server;

internal class MessageDispatcher
{
    private readonly Dictionary<ushort, IMessageHandler> _messageHandlers;
    private readonly EventBus _eventBus;

    public MessageDispatcher(EventBus eventBus)
    {
        _eventBus = eventBus;

        _messageHandlers = new Dictionary<ushort, IMessageHandler>
        {
            {(ushort)MessageType.ToGateway.LoginRequest, new LoginRequest(_eventBus)},
            {(ushort)MessageType.ToGateway.RegisterRequest, new RegisterRequest(_eventBus)},
            {(ushort)MessageType.ToGateway.NewCharacterRequest, new NewCharacterRequest(_eventBus)},
            {(ushort)MessageType.ToGateway.CharacterSelectRequest, new CharacterSelectRequest(_eventBus)},

            {(ushort)MessageType.ToAuthenticate.LoginAuthRequest, new LoginAuthRequest(_eventBus)},
            {(ushort)MessageType.ToAuthenticate.RegisterAuthRequest, new RegisterAuthRequest(_eventBus)},
            {(ushort)MessageType.ToAuthenticate.NewCharacterAuthRequest, new NewCharacterAuthRequest(_eventBus)},
            {(ushort)MessageType.ToAuthenticate.CharacterSelectAuthRequest, new CharacterSelectAuthRequest(_eventBus)},

            {(ushort)MessageType.ToGameServer.VerifyTokenRequest, new VerifyTokenRequest(_eventBus)},
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