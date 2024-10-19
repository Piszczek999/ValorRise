using System.Reflection;
using Riptide;
using ValorRise;
using ValorRise.Packets;

namespace ValorRiseGameServer;

public interface IServerPacketProcessor
{
    /// <summary>
    /// Registers a handler for a specific packet type.
    /// </summary>
    void Register(ushort packetId, Type handler);

    /// <summary>
    /// Processes an incoming packet by identifying its type and handling it accordingly.
    /// </summary>
    void Process(ushort packetId, Message packet);
}

public class ServerPacketProcessor : IServerPacketProcessor
{
    private IServerPacketListenerManager _listenerManager;
    private Dictionary<ushort, Func<Message, IServerPacket>> _packetConstructors = new();

    public ServerPacketProcessor(IServerPacketListenerManager listenerManager)
    {
        _listenerManager = listenerManager;

        // Get all loaded assemblies in the current application domain
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        // Retrieve all types that implement IPacket from all loaded assemblies
        var packetTypes = assemblies.SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(IServerPacket).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface);

        foreach (var packetType in packetTypes)
        {
            var packetAttribute = packetType.GetCustomAttribute<PacketAttribute>();
            if (packetAttribute != null)
            {
                Register(packetAttribute.PacketId, packetType);
            }
        }
    }

    public void Process(ushort packetId, Message buffer)
    {
        if (!_packetConstructors.TryGetValue(packetId, out var constructor))
        {
            Logger.Warning($"No Packet registered for packetId: {packetId}");
            return;
        }

        try
        {
            var packet = constructor(buffer);
            _listenerManager.ProcessPacket(packet);
        }
        catch (Exception ex)
        {
            Logger.Error($"Error processing packetId {packetId}: {ex.Message}");
        }
    }

    public void Register(ushort packetId, Type packetType)
    {
        var constructor = packetType.GetConstructor(new[] { typeof(Message) });
        if (constructor == null)
            throw new InvalidOperationException($"No valid constructor found for {packetType.Name}.");

        IServerPacket constructorDelegate(Message message) => (IServerPacket)constructor.Invoke(new object[] { message });

        if (!_packetConstructors.TryAdd(packetId, constructorDelegate))
        {
            throw new InvalidOperationException($"Handler for packetId {packetId} is already registered.");
        }
    }
}
