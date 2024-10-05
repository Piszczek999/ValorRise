using MongoDB.Bson;
using Riptide;

namespace ValorRise.Client;

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
            public static Message VerifyTokenRequest(string token)
            {
                var message = Message.Create(MessageSendMode.Reliable, MessageType.ToGameServer.VerifyTokenRequest);
                message.AddString(token);
                return message;
            }
        }
    }

    public static class FromGateway
    {
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

    public static class FromGameServer
    {
        public static class ToAuthenticate
        {

        }
    }
}