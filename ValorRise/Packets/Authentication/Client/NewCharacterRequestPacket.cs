using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Authentication.Client;

[Packet(PacketType.NewCharacterRequest, MessageSendMode.Reliable)]
public record NewCharacterRequestPacket(string Name, byte Slot) : IClientPacket
{
    public NewCharacterRequestPacket(Message buffer) : this(buffer.GetString(), buffer.GetByte())
    { }

    public void Write(Message packet) => packet
        .AddString(Name)
        .AddByte(Slot);
}
