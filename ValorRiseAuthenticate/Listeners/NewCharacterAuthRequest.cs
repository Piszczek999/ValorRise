using MongoDB.Bson;
using ValorRise;
using ValorRise.Enums;
using ValorRise.Models;
using ValorRise.Packets.Authentication.Gateway;
using ValorRise.Packets.Authentication.Server;
using ValorRiseAuthenticate;
using ValorRiseAuthenticate.MongoDB;

namespace ValorRiseAuthenticate.Listeners;

public class NewCharacterAuthRequest
{
    [ClientPacketListener]
    public async void Listener(NewCharacterAuthRequestPacket packet, ClientConnection connection)
    {
        if (await Database.CharacterRepository.GetByNameAsync(packet.Name) != null)
        {
            connection.SendPacket(new NewCharacterAuthResponsePacket(packet.ClientId, NewCharacterResult.NameTaken));
            return;
        }

        var characters = await Database.CharacterInfoRepository.GetAllByUserIdAsync(packet.UserId);
        if (characters.Count >= 3)
        {
            connection.SendPacket(new NewCharacterAuthResponsePacket(packet.ClientId, NewCharacterResult.CharacterLimitReached));
            return;
        }

        var newCharacter = new Character()
        {
            Id = ObjectId.GenerateNewId(),
            UserId = packet.UserId,
            Name = packet.Name,
            Level = 1,
            Exp = 0,
            MapId = 0,
            Position = System.Numerics.Vector2.Zero,
            Gold = 100,
            Health = 100,
            MaxHealth = 100,
            Mana = 100,
            MaxMana = 100,
            IsDead = false,
            CreatedAt = DateTime.UtcNow
        };

        var newCharacterInfo = CharacterInfo.FromCharacter(newCharacter);

        await Database.CharacterRepository.CreateAsync(newCharacter);
        await Database.CharacterInfoRepository.CreateAsync(newCharacterInfo);

        connection.SendPacket(new NewCharacterAuthResponsePacket(packet.ClientId, NewCharacterResult.Success));

        Logger.Debug("UserId: " + packet.UserId);
        characters = await Database.CharacterInfoRepository.GetAllByUserIdAsync(packet.UserId);
        connection.SendPacket(new CharacterInfosAuthResponsePacket(packet.ClientId, characters.ToArray()));
    }
}
