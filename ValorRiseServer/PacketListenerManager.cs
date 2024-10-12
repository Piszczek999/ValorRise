using System.Reflection;
using ValorRise;
using ValorRise.Packets;
using ValorRise.Packets.Play.Client;

namespace ValorRiseServer;

public class PacketListenerManager : IPacketListenerManager
{
    private readonly Dictionary<Type, Action<IClientPacket, PlayerConnection>> _listeners = new();

    public PacketListenerManager()
    {
        var listenerTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.GetMethods().Any(m => m.GetCustomAttribute<PacketListenerAttribute>() != null && !type.IsInterface));

        foreach (var type in listenerTypes)
        {
            var instance = Activator.CreateInstance(type);
            var methods = type.GetMethods().Where(m => m.GetCustomAttribute<PacketListenerAttribute>() != null);
            foreach (var method in methods)
            {
                var attribute = method.GetCustomAttribute<PacketListenerAttribute>();
                var packetType = attribute.PacketType;

                Action<IClientPacket, PlayerConnection> action = (packet, connection) =>
                {
                    if (packet is not ClientPlayerMovementPacket specificPacket)
                    {
                        throw new InvalidCastException($"Invalid packet type: expected {packetType.Name}, got {packet.GetType().Name}");
                    }

                    method.Invoke(instance, new object[] { specificPacket, connection });
                };

                RegisterListener(packetType, action);
            }
        }
    }

    public void RegisterListener(Type packetType, Action<IClientPacket, PlayerConnection> listener)
    {
        _listeners[packetType] = listener;
    }

    public void ProcessClientMessage(IClientPacket clientPacket, PlayerConnection connection)
    {
        var packetType = clientPacket.GetType();

        if (_listeners.TryGetValue(packetType, out var listener))
        {
            try
            {
                listener(clientPacket, connection);
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