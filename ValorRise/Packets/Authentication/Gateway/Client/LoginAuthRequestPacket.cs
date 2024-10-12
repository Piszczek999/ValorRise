using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Authentication.Gateway;

[Packet(PacketType.LoginAuthRequest, MessageSendMode.Reliable)]
public record LoginAuthRequestPacket(ushort ClientId, string Username, string Password) : IClientPacket
{
    public LoginAuthRequestPacket(Message buffer) : this(buffer.GetUShort(), buffer.GetString(), buffer.GetString())
    {

    }

    public void Write(Message buffer) => buffer
        .AddUShort(ClientId)
        .AddString(Username)
        .AddString(Password);
}
