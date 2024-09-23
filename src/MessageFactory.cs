namespace MMO_Library;
using Riptide;

public class MessageFactory
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

        public static Message CharactersInfo(byte count, List<CharacterInfo> characters)
        {
            var message = Message.Create(MessageSendMode.Reliable, MessageType.ToClient.CharactersInfo);
            message.AddByte(count);
            foreach (var character in characters)
            {
                message.AddCharacterInfo(character);
            }
            return message;
        }
    }

    public static class ToAuthenticate
    {
        public static Message RegisterRequest(ushort connectionId, string username, string password)
        {
            Message message = Message.Create(MessageSendMode.Reliable, MessageType.ToAuthenticate.RegisterRequest);
            message.AddUShort(connectionId);
            message.AddString(username);
            message.AddString(password);
            return message;
        }

        public static Message LoginRequest(ushort connectionId, string username, string password)
        {
            Message message = Message.Create(MessageSendMode.Reliable, MessageType.ToAuthenticate.LoginRequest);
            message.AddUShort(connectionId);
            message.AddString(username);
            message.AddString(password);
            return message;
        }
    }
}
