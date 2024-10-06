using MongoDB.Bson;
using Riptide;

namespace ValorRise.Server.Messages;

[Message((ushort)MessageType.ToAuthenticate.NewCharacterAuthRequest)]
internal class NewCharacterAuthRequest : IMessageHandler
{
    private GlobalEventHandler _eventHandler = MMOServer.GlobalEventHandler;

    public void HandleMessage(ushort gatewayId, Message message)
    {
        ushort clientId = message.GetUShort();
        ObjectId userId = message.GetObjectId();
        string name = message.GetString();

        var args = new NewCharacterAuthRequestEvent(gatewayId, clientId, userId, name);
        _eventHandler.InvokeEvent(args);
    }
}

public class NewCharacterAuthRequestEvent : EventArgs
{
    public ushort GatewayId { get; }
    public Connection Gateway { get => MMOServer.TryGetClient(GatewayId, out var gateway) ? gateway : null; }
    public ushort ClientId { get; }
    public ObjectId UserId { get; }
    public string Name { get; }

    public NewCharacterAuthRequestEvent(ushort gatewayId, ushort clientId, ObjectId userId, string name)
    {
        GatewayId = gatewayId;
        ClientId = clientId;
        UserId = userId;
        Name = name;
    }
}