using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Loading.Server;

[Packet(PacketType.MapInfo, MessageSendMode.Reliable)]
public record MapInfoPacket(ushort MapId) : IServerPacket
{
    public MapInfoPacket(Message packet) : this(packet.GetUShort())
    { }

    public void Write(Message packet) => packet
        .AddUShort(MapId);
}