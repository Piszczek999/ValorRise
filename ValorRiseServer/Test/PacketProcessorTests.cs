using Moq;
using ValorRiseServer;
using Xunit;

namespace ValorRise.Test;

public class PacketProcessorTests
{
    private readonly PacketProcessor _packetProcessor;
    private readonly Mock<IPacketListenerManager> _mockListenerManager;

    public PacketProcessorTests()
    {
        _mockListenerManager = new Mock<IPacketListenerManager>();
        _packetProcessor = new PacketProcessor(_mockListenerManager.Object);
    }

    [Fact]
    public void Register_ValidPacketType_ShouldRegisterSuccessfully()
    {
        // Arrange
        var packetId = (ushort)PacketType.CharacterSelectRequest;

        // Act

        // Assert
        Assert.True(_packetProcessor._packetConstructors.ContainsKey(packetId)); // Example way to check if it is registered
    }
}
