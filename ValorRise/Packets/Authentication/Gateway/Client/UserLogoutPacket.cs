using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;
using ValorRise.Models;

namespace ValorRise.Packets.Authentication.GameServer;

[Packet(PacketType.UserLogout, MessageSendMode.Reliable)]
public record UserLogoutPacket(ObjectId UserId) : IClientPacket
{
    public UserLogoutPacket(Message packet) : this(packet.GetObjectId())
    {

    }

    public void Write(Message packet) => packet
        .AddObjectId(UserId);
}
