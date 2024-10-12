using Riptide;

namespace ValorRise.Packets.Authentication.Client;

[Packet(PacketType.RegisterRequest, MessageSendMode.Reliable)]
public record RegisterRequestPacket(string Username, string Password) : IClientPacket
{
    public RegisterRequestPacket(Message packet) : this(packet.GetString(), packet.GetString())
    {

    }

    public void Write(Message packet) => packet
        .AddString(Username)
        .AddString(Password);
}
