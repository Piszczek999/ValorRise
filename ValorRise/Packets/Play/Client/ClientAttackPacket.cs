using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Client;

[Packet(PacketType.ClientAttack, MessageSendMode.Reliable)]
public record ClientAttackPacket(uint TargetId) : IClientPacket
{
    public ClientAttackPacket(Message packet) : this(
        TargetId: packet.GetUInt())
    { }

    public void Write(Message packet) => packet
        .AddUInt(TargetId);
}