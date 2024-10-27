using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Client;

[Packet(PacketType.FetchServerTime, MessageSendMode.Reliable)]
public record FetchServerTimePacket() : IClientPacket
{
    public FetchServerTimePacket(Message packet) : this()
    { }

    public void Write(Message packet)
    { }
}