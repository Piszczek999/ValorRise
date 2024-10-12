using Riptide;

namespace ValorRise.Packets.Authentication.Gateway;

[Packet(PacketType.CharacterSelectAuthResponse, MessageSendMode.Reliable)]
public record CharacterSelectAuthResponsePacket(ushort ClientId, string Token, string IpAddress, ushort Port) : IServerPacket
{
    public CharacterSelectAuthResponsePacket(Message packet) : this(packet.GetUShort(), packet.GetString(), packet.GetString(), packet.GetUShort())
    {

    }

    public void Write(Message packet) => packet
        .AddUShort(ClientId)
        .AddString(Token)
        .AddString(IpAddress)
        .AddUShort(Port);
}
