using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.EntityHealth, MessageSendMode.Reliable)]
public record EntityHealthPacket(
    ObjectId Id,
    float Health,
    float MaxHealth) : IServerPacket
{
    public EntityHealthPacket(Message packet) : this(
        packet.GetObjectId(),
        packet.GetFloat(),
        packet.GetFloat())
    { }

    public void Write(Message packet) => packet
        .AddObjectId(Id)
        .AddFloat(Health)
        .AddFloat(MaxHealth);
}