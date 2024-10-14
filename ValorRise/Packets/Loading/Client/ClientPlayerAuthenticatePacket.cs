using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Loading.Client;

[Packet(PacketType.PlayerAuthenticate, MessageSendMode.Reliable)]
public record ClientPlayerAuthenticatePacket(string Token) : IClientPacket
{
    public ClientPlayerAuthenticatePacket(Message packet) : this(
        packet.GetString())
    { }

    public void Write(Message packet) => packet
        .AddString(Token);
}