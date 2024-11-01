using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.SpawnEntity, MessageSendMode.Reliable)]
public record SpawnEntityPacket(
    ObjectId Id,
    EntityType EntityType,
    Vector2 Position) : IServerPacket
{
    public SpawnEntityPacket(Message packet) : this(
        packet.GetObjectId(),
        packet.GetEntityType(),
        packet.GetVector2())
    { }

    public void Write(Message packet) => packet
        .AddObjectId(Id)
        .AddEntityType(EntityType)
        .AddVector2(Position);
}