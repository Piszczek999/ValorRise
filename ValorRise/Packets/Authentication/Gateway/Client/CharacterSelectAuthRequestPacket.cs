using MongoDB.Bson;
using Riptide;

namespace ValorRise.Packets.Authentication.Gateway;

[Packet(PacketType.CharacterSelectAuthRequest, MessageSendMode.Reliable)]
public record CharacterSelectAuthRequestPacket(ushort ClientId, ObjectId UserId, ObjectId CharacterId) : IClientPacket
{
    public CharacterSelectAuthRequestPacket(Message packet) : this(packet.GetUShort(), packet.GetObjectId(), packet.GetObjectId())
    {

    }

    public void Write(Message packet) => packet
        .AddUShort(ClientId)
        .AddObjectId(UserId)
        .AddObjectId(CharacterId);
}
