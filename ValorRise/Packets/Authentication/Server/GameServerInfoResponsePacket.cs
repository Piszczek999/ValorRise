using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Authentication.Server;

[Packet(PacketType.GameServerInfoResponse, MessageSendMode.Reliable)]
public record GameServerInfoResponsePacket(byte MapId, ushort Port) : IServerPacket
{
    public GameServerInfoResponsePacket(Message packet) : this(packet.GetByte(), packet.GetUShort())
    { }

    public void Write(Message packet) => packet
        .AddByte(MapId)
        .AddUShort(Port);
}
