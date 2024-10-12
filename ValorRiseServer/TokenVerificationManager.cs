using ValorRiseServer.Entities;

namespace ValorRiseServer;

public class TokenVerificationManager : ITokenVerificationManager
{
    private readonly Dictionary<string, Player> ExpectedTokens = new();
    private readonly Dictionary<PlayerConnection, long> AwaitingVerifications = new();

    private long _lastVerificationCheckTime = 0;
    private long _lastTokenCheckTime = 0;

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
        AwaitingVerifications[connection] = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }

    public Player VerifyToken(PlayerConnection connection, string token)
    {
        long tokenTime = long.Parse(token[^64..]); // Right 64 chars
        long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        if (currentTime - tokenTime <= 30 && ExpectedTokens.TryGetValue(token, out Player player))
        {
            AwaitingVerifications.Remove(connection);
            ExpectedTokens.Remove(token);
            return player; // Token verified
        }

        AwaitingVerifications.Remove(connection);
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

        foreach (var client in AwaitingVerifications)
        {
            if (currentTime - client.Value >= timeoutDuration)
            {
                clientsToRemove.Add(client.Key);
            }
        }

        foreach (var client in clientsToRemove)
        {
            AwaitingVerifications.Remove(client);
            client.Disconnect();
        }
    }

    private void RemoveExpiredTokens(int timeoutDuration)
    {
        long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        List<string> tokensToRemove = new();

        foreach (var token in ExpectedTokens.Keys)
        {
            long tokenTime = long.Parse(token.Substring(token.Length - 64)); // Right 64 chars
            if (currentTime - tokenTime >= timeoutDuration)
            {
                tokensToRemove.Add(token);
            }
        }

        foreach (var token in tokensToRemove)
        {
            ExpectedTokens.Remove(token);
        }
    }
}
