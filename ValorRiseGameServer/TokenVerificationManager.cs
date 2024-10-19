using ValorRise.Models;
using ValorRiseGameServer.Entities;

namespace ValorRiseGameServer;

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
    void InitToken(string token, Character character);

    /// <summary>
    /// Starts the verification process for a given player connection.
    /// This method should be called when a player connects
    /// and is expected to verify their token.
    /// </summary>
    void Start(PlayerConnection connection);

    /// <summary>
    /// Verifies a token for a given player connection.
    /// If the token is valid and not expired, the associated player is returned.
    /// </summary>
    Character VerifyToken(PlayerConnection connection, string token);
}

public class TokenVerificationManager : ITokenVerificationManager
{
    private readonly Dictionary<string, Character> _expectedTokens = new();
    private readonly Dictionary<PlayerConnection, long> _awaitingVerifications = new();

    private long _lastVerificationCheckTime = 0;
    private long _lastTokenCheckTime = 0;

    public void InitToken(string token, Character character)
    {
        _expectedTokens.Add(token, character);
    }

    public void Update()
    {
        long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        // Check for expired verifications every 10 seconds
        if (currentTime - _lastVerificationCheckTime >= 10)
        {
            VerifinationExpirationTimeout();
            _lastVerificationCheckTime = currentTime;
        }

        // Check for expired tokens every 10 seconds
        if (currentTime - _lastTokenCheckTime >= 10)
        {
            TokenExpirationTimeout();
            _lastTokenCheckTime = currentTime;
        }
    }

    public void Start(PlayerConnection connection)
    {
        _awaitingVerifications[connection] = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }

    public Character VerifyToken(PlayerConnection connection, string token)
    {
        long tokenTime = long.Parse(token[64..]); // Right 64 chars
        long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        if (currentTime - tokenTime <= 30 && _expectedTokens.TryGetValue(token, out Character character))
        {
            _awaitingVerifications.Remove(connection);
            _expectedTokens.Remove(token);
            return character; // Token verified
        }

        _awaitingVerifications.Remove(connection);
        return null; // Token not verified
    }

    private void VerifinationExpirationTimeout()
    {
        RemoveExpiredClients(30);
    }

    private void TokenExpirationTimeout()
    {
        RemoveExpiredTokens(30);
    }

    private void RemoveExpiredClients(int timeoutDuration)
    {
        long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        List<PlayerConnection> clientsToRemove = new();

        foreach (var client in _awaitingVerifications)
        {
            if (currentTime - client.Value >= timeoutDuration)
            {
                clientsToRemove.Add(client.Key);
            }
        }

        foreach (var client in clientsToRemove)
        {
            _awaitingVerifications.Remove(client);
            client.Disconnect();
        }
    }

    private void RemoveExpiredTokens(int timeoutDuration)
    {
        long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        List<string> tokensToRemove = new();

        foreach (var token in _expectedTokens.Keys)
        {
            long tokenTime = long.Parse(token.Substring(token.Length - 64)); // Right 64 chars
            if (currentTime - tokenTime >= timeoutDuration)
            {
                tokensToRemove.Add(token);
            }
        }

        foreach (var token in tokensToRemove)
        {
            _expectedTokens.Remove(token);
        }
    }
}
