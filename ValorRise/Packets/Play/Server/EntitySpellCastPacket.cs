using System.Numerics;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.EntitySpellCast, MessageSendMode.Reliable)]
public record EntitySpellCastPacket(uint CasterId, SpellName SpellName, Vector2 Direction) : IServerPacket
{
    public EntitySpellCastPacket(Message buffer) : this(
        buffer.GetUInt(),
        (SpellName)buffer.GetByte(),
        buffer.GetVector2())
    { }

    public void Write(Message buffer) => buffer
        .AddUInt(CasterId)
        .AddByte((byte)SpellName)
        .AddVector2(Direction);
}