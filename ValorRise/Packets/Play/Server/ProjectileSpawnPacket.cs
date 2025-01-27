using System.Numerics;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.ProjectileSpawn, MessageSendMode.Reliable)]
public record ProjectileSpawnPacket(
    uint Id,
    ProjectileName ProjectileName,
    Vector2 Position,
    Vector2 Direction) : IServerPacket
{
    public ProjectileSpawnPacket(Message buffer) : this(
        buffer.GetUInt(),
        (ProjectileName)buffer.GetByte(),
        buffer.GetVector2(),
        buffer.GetVector2())
    { }

    public void Write(Message buffer) => buffer
        .AddUInt(Id)
        .AddByte((byte)ProjectileName)
        .AddVector2(Position)
        .AddVector2(Direction);
}