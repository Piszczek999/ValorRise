using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.PlayerFireball, MessageSendMode.Reliable)]
public record PlayerFireballPacket(long Timestamp, ObjectId CasterId, ObjectId FireballId, Vector2 Direction) : IServerPacket
{
    public PlayerFireballPacket(ObjectId CasterId, ObjectId FireballId, Vector2 Direction) : this(
        DateTimeOffset.Now.ToUnixTimeMilliseconds(),
        CasterId,
        FireballId,
        Direction)
    { }

    public PlayerFireballPacket(Message buffer) : this(
        buffer.GetLong(),
        buffer.GetObjectId(),
        buffer.GetObjectId(),
        buffer.GetVector2())
    { }

    public void Write(Message buffer) => buffer
        .AddLong(Timestamp)
        .AddObjectId(CasterId)
        .AddObjectId(FireballId)
        .AddVector2(Direction);
}