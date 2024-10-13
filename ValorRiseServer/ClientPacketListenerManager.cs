using System.Reflection;
using Riptide;
using ValorRise;
using ValorRise.Packets;

namespace ValorRiseServer;

public class ClientPacketListenerManager : IClientPacketListenerManager
{
    private readonly Dictionary<Type, Action<IClientPacket, ClientConnection>> _listeners = new();

    public void RegisterListener<T>(Action<T, ClientConnection> listener) where T : IClientPacket
    {
        var type = typeof(T);
        Action<IClientPacket, ClientConnection> action = (packet, conn) =>
        {
            if (packet is IClientPacket clientPacket)
                listener((T)clientPacket, conn);
        };
        _listeners[type] = action;
    }

    public void ProcessPacket(IClientPacket packet, ClientConnection connection)
    {
        var packetType = packet.GetType();

        if (_listeners.TryGetValue(packetType, out var listener))
        {
            try
            {
                listener(packet, connection);
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