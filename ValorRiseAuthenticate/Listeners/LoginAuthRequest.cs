using ValorRise;
using ValorRise.Enums;
using ValorRise.Packets.Authentication.Gateway;
using ValorRise.Packets.Authentication.Server;
using ValorRiseAuthenticate.MongoDB;

namespace ValorRiseAuthenticate.Listeners;

public class LoginAuthRequest
{
    private static readonly int _maxFailedAttempts = 5;
    private static readonly TimeSpan _lockoutDuration = TimeSpan.FromMinutes(5);

    [ClientPacketListener]
    public async void Listener(LoginAuthRequestPacket packet, ClientConnection connection)
    {
        var username = packet.Username.ToLower();
        var password = packet.Password;

        var user = await Database.UserRepository.GetByUsernameAsync(username);
        if (user == null)
        {
            Logger.Warning($"Authentication failed: User {username} not found.");
            connection.SendPacket(new LoginAuthResponsePacket(packet.ClientId, LoginResult.IncorrectCredentials));
            return;
        }

        if (user.IsLogged)
        {
            Logger.Debug($"Authentication failed: User {username} is already logged in.");
            connection.SendPacket(new LoginAuthResponsePacket(packet.ClientId, LoginResult.AlreadyLoggedIn));
            return;
        }

        // Check if user is locked out
        if (user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTime.UtcNow)
        {
            Logger.Debug($"Authentication failed: User {username} is locked out until {user.LockoutEnd.Value}.");
            connection.SendPacket(new LoginAuthResponsePacket(packet.ClientId, LoginResult.AccountLockedOut));
            return;
        }

        if (!BCryptPasswordHasher.Verify(password, user.PasswordHash))
        {
            user.FailedLoginAttempts += 1;

            if (user.FailedLoginAttempts >= _maxFailedAttempts)
            {
                user.LockoutEnd = DateTime.UtcNow.Add(_lockoutDuration);
                Logger.Debug($"User {username} has been locked out until {DateTime.UtcNow.Add(_lockoutDuration)}.");
            }

            await Database.UserRepository.UpdateAsync(user);

            Logger.Debug($"Authentication failed: Invalid password for user {username}. Failed attempts: {user.FailedLoginAttempts}.");
            connection.SendPacket(new LoginAuthResponsePacket(packet.ClientId, LoginResult.IncorrectCredentials));
            return;
        }

        // Reset failed attempts on successful login
        user.FailedLoginAttempts = 0;

        // Set IsLogged to true
        user.IsLogged = true;
        await Database.UserRepository.UpdateAsync(user);

        connection.SendPacket(new LoginAuthResponsePacket(packet.ClientId, LoginResult.Success, user.Id));
        Logger.Debug($"User {username} authenticated successfully.");

        var characters = await Database.CharacterInfoRepository.GetAllByUserIdAsync(user.Id);
        connection.SendPacket(new CharacterInfosAuthResponsePacket(packet.ClientId, characters.ToArray()));

    }
}
