using System.Reflection;
using ValorRise;
using ValorRise.Packets;

namespace ValorRiseClient;

public class ServerPacketListenerManager : IServerPacketListenerManager
{
    private readonly Dictionary<Type, Action<IServerPacket>> _listeners = new();

    public void RegisterListener<T>(Action<T> listener) where T : IServerPacket
    {
        var type = typeof(T);
        Action<IServerPacket> action = (packet) =>
        {
            if (packet is IServerPacket clientPacket)
                listener((T)clientPacket);
        };
        _listeners[type] = action;
    }

    public void ProcessPacket(IServerPacket packet)
    {
        var packetType = packet.GetType();

        if (_listeners.TryGetValue(packetType, out var listener))
        {
            try
            {
                listener(packet);
            }
            catch (Exception ex)
            {
                Logger.Error("Listener exception", ex);
            }
        }
        else
        {
            Logger.Warning($"No listeners registered for message type: {packetType.Name}");
        }
    }
}