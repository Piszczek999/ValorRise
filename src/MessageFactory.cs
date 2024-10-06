using MongoDB.Bson;
using Riptide;
using ValorRise.Server.Entities;

namespace ValorRise;

public static class MessageFactory
{
    #region FromClient
    public static class FromClient
    {
        public static class ToGateway
        {
            public static Message LoginRequest(string username, string password)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.LoginRequest)
                    .AddString(username)
                    .AddString(password);
            }

            public static Message RegisterRequest(string username, string password)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.RegisterRequest)
                    .AddString(username)
                    .AddString(password);
            }

            public static Message NewCharacterRequest(string name)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.NewCharacterRequest)
                    .AddString(name);
            }

            public static Message CharacterSelectRequest(ObjectId characterId)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.CharacterSelectRequest)
                    .AddObjectId(characterId);
            }
        }
        public static class ToGameServer
        {
            public static Message VerifyTokenRequest(string token)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToGameServer.VerifyTokenRequest)
                    .AddString(token);
            }

            public static Message PlayerMove(float x, float y)
            {
                return Message.Create(MessageSendMode.Unreliable, MessageType.ToGameServer.PlayerMove)
                    .AddFloat(x)
                    .AddFloat(y);
            }
        }
    }
    #endregion
    #region FromGateway
    public static class FromGateway
    {
        public static class ToClient
        {
            public static Message RegisterResponse(RegisterResult result)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToClient.RegisterResponse)
                    .AddByte((byte)result);
            }

            public static Message LoginResponse(LoginResult result)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToClient.LoginResponse)
                    .AddByte((byte)result);
            }

            public static Message CharactersInfoResponse(CharacterInfo[] characterInfos)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToClient.CharactersInfoResponse)
                    .AddSerializables(characterInfos);
            }

            public static Message NewCharacterResponse(NewCharacterResult result)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToClient.NewCharacterResponse)
                    .AddByte((byte)result);
            }

            public static Message CharacterSelectResponse(string token, string ipAddress, ushort port)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToClient.CharacterSelectResponse)
                    .AddString(token)
                    .AddString(ipAddress)
                    .AddUShort(port);
            }
        }
        public static class ToAuthenticate
        {
            public static Message RegisterAuthRequest(ushort clientId, string username, string password)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToAuthenticate.RegisterAuthRequest)
                    .AddUShort(clientId)
                    .AddString(username)
                    .AddString(password);
            }

            public static Message LoginAuthRequest(ushort clientId, string username, string password)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToAuthenticate.LoginAuthRequest)
                    .AddUShort(clientId)
                    .AddString(username)
                    .AddString(password);
            }

            public static Message NewCharacterAuthRequest(ushort clientId, ObjectId userId, string name)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToAuthenticate.NewCharacterAuthRequest)
                    .AddUShort(clientId)
                    .AddObjectId(userId)
                    .AddString(name);
            }

            public static Message CharacterSelectAuthRequest(ushort clientId, ObjectId userId, ObjectId characterId)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToAuthenticate.CharacterSelectAuthRequest)
                    .AddUShort(clientId)
                    .AddObjectId(userId)
                    .AddObjectId(characterId);
            }
        }
    }
    #endregion
    #region FromAuthenticate
    public static class FromAuthenticate
    {
        public static class ToGateway
        {
            public static Message LoginAuthResponse(ushort clientId, LoginResult result, ObjectId userId = default)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.LoginAuthResponse)
                    .AddUShort(clientId)
                    .AddByte((byte)result)
                    .AddObjectId(userId);
            }

            public static Message RegisterAuthResponse(ushort clientId, RegisterResult result)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.RegisterAuthResponse)
                    .AddUShort(clientId)
                    .AddByte((byte)result);
            }

            public static Message CharactersInfoAuthResponse(ushort clientId, CharacterInfo[] characters)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.CharactersInfoAuthResponse)
                    .AddUShort(clientId)
                    .AddSerializables(characters);
            }

            public static Message NewCharacterAuthResponse(ushort clientId, NewCharacterResult result)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.NewCharacterAuthResponse)
                    .AddUShort(clientId)
                    .AddByte((byte)result);
            }

            public static Message CharacterSelectAuthResponse(ushort clientId, string token, string ipAddress, ushort port)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.CharacterSelectAuthResponse)
                    .AddUShort(clientId)
                    .AddString(token)
                    .AddString(ipAddress)
                    .AddUShort(port);
            }
        }
        public static class ToGameServer
        {
            public static Message PlayerToken(string token, Character character)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToGameServer.PlayerToken)
                    .AddString(token)
                    .AddSerializable(character);
            }

            public static Message GameServerInfoResponse(ushort mapId, ushort port)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToGameServer.GameServerInfoResponse)
                    .AddUShort(mapId)
                    .AddUShort(port);
            }
        }
    }
    #endregion
    #region FromGameServer
    public static class FromGameServer
    {
        public static class ToClient
        {
            public static Message VerifyTokenResponse(bool result)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToClient.VerifyTokenResponse)
                    .AddBool(result);
            }

            public static Message InitLevel(ushort mapId)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToClient.InitLevel)
                    .AddUShort(mapId);
            }

            public static Message InitMainPlayer(Player player)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToClient.InitMainPlayer)
                    .AddSerializable(player);
            }

            public static Message SpawnEntity(Entity entity)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToClient.SpawnEntity)
                    .AddSerializable(entity);
            }
        }
        public static class ToAuthenticate
        {
            public static Message GameServerInfoRequest(string ipAddress)
            {
                return Message.Create(MessageSendMode.Reliable, MessageType.ToAuthenticate.GameServerInfoRequest)
                    .AddString(ipAddress);
            }
        }
    }
    #endregion
}