using System.Numerics;
using MongoDB.Bson;
using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.WorldStatsState, MessageSendMode.Reliable)]
public record WorldStatsStatePacket(EntityStatsState[] EntityStates) : IServerPacket
{
    public WorldStatsStatePacket(Message buffer) : this(
        buffer.GetSerializables<EntityStatsState>())
    { }

    public void Write(Message buffer) => buffer
        .AddSerializables(EntityStates);
}

public class EntityStatsState : IMessageSerializable
{
    public uint Id { get; set; }
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public float AttackSpeed { get; set; }

    public void Deserialize(Message message)
    {
        Id = message.GetUInt();
        Health = message.GetShort();
        MaxHealth = message.GetShort();
        AttackSpeed = message.GetShort() / 100f;
    }

    public void Serialize(Message message) => message
        .AddUInt(Id)
        .AddShort((short)(Health + 0.5f))
        .AddShort((short)(MaxHealth + 0.5f))
        .AddShort((short)(AttackSpeed * 100));

    public const int ByteSize = 10;
}