using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.SpawnEntity, MessageSendMode.Reliable)]
public record DespawnEntityPacket(ObjectId Id) : IServerPacket
{
    public DespawnEntityPacket(Message packet) : this(packet.GetObjectId()) { }

    public void Write(Message packet) => packet
        .AddObjectId(Id);
}