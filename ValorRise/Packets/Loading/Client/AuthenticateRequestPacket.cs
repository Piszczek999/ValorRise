using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Loading.Client;

[Packet(PacketType.PlayerAuthenticate, MessageSendMode.Reliable)]
public record AuthenticateRequestPacket(string Token) : IClientPacket
{
    public AuthenticateRequestPacket(Message packet) : this(
        packet.GetString())
    { }

    public void Write(Message packet) => packet
        .AddString(Token);
}