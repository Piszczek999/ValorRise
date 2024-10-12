using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Authentication.Gateway;

[Packet(PacketType.LoginResponse, MessageSendMode.Reliable)]
public record LoginResponsePacket(LoginResult Result) : IServerPacket
{
    public LoginResponsePacket(Message packet) : this((LoginResult)packet.GetByte())
    {

    }

    public void Write(Message packet) => packet
        .AddByte((byte)Result);
}
