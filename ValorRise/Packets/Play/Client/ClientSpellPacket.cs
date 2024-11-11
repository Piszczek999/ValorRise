using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Client;

[Packet(PacketType.ClientSpell, MessageSendMode.Reliable)]
public record ClientSpellPacket(SpellName SpellName, Vector2 TargetPosition) : IClientPacket
{
    public ClientSpellPacket(Message packet) : this(
        SpellName: (SpellName)packet.GetByte(),
        TargetPosition: packet.GetVector2())
    { }

    public void Write(Message packet) => packet
        .AddByte((byte)SpellName)
        .AddVector2(TargetPosition);
}