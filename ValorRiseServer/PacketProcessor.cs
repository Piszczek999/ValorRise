using System.Reflection;
using Riptide;
using ValorRise;
using ValorRise.Packets;

namespace ValorRiseServer;


public class PacketProcessor : IPacketProcessor
{
    private IPacketListenerManager _listenerManager;
    public Dictionary<ushort, Func<Message, IClientPacket>> _packetConstructors = new();

    public PacketProcessor(IPacketListenerManager listenerManager)
    {
        _listenerManager = listenerManager;

        // Get all loaded assemblies in the current application domain
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        // Retrieve all types that implement IClientPacket from all loaded assemblies
        var packetTypes = assemblies.SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(IClientPacket).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface);

        foreach (var packetType in packetTypes)
        {
            var packetAttribute = packetType.GetCustomAttribute<PacketAttribute>();
            if (packetAttribute != null)
            {
                Register(packetAttribute.PacketId, packetType);
            }
        }
    }

    public void Process(PlayerConnection connection, ushort packetId, Message packet)
    {
        if (!_packetConstructors.TryGetValue(packetId, out var constructor))
        {
            Logger.Warning($"No Packet registered for packetId: {packetId}");
            return;
        }

        try
        {
            var clientPacket = constructor(packet);
            var playerPacketEvent = new PlayerPacketEvent(connection.Player, clientPacket);
            ValorServer.GlobalEventNode.Invoke(playerPacketEvent);
            _listenerManager.ProcessClientMessage(clientPacket, connection);
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

        IClientPacket constructorDelegate(Message message) => (IClientPacket)constructor.Invoke(new object[] { message });

        if (!_packetConstructors.TryAdd(packetId, constructorDelegate))
        {
            throw new InvalidOperationException($"Handler for packetId {packetId} is already registered.");
        }
    }
}
