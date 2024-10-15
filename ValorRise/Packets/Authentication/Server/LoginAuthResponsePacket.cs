using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Authentication.Server;

[Packet(PacketType.LoginAuthResponse, MessageSendMode.Reliable)]
public record LoginAuthResponsePacket(ushort ClientId, LoginResult Result, ObjectId UserId = default) : IServerPacket
{
    public LoginAuthResponsePacket(Message packet) : this(packet.GetUShort(), (LoginResult)packet.GetByte(), packet.GetObjectId())
    { }

    public void Write(Message packet) => packet
        .AddUShort(ClientId)
        .AddByte((byte)Result)
        .AddObjectId(UserId);
}
