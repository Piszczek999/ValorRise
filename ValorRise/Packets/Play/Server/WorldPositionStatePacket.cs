using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.WorldPositionState, MessageSendMode.Unreliable)]
public record WorldPositionStatePacket(long Timestamp, EntityPositionState[] EntityStates) : IServerPacket
{
    public WorldPositionStatePacket(EntityPositionState[] EntityStates) : this(
        DateTimeOffset.Now.ToUnixTimeMilliseconds(),
        EntityStates)
    { }

    public WorldPositionStatePacket(Message buffer) : this(
        buffer.GetLong(),
        buffer.GetSerializables<EntityPositionState>())
    { }

    public void Write(Message buffer) => buffer
        .AddLong(Timestamp)
        .AddSerializables(EntityStates);
}

public class EntityPositionState : IMessageSerializable
{
    public uint Id { get; set; }
    public Vector2 Position { get; set; }

    public void Deserialize(Message message)
    {
        Id = message.GetUInt();
        Position = message.GetVector2();
    }

    public void Serialize(Message message) => message
        .AddUInt(Id)
        .AddVector2(Position);

    public const int ByteSize = 12;
}