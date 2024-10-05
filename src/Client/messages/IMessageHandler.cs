using Riptide;

namespace ValorRise.Client.Messages;

internal interface IMessageHandler
{
    void HandleMessage(Message message);
}