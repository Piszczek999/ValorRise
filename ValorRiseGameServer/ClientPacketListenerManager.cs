using System.Reflection;
using ValorRise;
using ValorRise.Packets;

namespace ValorRiseGameServer;

public interface IClientPacketListenerManager
{
    /// <summary>
    /// Registers a listener for a specific ClientPacket type.
    /// </summary>
    void RegisterListener(Type packetType, Action<IPacket, PlayerConnection> listener);

    /// <summary>
    /// Processes a ClientPacket by invoking all registered listeners for its type.
    /// </summary>
    void ProcessPacket(IPacket clientPacket, PlayerConnection connection);
}

public class ClientPacketListenerManager : IClientPacketListenerManager
{
    private readonly Dictionary<Type, Action<IPacket, PlayerConnection>> _listeners = new();

    public ClientPacketListenerManager()
    {
        var listenerTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.GetMethods().Any(m => m.GetCustomAttribute<ClientPacketListenerAttribute>() != null && !type.IsInterface));

        foreach (var type in listenerTypes)
        {
            var instance = Activator.CreateInstance(type);
            var methods = type.GetMethods().Where(m => m.GetCustomAttribute<ClientPacketListenerAttribute>() != null);
            foreach (var method in methods)
            {
                var parameters = method.GetParameters();
                if (parameters.Length > 2 || !typeof(IPacket).IsAssignableFrom(parameters[0].ParameterType))
                {
                    throw new Exception($"method {method.Name} has to have only one param of type IPacket.");
                }

                var packetType = parameters[0].ParameterType;

                Action<IPacket, PlayerConnection> action = (packet, connection) =>
                {
                    method.Invoke(instance, new object[] { packet, connection });
                };

                RegisterListener(packetType, action);
            }
        }
    }

    public void RegisterListener(Type packetType, Action<IPacket, PlayerConnection> listener)
    {
        _listeners[packetType] = listener;
    }

    public void ProcessPacket(IPacket packet, PlayerConnection connection)
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
                Logger.Error($"ClientListener exception", ex);
            }
        }
        else
        {
            Logger.Warning($"No ClientListeners registered for message type: {packetType.Name}");
        }
    }
}