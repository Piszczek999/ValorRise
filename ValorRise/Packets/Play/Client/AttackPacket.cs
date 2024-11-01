using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Client;

[Packet(PacketType.Attack, MessageSendMode.Reliable)]
public record AttackPacket(long Timestamp, ObjectId TargetId) : IClientPacket
{
    public AttackPacket(ObjectId TargetId) : this(
        DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
        TargetId)
    { }

    public AttackPacket(Message packet) : this(
        Timestamp: packet.GetLong(),
        TargetId: packet.GetObjectId())
    { }

    public void Write(Message packet) => packet
        .AddLong(Timestamp)
        .AddObjectId(TargetId);
}