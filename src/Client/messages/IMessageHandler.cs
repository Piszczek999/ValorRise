namespace MMOLibrary.Client;
using Riptide;

internal interface IMessageHandler
{
    void HandleMessage(Message message);
}