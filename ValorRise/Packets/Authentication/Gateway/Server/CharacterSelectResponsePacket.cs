using Riptide;

namespace ValorRise.Packets.Authentication.Gateway;

[Packet(PacketType.CharacterSelectResponse, MessageSendMode.Reliable)]
public record CharacterSelectResponsePacket(string Token, string IpAddress, ushort Port) : IServerPacket
{
    public CharacterSelectResponsePacket(Message packet) : this(packet.GetString(), packet.GetString(), packet.GetUShort())
    {

    }

    public void Write(Message packet) => packet
        .AddString(Token)
        .AddString(IpAddress)
        .AddUShort(Port);
}
