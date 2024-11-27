using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Authentication.Gateway;

[Packet(PacketType.CharacterSelectAuthRequest, MessageSendMode.Reliable)]
public record CharacterSelectAuthRequestPacket(ushort ClientId, ObjectId UserId, ObjectId CharacterId) : IClientPacket
{
    public CharacterSelectAuthRequestPacket(Message buffer) : this(
        buffer.GetUShort(),
        buffer.GetObjectId(),
        buffer.GetObjectId())
    { }

    public void Write(Message buffer) => buffer
        .AddUShort(ClientId)
        .AddObjectId(UserId)
        .AddObjectId(CharacterId);
}
