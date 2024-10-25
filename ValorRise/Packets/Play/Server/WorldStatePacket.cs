using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.WorldState, MessageSendMode.Unreliable)]
public record WorldStatePacket(long Timestamp, EntityState[] EntityStates) : IServerPacket
{
    public WorldStatePacket(EntityState[] EntityStates) : this(
        DateTimeOffset.Now.ToUnixTimeMilliseconds(),
        EntityStates)
    { }

    public WorldStatePacket(Message buffer) : this(
        buffer.GetLong(),
        buffer.GetSerializables<EntityState>())
    { }

    public void Write(Message buffer) => buffer
        .AddLong(Timestamp)
        .AddSerializables(EntityStates);
}

public class EntityState : IMessageSerializable
{
    public ObjectId Id { get; set; }
    public Vector2 Position { get; set; }

    public void Deserialize(Message message)
    {
        Id = message.GetObjectId();
        Position = message.GetVector2();
    }

    public void Serialize(Message message) => message
        .AddObjectId(Id)
        .AddVector2(Position);
}