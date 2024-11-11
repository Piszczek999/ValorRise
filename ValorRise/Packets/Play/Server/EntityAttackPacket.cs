using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.EntityAttack, MessageSendMode.Reliable)]
public record EntityAttackPacket(
    long Timestamp,
    ObjectId AttackerId,
    ObjectId TargetId) : IServerPacket
{
    public EntityAttackPacket(ObjectId AttackerId, ObjectId TargetId) : this(
        DateTimeOffset.Now.ToUnixTimeMilliseconds(),
        AttackerId,
        TargetId)
    { }

    public EntityAttackPacket(Message packet) : this(
        packet.GetLong(),
        packet.GetObjectId(),
        packet.GetObjectId())
    { }

    public void Write(Message packet) => packet
        .AddLong(Timestamp)
        .AddObjectId(AttackerId)
        .AddObjectId(TargetId);
}