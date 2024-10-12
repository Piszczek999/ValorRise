using Riptide;

namespace ValorRise;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class PacketAttribute : Attribute
{
    public ushort PacketId { get; set; }
    public MessageSendMode SendMode { get; set; }

    public PacketAttribute(PacketType packetType, MessageSendMode sendMode)
    {
        PacketId = (ushort)packetType;
        SendMode = sendMode;
    }
}
