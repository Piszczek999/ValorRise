namespace ValorRiseClient.Messages;
using Riptide;

internal interface IMessageHandler
{
    void HandleMessage(Message message);
}