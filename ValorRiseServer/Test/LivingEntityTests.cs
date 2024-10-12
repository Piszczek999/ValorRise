using System.Numerics;
using ValorRise.Enums;
using ValorRiseServer.Entities;
using Xunit;

public class LivingEntityTests
{
    class TestEntity : LivingEntity
    {
        public TestEntity() : base(EntityType.Player) { }
    }

    [Fact]
    public void UpdatePosition_ShouldMoveTowardsDestination()
    {
        // Arrange
        var entity = new TestEntity
        {
            Position = new Vector2(0, 0),
            Destination = new Vector2(10, 0),
            Speed = 5f
        };
        double deltaTime = 1.0;

        // Act
        entity.UpdatePosition(deltaTime);

        // Assert
        Assert.True(entity.Position.X > 0 && entity.Position.X < 10);
    }

    [Fact]
    public void UpdatePosition_ShouldStopAtDestination()
    {
        // Arrange
        var entity = new TestEntity
        {
            Position = new Vector2(0, 0),
            Destination = new Vector2(10, 0),
            Speed = 15f
        };
        double deltaTime = 1.0;

        // Act
        entity.UpdatePosition(deltaTime);

        // Assert
        Assert.Equal(entity.Destination, entity.Position);
    }

    [Fact]
    public void UpdatePosition_ShouldNotMoveIfAlreadyAtDestination()
    {
        // Arrange
        var entity = new TestEntity
        {
            Position = new Vector2(10, 10),
            Destination = new Vector2(10, 10),
            Speed = 5f
        };
        double deltaTime = 1.0;

        // Act
        entity.UpdatePosition(deltaTime);

        // Assert
        Assert.Equal(entity.Destination, entity.Position); // No movement should occur
    }

    [Fact]
    public void UpdatePosition_ShouldNotOvershootDestination()
    {
        // Arrange
        var entity = new TestEntity
        {
            Position = new Vector2(0, 0),
            Destination = new Vector2(10, 0),
            Speed = 12f
        };
        double deltaTime = 1.0;

        // Act
        entity.UpdatePosition(deltaTime);

        // Assert
        Assert.Equal(entity.Destination, entity.Position); // Should exactly stop at the destination
    }

    [Fact]
    public void UpdatePosition_ShouldHandleSmallMovement()
    {
        // Arrange
        var entity = new TestEntity
        {
            Position = new Vector2(0, 0),
            Destination = new Vector2(0.01f, 0.01f),
            Speed = 0.1f
        };
        double deltaTime = 1.0;

        // Act
        entity.UpdatePosition(deltaTime);

        // Assert
        Assert.Equal(entity.Destination, entity.Position); // Should snap to the destination
    }
}
