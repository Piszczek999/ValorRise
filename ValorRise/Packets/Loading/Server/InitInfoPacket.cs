using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Loading.Server;

[Packet(PacketType.InitInfo, MessageSendMode.Reliable)]
public record InitInfoPacket(uint PlayerId) : IServerPacket
{
    public InitInfoPacket(Message packet) : this(
        packet.GetUInt())
    { }

    public void Write(Message packet) => packet
        .AddUInt(PlayerId);
}