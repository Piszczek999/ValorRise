using System.Reflection;
using ValorRise;
using ValorRise.Packets;

namespace ValorRiseClient;

public class PacketListenerManager : IPacketListenerManager
{
    private readonly Dictionary<Type, Action<IPacket>> _listeners = new();

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
                var parameters = method.GetParameters();
                if (parameters.Length > 1 || !typeof(IPacket).IsAssignableFrom(parameters[0].ParameterType))
                {
                    throw new Exception($"method {method.Name} has to have only one param of type IPacket.");
                }

                var packetType = parameters[0].ParameterType;
                Action<IPacket> action = (packet) =>
                {
                    method.Invoke(instance, new object[] { packet });
                };

                RegisterListener(packetType, action);
            }
        }
    }

    public void RegisterListener(Type packetType, Action<IPacket> listener)
    {
        _listeners[packetType] = listener;
    }

    public void ProcessPacket(IPacket packet)
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