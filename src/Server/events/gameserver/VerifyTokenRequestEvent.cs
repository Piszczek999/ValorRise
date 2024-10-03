namespace MMOLibrary.Server;
using MongoDB.Bson;
using Riptide;

public class VerifyTokenRequestEvent : EventArgs
{
    public Connection Client { get; }
    public string Token { get; }

    public VerifyTokenRequestEvent(Connection client, string token)
    {
        Client = client;
        Token = token;
    }
}