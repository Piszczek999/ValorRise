using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Authentication.Server;

[Packet(PacketType.NewCharacterAuthResponse, MessageSendMode.Reliable)]
public record NewCharacterAuthResponsePacket(ushort ClientId, NewCharacterResult Result) : IServerPacket
{
    public NewCharacterAuthResponsePacket(Message packet) : this(packet.GetUShort(), (NewCharacterResult)packet.GetByte())
    { }

    public void Write(Message packet) => packet
        .AddUShort(ClientId)
        .AddByte((byte)Result);
}
