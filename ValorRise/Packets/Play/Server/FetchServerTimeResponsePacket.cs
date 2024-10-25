using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.FetchServerTimeResponse, MessageSendMode.Reliable)]
public record FetchServerTimeResponsePacket(long ServerTimestamp, long ClientTimestamp) : IServerPacket
{
    public FetchServerTimeResponsePacket(Message packet) : this(
        ServerTimestamp: packet.GetLong(),
        ClientTimestamp: packet.GetLong())
    { }

    public void Write(Message packet) => packet
        .AddLong(ServerTimestamp)
        .AddLong(ClientTimestamp);
}