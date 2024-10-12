using Riptide;

namespace ValorRise.Packets.Authentication.Gateway;

[Packet(PacketType.RegisterAuthRequest, MessageSendMode.Reliable)]
public record RegisterAuthRequestPacket(ushort ClientId, string Username, string Password) : IClientPacket
{
    public RegisterAuthRequestPacket(Message packet) : this(packet.GetUShort(), packet.GetString(), packet.GetString())
    {

    }

    public void Write(Message packet) => packet
        .AddString(Username)
        .AddString(Password);
}
