using MongoDB.Bson;
using Riptide;
using ValorRise.Server.Entities;

namespace ValorRise.Server;

public static class MessageFactory
{
    public static class FromGateway
    {
        public static class ToClient
        {
            public static Message RegisterResponse(RegisterResult result)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToClient.RegisterResponse);
                message.AddByte((byte)result);
                return message;
            }

            public static Message LoginResponse(LoginResult result)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToClient.LoginResponse);
                message.AddByte((byte)result);
                return message;
            }

            public static Message CharactersInfoResponse(CharacterInfo[] characters)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToClient.CharactersInfoResponse);
                foreach (var character in characters)
                {
                    message.AddCharacterInfo(character);
                }
                return message;
            }

            public static Message NewCharacterResponse(NewCharacterResult result)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToClient.NewCharacterResponse);
                message.AddByte((byte)result);
                return message;
            }

            public static Message CharacterSelectResponse(string token, string ipAddress)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToClient.CharacterSelectResponse);
                message.AddString(token);
                message.AddString(ipAddress);
                return message;
            }
        }
    }

    public static class FromAuthenticate
    {
        public static class ToGateway
        {
            public static Message LoginAuthResponse(ushort clientId, LoginResult result, ObjectId userId = default)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.LoginAuthResponse);
                message.AddUShort(clientId);
                message.AddByte((byte)result);
                message.AddObjectId(userId);
                return message;
            }

            public static Message RegisterAuthResponse(ushort clientId, RegisterResult result)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.RegisterAuthResponse);
                message.AddUShort(clientId);
                message.AddByte((byte)result);
                return message;
            }

            public static Message CharactersInfoAuthResponse(ushort clientId, List<CharacterInfo> characters)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.CharactersInfoAuthResponse);
                message.AddUShort(clientId);
                foreach (var character in characters)
                {
                    message.AddCharacterInfo(character);
                }
                return message;
            }

            public static Message NewCharacterAuthResponse(ushort clientId, NewCharacterResult result)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.NewCharacterAuthResponse);
                message.AddUShort(clientId);
                message.AddByte((byte)result);
                return message;
            }

            public static Message CharacterSelectAuthResponse(ushort clientId, string token, string ipAddress)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.CharacterSelectAuthResponse);
                message.AddUShort(clientId);
                message.AddString(token);
                message.AddString(ipAddress);
                return message;
            }
        }
        public static class ToGameServer
        {
            public static Message PlayerToken(string token, Character character)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToGameServer.PlayerToken);
                message.AddString(token);
                message.AddCharacter(character);
                return message;
            }
        }
    }

    public static class FromGameServer
    {
        public static class ToClient
        {
            public static Message VerifyTokenResponse(bool result)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToClient.VerifyTokenResponse);
                message.AddBool(result);
                return message;
            }
            public static Message InitLevel(ushort mapId)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToClient.InitLevel);
                message.AddUShort(mapId);
                return message;
            }
            public static Message InitMainPlayer(Character character)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToClient.InitMainPlayer);
                message.AddCharacter(character);
                return message;
            }
            public static Message SpawnEntity(Entity entity)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToClient.SpawnEntity);
                entity.Serialize(message);
                return message;
            }
        }
    }
}