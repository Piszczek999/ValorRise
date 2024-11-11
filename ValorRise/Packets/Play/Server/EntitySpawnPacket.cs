using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.EntitySpawn, MessageSendMode.Reliable)]
public record EntitySpawnPacket(
    ObjectId Id,
    EntityType EntityType,
    Vector2 Position,
    float Health,
    float MaxHealth,
    string Name) : IServerPacket
{
    public EntitySpawnPacket(Message packet) : this(
        packet.GetObjectId(),
        packet.GetEntityType(),
        packet.GetVector2(),
        packet.GetFloat(),
        packet.GetFloat(),
        packet.GetString())
    { }

    public void Write(Message packet) => packet
        .AddObjectId(Id)
        .AddEntityType(EntityType)
        .AddVector2(Position)
        .AddFloat(Health)
        .AddFloat(MaxHealth)
        .AddString(Name);
}