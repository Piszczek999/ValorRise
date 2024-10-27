using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.FetchServerTimeResponse, MessageSendMode.Reliable)]
public record FetchServerTimeResponsePacket(long ServerTimestamp) : IServerPacket
{
    public FetchServerTimeResponsePacket(Message packet) : this(
        ServerTimestamp: packet.GetLong())
    { }

    public void Write(Message packet) => packet
        .AddLong(ServerTimestamp);
}