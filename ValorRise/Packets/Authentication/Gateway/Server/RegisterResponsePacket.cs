using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Authentication.Gateway;

[Packet(PacketType.RegisterResponse, MessageSendMode.Reliable)]
public record RegisterResponsePacket(RegisterResult Result) : IServerPacket
{
    public RegisterResponsePacket(Message packet) : this(
        (RegisterResult)packet.GetByte())
    { }

    public void Write(Message packet) => packet
        .AddByte((byte)Result);
}
