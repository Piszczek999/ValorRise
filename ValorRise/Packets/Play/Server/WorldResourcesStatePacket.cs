using Riptide;
using ValorRise.Enums;

namespace ValorRise.Packets.Play.Server;

[Packet(PacketType.WorldHealthState, MessageSendMode.Reliable)]
public record WorldResourcesStatePacket(EntityResourcesState[] EntityStates) : IServerPacket
{
    public WorldResourcesStatePacket(Message buffer) : this(
        buffer.GetSerializables<EntityResourcesState>())
    { }

    public void Write(Message buffer) => buffer
        .AddSerializables(EntityStates);
}

public class EntityResourcesState : IMessageSerializable
{
    public uint Id { get; set; }
    public float Health { get; set; }
    public float? Mana { get; set; }

    public EntityResourcesState() { }
    public EntityResourcesState(uint id, float health, float? mana = null)
    {
        Id = id;
        Health = health;
        Mana = mana;
    }

    public void Deserialize(Message message)
    {
        Id = message.GetUInt();
        Health = message.GetShort();
        if (message.GetBool()) Mana = message.GetShort();
    }

    public void Serialize(Message message)
    {
        message
        .AddUInt(Id)
        .AddShort((short)(Health + 0.5f))
        .AddBool(Mana.HasValue);
        if (Mana.HasValue) message.AddShort((short)(Mana + 0.5f));
    }

    public const int ByteSize = 8;
}