using MongoDB.Bson;
using Riptide;

namespace ValorRise.Server.Messages;

[Message((ushort)MessageType.ToAuthenticate.NewCharacterAuthRequest)]
internal class NewCharacterAuthRequest : IMessageHandler
{
    public void HandleMessage(ushort gatewayId, Message message)
    {
        ushort clientId = message.GetUShort();
        ObjectId userId = message.GetObjectId();
        string name = message.GetString();

        if (!MMOServer.TryGetClient(gatewayId, out var gateway)) throw new InvalidOperationException("Gateway not found for specified clientId");
        var args = new NewCharacterAuthRequestEvent(gateway, clientId, userId, name);
        MMOServer.EventBus.Publish(args);
    }
}

public class NewCharacterAuthRequestEvent : EventArgs
{
    public Connection Gateway { get; }
    public ushort ClientId { get; }
    public ObjectId UserId { get; }
    public string Name { get; }

    public NewCharacterAuthRequestEvent(Connection gateway, ushort clientId, ObjectId userId, string name)
    {
        Gateway = gateway;
        ClientId = clientId;
        UserId = userId;
        Name = name;
    }
}