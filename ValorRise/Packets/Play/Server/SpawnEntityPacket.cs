using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.SpawnEntity, MessageSendMode.Reliable)]
public record SpawnEntityPacket(
    ObjectId Id,
    EntityType Type,
    Vector2 Position,
    Vector2 Destination) : IServerPacket
{
    public SpawnEntityPacket(Message packet) : this(
        packet.GetObjectId(),
        packet.GetEntityType(),
        packet.GetVector2(),
        packet.GetVector2())
    { }

    public void Write(Message packet) => packet
        .AddObjectId(Id)
        .AddEntityType(Type)
        .AddVector2(Position)
        .AddVector2(Destination);
}