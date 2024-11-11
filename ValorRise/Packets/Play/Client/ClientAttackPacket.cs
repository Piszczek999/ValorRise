using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Client;

[Packet(PacketType.ClientAttack, MessageSendMode.Reliable)]
public record ClientAttackPacket(ObjectId TargetId) : IClientPacket
{
    public ClientAttackPacket(Message packet) : this(
        TargetId: packet.GetObjectId())
    { }

    public void Write(Message packet) => packet
        .AddObjectId(TargetId);
}