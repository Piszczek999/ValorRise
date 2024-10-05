using Riptide;

namespace ValorRise.Server;

internal interface IMessageHandler
{
    void HandleMessage(ushort clientId, Message message);
}