using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.EntityAttack, MessageSendMode.Reliable)]
public record EntityAttackPacket(
    uint AttackerId,
    uint TargetId) : IServerPacket
{
    public EntityAttackPacket(Message packet) : this(
        packet.GetUInt(),
        packet.GetUInt())
    { }

    public void Write(Message packet) => packet
        .AddUInt(AttackerId)
        .AddUInt(TargetId);
}