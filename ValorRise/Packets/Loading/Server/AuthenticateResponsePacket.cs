using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Loading.Server;

[Packet(PacketType.VerifyTokenResponse, MessageSendMode.Reliable)]
public record AuthenticateResponsePacket(bool Result) : IServerPacket
{
    public AuthenticateResponsePacket(Message packet) : this(packet.GetBool())
    { }

    public void Write(Message packet) => packet
        .AddBool(Result);
}