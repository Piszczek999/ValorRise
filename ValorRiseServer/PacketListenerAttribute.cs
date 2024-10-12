namespace ValorRiseServer;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
internal class PacketListenerAttribute : Attribute
{
    public Type PacketType { get; }

    public PacketListenerAttribute(Type packetType)
    {
        PacketType = packetType;
    }
}