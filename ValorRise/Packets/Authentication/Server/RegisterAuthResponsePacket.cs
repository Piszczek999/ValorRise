using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Authentication.Server;

[Packet(PacketType.RegisterAuthResponse, MessageSendMode.Reliable)]
public record RegisterAuthResponsePacket(ushort ClientId, RegisterResult Result) : IServerPacket
{
    public RegisterAuthResponsePacket(Message packet) : this(packet.GetUShort(), (RegisterResult)packet.GetByte())
    { }

    public void Write(Message packet) => packet
        .AddUShort(ClientId)
        .AddByte((byte)Result);
}
