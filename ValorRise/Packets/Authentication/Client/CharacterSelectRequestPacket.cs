using MongoDB.Bson;
using Riptide;

namespace ValorRise.Packets.Authentication.Client;

[Packet(PacketType.CharacterSelectRequest, MessageSendMode.Reliable)]
public record CharacterSelectRequestPacket(ObjectId CharacterId) : IClientPacket
{
    public CharacterSelectRequestPacket(Message packet) : this(packet.GetObjectId())
    {

    }

    public void Write(Message packet) => packet
        .AddObjectId(CharacterId);
}
