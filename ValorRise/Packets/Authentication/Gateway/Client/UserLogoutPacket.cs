using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Authentication.GameServer;

[Packet(PacketType.UserLogout, MessageSendMode.Reliable)]
public record UserLogoutPacket(ObjectId UserId) : IClientPacket
{
    public UserLogoutPacket(Message buffer) : this(buffer.GetObjectId())
    { }

    public void Write(Message buffer) => buffer
        .AddObjectId(UserId);
}
