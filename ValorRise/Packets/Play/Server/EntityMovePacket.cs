using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.EntityMove, MessageSendMode.Unreliable)]
public record EntityMovePacket(ObjectId Id, Vector2 NewPosition) : IServerPacket
{
    public EntityMovePacket(Message packet) : this(
        packet.GetObjectId(),
        packet.GetVector2())
    { }

    public void Write(Message packet) => packet
        .AddObjectId(Id)
        .AddVector2(NewPosition);
}