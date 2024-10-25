using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Client;

[Packet(PacketType.FetchServerTime, MessageSendMode.Reliable)]
public record FetchServerTimePacket(long ClientTimestamp) : IClientPacket
{
    public FetchServerTimePacket(Message packet) : this(
        ClientTimestamp: packet.GetLong())
    { }

    public void Write(Message packet) => packet
        .AddLong(ClientTimestamp);
}