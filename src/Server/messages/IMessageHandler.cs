namespace MMOLibrary.Server;
using Riptide;

internal interface IMessageHandler
{
    void HandleMessage(ushort clientId, Message message);
}