using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.EntityDespawn, MessageSendMode.Reliable)]
public record EntityDespawnPacket(uint Id) : IServerPacket
{
    public EntityDespawnPacket(Message packet) : this(packet.GetUInt()) { }

    public void Write(Message packet) => packet
        .AddUInt(Id);
}