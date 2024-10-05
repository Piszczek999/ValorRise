namespace ValorRiseServer;
using Riptide;

internal interface IMessageHandler
{
    void HandleMessage(ushort clientId, Message message);
}