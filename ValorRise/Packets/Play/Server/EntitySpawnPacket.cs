using System.Numerics;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.EntitySpawn, MessageSendMode.Reliable)]
public record EntitySpawnPacket(
    uint Id,
    EntityName EntityName,
    Vector2 Position,
    string Name) : IServerPacket
{
    public EntitySpawnPacket(Message buffer) : this(
        buffer.GetUInt(),
        (EntityName)buffer.GetByte(),
        buffer.GetVector2(),
        buffer.GetString())
    { }

    public void Write(Message buffer) => buffer
        .AddUInt(Id)
        .AddByte((byte)EntityName)
        .AddVector2(Position)
        .AddString(Name);
}