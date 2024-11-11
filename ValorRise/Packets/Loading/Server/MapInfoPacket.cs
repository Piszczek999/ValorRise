using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Loading.Server;

[Packet(PacketType.MapInfo, MessageSendMode.Reliable)]
public record MapInfoPacket(byte MapId) : IServerPacket
{
    public MapInfoPacket(Message packet) : this(packet.GetByte())
    { }

    public void Write(Message packet) => packet
        .AddByte(MapId);
}