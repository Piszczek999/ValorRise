namespace MMO_Library.Server;
using Riptide;

internal interface IMessageHandler
{
    void HandleMessage(ushort clientId, Message message);
}