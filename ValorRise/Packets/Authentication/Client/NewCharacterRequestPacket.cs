using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Authentication.Client;

[Packet(PacketType.NewCharacterRequest, MessageSendMode.Reliable)]
public record NewCharacterRequestPacket(string Name, Class Class, byte Slot) : IClientPacket
{
    public NewCharacterRequestPacket(Message buffer) : this(buffer.GetString(), (Class)buffer.GetByte(), buffer.GetByte())
    { }

    public void Write(Message packet) => packet
        .AddString(Name)
        .AddByte((byte)Class)
        .AddByte(Slot);
}
