using Riptide;

namespace ValorRise.Packets.Authentication.Server;

[Packet(PacketType.GameServerInfoResponse, MessageSendMode.Reliable)]
public record GameServerInfoResponsePacket(ushort MapId, ushort Port) : IServerPacket
{
    public GameServerInfoResponsePacket(Message packet) : this(packet.GetUShort(), packet.GetUShort())
    {

    }

    public void Write(Message packet) => packet
        .AddUShort(MapId)
        .AddUShort(Port);
}
