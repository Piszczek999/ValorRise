using System.Numerics;
using ValorRiseServer;
using Xunit;

public class Vector2ExtensionsTests
{
    [Fact]
    public void Normalize_ShouldReturnUnitVector_WhenNonZeroVector()
    {
        // Arrange
        var vector = new Vector2(3, 4); // Magnitude = 5

        // Act
        var normalized = vector.Normalize();

        // Assert
        Assert.Equal(1, normalized.Length(), 5); // Unit length
        Assert.Equal(0.6f, normalized.X, 5); // X component after normalization
        Assert.Equal(0.8f, normalized.Y, 5); // Y component after normalization
    }

    [Fact]
    public void Normalize_ShouldReturnSameVector_WhenZeroVector()
    {
        // Arrange
        var vector = new Vector2(0, 0);

        // Act
        var normalized = vector.Normalize();

        // Assert
        Assert.Equal(vector, normalized); // Zero vector should remain unchanged
    }

    [Fact]
    public void Normalize_ShouldHandleNegativeValuesCorrectly()
    {
        // Arrange
        var vector = new Vector2(-3, -4); // Magnitude = 5

        // Act
        var normalized = vector.Normalize();

        // Assert
        Assert.Equal(1, normalized.Length(), 5); // Unit length
        Assert.Equal(-0.6f, normalized.X, 5); // X component after normalization
        Assert.Equal(-0.8f, normalized.Y, 5); // Y component after normalization
    }

    [Fact]
    public void Normalize_ShouldHandleSmallValuesCorrectly()
    {
        // Arrange
        var vector = new Vector2(0.0001f, 0.0001f);

        // Act
        var normalized = vector.Normalize();

        // Assert
        Assert.Equal(1, normalized.Length(), 5); // Unit length
    }
}
