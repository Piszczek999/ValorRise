using System.Numerics;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.ItemSpawn, MessageSendMode.Reliable)]
public record ItemSpawnPacket(
    uint Id,
    Vector2 Position,
    ItemName ItemName,
    ushort Amount) : IServerPacket
{
    public ItemSpawnPacket(Message buffer) : this(
        buffer.GetUInt(),
        buffer.GetVector2(),
        (ItemName)buffer.GetByte(),
        buffer.GetUShort())
    { }

    public void Write(Message buffer) => buffer
        .AddUInt(Id)
        .AddVector2(Position)
        .AddByte((byte)ItemName)
        .AddUShort(Amount);
}