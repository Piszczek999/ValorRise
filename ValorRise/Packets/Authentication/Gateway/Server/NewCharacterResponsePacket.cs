using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Authentication.Gateway;

[Packet(PacketType.NewCharacterResponse, MessageSendMode.Reliable)]
public record NewCharacterResponsePacket(NewCharacterResult NewCharacterResult) : IServerPacket
{
    public NewCharacterResponsePacket(Message packet) : this((NewCharacterResult)packet.GetByte())
    {

    }

    public void Write(Message packet) => packet
        .AddByte((byte)NewCharacterResult);
}
