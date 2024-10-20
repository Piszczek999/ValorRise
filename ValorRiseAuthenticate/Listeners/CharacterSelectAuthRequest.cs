using ValorRise;
using ValorRise.Enums;
using ValorRise.Packets.Authentication.Gateway;
using ValorRise.Packets.Authentication.Server;
using ValorRiseAuthenticate;
using ValorRiseAuthenticate.MongoDB;

namespace ValorRiseAuthenticate.Listeners;

public class CharacterSelectAuthRequest
{
    [ClientPacketListener]
    public async void Listener(CharacterSelectAuthRequestPacket packet, ClientConnection connection)
    {
        Logger.Debug($"CharacterId requested: {packet.CharacterId}");
        var character = await Database.CharacterRepository.GetByIdAsync(packet.CharacterId);
        if (character == null || character.UserId != packet.UserId)
        {
            var _packet = new CharacterSelectAuthResponsePacket(packet.ClientId, CharacterSelectResult.CharacterNotFound);
            connection.SendPacket(_packet);
            return;
        }

        if (!ValorServer.GameServerManager.TryGetServerByMap((Map)character.MapId, out var gameServer))
        {
            var _packet = new CharacterSelectAuthResponsePacket(packet.ClientId, CharacterSelectResult.ServerIsDown);
            connection.SendPacket(_packet);
            return;
        }

        var token = TokenGenerator.GenerateToken();
        var successPacket = new CharacterSelectAuthResponsePacket(packet.ClientId, CharacterSelectResult.Success, token, gameServer.HostAddress);
        connection.SendPacket(successPacket);
        gameServer.Connection.SendPacket(new CharacterTokenPacket(token, character));
    }
}
