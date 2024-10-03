namespace MMOLibrary;

using System.Collections;
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
            public static Message RegisterResult(RegisterResult result)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToClient.RegisterResponse);
                message.AddByte((byte)result);
                return message;
            }

            public static Message LoginResult(LoginResult result)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToClient.LoginResponse);
                message.AddByte((byte)result);
                return message;
            }

            public static Message CharactersInfoResult(byte count, IEnumerable<CharacterInfo> characters)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToClient.CharactersInfoResponse);
                message.AddByte(count);
                foreach (var character in characters)
                {
                    message.AddCharacterInfo(character);
                }
                return message;
            }

            public static Message NewCharacterResult(NewCharacterResult result)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToClient.NewCharacterResponse);
                message.AddByte((byte)result);
                return message;
            }

            public static Message CharacterSelectResult(CharacterSelectResult result, Character character)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToClient.CharacterSelectResponse);
                message.AddByte((byte)result);
                message.AddCharacter(character);
                return message;
            }
        }
        public static class ToAuthenticate
        {
            public static Message RegisterAuthRequest(ushort clientId, string username, string password)
            {
                Message message = Message.Create(MessageSendMode.Reliable, MessageType.ToAuthenticate.RegisterAuthRequest);
                message.AddUShort(clientId);
                message.AddString(username);
                message.AddString(password);
                return message;
            }

            public static Message LoginAuthRequest(ushort clientId, string username, string password)
            {
                Message message = Message.Create(MessageSendMode.Reliable, MessageType.ToAuthenticate.LoginAuthRequest);
                message.AddUShort(clientId);
                message.AddString(username);
                message.AddString(password);
                return message;
            }

            public static Message NewCharacterAuthRequest(ushort clientId, ObjectId userId, string name)
            {
                Message message = Message.Create(MessageSendMode.Reliable, MessageType.ToAuthenticate.NewCharacterAuthRequest);
                message.AddUShort(clientId);
                message.AddObjectId(userId);
                message.AddString(name);
                return message;
            }

            public static Message CharacterSelectAuthRequest(ushort clientId, ObjectId userId, ObjectId characterId)
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
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.LoginAuthResponse);
                message.AddUShort(clientId);
                message.AddByte((byte)result);
                message.AddObjectId(userId);
                return message;
            }

            public static Message RegisterResponse(ushort clientId, RegisterResult result)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.RegisterAuthResponse);
                message.AddUShort(clientId);
                message.AddByte((byte)result);
                return message;
            }

            public static Message CharactersInfoResponse(ushort clientId, byte count, IEnumerable<CharacterInfo> characters)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.CharactersInfoAuthResponse);
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
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.NewCharacterAuthResponse);
                message.AddUShort(clientId);
                message.AddByte((byte)result);
                return message;
            }

            public static Message CharacterSelectResponse(ushort clientId, CharacterSelectResult result, Character character)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToGateway.CharacterSelectAuthResponse);
                message.AddUShort(clientId);
                message.AddByte((byte)result);
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