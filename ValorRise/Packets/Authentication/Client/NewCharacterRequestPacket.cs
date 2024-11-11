using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Authentication.Client;

[Packet(PacketType.NewCharacterRequest, MessageSendMode.Reliable)]
public record NewCharacterRequestPacket(string Name, Class Class) : IClientPacket
{
    public NewCharacterRequestPacket(Message packet) : this(packet.GetString(), (Class)packet.GetByte())
    {

    }

    public void Write(Message packet) => packet
        .AddString(Name)
        .AddByte((byte)Class);
}
