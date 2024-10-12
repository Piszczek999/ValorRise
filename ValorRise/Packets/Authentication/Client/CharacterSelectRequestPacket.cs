using Riptide;

namespace ValorRise.Packets.Authentication.Client;

[Packet(PacketType.CharacterSelectRequest, MessageSendMode.Reliable)]
public record CharacterSelectRequestPacket(string Name) : IClientPacket
{
    public CharacterSelectRequestPacket(Message packet) : this(packet.GetString())
    {

    }

    public void Write(Message packet) => packet
        .AddString(Name);
}
