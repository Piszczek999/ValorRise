using System.Reflection;
using Riptide;
using ValorRise;
using ValorRise.Packets;

namespace ValorRiseServer;


public class PacketProcessor : IPacketProcessor
{
    private IPacketListenerManager _listenerManager;
    private Dictionary<ushort, Func<Message, IPacket>> _packetConstructors = new();

    public PacketProcessor(IPacketListenerManager listenerManager)
    {
        _listenerManager = listenerManager;

        // Get all loaded assemblies in the current application domain
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        // Retrieve all types that implement IPacket from all loaded assemblies
        var packetTypes = assemblies.SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(IPacket).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface);

        foreach (var packetType in packetTypes)
        {
            var packetAttribute = packetType.GetCustomAttribute<PacketAttribute>();
            if (packetAttribute != null)
            {
                Register(packetAttribute.PacketId, packetType);
            }
        }
    }

    public void Process(PlayerConnection connection, ushort packetId, Message buffer)
    {
        if (!_packetConstructors.TryGetValue(packetId, out var constructor))
        {
            Logger.Warning($"No Packet registered for packetId: {packetId}");
            return;
        }

        try
        {
            var packet = constructor(buffer);
            if (connection.Player != null)
            {
                var playerPacketEvent = new PlayerPacketEvent(connection.Player, (IClientPacket)packet);
                ValorServer.GlobalEventNode.Invoke(playerPacketEvent);
            }
            _listenerManager.ProcessPacket(packet, connection);
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

        IPacket constructorDelegate(Message message) => (IPacket)constructor.Invoke(new object[] { message });

        if (!_packetConstructors.TryAdd(packetId, constructorDelegate))
        {
            throw new InvalidOperationException($"Handler for packetId {packetId} is already registered.");
        }
    }
}
