using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.EntityHealthChange, MessageSendMode.Reliable)]
public record EntityHealthChangePacket(
    uint Id,
    float Health,
    float MaxHealth) : IServerPacket
{
    public EntityHealthChangePacket(Message packet) : this(
        packet.GetUInt(),
        packet.GetFloat(),
        packet.GetFloat())
    { }

    public void Write(Message packet) => packet
        .AddUInt(Id)
        .AddFloat(Health)
        .AddFloat(MaxHealth);
}