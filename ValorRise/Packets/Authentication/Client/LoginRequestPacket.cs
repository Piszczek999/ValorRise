using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Authentication.Client;

[Packet(PacketType.LoginRequest, MessageSendMode.Reliable)]
public record LoginRequestPacket(string Username, string Password) : IClientPacket
{
    public LoginRequestPacket(Message packet) : this(packet.GetString(), packet.GetString())
    {

    }

    public void Write(Message packet) => packet
        .AddString(Username)
        .AddString(Password);
}
