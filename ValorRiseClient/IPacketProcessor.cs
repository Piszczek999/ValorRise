using Riptide;

namespace ValorRiseClient;

public interface IPacketProcessor
{
    /// <summary>
    /// Registers a handler for a specific packet type.
    /// </summary>
    /// <param name="packetId">The identifier of the packet type.</param>
    /// <param name="handler">The handler that will process the packet.</param>
    void Register(ushort packetId, Type handler);

    /// <summary>
    /// Processes an incoming packet by identifying its type and handling it accordingly.
    /// </summary>
    /// <param name="connection">The connection from which the packet was received.</param>
    /// <param name="packetId">The identifier of the packet type.</param>
    /// <param name="packet">The packet data to be processed.</param>
    void Process(ushort packetId, Message packet);
}
