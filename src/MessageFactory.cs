namespace MMO_Library;

using MongoDB.Bson;
using Riptide;

public static class MessageFactory
{
    public static class FromClient
    {
        public static class ToGateway
        {
            public static Message LoginRequest(string username, string password)
            {
                Message message = Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.LoginRequest);
                message.AddString(username);
                message.AddString(password);
                return message;
            }

            public static Message RegisterRequest(string username, string password)
            {
                Message message = Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.RegisterRequest);
                message.AddString(username);
                message.AddString(password);
                return message;
            }

            public static Message NewCharacterRequest(string name)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.NewCharacterRequest);
                message.AddString(name);
                return message;
            }

            public static Message CharacterSelectRequest(ObjectId characterId)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.CharacterSelectRequest);
                message.AddObjectId(characterId);
                return message;
            }
        }
        public static class ToGameServer
        {

        }
    }

    public static class FromGateway
    {
        public static class ToClient
        {
            public static Message RegisterResponse(RegisterResult result)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToClient.RegisterResult);
                message.AddByte((byte)result);
                return message;
            }

            public static Message LoginResponse(LoginResult result)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToClient.LoginResult);
                message.AddByte((byte)result);
                return message;
            }

            public static Message CharactersInfo(byte count, List<CharacterInfo> characters)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToClient.CharactersInfoResult);
                message.AddByte(count);
                foreach (var character in characters)
                {
                    message.AddCharacterInfo(character);
                }
                return message;
            }

            public static Message NewCharacterResponse(NewCharacterResult result)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToClient.NewCharacterResult);
                message.AddByte((byte)result);
                return message;
            }

            public static Message Character(Character character)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToClient.CharacterSelectResult);
                message.AddCharacter(character);
                return message;
            }
        }
        public static class ToAuthenticate
        {
            public static Message RegisterRequest(ushort clientId, string username, string password)
            {
                Message message = Message.Create(MessageSendMode.Reliable, MessageType.ToAuthenticate.RegisterAuthRequest);
                message.AddUShort(clientId);
                message.AddString(username);
                message.AddString(password);
                return message;
            }

            public static Message LoginRequest(ushort clientId, string username, string password)
            {
                Message message = Message.Create(MessageSendMode.Reliable, MessageType.ToAuthenticate.LoginAuthRequest);
                message.AddUShort(clientId);
                message.AddString(username);
                message.AddString(password);
                return message;
            }

            public static Message NewCharacterRequest(ushort clientId, ObjectId userId, string name)
            {
                Message message = Message.Create(MessageSendMode.Reliable, MessageType.ToAuthenticate.NewCharacterAuthRequest);
                message.AddUShort(clientId);
                message.AddObjectId(userId);
                message.AddString(name);
                return message;
            }

            public static Message SelectCharacterRequest(ushort clientId, ObjectId userId, ObjectId characterId)
            {
                Message message = Message.Create(MessageSendMode.Reliable, MessageType.ToAuthenticate.CharacterSelectAuthRequest);
                message.AddUShort(clientId);
                message.AddObjectId(userId);
                message.AddObjectId(characterId);
                return message;
            }
        }
    }

    public static class FromAuthenticate
    {
        public static class ToGateway
        {

            public static Message LoginResponse(ushort clientId, LoginResult result, ObjectId userId = default)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.LoginResponse);
                message.AddUShort(clientId);
                message.AddByte((byte)result);
                message.AddObjectId(userId);
                return message;
            }

            public static Message RegisterResponse(ushort clientId, RegisterResult result)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.RegisterResponse);
                message.AddUShort(clientId);
                message.AddByte((byte)result);
                return message;
            }

            public static Message CharactersInfo(ushort clientId, byte count, CharacterInfo[] characters)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.CharactersInfoResponse);
                message.AddUShort(clientId);
                message.AddByte(count);
                foreach (var character in characters)
                {
                    message.AddCharacterInfo(character);
                }
                return message;
            }

            public static Message NewCharacterResponse(ushort clientId, NewCharacterResult result)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.NewCharacterResponse);
                message.AddUShort(clientId);
                message.AddByte((byte)result);
                return message;
            }

            public static Message CharacterSelectResponse(Character character)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.CharacterSelectResponse);
                message.AddCharacter(character);
                return message;
            }
        }
        public static class ToGameServer
        {

        }
    }

    public static class FromGameServer
    {
        public static class ToClient
        {

        }
        public static class ToAuthenticate
        {

        }
    }
}