using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Loading.Client;

[Packet(PacketType.GameServerInfoRequest, MessageSendMode.Reliable)]
public record GameServerInfoRequestPacket(string IpAddress) : IClientPacket
{
    public GameServerInfoRequestPacket(Message packet) : this(
        packet.GetString())
    { }

    public void Write(Message packet) => packet
        .AddString(IpAddress);
}