using MongoDB.Bson;
using ValorRiseServer.Entities;

namespace ValorRiseServer;
/// <summary>
/// Defines the contract for managing entities within the system.
/// </summary>
public interface IEntityManager
{
    /// <summary>
    /// Adds a new entity to the manager.
    /// </summary>
    /// <param name="entity">The entity to be added.</param>
    /// <returns>The added <see cref="Entity"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="entity"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when an entity with the same <see cref="Entity.Id"/> already exists.</exception>
    Entity AddEntity(Entity entity);

    /// <summary>
    /// Removes an existing entity from the manager by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to remove.</param>
    /// <returns><c>true</c> if the entity was successfully removed; otherwise, <c>false</c>.</returns>
    bool RemoveEntity(ObjectId id);

    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to retrieve.</param>
    /// <returns>
    /// The <see cref="Entity"/> if found; otherwise, <c>null</c>.
    /// </returns>
    Entity GetEntity(ObjectId id);

    /// <summary>
    /// Retrieves all entities currently managed.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{Entity}"/> containing all managed entities.</returns>
    IEnumerable<Entity> GetEntities();
}