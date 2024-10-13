using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Authentication.Client;

[Packet(PacketType.NewCharacterRequest, MessageSendMode.Reliable)]
public record NewCharacterRequestPacket(string Name) : IClientPacket
{
    public NewCharacterRequestPacket(Message packet) : this(packet.GetString())
    {

    }

    public void Write(Message packet) => packet
        .AddString(Name);
}
