using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Authentication.Gateway;

[Packet(PacketType.NewCharacterAuthRequest, MessageSendMode.Reliable)]
public record NewCharacterAuthRequestPacket(ushort ClientId, ObjectId UserId, string Name, byte Slot) : IClientPacket
{
    public NewCharacterAuthRequestPacket(Message buffer) : this(
        buffer.GetUShort(),
        buffer.GetObjectId(),
        buffer.GetString(),
        buffer.GetByte())
    { }

    public void Write(Message buffer) => buffer
        .AddUShort(ClientId)
        .AddObjectId(UserId)
        .AddString(Name)
        .AddByte(Slot);
}
