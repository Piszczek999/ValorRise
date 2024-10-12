using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Authentication.Gateway;

[Packet(PacketType.NewCharacterAuthRequest, MessageSendMode.Reliable)]
public record NewCharacterAuthRequestPacket(ushort ClientId, ObjectId UserId, string Name) : IClientPacket
{
    public NewCharacterAuthRequestPacket(Message packet) : this(packet.GetUShort(), packet.GetObjectId(), packet.GetString())
    {

    }

    public void Write(Message packet) => packet
        .AddUShort(ClientId)
        .AddObjectId(UserId)
        .AddString(Name);
}
