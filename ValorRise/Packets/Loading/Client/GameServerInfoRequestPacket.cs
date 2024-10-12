using System.Numerics;
using MongoDB.Bson;
using Riptide;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.GameServerInfoRequest, MessageSendMode.Reliable)]
public record GameServerInfoRequestPacket(string IpAddress) : IClientPacket
{
    public GameServerInfoRequestPacket(Message packet) : this(
        packet.GetString())
    { }

    public void Write(Message packet) => packet
        .AddString(IpAddress);
}