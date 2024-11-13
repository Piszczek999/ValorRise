using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.PlayerFireball, MessageSendMode.Reliable)]
public record PlayerFireballPacket(long Timestamp, uint CasterId, uint FireballId, Vector2 Direction) : IServerPacket
{
    public PlayerFireballPacket(uint CasterId, uint FireballId, Vector2 Direction) : this(
        DateTimeOffset.Now.ToUnixTimeMilliseconds(),
        CasterId,
        FireballId,
        Direction)
    { }

    public PlayerFireballPacket(Message buffer) : this(
        buffer.GetLong(),
        buffer.GetUInt(),
        buffer.GetUInt(),
        buffer.GetVector2())
    { }

    public void Write(Message buffer) => buffer
        .AddLong(Timestamp)
        .AddUInt(CasterId)
        .AddUInt(FireballId)
        .AddVector2(Direction);
}