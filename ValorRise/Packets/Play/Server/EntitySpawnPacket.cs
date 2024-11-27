using System.Numerics;
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
    public EntitySpawnPacket(Message buffer) : this(
        buffer.GetUInt(),
        (EntityType)buffer.GetByte(),
        buffer.GetVector2(),
        buffer.GetShort(),
        buffer.GetShort(),
        buffer.GetShort() / 100f,
        buffer.GetString())
    { }

    public void Write(Message buffer) => buffer
        .AddUInt(Id)
        .AddByte((byte)EntityType)
        .AddVector2(Position)
        .AddShort((short)(Health + 0.5f))
        .AddShort((short)(MaxHealth + 0.5f))
        .AddShort((short)(AttackSpeed * 100))
        .AddString(Name);
}