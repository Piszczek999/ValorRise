using ValorRise.Models;
using ValorRiseGameServer.Entities;

namespace ValorRiseGameServer;
/// <summary>
/// Interface for managing token verification and client session management.
/// </summary>
public interface ITokenVerificationManager
{
    /// <summary>
    /// Updates the state of the token verification manager,
    /// checking for expired verifications and tokens.
    /// This method should be called periodically (e.g., every second).
    /// </summary>
    void Update();

    /// <summary>
    /// Adds a new token to the token verification manager
    /// </summary>
    /// <param name="token">The token to be verified.</param>
    /// <param name="character">Character associated with token</param>
    void InitToken(string token, Character character);

    /// <summary>
    /// Starts the verification process for a given player connection.
    /// This method should be called when a player connects
    /// and is expected to verify their token.
    /// </summary>
    /// <param name="connection">The player connection awaiting verification.</param>
    void Start(PlayerConnection connection);

    /// <summary>
    /// Verifies a token for a given player connection.
    /// If the token is valid and not expired, the associated player is returned.
    /// </summary>
    /// <param name="connection">The player connection requesting token verification.</param>
    /// <param name="token">The token to be verified.</param>
    /// <returns>
    /// The verified <see cref="Character"/> if the token is valid; otherwise, <c>null</c>.
    /// </returns>
    Character VerifyToken(PlayerConnection connection, string token);
}