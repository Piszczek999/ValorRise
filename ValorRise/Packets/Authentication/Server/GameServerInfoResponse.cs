using Riptide;

namespace ValorRise.Packets.Authentication.Server;

[Packet(PacketType.GameServerInfoResponse, MessageSendMode.Reliable)]
public record GameServerInfoResponse(ushort MapId, ushort Port) : IServerPacket
{
    public GameServerInfoResponse(Message packet) : this(packet.GetUShort(), packet.GetUShort())
    {

    }

    public void Write(Message packet) => packet
        .AddUShort(MapId)
        .AddUShort(Port);
}
