using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.EntitySpawn, MessageSendMode.Reliable)]
public record EntitySpawnPacket(
    uint Id,
    EntityType EntityType,
    Vector2 Position,
    float Health,
    float MaxHealth,
    float AttackSpeed,
    string Name) : IServerPacket
{
    public EntitySpawnPacket(Message packet) : this(
        packet.GetUInt(),
        packet.GetEntityType(),
        packet.GetVector2(),
        packet.GetFloat(),
        packet.GetFloat(),
        packet.GetFloat(),
        packet.GetString())
    { }

    public void Write(Message packet) => packet
        .AddUInt(Id)
        .AddEntityType(EntityType)
        .AddVector2(Position)
        .AddFloat(Health)
        .AddFloat(MaxHealth)
        .AddFloat(AttackSpeed)
        .AddString(Name);
}