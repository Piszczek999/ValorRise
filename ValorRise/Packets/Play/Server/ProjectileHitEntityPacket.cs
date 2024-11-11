using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.ProjectileHitEntity, MessageSendMode.Reliable)]
public record ProjectileHitEntityPacket(ObjectId ProjectileId, ObjectId HitEntityId) : IServerPacket
{
    public ProjectileHitEntityPacket(Message packet) : this(
        packet.GetObjectId(),
        packet.GetObjectId())
    { }

    public void Write(Message packet) => packet
        .AddObjectId(ProjectileId)
        .AddObjectId(HitEntityId);
}