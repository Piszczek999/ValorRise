using ValorRise.Packets;

namespace ValorRiseServer;

public interface IClientPacketListenerManager
{
    /// <summary>
    /// Registers a listener for a specific ClientPacket type.
    /// </summary>
    /// <typeparam name="T">The type of ClientPacket to listen for.</typeparam>
    /// <param name="listener">The callback to invoke when a message of type T is processed.</param>
    void RegisterListener(Type packetType, Action<IPacket, PlayerConnection> listener);

    /// <summary>
    /// Processes a ClientPacket by invoking all registered listeners for its type.
    /// </summary>
    /// <param name="clientPacket">The packet to process.</param>
    /// <param name="connection">The player connection associated with the packet.</param>
    void ProcessPacket(IPacket clientPacket, PlayerConnection connection);
}