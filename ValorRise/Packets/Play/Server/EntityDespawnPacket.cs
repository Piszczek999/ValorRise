using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.EntityDespawn, MessageSendMode.Reliable)]
public record EntityDespawnPacket(ObjectId Id) : IServerPacket
{
    public EntityDespawnPacket(Message packet) : this(packet.GetObjectId()) { }

    public void Write(Message packet) => packet
        .AddObjectId(Id);
}