using MongoDB.Bson;
using ValorRise;
using ValorRise.Enums;
using ValorRise.Models;
using ValorRise.Packets.Authentication.Gateway;
using ValorRise.Packets.Authentication.Server;
using ValorRiseAuthenticate;
using ValorRiseAuthenticate.MongoDB;

namespace ValorRiseAuthenticate.Listeners;

public class RegisterAuthRequest
{
    [ClientPacketListener]
    public async void Listener(RegisterAuthRequestPacket packet, ClientConnection connection)
    {
        var username = packet.Username.ToLower();
        var password = packet.Password;

        if (!Validator.AreCredentialsValid(username, password))
        {
            connection.SendPacket(new RegisterAuthResponsePacket(packet.ClientId, RegisterResult.InvalidCredentials));
            return;
        }

        if (await Database.UserRepository.GetByUsernameAsync(username) != null)
        {
            connection.SendPacket(new RegisterAuthResponsePacket(packet.ClientId, RegisterResult.UsernameTaken));
            return;
        }

        var salt = BCryptPasswordHasher.GenerateSalt();
        var hashedPassword = BCryptPasswordHasher.HashPassword(password, salt);

        User newUser = new User
        {
            Username = username,
            Email = "johndoe@example.com", // Adjust this to accept an email parameter
            PasswordHash = hashedPassword,
            Salt = salt,
            Roles = new[] { "user" },
            CreatedAt = DateTime.UtcNow
        };

        var result = RegisterResult.Success;
        try
        {
            await Database.UserRepository.CreateAsync(newUser);
        }
        catch
        {
            result = RegisterResult.ServerError;
        }

        connection.SendPacket(new RegisterAuthResponsePacket(packet.ClientId, result));
    }
}
