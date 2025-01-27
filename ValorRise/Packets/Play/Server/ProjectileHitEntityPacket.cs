using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.ProjectileHitEntity, MessageSendMode.Reliable)]
public record ProjectileHitEntityPacket(uint ProjectileId, uint HitEntityId) : IServerPacket
{
    public ProjectileHitEntityPacket(Message packet) : this(
        packet.GetUInt(),
        packet.GetUInt())
    { }

    public void Write(Message packet) => packet
        .AddUInt(ProjectileId)
        .AddUInt(HitEntityId);
}