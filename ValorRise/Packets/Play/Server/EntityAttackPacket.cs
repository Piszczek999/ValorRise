using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.EntityAttack, MessageSendMode.Reliable)]
public record EntityAttackPacket(
    long Timestamp,
    uint AttackerId,
    uint TargetId) : IServerPacket
{
    public EntityAttackPacket(uint AttackerId, uint TargetId) : this(
        DateTimeOffset.Now.ToUnixTimeMilliseconds(),
        AttackerId,
        TargetId)
    { }

    public EntityAttackPacket(Message packet) : this(
        packet.GetLong(),
        packet.GetUInt(),
        packet.GetUInt())
    { }

    public void Write(Message packet) => packet
        .AddLong(Timestamp)
        .AddUInt(AttackerId)
        .AddUInt(TargetId);
}