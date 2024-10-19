using System.Reflection;
using ValorRise;
using ValorRise.Packets;
using ValorRiseGameServer;

namespace ValorRiseGameServer;

public interface IServerPacketListenerManager
{
    /// <summary>
    /// Registers a listener for a specific ClientPacket type.
    /// </summary>
    void RegisterListener(Type packetType, Action<IServerPacket> listener);

    /// <summary>
    /// Processes a ClientPacket by invoking all registered listeners for its type.
    /// </summary>
    void ProcessPacket(IServerPacket clientPacket);
}

public class ServerPacketListenerManager : IServerPacketListenerManager
{
    private readonly Dictionary<Type, Action<IServerPacket>> _listeners = new();

    public ServerPacketListenerManager()
    {
        var listenerTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.GetMethods().Any(m => m.GetCustomAttribute<ServerPacketListenerAttribute>() != null && !type.IsInterface));

        foreach (var type in listenerTypes)
        {
            var instance = Activator.CreateInstance(type);
            var methods = type.GetMethods().Where(m => m.GetCustomAttribute<ServerPacketListenerAttribute>() != null);
            foreach (var method in methods)
            {
                var parameters = method.GetParameters();
                if (parameters.Length > 1 || !typeof(IServerPacket).IsAssignableFrom(parameters[0].ParameterType))
                {
                    throw new Exception($"method {method.Name} has to have only one param of type IServerPacket.");
                }

                var packetType = parameters[0].ParameterType;

                Action<IServerPacket> action = (packet) =>
                {
                    method.Invoke(instance, new object[] { packet });
                };

                RegisterListener(packetType, action);
            }
        }
    }

    public void RegisterListener(Type packetType, Action<IServerPacket> listener)
    {
        _listeners[packetType] = listener;
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
                Logger.Error($"ServerListener exception", ex);
            }
        }
        else
        {
            Logger.Warning($"No ServerListeners registered for message type: {packetType.Name}");
        }
    }
}